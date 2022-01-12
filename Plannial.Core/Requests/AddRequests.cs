using Plannial.Data.Helpers;
using System;

namespace Plannial.Core.Requests
{
    public record AddExamRequest(string Name, string Description, DateTime DueDate, int SubjectId);
    public record AddHomeworkRequest(string Name, string Description, DateTime DueDate, int SubjectId);
    public record AddReminderRequest(string Name, string Description, DateTime? DueDate, Priority Priority);
    public record AddSubjectRequest(string Name, string Description);
    public record AddMessageRequest(string RecipientEmail, string Content);
    public record AddGradeRequest(string Grade, DateTime DateSet, string Note);
}
