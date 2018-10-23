// [WMR 20181022] NEW
// Exception.GetObjectData override requires SecurityCritical attribute, not supported in low-trust environments
//
// MSDN suggests to hook Exception.SerializeObjectState event instead
// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.isafeserializationdata?redirectedfrom=MSDN&view=netframework-4.7.2
//
// Problem: in netcore, Exception.SerializeObjectState event throws NotSupportedException
// See source code in coreclr repo:
// https://github.com/dotnet/coreclr/blob/3b807944d8822b44eb5085d6b95b130b4a91808f/src/System.Private.CoreLib/src/System/Exception.cs#L421
//
// protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
// {
// 	 add { throw new PlatformNotSupportedException(SR.PlatformNotSupported_SecureBinarySerialization); }
// 	 remove { throw new PlatformNotSupportedException(SR.PlatformNotSupported_SecureBinarySerialization); }
// }
// 
// * dotnetframework
//   Method Exception.GetObjectData in runtime is decorated with SecurityCritical attribute
//   => Overriding method in derived class must also declare SecurityCritical attribute
//   => incompatible with partial trust environment...
//   Workaround: Custom exceptions can hook the SerializeObjectState event
//   => compatible with partial trust environment
//   => NOT supported by JSON.NET, Orleans...
//
// * dotnetcore 2.0+
//   Method Exception.GetObjectData in runtime is NOT decorated with SecurityCritical attribute
//   Custom exceptions can override method Exception.GetObjectData
//   No need to specify SecurityCritical attribute
//   => compatible with partial trust environment
//   => Supported by JSON.NET, Orleans
//
// * dotnetcore 1.0
//   Exceptions are not serializable

#if NET_FW
#define SAFE_SERIALIZATION
#endif

#if !NETSTANDARD1_1 && !NET_FW
#define UNSAFE_SERIALIZATION
#endif

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
using System.Diagnostics;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Represents HL7 FHIR errors that occur during application execution.
    /// </summary>
#if !NETSTANDARD1_1
    [Serializable]
#endif
    public class FhirOperationException : Exception
    {
#if NET_FW && SAFE_SERIALIZATION
        FhirOperationExceptionData _state;

        /// <summary>Gets the outcome of the operation <see cref="OperationOutcome"/>.</summary>
        public OperationOutcome Outcome
        {
            get => _state.Outcome;
            private set => _state.Outcome = value;
        }

        /// <summary>Gets the HTTP Status Code that resulted in this Exception.</summary>
        public HttpStatusCode Status
        {
            get => _state.Status;
            private set => _state.Status = value;
        }
#else
        /// <summary>Gets or sets the outcome of the operation <see cref="OperationOutcome"/>.</summary>
        public OperationOutcome Outcome { get; private set; }

        /// <summary>The HTTP Status Code that resulted in this Exception.</summary>
        public HttpStatusCode Status { get; private set; }
#endif

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

// --- DEBUGGING ---
//#if NETSTANDARD1_1
//            Debug.WriteLine("NETSTANDARD1_1");
//#endif
//#if NETSTANDARD2_0
//            Debug.WriteLine("NETSTANDARD2_0");
//#endif
//#if NET_FW
//            Debug.WriteLine("NET_FW");
//#endif

#if NET_FW && SAFE_SERIALIZATION
            RegisterSerializeObjectState();
#endif
        }

#if NET_FW && SAFE_SERIALIZATION
        void RegisterSerializeObjectState()
        {
            // Custom serialization
            SerializeObjectState += OnSerializeObjectState;
        }

        // In response to SerializeObjectState, we need to provide any state to serialize with the exception.
        // In this case, since our state is already stored in an ISafeSerializationData implementation, we can just provide that.
        void OnSerializeObjectState(object exception, SafeSerializationEventArgs eventArgs)
        {
            eventArgs.AddSerializedState(_state);
        }

        struct FhirOperationExceptionData : ISafeSerializationData
        {
            public OperationOutcome Outcome;
            public HttpStatusCode Status;

            // This method is called when deserialization of the exception is complete.
            void ISafeSerializationData.CompleteDeserialization(object obj)
            {
                // Since the exception simply contains an instance of the exception state object, we can repopulate it 
                // here by just setting its instance field to be equal to this deserialized state instance.
                if (obj is FhirOperationException foe)
                {
                    foe.RegisterSerializeObjectState();
                    foe._state = this;
                }
            }
        }
#endif // NET_FW && SAFE_SERIALIZATION

        // [WMR 20181022] https://github.com/ewoutkramer/fhir-net-api/issues/737
        // Problem: low-trust environments ignore SecurityCritical attribute => assembly load error
        // i.e. this prevent use of Hl7.Fhir.Core assembly in low-trust environments (e.g. dotNetFiddle)

        // * dotNetFramework: Exception.GetObjectData is decorated with SecurityCritical attribute
        //   => derived class must also declare the attribute
        //   => incompatible with partial trust environment
        // * dotNetCore 1: Exceptions are not serializable
        // * dotNetCore 2: Exception.GetObjectData is NOT decorated with SecurityCritical attribute
        //   => no need to declare attribute in derived class

#if UNSAFE_SERIALIZATION

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param><exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
#if NET_FW
        [SecurityCritical]
        // [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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
        // [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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

#endif // UNSAFE_SERIALIZATION

    }

}
