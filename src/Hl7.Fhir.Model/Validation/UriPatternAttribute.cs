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

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UriPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(Uri))
                throw new ArgumentException("UriPatternAttribute can only be applied to .NET Uri properties");

            var uri = (Uri)value;

            if (uri.IsAbsoluteUri)
            {
                var uris = uri.ToString();
                
                if (uris.StartsWith("urn:oid:") && !OidPatternAttribute.IsValid(uris))
                    return FhirValidator.BuildResult(validationContext, "Uri uses an urn:oid scheme, but the oid {0} is incorrect", uris);
                else if (uris.StartsWith("urn:uuid:") && !UuidPatternAttribute.IsValid(uris))
                    return FhirValidator.BuildResult(validationContext, "Uri uses an urn:uuid schema, but the uuid {0} is incorrect", uris);
            }

            return ValidationResult.Success;
        }
    }
}
