using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PickProgram.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.ValidationAttributes
{
    public class AssignedEmployeeValidator : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Invoice invoice = (Invoice)validationContext.ObjectInstance;

            if (invoice.PickLocation.LocationDescription == "Offsite" && invoice.AssignedEmployee.FirstName != "Offsite")
            {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }
        private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Use generic message: i.e. The field {0} is invalid
            //return this.FormatErrorMessage(validationContext.DisplayName);

            // Custom message
            return $"{validationContext.DisplayName} must be set to Offsite when Offsite location is selected.";
        }
    }

}
