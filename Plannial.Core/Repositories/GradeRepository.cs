using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Grade> GetGradeAsync(string grade)
        {
            return await _context.Grades.FirstOrDefaultAsync(x => x.NormalizedValue == grade.ToUpper());
        }
    }
}
