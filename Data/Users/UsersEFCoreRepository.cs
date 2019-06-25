using Apka2.Model;
using Apka2.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Apka2.Data.Users
{
    public class UsersEFCoreRepository : IUsersRepository
    {
        private Context _context;
        

        public UsersEFCoreRepository(Context context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            //throw new Exception("Nie istnieje użytkownik o takim Id");
            return user;
        }




        public async Task<User> AddAsync(User user)
        {
            var temp = await _context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

            if (temp != null)
                return null;
            //TODO FluentValidation


            User addedUser;
            addedUser = (await _context.Users.AddAsync(user)).Entity;
            await _context.SaveChangesAsync();
            return addedUser;
        }

        public async Task<User> UpdateAsync(User user)
        {
            //FluentValidation

            var original = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == user.Id);
            if (original == null)
                //return null;
                throw new Exception("Nie istnieje użytkownik o takim Id");

            user.Role = RoleNames.User;
            user.Password = HashPassword(user.Password);
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (userToDelete == null)
                throw new Exception("Nie istnieje użytkownik o takim Id");

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;

            //throw new Exception("Nie istnieje użytkownik o takim Id");
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


    }
}
