using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    internal class TestContextBase : IDisposable
    {
        private readonly string _tempPath;

        public IResourceResolver Resolver { get; }

        public TestContextBase(string id) : this(id, null)
        {
        }

        public TestContextBase(string id, string category)
        {
            _tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

            category ??= "snapshot-test";

            ZipFile.ExtractToDirectory($"TestData\\{category}\\{id}\\Resources.zip", _tempPath);

            var directorySource = new DirectorySource(_tempPath);
            var zipSource = ZipSource.CreateValidationSource();

            Resolver = new CachedResolver(new MultiResolver(zipSource, directorySource));
        }

        public T GetResource<T>(string url) where T : class
        {
            var resource = Resolver.ResolveByCanonicalUri(url) as T;

            resource.Should().NotBeNull();

            return resource;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ElementDefinition GetElement(IEnumerable<ElementDefinition> elements, string elementId)
        {
            return elements.SingleOrDefault(x => x.ElementId == elementId);
        }

        public int GetElementIndex(IList<ElementDefinition> elements, string elementId)
        {
            var element = GetElement(elements, elementId);
            return element == null ? -1 : elements.IndexOf(element);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Directory.Delete(_tempPath, true);
        }
    }
}
