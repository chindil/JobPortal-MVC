using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Blog
{
    public class MainModel : BasePageModel
    {
        public void OnGet()
        {
            StatusMessage.SetSuccess("Dummy Status Message.");
        }
    }
}
