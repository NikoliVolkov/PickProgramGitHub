using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PickProgram.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.ValidationAttributes
{
    public class DateValidationAttribute : ValidationAttribute
    {
        private readonly string dateToCompare;

        public DateValidationAttribute(string dateToCompare)
        {
            this.dateToCompare = dateToCompare;
        }
        public override bool IsValid(object value)
        {
            if(value != null && dateToCompare != null) { 
            if (DateTime.Parse(value.ToString()) > Convert.ToDateTime(dateToCompare))
            {
                return false;
            }
            }

            return true;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var property = validationContext.ObjectType.GetProperty(dateToCompare);
            if (value != null && property.GetValue(validationContext.ObjectInstance) != null)
            {              

            var comparisonValue = property.GetValue(validationContext.ObjectInstance).ToString();
                

                if (DateTime.Parse(value.ToString()) > DateTime.Parse(comparisonValue))
                {
                        return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
        /*private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Use generic message: i.e. The field {0} is invalid
            //return this.FormatErrorMessage(validationContext.DisplayName);

            // Custom message
            return $"{validationContext.DisplayName} must be set to Offsite when Offsite location is selected.";
        }*/

    }

}
