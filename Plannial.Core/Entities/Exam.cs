using System;

namespace Plannial.Core.Entities
{
    public class Exam 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
