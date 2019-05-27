using Apka2.DAL;
using Apka2.Helpers;
using Apka2.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Apka2.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        void Register(User userParam);
        void Update(User user);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 0, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test1", Password = "test" },
        //    new User { Id = 2, FirstName = "Test", LastName = "User", Username = "test2", Password = "test" },
        //    new User { Id = 3, FirstName = "Test", LastName = "User", Username = "test3", Password = "test" },
        //    new User { Id = 4, FirstName = "Test", LastName = "User", Username = "test4", Password = "test" },
        //    new User { Id = 5, FirstName = "Test", LastName = "User", Username = "test5", Password = "test" }

        //};

        private readonly AppSettings _appSettings;
        private readonly IUser _users;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _users = new UserEntity();

        }

        public User Authenticate(string username, string password)
        {
            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            var user = _users.GetSavedUsers().SingleOrDefault(x => x.Username == username && x.Password == password);


            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public void Delete(int id)
        {
             _users.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            //return _users.Select(x => {
            //    x.Password = null;
            //    return x;
            //});

            return _users.GetSavedUsers()
                .Select(x =>
                {
                    x.Password = null;
                    return x;
                });

        }

        public void Register(User userParam)
        {
            var temp = new User
            {
                FirstName = userParam.FirstName,
                LastName = userParam.LastName,
                Username = userParam.Username,
                Password = userParam.Password,
                Role = Role.User
                

            };

            _users.SaveUser(temp);


        }

        public void Update(User user)
        {
            _users.Update(user);
        }
    }
}
