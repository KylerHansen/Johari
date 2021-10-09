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

        List<ClientResponse> clientResponses { get; set; }
        List<FriendResponse> friendResponses { get; set; }

        
        [BindProperty]
        public List<Adjective> adjectivesList { get; set; }
                
        [BindProperty]
        public Client client { get; set; }
        [BindProperty]
        public Dictionary<string, int> row0column0 { get; set; }
        [BindProperty]
        public Dictionary<string, int> row0column1 { get; set; }
        [BindProperty]
        public Dictionary<string, int> row1column0 { get; set; }
        [BindProperty]
        public Dictionary<string, int> row1column1 { get; set; }


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

                var query = from adj in adjectivesList
                            join cr in clientResponses on adj.Id equals cr.AdjectiveId
                            join fr in friendResponses on cr.AdjectiveId equals fr.AdjectiveId                           
                            group adj by adj.Name into grp                            
                            select new { key = grp.Key, Total = grp.Count()};

                row0column0 = new Dictionary<string, int>();
                row0column1 = new Dictionary<string, int>();
                row1column0 = new Dictionary<string, int>();
                row1column1 = new Dictionary<string, int>();

                foreach (var v in query)
                {                   
                    row0column0.Add(v.key,v.Total);                    
                }

                var query2 = from adj in adjectivesList                             
                             join fr in friendResponses on adj.Id equals fr.AdjectiveId                            
                             group adj by adj.Name into grp
                             select new { key = grp.Key, Total = grp.Count() };

                var query3 = from adj in adjectivesList
                             join cr in clientResponses on adj.Id equals cr.AdjectiveId
                             group adj by adj.Name into grp
                             select new { key = grp.Key, Total = grp.Count() };
               


                foreach (var v in query2)
                {                                       
                    if (!row0column0.ContainsKey(v.key))
                    {
                        row0column1.Add(v.key, v.Total);
                    }                    
                }


                foreach (var v in query3)
                {

                    if (!row0column0.ContainsKey(v.key))
                    {
                        row1column0.Add(v.key, v.Total);
                    }
                }

                foreach( var adj in adjectivesList)
                {
                    string name = adj.Name;
                    if(!row0column0.ContainsKey(name) && !row0column1.ContainsKey(name) && !row1column0.ContainsKey(name))
                    {
                        row1column1.Add(name, 0);
                    }
                }
     
            }

            return Page();
        }
    }
}
