/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

#pragma warning disable 1591 // suppress XML summary warnings

namespace Hl7.Fhir.Support
{
    public class Issue
    {
        public const string API_OPERATION_OUTCOME_SYSTEM = "http://hl7.org/fhir/dotnet-api-operation-outcome";

        public int Code;
        public OperationOutcome.IssueSeverity Severity;
        public OperationOutcome.IssueType Type;

        public CodeableConcept ToCodeableConcept(string text = null) => ToCodeableConcept(Code, text);

        public static CodeableConcept ToCodeableConcept(int issueCode, string text = null) =>
            new CodeableConcept(API_OPERATION_OUTCOME_SYSTEM, issueCode.ToString(), text);

        public OperationOutcome.IssueComponent ToIssueComponent(string message, ITypedElement location = null) =>
            ToIssueComponent(message, location?.Location);

        public OperationOutcome.IssueComponent ToIssueComponent(string message, string path = null)
        {
            // https://www.hl7.org/fhir/operationoutcome-definitions.html#OperationOutcome.issue.details
            // Comments: "A human readable description of the error issue SHOULD be placed in details.text."

            // var ic = new OperationOutcome.IssueComponent() { Severity = this.Severity, Code = this.Type, Diagnostics = message };
            var ic = new OperationOutcome.IssueComponent() { Severity = this.Severity, Code = this.Type };

            // Put numeric code + readable message into a CodeableConcept
            ic.Details = ToCodeableConcept(message);

            if (path is not null)
            {
                ic.Expression = new List<string> { path };
                // IssueComponent.Location is deprecated but we still set this because of backwards compatibility.
                ic.Location = new List<string> { path };
            }

            return ic;
        }

        internal Issue(int code, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType type)
        {
            Code = code;
            Severity = severity;
            Type = type;
        }
        /// <summary>Factory method.</summary>
        public static Issue Create(int code, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType type) =>
            new Issue(code, severity, type);

