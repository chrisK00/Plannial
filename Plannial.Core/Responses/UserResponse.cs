using System.Collections.Generic;

namespace Plannial.Core.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public ICollection<SubjectResponse> Subjects { get; set; } = new List<SubjectResponse>();
        public ICollection<ReminderResponse> Reminders { get; set; } = new List<ReminderResponse>();
    }
}