using System.Security.Claims;

namespace TaskManagementAPI.Helper
{
    public static class ClaimExtensions
    {
        public static Guid? GetOwnerIdClaim(this ClaimsPrincipal user)
        {
            var ownerIdClaim = user?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (Guid.TryParse(ownerIdClaim, out var ownerId))
            {
                return ownerId;
            }

            return null;
        }
    }
}
