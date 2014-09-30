using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Publication;
using System.IO;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class PublishProfile
    {     
        [TestMethod]
        public void PublishLipidProfile()
        {
            var source = ArtifactResolver.CreateDefault();
            var profile = (Profile)source.ReadResourceArtifact(new Uri("http://from.file/TestData/lipid.profile.xml"));

            var publisher = new ProfileTableGenerator(Directory.GetCurrentDirectory(), "test page", false);
            var result = publisher.generate(profile, false).ToString();
            File.WriteAllText(@"c:\temp\publisher.html",result);
        }
    }
}
