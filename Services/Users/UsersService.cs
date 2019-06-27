using Apka2.Data.Users;
using Apka2.Model;
using Apka2.Services.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _usersRepository.GetUsername(username);
            if (user == null) return null;

            byte[] hashBytes = Convert.FromBase64String(user.Password);

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);


            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return null;


            //var user = await _usersRepository
            //    .GetAsync(username, password);
            //if (user == null) return null;

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
                             
            //TODO resetowanie hasła
            //TODO ładniejszy wygląd 


            //TODO przeladowywanie tokena
         
            return user;
        }

        

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
            
        }

        public async Task<User> RegisterAsync(User user)
        {
           

            user.Role = RoleNames.User;
            user.Password = HashPassword(user.Password);
            var registeredUser = (await _usersRepository.AddAsync(user));
            return registeredUser;
        }

    }
}
