using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Extensions.Common;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ICandidateSignupDataService _corporatePublicDataService;
        public RegisterModel(ICandidateSignupDataService candidateSignupDataService )
        {
            _corporatePublicDataService = candidateSignupDataService;
        }
    
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                return LocalRedirect(JPPageSetting.HomePage);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!Input.Password.Compare(Input.ConfirmPassword))
                        return Page();

                    var newCand = new Stx.Shared.Models.DTO.HRM.UserSignupDTO()
                    {
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Email = Input.Email,
                        Password = Input.Password
                    };
                    var ret = await _corporatePublicDataService.Signup(newCand);
                    if (ret)
                    {
                        await HttpContext.ChallengeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
            return Page();
        }
    }
}