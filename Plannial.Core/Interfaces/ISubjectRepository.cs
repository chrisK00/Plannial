using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Get subjects including their related entities without tracking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Subjects including related entities without tracking</returns>
        Task<IEnumerable<Subject>> GetSubjectsAsync(string userId, CancellationToken cancellationToken);
        Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a subject by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>A subject including all related entities</returns>
        Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken);
        void RemoveExam(Exam exam);
        Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken = default);
        void RemoveHomework(Homework homework);
        Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default);
        Task<Grade> GetGradeAsync(string grade, CancellationToken cancellationToken = default);
    }
}
