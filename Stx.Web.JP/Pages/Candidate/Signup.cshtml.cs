using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Candidate
{
    public class SignupModel : BasePageModel
    {
        [BindProperty]
        public Stx.Shared.Models.HRM.HrCandidate BaseEntity { get; set; } = new Stx.Shared.Models.HRM.HrCandidate();

        public void OnGet()
        {
            InitializePage();
        }

        private async void InitializePage()
        {
            //for local test only
            //BaseEntity.Educations.Add(new HrCandidateEducation());
            //BaseEntity.Educations.Add(new HrCandidateEducation());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
            }
            catch (Exception ex)
            {
            }

            return Page(); 
        }

    }
}
