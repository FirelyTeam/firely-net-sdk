/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

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
    public class CodedException : Exception
    {
        public CodedException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public CodedException(string errorCode, string message, Exception? innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The unique and permanent code for this error.
        /// </summary>
        /// <remarks>Developers can assume that these codes will not change in future versions.</remarks>
        public string ErrorCode { get; }
    }
}

#nullable restore
