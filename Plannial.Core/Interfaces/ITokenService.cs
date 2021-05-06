using System.Threading.Tasks;
using Plannial.Core.Entities;

namespace Plannial.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
