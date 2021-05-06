﻿using System.Threading.Tasks;
using Plannial.Core.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(AppUser user);
        Task<AppUser> GetUserAsync(string id);
    }
}
