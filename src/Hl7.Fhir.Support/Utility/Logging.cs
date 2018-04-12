/*  
* Copyright (c) 2016, Firely (info@fire.ly) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System;

namespace Hl7.Fhir.Utility
{
    public static class Message
    {
        public static void Info(string messageFormat, params object[] messageArgs)
        {
#if DEBUGX
            Debug.WriteLine(Error.formatMessage(messageFormat,messageArgs)); 
#endif
        }
    }

    /// <summary>
    /// Utility class for creating and unwrapping <see cref="Exception"/> instances.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        public static ArgumentException Argument(string message)
        {
            return new ArgumentException(message);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        public static ArgumentException Argument(string parameterName, string message)
        {
            return new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        public static ArgumentException ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        public static ArgumentNullException ArgumentNull(string parameterName, string message)
        {
            return new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with a default message.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        public static ArgumentException ArgumentNullOrEmpty(string parameterName)
        {
            return Error.Argument(parameterName, $"The argument '{parameterName}' is null or empty.");
        }


        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        public static InvalidOperationException InvalidOperation(string message)
        {
            return new InvalidOperationException(message);
        }

        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        public static InvalidOperationException InvalidOperation(Exception innerException, string message)
        {
            return new InvalidOperationException(message, innerException);
        }

        /// <summary>
        /// Creates an <see cref="NotSupportedException"/>.
        /// </summary>
        public static NotSupportedException NotSupported(string message)
        {
            return new NotSupportedException(message);
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        public static NotImplementedException NotImplemented(string message)
        {
            return new NotImplementedException(message);
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        /// <returns></returns>
        public static NotImplementedException NotImplemented()
        {
            return new NotImplementedException();
        }

        public static FormatException Format(string message)
        {
            return new FormatException(message);
        }

        /// <summary> 
        /// Creates an <see cref="FormatException"/> with the provided properties. 
        /// </summary> 
        public static FormatException Format(string message, string location)
        {
            if (location != null)
                message += $" (at path '{location}')";

            return Format(message);
        }

        /// <summary> 
        /// Creates an <see cref="FormatException"/> with the provided properties. 
        /// </summary> 
        public static FormatException Format(string message, IPositionInfo pos)
        {
            string location = null;

            if (pos != null)
                location = $"line {pos.LineNumber}, pos {pos.LinePosition}";

            return Format(message, location);
        }
    }
}
