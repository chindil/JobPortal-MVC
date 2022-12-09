using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.CRM;
using Stx.Shared.Models.CRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Corporate
{
    public class DetailsModel : BasePageModel
    {
        private readonly ICorporatePublicDataService _corporatePublicDataService;

        public DetailsModel(ICorporatePublicDataService corporatePublicDataService)
        {
            _corporatePublicDataService = corporatePublicDataService;
        }

        [BindProperty] public CorporatePublicDTO InputModel { get; set; } = new CorporatePublicDTO();
        
        public async Task OnGet(int CorporateID)
        {
            try
            {
                StatusMessage.Reset();
                InputModel = await _corporatePublicDataService.GetRecordByID(CorporateID, CandidateID);
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
            }
        }
    }
}
