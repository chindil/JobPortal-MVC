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
    public class CertificateModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public CertificateModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "Certificate";

        [BindProperty] public HrCandidateCertificate Input { get; set; }
        [BindProperty] public List<HrCandidateCertificate> BaseEntryList { get; set; } = new List<HrCandidateCertificate>();

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
            BaseEntryList = await _candidateDataService.GetCertificates(CandidateID);
        }

        public async Task<IActionResult> OnPostSubmitEntry()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    var result = await _candidateDataService.UpdateCertificates(Input);
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
                        Input = new HrCandidateCertificate() { CandidateID = CandidateID, ID = -1 };
                    else
                        Input = BaseEntryList.Where(x => x.ID == id).FirstOrDefault();
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
                    var result = await _candidateDataService.DeleteResumeComponent("CERT", CandidateID, id);
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
