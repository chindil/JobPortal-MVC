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
    public class ExperienceModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public ExperienceModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "Experience";
        [BindProperty] public HrCandidateExperience Input { get; set; }
        [BindProperty] public List<HrCandidateExperience> BaseEntryList { get; set; } = new List<HrCandidateExperience>();

        public async Task OnGetAsync()
        {
            try
            {
                base.InitializePage();
                Input = null;
                await GetBaseEntryList();
            }
            catch (Exception ex)
            {
                SetPageStatus(ex);
                StatusMessage.SetError(ex.Message);
            }
        }

        private async Task GetBaseEntryList()
        {
            BaseEntryList = await _candidateDataService.GetExperiences(CandidateID);
        }

        public async Task<IActionResult> OnPostSubmitEntry()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    var result = await _candidateDataService.UpdateExperiences(Input);
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                    await GetBaseEntryList();
                    Input = null;
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                    return Page();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddEditEntity(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    await GetBaseEntryList();

                    if (id <= 0)
                        Input = new HrCandidateExperience() { CandidateID = CandidateID, ID = -1};
                    else 
                        Input = BaseEntryList.Where(x=> x.ID == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddEditEntityCancel(int id)
        {
            Input = null;
            await GetBaseEntryList();
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
                    var result = await _candidateDataService.DeleteResumeComponent("EXPE", CandidateID, id);
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
