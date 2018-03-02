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

            var emp = _dbConnection.Employee.Find(employeeId);
            return JsonConvert.SerializeObject(new { numOfParts = 999, assignedEmployee = emp.FirstName + " " + emp.LastName, assignedOn = invoice.AssignedDate.Value.Ticks });

        }

        public void CancelInvoice(int invoiceId)
        {
            _dbConnection.Invoice.Remove(_dbConnection.Invoice.Find(invoiceId));
            _dbConnection.SaveChanges();            
            
        }
    }
}
