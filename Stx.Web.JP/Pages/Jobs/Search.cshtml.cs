using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Common;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Shared.Models.Parm;

namespace Stx.Web.JP.Pages.Jobs
{
    public class SearchModel : PageModel
    {
        private readonly IJobSearchDataService _jobSearchDataService;
        private readonly IHrmGeneralDataService _hrmGeneralDataService;

        public SearchModel(IJobSearchDataService jobSearchDataService, IHrmGeneralDataService hrmGeneralDataService)
        {
            _jobSearchDataService = jobSearchDataService;
            _hrmGeneralDataService = hrmGeneralDataService;
        }   
        [TempData] public int joborderid { get; set; }
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public List<HrJobOrderSearch> InputModel { get; set; }
        [BindProperty] public HrJobSearchParmDTO SearchParms { get; set; } = new HrJobSearchParmDTO();
        [BindProperty] public List<ComboInt> JobIndustryList { get; set; } = new List<ComboInt>();
        [BindProperty] public List<ComboShort> JobFilterCareerLevelList { get; set; } = new List<ComboShort>();
        [BindProperty] public List<ComboShort> JobFilterEmploymentTypeList { get; set; } = new List<ComboShort>();
        public string SearchMode { get; set; } = "";

        public async Task OnGet()
        {
            try
            {
                StatusMessage.Reset();
                joborderid = Convert.ToInt32(TempData["joborderid"]);
                await SearchJobs(SearchParms);
                await LoadInitialData();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task OnGetSearchByCorpID(int corporateId)
        {
            try
            {
                StatusMessage.Reset();
                SearchMode = "CORP";
                SearchParms = new HrJobSearchParmDTO(corporateId);
                await SearchJobs(SearchParms);
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }
        public async Task OnGetSearchIndustry(int id)
        {
            try
            {
                StatusMessage.Reset();
                SearchMode = "INDSTRY";
                SearchParms = new HrJobSearchParmDTO() { JobIndustries = new List<string> { id.ToString() } };
                await SearchJobs(SearchParms);
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }
        
        private async Task LoadInitialData()
        {
            JobIndustryList = await _hrmGeneralDataService.GetJobIndustryList();
            JobFilterCareerLevelList = await _hrmGeneralDataService.GetCareerLevelList();
            JobFilterEmploymentTypeList = await _hrmGeneralDataService.GetEmploymentTypeList();
        }

        private async Task SearchJobs(HrJobSearchParmDTO searchParmDTO) //Stx.Shared.Models.Parm.HrJobSearchParmDTO parm)
        {
            //TempData["joborderid"] = parm.JobOrderID;
            try
            {
                StatusMessage.Reset();                
                InputModel = await _jobSearchDataService.Search(SearchParms);
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            } 
            
            try
            {
                await LoadInitialData();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostSearch()
        {
            await SearchJobs(SearchParms);
            return Page();
        }

    }
}
