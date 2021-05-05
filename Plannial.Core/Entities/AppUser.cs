using System.Collections.Generic;

namespace Plannial.Core.Entities
{
    public class AppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
