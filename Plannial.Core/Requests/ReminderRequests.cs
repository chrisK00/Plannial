using System;
using Plannial.Core.Helpers;

namespace Plannial.Core.Requests
{
    public record AddReminderRequest(string Name, string Description, DateTime? DueDate, Priority Priority);
}
