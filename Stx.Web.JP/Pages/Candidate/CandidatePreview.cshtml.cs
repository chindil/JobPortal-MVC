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
    public class CandidatePreviewModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public CandidatePreviewModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        [BindProperty]
        public HrCandidate InputModel { get; set; } = new HrCandidate();

        public async Task OnGet()
        {
            try
            {
                base.InitializePage();
                InputModel = await _candidateDataService.GetRecordPublicByID(CandidateID);
            }
            catch (Exception ex)
            {
                SetPageStatus(ex);
                StatusMessage.SetError(ex.Message);
            }
        }
    }
}