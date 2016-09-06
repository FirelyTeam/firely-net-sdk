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
	public class PortableResolverAndSourceExtensionTests
#else
    public class ResolverAndSourceExtensionTests
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
        public void ResolveExtensions()
        {
            var extDefn = source.FindExtensionDefinition("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            try
            {
                extDefn = source.FindExtensionDefinition("http://hl7.org/fhir/StructureDefinition/Patient");
                Assert.Fail();
            }
            catch
            {
                ;
            }
        }

        [TestMethod]
        public void ResolveStructures()
        {
            var extDefn = source.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));
        }


        [TestMethod]
        public void GetCoreModelTypeUrls()
        {
            var urls = source.GetCoreModelUrls();

            Assert.IsTrue(urls.All(s => s.StartsWith(XmlNs.FHIR)));
            Assert.IsTrue(urls.Any(s => s.EndsWith("/Patient")));
            Assert.IsTrue(urls.Any(s => s.EndsWith("/boolean")));
            Assert.IsTrue(urls.Any(s => s.EndsWith("/Coding")));
            Assert.IsFalse(urls.Any(s => s.EndsWith("/data-absent-reason")));
        }

        [TestMethod]
        public void GetCoreModelTypeByName()
        {
            var pat = source.GetStructureDefinitionForCoreType("Patient");
            Assert.IsNotNull(pat);
            Assert.AreEqual("Patient",pat.Snapshot.Element[0].Path);

            var boolean = source.GetStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            Assert.IsNotNull(boolean);
            Assert.AreEqual("boolean", boolean.Snapshot.Element[0].Path);
        }      
    }
#endif
}