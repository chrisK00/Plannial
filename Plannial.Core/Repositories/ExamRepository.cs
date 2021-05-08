using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Entities;
using Plannial.Core.Interfaces;

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
            var temp = cancellationToken.IsCancellationRequested;
            return await _context.Exams.FirstOrDefaultAsync(x => x.Id == examId && x.UserId == userId);
        }

        public void RemoveExam(Exam exam)
        {
            _context.Exams.Remove(exam);
        }
    }
}
