﻿using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source
{
    public partial class ZipSourceTests
    {
        //issue #2031
        [TestMethod]
        public void TestIncorrectFullUrlForValuesetComposeIncludeValueSetTitle()
        {
            var _resolver = ZipSource.CreateValidationSource();
            var stream = _resolver.LoadArtifactByName("extension-definitions.xml");
            var text = new StreamReader(stream).ReadToEnd();
            var bundle = new FhirXmlParser().Parse<Bundle>(text);

            var extensionEntry = bundle.Entry.Where(e => e.FullUrl == "http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle").FirstOrDefault();
            extensionEntry.Should().NotBeNull();
            var sd = extensionEntry.Resource as StructureDefinition;
            sd.Url.Should().Be("http://hl7.org/fhir/StructureDefinition/valueset-compose-include-valueSetTitle");
            sd.Id.Should().Be("valueset-compose-include-valueSetTitle");
        }
    }
}
