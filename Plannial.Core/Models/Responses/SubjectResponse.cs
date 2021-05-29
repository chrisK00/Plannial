using Plannial.Core.Models.Entities;

namespace Plannial.Core.Models.Responses
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Grade Grade { get; set; }
    }
}
