/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class InstantPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(DateTimeOffset))
                throw new ArgumentException("IdPatternAttribute can only be applied to DateTimeOffset properties");

            if (Instant.IsValidValue(value as string))
                return ValidationResult.Success;
            else
                return DotNetAttributeValidation.BuildResult(validationContext, "{0} is not a correctly formatted Instant", value as string);
        }
    }
}
