using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.Profiling;
using Fhir.Profiling.IO;
using System.Xml.XPath;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Introspection.Source;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestResources
    {
        [TestMethod]
        public void ResourceAccess()
        {
            var embedded = EmbeddedResourceAccess.Create<TestResources>();
            IArtifactSource source = embedded.CreateArtifactSource("Fhir.Profiling.Tests.Resources.validation.zip");
          
            Resource resource = source.ReadResourceArtifact(new Uri("http://hl7.org/fhir/profile/Patient"));
            Assert.IsNotNull(resource);
        }

        [TestMethod]
        public void TestTextoutput()
        {
            List<string> texts = EmbeddedResourceAccess.ZipXmlContentStrings<TestResources>("Fhir.Profiling.Tests.Resources.validation.zip").ToList();
            Assert.IsTrue(texts.Count > 0);
        }
    }
}
