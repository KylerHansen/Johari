using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Shared
{
    public class ProhibitedModel : PageModel
    {                
        [BindProperty]
        public string UrlPath { get; set; }
        [BindProperty]
        public string Reason { get; set; }
        public IActionResult OnGet(string path, string reason)
        {
            UrlPath = path;
            Reason = reason;

            return Page();
        }
    }
}
