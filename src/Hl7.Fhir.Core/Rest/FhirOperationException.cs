/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Runtime.Serialization;
using System.Security;

using Hl7.Fhir.Model;
using System.Net;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Represents HL7 FHIR errors that occur during application execution.
    /// </summary>
#if DOTNETFW
    [SerializableAttribute]
#endif
    public class FhirOperationException : Exception
    {
        /// <summary>
        /// Gets or sets the outcome of the operation <see cref="OperationOutcome"/>.
        /// </summary>
        public OperationOutcome Outcome { get; set; }
        /// <summary>
        /// The HTTP Status Code that resulted in this Exception
        /// </summary>
        /// <remarks>
        /// </remarks>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        public FhirOperationException(string message, HttpStatusCode status)
            : base(message)
        {
            Status = status;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public FhirOperationException(string message, HttpStatusCode status, Exception inner)
            : base(message, inner)
        {
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="outcome">The outcome of the operation <see cref="OperationOutcome"/>.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome, Exception inner)
            : base(message, inner)
        {
            Outcome = outcome;
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="status">The http status code associated with the message</param>
        /// <param name="outcome">The outcome of the operation <see cref="OperationOutcome"/>.</param>
        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome)
            : base(message)
        {
            Outcome = outcome;
            Status = status;
        }


#if DOTNETFW
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param><exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (this.Outcome != null)
            {
                info.AddValue("Outcome", this.Outcome);
            }

            base.GetObjectData(info, context);
        }
#endif
    }
}
