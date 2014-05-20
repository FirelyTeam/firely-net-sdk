/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

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
				// There is currently no validation in the portable .net
				// for the XDocument validation, would need to scan for
				// another implementation to cover this
#if !PORTABLE45
                var doc = XDocument.Parse(value as string);
                doc.Validate(SchemaCollection.ValidationSchemaSet, validationEventHandler: null);
#endif

				return ValidationResult.Success;
            }
            catch(Exception e)
            {
                return FhirValidator.BuildResult(validationContext, "Xml can not be parsed or is not valid: " + e.Message);
            }
        }
	}
}
