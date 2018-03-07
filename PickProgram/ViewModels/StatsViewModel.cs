using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PickProgram.ViewModels
{
    public class StatsViewModel
    {
        public int InvoiceId { get; set; }
        public List<SelectListItem> Employees { get; set; }
    }

}
