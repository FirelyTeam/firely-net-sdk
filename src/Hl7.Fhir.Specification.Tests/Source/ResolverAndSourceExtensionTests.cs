/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ResolverAndSourceExtensionTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext _)
        {
            source = ZipSource.CreateValidationSource();
        }

        private static ZipSource source = null;

#pragma warning disable CS0618 // Type or member is obsolete
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

// Let's keep a few of the obsolete calls in, so we test those too
#pragma warning restore CS0618 // Type or member is obsolete

        [TestMethod]
        public async T.Task ResolveStructureDefs()
        {
            var extDefn = await source.FindStructureDefinitionAsync("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);

            extDefn = await source.FindStructureDefinitionAsync("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
        }

        [TestMethod]
        public async T.Task ResolveCoreStructureDefs()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var patDefn = source.FindStructureDefinitionForCoreType("Patient");
            Assert.IsNotNull(patDefn);

            var patDefn2 = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Patient);
            Assert.IsNotNull(patDefn2);
            Assert.AreEqual(patDefn.Url, patDefn2.Url);
#pragma warning restore CS0618 // Type or member is obsolete

            var some = await source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.HumanName);
            Assert.IsNotNull(some);

            some = await source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.String);
            Assert.IsNotNull(some);
        }

        [TestMethod]
        public async T.Task FindValueSet()
        {
            // As defined in the spec here: http://hl7.org/fhir/2016Sep/valueset-contact-point-system.html

#pragma warning disable CS0618 // Type or member is obsolete
            var vs = source.FindValueSet("http://hl7.org/fhir/ValueSet/contact-point-system");
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.IsNotNull(vs);

            vs = await source.FindValueSetAsync("http://hl7.org/fhir/ValueSet/contact-point-system");
            Assert.IsNotNull(vs);

            // STU3: Relationship between ValueSet and CodingSystem changed, so we need to make some fundamental changes here
            //var vs2 = source.FindValueSetBySystem("http://hl7.org/fhir/contact-point-system");
            //Assert.IsNotNull(vs2);

            //Assert.AreEqual(vs.Url, vs2.Url);
        }

        [TestMethod]
        public void FindAllResources()
        {
            var uris = source.ListResourceUris(ResourceType.ConceptMap).ToList();
            Assert.IsTrue(uris.Any());

            var cmaps = source.FindAll<ConceptMap>().ToList();
            Assert.AreEqual(uris.Count(), cmaps.Count());
        }

        [TestMethod]
        public async T.Task GetCoreModelTypeByName()
        {
            var pat = await source.FindStructureDefinitionForCoreTypeAsync("Patient");
            Assert.IsNotNull(pat);
            Assert.AreEqual("Patient", pat.Snapshot.Element[0].Path);

#pragma warning disable CS0618 // Type or member is obsolete
            var boolean = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Boolean);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.IsNotNull(boolean);
            Assert.AreEqual("boolean", boolean.Snapshot.Element[0].Path);
        }

        [TestMethod]
        public async T.Task FindStructureDefinitionForCoreTypeLogicalModel()
        {
            var ccdaAnyCanonical = "http://hl7.org/fhir/cda/StructureDefinition/ANY";
            var resolver = new MultiResolver(source, new LogicalModelTypeResourceResolver());
            var logicalModel = await resolver.FindStructureDefinitionForCoreTypeAsync(ccdaAnyCanonical);
            Assert.IsNotNull(logicalModel);
            Assert.AreEqual(ccdaAnyCanonical, logicalModel.Type);
        }

        private class LogicalModelTypeResourceResolver : IResourceResolver
        {
            public Resource ResolveByCanonicalUri(string uri)
            {
                var customLogicalModelDataTypeXml = File.ReadAllText(Path.Combine("TestData/ccda", "CCDA_ANY.xml"));
                var sd = new FhirXmlParser().Parse<StructureDefinition>(customLogicalModelDataTypeXml);
                if (sd.Type.Equals(uri))
                    return sd;

                return null;
            }

            public Resource ResolveByUri(string uri)
            {
                throw new NotImplementedException();
            }
        }

    }
}
