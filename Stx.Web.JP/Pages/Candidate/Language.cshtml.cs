using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;

namespace Stx.Web.JP.Pages.Candidate
{
    public class LanguageModel : PageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public LanguageModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "Language";
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public HrCandidateLanguage Input { get; set; }
        [BindProperty] public List<HrCandidateLanguage> BaseEntryList { get; set; } = new List<HrCandidateLanguage>();
        private int CandidiateID = 100;

        public async Task OnGetAsync()
        {
            try
            {
                StatusMessage.Reset();
                await GetBaseEntryList();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }
        private async Task GetBaseEntryList()
        {
            BaseEntryList = await _candidateDataService.GetLanguages(100);
        }

        public async Task<IActionResult> OnPostSubmitEntry()
        {
            try
            {
                StatusMessage.Reset();
                BaseEntryList.ForEach(m => m.CandidateID = 100);
                var result = await _candidateDataService.UpdateLanguages(BaseEntryList);
                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAddEditEntity(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isContinue = true;
                    if (BaseEntryList.Any(x => string.IsNullOrWhiteSpace(x.Language)))
                    {
                        isContinue = false;
                    }
                    StatusMessage.Reset();
                    await GetBaseEntryList();
                    if (!isContinue) return Page();

                    BaseEntryList.Add(new HrCandidateLanguage() { CandidateID = CandidiateID, ID = -1 });
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteEntity(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Input = null;
                    StatusMessage.Reset();
                    var result = await _candidateDataService.DeleteResumeComponent("LANG", CandidiateID, id);
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                    await GetBaseEntryList();
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
