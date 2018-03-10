using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;
using PickProgram.ViewModels;

namespace PickProgram.ViewComponents
{
    public class StatsViewComponent : ViewComponent
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public StatsViewComponent(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stats = await GetStatsAsync();
            int totalParts = 0;

            var totalInvoices = stats.Count();
            foreach (var p in stats)
            {
                totalParts += p.TotalParts;
            }

            var svm = new StatsViewModel()
            {
                TotalInvoices = totalInvoices,
                TotalParts = totalParts,
                EmployeeStats = stats
            };

            return View(svm);
        }
        private Task<List<EmployeeStat>> GetStatsAsync()
        {
            var invoicesForToday = _invoiceRepository.GetCompletedInvoicesForToday();
            var aggregateStats = invoicesForToday.GroupBy(p => p.AssignedEmployee.Nickname).Select(n => new EmployeeStat { Nickname = n.First().AssignedEmployee.Nickname, TotalParts = n.Sum(p => p.NumberOfParts), TotalInvoices = n.Count() }).OrderByDescending(n => n.TotalParts);


            return aggregateStats.ToListAsync();
        }
    }
}