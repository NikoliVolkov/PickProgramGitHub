﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> GetInvoices();
        IEnumerable<Invoice> GetPendingInvoices();
        IEnumerable<Invoice> GetAllCompletedInvoicesLast30();
        IEnumerable<Invoice> GetAllCompletedInvoicesForRange(DateTime startDate, DateTime endDate);
        IQueryable<Invoice> GetCompletedInvoicesForToday();
        Invoice GetInvoiceById(int invoiceId);
        IActionResult AddInvoice(Invoice newInvoice);
        string AssignEmployee(int invoiceId, int employeeId);
        IActionResult CloseInvoice(int invoiceId);
        IActionResult CancelInvoice(int invoiceId);
    }
}
