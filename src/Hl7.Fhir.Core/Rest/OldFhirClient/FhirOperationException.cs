/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


// [WMR 20181024] Issue #737
// Removed serialization support from FhirOperationException (cf. all other custom exceptions)
// Problems:
// - NetFramework: Exception.GetObjectData override requires SecurityCritical attribute
//    => incompatible with partial-trust environments, e.g. dotNetFiddle
//    Alternative ISafeSerializationInfo interface is not widely supported and obsolete in netCore
// - NetCore 1.1 : no support for serialiation
// - NetCore 2.0 : reintroduces limited support for serialiation
// No clear picture of use cases that depend on exception serialization; remove for now
// If necessary, we can re-introduce serialization support in a future release (by customer demand)

using Hl7.Fhir.Model;
using System;
using System.Net;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Represents HL7 FHIR errors that occur during application execution.
    /// </summary>
    public class FhirOperationException : Exception
    {
        /// <summary>Gets or sets the outcome of the operation <see cref="OperationOutcome"/>.</summary>
        public OperationOutcome Outcome { get; private set; }

        /// <summary>The HTTP Status Code that resulted in this Exception.</summary>
        public HttpStatusCode Status { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        public FhirOperationException(string message, HttpStatusCode status)
            : this(message, status, null, null) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a <c>null</c> reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public FhirOperationException(string message, HttpStatusCode status, Exception inner)
            : this(message, status, null, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="outcome">The outcome of the operation <see cref="OperationOutcome"/>.</param>
        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome)
            : this(message, status, outcome, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="outcome">The outcome of the operation <see cref="OperationOutcome"/>.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a <c>null</c> reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome, Exception inner)
            : base(message, inner)
        {
            Outcome = outcome;
            Status = status;
        }
    }

}