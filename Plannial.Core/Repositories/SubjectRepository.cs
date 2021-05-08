using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Entities;
using Plannial.Core.Interfaces;
using Plannial.Core.Responses;

namespace Plannial.Core.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken)
        {
            await _context.AddAsync(subject, cancellationToken);
        }

        public async Task<IEnumerable<SubjectResponse>> GetSubjectResponsesAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Homeworks).Include(x => x.Exams)
                .Where(x => x.UserId == userId)
                .Select(s => new SubjectResponse
                {
                    Id = s.Id,
                    Description = s.Description,
                    Name = s.Name,
                    Exams = s.Exams.Select(e => new ExamResponse
                    {
                        Id = e.Id
                    }).ToList(),
                    Homeworks = s.Homeworks.Select(h => new HomeworkResponse
                    {
                        Id = h.Id
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Homeworks).Include(x => x.Exams)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }
    }
}
