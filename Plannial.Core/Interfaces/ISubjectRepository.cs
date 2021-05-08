using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Entities;
using Plannial.Core.Responses;

namespace Plannial.Core.Interfaces
{
    public interface ISubjectRepository
    {
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
