using Hl7.Fhir.Model;
using System.IO;

namespace Hl7.Fhir.Specification.Source
{
    public class ZipSource : CommonZipSource
    {
        public ZipSource(string zipPath) : base(ModelInfo.ModelInspector, zipPath)
        {
        }

        public ZipSource(string zipPath, DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, zipPath, settings)
        {
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


    }
}