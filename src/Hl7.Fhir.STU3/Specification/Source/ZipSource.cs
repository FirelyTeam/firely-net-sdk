/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from a ZIP archive. Thread-safe.</summary>
    /// <remarks>Extracts the ZIP archive to a temporary folder and delegates to the <see cref="DirectorySource"/>.</remarks>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ZipSource : ISummarySource, IConformanceSource, IArtifactSource, IResourceResolver, IAsyncResourceResolver
    {
#pragma warning disable IDE1006 // Naming Styles - cannot fix because of bw-compatibility
        public const string SpecificationZipFileName = "specification.zip";
#pragma warning restore IDE1006 // Naming Styles
        private static readonly string CACHEHINT = ZipCacher.BuildDefaultCacheDirectoryName(typeof(ZipSource).Assembly);
        private static readonly string CACHEDIRPATH = Path.Combine(Path.GetTempPath(), CACHEHINT);


        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip"
        /// found in the path passed to this function.</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        /// <param name="path">A path to a directory containing the specification.zip file.</param>
        public static ZipSource CreateValidationSource(string path)
        {
            return !File.Exists(path)
                ? throw new FileNotFoundException($"Cannot create a {nameof(ZipSource)} for the core specification: '{SpecificationZipFileName}' was not found.")
                : new ZipSource(path);
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip".</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        public static ZipSource CreateValidationSource()
        {
            var path = Path.Combine(DirectorySource.SpecificationDirectory, SpecificationZipFileName);
            return CreateValidationSource(path);
        }

        private string _mask;
        // [WMR 20171102] Use Lazy<T> for thread-safe initialization
        private readonly Lazy<DirectorySource> _lazySource;
        private readonly DirectorySourceSettings _settings;

        /// <summary>Create a new <see cref="ZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        public ZipSource(string zipPath) : this(zipPath, DirectorySourceSettings.CreateDefault()) { }

        /// <summary>Create a new <see cref="ZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        /// <param name="settings">Configuration settings for the internal <see cref="DirectorySource"/> instance.</param>
        public ZipSource(string zipPath, DirectorySourceSettings settings) : this(zipPath, CACHEDIRPATH, settings)
        {
            // Nothing
        }

        /// <inheritdoc cref="ZipSource(string, string, DirectorySourceSettings)"/>
        public ZipSource(string zipPath, string extractionDirectory) :
            this(zipPath, extractionDirectory, DirectorySourceSettings.CreateDefault())
        {
            // Nothing
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        /// <param name="extractionDirectory">The full path of the directory where the zip file will be extracted.</param>
        /// <param name="settings">Configuration settings for the internal <see cref="DirectorySource"/> instance.</param>
        public ZipSource(string zipPath, string extractionDirectory, DirectorySourceSettings settings)
        {
            if (string.IsNullOrEmpty(zipPath)) { throw Error.ArgumentNull(nameof(zipPath)); }
            ZipPath = zipPath;
            CacheDirectory = extractionDirectory;
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            // Always clone the incoming reference, especially since we're forcing IncludeSubDirectories
            _settings = settings.Clone();
            _lazySource = new Lazy<DirectorySource>(createSource);
        }

        /// <summary>Gets the location of the ZIP archive, as specified in the constructor.</summary>
        public string ZipPath { get; }

        /// <summary>
        /// Gets the location of the directory where the zip archive will be extracted and cached.
        /// </summary>
        public string CacheDirectory { get; }

        /// <summary>Determines if the <see cref="ZipSource"/> has already extracted the contents of the specified ZIP archive.</summary>
        /// <remarks>The <see cref="ZipSource"/> extracts the contents of the ZIP archive on demand.</remarks>
        public bool IsPrepared => _lazySource.IsValueCreated;

        /// <summary>Gets the location of the ZIP archive extraction folder (if prepared), or <c>null</c> otherwise.</summary>
        /// <remarks>
        /// The <see cref="ZipSource"/> extracts the contents of the ZIP archive on demand.
        /// If <see cref="IsPrepared"/> equals <c>false</c>, then the extraction folder is not yet known
        /// and <see cref="ExtractPath"/> returns <c>null</c>.
        /// </remarks>
        public string ExtractPath => _lazySource.IsValueCreated
            ? _lazySource.Value?.ContentDirectory
            : null;

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the ZIP archive.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// </summary>
        public string Mask
        {
            get { return _mask; }
            set
            {
                _mask = value;
                // No need to lock, DirectorySource is synchronized
                var source = _lazySource.Value;
                if (source != null)
                {
                    source.Mask = Mask;
                }
                // Otherwise, CreateSource will assign the specified mask
            }
        }

        /// <summary>Returns a reference to the internal <see cref="IConformanceSource"/> that exposes the contents of the ZIP archive.</summary>
        // Obsoleted since 2022-11-21, EK
        [Obsolete("ZipSource itself implements IConformanceSource, use that implementation instead of this property.")]
        public IConformanceSource Source => this;

        /// <summary>Returns a reference to the internal <see cref="DirectorySource"/> that exposes the contents of the ZIP archive.</summary>
        protected DirectorySource FileSource => _lazySource.Value;

        /// <summary>Extract the contents of the specified ZIP archive.</summary>
        /// <remarks>The <see cref="ZipSource"/> automatically unpacks the ZIP archive on demand.</remarks>
        public void Prepare()
        {
            // Access the Lazy<T>.Value property to force creation
            // Evaluate the result to prevent compiler optimization in RELEASE build
            // Should never throw...
            if (_lazySource.Value == null) { throw new InvalidOperationException(); };
        }

        #region IArtifactSource

        /// <summary>Gets a list of artifact filenames.</summary>
        public IEnumerable<string> ListArtifactNames() => FileSource.ListArtifactNames();

        /// <summary>Load the artifact with the specified filename.</summary>
        /// <param name="name">The filename of the artifact.</param>
        public Stream LoadArtifactByName(string name) => FileSource.LoadArtifactByName(name);

        #endregion

        #region IConformanceSource

        /// <summary>List all resource uris, optionally filtered by type.</summary>
        /// <param name="filter">A <see cref="ResourceType"/> enum value.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of uri strings.</returns>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = default)
            => FileSource.ListResourceUris(filter);

        /// <summary>
        /// Find a <see cref="CodeSystem"/> resource by a <see cref="ValueSet"/> canonical url that contains all codes from that codesystem.
        /// </summary>
        /// <param name="valueSetUri">The canonical uri of a <see cref="ValueSet"/> resource.</param>
        /// <returns>A <see cref="CodeSystem"/> resource, or <c>null</c>.</returns>
        /// <remarks>
        /// It is very common for valuesets to represent all codes from a specific/smaller code system.
        /// These are indicated by he CodeSystem.valueSet element, which is searched here.
        /// </remarks>
        public CodeSystem FindCodeSystemByValueSet(string valueSetUri) => FileSource.FindCodeSystemByValueSet(valueSetUri);

        /// <summary>Find <see cref="ConceptMap"/> resources which map from the given source to the given target.</summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns>A sequence of <see cref="ConceptMap"/> resources.</returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => FileSource.FindConceptMaps(sourceUri, targetUri);

        /// <summary>Finds a <see cref="NamingSystem"/> resource by matching any of a system's UniqueIds.</summary>
        /// <param name="uniqueId">The unique id of a <see cref="NamingSystem"/> resource.</param>
        /// <returns>A <see cref="NamingSystem"/> resource, or <c>null</c>.</returns>
        public NamingSystem FindNamingSystem(string uniqueId) => FileSource.FindNamingSystem(uniqueId);

        #endregion

        #region ISummarySource

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with key information about each FHIR artifact provided by the source.</summary>
        public IEnumerable<ArtifactSummary> ListSummaries() => FileSource.ListSummaries();

        /// <summary>
        /// Load the resource from which the specified summary was generated.
        /// <para>
        /// The internal <see cref="DirectorySource"/> instance annotates returned resource instances
        /// with an <seealso cref="OriginAnnotation"/> that captures the value of the
        /// <see cref="ArtifactSummary.Origin"/> property.
        /// The <seealso cref="OriginAnnotationExtensions.GetOrigin(Resource)"/> extension method 
        /// provides access to the annotated location.
        /// </para>
        /// </summary>
        /// <param name="summary">An <see cref="ArtifactSummary"/> instance generated by this source.</param>
        /// <returns>A new <see cref="Resource"/> instance, or <c>null</c>.</returns>
        /// <remarks>
        /// The <see cref="ArtifactSummary.Origin"/> and <see cref="ArtifactSummary.Position"/>
        /// summary properties allow the source to identify and resolve the artifact.
        /// </remarks>
        public Resource LoadBySummary(ArtifactSummary summary) => FileSource.LoadBySummary(summary);

        #endregion

        #region IResourceResolver

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        public Resource ResolveByUri(string uri) => TryResolveByUri(uri).Value;

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        public Resource ResolveByCanonicalUri(string uri) => TryResolveByCanonicalUri(uri).Value;

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        public ResolverResult TryResolveByUri(string uri) => FileSource.TryResolveByUri(uri);
        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        public ResolverResult TryResolveByCanonicalUri(string uri) => FileSource.TryResolveByCanonicalUri(uri);

        public async Task<Resource> ResolveByUriAsync(string uri)
        {
            var result = await TryResolveByUriAsync(uri).ConfigureAwait(false);
            return result.Value;
        }
        public async Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            var result = await TryResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
            return result.Value;
        }
        
        /// <inheritdoc/>
        public Task<ResolverResult> TryResolveByUriAsync(string uri) => FileSource.TryResolveByUriAsync(uri);
        /// <inheritdoc/>
        public Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri) => FileSource.TryResolveByCanonicalUriAsync(uri);

        #endregion

        /// <summary>
        /// Unpacks the zip-file and constructs a new FileArtifactSource on the unzipped directory
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        private DirectorySource createSource()
        {
            if (!File.Exists(ZipPath))
            {
                throw new FileNotFoundException($"Cannot prepare {nameof(ZipSource)}: file '{ZipPath}' was not found");
            }

            var zc = new ZipCacher(ZipPath, CacheDirectory);
            var source = new DirectorySource(zc.GetContentDirectory(), _settings);

            var mask = Mask;
            if (!string.IsNullOrEmpty(mask))
            {
                source.Mask = mask;
            }
            return source;
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal virtual string DebuggerDisplay
            => $"{GetType().Name} for '{ZipPath}'"
            + (IsPrepared ? $" | Extracted to '{ExtractPath}'" : null);

    }
}
