using Plannial.Core.Models.Entities;

namespace Plannial.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
