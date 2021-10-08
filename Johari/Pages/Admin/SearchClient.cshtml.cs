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
    public class SearchClientModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public SearchClientModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [BindProperty]
        public int newResponseLimit { get; set; }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SD.AdminRole))
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {            

            List<Client> clientList = new();

            clientList = (List<Client>)_unitofWork.Client.List();

            if(clientList != null && newResponseLimit > 0)
            {
                foreach(Client c in clientList)
                {
                    c.ResponseLimit = newResponseLimit;
                    _unitofWork.Client.Update(c);
                }
            }

            _unitofWork.Commit();

            return Page();
        }
    }
}
