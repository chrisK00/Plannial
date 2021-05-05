using System.Collections.Generic;

namespace Plannial.Core.Entities
{
    public class AppUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
        public ICollection<Message> MessagesRecieved { get; set; } = new List<Message>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    }
}
