using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddUserAsync(AppUser user)
        {
            await _context.AddAsync(user);
        }
    }
}
