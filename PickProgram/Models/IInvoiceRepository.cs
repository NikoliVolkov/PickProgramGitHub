using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> GetInvoices();
        Invoice GetInvoiceById(int invoiceId);
        void AddInvoice(Invoice newInvoice);
        string AssignEmployee(int invoiceId, int employeeId);
    }
}
