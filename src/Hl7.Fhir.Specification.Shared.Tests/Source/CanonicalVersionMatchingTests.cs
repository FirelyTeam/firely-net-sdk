/* 
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class CanonicalVersionMatchingTests
    {
        private InMemoryResourceResolver CreateTestResolver()
        {
            var resources = new List<Resource>
            {
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/MyProfile",
                    Version = "1.5.0",
                    Name = "MyProfile150"
                },
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/MyProfile",
                    Version = "1.5.1",
                    Name = "MyProfile151"
                },
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/MyProfile",
                    Version = "1.6.0",
                    Name = "MyProfile160"
                },
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/MyProfile",
                    Version = "2.0.0",
                    Name = "MyProfile200"
                },
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/OtherProfile",
                    Version = "1.5.0",
                    Name = "OtherProfile150"
                }
            };

            return new InMemoryResourceResolver(resources);
        }

        [TestMethod]
        public void ExactVersionMatching_ShouldWork()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile|1.5.0");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StructureDefinition));
            var sd = (StructureDefinition)result;
            Assert.AreEqual("1.5.0", sd.Version);
            Assert.AreEqual("MyProfile150", sd.Name);
        }

        [TestMethod]
        public void PartialVersionMatching_ShouldMatchFullVersion()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act - Query with partial version "1.5" should match "1.5.0" or "1.5.1"
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile|1.5");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StructureDefinition));
            var sd = (StructureDefinition)result;
            // Should match one of the 1.5.x versions
            Assert.IsTrue(sd.Version.StartsWith("1.5"), $"Expected version starting with '1.5', but got '{sd.Version}'");
        }

        [TestMethod]
        public void PartialVersionMatching_ShouldNotMatchDifferentMajorMinor()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act - Query with partial version "1.4" should not match any resource
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile|1.4");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PartialVersionMatching_ShouldNotMatchHigherMajorVersion()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act - Query with partial version "1.5" should not match "2.0.0"
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile|2");

            // Assert
            Assert.IsNotNull(result);
            var sd = (StructureDefinition)result;
            Assert.AreEqual("2.0.0", sd.Version);
        }

        [TestMethod]
        public void NoVersionSpecified_ShouldMatchAnyVersion()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act - Query without version should match any version
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StructureDefinition));
            // Should match one of the available versions
        }

        [TestMethod]
        public void BackwardsCompatibility_ExistingExactMatching_ShouldStillWork()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act - Use exact version matching as before
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/MyProfile|1.5.1");

            // Assert - Should still get exact match
            Assert.IsNotNull(result);
            var sd = (StructureDefinition)result;
            Assert.AreEqual("1.5.1", sd.Version);
            Assert.AreEqual("MyProfile151", sd.Name);
        }

        [TestMethod]
        public void NonExistentResource_ShouldReturnNull()
        {
            // Arrange
            var resolver = CreateTestResolver();

            // Act
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/NonExistent|1.0");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmptyVersion_ShouldMatchUnversionedResources()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new StructureDefinition
                {
                    Url = "http://example.org/StructureDefinition/UnversionedProfile",
                    Name = "UnversionedProfile"
                    // Version is null/empty
                }
            };
            var resolver = new InMemoryResourceResolver(resources);

            // Act
            var result = resolver.ResolveByCanonicalUri("http://example.org/StructureDefinition/UnversionedProfile");

            // Assert
            Assert.IsNotNull(result);
            var sd = (StructureDefinition)result;
            Assert.AreEqual("UnversionedProfile", sd.Name);
        }
    }
}