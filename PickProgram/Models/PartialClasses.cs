using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    [ModelMetadataType(typeof(InvoiceModelMetadata))]
    public partial class Invoice
    {
        [NotMapped]
        public string StartDateInTicks { get; set; }
        [NotMapped]
        public string AssignedDateInTicks { get; set; }
        [NotMapped]
        public string FinishDateInTicks { get; set; }
        [NotMapped]
        public string TotalPullTime { get; set; }
        [NotMapped]
        public string TotalPullTimeInTicks { get; set; }
    }
}
