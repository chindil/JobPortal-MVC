using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Web.JP.Core;
using Stx.Shared;
using Microsoft.AspNetCore.Identity;
using Stx.Shared.Extensions.Common;

namespace Stx.Web.JP.Pages.Account
{
    public class ActionModel : PageModel
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;

        //public ActionModel(SignInManager<ApplicationUser> signInManager)
        //{
        //    _signInManager = signInManager;
        //}

        public async Task<IActionResult> OnGet(string act)
        {
            if (act.Compare("signin"))
            {
                //await HttpContext.AuthenticateAsync();
                return Challenge(new AuthenticationProperties { 
                    RedirectUri = JPPageSetting.HomePage
                }, "oidc");
            }
            else if (act.Compare("signout"))
            {
                return SignOut(new AuthenticationProperties
                {
                    RedirectUri = JPPageSetting.HomePage
                }, "Cookies", "oidc");

                //await HttpContext.SignOutAsync("Cookies");
                //await HttpContext.SignOutAsync("oidc");
                //await _signInManager.SignOutAsync();
                // delete local authentication cookie
                //await HttpContext.SignOutAsync();
                // Clear the existing external cookie to ensure a clean login process
                //await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                // Clear the existing external cookie to ensure a clean login process
                //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                return SignOut("Cookies", "oidc");
            }
            else if (act.Compare("signup"))
            {
                return LocalRedirect("/Account/Register");
            }

            return LocalRedirect(JPPageSetting.HomePage);

        }
    }
}
