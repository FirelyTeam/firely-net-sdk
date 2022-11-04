using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.IO;

namespace Hl7.Fhir.Specification.Source
{
    public class ZipSource : CommonZipSource
    {
        public ZipSource(string zipPath) : base(ModelInfo.ModelInspector, zipPath)
        {
            directorySourceFactory = (inspector, contentDirectory, settings) => new DirectorySource(contentDirectory, settings);
        }

        public ZipSource(string zipPath, DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, zipPath, settings)
        {
            directorySourceFactory = (inspector, contentDirectory, settings) => new DirectorySource(contentDirectory, settings);
        }

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

        /// <summary>Find <see cref="ConceptMap"/> resources which map from the given source to the given target.</summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns>A sequence of <see cref="ConceptMap"/> resources.</returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        public new IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => DirectorySource.FindConceptMaps(sourceUri, targetUri);

        /// <summary>Finds a <see cref="NamingSystem"/> resource by matching any of a system's UniqueIds.</summary>
        /// <param name="uniqueId">The unique id of a <see cref="NamingSystem"/> resource.</param>
        /// <returns>A <see cref="NamingSystem"/> resource, or <c>null</c>.</returns>
        public new NamingSystem FindNamingSystem(string uniqueId) => DirectorySource.FindNamingSystem(uniqueId);
    }
}