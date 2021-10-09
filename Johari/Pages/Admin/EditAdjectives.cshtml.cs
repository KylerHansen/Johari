using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Admin
{
    public class EditAdjectivesModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public EditAdjectivesModel(IUnitofWork unitofWork) => _unitofWork = unitofWork;

        [BindProperty]
        public Adjective adjective { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //retrieve the role from the form
            string type = Request.Form["rdType"].ToString();

            if(type != "")
            {
                adjective.Type = (byte)int.Parse(type);
            }                        

            if (ModelState.IsValid)

                if (adjective != null)
            {
                _unitofWork.Adjective.Add(adjective);
            }

            return RedirectToPage("/Admin/EditAdjectives");
            
        }
    }
}
