/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
    public class ResolverAndSourceExtensionTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static IConformanceSource source = null;

        [TestMethod]
        public void ResolveExtensions()
        {
            var extDefn = source.FindExtensionDefinition("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);

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
        public void ResolveStructureDefs()
        {
            var extDefn = source.FindStructureDefinition("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);

            extDefn = source.FindStructureDefinition("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
        }

        [TestMethod]
        public void ResolveCoreStructureDefs()
        {
            var patDefn = source.FindStructureDefinitionForCoreType("Patient");
            Assert.IsNotNull(patDefn);

            var patDefn2 = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            Assert.IsNotNull(patDefn2);

            Assert.AreEqual(patDefn.Url, patDefn2.Url);

            var some = source.FindStructureDefinitionForCoreType(FHIRDefinedType.HumanName);
            Assert.IsNotNull(some);

            some = source.FindStructureDefinitionForCoreType(FHIRDefinedType.String);
            Assert.IsNotNull(some);
        }


        [TestMethod]
        public void FindValueSet()
        {
            var vs = source.FindValueSet("http://hl7.org/fhir/ValueSet/contact-point-system");
            Assert.IsNotNull(vs);

            var vs2 = source.FindValueSetBySystem("http://hl7.org/fhir/contact-point-system");
            Assert.IsNotNull(vs2);

            Assert.AreEqual(vs.Url, vs2.Url);
        }

        [TestMethod]
        public void FindAllResources()
        {
            var uris = source.ListResourceUris(ResourceType.ConceptMap);
            Assert.IsTrue(uris.Any());

            var cmaps = source.FindAll<ConceptMap>();
            Assert.AreEqual(uris.Count(), cmaps.Count());
        }

        [TestMethod]
        public void GetCoreModelTypeByName()
        {
            var pat = source.FindStructureDefinitionForCoreType("Patient");
            Assert.IsNotNull(pat);
            Assert.AreEqual("Patient", pat.Snapshot.Element[0].Path);

            var boolean = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            Assert.IsNotNull(boolean);
            Assert.AreEqual("boolean", boolean.Snapshot.Element[0].Path);
        }

    }
}