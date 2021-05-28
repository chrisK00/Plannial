using System;

namespace Plannial.Core.Models.Entities
{
    public class Homework : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }                                                                                
        public DateTime DueDate { get; set; }
        public int SubjectId { get; set; }
    }
}
