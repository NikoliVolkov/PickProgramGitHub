using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PickProgram.Models
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceTrackerContext _dbConnection;
        public InvoiceRepository(InvoiceTrackerContext dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _dbConnection.Invoice.FirstOrDefault(p => p.InvoiceId == invoiceId);
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            var invoiceData = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Include(p => p.PickLocation);
            return invoiceData;
        }

        public IQueryable<Invoice> GetCompletedInvoicesForToday()
        {
            var today = DateTime.Today;
            var completedInvoicesForToday = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Where(p => p.FinishDate.Value.Date == today && p.Status.Status == "Complete");
            //var today = DateTime.Today;
            //var q = db.Games.Where(t => DbFunctions.TruncateTime(t.StartDate) >= today);

            return completedInvoicesForToday.AsQueryable();
        }
        
        public void AddInvoice(Invoice newInvoice)
        {
            _dbConnection.Invoice.Add(newInvoice);
            _dbConnection.SaveChanges();
        }

        public string AssignEmployee(int invoiceId, int employeeId)
        {
            var invoice = _dbConnection.Invoice.Find(invoiceId);
            invoice.AssignedEmployeeId = employeeId;
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var utcNow = DateTime.UtcNow;
            var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone);
            invoice.AssignedDate = pacificNow;
            _dbConnection.SaveChanges();

            DateTime dotNetTicks = new DateTime(invoice.AssignedDate.Value.Year, invoice.AssignedDate.Value.Month, invoice.AssignedDate.Value.Day, invoice.AssignedDate.Value.Hour, invoice.AssignedDate.Value.Minute, invoice.AssignedDate.Value.Second);

            //check if currently in daylight savings to modify display
            DateTime currentDate = DateTime.Now; 
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            bool isCurrentlyDaylightSavings = tzi.IsDaylightSavingTime(currentDate);

            double jsTimestamp = (dotNetTicks.AddHours(8).Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            if (isCurrentlyDaylightSavings)
            {
                jsTimestamp = (dotNetTicks.AddHours(7).Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            }
          


            var emp = _dbConnection.Employee.Find(employeeId);
            return JsonConvert.SerializeObject(new { assignedEmployee = emp.Nickname, assignedOn = jsTimestamp });

        }

        public void CancelInvoice(int invoiceId)
        {
            _dbConnection.Invoice.Remove(_dbConnection.Invoice.Find(invoiceId));
            _dbConnection.SaveChanges();            
            
        }
    }
}
