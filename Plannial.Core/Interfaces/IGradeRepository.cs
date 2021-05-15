using System.Threading.Tasks;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface IGradeRepository
    {
        public Task<Grade> GetGradeAsync(string grade);
    }
}
