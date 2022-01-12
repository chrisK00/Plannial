using System.Threading.Tasks;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserAsync(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task AddUserAsync(AppUser user, string password);
    }
}
