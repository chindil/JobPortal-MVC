using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Stx.Web.JP.Extensions
{
    public static class IdentityExtension
    {
        public static int GetId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            var id = claimsIdentity.FindFirst("usrid")?.Value;
            return Convert.ToInt32(id);
        }
    }
}
