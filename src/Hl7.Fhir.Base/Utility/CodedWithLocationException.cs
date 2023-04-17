/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;

#nullable enable

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// This is a class of Exceptions that is raised by the SDK and is coded with a unique code
    /// to enable callers to identify this exception and react appropriately on the code.
    /// </summary>
    /// <remarks>Most modules of the SDK using this Exception will create specific subclasses
    /// for this subclass, providing a list of codes used by that module.</remarks>
    public class CodedWithLocationException : CodedException
    {
        public CodedWithLocationException(string errorCode, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType) : base(errorCode, message)
        {
            IssueSeverity = issueSeverity;
            IssueType = issueType;
        }

        public CodedWithLocationException(string errorCode, string message, OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType, Exception? innerException) : base(errorCode, message, innerException)
        {
            IssueSeverity = issueSeverity;
            IssueType = issueType;
        }

        /// <summary>
        /// CodeSystem to be used in generating error messages in the OperationOutcome
        /// </summary>
        public static string ValidationErrorMessageCodeSystem = "http://firely.com/CodeSystem/ErrorMessages";

        /// <summary>
        /// Convert to an OperationOutcome.Issue
        /// </summary>
        /// <returns></returns>
        public virtual Model.OperationOutcome.IssueComponent ToIssue()
        {
            var result = new Model.OperationOutcome.IssueComponent()
            {
                Severity = IssueSeverity,
                Code = IssueType,
                Details = new Model.CodeableConcept(ValidationErrorMessageCodeSystem, ErrorCode, null, FormattedMessage)
            };
            if (LineNumber.HasValue && Position.HasValue)
                result.Location = new[] { $"line {LineNumber}, position {Position}" };
            if (!string.IsNullOrEmpty(Location))
                result.Expression = new[] { Location };

            return result;
        }

        /// <summary>
        /// Severity of this specific issue
        /// </summary>
        /// <remarks>
        /// Setter is public to permit others to upgrade/downgrade specific issues
        /// as needed
        /// </remarks>
        public OperationOutcome.IssueSeverity IssueSeverity { get; set; } = OperationOutcome.IssueSeverity.Error;

        /// <summary>
        /// Type of issue to report in a FHIR OperationOutcome
        /// </summary>
        public OperationOutcome.IssueType IssueType { get; protected set; } = OperationOutcome.IssueType.Invalid;

        /// <summary>
        /// The error message without any location information appended to it
        /// </summary>
        public string? FormattedMessage { get; protected set; }

        /// <summary>
        /// The line number of the error in the original source data
        /// </summary>
        public long? LineNumber { get; protected set; }

        /// <summary>
        /// The position of the error on the line in the original source data
        /// </summary>
        public long? Position { get; protected set; }

        /// <summary>
        /// The location of the error in the resource in simple fhirpath format
        /// </summary>
        public string? Location { get; protected set; }
    }
}

#nullable restore
