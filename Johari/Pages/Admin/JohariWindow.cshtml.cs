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
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        Client client { get; set; }        
        List<ClientResponse> clientResponses { get; set; }
        List<FriendResponse> friendResponses { get; set; }
        List<Adjective> adjectivesList { get; set; }


        public IActionResult OnGet(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SD.AdminRole))
            {
                return RedirectToPage("/Shared/Prohibited", new { path = "/Admin/JohariWindow", reason = "You are not signed in." });
            }

            if(id != null)
            {               
                client = _unitofWork.Client.Get(c => c.Id == id);

                clientResponses = _unitofWork.ClientResponse.List(r => r.ClientId == client.Id).ToList();
                friendResponses = _unitofWork.FriendResponse.List(r => r.ClientId == client.Id).ToList();
                adjectivesList = _unitofWork.Adjective.List().ToList();

                var query = from clientRe in clientResponses
                            join adj in adjectivesList on clientRe.AdjectiveId equals adj.Id
                            join fr in friendResponses on clientRe.AdjectiveId equals fr.AdjectiveId                           
                            group adj by adj.Name into grp
                            select new { key = grp.Key, Total = grp.Count()};

                foreach(var v in query)
                {
                    System.Diagnostics.Debug.WriteLine($"{v.key+":"}{v.Total}");
                }
            }

            //Select AdjName, Count(AdjName)
            //FROM Adjective a
            //LEFT JOIN ClientResponse cr
            //ON cr.adjectiveID = a.id
            //LEFT JOIN FriendResponse fr
            //ON cr.AdjectiveId = fr.AdjectiveId
            //WHERE cr.ClientID like 'ClientIdstring'
            //AND fr.AdjectiveId IS NULL
            //GROUP BY AdjName


            //--Hidden(bottom left corner).Adjectives that are only on client's list and not on any of the friend’s lists (Count = 1) 
            //Select AdjName, Count(AdjName)
            //FROM Adjective a
            //LEFT JOIN ClientResponse cr
            //ON cr.adjectiveID = a.id
            //LEFT JOIN FriendResponse fr
            //ON cr.AdjectiveId = fr.AdjectiveId
            //WHERE cr.ClientID like 'ClientIdstring'
            //AND fr.AdjectiveId IS NOT NULL
            //GROUP BY AdjName

            //--Blind(upper right corner).Words that are only on your family's and friend's list but not on your own(Count > 1 AND ClientID is null)
            //Select AdjName, Count(AdjName)
            //FROM Adjective a
            //LEFT JOIN FriendResponse fr
            //ON fr.adjectiveID = a.id
            //LEFT JOIN ClientResponse cr
            //ON cr.AdjectiveId = fr.AdjectiveId
            //WHERE fr.ClientID like 'ClientIdstring'
            //AND cr.AdjectiveId IS NULL
            //GROUP BY AdjName

            //--Unknown: (bottom right corner). "UNSHARED WORDS"(Count = 0)
            //Select AdjName, Count(AdjName)
            //FROM Adjective a
            //LEFT JOIN ClientResponse cr
            //ON cr.adjectiveID = a.id
            //LEFT JOIN FriendResponse fr
            //ON a.Id = fr.AdjectiveId
            //WHERE cr.ClientID like 'ClientIdstring'
            //AND a.Id IS NOT NULL
            //GROUP BY AdjName




            return Page();
        }
    }
}
