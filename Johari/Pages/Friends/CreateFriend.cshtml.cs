using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Friends
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [BindProperty]
        public Friend userFriend { get; set; } 
        
        [BindProperty]
        public string clientAspId { get; set; }
                
        public ApplicationUser referredClient { get; set; }              

        public IActionResult OnGet(string clientId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(SD.FriendRole))
                {
                    userFriend = new Friend();
                    referredClient = new ApplicationUser();

                    clientAspId = clientId;
                   
                    referredClient = _unitofWork.ApplicationUser.Get(c => c.Id == clientId);
                    

                    if (referredClient == null)
                    {
                        return NotFound();
                    }

                    return Page();

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {

                return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/CreateFriend", reason = "You are not signed in." });

            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {               
                return Page();
            }

            try
            {
                

                //get the current User Id
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userFriend.AspNetUsersId = claim.Value;               

               
         

                _unitofWork.Friend.Add(userFriend);
                _unitofWork.Commit();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            //Maybe I should just pass in the client Id instead of the aspnet user id. 

            int clientId = _unitofWork.Client.Get(c => c.AspNetUsersId == clientAspId).Id;


            return RedirectToPage("/Clients/Responses", new { id = clientId});
        }
    }
}
