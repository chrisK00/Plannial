using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

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
            return await _context.Messages.FirstOrDefaultAsync(x => x.SenderId == userId || x.RecipientId == userId
            && x.Id == messageId);
        }

        public async Task<IEnumerable<MessageResponse>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default)
        {
            var query = _context.Messages.OrderByDescending(m => m.DateSent).AsNoTracking().AsQueryable();

            query = messageParams.FilterBy switch
            {
                "Inbox" => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted),
                "Outbox" => query.Where(x => x.SenderId == userId && !x.SenderDeleted),
                _ => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted && x.DateRead == null)
            };

            return await query.Select(msg => new MessageResponse
            {
                SenderId = msg.SenderId,
                RecipientId = msg.RecipientId,
                Content = msg.Content,
                DateSent = msg.DateSent,
                DateRead = msg.DateRead,
                Id = msg.Id
            }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<MessageResponse>> GetMessageThreadAsync(string userId, string otherUserId, CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages
                .Where(x => x.RecipientId == userId && !x.RecipientDeleted && x.SenderId == otherUserId
                || x.SenderId == userId && !x.SenderDeleted && x.RecipientId == otherUserId).ToListAsync(cancellationToken: cancellationToken);

            var unreadMessages = messages.Where(x => x.DateRead == null && x.RecipientId == userId);

            foreach (var message in unreadMessages)
            {
                message.DateRead = DateTime.UtcNow;
            }

            return messages.Select(msg => new MessageResponse
            {
                SenderId = msg.SenderId,
                RecipientId = msg.RecipientId,
                Content = msg.Content,
                DateSent = msg.DateSent,
                DateRead = msg.DateRead,
                Id = msg.Id
            });
        }

        public void RemoveMessage(Message message)
        {
            _context.Remove(message);
        }
    }
}
