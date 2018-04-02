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
            foreach(var inv in invoiceData)
            {
                string tickValue = inv.StartDate.Ticks.ToString();
                inv.StartDateInTicks = tickValue;
            }
            return invoiceData;
        }
        public IEnumerable<Invoice> GetPendingInvoices()
        {
            var invoiceData = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Include(p => p.PickLocation).Where(p => p.Status.Status != "Complete");
            foreach (var inv in invoiceData)
            {
                string tickValue = inv.StartDate.Ticks.ToString();
                inv.StartDateInTicks = tickValue;
            }
            return invoiceData;
        }
        /*public IQueryable<Invoice> GetCompletedInvoicesForDate(DateTime reportDate)
        {
            var completedInvoicesForDate = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Where(p => p.FinishDate.Value.Date == reportDate.Date && p.Status.Status == "Complete");
            //var today = DateTime.Today;
            //var q = db.Games.Where(t => DbFunctions.TruncateTime(t.StartDate) >= today);

            return completedInvoicesForDate.AsQueryable();
        }*/
        public IEnumerable<Invoice> GetAllCompletedInvoicesLast30()
        {
            //get 30 days before today
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var utcNow = DateTime.UtcNow;
            var pacificNowMinus30 = (TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone).Date.AddDays(-30));

            var allCompletedInvoices = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Include(p => p.PickLocation).Where(p => p.Status.Status == "Complete" && p.FinishDate.Value.Date > pacificNowMinus30);
            foreach (var inv in allCompletedInvoices)
            {
                //get total time taken column for table
                string startTickValue = inv.StartDate.Ticks.ToString();
                inv.StartDateInTicks = startTickValue;

                string assignedTickValue = inv.AssignedDate.Value.Ticks.ToString();
                inv.AssignedDateInTicks = assignedTickValue;

                string endTickValue = inv.FinishDate.Value.Ticks.ToString();
                inv.FinishDateInTicks = endTickValue;

                //remove fractional seconds for display
                var assignedSansMilli = new DateTime(inv.AssignedDate.Value.Year, inv.AssignedDate.Value.Month, inv.AssignedDate.Value.Day, inv.AssignedDate.Value.Hour, inv.AssignedDate.Value.Minute, inv.AssignedDate.Value.Second);
                var finishSansMilli = new DateTime(inv.FinishDate.Value.Year, inv.FinishDate.Value.Month, inv.FinishDate.Value.Day, inv.FinishDate.Value.Hour, inv.FinishDate.Value.Minute, inv.FinishDate.Value.Second);
                //get time difference between when ticket assigned and closed
                var timeSpan = (finishSansMilli - assignedSansMilli);
                //double roundedSeconds = Math.Ceiling(timeSpan.Seconds + timeSpan.Milliseconds * 0.001);
                double hoursSegment = Math.Floor(timeSpan.TotalHours);
                string totalPullTime = String.Format("{0}h {1}m {2}s", hoursSegment, timeSpan.Minutes, timeSpan.Seconds);
                inv.TotalPullTime = totalPullTime;
                inv.TotalPullTimeInTicks = timeSpan.Ticks.ToString();
            }
            return allCompletedInvoices;
        }

        public IQueryable<Invoice> GetCompletedInvoicesForToday()
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var utcNow = DateTime.UtcNow;
            var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone).Date;
            var completedInvoicesForToday = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Where(p => p.FinishDate.Value.Date == pacificNow && p.Status.Status == "Complete");
            //var today = DateTime.Today;
            //var q = db.Games.Where(t => DbFunctions.TruncateTime(t.StartDate) >= today);

            return completedInvoicesForToday.AsQueryable();
        }
        
        public IActionResult AddInvoice(Invoice newInvoice)
        {
            using (var transaction = _dbConnection.Database.BeginTransaction())
            {
                try
                {
                    _dbConnection.Invoice.Add(newInvoice);
                    _dbConnection.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new BadRequestResult();
                }
            }

            return new OkResult();
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

        public IActionResult CloseInvoice(int invoiceId)
        {
            //_dbConnection.Invoice.Remove(_dbConnection.Invoice.Find(invoiceId));
            //_dbConnection.SaveChanges();

                using (var transaction = _dbConnection.Database.BeginTransaction())
                {
                    try
                    {
                        var completedId = _dbConnection.InvoiceStatus.Single(p => p.Status == "Complete").StatusId;
                        var invoice = _dbConnection.Invoice.Find(invoiceId);
                        var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                        var utcNow = DateTime.UtcNow;
                        var pacificNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone);
                        invoice.FinishDate = pacificNow;
                        invoice.StatusId = completedId;
                        _dbConnection.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new BadRequestResult();
                    }
                }

            return new OkResult();
        }

        public IActionResult CancelInvoice(int invoiceId)
        {
            using (var transaction = _dbConnection.Database.BeginTransaction())
            {
                try
                {
                    _dbConnection.Invoice.Remove(_dbConnection.Invoice.Find(invoiceId));
                    _dbConnection.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new BadRequestResult();
                }
            }

            return new OkResult();

        }
    }
}
