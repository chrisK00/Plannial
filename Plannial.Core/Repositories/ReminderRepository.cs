using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class ReminderRepository : IReminderRepository
    {
        private readonly DataContext _context;

        public ReminderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken)
        {
            await _context.Reminders.AddAsync(reminder, cancellationToken);
        }

        public async Task<Reminder> GetReminderAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            return await _context.Reminders.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }

        public async Task<IEnumerable<ReminderResponse>> GetReminderResponsesAsync(string userId, ReminderParams reminderParams,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Reminders.Where(x => x.UserId == userId).Select(reminder => new ReminderResponse
            {
                Id = reminder.Id,
                Category = reminder.Category.Name,
                Priority = reminder.Priority,
                DeletedDate = reminder.DeletedDate,
                Description = reminder.Description,
                DueDate = reminder.DueDate,
                Name = reminder.Name
            }).AsNoTracking().AsQueryable();

            query = reminderParams.FilterBy switch
            {
                "due" => query.Where(x => x.DueDate <= DateTime.UtcNow),
                "all" => query.IgnoreQueryFilters(),
                _ => query
            };

            query = reminderParams.OrderBy switch
            {
                "category" => query.OrderBy(x => x.Category),
                "due" => query.OrderBy(x => x.DueDate),
                "priority" => query.OrderByDescending(x => x.Priority),
                _ => query.OrderByDescending(x => x.Priority)
            };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Reminder>> GetRemindersAsync(string userId, IEnumerable<int> reminderIds)
        {
            return await _context.Reminders.Where(x => x.UserId == userId)
                .Where(reminder => reminderIds.Contains(reminder.Id)).ToListAsync();
        }

        public void RemoveReminders(IEnumerable<Reminder> reminders)
        {
            _context.RemoveRange(reminders);
        }
    }
}
