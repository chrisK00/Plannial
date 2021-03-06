using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return;
            }

            var sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.Append(error.Description);
            }

            throw new InvalidOperationException(sb.ToString());
        }

        public async Task<AppUser> GetUserAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email.Trim());
        }
    }
}
