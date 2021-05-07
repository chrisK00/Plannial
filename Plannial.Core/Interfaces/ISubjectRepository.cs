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
        Task AddSubjectAsync(Subject subject);
    }
}
