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
        private ArtifactResolver _source;

        [TestInitialize]
        public void Setup()
        {
            IArtifactSource multi = new MultiArtifactSource(new FileDirectoryArtifactSource("TestData/snapshot-test"), new FileDirectoryArtifactSource(includeSubdirectories: true), ZipArtifactSource.CreateValidationSource());
            _source = new ArtifactResolver(new CachedArtifactSource(multi));
        }


        [TestMethod]
        public void GenerateGroupSnapshot()
        {                                  
            var sd = _source.GetStructureDefinition("http://example.org/fhir/StructureDefinition/human-group");
            Assert.IsNotNull(sd);

            generateSnapshotAndCompare(sd, _source);
        }


        //[TestMethod, Ignore]
        //public void GenerateNorwegianSnapshots()
        //{
        //    var mySource = new FileDirectoryArtifactSource(@"C:\Git\helsenord.ig\Source\Chapter.3.Package", includeSubdirectories: false);
        //    var stdSource = ZipArtifactSource.CreateValidationSource();
        //    var resolver = new ArtifactResolver(new MultiArtifactSource(mySource, stdSource));

        //    var sources = new[] { "noHealthcareService", "noHealthcareServiceLocation", "noOrganization", "noPractitioner", "acronym" };

        //    var generator = new SnapshotGenerator(resolver, markChanges: false);        

        //    foreach (var source in sources)
        //    {
        //        var sd = resolver.GetStructureDefinition("http://hl7.no/fhir/StructureDefinition/" + source);
        //        Assert.IsNotNull(sd, "Cannot find SD " + sd.Url);

        //        generator.Generate(sd);
        //        File.WriteAllText(@"C:\Git\helsenord.ig\Source\Chapter.3.Package\structure." + source + ".xml", FhirSerializer.SerializeResourceToXml(sd));
        //    }           
        //}


        [TestMethod,Ignore]
        public void GenerateSnapshot()
        {           
            foreach (var original in findConstraintStrucDefs())
            {
                if (original.Snapshot == null) continue;        // nothing to test, original does not have a snapshot

                // Fix choiceXXX -> choice[x] bug in Grahame's differentials
           //     repairChoiceBug(original.Snapshot);
               // repairChoiceBug(original.Differential);

                Debug.WriteLine("Generating Snapshot for " + original.Url);

                if (original.Url == "http://hl7.org/fhir/StructureDefinition/condition-daf-dafcondition" ||
                    original.Url == "http://hl7.org/fhir/StructureDefinition/valueset-sdc-structureddatacapturevalueset" ||
                      original.Url == "http://hl7.org/fhir/StructureDefinition/valueset-sdc-de-structureddatacapturevalueset" ||
                    original.Url == "http://hl7.org/fhir/StructureDefinition/medicationdispense-daf-dafmedicationdispense")
                {
                    Debug.WriteLine("skipped");
                }
                else
                    generateSnapshotAndCompare(original, _source);
            }
        }

        private static void generateSnapshotAndCompare(StructureDefinition original, ArtifactResolver source)
        {
            var generator = new SnapshotGenerator(source, markChanges: false);        

            var expanded = (StructureDefinition)original.DeepCopy();
            Assert.IsTrue(original.IsExactly(expanded));

            generator.Generate(expanded);

            // Simulate bug in Grahame's expander
            //if (original.Snapshot.Element.Count == expanded.Snapshot.Element.Count)
            //{
            //    for (var ix = 0; ix < expanded.Snapshot.Element.Count; ix++)
            //    {
            //        if (original.Snapshot.Element[ix].Path == expanded.Snapshot.Element[ix].Path)
            //        {
            //            expanded.Snapshot.Element[ix].Min = original.Snapshot.Element[ix].Min;
            //            expanded.Snapshot.Element[ix].MustSupport = original.Snapshot.Element[ix].MustSupport;
            //        }
            //    }
            //}
            
            var areEqual = original.IsExactly(expanded);

            if (!areEqual)
            {
                File.WriteAllText("c:\\temp\\snapshotgen-source.xml", FhirSerializer.SerializeResourceToXml(original));
                File.WriteAllText("c:\\temp\\snapshotgen-dest.xml", FhirSerializer.SerializeResourceToXml(expanded));
            }

            Assert.IsTrue(areEqual);
        }

        private static void repairChoiceBug(IElementList original)
        {
            foreach (var elem in original.Element)
            {
                if (!elem.Type.IsNullOrEmpty())
                {
                    var typeName = elem.Type.First().Code;
                    if (elem.Path.ToUpper().EndsWith(typeName.ToUpper()) && elem.Path.Length > typeName.Length 
                        && elem.Path[elem.Path.Length-typeName.Length-1] != '.' && !elem.IsExtension() &&
                        elem.Path!="ValueSet.lockedDate")
                    {
                        elem.Path = elem.Path.Substring(0, elem.Path.Length - typeName.Length) + "[x]";
                    }
                }
            }
        }


        private IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
            var sdsInSpec = _source.ListConformanceResources().Where(ci => ci.Type == ResourceType.StructureDefinition);

            foreach (var sdInfo in sdsInSpec)
            {
                var sd = _source.GetStructureDefinition(sdInfo.Url);

                if (sd == null) throw new InvalidOperationException(("Source listed canonical url {0} [source {1}], " +
                    "but could not get structure definition by that url later on!").FormatWith(sdInfo.Url, sdInfo.Origin));

                if (sd.IsConstraint)
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
            Assert.IsNotNull(qStructDef.Snapshot);

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