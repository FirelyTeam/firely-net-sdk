// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace Hl7.Fhir.Support
{
    internal static class Message
    {
        internal static void Info(string messageFormat, params object[] messageArgs)
        {
#if DEBUGX
            Debug.WriteLine(Error.formatMessage(messageFormat,messageArgs));
#endif
        }
    }


    /// <summary>
    /// Utility class for creating and unwrapping <see cref="Exception"/> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        /// Formats the specified resource string using <see cref="M:CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The formatted string.</returns>
        internal static string formatMessage(string format, params object[] args)
        {
            return String.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification="Utility method that might become useful for future usecases")]
        internal static ArgumentException Argument(string message)
        {
            return new ArgumentException(message);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with the provided properties.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static ArgumentException Argument(string parameterName, string message)
        {
            return new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        internal static ArgumentException ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Creates an <see cref="ArgumentNullException"/> with the provided properties.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
        {
            return new ArgumentNullException(parameterName, Error.formatMessage(messageFormat, messageArgs));
        }

        /// <summary>
        /// Creates an <see cref="ArgumentException"/> with a default message.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
        {
            return Error.Argument(parameterName, "The argument '{0}' is null or empty.".FormatWith(parameterName));
        }


        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
        {
            return new InvalidOperationException(Error.formatMessage(messageFormat, messageArgs));
        }

        /// <summary>
        /// Creates an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="innerException">Inner exception</param>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
        {
            return new InvalidOperationException(Error.formatMessage(messageFormat, messageArgs), innerException);
        }

        /// <summary>
        /// Creates an <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
        {
            return new NotSupportedException(Error.formatMessage(messageFormat, messageArgs));
        }

        /// <summary>
        /// Creates an <see cref="FormatException"/> with the provided properties.
        /// </summary>
        /// <param name="message">A string explaining the reason for the exception.</param>
        /// <param name="pos">Optional line position information for the message</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Utility method that might become useful for future usecases")]
        internal static FormatException Format(string message, IPositionInfo pos)
        {
            string excMessage;

            if (pos != null)
            {
                excMessage = String.Format("At line {0}, pos {1}: {2}", pos.LineNumber, pos.LinePosition, message);
            }
            else
                excMessage = Error.formatMessage(message);

            return new FormatException(excMessage);
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception"/>.</returns>
        internal static NotImplementedException NotImplemented(string messageFormat, params object[] messageArgs)
        {
            return new NotImplementedException(Error.formatMessage(messageFormat, messageArgs));
        }

        /// <summary>
        /// Creates an <see cref="NotImplementedException"/>.
        /// </summary>
        /// <returns></returns>
        internal static NotImplementedException NotImplemented()
        {
            return new NotImplementedException();
        }

        // [WMR 20160721] NEW - To signal unresolved resource references
        internal static ResourceReferenceNotFoundException ResourceReferenceNotFoundException(string url)
        {
            return new ResourceReferenceNotFoundException(url);
        }

        internal static ResourceReferenceNotFoundException ResourceReferenceNotFoundException(string url, string messageFormat, params object[] messageArgs)
        {
            return new ResourceReferenceNotFoundException(url, Error.formatMessage(messageFormat, messageArgs));
        }

    }
}
