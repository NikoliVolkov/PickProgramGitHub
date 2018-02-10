using System;
using System.Collections.Generic;

namespace PickProgram.Models
{
    public partial class PickLocation
    {
        public PickLocation()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int LocationId { get; set; }
        public string LocationDescription { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
