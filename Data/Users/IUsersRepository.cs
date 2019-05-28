using Apka2.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apka2.Data.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string username, string password);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
