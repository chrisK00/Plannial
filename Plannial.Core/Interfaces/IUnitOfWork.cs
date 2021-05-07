using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
