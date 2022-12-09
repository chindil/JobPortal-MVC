using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Shared;
using Stx.Web.JP.Core;
using System.Text.Json;
using Stx.Shared.Models.DTO.HRM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Stx.Web.JP.Pages.Jobs
{
    public class DetailsModel : BasePageModel
    {
        private readonly IJobOrderPreviewDataService _jobOrderPreviewDataService;
        private readonly IJobSendoutDataService _jobSendoutDataService;

        public DetailsModel(IJobOrderPreviewDataService jobOrderPreviewDataService, IJobSendoutDataService jobSendoutDataService)
        {
            _jobOrderPreviewDataService = jobOrderPreviewDataService;
            _jobSendoutDataService = jobSendoutDataService;
            base.InitializePage();
        }

        [BindProperty] public HrJobOrderPreviewDTO InputModel { get; set; }

        public async Task OnGetAsync(int JobOrderID, int CandidateId)
        {
           InputModel = await _jobOrderPreviewDataService.GetRecordByID(JobOrderID, CandidateId);
        } 

        public async Task<IActionResult> OnPostAsync(HrJobSendout model)
        {
            TempData["joborderid"] = model.JobOrderID;
            try
            {
                StatusMessage.Reset();
                await _jobSendoutDataService.Submit(model);
                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                return LocalRedirect("/jobs/search");
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                return LocalRedirect("/jobs/search");
            }
        }
        
        public async Task<IActionResult> OnPostSubmitApplication(HrJobSendout model)
        {
            TempData["joborderid"] = model.JobOrderID;
            try
            {
                StatusMessage.Reset();
                await _jobSendoutDataService.Submit(model);
                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                return LocalRedirect("/jobs/search");
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                return LocalRedirect("/jobs/search");
            }
        }

        #region Partial Views

        public async Task<IActionResult> OnGetJobDetailsPartial(int Id)
        {
            try
            {
                StatusMessage.Reset();
                InputModel = await _jobOrderPreviewDataService.GetRecordByID(Id, CandidateID);
                if (InputModel != null)
                {
                    return new PartialViewResult
                    {
                        ViewName = "_JobDetails",
                        ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<HrJobOrderPreviewDTO>(ViewData, InputModel)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<IActionResult> OnGetJobSubmitDataPartial(int Id, int CandidateId) //101=test customer
        //{
        //    try
        //    {
        //        StatusMessage.Reset();
        //        JobSubmitDto = await _jobOrderPreviewDataService.GetRecordByID(Id, CandidateId);
        //        if (JobSubmitDto != null)
        //        {
        //            return new PartialViewResult
        //            {
        //                ViewName = "_JobSubmit",
        //                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<HrJobOrderPreviewDTO>(ViewData, JobSubmitDto)
        //            };
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        #endregion

        public async Task<IActionResult> OnGetJobAction(string actionName,string actionValue, int jobOrderId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new { data = "notlogged" });
                    //return LocalRedirect("/Account/Action?act=signin");
                    //await HttpContext.ChallengeAsync();
                    //Challenge(new AuthenticationProperties
                    //{
                    //    RedirectUri = JPPageSetting.HomePage
                    //}, "oidc");
                }

                var actionData = new CandidateJobOrderActionDto { ActionName = actionName, ActionValue = actionValue, JobOrderID = jobOrderId, CandidateID = CandidateID };
                var rslt = await _jobOrderPreviewDataService.Action(actionData);
                return new JsonResult(new { status = rslt });
            }
            catch { }
            return null;
        }

        public async Task<IActionResult> OnPostJobAction(string actionName, string actionValue, int jobOrderId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new JsonResult(new { data = "notlogged" });
                    //return LocalRedirect("/Account/Action?act=signin");
                    //await HttpContext.ChallengeAsync();
                    //Challenge(new AuthenticationProperties
                    //{
                    //    RedirectUri = JPPageSetting.HomePage
                    //}, "oidc");
                }

                var actionData = new CandidateJobOrderActionDto { ActionName = actionName, ActionValue = actionValue, JobOrderID = jobOrderId, CandidateID = CandidateID };
                var rslt = await _jobOrderPreviewDataService.Action(actionData);
                return new JsonResult(new { status = rslt });
            }
            catch { }
            return null;
        }
    }
}
