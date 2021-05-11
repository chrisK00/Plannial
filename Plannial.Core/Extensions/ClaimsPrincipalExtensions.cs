using System;
using System.Security.Claims;

namespace Plannial.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

    }
}
