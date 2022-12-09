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
    public class SettingsModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public SettingsModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "Settings";
        [BindProperty] public HrCandidatePref BaseEntry { get; set; }


        public void OnGet()
        {
        }
    }
}
