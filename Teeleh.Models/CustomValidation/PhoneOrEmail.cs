using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PhoneOrEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Phone = (string)validationContext.ObjectType.GetProperty("PhoneNumber")
                .GetValue(validationContext.ObjectInstance, null);
            string Email = (string)validationContext.ObjectType.GetProperty("Email")
                .GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Email))
                return new ValidationResult("At least one is required!!");
            return ValidationResult.Success;
        }
    }
}
