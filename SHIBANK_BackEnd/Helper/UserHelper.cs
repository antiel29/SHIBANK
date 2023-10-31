using System.Security.Claims;

namespace SHIBANK.Helper
{
    public static class UserHelper
    {
        public static int GetUserIdFromClaim(ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idClaim, out int id))
            {
                return id;
            }
            else
            {
                throw new InvalidOperationException("Error getting user id");
            }
        }
    }
}