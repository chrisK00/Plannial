using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly DataContext _context;

        public HomeworkRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddHomeworkAsync(Homework homework, CancellationToken cancellationToken)
        {
            await _context.Homeworks.AddAsync(homework, cancellationToken);
        }

        public async Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Homeworks.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == homeworkId, cancellationToken);
        }

        public async Task<IEnumerable<Homework>> GetHomeworksAsync(string userId, int? subjectId, CancellationToken cancellationToken = default)
        {
            var query = _context.Homeworks.Where(x => x.UserId == userId).AsNoTracking().AsQueryable();
            if (subjectId.HasValue)
            {
               query = query.Where(x => x.SubjectId == subjectId);
            }

            return await query.OrderByDescending(x => x.DueDate).ToListAsync(cancellationToken);
        }

        public void RemoveHomework(Homework homework)
        {
            _context.Homeworks.Remove(homework);
        }
    }
}
