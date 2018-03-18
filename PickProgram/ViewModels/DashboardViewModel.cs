using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace PickProgram.ViewModels
{
    public class DashboardViewModel
    {

        public Invoice NewInvoice { get; set; }
        //had to separate the following fields because I couldn't change default validation error message on int
        [Required(ErrorMessage = "Enter the number of parts to be pulled on the invoice.")]
        public int? NumberOfPartsInVM { get; set; }
        [Required(ErrorMessage = "Select the pick location for the parts on the invoice.")]
        public int? PickLocationIdInVM { get; set; }
        public List<PickLocation> PickLocations { get; set; }
        public List<SelectListItem> Employees { get; set; }
        public List<Invoice> ListOfInvoicesOnsite { get; set; }
        public List<Invoice> ListOfInvoicesOffsite { get; set; }

        public DashboardViewModel()
        {
            ListOfInvoicesOffsite = new List<Invoice>();
            ListOfInvoicesOnsite = new List<Invoice>();
        }
    }

}
