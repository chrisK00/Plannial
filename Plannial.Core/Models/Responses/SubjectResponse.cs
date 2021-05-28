using System.Collections.Generic;

namespace Plannial.Core.Models.Responses
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExamDetailResponse> Exams { get; set; } = new List<ExamDetailResponse>();
        public ICollection<HomeworkListResponse> Homeworks { get; set; } = new List<HomeworkListResponse>();
        public string Grade { get; set; }
    }
}
