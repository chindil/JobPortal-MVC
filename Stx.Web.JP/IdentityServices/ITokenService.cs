using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stx.Web.JP.IdentityServices
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
