using System.Collections.Generic;

namespace Plannial.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
