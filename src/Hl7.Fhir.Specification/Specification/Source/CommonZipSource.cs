/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from a ZIP archive. Thread-safe.</summary>
    /// <remarks>Extracts the ZIP archive to a temporary folder and delegates to the <see cref="CommonDirectorySource"/>.</remarks>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class CommonZipSource : ISummarySource, ICommonConformanceSource, IArtifactSource, IResourceResolver, IAsyncResourceResolver
    {
        public const string SpecificationZipFileName = "specification.zip";

        /// <summary>
        /// Factory method to create a specific DirectorySource
        /// </summary>
        protected Func<ModelInspector, string, DirectorySourceSettings, CommonDirectorySource> directorySourceFactory = (inspector, contentDirectory, settings) => new CommonDirectorySource(inspector, contentDirectory, settings);

        private string _mask;
        // [WMR 20171102] Use Lazy<T> for thread-safe initialization
        private readonly Lazy<CommonDirectorySource> _lazySource;
        private readonly DirectorySourceSettings _settings;
        private readonly ModelInspector _inspector;

        /// <summary>Create a new <see cref="CommonZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="inspector"></param>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        public CommonZipSource(ModelInspector inspector, string zipPath) : this(inspector, zipPath, DirectorySourceSettings.CreateDefault()) { }

        /// <summary>Create a new <see cref="CommonZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="inspector"></param>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        /// <param name="settings">Configuration settings for the internal <see cref="CommonDirectorySource"/> instance.</param>
        public CommonZipSource(ModelInspector inspector, string zipPath, DirectorySourceSettings settings)
        {
            if (string.IsNullOrEmpty(zipPath)) { throw Error.ArgumentNull(nameof(zipPath)); }
            _inspector = inspector;
            ZipPath = zipPath;
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            // Always clone the incoming reference, especially since we're forcing IncludeSubDirectories
            _settings = settings.Clone();
            _lazySource = new Lazy<CommonDirectorySource>(createSource);
        }

        /// <summary>Gets the location of the ZIP archive, as specified in the constructor.</summary>
        public string ZipPath { get; }

        /// <summary>Determines if the <see cref="CommonZipSource"/> has already extracted the contents of the specified ZIP archive.</summary>
        /// <remarks>The <see cref="CommonZipSource"/> extracts the contents of the ZIP archive on demand.</remarks>
        public bool IsPrepared => _lazySource.IsValueCreated;

        /// <summary>Gets the location of the ZIP archive extraction folder (if prepared), or <c>null</c> otherwise.</summary>
        /// <remarks>
        /// The <see cref="CommonZipSource"/> extracts the contents of the ZIP archive on demand.
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

        /// <summary>Returns a reference to the internal <see cref="CommonDirectorySource"/> that exposes the contents of the ZIP archive.</summary>
        protected CommonDirectorySource FileSource => _lazySource.Value;

        /// <summary>Extract the contents of the specified ZIP archive.</summary>
        /// <remarks>The <see cref="CommonZipSource"/> automatically unpacks the ZIP archive on demand.</remarks>
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

        #region ICommonConformanceSource
        /// <inheritdoc/>
        public CodeSystem FindCodeSystemByValueSet(string valueSetUri) => FileSource.FindCodeSystemByValueSet(valueSetUri);
        #endregion

        #region ISummarySource

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with key information about each FHIR artifact provided by the source.</summary>
        public IEnumerable<ArtifactSummary> ListSummaries() => FileSource.ListSummaries();

        /// <summary>
        /// Load the resource from which the specified summary was generated.
        /// <para>
        /// The internal <see cref="CommonDirectorySource"/> instance annotates returned resource instances
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
        public Resource ResolveByUri(string uri) => FileSource.ResolveByUri(uri);

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        public Resource ResolveByCanonicalUri(string uri) => FileSource.ResolveByCanonicalUri(uri);

        public Task<Resource> ResolveByUriAsync(string uri) => FileSource.ResolveByUriAsync(uri);
        public Task<Resource> ResolveByCanonicalUriAsync(string uri) => FileSource.ResolveByCanonicalUriAsync(uri);

        #endregion

        /// <summary>
        /// Unpacks the zip-file and constructs a new FileArtifactSource on the unzipped directory
        /// </summary>
        /// <remarks>This is an expensive operations and should be run once. As well, it unpacks files on the
        /// file system and is not thread-safe.</remarks>
        private CommonDirectorySource createSource()
        {
            if (!File.Exists(ZipPath))
            {
                throw new FileNotFoundException($"Cannot prepare {nameof(CommonZipSource)}: file '{ZipPath}' was not found");
            }

            var zc = new ZipCacher(ZipPath, GetCacheKey());
            var source = directorySourceFactory(_inspector, zc.GetContentDirectory(), _settings);

            var mask = Mask;
            if (!string.IsNullOrEmpty(mask))
            {
                source.Mask = mask;
            }
            return source;
        }

        private string GetCacheKey()
        {
            Assembly assembly = typeof(CommonZipSource).GetTypeInfo().Assembly;
            var versionInfo = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var productInfo = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return $"FhirArtifactCache-{versionInfo.InformationalVersion}-{productInfo.Product}";
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name} for '{ZipPath}'"
            + (IsPrepared ? $" | Extracted to '{ExtractPath}'" : null);

    }
}
