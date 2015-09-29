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
            source = ArtifactResolver.CreateOffline();
        }

        ArtifactResolver source = null;


        [TestMethod]
        public void GetConceptMaps()
        {
            var conceptMapUrls = source.ListConformanceResources().Where(info => info.Type == ResourceType.ConceptMap).Select(info => info.Url);
            var conceptMaps = conceptMapUrls.Select( url => (ConceptMap)source.LoadConformanceResourceByUrl(url));

            Assert.IsTrue(conceptMaps.Count() > 0);
            Assert.IsTrue(conceptMaps.Any(cm => cm.Id == "v2-address-use"));
        }

        [TestMethod]
        public void ResolveExtensions()
        {
            var extDefn = source.GetExtensionDefinition("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            try
            {
                extDefn = source.GetExtensionDefinition("http://hl7.org/fhir/StructureDefinition/Patient");
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
        public void ResolveValueSet()
        {
            var vsDICOM = source.GetValueSetBySystem("http://nema.org/dicom/dicm");
            Assert.IsNotNull(vsDICOM);

            vsDICOM = source.GetValueSet(vsDICOM.Url);
            Assert.IsNotNull(vsDICOM);

            vsDICOM = source.GetValueSetBySystem("http://nema.org/dicom/dicmQQQQ");
            Assert.IsNull(vsDICOM);            
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
        }      
    }
#endif
}