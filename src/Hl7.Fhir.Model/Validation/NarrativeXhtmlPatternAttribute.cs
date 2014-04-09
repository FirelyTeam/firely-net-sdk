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
        private static Lazy<XmlSchemaSet> schemaSet = new Lazy<XmlSchemaSet>(compileXhtmlSchema, true);

        private static XmlSchemaSet compileXhtmlSchema()
        {
            Stream s = typeof(NarrativeXhtmlPatternAttribute).Assembly.GetManifestResourceStream("Hl7.Fhir.Model.fhir-xhtml1-strict.xsd");
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("http://www.w3.org/1999/xhtml", XmlReader.Create(s));

            return schemas;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null) return ValidationResult.Success;

            if (value.GetType() != typeof(string))
                throw new ArgumentException("CodePatternAttribute can only be applied to string properties");

            try
            {
                var doc = XDocument.Parse(value as string);
                doc.Validate(schemaSet.Value, null, false);

                return ValidationResult.Success;
            }
            catch(Exception e)
            {
                return FhirValidator.BuildResult(validationContext, "Xml can not be parsed or is not valid: " + e.Message);
            }
        }
    }
}
