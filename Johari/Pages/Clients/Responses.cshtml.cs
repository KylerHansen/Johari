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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Johari.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }               

        [BindProperty]
        public IList<SelectListItem> PosAdjectives { get; set; }

        [BindProperty]
        public IList<SelectListItem> NegAdjectives { get; set; }
        
        public List<Adjective> ListOfPosAdjectives { get; set; }

        public List<Adjective> ListOfNegAdjectives { get; set; }

        [BindProperty]
        public Client UserClient { get; set; }          


        public IActionResult OnGet(int ? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Shared/Prohibited", new { path = "/Clients/Responses", reason = "You are not signed in." });
            }

            UserClient = new Client();
            

            if (id == null && User.IsInRole(SD.ClientRole))
            {                                       
                //get the current Users Client data
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                UserClient = _unitofWork.Client.Get(c => c.AspNetUsersId == claim.Value); 
                
                if(UserClient.hasResponded == true)
                {
                    return RedirectToPage("./SubmissionConfirmed");
                }
            }
            else if(User.IsInRole(SD.FriendRole))                          
            {
                //get client based on id passed in. 
                UserClient = _unitofWork.Client.Get(c => c.Id == id);                
            }
            else
            {               
                return NotFound();
            }


            if (UserClient == null)
            {
                return NotFound();
            }

            List<Adjective> AdjectiveList = new();
            AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List();

            ListOfPosAdjectives = new List<Adjective>();
            ListOfNegAdjectives = new List<Adjective>();

            foreach (Adjective adj in AdjectiveList)
            {
                if (adj.Type == 1)
                {
                    ListOfPosAdjectives.Add(adj);
                }
                else
                {
                    ListOfNegAdjectives.Add(adj);
                }
            }

            PosAdjectives = ListOfPosAdjectives.ToList<Adjective>()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .ToList<SelectListItem>();

            NegAdjectives = ListOfNegAdjectives.ToList<Adjective>()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .ToList<SelectListItem>();           

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            if (User.IsInRole(SD.ClientRole))
            {
                try
                {
                    List<ClientResponse> clientResponses = new List<ClientResponse>();
                    //check if there is any existing responses
                    clientResponses = (List<ClientResponse>)_unitofWork.ClientResponse.List(c => c.ClientId == UserClient.Id).ToList();

                    if (clientResponses.Count > 0)
                    {    //Remove any previous responses.         
                        _unitofWork.ClientResponse.Delete(clientResponses);
                    }

                } catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                foreach (SelectListItem Adjective in PosAdjectives)
                {
                    if (Adjective.Selected)
                    {
                        ClientResponse Response = new()
                        {
                            AdjectiveId = Int32.Parse(Adjective.Value),
                            ClientId = UserClient.Id
                        };
                        _unitofWork.ClientResponse.Add(Response);
                    }
                }

                foreach (SelectListItem Adjective in NegAdjectives)
                {
                    if (Adjective.Selected)
                    {
                        ClientResponse Response = new()
                        {
                            AdjectiveId = Int32.Parse(Adjective.Value),
                            ClientId = UserClient.Id
                        };
                        _unitofWork.ClientResponse.Add(Response);
                    }
                }


                //Update Client Response               
                Client person = _unitofWork.Client.Get(c => c.Id == UserClient.Id);
                person.hasResponded = true;
                _unitofWork.Client.Update(person);

            }
            else if (User.IsInRole(SD.FriendRole))
            {                                                
                //get the current User Id which is friend id
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);              
                
                List<FriendResponse> friendResponses = new List<FriendResponse>();
                int friendId;
                int clientId;

                try
                {
                    friendId = _unitofWork.Friend.Get(f => f.AspNetUsersId == claim.Value).Id;
                    clientId = UserClient.Id;

                    //check if there are any responses where friend id is user id and client id is client id
                    friendResponses = (List<FriendResponse>)_unitofWork.FriendResponse.List(r => r.FriendId == friendId && r.ClientId == clientId).ToList();

                    if (friendResponses.Count > 0)
                    {    //Remove any previous responses that the friend has made.          
                        _unitofWork.FriendResponse.Delete(friendResponses);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                foreach (SelectListItem Adjective in PosAdjectives)
                {
                    if (Adjective.Selected)
                    {
                        FriendResponse Response = new()
                        {
                            AdjectiveId = Int32.Parse(Adjective.Value),
                            ClientId = clientId,
                            FriendId = friendId
                        };
                        _unitofWork.FriendResponse.Add(Response);
                    }
                }

                foreach (SelectListItem Adjective in NegAdjectives)
                {
                    if (Adjective.Selected)
                    {
                        FriendResponse Response = new()
                        {
                            AdjectiveId = Int32.Parse(Adjective.Value),
                            ClientId = clientId,
                            FriendId = friendId
                        };
                        _unitofWork.FriendResponse.Add(Response);
                    }
                }

                //Update Client Count
                Client updateClient = _unitofWork.Client.Get(c => c.Id == clientId);
                
                updateClient.ResponseSubmissionCount += 1;

                _unitofWork.Client.Update(updateClient);


            }


            _unitofWork.Commit();

            return RedirectToPage("./SubmissionConfirmed");

        }
    }
}
