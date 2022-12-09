using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Constants;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.DTO.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Candidate
{
    public class ResumeStoreModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public ResumeStoreModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }

        public string PageKey { get; set; } = "ResumeStore";
        [BindProperty] public List<HrCandidateMultiData>  BaseEntries { get; set; } = new List<HrCandidateMultiData>();
        [BindProperty] public HrCandidateMultiData BaseEntryNew { get; set; } = new HrCandidateMultiData();
        [BindProperty] public bool IsAddNewMode { get; set; }
        [BindProperty] public IFormFile ResumeFile1 { get; set; }  
        [BindProperty] public IFormFile ResumeFile2 { get; set; }

        public async Task OnGetAsync()
        {
            StatusMessage.Reset();
            IsAddNewMode = false;
            await IntializePage(null);
        }

        public async Task OnGetAddNewResume()
        {
            IsAddNewMode = true;
            try
            {
                StatusMessage.Reset();
                var baseEntries = await _candidateDataService.GetCandidateMultiData(CandidateID, HrCandidateMultiDataTypes.CandidateResumeFile);
                if (BaseEntries.Count >= 2)
                {
                    StatusMessage.SetMessage("You may only allowed to upload 2 resumes. Please remove an existing resume before add a new resume.", Stx.Shared.Common.MsgType.Warning);
                    await IntializePage(baseEntries);
                    return;
                }

                BaseEntryNew = new HrCandidateMultiData()
                {
                    RecordType = HrCandidateMultiDataTypes.CandidateResumeFile,
                    EntityDesc = ""
                };
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task IntializePage(List<HrCandidateMultiData> baseEntries)
        {
            try
            {
                if (baseEntries == null)
                {
                    BaseEntries = await _candidateDataService.GetCandidateMultiData(CandidateID, HrCandidateMultiDataTypes.CandidateResumeFile);
                }

                //if (BaseEntries.Count == 1)
                //{
                //    BaseEntries.Add(new HrCandidateMultiData() { RecordType = HrCandidateMultiDataTypes.CandidateResumeFile, EntityDesc = "[No resume added 2]" });
                //}
                //if (BaseEntries.Count <= 0)
                //{
                //    BaseEntries.Add(new HrCandidateMultiData() { RecordType = HrCandidateMultiDataTypes.CandidateResumeFile, EntityDesc = "[No resume added 1]" });
                //    BaseEntries.Add(new HrCandidateMultiData() { RecordType = HrCandidateMultiDataTypes.CandidateResumeFile, EntityDesc = "[No resume added 2]" });
                //}
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAddNewResume()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    if (ResumeFile1?.Length > 0)
                    {
                        BaseEntryNew.RecordType = HrCandidateMultiDataTypes.CandidateResumeFile;
                        BaseEntryNew.CandidateID = CandidateID;
                        BaseEntryNew.EntityValue = ResumeFile1.FileName;
                        var result = await _candidateDataService.UploadCandidateResumes(ResumeFile1, BaseEntryNew);
                        StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                        return LocalRedirect("/Candidate/ResumeStore");
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                    await IntializePage(null);
                    IsAddNewMode = true;

                    //return LocalRedirect("");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteResume(string fileName)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    var result = await _candidateDataService.DeleteCandidateFile(CandidateID, fileName);
                    await IntializePage(null);
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                    await IntializePage(null);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUploadResume()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    if(ResumeFile1?.Length > 0)
                    {
                        BaseEntries[0].CandidateID = CandidateID;
                        BaseEntries[0].EntityValue = ResumeFile1.FileName;
                    }
                    if (ResumeFile2?.Length > 0)
                    {
                        BaseEntries[1].CandidateID = CandidateID;
                        BaseEntries[1].EntityValue = ResumeFile2.FileName;
                    }
                    var result = await _candidateDataService.UploadCandidateResumes(ResumeFile1, ResumeFile2, BaseEntries);
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
