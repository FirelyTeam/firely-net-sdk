using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class IValueProviderFPExtensionsTests
    {
        [TestMethod]
        public void CacheInitializedWithUserDefinedSizeTest()
        {
            // Reinitialize the cache, because it could be already initialized by other unittests
            IValueProviderFPExtensions.ReInitializeCache();

            // arrange and act
            IValueProviderFPExtensions.MAX_FP_EXPRESSION_CACHE_SIZE = 3000;
            var node = ElementNode.ForPrimitive("string");
            // the select will initialize the cache
            node.Select("exists()");

            // assert
            var cacheField = typeof(IValueProviderFPExtensions).GetField("CACHE", BindingFlags.Static | BindingFlags.NonPublic);
            cacheField.Should().NotBeNull();

            var lazy = cacheField.GetValue(null) as Lazy<FhirPathCompilerCache>;
            lazy.IsValueCreated.Should().BeTrue();

            var cacheSizeField = typeof(FhirPathCompilerCache).GetField("_cacheSize", BindingFlags.NonPublic | BindingFlags.Instance);
            cacheSizeField.Should().NotBeNull();
            var cacheSize = cacheSizeField.GetValue(lazy.Value) as int?;
            cacheSize.Should().Be(3000);
        }
    }
}