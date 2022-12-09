using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.DTO.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Jobs
{
    public class SubmitModel : BasePageModel
    {
        private readonly IJobOrderPreviewDataService _jobOrderPreviewDataService;
        private readonly IJobSendoutDataService _jobSendoutDataService;

        public SubmitModel(IJobOrderPreviewDataService jobOrderPreviewDataService, IJobSendoutDataService jobSendoutDataService)
        {
            _jobOrderPreviewDataService = jobOrderPreviewDataService;
            _jobSendoutDataService = jobSendoutDataService;
        }
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public HrJobSendoutDTO BaseEntry { get; set; } = new HrJobSendoutDTO();
        //[BindProperty] public List<HrReviewQuestion> ReviewQuestions { get; set; } = new List<HrReviewQuestion>();

        [BindProperty] public bool IsApplicSubmitted { get; set; } = false;


        public async Task OnGetAsync(int jobId)
        {
            try
            {
                StatusMessage.Reset();
                if (jobId <= 0)
                {
                    StatusMessage.SetError("No valid job details found.");
                }
                else
                {
                   

                    BaseEntry = await LoadBaseData(jobId);
                }
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task<HrJobSendoutDTO> LoadBaseData(int jobId)
        {
            return await _jobSendoutDataService.GetData(jobId, CandidateID);
            //ReviewQuestions = BaseEntry.ReviewQuestions;
        }

        /// <summary>
        /// Get similar jobs which the applicant whose applied for this also job applied for.
        /// May show as separate lists:  People-Also-Applied-For & Similar-Jobs
        /// </summary>
        /// <param name="jobId">Job Order Id</param>
        /// <returns></returns>
        public async Task LoadPeopleAlsoAppliedAndSimilarJobs(int jobId)
        {
            //BaseEntry = await _jobSendoutDataService.GetData(jobId, CandidiateID);
        }

        public async Task<IActionResult> OnPostSubmitApplication()
        {
            try
            {
                StatusMessage.Reset();
                HrJobSendout jobSendout = new HrJobSendout();
                jobSendout.ID = 0;
                jobSendout.CandidateID = CandidateID;
                jobSendout.JobOrderID = BaseEntry.JobOrderID;
                jobSendout.Sender = BaseEntry.CoverLetter;
                jobSendout.CorporateEmail = BaseEntry.ResumeName;

                //var dd = model.ReviewQuestions.Count;
                var yyy = BaseEntry.ReviewQuestions.Count;

                try
                {
                    IsApplicSubmitted = await _jobSendoutDataService.Submit(jobSendout);
                    if (IsApplicSubmitted)
                    {
                        StatusMessage.SetSuccess("Your application has been sent.");
                        await LoadPeopleAlsoAppliedAndSimilarJobs(BaseEntry.JobOrderID);
                    }
                    else
                    {
                        var dbdata = await LoadBaseData(BaseEntry.JobOrderID);
                        dbdata.CopyReviewQuestionUserAnswers(BaseEntry.ReviewQuestions);
                        BaseEntry = dbdata;
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                    var dbdata = await LoadBaseData(BaseEntry.JobOrderID);
                    dbdata.CopyReviewQuestionUserAnswers(BaseEntry.ReviewQuestions);
                    BaseEntry = dbdata;
                }
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
            return Page();
        }
    }
}
