using System;

namespace Plannial.Data.Models.Responses
{
    public class HomeworkDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}