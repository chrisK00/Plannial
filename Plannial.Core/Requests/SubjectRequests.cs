using System.Collections.Generic;
using Plannial.Core.Entities;

namespace Plannial.Core.Requests
{
    public record AddSubjectRequest(string Name, string Description, ICollection<Exam> Exams, ICollection<Homework> Homeworks);
}
