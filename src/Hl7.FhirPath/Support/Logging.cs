/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System;

namespace Furore.Support
{
    /// <summary>
    /// Utility class for creating and unwrapping <see cref="Exception"/> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        internal static ArgumentException Argument(string message)
        {
            return new ArgumentException(message);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        internal static ArgumentException Argument(string parameterName, string message)
        {
            return new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        internal static ArgumentException ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        internal static ArgumentNullException ArgumentNull(string parameterName, string message)
        {
            return new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with a default message.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
        {
            return Error.Argument(parameterName, $"The argument '{parameterName}' is null or empty.");
        }


        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        internal static InvalidOperationException InvalidOperation(string message)
        {
            return new InvalidOperationException(message);
        }

        internal static Exception Format(string v, object p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        internal static InvalidOperationException InvalidOperation(Exception innerException, string message)
        {
            return new InvalidOperationException(message, innerException);
        }

        /// <summary>
        /// Creates an <see cref="NotSupportedException"/>.
        /// </summary>
        internal static NotSupportedException NotSupported(string message)
        {
            return new NotSupportedException(message);
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        internal static NotImplementedException NotImplemented(string message)
        {
            return new NotImplementedException(message);
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        /// <returns></returns>
        internal static NotImplementedException NotImplemented()
        {
            return new NotImplementedException();
        }

        internal static FormatException Format(string message)
        {
            return new FormatException(message);
        }
    }
}
