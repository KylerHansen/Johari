using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Johari.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {

                //get the current User Id
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string aspNetId = claim.Value;

                if (User.IsInRole(SD.ClientRole))
                {
                    Client client = _unitofWork.Client.Get(c => c.AspNetUsersId == aspNetId);

                    if(client == null)
                    {
                        return RedirectToPage("/Clients/CreateClient");
                    }
                                                               
                    
                }else if (User.IsInRole(SD.FriendRole))
                {
                    Friend friend = _unitofWork.Friend.Get(f => f.AspNetUsersId == aspNetId);

                    if (friend == null)
                    {
                        return RedirectToPage("/Friends/EnterId");
                    }
                }
                else
                {
                    //Admin Role
                }

                return Page();

            }
            else
            {
                return Page(); //No account
            }
        }
        
        //TODO DEPENDING ON THE ROLE DIRECT THEM TO DIFFERENT PAGES. 
        //If the client doesn't have an id send them to create one.      
    }
}
