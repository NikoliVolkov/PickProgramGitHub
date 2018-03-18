using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PickProgram.Models
{
    public class InvoiceModelMetadata
    {
        [Required(ErrorMessage = "You must input an invoice number.")]
        [DisplayName("Invoice Number")]
        public string InvoiceNumber { get; set; }
        /*[Range(1, 100, ErrorMessage = "UPRN must be numeric")]
        public int? NumberOfParts { get; set; }*/
    }
}
