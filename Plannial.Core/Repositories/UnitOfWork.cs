﻿using System.Threading.Tasks;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;

namespace Plannial.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}