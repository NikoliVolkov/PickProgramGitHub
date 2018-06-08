using System;
using System.Collections.Generic;

namespace PickProgram.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public DateTime? DeactivateDate { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
