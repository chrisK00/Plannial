using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Data;
using Plannial.Core.Entities;
using Plannial.Core.Interfaces;

namespace Plannial.Core.Services
{
    public class OwnedEntityAuthService : IOwnedEntityAuthService
    {
        private readonly DataContext _context;
        private readonly ILogger<OwnedEntityAuthService> _logger;

        public OwnedEntityAuthService(DataContext context, ILogger<OwnedEntityAuthService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OwnsAsync<T>(int entityId, string userId, CancellationToken cancellationToken) where T : BaseOwnedEntity
        {
            var IsOwner = await _context.Set<T>().AnyAsync(x => x.Id == entityId && x.UserId == userId, cancellationToken);

            if (!IsOwner)
            {
                _logger.LogError($"User {userId} does not own {entityId} of type {typeof(T)}");
                throw new UnauthorizedAccessException("You dont own this item");
            }
        }
    }
}
