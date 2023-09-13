using System.Security.Claims;

namespace API.Infrastructure
{
    public static class UserExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.Claims.First(e => e.Type.EndsWith("nameidentifier"))?.Value);
        }
    }
}
