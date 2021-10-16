using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MKTFY.API.Helpers
{
    public static class UserHelpers
    {
       //get user information from http header, Auth0 stores userId in "sub" property
        public static string GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? principal.FindFirst(c => c.Type == "sub");
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                return userIdClaim.Value;

            return null;
        }
    }
}
