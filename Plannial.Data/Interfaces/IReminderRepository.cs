using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Data.Models.Entities;
using Plannial.Data.Models.Params;

namespace Plannial.Data.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder> GetReminderAsync(string userId, int reminderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Reminder>> GetRemindersAsync(string userId, ReminderParams reminderParams, CancellationToken cancellationToken = default);
        Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);

        /// <summary>
        /// Gets reminders that matches the passed in list of reminder ids while ignoring query filters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reminderIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Reminders with the specified ids if found</returns>
        Task<IReadOnlyCollection<Reminder>> GetRemindersAsync(string userId, IEnumerable<int> reminderIds, CancellationToken cancellationToken = default);
        void RemoveReminders(IEnumerable<Reminder> reminders);
    }
}
