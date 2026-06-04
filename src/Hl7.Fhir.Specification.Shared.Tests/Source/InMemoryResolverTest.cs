using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests;

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

        var resource = await resolver.ResolveByUriAsync("StructureDefinition/profile1") as IConformanceResource;
        resource.Should().NotBeNull();
        resource!.Url.Should().Be("http://fire.ly/StructureDefinition/profile1");

        resource = await resolver.ResolveByUriAsync("StructureDefinition/profile2") as IConformanceResource;
        resource.Should().NotBeNull();
        resource!.Url.Should().Be("http://fire.ly/StructureDefinition/profile2");

        resource = await resolver.ResolveByCanonicalUriAsync("StructureDefinition/profile3") as IConformanceResource;
        resource.Should().BeNull();

        resource = await resolver.ResolveByUriAsync("StructureDefinition/non-existing-url") as IConformanceResource;
        resource.Should().BeNull();

        var resource2 = await resolver.ResolveByUriAsync("Patient/123");
        resource2.Should().NotBeNull();
        resource2.Id.Should().Be("123");
    }

    [TestMethod]
    public void PartialVersionMatching_ShouldWork()
    {
        // Arrange - Create test resources with different versions
        var resources = new List<Resource>
        {
            new StructureDefinition
            {
                Url = "http://example.org/StructureDefinition/TestProfile",
                Version = "1.5.0",
                Name = "TestProfile150"
            },
            new StructureDefinition
            {
                Url = "http://example.org/StructureDefinition/TestProfile",
                Version = "1.5.1",
                Name = "TestProfile151"
            },
            new StructureDefinition
            {
                Url = "http://example.org/StructureDefinition/TestProfile",
                Version = "1.6.0",
                Name = "TestProfile160"
            }
        };

        var resolver = new InMemoryResourceResolver(resources);

        // Act & Assert - Test exact version matching (should still work)
        var exactResult = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/TestProfile|1.5.0");
        Assert.IsNotNull(exactResult);
        var exactSd = (StructureDefinition)exactResult;
        Assert.AreEqual("1.5.0", exactSd.Version);

        // Act & Assert - Test partial version matching (new functionality)
        var partialResult = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/TestProfile|1.5");
        Assert.IsNotNull(partialResult, "Partial version matching should return a result");
        var partialSd = (StructureDefinition)partialResult;
        Assert.StartsWith("1.5", partialSd.Version, $"Expected version starting with '1.5', but got '{partialSd.Version}'");

        // Act & Assert - Test that wrong partial version returns null
        var wrongResult = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/TestProfile|1.4");
        Assert.IsNull(wrongResult, "Non-matching partial version should return null");
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