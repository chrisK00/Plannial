using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

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

        public async Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Exams.FirstOrDefaultAsync(x => x.Id == examId && x.UserId == userId, cancellationToken);
        }

        public void RemoveExam(Exam exam)
        {
            _context.Exams.Remove(exam);
        }

        public async Task<Homework> GetHomeworkAsync(int homeworkId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Homeworks.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == homeworkId, cancellationToken);
        }

        public void RemoveHomework(Homework homework)
        {
            _context.Homeworks.Remove(homework);
        }

        public async Task<Grade> GetGradeAsync(string grade, CancellationToken cancellationToken = default)
        {
            return await _context.Grades.FirstOrDefaultAsync(x => x.Value == grade.ToUpper(), cancellationToken);
        }
    }
}
