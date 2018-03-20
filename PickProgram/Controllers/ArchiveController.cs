using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickProgram.Models;
using PickProgram.ViewModels;

namespace PickProgram.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public ArchiveController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public IActionResult Index()
        {
            var completedInvoices = _invoiceRepository.GetAllCompletedInvoices().OrderBy(p => p.AssignedEmployee.Nickname).ToList();
            //var avm = new ArchiveViewModel(){ };
            return View(completedInvoices);
        }
    }
}