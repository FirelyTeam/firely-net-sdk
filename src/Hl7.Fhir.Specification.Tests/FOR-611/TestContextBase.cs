using FluentAssertions;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using System;
using System.IO;
using System.IO.Compression;

namespace Hl7.Fhir.Specification.Tests
{
    public class TestContextBase : IDisposable
    {
        private readonly string _tempPath;

        public IResourceResolver Resolver { get; }

        public TestContextBase(string sourceFolder)
        {
            _tempPath = Path.Combine(Path.GetTempPath(), sourceFolder);

            if (Directory.Exists(_tempPath))
                Directory.Delete(_tempPath, true);

            ZipFile.ExtractToDirectory($"{sourceFolder}\\Resources.zip", _tempPath);

            var parserSettings = ParserSettings.CreateDefault();

            parserSettings.AcceptUnknownMembers = true;
            parserSettings.AllowUnrecognizedEnums = true;

            var dirSource = new DirectorySource(_tempPath, new DirectorySourceSettings { IncludeSubDirectories = true, ParserSettings = parserSettings });
            var zipSource = ZipSource.CreateValidationSource();

            Resolver = new CachedResolver(new MultiResolver(zipSource, dirSource));
        }

        public T GetResource<T>(string url) where T : class
        {
            var resource = Resolver.ResolveByCanonicalUri(url) as T;

            resource.Should().NotBeNull();

            return resource;
        }

        public virtual void Dispose()
        {
            Directory.Delete(_tempPath, true);
        }
    }
}
