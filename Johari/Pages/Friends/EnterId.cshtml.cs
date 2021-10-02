using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Friends
{
    public class EnterIdModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public EnterIdModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [BindProperty]
        public string RefferedClientId { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(SD.FriendRole))
                {                  
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/EnterId", reason = "Must be logged in a friends & family account." });
                }
            }
            else
            {
                

                return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/EnterId", reason = "Must be logged in a friends & family account." });

            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //get the current User Id
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string aspNetId = claim.Value;

            Friend friend = _unitofWork.Friend.Get(f => f.AspNetUsersId == aspNetId);
            int refferedClientId = _unitofWork.Client.Get(c => c.AspNetUsersId == RefferedClientId).Id;

            if (friend == null)
            {
                return RedirectToPage("/Friends/CreateFriend", new { clientId = RefferedClientId });
            }

            return RedirectToPage("/Clients/Responses", new { id = refferedClientId });
        }
    }
}
