using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IOwnedEntityAuthService
    {
        Task OwnsAsync<T>(int entityId, string userId, CancellationToken cancellationToken) where T : BaseOwnedEntity;
    }
}
