
using Apka2.Model;
using System.Threading.Tasks;

namespace Apka2.Services.Users
{
    public interface IUsersService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user);
        

        string HashPassword(string password);
        


    }
}
