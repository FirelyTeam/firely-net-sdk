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
        public static bool IsValidValue(string value)
        {
            try
            {
                // There is currently no validation in the portable .net
                // for the XDocument validation, would need to scan for
                // another implementation to cover this
#if !PORTABLE45
                var doc = XDocument.Parse(value as string);
                doc.Validate(_xhtmlSchemaSet.Value, validationEventHandler: null);
#endif

                return true;
            }
            catch
            {
                return false;
            }
        }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(string))
                throw new ArgumentException("CodePatternAttribute can only be applied to string properties");

            if(IsValidValue(value as string))
                return ValidationResult.Success;
            else 
                return FhirValidator.BuildResult(validationContext, "Xml can not be parsed or is not valid according to the (limited) FHIR scheme");
        }

#if !PORTABLE45
        private static Lazy<XmlSchemaSet> _xhtmlSchemaSet = new Lazy<XmlSchemaSet>(compileXhtmlSchema, true);

        private static XmlSchemaSet compileXhtmlSchema()
        {
            var assembly = typeof(NarrativeXhtmlPatternAttribute).Assembly;
            XmlSchemaSet schemas = new XmlSchemaSet();

            Stream schema = assembly.GetManifestResourceStream("Hl7.Fhir.Validation.xhtml.fhir-xhtml.xsd"); 
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
            schema = assembly.GetManifestResourceStream("Hl7.Fhir.Validation.xhtml.xml.xsd"); 
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file

            schemas.Compile();

            return schemas;
        }
#endif
	}
}
