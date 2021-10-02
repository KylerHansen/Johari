using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Clients
{
    public class ResponseSubmissionConfirmedModel : PageModel
    {
        public string userAspId;
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(SD.ClientRole))
                {
                    //get the current User Id
                    var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    userAspId = claim.Value;

                    return Page();
                }
                else
                {
                    //Navigate Friend to Thank You page. 
                    return RedirectToPage("/Friends/ThankYou");
                }
            }
            else
            {
                return RedirectToPage("/Shared/Prohibited", new { path = "/Clients/SubmissionConfirmed", reason = "You are not signed in." });
            }
        }
    }
}
