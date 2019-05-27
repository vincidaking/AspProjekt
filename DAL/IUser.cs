using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.DAL
{
    interface IUser
    {
        IEnumerable<User> GetSavedUsers();
        void SaveUser(User user);
        void Update(User user);
        void Delete(int id);

    }
}
