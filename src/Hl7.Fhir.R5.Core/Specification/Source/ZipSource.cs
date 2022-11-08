using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.IO;
using File = System.IO.File;

namespace Hl7.Fhir.Specification.Source
{
    public class ZipSource : CommonZipSource, IConformanceSource
    {
        public ZipSource(string zipPath) : base(ModelInfo.ModelInspector, zipPath)
        {
            directorySourceFactory = (inspector, contentDirectory, settings) => new DirectorySource(contentDirectory, settings);
        }

        public ZipSource(string zipPath, DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, zipPath, settings)
        {
            directorySourceFactory = (inspector, contentDirectory, settings) => new DirectorySource(contentDirectory, settings);
        }

        /// <summary>Returns a reference to the internal <see cref="IConformanceSource"/> that exposes the contents of the ZIP archive.</summary>
        public IConformanceSource Source => DirectorySource;

        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip"
        /// found in the path passed to this function.</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        /// <param name="path">A path to a directory containing the specification.zip file.</param>
        public static ZipSource CreateValidationSource(string path)
        {
            return !File.Exists(path)
                ? throw new FileNotFoundException($"Cannot create a {nameof(CommonZipSource)} for the core specification: '{SpecificationZipFileName}' was not found.")
                : new ZipSource(path);
        }

        /// <summary>Create a new <see cref="ZipSource"/> instance to read FHIR artifacts from the core specification archive "specification.zip".</summary>
        /// <returns>A new <see cref="ZipSource"/> instance.</returns>
        public static ZipSource CreateValidationSource()
        {
            var path = Path.Combine(CommonDirectorySource.SpecificationDirectory, SpecificationZipFileName);
            return CreateValidationSource(path);
        }

        private DirectorySource DirectorySource => FileSource as DirectorySource;

        #region IConformanceSource
        /// <inheritdoc/>
        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => DirectorySource.FindConceptMaps(sourceUri, targetUri);

        /// <inheritdoc/>
        public NamingSystem FindNamingSystem(string uniqueId) => DirectorySource.FindNamingSystem(uniqueId);

        /// <inheritdoc/>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = default) => DirectorySource.ListResourceUris(filter);
        #endregion
    }
}