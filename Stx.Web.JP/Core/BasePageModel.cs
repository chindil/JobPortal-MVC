using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Exceptions;
using Stx.Shared.Extensions.Common;
using Stx.Web.JP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stx.Web.JP.Core
{
    public class BasePageModel : PageModel
    {

        [BindProperty] public Stx.Shared.Common.AlertMsg StatusMessage { get; set; } = new Stx.Shared.Common.AlertMsg();
        [BindProperty] public PageStatus PageStatus { get; set; } = new PageStatus();

        public BasePageModel()
        {
            StatusMessage.Reset();
            PageStatus.IsLoading = false;
        }

        private int _CandidateID = 0;
        public int CandidateID
        {
            get
            {
                if (_CandidateID > 0)
                    return _CandidateID;
                else 
                {
                    _CandidateID = HttpContext.User.Identity.GetId(); 
                    return _CandidateID;
                }
            }
        }

        public bool IsLoading
        {
            set { PageStatus.IsLoading = value; }
        }

        protected void InitializePage()
        {
            StatusMessage.Reset();
            PageStatus.IsLoading = false;            
        }

        public void SetPageStatus(Exception ex, bool isLoading = false)
        {
            IsLoading = false;
            if (ex is StxHttpResponseException rex)
            {
                PageStatus.StatusCode = (int)rex.StatusCode;
            }
            else
            {
                PageStatus.StatusCode = 1;
            }
        }


    }
}
