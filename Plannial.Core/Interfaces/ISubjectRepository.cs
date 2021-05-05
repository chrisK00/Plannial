using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plannial.Core.Responses;

namespace Plannial.Core.Interfaces
{
    public interface ISubjectRepository
    {
        Task<SubjectResponse> GetSubjectResponseById(int id);
    }
}
