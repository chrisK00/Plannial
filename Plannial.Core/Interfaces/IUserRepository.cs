using System;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(AppUser user);
        Task<AppUser> GetUserAsync(Guid id);
        Task<AppUser> GetUserByEmailAsync(string email);
    }
}
