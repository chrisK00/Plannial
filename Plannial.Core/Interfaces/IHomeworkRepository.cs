using System.Threading;
using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IHomeworkRepository
    {
        public void RemoveHomework(Homework homework);
        public Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default);
    }
}
