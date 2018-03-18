using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using PickProgram.ValidationAttributes;

namespace PickProgram.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int NumberOfParts { get; set; }
        public int PickLocationId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? AssignedDate { get; set; }

        public Employee AssignedEmployee { get; set; }
        public PickLocation PickLocation { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
