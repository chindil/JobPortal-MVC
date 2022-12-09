using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Web.JP.Extensions;

namespace Stx.Web.JP.Core
{
    public class JPPageSetting : PageModel
    {
        public static string HomePage { get; private set; } = "/jobs/search";

    }
}
