using System;
using Plannial.Core.Helpers;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Models.Requests
{
    public record AddExamRequest(string Name, string Description, DateTime DueDate);
    public record AddHomeworkRequest(string Name, string Description, DateTime DueDate, int SubjectId);
    public record AddReminderRequest(string Name, string Description, DateTime? DueDate, Priority Priority);
    public record AddSubjectRequest(string Name, string Description);
    public record AddMessageRequest(string RecipientEmail, string Content);
    public record AddSubjectGradeRequest(string Grade);
}
