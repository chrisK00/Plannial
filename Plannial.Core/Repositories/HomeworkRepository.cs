using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly DataContext _context;

        public HomeworkRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Homeworks.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == homeworkId, cancellationToken);
        }

        public void RemoveHomework(Homework homework)
        {
            _context.Homeworks.Remove(homework);
        }
    }
}
