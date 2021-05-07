using System;
using Plannial.Core.Helpers;

namespace Plannial.Core.Requests
{
    public record AddExamRequest(string Name, string Description, DateTime DueDate);
    public record AddHomeworkRequest(string Name, string Description, DateTime DueDate);
    public record AddReminderRequest(string Name, string Description, DateTime? DueDate, Priority Priority);
    public record AddSubjectRequest(string Name, string Description);

}
