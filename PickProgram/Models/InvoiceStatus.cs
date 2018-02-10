using System;
using System.Collections.Generic;

namespace PickProgram.Models
{
    public partial class InvoiceStatus
    {
        public InvoiceStatus()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
