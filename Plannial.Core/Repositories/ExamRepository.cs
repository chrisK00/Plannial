using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly DataContext _context;

        public ExamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken)
        {
            return await _context.Exams.FirstOrDefaultAsync(x => x.Id == examId && x.UserId == userId, cancellationToken);
        }

        public void RemoveExam(Exam exam)
        {
            _context.Exams.Remove(exam);
        }
    }
}
