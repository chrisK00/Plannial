using System;

namespace Plannial.Core.Entities
{
    public class Homework : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
