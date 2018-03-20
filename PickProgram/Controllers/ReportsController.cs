using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickProgram.ViewModels;

namespace PickProgram.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            var avm = new ReportsViewModel(){ };
            return View(avm);
        }
        [HttpPost]
        public IActionResult Index(int a)
        {
            return View();
        }
    }
}