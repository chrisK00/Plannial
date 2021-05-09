using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Interfaces
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Get subjects including their related entities as response models without tracking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Subjects as response models including related entities without tracking</returns>
        Task<IEnumerable<SubjectResponse>> GetSubjectResponsesAsync(string userId, CancellationToken cancellationToken);
        Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a subject by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>A subject including all related entities</returns>
        Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken);
    }
}
