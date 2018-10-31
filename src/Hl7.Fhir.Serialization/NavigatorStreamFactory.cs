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

#if !NETSTANDARD1_1

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Factory interface to create new <see cref="INavigatorStream"/> instances for navigating
    /// serialized resources, independent of the underlying resource serialization format.
    /// </summary>
    public interface INavigatorStreamFactory
    {
        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource stream, independent of the serialization format.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> for reading a serialized FHIR resource.</param>
        /// <param name="format">A string value that represents the FHIR resource serialization format, as defined by <see cref="FhirSerializationFormats"/>.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance.</returns>
        /// <seealso cref="FhirSerializationFormats"/>
        INavigatorStream Create(Stream stream, string format);

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="path">File path specification of a FHIR resource file.</param>
        /// <returns>A new <see cref="INavigatorStream"/> instance, or <c>null</c> (unsupported file extension).</returns>
        INavigatorStream Create(string path);

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the XML parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirXmlParsingSettings"/> instance.</value>
        FhirXmlParsingSettings XmlParserSettings { get; set;}

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the JSON parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirJsonParsingSettings"/> instance.</value>
        FhirJsonParsingSettings JsonParserSettings { get; set; }
    }

    /// <summary>
    /// Factory to create new <see cref="INavigatorStream"/> instances for navigating
    /// serialized resources, independent of the underlying resource serialization format.
    /// <para>
    /// Uses the specified configuration settings for the internal XML and JSON parsers.
    /// Alternatively, use the <see cref="DefaultNavigatorStreamFactory"/> to use default parser configuration settings.
    /// </para>
    /// </summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    /// <seealso cref="DefaultNavigatorStreamFactory"/>
    public class NavigatorStreamFactory : INavigatorStreamFactory
    {
        /// <summary>
        /// Creates a new <see cref="NavigatorStreamFactory"/> instance for the default parser settings.
        /// Enables <see cref="FhirXmlParsingSettings.PermissiveParsing"/> and <see cref="FhirJsonParsingSettings.PermissiveParsing"/>.
        /// </summary>
        internal static NavigatorStreamFactory CreateDefault() => new NavigatorStreamFactory();

        /// <summary>Determines serialization format by inspecting the file extension.</summary>
        /// <param name="path">File path to a FHIR artifact.</param>
        /// <returns>A constant string value as defined by <see cref="FhirSerializationFormats"/>, or <c>null</c>.</returns>
        public static string GetSerializationFormat(string path)
        {
            if (FhirFileFormats.HasXmlExtension(path)) { return FhirSerializationFormats.Xml; }
            if (FhirFileFormats.HasJsonExtension(path)) { return FhirSerializationFormats.Json; }
            return null;
        }

        private FhirXmlParsingSettings _xmlParsingSettings; // = FhirXmlParsingSettings.CreateDefault();
        private FhirJsonParsingSettings _jsonParsingSettings; // = FhirJsonParsingSettings.CreateDefault();

        /// <summary>
        /// Default constructor.
        /// Creates a new <see cref="NavigatorStreamFactory"/> instance for the default parser settings.
        /// Enables <see cref="FhirXmlParsingSettings.PermissiveParsing"/> and <see cref="FhirJsonParsingSettings.PermissiveParsing"/>.
        /// </summary>
        public NavigatorStreamFactory() : this(true) { }

        /// <summary>Create a new <see cref="NavigatorStreamFactory"/> instance.</summary>
        /// <param name="permissiveParsing">Specify <c>true</c> to suppress raising exceptions for recoverable parsing errors.</param>
        public NavigatorStreamFactory(bool permissiveParsing)
        {
            XmlParserSettings = new FhirXmlParsingSettings() { PermissiveParsing = permissiveParsing };
            JsonParserSettings = new FhirJsonParsingSettings() { PermissiveParsing = permissiveParsing };
        }

        /// <summary>Create a new <see cref="NavigatorStreamFactory"/> instance for the specified parser configuration settings.</summary>
        /// <param name="xmlParsingSettings">Configuration settings that control the behavior of the internal XML parser.</param>
        /// <param name="jsonParsingSettings">Configuration settings that control the behavior of the internal JSON parser.</param>
        public NavigatorStreamFactory(FhirXmlParsingSettings xmlParsingSettings, FhirJsonParsingSettings jsonParsingSettings)
        {
            XmlParserSettings = xmlParsingSettings;
            JsonParserSettings = jsonParsingSettings;
        }

        /// <summary>Determines if disposing the <see cref="INavigatorStream"/> should also dispose the internal <see cref="Stream"/> instance.</summary>
        public bool DisposeStream { get; set; } = true;

        // [WMR 20181026] EXPERIMENTAL

        // <summary>
        // Gets or sets a boolean value that controls behavior of the <see cref="Create(string)"/>
        // methods when encountering an unsupported file/serialization format.
        // <para>
        // If <c>true</c>, throw a <see cref="NotSupportedException"/>, otherwise (<c>false</c>) return <c>null</c>.
        // </para>
        // </summary>
        // public bool ThrowOnUnsupportedFormat { get; set; } = false;

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the XML parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirXmlParsingSettings"/> instance.</value>
        public FhirXmlParsingSettings XmlParserSettings
        {
            get => _xmlParsingSettings;
            set => _xmlParsingSettings = value ?? FhirXmlParsingSettings.CreateDefault();
        }

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the JSON parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirJsonParsingSettings"/> instance.</value>
        public FhirJsonParsingSettings JsonParserSettings
        {
            get => _jsonParsingSettings;
            set => _jsonParsingSettings = value ?? FhirJsonParsingSettings.CreateDefault();
        }

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource stream, independent of the serialization format,
        /// using the current <see cref="XmlParserSettings"/> or <see cref="JsonParserSettings"/>.
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
                    return new XmlNavigatorStream(stream, disposeStream, XmlParserSettings);
                case FhirSerializationFormats.Json:
                    return new JsonNavigatorStream(stream, disposeStream, JsonParserSettings);
                default:
                    //if (ThrowOnUnsupportedFormat)
                    //{
                    //    throw Error.NotSupported($"Unsupported FHIR serialization format ('{format}').");
                    //}
                    return null;
            }
        }

        /// <summary>
        /// Creates a new <see cref="INavigatorStream"/> instance to access the contents of a
        /// serialized resource, independent of the underlying resource serialization format,
        /// using the current <see cref="XmlParserSettings"/> or <see cref="JsonParserSettings"/>.
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
                return XmlNavigatorStream.FromPath(path, disposeStream, XmlParserSettings);
            }
            if (FhirFileFormats.HasJsonExtension(path))
            {
                return JsonNavigatorStream.FromPath(path, disposeStream, JsonParserSettings);
            }

            // Unsupported extension
            //if (ThrowOnUnsupportedFormat)
            //{
            //    throw Error.NotSupported($"Unsupported file format ('{path}').");
            //}

            return null;
        }
    }

    /// <summary>
    /// Static default factory to create new <see cref="INavigatorStream"/> instances for navigating
    /// serialized resources, independent of the underlying resource serialization format.
    /// <para>
    /// Uses the default configuration settings for the internal XML and JSON parsers.
    /// Alternatively, use the <see cref="NavigatorStreamFactory"/> class to specify custom parser settings.
    /// </para>
    /// </summary>
    /// <remarks>Supports FHIR resource files with ".xml" and ".json" extensions.</remarks>
    /// <seealso cref="NavigatorStreamFactory"/>
    public class DefaultNavigatorStreamFactory
    {
        private static readonly NavigatorStreamFactory _factory = NavigatorStreamFactory.CreateDefault();

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
            => NavigatorStreamFactory.GetSerializationFormat(path);

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



}

#endif
