/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.IO;

#if NET_FILESYSTEM

namespace Hl7.Fhir.Serialization
{
    // [WMR 20171031] We want consumers to be able to implement custom serialization formats.
    // By defining format constants as strings (instead of enum), consumers can define additional values.

    /// <summary>
    /// Factory to create new <see cref="INavigatorStream"/> instances to navigate
    /// serialized resources, independent of the underlying resource serialization format.
    /// </summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    public static class DefaultNavigatorStreamFactory
    {
        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource stream, independent of the serialization format.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> for reading a serialized FHIR resource.</param>
        /// <param name="format">A string value that represents the FHIR resource serialization format, as defined by <see cref="FhirSerializationFormats"/>.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance.</returns>
        /// <remarks>Supports XML and JSON serialization formats.</remarks>
        /// <exception cref="NotSupportedException">The specified FHIR resource serialization format is not supported.</exception>
        /// <seealso cref="FhirSerializationFormats"/>
        public static INavigatorStream Create(Stream stream, string format) => Create(stream, format, true);

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource stream, independent of the serialization format.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> for reading a serialized FHIR resource.</param>
        /// <param name="format">A string value that represents the FHIR resource serialization format, as defined by <see cref="FhirSerializationFormats"/>.</param>
        /// <param name="disposeStream">Determines if the <see cref="IDisposable.Dispose()"/> method should also dispose the specified <paramref name="stream"/> instance.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance.</returns>
        /// <remarks>Supports XML and JSON serialization formats.</remarks>
        /// <exception cref="NotSupportedException">The specified FHIR resource serialization format is not supported.</exception>
        /// <seealso cref="FhirSerializationFormats"/>
        public static INavigatorStream Create(Stream stream, string format, bool disposeStream)
        {
            switch (format)
            {
                case FhirSerializationFormats.Xml:
                    return new XmlNavigatorStream(stream, disposeStream);
                case FhirSerializationFormats.Json:
                    return new JsonNavigatorStream(stream, disposeStream);
                default:
                    throw Error.NotSupported($"Unsupported FHIR serialization format ('{format}').");
            }
        }

        /// <summary>Determines serialization format by inspecting the file extension.</summary>
        /// <param name="path">File path to a FHIR artifact.</param>
        /// <returns>A constant string value as defined by <see cref="FhirSerializationFormats"/>, or <c>null</c>.</returns>
        public static string GetSerializationFormat(string path)
        {
            if (FhirFileFormats.HasXmlExtension(path))
            {
                return FhirSerializationFormats.Xml;
            }
            if (FhirFileFormats.HasJsonExtension(path))
            {
                return FhirSerializationFormats.Json;
            }
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
        public static INavigatorStream Create(string path)
        {
            if (FhirFileFormats.HasXmlExtension(path))
            {
                return XmlNavigatorStream.FromPath(path);
            }
            if (FhirFileFormats.HasJsonExtension(path))
            {
                return JsonNavigatorStream.FromPath(path);
            }

            // Unsupported extension
            return null;
        }

    }
}

#endif
