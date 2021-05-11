using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
