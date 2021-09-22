using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
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
            id = 1;

            UserClient = new Client();

            if (id != 0)
            {
                UserClient = _unitofWork.Client.Get(c => c.Id == id); //get the client 

                if (UserClient == null)
                {
                    return NotFound();
                }
                
                         
                List<Adjective> AdjectiveList = new List<Adjective>();
                AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List();

                ListOfPosAdjectives = new List<Adjective>();
                ListOfNegAdjectives = new List<Adjective>();

                foreach (Adjective adj in AdjectiveList)
                {
                    if(adj.Type == 1)
                    {
                        ListOfPosAdjectives.Add(adj);
                    }
                    else
                    {
                        ListOfNegAdjectives.Add(adj);
                    }
                }


                PosAdjectives = ListOfPosAdjectives.ToList<Adjective>()
                    .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString()})
                    .ToList<SelectListItem>();

                NegAdjectives = ListOfNegAdjectives.ToList<Adjective>()
                    .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                    .ToList<SelectListItem>();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            
            try
            {  

                List<ClientResponse> clientResponses = new List<ClientResponse>();
                //check if there is any existing responses
                clientResponses = (List<ClientResponse>)_unitofWork.ClientResponse.List(c => c.ClientId == UserClient.Id).ToList();               

                if (clientResponses.Count > 0)
                {        //Remove any previous responses.         
                    _unitofWork.ClientResponse.Delete(clientResponses);
                }

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
            foreach (SelectListItem Adjective in PosAdjectives)
            {
                if (Adjective.Selected)
                {
                    ClientResponse Response = new ClientResponse
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
                    ClientResponse Response = new ClientResponse
                    {
                        AdjectiveId = Int32.Parse(Adjective.Value),
                        ClientId = UserClient.Id
                    };
                    _unitofWork.ClientResponse.Add(Response);
                }
            }


            _unitofWork.Commit();


            return RedirectToPage("./Index");
            //return Page(); //send to submission confirmation page. 

        }
    }
}
