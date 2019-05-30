using Apka2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                throw new Exception("Nie istnieje użytkownik o takim Id");
            return user;
        }

        public async Task<User> AddAsync(User user)
        {
            //FluentValidation
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
                throw new Exception("Nie istnieje użytkownik o takim Id");

            user.Role = RoleNames.User;
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


    }
}
