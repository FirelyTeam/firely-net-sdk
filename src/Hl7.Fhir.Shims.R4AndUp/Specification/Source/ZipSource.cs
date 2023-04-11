#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using File = System.IO.File;

namespace Hl7.Fhir.Specification.Source
{
    public class ZipSource : CommonZipSource, IConformanceSource
    {
        /// <inheritdoc cref="ZipSource(string, DirectorySourceSettings)"/>
        public ZipSource(string zipPath) : this(zipPath, DirectorySourceSettings.CreateDefault())
        {
            // Nothing
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        /// <param name="settings">Configuration settings for the internal <see cref="DirectorySource"/> instance.</param>
        public ZipSource(string zipPath, DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, zipPath, CACHEDIRPATH, settings)
        {
            // Nothing
        }

        /// <inheritdoc cref="ZipSource(string, string, DirectorySourceSettings)"/>
        public ZipSource(string zipPath, string extractionDirectory) :
            base(ModelInfo.ModelInspector, zipPath, extractionDirectory, DirectorySourceSettings.CreateDefault())
        {
            // Nothing
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance for the ZIP archive with the specified file path.</summary>
        /// <param name="zipPath">File path to a ZIP archive.</param>
        /// <param name="extractionDirectory">The full path of the directory where the zip file will be extracted.</param>
        /// <param name="settings">Configuration settings for the internal <see cref="DirectorySource"/> instance.</param>
        public ZipSource(string zipPath, string extractionDirectory, DirectorySourceSettings settings) :
            base(ModelInfo.ModelInspector, zipPath, extractionDirectory, settings)
        {
            // Nothing
        }

        private static readonly string CACHEHINT = ZipCacher.BuildDefaultCacheDirectoryName(typeof(ZipSource).Assembly);
        private static readonly string CACHEDIRPATH = Path.Combine(Path.GetTempPath(), CACHEHINT);

        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip"
        /// found in the path passed to this function.</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        /// <param name="path">A path to a directory containing the specification.zip file.</param>
        public static ZipSource CreateValidationSource(string path)
        {
            return !File.Exists(path)
                ? throw new FileNotFoundException($"Cannot create a {nameof(CommonZipSource)} for the core specification: '{SpecificationZipFileName}' was not found.")
                : new ZipSource(path, new DirectorySourceSettings { IncludeSubDirectories = true });
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip".</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        public static ZipSource CreateValidationSource()
        {
            var path = Path.Combine(CommonDirectorySource.SpecificationDirectory, SpecificationZipFileName);
            return CreateValidationSource(path);
        }

        #region IConformanceSource
        // Obsoleted since 2022-11-21, EK
        [Obsolete("ZipSource itself implements IConformanceSource, use that implementation instead of this property.")]
        public IConformanceSource Source => this;


        public IEnumerable<string> ListResourceUris(ResourceType? filter = null) =>
            FileSource.ListResourceUris(filter?.GetLiteral());
        public IEnumerable<ConceptMap> FindConceptMaps(string? sourceUri = null, string? targetUri = null) =>
            FileSource.FindConceptMaps<ConceptMap>(sourceUri, targetUri);
        public NamingSystem? FindNamingSystem(string uniqueId) => FileSource.FindNamingSystem<NamingSystem>(uniqueId);
        #endregion
    }
}
#nullable restore