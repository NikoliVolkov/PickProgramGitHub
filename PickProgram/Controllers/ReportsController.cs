using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickProgram.ViewModels;
using PickProgram.Models;

namespace PickProgram.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public ReportsController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult SummaryReport()
        {

            var rvm = new ReportsViewModel() {
                SummaryListOfInvoices = new List<Invoice>()
            };
            return View(rvm);
        }
        [HttpPost]
        public IActionResult SummaryReport(ReportsViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                rvm.SummaryListOfInvoices = _invoiceRepository.GetAllCompletedInvoicesForRange(Convert.ToDateTime(rvm.StartDate), Convert.ToDateTime(rvm.EndDate));
                return View(rvm);
            }
            rvm.SummaryListOfInvoices = new List<Invoice>();
            return View(rvm);
        }
        public IActionResult GetEmptyReport()
        {
            var emptyInvoiceList = new List<Invoice>();
            return PartialView("_SummaryInvoiceList", emptyInvoiceList);
        }
    }
}