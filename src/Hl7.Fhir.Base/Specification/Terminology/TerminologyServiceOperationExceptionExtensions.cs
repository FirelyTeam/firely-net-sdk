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
using System;
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
    /// These are codes from the Terminology Issue Type code system
    /// (http://hl7.org/fhir/tools/CodeSystem/tx-issue-type)
    /// See https://hl7.org/fhir/tools/0.8.0/CodeSystem-tx-issue-type.html.
    /// </summary>
    private static readonly Code NOT_FOUND = new("not-found");
    private static readonly Code INVALID_DATA = new("invalid-data");
    private static readonly Code INVALID_CODE = new("invalid-code");
    private static readonly Code INVALID_DISPLAY = new("invalid-display");
    private static readonly Code CANNOT_INFER = new("cannot-infer");
    private static readonly Code CODE_RULE = new("code-rule");
    private static readonly Code VS_INVALID = new("vs-invalid");
    private static readonly Code NOT_IN_VS = new("not-in-vs");
    private static readonly Code CODE_COMMENT = new("code-comment");

    private const string TX_ISSUE_TYPE_SYSTEM = "http://hl7.org/fhir/tools/CodeSystem/tx-issue-type";

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
            IssueSeverity severity, IssueType issueType, Code? txIssueType = null) =>
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
                                    Code = txIssueType.Literal, System = TX_ISSUE_TYPE_SYSTEM
                                } ]
                            }
                            : null,
                        Diagnostics = details,
                    }
                ]
            };
    }

    // These report the same kinds of errors as the methods above, but as issues in a $validate-code result.
    extension(ValidateCodeResult result)
    {
        /// <summary>
        /// Reports that a coded parameter is invalidly formed, e.g. a coding without a code.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="IncompleteCodedParameter"/>.</remarks>
        public IssueComponent AddIncompleteCodedParameter(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Error, IssueType.Invalid, INVALID_DATA, expression);

        /// <summary>
        /// Reports that a code is not part of the specified code system.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="CodeNotInSystem"/>.</remarks>
        public IssueComponent AddCodeNotInSystem(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Error, IssueType.CodeInvalid, INVALID_CODE, expression);

        /// <summary>
        /// Reports that a code is not part of the value set it is validated against.
        /// </summary>
        public IssueComponent AddCodeNotInValueSet(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Error, IssueType.CodeInvalid, NOT_IN_VS, expression);

        /// <summary>
        /// Reports that a code is valid, but its display is not. This is a warning, the code is still valid.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="InvalidDisplay"/>.</remarks>
        [Obsolete("Use the overload with 'lenient'. The default flips to strict (an error) in the next major version.")]
        public IssueComponent AddInvalidDisplay(string details, params string[] expression) =>
            result.AddInvalidDisplay(details, lenient: true, expression);

        /// <summary>
        /// Reports that the display of a code does not match it. When <paramref name="lenient"/> is true, this is a warning and
        /// the code is still valid; otherwise it is an error and the code is not valid. This follows the
        /// <c>lenient-display-validation</c> parameter of $validate-code.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="InvalidDisplay"/>.
        /// It only adds the issue: the caller sets the result to false when <paramref name="lenient"/> is false.</remarks>
        public IssueComponent AddInvalidDisplay(string details, bool lenient, params string[] expression) =>
            result.addIssue(details, lenient ? IssueSeverity.Warning : IssueSeverity.Error, IssueType.Invalid, INVALID_DISPLAY, expression);

        /// <summary>
        /// Reports that a code fails a business rule of the operation, e.g. it is abstract where that is not allowed.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="UnprocessableCodedParameter"/>.</remarks>
        public IssueComponent AddUnprocessableCodedParameter(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Error, IssueType.BusinessRule, CODE_RULE, expression);

        /// <summary>
        /// Reports that a code system needed to validate the code cannot be resolved.
        /// </summary>
        /// <remarks>This is the $validate-code result counterpart of <see cref="Unresolvable"/>.</remarks>
        public IssueComponent AddUnresolvable(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Error, IssueType.NotFound, NOT_FOUND, expression);

        /// <summary>
        /// Reports a remark about a valid code, e.g. that the concept is inactive or deprecated. This is a warning.
        /// </summary>
        public IssueComponent AddCodeComment(string details, params string[] expression) =>
            result.addIssue(details, IssueSeverity.Warning, IssueType.BusinessRule, CODE_COMMENT, expression);

        private IssueComponent addIssue(string details, IssueSeverity severity, IssueType issueType, Code txIssueType, string[] expression)
        {
            var issue = new IssueComponent
            {
                Severity = severity,
                Code = issueType,
                Details = new CodeableConcept(TX_ISSUE_TYPE_SYSTEM, txIssueType.Literal!, details),
            };

            if (expression.Length > 0)
                issue.Expression = expression;

            result.AddIssue(issue);
            return issue;
        }
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