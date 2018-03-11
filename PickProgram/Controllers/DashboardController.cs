using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PickProgram.Models;
using PickProgram.ViewModels;
//using Newtonsoft.Json;


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
                Employees = PopulateEmployeeSelectListOnsite(),
                PickLocations = GetPickLocations(),
                ListOfInvoicesOnsite = GetOnsiteInvoices(),
                ListOfInvoicesOffsite = GetOffsiteInvoices()
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
                if(dvm.NewInvoice.AssignedEmployeeId != null)
                {
                    dvm.NewInvoice.AssignedDate = pacificNow;
                }

                _invoiceRepository.AddInvoice(dvm.NewInvoice);

                TempData["Message"] = "Invoice created successfully.";                

                return RedirectToAction("Main");

            }

            dvm.Employees = PopulateEmployeeSelectListOnsite();
            dvm.PickLocations = GetPickLocations();
            dvm.ListOfInvoicesOnsite = GetOnsiteInvoices();
            return View(dvm);
        }
        /* No need to use this, can update using jquery
        public string RefreshEmployeeSelectList()
        {
            var empArray = new List<Object>();
            var empList =_employeeRepository.GetEmployeesUnassigned().Where(e => e.Nickname != "Offsite").OrderBy(e => e.Nickname).ToList();
            foreach(var x in empList)
            {
                empArray.Add(new
                {
                    Value = x.EmployeeId.ToString(),
                    Text = x.Nickname
                });
            }

            return JsonConvert.SerializeObject(empArray);
        }*/

        public List<SelectListItem> PopulateEmployeeSelectListOnsite()
        {
            var empSelectList = new List<SelectListItem>();
            var empList = _employeeRepository.GetEmployeesUnassigned().Where(e => e.Nickname != "Offsite").ToList();
            foreach (var x in empList)
            {
                empSelectList.Add(new SelectListItem()
                {
                    Value = x.EmployeeId.ToString(),
                    Text = x.Nickname
                });
            }

            return empSelectList.OrderBy(x => x.Text).ToList();
        }

        public List<PickLocation> GetPickLocations()
        {
            return _picklocationRepository.GetPickLocations().ToList();
        }

        public List<Invoice> GetOnsiteInvoices()
        {
            return _invoiceRepository.GetInvoices().Where(p => p.PickLocation.LocationDescription != "Offsite" && p.Status.Status != "Complete").ToList();
            //return _invoiceRepository.GetInvoices().Where(p => p.PickLocation.LocationDescription != "Offsite").OrderByDescending(p => p.AssignedDate.HasValue).ThenBy(p => p.AssignedDate).ToList();
        }
        public List<Invoice> GetOffsiteInvoices()
        {
            return _invoiceRepository.GetInvoices().Where(p => p.PickLocation.LocationDescription == "Offsite" && p.Status.Status != "Complete").ToList();
        }

        public IActionResult GetStatsVC()
        {
            //return PartialView("_StatSection", svm);
            return ViewComponent("Stats");
        }

        public IActionResult GetEmployeeDDLUnassigned()
        {
            var empSelectList = PopulateEmployeeSelectListOnsite();
            return PartialView("_EmployeeDropdown", empSelectList);
        }
        public string AssignEmployee(int id, int id2)
        {
            return _invoiceRepository.AssignEmployee(id, id2);
        }
        [HttpPost]
        public IActionResult CloseInvoice(int id)
        {
            return _invoiceRepository.CloseInvoice(id);
        }
        [HttpPost]
        public IActionResult CancelInvoice(int id)
        {
            return _invoiceRepository.CancelInvoice(id);
        }
    }
}