using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannial.Core.Models.Requests
{
    public record UpdateExamRequest(string Name, string Description, DateTime DueDate);
}
