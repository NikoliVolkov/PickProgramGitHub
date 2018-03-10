using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public partial class EmployeeStat
    {
        public string Nickname { get; set; }
        public int TotalInvoices { get; set; }
        public int TotalParts { get; set; }
    }
}
