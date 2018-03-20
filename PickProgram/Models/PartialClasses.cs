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
    }
}
