﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickProgram.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PickProgram.ViewModels
{
    public class StatsViewModel
    {
        public int TotalInvoices { get; set; }
        public int TotalParts { get; set; }
        public List<EmployeeStat> EmployeeStats{ get; set; }
    }

}
