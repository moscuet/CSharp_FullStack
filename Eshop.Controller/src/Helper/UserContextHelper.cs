using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public static class UserContextHelper
{
    public static (Guid? userId, string? userRole) GetUserClaims(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        var userRoleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
        
        Guid? currentUserId = userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
        string? currentUserRole = userRoleClaim?.Value;

        return (currentUserId, currentUserRole);
    }
}