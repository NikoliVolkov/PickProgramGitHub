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
            var activeEmployees = _employeeRepository.GetEmployees().Where(e => e.DeactivateDate == null && e.Nickname != "Offsite").ToList();
            var inactiveEmployees = _employeeRepository.GetEmployees().Where(e => e.DeactivateDate.HasValue && e.Nickname != "Offsite").ToList();
            var emvm = new EmployeeManageViewModel
            {
                ActiveEmployees = activeEmployees,
                InactiveEmployees = inactiveEmployees
            };
            return View(emvm);
        }
    }
}