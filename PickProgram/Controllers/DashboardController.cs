using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PickProgram.Models;
using PickProgram.ViewModels;


namespace PickProgram.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPickLocationRepository _picklocationRepository;
        public DashboardController(IInvoiceRepository invoiceRepository, IEmployeeRepository employeeRepository, IPickLocationRepository pickLocationRepository)
        {
            _invoiceRepository = invoiceRepository;
            _employeeRepository = employeeRepository;
            _picklocationRepository = pickLocationRepository;
        }
        public IActionResult Main()
        {
            var dvm = new DashboardViewModel()
            {
                Employees = PopulateEmployeeSelectList(),
                PickLocations = _picklocationRepository.GetPickLocations().ToList(),
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
                    Employees = PopulateEmployeeSelectList(),
                    PickLocations = _picklocationRepository.GetPickLocations().ToList(),
                    ListOfInvoices = _invoiceRepository.GetInvoices().OrderBy(p => p.StartDate).ToList()
                };
                return View(dvmUpdated);
            }

            dvm.Employees = PopulateEmployeeSelectList();
            dvm.PickLocations = _picklocationRepository.GetPickLocations().ToList();
            dvm.ListOfInvoices = _invoiceRepository.GetInvoices().OrderBy(p => p.StartDate).ToList();
            return View(dvm);
        }

        public List<SelectListItem> PopulateEmployeeSelectList()
        {
            var empSelectList = new List<SelectListItem>();
            var empList =_employeeRepository.GetEmployees().ToList();
            foreach(var x in empList)
            {
                empSelectList.Add(new SelectListItem()
                {
                    Value = x.EmployeeId.ToString(),
                    Text = x.LastName + ", " + x.FirstName
                });
            }

            return empSelectList.OrderBy(x => x.Text).ToList();
        }
    }
}