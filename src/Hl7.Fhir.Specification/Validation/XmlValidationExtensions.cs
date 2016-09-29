/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Add support for validating against Base subclasses (instead of IElementNavigator) to the Validator
    /// </summary>
    public static class XmlValidationExtensions
    {
        public static OperationOutcome Validate(this Validator me, XmlReader instance)
        {
            var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
            var result = me.ValidateXml(doc);
            
            var poco = (new FhirXmlParser()).Parse(doc.ToString(), typeof(Resource));
            result.Add(me.Validate(poco));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, params string[] definitionUris)
        {
            var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
            var result = me.ValidateXml(doc);

            var poco = (new FhirXmlParser()).Parse(doc.ToString(), typeof(Resource));
            result.Add(me.Validate(poco, definitionUris));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, StructureDefinition structureDefinition)
        {
            var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
            var result = me.ValidateXml(doc);

            var poco = (new FhirXmlParser()).Parse(doc.ToString(), typeof(Resource));
            result.Add(me.Validate(poco, structureDefinition));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
            var result = me.ValidateXml(doc);

            var poco = (new FhirXmlParser()).Parse(doc.ToString(), typeof(Resource));
            result.Add(me.Validate(poco, structureDefinitions));

            return result;
        }

        internal static OperationOutcome ValidateXml(this Validator me, XDocument instance)
        {
            var result = new OperationOutcome();

            if (me.Settings.EnableXsdValidation)
            {
                ValidationEventHandler veh = (o, args) => result.AddIssue(ToIssueComponent(args));
                instance.Validate(SchemaCollection.ValidationSchemaSet, veh);
            }

            return result;
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
