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
using Hl7.Fhir.Specification.Expansion;
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
        private ArtifactResolver _source;

        [TestInitialize]
        public void Setup()
        {
            _source = ArtifactResolver.CreateCachedDefault();
        }


        [TestMethod]
        public void GenerateSnapshot()
        {
            var generator = new SnapshotGenerator(_source);

            foreach (var original in findConstraintStrucDefs())
            {
                var expanded = (StructureDefinition)original.DeepCopy();
                Assert.IsTrue(original.IsExactly(expanded));

                generator.Generate(expanded);

                var areEqual = original.IsExactly(expanded);

                if (!areEqual)
                {
                    File.WriteAllText("c:\\temp\\expanded-java.xml", FhirSerializer.SerializeResourceToXml(original));
                    File.WriteAllText("c:\\temp\\expanded-dotnet.xml", FhirSerializer.SerializeResourceToXml(expanded));
                }

                Assert.IsTrue(areEqual);
            }
        }


        private IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
            var sdsInSpec = _source.ListConformanceResources().Where(ci => ci.Type == ResourceType.StructureDefinition);

            foreach (var sdInfo in sdsInSpec)
            {
                var sd = _source.GetStructureDefinition(sdInfo.Url);

                if (sd.Type == StructureDefinition.StructureDefinitionType.Constraint)
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
            var qStructDef = _source.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/Questionnaire");
            Assert.IsNotNull(qStructDef);

            var nav = new ElementNavigator(qStructDef.Snapshot.Element);
            
            nav.JumpToFirst("Questionnaire.telecom");
            Assert.IsTrue(nav.ExpandElement(_source));
            Assert.IsTrue(nav.MoveToChild("period"), "Did not move into complex datatype ContactPoint");

            nav.JumpToFirst("Questionnaire.group");
            Assert.IsTrue(nav.ExpandElement(_source));
            Assert.IsTrue(nav.MoveToChild("title"), "Did not move into internally defined backbone element Group");
        }

    }
}