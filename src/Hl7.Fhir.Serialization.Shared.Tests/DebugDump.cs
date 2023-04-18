/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;

namespace Hl7.Fhir.Serialization.Tests
{
    public static class DebugDump
    {
        /// <summary>
        /// Dump the provided FHIR Resource fragment to the console in XML (pretty printed)
        /// </summary>
        /// <param name="fragment"></param>
        public static void OutputXml(Base fragment)
        {
            if (fragment == null)
                Console.WriteLine("(null)");
            else
            {
                var doc = System.Xml.Linq.XDocument.Parse(new FhirXmlSerializer().SerializeToString(fragment));
                Console.WriteLine(doc.ToString(System.Xml.Linq.SaveOptions.None));
            }
        }

        /// <summary>
        /// Dump the provided FHIR Resource fragment to the console in Json (pretty printed)
        /// </summary>
        /// <param name="fragment"></param>
        public static void OutputJson(Base fragment)
        {
            if (fragment == null)
                Console.WriteLine("(null)");
            else
            {
                Console.WriteLine(new FhirJsonSerializer(new SerializerSettings() { Pretty = true }).SerializeToString(fragment));
            }
        }

        /// <summary>
        /// Convert to an OperationOutcome
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static OperationOutcome ToOperationOutcome(this DeserializationFailedException ex)
        {
            // Need to convert the list of general exceptions into an OperationOutcome.
            OperationOutcome oc = new OperationOutcome();
            foreach (var e in ex.Exceptions)
            {
                var issue =
                new OperationOutcome.IssueComponent()
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = OperationOutcome.IssueType.Invalid
                };
                if (e is CodedWithLocationException ecl)
                {
                    issue = ecl.ToIssue();
                }
                oc.Issue.Add(issue);
            }

            return oc;
        }

        /// <summary>
        /// CodeSystem to be used in generating error messages in the OperationOutcome
        /// </summary>
        public static string ValidationErrorMessageCodeSystem = "http://firely.com/CodeSystem/ErrorMessages";

        /// <summary>
        /// Convert to an OperationOutcome.Issue
        /// </summary>
        /// <returns></returns>
        public static Model.OperationOutcome.IssueComponent ToIssue(this CodedWithLocationException me)
        {
            string shortDisplay = null;

            var result = new Model.OperationOutcome.IssueComponent()
            {
                Severity = me.IssueSeverity,
                Code = me.IssueType,
                Details = new Model.CodeableConcept(ValidationErrorMessageCodeSystem, me.ErrorCode, shortDisplay, me.FormattedMessage)
            };

            if (me.LineNumber.HasValue && me.Position.HasValue)
                result.Location = new[] { $"line {me.LineNumber}, position {me.Position}" };
            if (!string.IsNullOrEmpty(me.Location))
                result.Expression = new[] { me.Location };

            return result;
        }
    }
}
