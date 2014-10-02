using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Publication;
using System.IO;
using Hl7.Fhir.Serialization;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class PublishProfile
    {     
        [TestMethod]
        public void PublishLipidProfile()
        {
            var source = new FileArtifactSource(true);
            var profile = (Profile)source.ReadResourceArtifact(new Uri("http://from.file/TestData/lipid.profile.xml"));

            var publisher = new ProfileTableGenerator(@"c:\temp\publisher", "test page", false);

            var result = File.ReadAllText(@"TestData\publish-header.xml");
            result += publisher.generate(profile, false).ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
            result += File.ReadAllText(@"TestData\publish-footer.xml");

            File.WriteAllText(@"c:\temp\publisher\publisher.html",result);
        }

        [TestMethod]
        public void PublishLipidProfileStructures()
        {
            var source = new FileArtifactSource(true);
            var profile = (Profile)source.ReadResourceArtifact(new Uri("http://from.file/TestData/lipid.profile.xml"));

            var publisher = new StructureGenerator();

            foreach (var structure in profile.Structure)
            {
                var result = File.ReadAllText(@"TestData\publish-header.xml");
                result += publisher.generateStructureTable("bla.html", structure, false, @"c:\temp\publisher", false, profile, "http://nu.nl/publisher.html", "publisher.html")
                        .ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                result += File.ReadAllText(@"TestData\publish-footer.xml");

                File.WriteAllText(@"c:\temp\publisher\" + structure.Name + ".html", result);
            }
        }

    }
}
