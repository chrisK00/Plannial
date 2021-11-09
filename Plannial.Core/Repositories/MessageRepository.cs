using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _context.Messages.AddAsync(message, cancellationToken);
        }

        public async Task<Message> GetMessage(int messageId, string userId)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.SenderId == userId
            || (x.RecipientId == userId && x.Id == messageId));
        }

        public async Task<IEnumerable<Message>> GetMessageThreadAsync(string userId, string otherUserId, CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages
                .Where(x => (x.RecipientId == userId && !x.RecipientDeleted && x.SenderId == otherUserId)
                || (x.SenderId == userId && !x.SenderDeleted && x.RecipientId == otherUserId)).ToListAsync(cancellationToken);

            return messages;
        }

        public void RemoveMessage(Message message)
        {
            _context.Remove(message);
        }
    }
}
