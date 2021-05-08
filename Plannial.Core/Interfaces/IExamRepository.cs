using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IExamRepository
    {
        public void RemoveExam(Exam exam);
        public Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken = default);
    }
}
