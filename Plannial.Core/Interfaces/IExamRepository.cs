using System;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IExamRepository
    {
        public void RemoveExam(Exam exam);
        public Task<Exam> GetExamAsync(int examId, Guid userId, CancellationToken cancellationToken = default);
    }
}
