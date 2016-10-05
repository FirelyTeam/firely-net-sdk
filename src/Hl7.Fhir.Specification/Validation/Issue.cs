/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.ElementModel;

namespace Hl7.Fhir.Validation
{
    public class Issue
    {
        public int Code;
        public OperationOutcome.IssueSeverity Severity;
        public OperationOutcome.IssueType Type;

        public CodeableConcept ToCodeableConcept(string text = null)
        {
            return ToCodeableConcept(Code, text);
        }

        public static CodeableConcept ToCodeableConcept(int issueCode, string text = null)
        {
            return new CodeableConcept("http://hl7.org/fhir/validation-operation-outcome", issueCode.ToString(), text);
        }

        public OperationOutcome.IssueComponent ToIssueComponent(string message, INamedNode location = null)
        {
            return ToIssueComponent(message, location != null ? location.Path : null);
        }

        public OperationOutcome.IssueComponent ToIssueComponent(string message, string path = null)
        {
            // https://www.hl7.org/fhir/operationoutcome-definitions.html#OperationOutcome.issue.details
            // Comments: "A human readable description of the error issue SHOULD be placed in details.text."

            // var ic = new OperationOutcome.IssueComponent() { Severity = this.Severity, Code = this.Type, Diagnostics = message };
            var ic = new OperationOutcome.IssueComponent() { Severity = this.Severity, Code = this.Type };
            ic.Details = ToCodeableConcept(message);

            if (path != null) ic.Location = new List<string> { path };

            return ic;
        }

        /// <summary>Factory method.</summary>
        internal static Issue Create(int code, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType type)
        {
            return new Issue() { Code = code, Severity = severity, Type = type };
        }

        // Content errors
        public static readonly Issue CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN = Create(1000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN = Create(1001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
//        public static readonly Issue CONTENT_ELEMENT_HAS_UNKNOWN_TYPE = def(1002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_INCORRECT_TYPE = Create(1003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_MUST_MATCH_TYPE = Create(1004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_VALUE_TOO_LONG = Create(1005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE = Create(1006, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_INCORRECT_OCCURRENCE = Create(1007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_FIXED_VALUE = Create(1008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_PATTERN_VALUE = Create(1009, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_CHOICE_WITH_NO_ACTUAL_TYPE = Create(1010, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_CHOICE_INVALID_INSTANCE_TYPE = Create(1011, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_FAILS_ERROR_CONSTRAINT = Create(1012, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_FAILS_WARNING_CONSTRAINT = Create(1013, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid);

        // Profile problems
        public static readonly Issue PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE = Create(2000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAX_USES_UNORDERED_TYPE = Create(2001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE = Create(2002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE = Create(2003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF = Create(2004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
//        public static readonly Issue PROFILE_ELEMENTDEF_NO_PRIMITIVE_REGEX = def(2005, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE = Create(2006, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CARDINALITY_MISSING = Create(2007, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_IS_EMPTY = Create(2008, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_FLUENTPATH_EXPRESSION = Create(2009, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);


        // Unsupported 
        public static readonly Issue UNSUPPORTED_SLICING_NOT_SUPPORTED = Create(3000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue UNSUPPORTED_NESTED_BUNDLES = Create(3001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue UNSUPPORTED_FOLLOWING_REFERENCES = Create(3002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);
        public static readonly Issue UNSUPPORTED_CONSTRAINT_WITHOUT_FLUENTPATH = Create(3003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);

        // Non-availability, incomplete data
        public static readonly Issue UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE = Create(4000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        //public static readonly Issue UNAVAILABLE_ELEMENTDEF_WITHOUT_STRUCTDEF = def(4001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_NEED_SNAPSHOT = Create(4002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_SNAPSHOT_GENERATION_FAILED = Create(4003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        // [WMR 20161003] Added for snapshot generator
        public static readonly Issue UNAVAILABLE_NEED_DIFFERENTIAL = Create(4004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);

        // Processing information
        public static readonly Issue PROCESSING_PROGRESS = Create(5000, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
        public static readonly Issue PROCESSING_CONSTRAINT_VALIDATION_INACTIVE = Create(5001, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
    }

}