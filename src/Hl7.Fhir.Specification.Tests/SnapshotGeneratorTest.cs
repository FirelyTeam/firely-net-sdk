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
using System.Collections.Generic;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableSnapshotGeneratorTest
#else
    public class SnapshotGeneratorTest
#endif
    {
        private ArtifactResolver _testSource;

        [TestInitialize]
        public void Setup()
        {
            _testSource = new ArtifactResolver(new CachedArtifactSource(new FileDirectoryArtifactSource("TestData/snapshot-test")));
        }


        [TestMethod]
        public void GenerateSingleSnapshot()
        {                                  
            var sd = _testSource.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/genetics");
            Assert.IsNotNull(sd);

            generateSnapshotAndCompare(sd, _testSource);
        }

  
        [TestMethod]
        public void GenerateSnapshot()
        {           
            foreach (var original in findConstraintStrucDefs())
            {
                if (original.Snapshot == null) continue;        // nothing to test, original does not have a snapshot

                Debug.WriteLine("Generating Snapshot for " + original.Url);

                generateSnapshotAndCompare(original, _testSource);
            }
        }

        private static void generateSnapshotAndCompare(StructureDefinition original, ArtifactResolver source)
        {
            var generator = new SnapshotGenerator(source, markChanges: false);        

            var expanded = (StructureDefinition)original.DeepCopy();
            Assert.IsTrue(original.IsExactly(expanded));

            generator.Generate(expanded);
           
            var areEqual = original.IsExactly(expanded);

            if (!areEqual)
            {
                File.WriteAllText("c:\\temp\\snapshotgen-source.xml", FhirSerializer.SerializeResourceToXml(original));
                File.WriteAllText("c:\\temp\\snapshotgen-dest.xml", FhirSerializer.SerializeResourceToXml(expanded));
            }

            Assert.IsTrue(areEqual);
        }
   
        private IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
            var testSDs = _testSource.ListConformanceResources().Where(ci => ci.Type == ResourceType.StructureDefinition);

            foreach (var sdInfo in testSDs)
            {
                var sd = _testSource.GetStructureDefinition(sdInfo.Url);

                if (sd == null) throw new InvalidOperationException(("Source listed canonical url {0} [source {1}], " +
                    "but could not get structure definition by that url later on!").FormatWith(sdInfo.Url, sdInfo.Origin));

                if (sd.IsConstraint || sd.IsExtension)
                    yield return sd;
            }
        }

        [TestMethod]
        public void MakeDifferentialTree()
        {
            var e = new List<ElementDefinition>();

            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C2" });
            e.Add(new ElementDefinition() { Path = "A.B" });
            e.Add(new ElementDefinition() { Path = "A.B.C1.D" });
            e.Add(new ElementDefinition() { Path = "A.D.F" });

            var tree = new DifferentialTreeConstructor(e).MakeTree();
            Assert.IsNotNull(tree);

            var nav = new ElementNavigator(tree);
            Assert.AreEqual(10, nav.Count);

            Assert.IsTrue(nav.MoveToChild("A"));
            Assert.IsTrue(nav.MoveToChild("B"));
            Assert.IsTrue(nav.MoveToChild("C1"));
            Assert.IsTrue(nav.MoveToNext("C1"));
            Assert.IsTrue(nav.MoveToNext("C2"));

            Assert.IsTrue(nav.MoveToParent());  // 1st A.B
            Assert.IsTrue(nav.MoveToNext() && nav.Path == "A.B");  // (now) 2nd A.B
            Assert.IsTrue(nav.MoveToChild("C1"));
            Assert.IsTrue(nav.MoveToChild("D"));

            Assert.IsTrue(nav.MoveToParent());  // A.B.C1
            Assert.IsTrue(nav.MoveToParent());  // A.B (2nd)
            Assert.IsTrue(nav.MoveToNext() && nav.Path == "A.D");
            Assert.IsTrue(nav.MoveToChild("F"));
        }

     
        [TestMethod]
        public void TestExpandChild()
        {            
            var qStructDef = _testSource.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/Questionnaire");
            Assert.IsNotNull(qStructDef);
            Assert.IsNotNull(qStructDef.Snapshot);

            var nav = new ElementNavigator(qStructDef.Snapshot.Element);
            
            nav.JumpToFirst("Questionnaire.telecom");
            Assert.IsTrue(SnapshotGenerator.ExpandElement(nav,_testSource));
            Assert.IsTrue(nav.MoveToChild("period"), "Did not move into complex datatype ContactPoint");

            nav.JumpToFirst("Questionnaire.group");
            Assert.IsTrue(SnapshotGenerator.ExpandElement(nav,_testSource));
            Assert.IsTrue(nav.MoveToChild("title"), "Did not move into internally defined backbone element Group");
        }

    }
}