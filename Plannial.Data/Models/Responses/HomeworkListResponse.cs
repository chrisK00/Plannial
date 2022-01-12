using System;

namespace Plannial.Data.Models.Responses
{
    public class HomeworkListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }
}