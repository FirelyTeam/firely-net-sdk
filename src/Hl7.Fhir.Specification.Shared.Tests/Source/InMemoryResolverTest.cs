using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class InMemoryResourceResolverTest
    {
        private StructureDefinition profile1 => new() { Id = "profile1", Url = "http://fire.ly/StructureDefinition/profile1" };
        private StructureDefinition profile2 => new() { Id = "profile2", Url = "http://fire.ly/StructureDefinition/profile2" };
        private StructureDefinition profile3 => new() { Id = null, Url = "http://fire.ly/StructureDefinition/profile3" };
        private Patient patient1 => new() { Id = "123" };


        [TestMethod]
        public async System.Threading.Tasks.Task TestResolverByCanonicalUrl()
        {
            var resolver = new InMemoryResourceResolver(profile1, profile2, profile3, patient1);

            var resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile1");
            resource.Should().NotBeNull();
            resource.Id.Should().Be("profile1");

            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile2");
            resource.Should().NotBeNull();
            resource.Id.Should().Be("profile2");

            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile3");
            resource.Should().NotBeNull();
            resource.Id.Should().BeNull();
            ((IConformanceResource)resource).Url.Should().Be("http://fire.ly/StructureDefinition/profile3");

            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/non-existing-url");
            resource.Should().BeNull();

            resource = await resolver.ResolveByCanonicalUriAsync("Patient/123");
            resource.Should().BeNull();
        }


        [TestMethod]
        public async System.Threading.Tasks.Task TestResolverByUri()
        {
            var resolver = new InMemoryResourceResolver(profile1, profile2, profile3, patient1);

            var resource = await resolver.ResolveByUriAsync("StructureDefinition/profile1");
            resource.Should().NotBeNull();
            ((IConformanceResource)resource).Url.Should().Be("http://fire.ly/StructureDefinition/profile1");

            resource = await resolver.ResolveByUriAsync("StructureDefinition/profile2");
            resource.Should().NotBeNull();
            ((IConformanceResource)resource).Url.Should().Be("http://fire.ly/StructureDefinition/profile2");

            resource = await resolver.ResolveByCanonicalUriAsync("StructureDefinition/profile3");
            resource.Should().BeNull();

            resource = await resolver.ResolveByUriAsync("StructureDefinition/non-existing-url");
            resource.Should().BeNull();

            resource = await resolver.ResolveByUriAsync("Patient/123");
            resource.Should().NotBeNull();
            resource.Id.Should().Be("123");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestLoadFunctions()
        {
            var resolver = new InMemoryResourceResolver();
            var resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile1");
            resource.Should().BeNull();

            resolver.Add(profile1);
            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile1");
            resource.Should().NotBeNull();


            resolver.Reload(profile2);

            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile1");
            resource.Should().BeNull();
            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile2");
            resource.Should().NotBeNull();

            resolver.Clear();
            resource = await resolver.ResolveByCanonicalUriAsync("http://fire.ly/StructureDefinition/profile2");
            resource.Should().BeNull();
        }
    }
}
