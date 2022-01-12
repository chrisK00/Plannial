using Plannial.Data.Models.Params;
using Plannial.Data.Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Data.Interfaces
{
    public interface IMessageReadRepository
    {
        Task<IEnumerable<MessageResponse>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default);
    }
}
