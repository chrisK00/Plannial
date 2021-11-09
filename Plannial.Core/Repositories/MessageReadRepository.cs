using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Repositories
{
    public class MessageReadRepository : IMessageReadRepository
    {
        private readonly DataContext _context;

        public MessageReadRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageResponse>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default)
        {
            var query = _context.Messages.OrderByDescending(m => m.DateSent).AsNoTracking();

            query = messageParams.FilterBy switch
            {
                "Inbox" => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted),
                "Outbox" => query.Where(x => x.SenderId == userId && !x.SenderDeleted),
                _ => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted && x.DateRead == null)
            };

            var relatedSenders = await _context.Users
                .Where(u => query.Select(m => m.SenderId).Contains(u.Id))
                .Select(x => new { x.UserName, x.Id })
                .ToListAsync(cancellationToken);

            var relatedRecipients = await _context.Users
                .Where(u => query.Select(m => m.RecipientId).Contains(u.Id))
                .Select(x => new { x.UserName, x.Id })
                .ToListAsync(cancellationToken);

            var messages = await query.ToListAsync(cancellationToken);

            return messages.Select(m => new MessageResponse
            {
                Id = m.Id,
                SenderId = m.SenderId,
                RecipientId = m.RecipientId,
                Content = m.Content,
                DateRead = m.DateRead,
                DateSent = m.DateSent,
                RecipientUsername = relatedRecipients.FirstOrDefault(x => x.Id == m.RecipientId)?.UserName,
                SenderUsername = relatedRecipients.FirstOrDefault(x => x.Id == m.SenderId)?.UserName
            }).ToList();
        }
    }
}
