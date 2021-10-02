using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Friends
{
    public class ThankYouModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(SD.ClientRole))
                {
                    return NotFound();
                }

                return Page();
            }

            return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/ThankYou", reason = "You are not signed in." });
        }
    }
}
