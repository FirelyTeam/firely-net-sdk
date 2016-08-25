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

        public CodeableConcept ToCodeableConcept()
        {
            return ToCodeableConcept(Code);
        }

        public static CodeableConcept ToCodeableConcept(int issueCode)
        {
            return new CodeableConcept("http://hl7.org/fhir/validation-operation-outcome", issueCode.ToString());
        }

        public OperationOutcome.IssueComponent ToIssueComponent(string message, INamedNode location = null)
        {
            return ToIssueComponent(message, location != null ? location.Path : null);
        }

        public OperationOutcome.IssueComponent ToIssueComponent(string message, string path=null)
        {
            var ic = new OperationOutcome.IssueComponent() { Severity = this.Severity, Code = this.Type, Diagnostics = message };
            ic.Details = ToCodeableConcept();

            if (path != null) ic.Location = new List<string> { path };

            return ic;
        }

        private static Issue def(int code, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType type)
        {
            return new Issue() { Code = code, Severity = severity, Type = type };
        }

        // Content errors
        public static readonly Issue CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN = def(1000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN = def(1001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
//        public static readonly Issue CONTENT_ELEMENT_HAS_UNKNOWN_TYPE = def(1002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_HAS_INCORRECT_TYPE = def(1003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_MUST_MATCH_TYPE = def(1004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_VALUE_TOO_LONG = def(1005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE = def(1006, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_INCORRECT_OCCURRENCE = def(1007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_FIXED_VALUE = def(1008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_DOES_NOT_MATCH_PATTERN_VALUE = def(1009, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        public static readonly Issue CONTENT_ELEMENT_CHOICE_WITH_NO_ACTUAL_TYPE = def(1010, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // Profile problems
        public static readonly Issue PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE = def(2000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAX_USES_UNORDERED_TYPE = def(2001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE = def(2002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE = def(2003, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE = def(2004, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
//        public static readonly Issue PROFILE_ELEMENTDEF_NO_PRIMITIVE_REGEX = def(2005, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE = def(2006, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CARDINALITY_MISSING = def(2007, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_IS_EMPTY = def(2008, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_CHOICE_WITHOUT_XSUFFIX = def(2009, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);


        // Unsupported 
        public static readonly Issue UNSUPPORTED_SLICING_NOT_SUPPORTED = def(3000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.NotSupported);

        // Non-availability, incomplete data
        public static readonly Issue UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE = def(4000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_ELEMENTDEF_WITHOUT_STRUCTDEF = def(4001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_NEED_SNAPSHOT = def(4002, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);
        public static readonly Issue UNAVAILABLE_SNAPSHOT_GENERATION_FAILED = def(40032, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Incomplete);

        // Processing information
        public static readonly Issue PROCESSING_PROGRESS = def(5000, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational);
    }

}