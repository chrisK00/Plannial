using System.Collections.Generic;

namespace Plannial.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
