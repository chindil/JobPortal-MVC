using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Candidate
{
    public class MyVisibilityModel : BasePageModel
    {
        private readonly ICandidateProfileDataService _dataService;

        public MyVisibilityModel (ICandidateProfileDataService dataService)
        {
            _dataService = dataService;
        }
        public string PageKey { get; set; } = "Visibility";

        [BindProperty]
        public HrCandidatePref BaseEntry { get; set; }

        public async Task OnGetAsync(int Id )
        {

        }
    }
}
