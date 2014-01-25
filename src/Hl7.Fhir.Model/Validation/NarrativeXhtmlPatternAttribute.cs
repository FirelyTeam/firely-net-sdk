using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NarrativeXhtmlPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(string))
                throw new ArgumentException("CodePatternAttribute can only be applied to string properties");

            try
            {
                XElement.Parse(value as string);
                return ValidationResult.Success;
            }
            catch(Exception e)
            {
                return new ValidationResult("Xml could not be parsed: " + e.Message);
            }

            //TODO: Extra constraints FHIR puts in Narrative xhtml contents
        }
    }
}
