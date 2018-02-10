using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var invoiceData = _dbConnection.Invoice.Include(p => p.Status).Include(p => p.AssignedEmployee).Where(p => p.Status.Status == "Pending");
            return invoiceData;
        }
        
        public void AddInvoice(Invoice newInvoice)
        {
            _dbConnection.Invoice.Add(newInvoice);
            _dbConnection.SaveChanges();
        }
    }
}
