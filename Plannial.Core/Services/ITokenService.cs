using Plannial.Data.Models.Entities;

namespace Plannial.Core.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
