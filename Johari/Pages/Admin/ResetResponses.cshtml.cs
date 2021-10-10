using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Admin
{
    public class ResetResponsesModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public ResetResponsesModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        Client client { get; set; }
        List<ClientResponse> clientResponses { get; set; }
        List<FriendResponse> friendResponses { get; set; }

        public IActionResult OnGet(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SD.AdminRole))
            {
                return RedirectToPage("/Shared/Prohibited", new { path = "/Admin/SearchClient", reason = "You are not signed in." });
            }

            if(id != null)
            {
                client = _unitofWork.Client.Get(c => c.Id == id);

                if(client != null)
                {

                    clientResponses = _unitofWork.ClientResponse.List(r => r.ClientId == client.Id).ToList();
                    friendResponses = _unitofWork.FriendResponse.List(r => r.ClientId == client.Id).ToList();

                    if(clientResponses.Count > 0)
                    {
                        foreach(var res in clientResponses)
                        {
                            _unitofWork.ClientResponse.Delete(res);
                        }
                    }

                    if (friendResponses.Count > 0)
                    {
                        foreach (var res in friendResponses)
                        {
                            _unitofWork.FriendResponse.Delete(res);
                        }
                    }

                    client.ResponseSubmissionCount = 0;
                    client.hasResponded = false;

                    _unitofWork.Client.Update(client);

                    _unitofWork.Commit();

                    return Page();
                }                
            }

            return NotFound();
        }
    }
}
