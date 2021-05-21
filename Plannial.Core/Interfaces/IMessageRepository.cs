using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message, CancellationToken cancellationToken);
        Task<IEnumerable<MessageResponse>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default);
        Task<IEnumerable<MessageResponse>> GetMessageThreadAsync(string userId, string otherUserId, CancellationToken cancellationToken = default);
        void RemoveMessage(Message message);

        /// <summary>
        /// Gets a message
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns>A message if found otherwise null</returns>
        Task<Message> GetMessage(int messageId, string userId);
    }
}
