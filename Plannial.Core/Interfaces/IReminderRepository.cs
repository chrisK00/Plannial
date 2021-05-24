using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder> GetReminderAsync(string userId, int reminderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ReminderResponse>> GetReminderResponsesAsync(string userId, ReminderParams reminderParams, CancellationToken cancellationToken = default);
        Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Reminder>> GetRemindersAsync(string userId, IEnumerable<int> reminderIds, CancellationToken cancellationToken = default);
        void RemoveReminders(IEnumerable<Reminder> reminders);
    }
}
