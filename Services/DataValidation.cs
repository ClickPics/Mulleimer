using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenithWebsite.Models;

namespace ZenithWebsite.Validation
{
    /// <summary>
    /// Ensure that the EndDateTime is after the StartDateTime
    /// </summary>
    public class DateValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Event)validationContext.ObjectInstance;
            if (model.EndDateTime < model.StartDateTime)
                return new ValidationResult("End Date Time cannot be before Start Date Time");
            return null;

        }
    }
}