        // Validation resource instance errors
        public static readonly Issue CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN = Create(1000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN = Create(1001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_INCORRECT_TYPE = Create(1003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_MUST_MATCH_TYPE = Create(1004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_VALUE_TOO_LONG = Create(1005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE = Create(1006, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_FIXED_VALUE = Create(1008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_PATTERN_VALUE = Create(1009, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_CANNOT_DETERMINE_TYPE = Create(1010, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_CHOICE_INVALID_INSTANCE_TYPE = Create(1011, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_FAILS_ERROR_CONSTRAINT = Create(1012, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invariant);
        public static readonly Issue CONTENT_ELEMENT_FAILS_WARNING_CONSTRAINT = Create(1013, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invariant);
        public static readonly Issue CONTENT_REFERENCE_OF_INVALID_KIND = Create(1015, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_CONTAINED_REFERENCE_NOT_RESOLVABLE = Create(1016, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_UNPARSEABLE_REFERENCE = Create(1017, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_REFERENCE_NOT_RESOLVABLE = Create(1018, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_SMALL = Create(1019, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_LARGE = Create(1020, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_PRIMITIVE_VALUE_NOT_COMPARABLE = Create(1021, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_MISMATCHING_PROFILES = Create(1022, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_INVALID_FOR_REQUIRED_BINDING = Create(1023, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_INVALID_FOR_NON_REQUIRED_BINDING = Create(1024, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_TYPE_NOT_BINDEABLE = Create(1025, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_FAILS_SLICING_RULE = Create(1026, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_SLICING_OUT_OF_ORDER = Create(1027, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_INCORRECT_OCCURRENCE = Create(1028, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_NAME_DOESNT_MATCH_DEFINITION = Create(1029, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_REFERENCE_CYCLE_DETECTED = Create(1030, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Informational);

        public static readonly Issue XSD_VALIDATION_ERROR = Create(1100, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue XSD_VALIDATION_WARNING = Create(1101, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid);
        public static readonly Issue XSD_CONTENT_POCO_PARSING_FAILED = Create(1102, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // Profile problems
        public static readonly Issue PROFILE_ELEMENTDEF_MIN_MAX_USES_UNORDERED_TYPE = Create(2000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAX_USES_UNORDERED_TYPE = Create(2001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE = Create(2002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE = Create(2003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF = Create(2004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        //        public static readonly Issue PROFILE_ELEMENTDEF_NO_PRIMITIVE_REGEX = def(2005, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE = Create(2006, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CARDINALITY_MISSING = Create(2007, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_IS_EMPTY = Create(2008, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_FHIRPATH_EXPRESSION = Create(2009, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_NO_PROFILE_TO_VALIDATE_AGAINST = Create(2010, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue PROFILE_ELEMENTDEF_INCORRECT = Create(2012, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        [Obsolete("This issue will not be raised by the validator anymore. Use 'PROFILE_ELEMENTDEF_INCORRECT' instead.")] // Obsolete on 20190409 by Marco
        public static readonly Issue PROFILE_INCOMPLETE_BINDING = Create(2011, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue PROFILE_INSTANCE_MATCHES_MULTIPLE_SLICES = Create(2013, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Structure);

        // Unsupported 
        public static readonly Issue UNSUPPORTED_SLICING_NOT_SUPPORTED = Create(3000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue UNSUPPORTED_CONSTRAINT_WITHOUT_FHIRPATH = Create(3003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNSUPPORTED_MIN_MAX_QUANTITY = Create(3004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue UNSUPPORTED_BINDING_NOT_SUPPORTED_BY_SERVICE = Create(3006, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);

        // Non-availability, incomplete data
        public static readonly Issue UNAVAILABLE_REFERENCED_PROFILE = Create(4000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_NEED_SNAPSHOT = Create(4002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_SNAPSHOT_GENERATION_FAILED = Create(4003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_NEED_DIFFERENTIAL = Create(4004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_REFERENCED_RESOURCE = Create(4005, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_TERMINOLOGY_SERVER = Create(4007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_VALIDATE_CODE_FAILED = Create(4008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_REFERENCED_PROFILE_WARNING = Create(4009, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);

        // Processing information
        public static readonly Issue PROCESSING_PROGRESS = Create(5000, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
        // public static readonly Issue PROCESSING_CONSTRAINT_VALIDATION_INACTIVEX = Create(5001, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
        public static readonly Issue PROCESSING_START_NESTED_VALIDATION = Create(5002, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
        public static readonly Issue PROCESSING_CATASTROPHIC_FAILURE = Create(5003, OperationOutcome.IssueSeverity.Fatal, OperationOutcome.IssueType.Exception);
        public static readonly Issue PROCESSING_REPEATED_ERROR = Create(5004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Informational);

        // Terminology specific errors    
        public static readonly Issue TERMINOLOGY_CODE_NOT_IN_VALUESET = Create(6001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.CodeInvalid);
        public static readonly Issue TERMINOLOGY_ABSTRACT_CODE_NOT_ALLOWED = Create(6002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.CodeInvalid);
        public static readonly Issue TERMINOLOGY_INCORRECT_DISPLAY = Create(6003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.CodeInvalid);
        public static readonly Issue TERMINOLOGY_SERVICE_FAILED = Create(6004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue TERMINOLOGY_NO_CODE_IN_INSTANCE = Create(6005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.CodeInvalid);

        // Since the terminology service can't return Operation Outcomes, but only true of false. Terminology issues are split up into two categories: warnings of errors.
        // Error means the code is invalid, warning contains just an informational message as outcome.
        public static readonly Issue TERMINOLOGY_OUTPUT_WARNING = Create(6006, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Informational);
        public static readonly Issue TERMINOLOGY_OUTPUT_ERROR = Create(6007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.CodeInvalid);

    }


    public static class OperationOutcomeIssueExtensions
    {
        public static OperationOutcome NewOutcomeWithIssue(this Issue infoIssue, string message, ITypedElement location)
        {
            var outcome = new OperationOutcome();
            var issue = infoIssue.ToIssueComponent(message, location);
            outcome.AddIssue(issue);
            return outcome;
        }

        public static OperationOutcome NewOutcomeWithIssue(this Issue infoIssue, string message, string location = null)
        {
            var outcome = new OperationOutcome();
            var issue = infoIssue.ToIssueComponent(message, location);
            outcome.AddIssue(issue);
            return outcome;
        }

        public static OperationOutcome.IssueComponent AddIssue(this OperationOutcome outcome, string message, Issue infoIssue, ITypedElement location)
        {
            var issue = infoIssue.ToIssueComponent(message, location);
            outcome.AddIssue(issue);
            return issue;
        }

        public static OperationOutcome.IssueComponent AddIssue(this OperationOutcome outcome, string message, Issue infoIssue, string location = null)
        {
            var issue = infoIssue.ToIssueComponent(message, location);
            outcome.AddIssue(issue);
            return issue;
        }
    }
}