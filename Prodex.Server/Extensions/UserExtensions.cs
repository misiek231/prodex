using Prodex.Shared.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Prodex.Server.Extensions
{
    public static class UserExtensions
    {
        public static bool HasRole(this ClaimsPrincipal user, UserType userType)
        {
            return user.HasClaim(ClaimTypes.Role, userType.ToString());
        }

        public static long Id(this ClaimsPrincipal principal)
        {
            return long.Parse(principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value);
        }
    }
}
