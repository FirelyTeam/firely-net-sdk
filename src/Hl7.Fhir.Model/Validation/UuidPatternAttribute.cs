using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UuidPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(string))
                throw new ArgumentException("UuidPatternAttribute can only be applied to string properties");

            if (UuidPatternAttribute.IsValid((string)value))
                return ValidationResult.Success;
            else
                return FhirValidator.BuildResult(validationContext, "{0} is not a correctly formatted Uuid", (string)value);
        }

        public static bool IsValid(string value)
        {
            return Regex.IsMatch(value, "^" + Uuid.PATTERN + "$", RegexOptions.Singleline);
        }
    }
}
