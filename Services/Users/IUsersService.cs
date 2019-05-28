using Apka2.DTOS;
using Apka2.Entities;
using System.Threading.Tasks;

namespace Apka2.Services.Users
{
    public interface IUsersService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user);
    }
}
