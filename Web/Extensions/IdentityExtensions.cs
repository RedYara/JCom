using System.Security.Claims;
using System.Security.Principal;

namespace Web.Extensions;

public static class IdentityExtensions
{
    public static string GetUserTag(this IIdentity identity) => identity?.GetClaimValue(CustomClaimTypes.UserTagIdentifier);
    public static string GetUserName(this IIdentity identity) => identity?.GetClaimValue(CustomClaimTypes.UserNameIdentifier);
    public static string GetUserId(this IIdentity identity) => identity?.GetClaimValue(CustomClaimTypes.UserIdIdentifier);
    public static string GetUserImagePath(this IIdentity identity) => identity?.GetClaimValue(CustomClaimTypes.UserImagePathIdentifier);

    private static string GetClaimValue(this IIdentity identity, string claimType)
    {
        if (identity is ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.TryGetClaimValue(claimType, out var value);
            return value;
        }
        return null;
    }

    private static bool TryGetClaimValue(this ClaimsIdentity identity, string claimType, out string value)
    {
        var claim = identity.FindFirst(claimType);
        value = claim?.Value;
        return claim != null;
    }

}