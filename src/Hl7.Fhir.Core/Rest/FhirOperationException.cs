/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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

// #if !NETSTANDARD1_1
// #define SERIALIZABLE
// #endif

using Hl7.Fhir.Model;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Represents HL7 FHIR errors that occur during application execution.
    /// </summary>
#if SERIALIZABLE
    [Serializable]
#endif
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


        // [WMR 20181022] https://github.com/ewoutkramer/fhir-net-api/issues/737
        // Problem: low-trust environments ignore SecurityCritical attribute => assembly load error
        // i.e. this prevent use of Hl7.Fhir.Core assembly in low-trust environments (e.g. dotNetFiddle)

        // * dotNetFramework: Exception.GetObjectData is decorated with SecurityCritical attribute
        //   => derived class must also declare the attribute
        //   => incompatible with partial trust environment
        // * dotNetCore 1: Exceptions are not serializable
        // * dotNetCore 2: Exception.GetObjectData is NOT decorated with SecurityCritical attribute
        //   => no need to declare attribute in derived class

#if SERIALIZABLE

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param><exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
#if NET_FW
        [SecurityCritical]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (this.Outcome != null)
            {
                info.AddValue(nameof(Outcome), this.Outcome);
            }

            if (this.Status != 0)
            {
                info.AddValue(nameof(Status), (int)this.Status);
            }

            base.GetObjectData(info, context);
        }

        // De-serialization ctor

        /// <summary>Initializes a new instance of the <see cref="FhirOperationException"/> class with serialized data.</summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
#if NET_FW
        [SecurityCritical]
#endif
        public FhirOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info.GetValue(nameof(Outcome), typeof(OperationOutcome)) is OperationOutcome oo)
            {
                this.Outcome = oo;
            }

            var status = info.GetInt32(nameof(Status));
            if (status != 0) // && Enum.IsDefined(typeof(HttpStatusCode), status))
            {
                this.Status = (HttpStatusCode)status;
            }
        }

#endif

    }

}
