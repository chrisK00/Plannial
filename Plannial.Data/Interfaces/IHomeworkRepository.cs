using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Interfaces
{
    public interface IHomeworkRepository
    {
        public void RemoveHomework(Homework homework);
        public Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Homework>> GetHomeworksAsync(string userId, int? subjectId, CancellationToken cancellationToken = default);
        Task AddHomeworkAsync(Homework homework, CancellationToken cancellationToken);
    }
}
