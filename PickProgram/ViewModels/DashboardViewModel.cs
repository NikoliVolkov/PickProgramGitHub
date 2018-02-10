using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;

namespace PickProgram.ViewModels
{
    public class DashboardViewModel
    {
        public string SuccessMessage { get; set; }
        public Invoice NewInvoice { get; set; }
        public List<Invoice> ListOfInvoices { get; set; }

        public DashboardViewModel()
        {
            SuccessMessage = string.Empty;
        }
    }

}
