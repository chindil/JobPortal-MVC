using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Candidate
{
    public class DashboardModel : BasePageModel
    {
        private readonly ICandidateDataService PageDataService;

        public DashboardModel(ICandidateDataService pageDataService)
        {
            PageDataService = pageDataService;
        }

        [BindProperty]
        public HrCandidate BaseEntity { get; set; } = new HrCandidate();

        public async Task OnGetAsync()
        {
            try
            {
                BaseEntity = await PageDataService.GetRecordByCD(User.Identity.Name ?? "USER@EXAMPLE.COM"); //example
            }
            catch (Exception ex)
            {
                SetPageStatus(ex);

            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //if (ok)
                //{
                //    BaseEntity = await PageDataService.UpdateRecord(User.Identity.Name ?? "USER@EXAMPLE.COM");
                //}
            }
            catch (Exception ex)
            {
            }

            return Page();
        }

    }
}
