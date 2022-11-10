/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Add support for validating data straight from an <see cref="XmlReader"/>.
    /// </summary>
    public static class XmlValidationExtensions
    {
        private static XmlSchemaSet getSchemaSetFromSettings(Validator v) => v.Settings.XsdSchemaCollection?.MinimalSchemas ??
                            SchemaCollection.ValidationSchemaSet;

        public static OperationOutcome Validate(this Validator me, XmlReader instance)
        {
            var result = me.ValidatedParseXml(instance, getSchemaSetFromSettings(me), out var poco);

            if (poco != null)
                result.Add(me.Validate(poco));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, params string[] definitionUris)
        {
            var result = me.ValidatedParseXml(instance, getSchemaSetFromSettings(me), out var poco);

            if (poco != null)
                result.Add(me.Validate(poco, definitionUris));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, StructureDefinition structureDefinition)
        {
            var result = me.ValidatedParseXml(instance, getSchemaSetFromSettings(me), out var poco);

            if (poco != null)
                result.Add(me.Validate(poco, structureDefinition));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            var result = me.ValidatedParseXml(instance, getSchemaSetFromSettings(me), out var poco);

            if (poco != null)
                result.Add(me.Validate(poco, structureDefinitions));

            return result;
        }


        internal static OperationOutcome ValidatedParseXml(this Validator me, XmlReader instance, XmlSchemaSet xsdSchemas, out Resource? poco)
        {
            var result = new OperationOutcome();

            try
            {

                if (me.Settings.EnableXsdValidation)
                {
                    var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
                    result.Add(validateXml(doc, xsdSchemas));
                    instance = doc.CreateReader();
                }

                poco = (Resource)(new FhirXmlParser()).Parse(instance, typeof(Resource));
            }
            catch (Exception e)
            {
                result.AddIssue($"Parsing of Xml into a FHIR Poco failed: {e.Message}", Issue.XSD_CONTENT_POCO_PARSING_FAILED, default(string));
                poco = null;
            }

            return result;

            static OperationOutcome validateXml(XDocument instance, XmlSchemaSet xsdSchemas)
            {
                var result = new OperationOutcome();

                instance.Validate(xsdSchemas, (o, args) => { result.AddIssue(ToIssueComponent(args)); });

                return result;
            }

        }


        private static OperationOutcome.IssueComponent ToIssueComponent(ValidationEventArgs args)
        {
            string message = $".NET Xsd validation {args.Severity}: {args.Message}";
            string pos = $"line: { args.Exception.LineNumber}, pos: { args.Exception.LinePosition}";

            if (args.Severity == XmlSeverityType.Error)
                return Issue.XSD_VALIDATION_ERROR.ToIssueComponent(message, pos);
            else
                return Issue.XSD_VALIDATION_WARNING.ToIssueComponent(message, pos);
        }
    }
}

#nullable restore