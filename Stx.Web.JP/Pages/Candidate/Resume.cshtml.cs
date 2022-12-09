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
    public class ResumeModel : BasePageModel
    {
        private readonly ICandidateDataService _dataService;

        public ResumeModel(ICandidateDataService dataService)
        {
            _dataService = dataService;
        }

        public string PageKey { get; set; } = "Resume";
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public HrCandidate BaseEntry { get; set; } = new HrCandidate();


        public async Task OnGetAsync()
        {
            try
            {
                base.InitializePage();
                IsLoading = true;
                BaseEntry = await _dataService.GetRecordByID(CandidateID); 
                IsLoading = false;
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
                    var result = await _dataService.UpdateRecord(BaseEntry, Stx.Shared.Status.EntryState.Update, "");
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

        public async Task<IActionResult> OnPostAddEditEntity(string category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    return LocalRedirect($"~/Candidate/{category}");
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return Page();
        }
    }
}