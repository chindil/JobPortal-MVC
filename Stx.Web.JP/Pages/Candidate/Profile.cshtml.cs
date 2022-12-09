using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;
using Stx.Web.JP.Extensions;

namespace Stx.Web.JP.Pages.Candidate
{
    public class ProfileModel : BasePageModel
    {
        private readonly ICandidateProfileDataService _dataService;

        public ProfileModel(ICandidateProfileDataService dataService)
        {
            _dataService = dataService;
        }
        public string PageKey { get; set; } = "Profile";

        [BindProperty] public HrCandidateProfile BaseEntry { get; set; } = new HrCandidateProfile();
        [BindProperty] public IFormFile FormFile1 { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                base.InitializePage();
                BaseEntry = await _dataService.GetRecordByID(CandidateID);
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
        public async Task<IActionResult> OnPostUploadResume()
        {
            try
            {
                StatusMessage.Reset();
                var result = await _dataService.UpdateProfileImage(FormFile1, CandidateID);

                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                return Page();
            }
            return Page();
        }
    }
}
