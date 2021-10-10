﻿using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Johari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjectiveController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public AdjectiveController(IUnitofWork unitofWork) => _unitofWork = unitofWork;

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitofWork.Adjective.List() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitofWork.Adjective.Get(c => c.Id == id); //get it from database
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitofWork.Adjective.Delete(objFromDb);
            _unitofWork.Commit();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
