using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using PickProgram.ValidationAttributes;

namespace PickProgram.ViewModels
{
    public class ReportsViewModel
    {
        [Required(ErrorMessage = "Please enter a start date for the report.")]
        [DateValidation("EndDate", ErrorMessage = "Please select a valid start date that occurs before the selected end date.")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "Please enter an end date for the report.")]
        public string EndDate { get; set; }
        public IEnumerable<Invoice> SummaryListOfInvoices { get; set; }
    }

}
