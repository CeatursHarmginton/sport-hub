using System.Security.Claims;

namespace SportHub.API.Controllers;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetUserId(this ClaimsPrincipal principal, out long userId)
    {
        var claimValue = principal.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? principal.FindFirstValue("userId");
        return long.TryParse(claimValue, out userId);
    }

    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        return principal.IsInRole("ADMIN");
    }
}
