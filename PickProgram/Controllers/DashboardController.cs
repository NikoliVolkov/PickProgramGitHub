using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickProgram.Models;
using PickProgram.ViewModels;

namespace PickProgram.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public DashboardController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public IActionResult Main()
        {

            var dvm = new DashboardViewModel()
            {
                ListOfInvoices = _invoiceRepository.GetInvoices().OrderBy(p => p.StartDate).ToList()
            };
            return View(dvm);
        }
        [HttpPost]
        public IActionResult Main(DashboardViewModel dvm)
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var utcNow = DateTime.UtcNow;
            var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone);
            dvm.NewInvoice.StartDate = pacificNow;
            if (ModelState.IsValid)
            {
                _invoiceRepository.AddInvoice(dvm.NewInvoice);
                var dvmUpdated = new DashboardViewModel()
                {
                    SuccessMessage = "Invoice created successfully.",
                    ListOfInvoices = _invoiceRepository.GetInvoices().OrderBy(p => p.StartDate).ToList()
                };
                return View(dvmUpdated);
            }

            dvm.ListOfInvoices = _invoiceRepository.GetInvoices().OrderBy(p => p.StartDate).ToList();
            return View(dvm);
        }
    }
}