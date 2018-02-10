using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PickProgram.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        [Required]
        [DisplayName("Invoice Number")]
        public string InvoiceNumber { get; set; }
        [Required]
        [DisplayName("Number of Parts")]
        public int NumberOfParts { get; set; }
        public int? PickingLocationId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public Employee AssignedEmployee { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
