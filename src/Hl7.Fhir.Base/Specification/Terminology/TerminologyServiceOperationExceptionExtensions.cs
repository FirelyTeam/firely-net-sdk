/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Net;
using static Hl7.Fhir.Model.OperationOutcome;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Provides helper method for consistently reporting errors from terminology service operations.
/// </summary>
/// <remarks>
/// Since FHIR specification failed to define specific error conditions for terminology service operations,
/// there is not much consistency in how terminology services report errors. This class therefore describes
/// how terminology service implementations in Firely .NET SDK should report errors, but it cannot be
/// guaranteed that other implementations will follow the same conventions.
/// </remarks>
public static class TerminologyServiceOperationExceptionExtensions
{
    /// <summary>
    /// The system of the Terminology Issue Type code system, used in <c>OperationOutcome.issue.details.coding</c>.
    /// </summary>
    public const string TX_ISSUE_TYPE_SYSTEM = "http://hl7.org/fhir/tools/CodeSystem/tx-issue-type";

    // These are codes from the Terminology Issue Type code system
    // (http://hl7.org/fhir/tools/CodeSystem/tx-issue-type)
    // See https://build.fhir.org/ig/FHIR/fhir-tools-ig/CodeSystem-tx-issue-type.html.

    /// <summary>A resource (e.g. a CodeSystem or ValueSet) needed to process the request could not be found.</summary>
    public const string NOT_FOUND = "not-found";
    /// <summary>The data is invalidly formed.</summary>
    public const string INVALID_DATA = "invalid-data";
    /// <summary>The code is not valid in the code system.</summary>
    public const string INVALID_CODE = "invalid-code";
    /// <summary>The display is not valid for the code.</summary>
    public const string INVALID_DISPLAY = "invalid-display";
    /// <summary>The system cannot be inferred from the context.</summary>
    public const string CANNOT_INFER = "cannot-infer";
    /// <summary>The code fails a rule, e.g. it is abstract or inactive where that is not allowed.</summary>
    public const string CODE_RULE = "code-rule";
    /// <summary>The value set is invalid and cannot be processed.</summary>
    public const string VS_INVALID = "vs-invalid";
    /// <summary>The code is valid in its code system, but not in the value set.</summary>
    public const string NOT_IN_VS = "not-in-vs";
    /// <summary>This coding of a CodeableConcept is not in the value set, but another coding is.</summary>
    public const string THIS_CODE_NOT_IN_VS = "this-code-not-in-vs";
    /// <summary>A comment about the code, e.g. that it is inactive or deprecated.</summary>
    public const string CODE_COMMENT = "code-comment";
    /// <summary>The status of a resource used in processing (e.g. draft, retired) is worth noting.</summary>
    public const string STATUS_CHECK = "status-check";

    extension(FhirOperationException)
    {
        /// <summary>
        /// Use this error when there is an issue with provided parameters for an operation,
        /// e.g. a required parameter is missing or a combination of parameters is invalid.
        /// </summary>
        public static FhirOperationException InvalidOperationInvocation(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.Invalid);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// This error indicates that a coded parameter is invalidly formed,
        /// e.g. a code is provided without a system, a CondeableConcept without codes, or a coding without a code
        /// </summary>
        public static FhirOperationException IncompleteCodedParameter(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.Invalid, INVALID_DATA);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// This error indicates that a coded parameter is well-formed,
        /// but code is not part of specified code system
        /// </summary>
        public static FhirOperationException CodeNotInSystem(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.CodeInvalid, INVALID_CODE);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// The coded parameter well-formed, but misses a system and system cannot be inferred
        /// from the given valueset context.
        /// </summary>
        public static FhirOperationException SystemCannotBeInferred(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.CodeInvalid, CANNOT_INFER);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// The coded parameter is well-formed and can be found in the system, but display is invalid.
        /// </summary>
        public static FhirOperationException InvalidDisplay(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.CodeInvalid, INVALID_DISPLAY);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// The coded parameter is well-formed, but fails a business rule for the operation,
        /// e.g. a $subsumes where codes are not in the same code system.
        /// </summary>
        /// <remarks>This is the same kind of error as <see cref="UnprocessableParameter"/>, but for coded parameters.</remarks>
        public static FhirOperationException UnprocessableCodedParameter(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.BusinessRule, CODE_RULE);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// The (non-coded) parameters are well-formed, but fail a business rule for the operation,
        /// e.g. they specify an incorrect filterProperty, offset or count etcetera.
        /// </summary>
        /// <remarks>This is the same kind of error as <see cref="UnprocessableCodedParameter"/>, but for non-coded parameters.</remarks>
        public static FhirOperationException UnprocessableParameter(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.BusinessRule);

            // The invocation itself is invalid, processing has not started, so return BadRequest
            return new(details, HttpStatusCode.BadRequest, oo);
        }

        /// <summary>
        /// A valueset or codesystem was found while processing that cannot be resolved. This
        /// can be a reference in a parameter, or a reference from within a ValueSet definition.
        /// </summary>
        public static FhirOperationException Unresolvable(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.NotFound, NOT_FOUND);

            // Processing has started, but we encounter a problem, so return UnprocessableEntity
            return new(details, HttpStatusCode.UnprocessableContent, oo);
        }

        /// <summary>
        /// Use this error when provided (or resolved) ValueSet cannot be processed,
        /// like an invalid include or exclude definition, an incorrect filter or property,
        /// </summary>
        public static FhirOperationException InvalidValueSet(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.BusinessRule, VS_INVALID);

            // Processing has started, but we encounter a problem, so return UnprocessableEntity
            return new(details, HttpStatusCode.UnprocessableContent, oo);
        }

        /// <summary>
        /// Use this error when an operation would exceed some imposed limit, e.g. maximum expansion size.
        /// </summary>
        public static FhirOperationException TooCostly(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.TooCostly);

            // Processing has started, but we encounter a problem, so return UnprocessableEntity
            return new(details, HttpStatusCode.UnprocessableContent, oo);
        }

        /// <summary>
        /// Use this error when an operation, or a requested feature of an operation, is not supported.
        /// E.g. a filter that is known, but not supported by this terminology service implementation.
        /// </summary>
        public static FhirOperationException NotSupported(string details)
        {
            var oo = createOutcome(details, IssueSeverity.Error, IssueType.NotSupported);

            // Processing has started, but we encounter a problem, so return UnprocessableEntity
            // Note, this is debatable, as one could also argue for NotImplemented (501), but
            // here, we choose UnprocessableEntity, as this is not necessarily a condition that
            // requires immediate action - something a 5xx would suggest.
            return new(details, HttpStatusCode.UnprocessableContent, oo);
        }

        private static OperationOutcome createOutcome(
            string details,
            IssueSeverity severity, IssueType issueType, string? txIssueType = null) =>
            new()
            {
                Issue =
                [
                    new()
                    {
                        Severity = severity,
                        Code = issueType,
                        Details = txIssueType != null
                            ? new CodeableConcept
                            {
                                Coding = [ new()
                                {
                                    Code = txIssueType, System = TX_ISSUE_TYPE_SYSTEM
                                } ]
                            }
                            : null,
                        Diagnostics = details,
                    }
                ]
            };
    }
}

#if NETSTANDARD2_1
internal static class HttpStatusCodePolyfills
{
    extension(HttpStatusCode)
    {
        public static HttpStatusCode UnprocessableContent => (HttpStatusCode)422;
    }
}
#endif