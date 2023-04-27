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
        /// Severity of this specific issue.
        /// </summary>
        /// <remarks>
        /// Setter is public to permit others to upgrade/downgrade specific issues
        /// as needed.
        /// </remarks>
        public OperationOutcome.IssueSeverity IssueSeverity { get; init; } = OperationOutcome.IssueSeverity.Error;

        /// <summary>
        /// Type of issue to report in a FHIR OperationOutcome.
        /// </summary>
        public OperationOutcome.IssueType IssueType { get; init; } = OperationOutcome.IssueType.Invalid;

        /// <summary>
        /// The error message without any location information appended to it (which is in Exception.Message property).
        /// </summary>
        public string? BaseErrorMessage { get; init; }

        /// <summary>
        /// The line number of the error in the original source data.
        /// </summary>
        public long? LineNumber { get; init; }

        /// <summary>
        /// The position of the error on the line in the original source data.
        /// </summary>
        public long? Position { get; init; }

        /// <summary>
        /// The instancePath of the error in the resource in simple fhirpath format.
        /// </summary>
        public string? Location { get; init; }
    }
}

#nullable restore
