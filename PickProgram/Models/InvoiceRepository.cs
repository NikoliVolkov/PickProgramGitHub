﻿using System;
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
            _dbConnection.SaveChanges();
            var emp = _dbConnection.Employee.Find(employeeId);
            return emp.FirstName + "&nbsp;" + emp.LastName;


        }
    }
}
