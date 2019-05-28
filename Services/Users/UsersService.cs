using Apka2.Data.Users;
using Apka2.DTOS;
using Apka2.Entities;
using Apka2.Model;
using Apka2.Services.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Apka2.Services
{

    public class UsersService : IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly IUsersRepository _usersRepository;

        public UsersService(IOptions<AppSettings> appSettings, IUsersRepository usersRepository)
        {
            _appSettings = appSettings.Value;
            _usersRepository = usersRepository;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _usersRepository
                .GetAsync(username, password);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
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


        public async Task<User> RegisterAsync(User user)
        {
            //mappowanie na entity
            //var entity = _mapper.Map<User>(user);
            //User entity = new User();
            //entity.LastName = user.LastName;
            user.Role = RoleNames.User;
            var registeredUser = (await _usersRepository.AddAsync(user));
            return registeredUser;
        }
    }
}
