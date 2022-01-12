using Plannial.Data.Models.Responses;

namespace Plannial.Data.Models.Responses
{
    public class SubjectDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GradeDetailResponse Grade { get; set; }
    }
}
