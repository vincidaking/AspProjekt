using Apka2.Entities;
using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.DAL
{
    public class UserEntity : IUser, IDisposable
    {
        private Context _context = new Context();



        public IEnumerable<User> GetSavedUsers()
        {
            _context.Database.EnsureCreated();

            return _context.User.ToList();

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            //if (string.IsNullOrWhiteSpace(user.Password))
            //    throw new AppException("Password is required");

            //if (_context.Users.Any(x => x.Username == user.Username))
            //    throw new AppException("Username \"" + user.Username + "\" is already taken");

            
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            var temp = _context.User.Find(user.Id);

            
            if(user.LastName!=null)
                temp.LastName = user.LastName;
            if(user.FirstName!=null)
                temp.FirstName =user.FirstName;
            if (user.Username != null)
                temp.Username = user.Username;
            if (user.Password != null)
                temp.Password = user.Password;

            

            _context.User.Update(temp);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }


        }
    }
}
