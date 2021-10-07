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
        public IActionResult OnGet(string id)
        {
            //TODO: THIS NEEDS TO BE CLEANED UP.
            if (User.Identity.IsAuthenticated && id != null)
            {
                if (User.IsInRole(SD.FriendRole))
                {
                    //get current user                    
                    var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    string friendAspNetId = claim.Value;                  

                    try
                    {    //if the client id is valid
                        Client checkClient = _unitofWork.Client.Get(c => c.AspNetUsersId == id);
                        if (checkClient != null)
                        {   //if the friend has an account already
                            Friend checkFriend = _unitofWork.Friend.Get(f => f.AspNetUsersId == friendAspNetId);
                            if (checkFriend != null)
                            {   //if the friend has made a submission for the client with the given id already
                                FriendResponse checkSubmission = _unitofWork.FriendResponse.Get(r => r.ClientId == checkClient.Id && r.FriendId == checkFriend.Id);
                                if(checkSubmission != null)
                                {
                                    //redirect to submission made
                                    return RedirectToPage("/Friends/ThankYou");
                                }
                                else
                                {
                                    if (checkClient.ResponseSubmissionCount < checkClient.ResponseLimit)
                                    {
                                        //proceed to response page. 
                                        return RedirectToPage("/Clients/Responses", new { id = checkClient.Id });
                                    }
                                    return RedirectToPage("/Friends/ResponseLimitReached");
                                }
                            }
                            else
                            {
                                RefferedClientId = id;
                                return Page();
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception e)
                    {
                        return NotFound();
                    }                    
                   
                }
                else
                {
                    return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/EnterId?id="+id, reason = "Must be logged in a friends & family account." });
                }
            }
            else
            {                
                return RedirectToPage("/Shared/Prohibited", new { path = "/Friends/EnterId?id="+id, reason = "Must be logged in a friends & family account." });

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

            try
            {
                Friend friend = _unitofWork.Friend.Get(f => f.AspNetUsersId == aspNetId);
                int refferedClientId = _unitofWork.Client.Get(c => c.AspNetUsersId == RefferedClientId).Id;

                if (friend == null)
                {
                    return RedirectToPage("/Friends/CreateFriend", new { clientId = RefferedClientId });
                }

                return RedirectToPage("/Clients/Responses", new { id = refferedClientId });
                }
            catch (Exception e)
            {
                return RedirectToPage("/Friends/CreateFriend", new { clientId = RefferedClientId });
            }

        }
    }
}
