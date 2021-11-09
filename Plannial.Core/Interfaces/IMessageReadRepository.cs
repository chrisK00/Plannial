using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Interfaces
{
    public interface IMessageReadRepository
    {
        Task<IEnumerable<MessageResponse>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default);
    }
}
