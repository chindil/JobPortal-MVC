using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.CRM;
using Stx.Shared.Models.CRM;
using Stx.Shared.Models.Parm;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Corporate
{
    public class SearchModel : BasePageModel
    {
        private readonly ICorporatePublicDataService _corporatePublicDataService;

        public SearchModel(ICorporatePublicDataService corporatePublicDataService)
        {
            _corporatePublicDataService = corporatePublicDataService;
        }

        //public string PageKey { get; set; } = "AdditnlInfo";
        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public List<CorporatePublicDTO> BaseEntry { get; set; }
        [BindProperty] public string Keyword { get; set; }
        [BindProperty] public string Location { get; set; }
        [BindProperty] public string JobIndustries { get; set; }


        public async Task OnGetAsync() 
        {
            try
            {
                var parm = new HrJobSearchParmDTO() { Keyword = Keyword, Location = Location, CandidateID = CandidateID };
                BaseEntry = await _corporatePublicDataService.Search(parm);               
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);                
            }
        }

        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                StatusMessage.Reset();
                var parm = new HrJobSearchParmDTO() { Keyword = Keyword, Location = Location, CandidateID = CandidateID };
                BaseEntry = await _corporatePublicDataService.Search(parm);               
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                return Page();
            }
        }
    }
}
