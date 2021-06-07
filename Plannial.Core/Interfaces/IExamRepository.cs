using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IExamRepository
    {
        public void RemoveExam(Exam exam);
        public Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Exam>> GetExamsAsync(string userId, int? subjectId, CancellationToken cancellationToken = default);
        Task AddExamAsync(Exam exam, CancellationToken cancellationToken);
    }
}
