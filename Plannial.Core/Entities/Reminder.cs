using System;
using Plannial.Core.Helpers;

namespace Plannial.Core.Entities
{
    public class Reminder : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Category Category { get; set; }
    }
}
