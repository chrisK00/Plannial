using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

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

        public async Task<IEnumerable<Subject>> GetSubjectsAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Homeworks).Include(x => x.Exams).Include(x => x.Grade)
                .Where(x => x.UserId == userId).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Grade)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }
    }
}
