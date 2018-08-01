/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hl7.Fhir.Validation
{
#if NET_XSD_SCHEMA

    /// <summary>
    /// Add support for validating against Base subclasses (instead of IElementNavigator) to the Validator
    /// </summary>
    public static class XmlValidationExtensions
    {
        public static OperationOutcome Validate(this Validator me, XmlReader instance)
        {
            Resource poco;
            var result = me.ValidatedParseXml(instance, out poco);

            if (poco != null)
                result.Add(me.Validate(poco));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, params string[] definitionUris)
        {
            Resource poco;
            var result = me.ValidatedParseXml(instance, out poco);

            if (poco != null)
                result.Add(me.Validate(poco, definitionUris));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, StructureDefinition structureDefinition)
        {
            Resource poco;
            var result = me.ValidatedParseXml(instance, out poco);

            if (poco != null)
                result.Add(me.Validate(poco, structureDefinition));

            return result;
        }

        public static OperationOutcome Validate(this Validator me, XmlReader instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            Resource poco;
            var result = me.ValidatedParseXml(instance, out poco);

            if (poco != null)
                result.Add(me.Validate(poco, structureDefinitions));

            return result;
        }


        internal static OperationOutcome ValidatedParseXml(this Validator me, XmlReader instance, out Resource poco)
        {
            var result = new OperationOutcome();

            try
            {

                if (me.Settings.EnableXsdValidation)
                {
                    var doc = XDocument.Load(instance, LoadOptions.SetLineInfo);
                    result.Add(me.ValidateXml(doc));
                    instance = doc.CreateReader();
                }

                poco = (Resource)(new FhirXmlParser()).Parse(instance, typeof(Resource));
            }
            catch (Exception e)
            {
                result.AddIssue($"Parsing of Xml into a FHIR Poco failed: {e.Message}", Issue.XSD_CONTENT_POCO_PARSING_FAILED, (string)null);
                poco = null;
            }

            return result;
        }



        internal static OperationOutcome ValidateXml(this Validator me, XDocument instance)
        {
            var result = new OperationOutcome();

            ValidationEventHandler veh = (o, args) => result.AddIssue(ToIssueComponent(args));
            instance.Validate(SchemaCollection.ValidationSchemaSet, veh);

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

#endif
}
