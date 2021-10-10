using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Clients
{
    public class CreateClientModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public CreateClientModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
             
        [BindProperty]
        public DateTime birthday { get; set; }              

        public ApplicationUser aspNetUser { get; set; }
      

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Shared/Prohibited", new { path = "/Clients/CreateClient", reason = "You are not signed in." });
            }           


            return Page();
        }

        public IActionResult OnPost()
        {
            
            //get the current User Id
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            aspNetUser = _unitofWork.ApplicationUser.Get(c => c.Id == claim.Value);

            Client userClient = new Client();

            string gender = Request.Form["rdUserGender"].ToString();

            if (aspNetUser != null)
            {
                userClient.AspNetUsersId = claim.Value;
                userClient.First_Name = aspNetUser.FirstName;
                userClient.Last_Name = aspNetUser.LastName;
                userClient.Gender = gender;
                userClient.Birthday = birthday;
                userClient.ResponseLimit = 4;
                userClient.ResponseSubmissionCount = 0;
                userClient.hasResponded = false;

                _unitofWork.Client.Add(userClient);

                _unitofWork.Commit();
                
                return RedirectToPage("/Clients/Responses");
            }
            

            return Page(); //direct them to responses page. 
        }
    }
}
