/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.Model;
using Errors = Hl7.Fhir.Utility.Error;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using Hl7.Fhir.Serialization;
using System.IO;

namespace Hl7.Fhir.Specification.Source.Summary
{
    // Note:
    // 1. ArtifactSummaryGenerator creates new (mutable) ArtifactSummaryPropertyBag
    // 2. ArtifactSummaryGenerator calls the available ArtifactSummaryHarvester delegates
    //    to harvest artifact summary information and add it to the property bag
    // 3. ArtifactSummaryGenerator creates a new (read-only) ArtifactSummary instance
    //    from the initialized property bag and returns it to the caller.
    //    This way, harvested property values are protected against modification by callers.

    /// <summary>Represents summary information harvested from a FHIR artifact.</summary>
    /// <remarks>
    /// Created by the <see cref="ArtifactSummaryGenerator"/> class from
    /// a property bag containing the harvested summary information.
    /// <para>
    /// Implements the <see cref="IArtifactSummaryPropertyBag"/> interface
    /// to support extension methods for accessing specific harvested properties.
    /// </para>
    /// </remarks>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ArtifactSummary : IArtifactSummaryPropertyBag
    {
        // Note: omit leading underscore to be CLS compliant
        protected readonly IArtifactSummaryPropertyBag properties;

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from the specified exception.</summary>
        /// <param name="error">An exception that occured while harvesting artifact summary information.</param>
        public static ArtifactSummary FromException(Exception error) => FromException(error, null);

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from the specified exception.</summary>
        /// <param name="error">An exception that occured while harvesting artifact summary information.</param>
        /// <param name="origin">The original location of the target artifact.</param>
        public static ArtifactSummary FromException(Exception error, string origin)
        {
            if (error == null) { throw Errors.ArgumentNull(nameof(error)); }
            // Create a new (default) ArtifactSummaryPropertyBag to store the artifact origin
            var properties = new ArtifactSummaryPropertyBag();
            if (!string.IsNullOrEmpty(origin))
            {
                properties[ArtifactSummaryProperties.OriginKey] = origin;
            }
            return new ArtifactSummary(properties, error);
        }

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from a set of harvested artifact summary properties.</summary>
        /// <param name="properties">A property bag with harvested artifact summary information.</param>
        public ArtifactSummary(IArtifactSummaryPropertyBag properties) : this(properties, null) { }

        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> instance from a set of harvested artifact summary properties
        /// and a runtime exception that occured during harvesting.
        /// </summary>
        public ArtifactSummary(IArtifactSummaryPropertyBag properties, Exception error)
        {
            this.properties = properties ?? throw Errors.ArgumentNull(nameof(properties));
            this.Error = error;

            // Try to parse the specified type name to a known enum value
            var typeName = ResourceTypeName;
            if (typeName != null)
            {
                ResourceType = ModelInfo.FhirTypeNameToResourceType(ResourceTypeName);
            }
        }

        /// <summary>Returns information about errors that occured while generating the artifact summary.</summary>
        public Exception Error { get; }

        /// <summary>Indicates if any errors occured while generating the artifact summary.</summary>
        /// <remarks>If <c>true</c>, then the <see cref="Error"/> property returns detailed error information.</remarks>
        public bool IsFaulted => Error != null; // cf. Task

        /// <summary>Gets the original location of the associated artifact.</summary>
        public string Origin => properties.GetOrigin();

        /// <summary>Gets the size of the original artifact file.</summary>
        public long? FileSize => properties.GetFileSize();

        /// <summary>Gets the last modified date of the original artifact file.</summary>
        public DateTime? LastModified => properties.GetLastModified();

        /// <summary>
        /// Get a string value that represents the artifact serialization format,
        /// as defined by the <see cref="FhirSerializationFormats"/> class, if available.
        /// </summary>
        public string SerializationFormat => properties.GetSerializationFormat();

        /// <summary>
        /// Gets an opaque value that represents the position of the artifact within the container.
        /// Allows the <see cref="DirectorySource"/> to retrieve and deserialize the associated artifact.
        /// </summary>
        public string Position => properties.GetPosition();

        /// <summary>Gets the type name of the resource.</summary>
        public string ResourceTypeName => properties.GetTypeName();

        /// <summary>Gets the type of the resource, parsed from the original <see cref="ResourceTypeName"/> value, or <c>null</c>.</summary>
        public ResourceType? ResourceType { get; }

        /// <summary>Gets the resource uri.</summary>
        /// <remarks>The <see cref="DirectorySource"/> generates virtual uri values for resources that are not bundle entries.</remarks>
        public string ResourceUri => properties.GetResourceUri();

        #region IEnumerable

        /// <summary>Returns an enumerator that iterates through the summary properties.</summary>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => properties.GetEnumerator();

        #endregion

        #region IReadOnlyCollection

        /// <summary>Gets the number of harvested property values.</summary>
        public int Count => properties.Count;

        #endregion

        #region IReadOnlyDictionary

        /// <summary>Gets the property value associated with the specified property key.</summary>
        public object this[string key] => properties[key];

        /// <summary>Gets a collection of property keys.</summary>
        public IEnumerable<string> Keys => properties.Keys;

        /// <summary>Gets a collection of property values.</summary>
        public IEnumerable<object> Values => properties.Values;

        /// <summary>Determines wether the summary contains a property value for the specified property key.</summary>
        public bool ContainsKey(string key) => properties.ContainsKey(key);

        /// <summary>Gets the property value associated with the specified property key.</summary>
        public bool TryGetValue(string key, out object value) => properties.TryGetValue(key, out value);

        #endregion

        /// <summary>
        /// Try to deserialize the target artifact described by an <see cref="ArtifactSummary"/> instance from disk.
        /// <para>
        /// Loads resources from disk, based on summary information generated by the
        /// <see cref="ArtifactSummaryGenerator.Generate(string, ArtifactSummaryHarvester[])"/> method.
        /// Alternatively, the <see cref="LoadResource{T}(Stream)"/> method supports loading resources from a stream.
        /// </para>
        /// </summary> 
        /// <typeparam name="T">The resource type to return.</typeparam>
        /// <returns>A new resource instance of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        /// <remarks>
        /// This method annotates returned resource instances with an <seealso cref="OriginAnnotation"/>
        /// that captures the value of the <see cref="Origin"/> property.
        /// The <seealso cref="OriginAnnotationExtensions.GetOrigin(Resource)"/> extension method 
        /// provides access to the annotated location.
        /// </remarks>
        /// <seealso cref="LoadResource{T}(Stream)"/>
        public T LoadResource<T>() where T : Resource
        {
            // File path of the containing resource file (could be a Bundle)


            // [WMR 20180419] TODO: Also support non-file based input streams (Generate from stream)
            // - Implement ctor to inject a factory method for creating a navigator from (abstract) origin information

            var origin = Origin;
            if (string.IsNullOrEmpty(origin))
            {
                throw Errors.NotSupported("Cannot load resource from summary. The Origin property value is empty or missing.");
            }

            var pos = Position;
            if (string.IsNullOrEmpty(pos))
            {
                throw Errors.NotSupported("Cannot load resource from summary. The Position property value is empty or missing.");
            }

            T result = null;
            using (var navStream = DefaultNavigatorStreamFactory.Create(origin))
            {

                // Handle exceptions & null return values?
                // e.g. file may have been deleted/renamed since last scan

                // Advance stream to the target resource (e.g. specific Bundle entry)
                if (navStream != null && navStream.Seek(pos))
                {
                    // Create navigator for the target resource
                    var nav = navStream.Current;
                    if (nav != null)
                    {
                        // Parse target resource from navigator
                        var parser = new BaseFhirParser();
                        result = parser.Parse<T>(nav);
                        // Add origin annotation
                        result?.SetOrigin(origin);
                    }
                }
            }

            return result;
        }

        // cf. ArtifactSummaryGenerator.Generate(Stream stream, ...)

        /// <summary>
        /// Try to deserialize the target artifact described by an <see cref="ArtifactSummary"/> instance
        /// from the specified input stream.
        /// <para>
        /// Loads resources from the specified stream, based on summary information generated by the
        /// <see cref="ArtifactSummaryGenerator.Generate(Stream, string, ArtifactSummaryHarvester[])"/> method.
        /// Alternatively, the <see cref="LoadResource{T}()"/> method supports loading resources from disk.
        /// </para>
        /// </summary>
        /// <param name="stream">An readable resource stream.</param>
        /// <typeparam name="T">The resource type to return.</typeparam>
        /// <returns>A new resource instance of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        public T LoadResource<T>(Stream stream) where T : Resource
        {
            if (stream == null) { throw Errors.ArgumentNull(nameof(stream)); }

            var format = SerializationFormat;
            if (string.IsNullOrEmpty(format))
            {
                throw Errors.NotSupported("Cannot load resource from summary. The SerializationFormat property value is empty or missing.");
            }

            var pos = Position;
            if (string.IsNullOrEmpty(pos))
            {
                throw Errors.NotSupported("Cannot load resource from summary. The Position property value is empty or missing.");
            }

            T result = null;

            using (var navStream = DefaultNavigatorStreamFactory.Create(stream, SerializationFormat))
            {
                // return loadResourceFromNavigatorStream<T>(navStream, Origin, pos);

                // Advance stream to the target resource (e.g. specific Bundle entry)
                if (navStream != null && navStream.MoveNext(pos))
                {
                    // Create navigator for the target resource
                    var nav = navStream.Current;
                    if (nav != null)
                    {
                        // Parse target resource from navigator
                        var parser = new BaseFhirParser();
                        result = parser.Parse<T>(nav);
                        // Add origin annotation
                        // result?.SetOrigin(origin);
                    }
                }
            }

            return result;
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual string DebuggerDisplay
            => $"{GetType().Name} for {ResourceTypeName} | Origin: {Origin}"
             + (IsFaulted ? " | Error: " + Error.Message : string.Empty);
    }
}

#endif
