using System;
using Plannial.Data.Helpers;

namespace Plannial.Data.Models.Entities
{
    public class Reminder : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
