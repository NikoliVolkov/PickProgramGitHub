using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickProgram.Models;
using PickProgram.ViewModels;

namespace PickProgram.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ManageController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Personnel()
        {
            //var completedInvoices = _invoiceRepository.GetAllCompletedInvoicesLast30().ToList();
            //var avm = new ArchiveViewModel(){ };
            return View();
        }
    }
}