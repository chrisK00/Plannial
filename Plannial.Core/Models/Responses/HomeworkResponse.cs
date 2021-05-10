using System;

namespace Plannial.Core.Models.Responses
{
    public class HomeworkResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}