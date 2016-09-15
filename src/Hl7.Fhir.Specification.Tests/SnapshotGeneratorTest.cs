/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
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
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Rest;
using System.Text;
using System.Xml;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableSnapshotGeneratorTest
#else
    public class SnapshotGeneratorTest
#endif
    {
        private SnapshotGenerator _generator;
        private IResourceResolver _testResolver;
        private IConformanceSource _source;

        private readonly SnapshotGeneratorSettings _settings = new SnapshotGeneratorSettings()
        {
            // MarkChanges = false,
            MergeTypeProfiles = true,
            // Throw on unresolved profile references; must include in TestData folder
            ExpandExternalProfiles = false,
            NormalizeElementBase = false
        };

        [TestInitialize]
        public void Setup()
        {
            _source = new DirectorySource("TestData/snapshot-test", includeSubdirectories: true);
            _testResolver = new CachedResolver(_source);
        }

        // [WMR 20160718] Generate snapshot for extension definition fails with exception:
        // System.ArgumentException: structure is not a constraint or extension


        [TestMethod]
        public void GenerateExtensionSnapshot()
        {
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");
            // TODO
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-research-authorization");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-legal-case");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/us-core-religion");
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);
            _settings.NormalizeElementBase = true;
            _settings.ExpandExternalProfiles = true;
            _settings.ForceExpandAll = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            //var sdExt = _testSource.GetStructureDefinitionForCoreType(FHIRDefinedType.Extension);
            //Assert.IsNotNull(sdExt);
            //Assert.IsTrue(sdExt.HasSnapshot);
            //Assert.IsTrue(sdExt.Snapshot.Element[0].Condition.Contains("ele-1"));
            //Assert.IsTrue(sdExt.Snapshot.Element[0].Condition.Contains("ext-1"));

            //Assert.IsTrue(expanded.Snapshot.Element[0].Condition.Contains("ele-1"));
            //Assert.IsTrue(expanded.Snapshot.Element[0].Condition.Contains("ext-1"));
        }


        [TestMethod]
        public void GenerateSingleSnapshot()
        {
            _settings.MergeTypeProfiles = true;
            _settings.NormalizeElementBase = true;
            _settings.ExpandExternalProfiles = true;
            _settings.MarkChanges = false;
            _settings.ForceExpandAll = true; // TEST

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/xdsdocumentreference");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-medicationorder");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/shareablevalueset");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-alternate");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-result");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-procedurerequest");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");

            // [WMR 20160825] Examples by Simone Heckman - custom, free-form canonical url
            // => ResourceIdentity is obsolete!
            // var sd = _testResolver.FindStructureDefinition(@"http://fhir.de/StructureDefinition/kbv/betriebsstaette");
            // var sd = _testResolver.FindStructureDefinition(@"http://fhir.de/StructureDefinition/kbv/istNebenbetriebsstaette");

            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBasic");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

        }

        [TestMethod]
        public void GenerateRepeatedSnapshot()
        {
            StructureDefinition expanded;
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/measurereport");
            generateSnapshotAndCompare(sd, _testResolver, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            // [WMR 20160903] TODO: Second expansion fails, base paths are now normalized...? (e.g. DomainResource.text)
            sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/clinicaldocument");
            generateSnapshotAndCompare(sd, _testResolver, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }


        [TestMethod]
        public void TestExpandAllComplexElements()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(sd);
            generateSnapshot(sd, _testResolver);
            Assert.IsTrue(sd.HasSnapshot);
            var elems = sd.Snapshot.Element;
            Assert.AreEqual("Patient.identifier", elems[9].Path);
            Assert.AreEqual("Patient.active", elems[10].Path);
            var expanded = expandAllComplexElements(sd.Differential.Element);
            Assert.IsNotNull(expanded);
            foreach (var elem in expanded)
            {
                Debug.WriteLine("{0}  |  {1}", elem.Path, elem.Base != null ? elem.Base.Path : null);
            }
            int i = 0;
            Assert.AreEqual("Patient.identifier", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.id", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.extension", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.use", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.id", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.extension", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.id", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.extension", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.system", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.version", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.code", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.display", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.userSelected", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.type.text", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.system", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.value", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.period", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.period.id", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.period.extension", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.period.start", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.period.end", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.id", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.extension", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.reference", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.display", expanded[++i].Path);

            for (int j = 1; j < expanded.Count; j++)
            {
                if (isExpandableElement(expanded[j]))
                {
                    verifyExpandElement(expanded[j], elems, expanded);
                }
            }
        }

        IList<ElementDefinition> expandAllComplexElements(IList<ElementDefinition> elements)
        {
            IList<ElementDefinition> expanded = elements.DeepCopy().ToList();
            var nav = new ElementDefinitionNavigator(expanded);
            // Skip root element
            if (nav.MoveToFirstChild() && nav.MoveToFirstChild())
            {
                if (_generator == null)
                {
                    _generator = new SnapshotGenerator(_testResolver, _settings);
                }
                expandAllComplexChildElements(ref expanded, ref nav);
            }
            return expanded;
        }

        void expandAllComplexChildElements(ref IList<ElementDefinition> expanded, ref ElementDefinitionNavigator nav)
        {
            do
            {
                var elem = nav.Current;
                if (isExpandableElement(elem))
                {
                    expanded = _generator.ExpandElement(expanded, elem);
                    // Must re-initialize the navigator... bit inefficient
                    nav = new ElementDefinitionNavigator(expanded);
                    if (!nav.MoveTo(elem))
                    {
                        // Shouldn't happen...?
                        throw new InvalidOperationException("SnapshotGenerator.ExpandElement returned an invalid result.");
                    }
                }
                if (nav.MoveToFirstChild())
                {
                    expandAllComplexChildElements(ref expanded, ref nav);
                    if (!nav.MoveToParent())
                    {
                        // Shouldn't happen...?
                        throw new InvalidOperationException("expandAllComplexChildElements returned an invalid navigator.");
                    }
                }
            } while (nav.MoveToNext());
        }

        bool isExpandableElement(ElementDefinition element)
        {
            var typeCode = element.PrimaryTypeCode();
            return typeCode.HasValue
                   && element.Type.Count == 1
                   && ModelInfo.IsDataType(typeCode.Value)
                   && typeCode.Value != FHIRDefinedType.Extension
                   && typeCode.Value != FHIRDefinedType.BackboneElement;
        }


        [TestMethod]
        public void TestSnapshotRecursionChecker()
        {
            // Following structuredefinition has a recursive element type profile
            // Verify that the snapshot generator detects recursion and aborts with exception

            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBundle");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            bool exceptionRaised = false;
            try
            {
                generateSnapshotAndCompare(sd, _testResolver, out expanded);
                dumpOutcome(_generator.Outcome);
                dumpBasePaths(expanded);
            }
            catch (Exception ex)
            {
                Debug.Print("{0}: {1}".FormatWith(ex.GetType().Name, ex.Message));
                exceptionRaised = ex is NotSupportedException;
            }
            Assert.IsTrue(exceptionRaised);
        }

        [TestMethod]
        public void GenerateSingleSnapshotNormalizeBase()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBasic");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);
            // _settings.RewriteElementBase = true;
            _settings.NormalizeElementBase = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        public void GenerateDerivedProfileSnapshot()
        {
            // cqif-guidanceartifact profile is derived from cqif-knowledgemodule
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-encounter");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        public void GeneratePatientWithExtensionsSnapshot()
        {
            // Example by Chris Grenz
            // https://github.com/chrisgrenz/FHIR-Primer/blob/master/profiles/patient-extensions-profile.xml
            // Manually downgraded from FHIR v1.4.0 to v1.0.2

            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/SD/patient-with-extensions");
            var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            Assert.IsNotNull(sd);
            Assert.IsTrue(sd.HasSnapshot);

            // [WMR 20160906] Remove ElementDefinition@id attributes (not supported yet)
            foreach (var elem in sd.Snapshot.Element)
            {
                elem.ElementId = null;
            }

            // dumpReferences(sd);
            _settings.NormalizeElementBase = true;
            _settings.MergeTypeProfiles = true;
            _settings.ExpandExternalProfiles = true;
            _settings.ForceExpandAll = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            // Verify internal recursive expansion of base types, e.g. DomainResource
            //var resourceDef = _testSource.GetStructureDefinitionForCoreType(FHIRDefinedType.Resource);
            //Assert.IsNotNull(resourceDef);
            //Assert.IsTrue(resourceDef.HasSnapshot);

            //var domainResourceDef = _testSource.GetStructureDefinitionForCoreType(FHIRDefinedType.DomainResource);
            //Assert.IsNotNull(domainResourceDef);
            //Assert.IsTrue(domainResourceDef.HasSnapshot);

            // TODO: test inheritance of condition/constraint/mapping
        }

        [TestMethod]
        public void GenerateSnapshotExpandExternalProfile()
        {
            // Profile MyLocation references extension MyLocationExtension
            // MyLocationExtension extension profile does not have a snapshot component => expand on demand
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyLocation");
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            var extensionElements = sd.Differential.Element.Where(e => e.IsExtension());
            Assert.IsNotNull(extensionElements);
            Assert.AreEqual(2, extensionElements.Count()); // Extension slicing entry + first extension definition
            var extensionElement = extensionElements.Skip(1).FirstOrDefault();
            var extensionType = extensionElement.Type.FirstOrDefault();
            Assert.IsNotNull(extensionType);
            Assert.AreEqual(FHIRDefinedType.Extension, extensionType.Code);
            Assert.IsNotNull(extensionType.Profile);
            var extDefUrl = extensionType.Profile.FirstOrDefault();
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/MyLocationExtension", extDefUrl);
            var ext = _testResolver.FindStructureDefinition(extDefUrl);
            Assert.IsNotNull(ext);
            Assert.IsNull(ext.Snapshot);

            // dumpReferences(sd);

            _settings.ExpandExternalProfiles = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        [Ignore]
        public void GenerateSnapshotIgnoreMissingExternalProfile()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyObservation");
            Assert.IsNotNull(sd);

            dumpReferences(sd, true);

            _settings.MergeTypeProfiles = true;             // Merge the external type/extension profiles
            _settings.ExpandExternalProfiles = false;       // Don't generate missing snapshots
            //
            // OBSOLETE - TODO
            //
            // _settings.ExpandUnconstrainedElements = true;   // Force the external type profiles to be expanded (even w/o any diff constraints)

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testResolver, out expanded);

            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            Assert.IsNotNull(outcome);
            Assert.AreEqual(3, outcome.Issue.Count);

            assertProfileNotFoundIssue(outcome.Issue[0], Validation.Issue.UNAVAILABLE_NEED_SNAPSHOT, "http://example.org/fhir/StructureDefinition/MyExtensionNoSnapshot");
            assertProfileNotFoundIssue(outcome.Issue[1], Validation.Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "http://example.org/fhir/StructureDefinition/MyIdentifier");
            assertProfileNotFoundIssue(outcome.Issue[2], Validation.Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "http://example.org/fhir/StructureDefinition/MyCodeableConcept");
        }

        static void assertProfileNotFoundIssue(OperationOutcome.IssueComponent issue, Validation.Issue expected, string profileUrl)
        {
            Assert.IsNotNull(issue);
            Assert.AreEqual(expected.Type, issue.Code);
            Assert.AreEqual(expected.Severity, issue.Severity);
            Assert.AreEqual(expected.Code.ToString(), issue.Details.Coding[0].Code);
            Assert.IsNotNull(issue.Extension);
            Assert.AreEqual(profileUrl, issue.Diagnostics);
        }

        // [WMR 20160721] Following profiles are not yet handled (TODO)
        private readonly string[] skippedProfiles =
        {
			// Differential defines constraint on MedicationOrder.reason[x]
			// Snapshot renames this element to MedicationOrder.reasonCodeableConcept - is this mandatory?
			// @"http://hl7.org/fhir/StructureDefinition/gao-medicationorder",
		};

        [TestMethod]
        // [Ignore]
        public void GenerateSnapshot()
        {
            var start = DateTime.Now;
            int count = 0;

            foreach (var original in findConstraintStrucDefs()
                // [WMR 20160721] Skip invalid profiles
                .Where(sd => !skippedProfiles.Contains(sd.Url))
            )
            {
                // nothing to test, original does not have a snapshot
                if (original.Snapshot == null) continue;

                Debug.WriteLine("Generating Snapshot for " + original.Url);

                generateSnapshotAndCompare(original, _testResolver);
                count++;
            }

            var duration = DateTime.Now.Subtract(start).TotalMilliseconds;
            var avg = duration / count;
            Debug.WriteLine("Expanded {0} profiles in {1} ms = {2} ms per profile on average.".FormatWith(count, duration, avg));
        }

        //private void forDoc()
        //{
        //    FhirXmlParser parser = new FhirXmlParser(new ParserSettings { AcceptUnknownMembers = true });
        //    IFhirReader xmlWithPatientData = null;
        //    var patient = parser.Parse<Patient>(xmlWithPatientData);

        //    // -----

        //    ArtifactResolver source = ArtifactResolver.CreateCachedDefault();
        //    var settings = new SnapshotGeneratorSettings { IgnoreMissingTypeProfiles = true };
        //    StructureDefinition profile = null;

        //    var generator = new SnapshotGenerator(source, _settings);
        //    generator.Generate(profile);
        //}

        StructureDefinition generateSnapshot(StructureDefinition original, IResourceResolver source)
        {
            // var generator = new SnapshotGenerator(source, _settings);
            if (_generator == null)
            {
                _generator = new SnapshotGenerator(source ?? _testResolver, _settings);
            }

            var expanded = (StructureDefinition)original.DeepCopy();
            Assert.IsTrue(original.IsExactly(expanded));

            _generator.Update(expanded);

            return expanded;
        }

        bool generateSnapshotAndCompare(StructureDefinition original, IResourceResolver source)
        {
            StructureDefinition expanded;
            return generateSnapshotAndCompare(original, source, out expanded);
        }

        bool generateSnapshotAndCompare(StructureDefinition original, IResourceResolver source, out StructureDefinition expanded)
        {
            expanded = generateSnapshot(original, source);

            var areEqual = original.IsExactly(expanded);

            // [WMR 20160803] Always save output to separate file, convenient for debugging
            // if (!areEqual)
            // {
            var tempPath = Path.GetTempPath();
            File.WriteAllText(Path.Combine(tempPath, "snapshotgen-source.xml"), FhirSerializer.SerializeResourceToXml(original));
            File.WriteAllText(Path.Combine(tempPath, "snapshotgen-dest.xml"), FhirSerializer.SerializeResourceToXml(expanded));
            // }

            // Assert.IsTrue(areEqual);
            Debug.WriteLineIf(!areEqual, "WARNING: '{0}' Expansion ({1} elements) is not equal to original ({2} elements)!".FormatWith(
                original.Name, original.HasSnapshot ? original.Snapshot.Element.Count : 0, expanded.HasSnapshot ? expanded.Snapshot.Element.Count : 0)
            );

            return areEqual;
        }

        IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
            var testSDs = _source.FindAll<StructureDefinition>();

            foreach (var testSD in testSDs)
            {
                var sdInfo = testSD.Annotation<OriginInformation>();
                // [WMR 20160721] Select all profiles in profiles-others.xml
                var fileName = Path.GetFileNameWithoutExtension(sdInfo.Origin);
                if (fileName == "profiles-others")
                {
                    //var sd = _testResolver.FindStructureDefinition(sdInfo.Canonical);

                    //if (sd == null) throw new InvalidOperationException(("Source listed canonical url {0} [source {1}], " +
                    //    "but could not get structure definition by that url later on!").FormatWith(sdInfo.Canonical, sdInfo.Origin));

                    if (testSD.IsConstraint || testSD.IsExtension)
                        yield return testSD;
                }
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

            var nav = new ElementDefinitionNavigator(tree);
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
            var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Questionnaire);
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            var nav = new ElementDefinitionNavigator(sd.Snapshot.Element);

            var generator = new SnapshotGenerator(_testResolver, SnapshotGeneratorSettings.Default);

            nav.JumpToFirst("Questionnaire.telecom");
            Assert.IsTrue(generator.expandElement(nav));
            Assert.IsTrue(nav.MoveToChild("period"), "Did not move into complex datatype ContactPoint");

            nav.JumpToFirst("Questionnaire.group");
            Assert.IsTrue(generator.expandElement(nav));
            Assert.IsTrue(nav.MoveToChild("title"), "Did not move into internally defined backbone element Group");
        }

        // [WMR 20160802] NEW - Expand a single element

        [TestMethod]
        public void TestExpandElement_PatientIdentifier()
        {
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Patient", "Patient.identifier");
        }

        [TestMethod]
        public void TestExpandElement_PatientName()
        {
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Patient", "Patient.name");
        }

        [TestMethod]
        public void TestExpandElement_QuestionnaireGroupGroup()
        {
            // Validate name reference expansion
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Questionnaire", "Questionnaire.group.group");
        }

        [TestMethod]
        public void TestExpandElement_QuestionnaireGroupQuestionGroup()
        {
            // Validate name reference expansion
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Questionnaire", "Questionnaire.group.question.group");
        }

        [TestMethod]
        public void TestExpandElement_Slice()
        {
            var sd = _testResolver.FindStructureDefinition("http://hl7.org/fhir/StructureDefinition/lipidprofile");
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            // DiagnosticReport.result is sliced
            var nav = new ElementDefinitionNavigator(sd.Snapshot.Element);

            // Move to slicing entry
            nav.JumpToFirst("DiagnosticReport.result");
            Assert.IsNotNull(nav.Current.Slicing);

            // Move to first (named) slice
            nav.MoveToNext();
            Assert.AreEqual(nav.Path, "DiagnosticReport.result");
            Assert.IsNotNull(nav.Current.Name);

            testExpandElement(sd, nav.Current);
        }

        void testExpandElement(string srcProfileUrl, string expandElemPath)
        {
            // Prepare...
            var sd = _testResolver.FindStructureDefinition(srcProfileUrl);
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            var elems = sd.Snapshot.Element;
            Assert.IsNotNull(elems);

            Debug.WriteLine("Input:");
            Debug.Indent();
            Debug.WriteLine(string.Join(Environment.NewLine, elems.Where(e => e.Path.StartsWith(expandElemPath)).Select(e => e.Path)));
            Debug.Unindent();

            var elem = elems.FirstOrDefault(e => e.Path == expandElemPath);
            testExpandElement(sd, elem);
        }

        void testExpandElement(StructureDefinition sd, ElementDefinition elem)
        {
            Assert.IsNotNull(elem);
            var elems = sd.Snapshot.Element;
            Assert.IsTrue(elems.Contains(elem));

            // Test...
            _generator = new SnapshotGenerator(_testResolver, _settings);
            var result = _generator.ExpandElement(elems, elem);

            // Verify results
            verifyExpandElement(elem, elems, result);
        }

        void verifyExpandElement(ElementDefinition elem, List<ElementDefinition> elems, IList<ElementDefinition> result)
        {
            var expandElemPath = elem.Path;

            // Debug.WriteLine("\r\nOutput:");
            // Debug.WriteLine(string.Join(Environment.NewLine, result.Where(e => e.Path.StartsWith(expandElemPath)).Select(e => e.Path)));

            Assert.IsNotNull(elem.Type);
            var elemType = elem.Type.FirstOrDefault();
            var nameRef = elem.NameReference;
            if (elemType != null)
            {
                // Validate type profile expansion
                var elemTypeCode = elemType.Code.Value;
                Assert.IsNotNull(elemTypeCode);

                var elemProfile = elemType.Profile.FirstOrDefault();
                var sdType = elemProfile != null && elemTypeCode != FHIRDefinedType.Reference
                    ? _testResolver.FindStructureDefinition(elemProfile)
                    : _testResolver.FindStructureDefinitionForCoreType(elemTypeCode);

                Assert.IsNotNull(sdType);
                Assert.IsNotNull(sdType.Snapshot);
                Assert.IsNotNull(sdType.Snapshot.Element);
                Assert.IsTrue(sdType.Snapshot.Element.Count > 0);

                // Debug.WriteLine("\r\nType:");
                // Debug.WriteLine(string.Join(Environment.NewLine, sdType.Snapshot.Element.Select(e => e.Path)));

                sdType.Snapshot.Rebase(expandElemPath);
                var typeElems = sdType.Snapshot.Element;

                var nav = new ElementDefinitionNavigator(result);
                //Assert.IsTrue(result.Count == elems.Count + typeElems.Count - 1);
                //if (elem.Name == null)
                //{
                //    Assert.IsTrue(result.Where(e => e.Path.StartsWith(expandElemPath)).Count() == typeElems.Count);
                //}
                //else
                if (elem.Name != null)
                {
                    // The expanded element represents a slice
                    // var nav = new ElementDefinitionNavigator(result);
                    nav.JumpToNameReference(elem.Name);
                    var cnt = 1;
                    Assert.IsTrue(nav.MoveToFirstChild());
                    do
                    {
                        Assert.AreEqual(typeElems[cnt++].Path, nav.Path);
                    } while (nav.MoveToNext());
                    Assert.AreEqual(typeElems.Count, cnt);
                }

                // var startPos = result.IndexOf(elem);
                nav.Reset();
                Assert.IsTrue(nav.MoveTo(elem));
                Assert.IsTrue(nav.MoveToFirstChild());
                for (int i = 1; i < typeElems.Count; i++)
                {
                    var path = typeElems[i].Path;
                    // Assert.IsTrue(result[startPos + i].Path.EndsWith(path, StringComparison.OrdinalIgnoreCase));
                    Assert.IsTrue(nav.Path.EndsWith(path, StringComparison.OrdinalIgnoreCase));
                    nav.MoveToNext();
                }

            }
            else if (nameRef != null)
            {
                // Validate name reference expansion
                var nav = new ElementDefinitionNavigator(elems);
                Assert.IsTrue(nav.JumpToNameReference(nameRef));
                var prefix = nav.Path;
                Assert.IsTrue(nav.MoveToFirstChild());
                var pos = result.IndexOf(elem);

                Debug.WriteLine("\r\nName Reference:");
                Debug.Indent();
                do
                {
                    Debug.WriteLine(nav.Path);
                    var srcPath = nav.Path.Substring(prefix.Length);
                    var tgtPath = result[++pos].Path.Substring(expandElemPath.Length);
                    Assert.AreEqual(srcPath, tgtPath);
                } while (nav.MoveToNext());
                Debug.Unindent();
            }
        }

        // [WMR 20160722] For debugging purposes
        [Conditional("DEBUG")]
        void dumpReferences(StructureDefinition sd, bool differential = false)
        {
            if (sd != null)
            {
                Debug.WriteLine("References for StructureDefinition '{0}' ('{1}')".FormatWith(sd.Name, sd.Url));
                Debug.WriteLine("Base = '{0}'".FormatWith(sd.Base));

                // FhirClient client = new FhirClient("http://fhir2.healthintersections.com.au/open/");
                // var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\snapshot-test\download");
                // if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

                var component = differential ? sd.Differential.Element : sd.Snapshot.Element;
                var profiles = enumerateDistinctTypeProfiles(component);

                Debug.Indent();
                foreach (var profile in profiles)
                {
                    Debug.WriteLine(profile);

                    // How to determine the original filename?
                    //try
                    //{
                    //    var xml = client.Get(profile);
                    //    var filePath = Path.Combine()
                    //    File.WriteAllText(folderPath, )
                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.WriteLine(ex.Message);
                    //}
                }
                Debug.Unindent();
            }
        }

        static IEnumerable<string> enumerateDistinctTypeProfiles(IList<ElementDefinition> elements)
        {
            return elements.SelectMany(e => e.Type).SelectMany(t => t.Profile).Distinct();
        }

        [Conditional("DEBUG")]
        static void dumpBaseElems(IList<ElementDefinition> elements)
        {
            Debug.Print(string.Join(Environment.NewLine,
                elements.Select(e =>
                {
                    var bea = e.Annotation<BaseDefAnnotation>();
                    var be = bea != null ? bea.BaseElementDefinition : null;
                    return "  #{0} '{1}' - '{2}' => #{3} '{4}' - '{5}'"
                        .FormatWith(
                            e.GetHashCode(),
                            e.Path,
                            e.Base != null ? e.Base.Path : null,
                            be != null ? (int?)be.GetHashCode() : null,
                            be != null ? be.Path : null,
                            be != null && be.Base != null ? be.Base.Path : null
                        );
                })
            ));
        }

        [Conditional("DEBUG")]
        void dumpBasePaths(StructureDefinition sd)
        {
            if (sd != null && sd.Snapshot != null)
            {
                Debug.WriteLine("StructureDefinition '{0}' ('{1}')".FormatWith(sd.Name, sd.Url));
                Debug.WriteLine("Base = '{0}'".FormatWith(sd.Base));
                // Debug.Indent();
                Debug.Print("Element.Path | Element.Base.Path");
                Debug.Print(new string('=', 100));
                foreach (var elem in sd.Snapshot.Element)
                {
                    Debug.WriteLine("{0}  |  {1}", elem.Path, elem.Base != null ? elem.Base.Path : null);
                }
                // Debug.Unindent();
            }
        }

        [Conditional("DEBUG")]
        void dumpOutcome(OperationOutcome outcome)
        {
            if (outcome != null)
            {
                Debug.Print("OperationOutcome: {0} issues", outcome.Issue.Count);
                for (int i = 0; i < outcome.Issue.Count; i++)
                {
                    dumpIssue(outcome.Issue[i], i);
                }
            }
        }

        [Conditional("DEBUG")]
        private void dumpIssue(OperationOutcome.IssueComponent issue, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Issue #{0}: Severity = '{1}' Code = '{2}'", index, issue.Severity, issue.Code);
            if (issue.Details != null)
            {
                sb.AppendFormat(" Details: '{0}'", string.Join(" | ", issue.Details.Coding.Select(c => c.Code)));
                if (issue.Details.Text != null) sb.AppendFormat(" Text : '{0}'", issue.Details.Text);
            }
            if (issue.Diagnostics != null) { sb.AppendFormat(" Profile: '{0}'", issue.Diagnostics); }
            if (issue.Location != null) { sb.AppendFormat(" Path: '{0}'", string.Join(" | ", issue.Location)); }

            Debug.Print(sb.ToString());
        }


        [TestMethod]
        public void GenerateSnapshotEmitBaseData()
        {
            var source = _testResolver;

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/shareablevalueset");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyLocation");
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyPatient");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension1");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Meta");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-basic-guidance-action");


            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.ExpandExternalProfiles = true;
            settings.MergeTypeProfiles = true;
            settings.MarkChanges = true;
            settings.NormalizeElementBase = true;
            settings.ForceExpandAll = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += ProfileHandler;
                _generator.PrepareElement += ElementHandler;
                _generator.Constraint += ConstraintHandler;

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, source, out expanded);

                dumpOutcome(_generator.Outcome);

                assertBaseDefs(expanded, settings);

                if (sd.Url == @"http://example.org/fhir/StructureDefinition/MyPatient")
                {
                    var sdBase = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
                    assertBaseDefs(sdBase, settings);

                    var sdElem = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
                    assertBaseDefs(sdElem, settings);

                    var sdExt = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");
                    assertBaseDefs(sdExt, settings);

                    var sdExt1 = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension1");
                    assertBaseDefs(sdExt1, settings);

                    var sdExt2 = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension2");
                    assertBaseDefs(sdExt2, settings);
                }

            }
            finally
            {
                // Detach event handlers
                _generator.Constraint -= ConstraintHandler;
                _generator.PrepareElement -= ElementHandler;
                _generator.PrepareBaseProfile -= ProfileHandler;
            }
        }

        
        // [WMR 20160816] Test custom annotations containing associated base definitions
        class BaseDefAnnotation
        {
            public BaseDefAnnotation(ElementDefinition baseElemDef) { BaseElementDefinition = baseElemDef; }
            public ElementDefinition BaseElementDefinition { get; private set; }
        }

        void ProfileHandler(object sender, SnapshotBaseProfileEventArgs e)
        {
            var profile = e.Profile;
            // Assert.IsTrue(sd.Url != profile.Url || sd.IsExactly(profile));
            var baseProfile = e.BaseProfile;
            Assert.IsNotNull(baseProfile);
            Debug.WriteLine("[SnapshotBaseProfileHandler] Profile #{0} '{1}' Base = '{2}'".FormatWith(profile.GetHashCode(), profile.Url, profile.Base));
            Debug.Print("[SnapshotBaseProfileHandler] Base Profile #{0} '{1}'".FormatWith(baseProfile.GetHashCode(), baseProfile.Url));
            var rootElem = baseProfile.Snapshot.Element[0];
            Debug.Print("[SnapshotBaseProfileHandler] Base Root element #{0} '{1}'".FormatWith(rootElem.GetHashCode(), rootElem.Path));
            Assert.AreEqual(profile.Base, baseProfile.Url);
        }

        void ElementHandler(object sender, SnapshotElementEventArgs e)
        {
            var elem = e.Element;
            Assert.IsNotNull(elem);
            var ann = elem.Annotation<BaseDefAnnotation>();
            // We want to annotate a reference to the matching base element from the (immediate) base profile.
            // When the snapshot generator expands external profiles, then this handler is called once for each
            // profile in the base hierarchy, starting at the root profile, e.g. Resource => DomainResource => Patient.
            // Each time we recreate the annotation, so the final annotation contains a reference to the immediate base.
            if (ann != null)
            {
                elem.RemoveAnnotations<BaseDefAnnotation>();
            }
            var baseDef = e.BaseElement;
            var baseStruct = e.BaseStructure;
            elem.AddAnnotation(new BaseDefAnnotation(baseDef));
            Debug.Write("[SnapshotElementHandler] #{0} '{1}' - Base: #{2} '{3}' - Base Structure '{4}'".FormatWith(elem.GetHashCode(), elem.Path, baseDef != null ? baseDef.GetHashCode() : 0, baseDef != null ? baseDef.Path : null, baseStruct != null ? baseStruct.Url : null));
            Debug.WriteLine(ann != null && ann.BaseElementDefinition != null ? " (old Base: #{0} '{1}')".FormatWith(ann.BaseElementDefinition.GetHashCode(), ann.BaseElementDefinition.Path) : "");
        }

        void ConstraintHandler(object sender, SnapshotConstraintEventArgs e)
        {
            var elem = e.Element as ElementDefinition;
            if (elem != null)
            {
                var changed = elem.GetChangedByDiff() == true;
                Debug.Print("[SnapshotConstraintHandler] #{0} '{1}'{2}".FormatWith(elem.GetHashCode(), elem.Path, changed ? " CHANGED!" : null));
            }
        }

        static void assertBaseDefs(StructureDefinition sd, SnapshotGeneratorSettings settings)
        {
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);
            var elems = sd.Snapshot.Element;
            Assert.IsNotNull(elems);
            Assert.IsTrue(elems.Count > 0);

            var isConstraint = sd.ConstrainedType.HasValue;

            Debug.Print("\r\nStructureDefinition '{0}' url = '{1}'", sd.Name, sd.Url);
            Debug.Print("# | Constraints? | Changed? | Element.Path | Element.Base.Path | BaseElement.Path | #Base | Invalid?");
            Debug.Print(new string('=', 100));
            foreach (var elem in elems)
            {
                // Each element should have a valid Base component, unless the profile is a core type/resource definition (no base)
                Assert.IsTrue(!isConstraint || elem.Base != null);

                var ann = elem.Annotation<BaseDefAnnotation>();
                var baseDef = ann != null ? ann.BaseElementDefinition : null;
                Assert.AreNotEqual(elem, baseDef);

                var hasChanges = HasChanges(elem);
                var hasConstraints = false;
                if (baseDef != null) // && elem.Base != null)
                {
                    // If normalizing, then elem.Base.Path refers to the defining profile (e.g. DomainResource),
                    // whereas baseDef refers to the immediate base profile (e.g. Patient)
                    Debug.Assert(settings.NormalizeElementBase || elem.Base == null || ElementDefinitionNavigator.IsCandidateBaseElementPath(elem.Base.Path, baseDef.Path));
                    hasConstraints = HasConstraints(elem, baseDef);
                }
                var isValid = hasChanges == hasConstraints;
                Debug.WriteLine("{0,10}  |  {1}  |  {2,-12}  |  {3,-50}  |  {4,-40}  |  {5,-40}  |  {6,10}  |  {7}",
                    elem.GetHashCode(),
                    hasConstraints ? "+" : "-",
                    getChangeDescription(elem),
                    elem.Path,
                    elem.Base != null ? elem.Base.Path : null,
                    baseDef != null ? baseDef.Path : null,
                    baseDef != null ? baseDef.GetHashCode().ToString() : null,
                    !isValid ? "!!!" : ""
                );
                //Assert.IsTrue(baseDef == null || isValid);
                // Debug.Assert(baseDef == null || isValid);
            }
        }

        // Utility function to compare element and base element
        // Path, Base and CHANGED_BY_DIFF_EXT extension are excluded from comparison
        // Returns true if the element has any other constraints on base
        static bool HasConstraints(ElementDefinition elem, ElementDefinition baseElem)
        {
            var elemClone = (ElementDefinition)elem.DeepCopy();
            var baseClone = (ElementDefinition)baseElem.DeepCopy();

            // Path & Base are expected to differ
            baseClone.Path = elem.Path;
            baseClone.Base = elem.Base;

            // Also ignore any Changed extensions on base and diff
            elemClone.RemoveAllChangedByDiff();
            baseClone.RemoveAllChangedByDiff();

            return !baseClone.IsExactly(elemClone);
        }

        // Returns true if the specified element or any of its' components contain the CHANGED_BY_DIFF_EXT extension
        static bool HasChanges(ElementDefinition elem)
        {
            return isChanged(elem)
                || hasChanges(elem.AliasElement)
                || isChanged(elem.Base)
                || isChanged(elem.Binding)
                || hasChanges(elem.Code)
                || isChanged(elem.CommentsElement)
                || hasChanges(elem.ConditionElement)
                || hasChanges(elem.Constraint)
                || isChanged(elem.DefaultValue)
                || isChanged(elem.DefinitionElement)
                || isChanged(elem.Example)
                || hasChanges(elem.Extension)
                || hasChanges(elem.FhirCommentsElement)
                || isChanged(elem.Fixed)
                || isChanged(elem.IsModifierElement)
                || isChanged(elem.IsSummaryElement)
                || isChanged(elem.LabelElement)
                || hasChanges(elem.Mapping)
                || isChanged(elem.MaxElement)
                || isChanged(elem.MaxLengthElement)
                || isChanged(elem.MaxValue)
                || isChanged(elem.MeaningWhenMissingElement)
                || isChanged(elem.MinElement)
                || isChanged(elem.MinValue)
                || isChanged(elem.MustSupportElement)
                || isChanged(elem.NameElement)
                || isChanged(elem.NameReferenceElement)
                || isChanged(elem.PathElement)
                || isChanged(elem.Pattern)
                || hasChanges(elem.RepresentationElement)
                || isChanged(elem.RequirementsElement)
                || isChanged(elem.ShortElement)
                || isChanged(elem.Slicing)
                || hasChanges(elem.Type);
        }

        static string getChangeDescription(ElementDefinition element)
        {
            if (isChanged(element.Slicing)) { return "Slicing"; }       // Moved to front
            if (hasChanges(element.Type)) { return "Type"; }            // Moved to front
            if (isChanged(element.ShortElement)) { return "Short"; }    // Moved to front

            if (hasChanges(element.AliasElement)) { return "Alias"; }
            if (isChanged(element.Base)) { return "Base"; }
            if (isChanged(element.Binding)) { return "Binding"; }
            if (hasChanges(element.Code)) { return "Code"; }
            if (isChanged(element.CommentsElement)) { return "Comments"; }
            if (hasChanges(element.ConditionElement)) { return "Condition"; }
            if (hasChanges(element.Constraint)) { return "Constraint"; }
            if (isChanged(element.DefaultValue)) { return "DefaultValue"; }
            if (isChanged(element.DefinitionElement)) { return "Definition"; }
            if (isChanged(element.Example)) { return "Example"; }
            if (hasChanges(element.Extension)) { return "Extension"; }
            if (hasChanges(element.FhirCommentsElement)) { return "FhirComments"; }
            if (isChanged(element.Fixed)) { return "Fixed"; }
            if (isChanged(element.IsModifierElement)) { return "IsModifier"; }
            if (isChanged(element.IsSummaryElement)) { return "IsSummary"; }
            if (isChanged(element.LabelElement)) { return "Label"; }
            if (hasChanges(element.Mapping)) { return "Mapping"; }
            if (isChanged(element.MaxElement)) { return "Max"; }
            if (isChanged(element.MaxLengthElement)) { return "MaxLength"; }
            if (isChanged(element.MaxValue)) { return "MaxValue"; }
            if (isChanged(element.MeaningWhenMissingElement)) { return "MeaningWhenMissing"; }
            if (isChanged(element.MinElement)) { return "Min"; }
            if (isChanged(element.MinValue)) { return "MinValue"; }
            if (isChanged(element.MustSupportElement)) { return "MustSupport"; }
            if (isChanged(element.NameElement)) { return "Name"; }
            if (isChanged(element.NameReferenceElement)) { return "NameReference"; }
            if (isChanged(element.PathElement)) { return "Path"; }
            if (isChanged(element.Pattern)) { return "Pattern"; }
            if (hasChanges(element.RepresentationElement)) { return "Representation"; }
            if (isChanged(element.RequirementsElement)) { return "Requirements"; }
            //if (IsChanged(element.ShortElement)) { return "Short"; }
            //if (IsChanged(element.Slicing)) { return "Slicing"; }
            //if (HasChanges(element.Type)) { return "Type"; }

            if (isChanged(element)) { return "Element"; }           // Moved to back

            return string.Empty;
        }

        static bool hasChanges<T>(IList<T> extendables) where T : IExtendable
        {
            return extendables != null ? extendables.Any(e => isChanged(e)) : false;
        }

        static bool isChanged(IExtendable extendable)
        {
            return extendable != null && extendable.GetChangedByDiff() == true;
        }

        // [WMR 20160902] NEW
        [TestMethod]
        public void TestExpandCoreResource()
        {
            // First prepare Element root type
            // var sdElem = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
            // generateSnapshot(sdElem, _source);

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/BackboneElement");
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/integer");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/positiveInt");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/string");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/code");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/id");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Meta");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/HumanName");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Quantity");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/SimpleQuantity");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Resource");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/DomainResource");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Basic");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Questionnaire");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            _settings.NormalizeElementBase = true;
            _settings.ForceExpandAll = true;
            _settings.MergeTypeProfiles = true;
            _settings.ExpandExternalProfiles = true;

            StructureDefinition expanded;
            var result = generateSnapshotAndCompare(sd, _testResolver, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            if (!result)
            {
                Debug.Print("Expanded is not exactly equal to original... verifying...");
                result = verifyElementBase(sd, expanded);
            }

            Assert.IsTrue(result);
        }

        IEnumerable<T> enumerateBundleStream<T>(Stream stream) where T : Resource
        {
            using (var reader = XmlReader.Create(stream))
            {
                var parser = new FhirXmlParser();
                var bundle = parser.Parse<Bundle>(reader);
                foreach (var entry in bundle.Entry)
                {
                    var res = entry.Resource as T;
                    if (res != null) { yield return res; }
                }
            }
        }

        // [WMR 20160912] Expand all core data types
        // Start at root type Element, then expand derived types (recursively)
        // This ensures that we can annotate valid references to base elements (generated previously)

        [TestMethod]
        public void TestExpandCoreTypes()
        {
            var result = true;
            var source = new DirectorySource("TestData/snapshot-test", false);
            var resolver = new CachedResolver(source); // IMPORTANT!

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.ExpandExternalProfiles = true;
            settings.MergeTypeProfiles = true;
            // settings.MarkChanges = true;
            settings.NormalizeElementBase = true;
            // settings.ForceExpandAll = true;
            _generator = new SnapshotGenerator(resolver, settings);

            _generator.PrepareElement += ElementHandler;

            try
            {
                // HACK! CachedResolver doesn't expose LoadArtifactByName
                // So first enumerate source to get url's, then enumerate CachedResolver to persist snapshots (!)
                ProfileInfo[] coreProfileInfo;
                using (var stream = source.LoadArtifactByName("profiles-types.xml"))
                {
                    // var coreDefs = EnumerateBundleStream<StructureDefinition>(stream).ToList();
                    // expandCoreProfilesDerivedFrom(coreDefs, null);

                    var coreDefs = enumerateBundleStream<StructureDefinition>(stream);
                    coreProfileInfo = coreDefs.Select(sd => new ProfileInfo() { Url = sd.Url, Base = sd.Base }).ToArray();
                }
                expandStructuresBasedOn(resolver, coreProfileInfo, null);
            }
            finally
            {
                _generator.PrepareElement -= ElementHandler;
            }
            Assert.IsTrue(result);
        }

        struct ProfileInfo { public string Url; public string Base; }

        void expandStructuresBasedOn(IResourceResolver resolver, ProfileInfo[] profileInfo, string baseUrl)
        {
            var derivedStructures = profileInfo.Where(pi => pi.Base == baseUrl);
            if (derivedStructures.Any())
            {
                Debug.WriteLineIf(derivedStructures.Any(), "Expand structures derived from: '{0}'".FormatWith(baseUrl));
                foreach (var info in derivedStructures)
                {
                    var sd = resolver.FindStructureDefinition(info.Url);
                    Assert.IsNotNull(sd);
                    updateSnapshot(sd);
                    expandStructuresBasedOn(resolver, profileInfo, sd.Url);
                }
            }
        }

        void updateSnapshot(StructureDefinition sd)
        {
            Assert.IsNotNull(sd);
            Debug.Print("Profile: '{0}' : '{1}'".FormatWith(sd.Url, sd.Base));
            // Important! Must expand original instances, not clones!
            // var original = sd.DeepCopy() as StructureDefinition;
            _generator.Update(sd);
            // result &= verifyElementBase(original, entry);
            dumpOutcome(_generator.Outcome);
            dumpBaseElems(sd.Snapshot.Element);
        }

        // Verify ElementDefinition.Base components
        bool verifyElementBase(StructureDefinition original, StructureDefinition expanded)
        {
            var originalElems = original.HasSnapshot ? original.Snapshot.Element : new List<ElementDefinition>();
            var expandedElems = expanded.HasSnapshot ? expanded.Snapshot.Element : new List<ElementDefinition>();
            var isConstraint = expanded.ConstrainedType.HasValue;
            Debug.Print("Original has {0} elements, expanded has {1} elements...".FormatWith(originalElems.Count, expandedElems.Count));

            // dumpBasePaths(original);

            bool verified = false;
            if (expandedElems.Count < originalElems.Count)
            {
                for (int i = 0; i < originalElems.Count; i++)
                {
                    var elem = originalElems[i];
                    var match = expandedElems.Any(e => e.Path == elem.Path);
                    if (!match)
                    {
                        Debug.Print("{0} has not been expanded...".FormatWith(elem.Path));
                    }
                }
            }
            else if (expandedElems.Count == originalElems.Count)
            {
                verified = true;
                if (_settings.NormalizeElementBase)
                {
                    var rootElemName = expandedElems[0].Path;

                    //var baseProfileUrl = expanded.Base;
                    //var baseProfile = baseProfileUrl != null ? _testResolver.FindStructureDefinition(baseProfileUrl) : null;
                    //var baseRootElemName = baseProfile != null && baseProfile.Snapshot != null ? baseProfile.Snapshot.Element[0].Path : null;
                    //if (expandedElems.Count > 0 && baseRootElemName != null)
                    //{
                    //    verified &= verifyBasePath(expandedElems[0], originalElems[0], baseRootElemName);
                    //}

                    if (expanded.Kind == StructureDefinition.StructureDefinitionKind.Datatype)
                    {
                        if (rootElemName != "Element")
                        {
                            verified &= verifyBasePath(expandedElems[0], originalElems[0], "Element");
                        }

                        if (rootElemName != "Element" && expandedElems.Count > 2)
                        {
                            verified &= verifyBasePath(expandedElems[1], originalElems[1], "Element.id");
                            verified &= verifyBasePath(expandedElems[2], originalElems[2], "Element.extension");
                        }
                    }
                    else if (expanded.Kind == StructureDefinition.StructureDefinitionKind.Resource)
                    {
                        if (rootElemName != "Resource")
                        {
                            verified &= verifyBasePath(expandedElems[0], originalElems[0], "Resource");
                        }

                        if (rootElemName != "Resource" && expandedElems.Count > 4)
                        {
                            verified &= verifyBasePath(expandedElems[1], originalElems[1], "Resource.id");
                            verified &= verifyBasePath(expandedElems[2], originalElems[2], "Resource.meta");
                            verified &= verifyBasePath(expandedElems[3], originalElems[3], "Resource.implicitRules");
                            verified &= verifyBasePath(expandedElems[4], originalElems[4], "Resource.language");
                        }
                        if (rootElemName != "DomainResource" && expandedElems.Count > 8)
                        {
                            verified &= verifyBasePath(expandedElems[5], originalElems[5], "DomainResource.text");
                            verified &= verifyBasePath(expandedElems[6], originalElems[6], "DomainResource.contained");
                            verified &= verifyBasePath(expandedElems[7], originalElems[7], "DomainResource.extension");
                            verified &= verifyBasePath(expandedElems[8], originalElems[8], "DomainResource.modifierExtension");
                        }
                        for (int i = 9; i < expandedElems.Count; i++)
                        {
                            var path = expandedElems[i].Path;
                            if (path.EndsWith(".id"))
                            {
                                verified &= verifyBasePath(expandedElems[i], originalElems[i], "Element.id");
                            }
                            else if (path.EndsWith(".extension"))
                            {
                                verified &= verifyBasePath(expandedElems[i], originalElems[i], "Element.extension");
                            }
                            else if (path.EndsWith(".modifierExtension"))
                            {
                                verified &= verifyBasePath(expandedElems[i], originalElems[i], "BackboneElement.modifierExtension");
                            }
                            else
                            {
                                if (!isConstraint)
                                {
                                    // New resource element
                                    verified &= verifyBasePath(expandedElems[i], originalElems[i], isConstraint ? expandedElems[i].Path : null);
                                    verified &= verifyBasePath(originalElems[i], originalElems[i], isConstraint ? originalElems[i].Path : null);
                                }
                            }
                        }
                    }
                }
                else if (isConstraint)
                {
                    for (int i = 0; i < expandedElems.Count; i++)
                    {
                        if (originalElems[i].Base == null) { verified = false; Debug.WriteLine("ORIGINAL: Path = {0}  => BASE IS MISSING".FormatWith(originalElems[i].Path)); }
                        if (expandedElems[i].Base == null) { verified = false; Debug.WriteLine("EXPANDED: Path = {0}  => BASE IS MISSING".FormatWith(expandedElems[i].Path)); }
                    }
                }


            }
            return verified;
        }

        static bool verifyBasePath(ElementDefinition elem, ElementDefinition orgElem, string path = "")
        {
            bool result = false;
            if (!string.IsNullOrEmpty(path))
            {
                // Assert.IsNotNull(elem.Base);
                // Assert.AreEqual(path, elem.Base.Path);

                // Assert.IsNotNull(baseElem.Base);
                // Assert.AreEqual(path, baseElem.Base.Path);

                result = elem.Base != null && path == elem.Base.Path;

                Debug.WriteLineIf(elem.Base == null, "EXPANDED: Path = {0}  => BASE IS MISSING".FormatWith(elem.Path));
                Debug.WriteLineIf(orgElem.Base == null, "ORIGINAL: Path = {0}  => BASE IS MISSING".FormatWith(orgElem.Path));

                Debug.WriteLineIf(elem.Base != null && path != elem.Base.Path, "EXPANDED: Path = {0} Base = {1} != {2} => INVALID BASE PATH".FormatWith(elem.Path, elem.Base != null ? elem.Base.Path : null, path));
                Debug.WriteLineIf(orgElem.Base != null && path != orgElem.Base.Path, "ORIGINAL: Path = {0} Base = {1} != {2} => INVALID BASE PATH".FormatWith(orgElem.Path, orgElem.Base != null ? orgElem.Base.Path : null, path));

            }
            else
            {
                // New resource element
                // Assert.IsNull(elem.Base);
                // Assert.IsNull(baseElem.Base);

                result = elem.Base == null;

                Debug.WriteLineIf(elem.Base != null, "EXPANDED: Path = {0} Base = {1} != '' => BASE SHOULD BE NULL".FormatWith(elem.Path, elem.Base != null ? elem.Base.Path : null, path));
                Debug.WriteLineIf(orgElem.Base != null, "ORIGINAL: Path = {0} Base = {1} != '' => BASE SHOULD BE NULL".FormatWith(orgElem.Path, orgElem.Base != null ? orgElem.Base.Path : null, path));

            }
            return result;
        }

    }

}
