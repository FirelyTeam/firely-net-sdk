/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.IO;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableArtifactResolverTests
#else
    public class ArtifactResolverTests
#endif
    {
#if !PORTABLE45
        [TestInitialize]
        public void SetupSource()
        {
            source = ZipSource.CreateValidationSource();
        }

        IConformanceSource source = null;


        [TestMethod]
        public void FindConceptMaps()
        {
            var conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use");
            Assert.AreEqual(2, conceptMaps.Count());
            Assert.IsNotNull(conceptMaps.First().Annotation<OriginInformation>());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v2-0190");
            Assert.AreEqual(1, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/v3-AddressUse");
            Assert.AreEqual(1, conceptMaps.Count());

            conceptMaps = source.FindConceptMaps("http://hl7.org/fhir/ValueSet/address-use", "http://hl7.org/fhir/ValueSet/somethingelse");
            Assert.AreEqual(0, conceptMaps.Count());
        }

        [TestMethod]
        public void FindValueSets()
        {
            // A Fhir valueset
            var vs = source.FindValueSetBySystem("http://hl7.org/fhir/contact-point-system");
            Assert.IsNotNull(vs);
            Assert.IsNotNull(vs.Annotation<OriginInformation>());

            // A non-HL7 valueset
            vs = source.FindValueSetBySystem("http://nema.org/dicom/dicm");
            Assert.IsNotNull(vs);

            // One from v2-tables
            vs = source.FindValueSetBySystem("http://hl7.org/fhir/v2/0145");
            Assert.IsNotNull(vs);

            // One from v3-codesystems
            vs = source.FindValueSetBySystem("http://hl7.org/fhir/v3/ActCode");
            Assert.IsNotNull(vs);

            // Something non-existent
            vs = source.FindValueSetBySystem("http://nema.org/dicom/dicmQQQQ");
            Assert.IsNull(vs);
        }

        [TestMethod]
        public void ListCanonicalUris()
        {
            var vs = source.ListResourceUris(ResourceType.ValueSet); Assert.IsTrue(vs.Any());
            var cm = source.ListResourceUris(ResourceType.ConceptMap); Assert.IsTrue(cm.Any());
            var ns = source.ListResourceUris(ResourceType.NamingSystem); Assert.IsTrue(ns.Any());
            var sd = source.ListResourceUris(ResourceType.StructureDefinition); Assert.IsTrue(sd.Any());
            var de = source.ListResourceUris(ResourceType.DataElement); Assert.IsTrue(de.Any());
            var cf = source.ListResourceUris(ResourceType.Conformance); Assert.IsTrue(cf.Any());
            var od = source.ListResourceUris(ResourceType.OperationDefinition); Assert.IsTrue(od.Any());
            var sp = source.ListResourceUris(ResourceType.SearchParameter); Assert.IsTrue(sp.Any());
            var all = source.ListResourceUris();

            Assert.AreEqual(vs.Count() + cm.Count() + ns.Count() + sd.Count() + de.Count() + cf.Count() + od.Count() + sp.Count(), all.Count());

            Assert.IsTrue(vs.Contains("http://hl7.org/fhir/ValueSet/contact-point-system"));
            Assert.IsTrue(cm.Contains("http://hl7.org/fhir/ConceptMap/v2-contact-point-use"));
            Assert.IsTrue(sd.Contains("http://hl7.org/fhir/StructureDefinition/shareablevalueset"));
            Assert.IsTrue(de.Contains("http://hl7.org/fhir/DataElement/Device.manufactureDate"));
            // TODO: Are there any other conformance resources present in validation.zip?           
        }
    }


#endif
}