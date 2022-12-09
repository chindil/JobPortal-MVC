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
    public class AdditionalInfoModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public AdditionalInfoModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "AdditnlInfo";
        [BindProperty] public HrCandidate BaseEntry { get; set; }
                
        public async Task OnGet()
        {
            try
            {
                base.InitializePage();
                BaseEntry = await _candidateDataService.GetRecordByID(CandidateID); //example
            }
            catch (Exception ex)
            {
                SetPageStatus(ex);
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    BaseEntry = await _candidateDataService.PartialUpdate(BaseEntry, "Resume_Additional_Info");
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                    return Page();
                }
            }
            return Page();
        }

    }
}
