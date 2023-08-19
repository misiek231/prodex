using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Prodex.Server.Extensions
{
    public static class UserExtensions
    {
        public static long Id(this IPrincipal principal)
        {
            return long.Parse((principal as ClaimsPrincipal).FindFirst(JwtRegisteredClaimNames.Jti)?.Value);
        }
    }
}
