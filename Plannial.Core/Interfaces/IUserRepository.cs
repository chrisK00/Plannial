using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserAsync(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
    }
}
