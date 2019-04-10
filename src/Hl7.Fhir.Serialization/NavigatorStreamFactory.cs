/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.IO;

#if !NETSTANDARD1_1

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Factory to create new <see cref="INavigatorStream"/> instances for navigating
    /// serialized resources, independent of the underlying resource serialization format.
    /// <para>
    /// The internal XML and JSON parsers use the default configuration settings.
    /// Alternatively, use <see cref="ConfigurableNavigatorStreamFactory"/> to specify custom parser settings.
    /// </para>
    /// </summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    /// <seealso cref="ConfigurableNavigatorStreamFactory"/>
    public class DefaultNavigatorStreamFactory
    {
        private static readonly ConfigurableNavigatorStreamFactory _factory = ConfigurableNavigatorStreamFactory.CreateDefault();

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
        public static INavigatorStream Create(Stream stream, string format) // => Create(stream, format, true);
            => _factory.Create(stream, format);

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
            => _factory.Create(stream, format, disposeStream);

        /// <summary>Determines serialization format by inspecting the file extension.</summary>
        /// <param name="path">File path to a FHIR artifact.</param>
        /// <returns>A constant string value as defined by <see cref="FhirSerializationFormats"/>, or <c>null</c>.</returns>
        public static string GetSerializationFormat(string path)
            => ConfigurableNavigatorStreamFactory.GetSerializationFormat(path);

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
        public static INavigatorStream Create(string path)
            => _factory.Create(path);

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <param name="disposeStream">Determines if the <see cref="IDisposable.Dispose()"/> method should also dispose the internal <see cref="Stream"/> instance.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
        public static INavigatorStream Create(string path, bool disposeStream)
            => _factory.Create(path, disposeStream);
    }

    /// <summary>
    /// Factory to create new <see cref="INavigatorStream"/> instances for navigating
    /// serialized resources, independent of the underlying resource serialization format.
    /// <para>
    /// The internal XML and JSON parsers use the specified configuration settings.
    /// Alternatively, use the <see cref="DefaultNavigatorStreamFactory"/> to use default parser configuration settings.
    /// </para>
    /// </summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    /// <seealso cref="DefaultNavigatorStreamFactory"/>
    public class ConfigurableNavigatorStreamFactory
    {
        /// <summary>Create a new <see cref="ConfigurableNavigatorStreamFactory"/> instance for the default parser settings.</summary>
        internal static ConfigurableNavigatorStreamFactory CreateDefault() => new ConfigurableNavigatorStreamFactory(null, null);

        /// <summary>Determines serialization format by inspecting the file extension.</summary>
        /// <param name="path">File path to a FHIR artifact.</param>
        /// <returns>A constant string value as defined by <see cref="FhirSerializationFormats"/>, or <c>null</c>.</returns>
        public static string GetSerializationFormat(string path)
        {
            if (FhirFileFormats.HasXmlExtension(path)) { return FhirSerializationFormats.Xml; }
            if (FhirFileFormats.HasJsonExtension(path)) { return FhirSerializationFormats.Json; }
            return null;
        }

        /// <summary>Create a new <see cref="ConfigurableNavigatorStreamFactory"/> instance for the specified parser configuration settings.</summary>
        /// <param name="xmlParsingSettings">Configuration settings that control the behavior of the internal XML parser.</param>
        /// <param name="jsonParsingSettings">Configuration settings that control the behavior of the internal JSON parser.</param>
        public ConfigurableNavigatorStreamFactory(FhirXmlParsingSettings xmlParsingSettings, FhirJsonParsingSettings jsonParsingSettings)
        {
            XmlParsingSettings = xmlParsingSettings?.Clone() ?? FhirXmlParsingSettings.CreateDefault();
            JsonParsingSettings = jsonParsingSettings?.Clone() ?? FhirJsonParsingSettings.CreateDefault();
        }

        /// <summary>Create a new <see cref="ConfigurableNavigatorStreamFactory"/> instance.</summary>
        /// <param name="permissiveParsing">Specify <c>true</c> to suppress raising exceptions for recoverable parsing errors.</param>
        public ConfigurableNavigatorStreamFactory(bool permissiveParsing)
        {
            XmlParsingSettings = new FhirXmlParsingSettings() { PermissiveParsing = permissiveParsing };
            JsonParsingSettings = new FhirJsonParsingSettings() { PermissiveParsing = permissiveParsing };
        }

        /// <summary>Determines if disposing the <see cref="INavigatorStream"/> should also dispose the internal <see cref="Stream"/> instance.</summary>
        public bool DisposeStream { get; set; } = true;

        // [WMR 20181026] EXPERIMENTAL

        /// <summary>
        /// Gets or sets a boolean value that controls behavior of the <see cref="Create(string)"/>
        /// methods when encountering an unsupported file/serialization format.
        /// <para>
        /// If <c>true</c>, throw a <see cref="NotSupportedException"/>, otherwise (<c>false</c>) return <c>null</c>.
        /// </para>
        /// </summary>
        public bool ThrowOnUnsupportedFormat { get; set; } = true;

        /// <summary>Gets the configuration settings that control the behavior of the XML parser.</summary>
        public FhirXmlParsingSettings XmlParsingSettings { get; }

        /// <summary>Gets the configuration settings that control the behavior of the JSON parser.</summary>
        public FhirJsonParsingSettings JsonParsingSettings { get; }

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource stream, independent of the serialization format,
        /// using the current <see cref="XmlParsingSettings"/> or <see cref="JsonParsingSettings"/>.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> for reading a serialized FHIR resource.</param>
        /// <param name="format">A string value that represents the FHIR resource serialization format, as defined by <see cref="FhirSerializationFormats"/>.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance.</returns>
        /// <remarks>Supports XML and JSON serialization formats.</remarks>
        /// <exception cref="NotSupportedException">The specified FHIR resource serialization format is not supported.</exception>
        /// <seealso cref="FhirSerializationFormats"/>
        public INavigatorStream Create(Stream stream, string format) => Create(stream, format, DisposeStream);

        // Called by DefaultNavigatorStream.Create(Stream stream, string format, bool disposeStream)
        internal INavigatorStream Create(Stream stream, string format, bool disposeStream)
        {
            switch (format)
            {
                case FhirSerializationFormats.Xml:
                    return new XmlNavigatorStream(stream, disposeStream, XmlParsingSettings);
                case FhirSerializationFormats.Json:
                    return new JsonNavigatorStream(stream, disposeStream, JsonParsingSettings);
                default:
                    if (ThrowOnUnsupportedFormat)
                    {
                        throw Error.NotSupported($"Unsupported FHIR serialization format ('{format}').");
                    }
                    return null;
            }
        }

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format,
        /// using the current <see cref="XmlParsingSettings"/> or <see cref="JsonParsingSettings"/>.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
        public INavigatorStream Create(string path) => Create(path, DisposeStream);

        // Called by DefaultNavigatorStream.Create(string path, bool disposeStream)
        internal INavigatorStream Create(string path, bool disposeStream)
        {
            if (FhirFileFormats.HasXmlExtension(path))
            {
                return XmlNavigatorStream.FromPath(path, disposeStream, XmlParsingSettings);
            }
            if (FhirFileFormats.HasJsonExtension(path))
            {
                return JsonNavigatorStream.FromPath(path, disposeStream, JsonParsingSettings);
            }

            // Unsupported extension
            if (ThrowOnUnsupportedFormat)
            {
                throw Error.NotSupported($"Unsupported file format ('{path}').");
            }
            return null;
        }
    }

}

#endif
