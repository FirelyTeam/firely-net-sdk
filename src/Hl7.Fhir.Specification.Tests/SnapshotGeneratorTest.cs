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
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Rest;
using System.Text;

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
        private IArtifactSource _testSource;
        private readonly SnapshotGeneratorSettings _settings = new SnapshotGeneratorSettings()
        {
            // MarkChanges = false,
            MergeTypeProfiles = true,
            // Throw on unresolved profile references; must include in TestData folder
            // IgnoreUnresolvedProfiles = false,
            ExpandExternalProfiles = false,
            // RewriteElementBase = false,
            NormalizeElementBase = false
        };

        [TestInitialize]
        public void Setup()
        {
            _testSource = new ArtifactResolver(new CachedArtifactSource(new FileDirectoryArtifactSource("TestData/snapshot-test", includeSubdirectories: true)));
        }

        // [WMR 20160718] Generate snapshot for extension definition fails with exception:
        // System.ArgumentException: structure is not a constraint or extension

        [TestMethod]
        //[Ignore]
        public void GenerateExtensionSnapshot()
        {
            var sd = _testSource.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        // [Ignore] // For debugging purposes
        public void GenerateSingleSnapshot()
        {
            // _settings.MergeTypeProfiles = false;

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-result");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/xdsdocumentreference");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-medicationorder");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/shareablevalueset");

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-alternate");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-result");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/gao-procedurerequest");
            var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");

            // [WMR 20160825] Examples by Simone Heckman - custom, free-form canonical url
            // => ResourceIdentity is obsolete!
            // var sd = _testSource.GetStructureDefinition(@"http://fhir.de/StructureDefinition/kbv/betriebsstaette");
            // var sd = _testSource.GetStructureDefinition(@"http://fhir.de/StructureDefinition/kbv/istNebenbetriebsstaette");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

        }

        [TestMethod]
        public void GenerateRepeatedSnapshot()
        {
            StructureDefinition expanded;
            var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/measurereport");
            generateSnapshotAndCompare(sd, _testSource, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            // [WMR 20160903] TODO: Second expansion fails, base paths are now normalized...? (e.g. DomainResource.text)
            sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/clinicaldocument");
            generateSnapshotAndCompare(sd, _testSource, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        // [Ignore] // For debugging purposes
        public void GenerateSnapshotExpandUnconstrainedElements()
        {
            _settings.ExpandUnconstrainedElements = true;

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-patient");

            // [WMR 20160818] Verify that full expansion does not hang on recursive named references
            var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            // StructureDefinition expanded;
            // generateSnapshotAndCompare(sd, _testSource, out expanded);

            var expanded = generateSnapshot(sd, _testSource);
            var areEqual = sd.IsExactly(expanded);
            Assert.IsFalse(areEqual);

            dumpBasePaths(expanded);
        }


        [TestMethod]
        // [Ignore] // For debugging purposes
        public void GenerateRecursiveSnapshot()
        {
            // Following structuredefinition has a recursive element type profile
            // Verify that the snapshot generator detects recursion and aborts with exception

            var sd = _testSource.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBundle");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            bool exceptionRaised = false;
            try
            {
                generateSnapshotAndCompare(sd, _testSource, out expanded);
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
        // [Ignore] // For debugging purposes
        public void GenerateSingleSnapshotNormalizeBase()
        {
            var sd = _testSource.GetStructureDefinition(@"http://example.org/StructureDefinition/MyBasic");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);
            // _settings.RewriteElementBase = true;
            _settings.NormalizeElementBase = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        // [Ignore] // For debugging purposes
        public void GenerateDerivedProfileSnapshot()
        {
            // cqif-guidanceartifact profile is derived from cqif-knowledgemodule
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-patient");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-encounter");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        //[Ignore]
        public void GeneratePatientWithExtensionsSnapshot()
        {
            // Example by Chris Grenz
            // https://github.com/chrisgrenz/FHIR-Primer/blob/master/profiles/patient-extensions-profile.xml
            // Manually downgraded from FHIR v1.4.0 to v1.0.2

            // var sd = _testSource.GetStructureDefinition(@"http://example.com/fhir/SD/patient-with-extensions");
            var sd = _testSource.GetStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);
            _settings.NormalizeElementBase = true;
            _settings.MergeTypeProfiles = true;
            _settings.ExpandExternalProfiles = true;
            // _settings.IgnoreUnresolvedProfiles = false;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        //[Ignore]
        public void GenerateSnapshotExpandExternalProfile()
        {
            // Profile MyLocation references extension MyLocationExtension
            // MyLocationExtension extension profile does not have a snapshot component => expand on demand
            var sd = _testSource.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyLocation");
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
            var ext = _testSource.GetStructureDefinition(extDefUrl);
            Assert.IsNotNull(ext);
            Assert.IsNull(ext.Snapshot);

            // dumpReferences(sd);

            _settings.ExpandExternalProfiles = true;

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        //[Ignore]
        public void GenerateSnapshotIgnoreMissingExternalProfile()
        {
            var sd = _testSource.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyObservation");
            Assert.IsNotNull(sd);

            dumpReferences(sd, true);

            // _settings.IgnoreUnresolvedProfiles = true;   // On missing profile, aggregate information and continue
            _settings.MergeTypeProfiles = true;             // Merge the external type/extension profiles
            _settings.ExpandExternalProfiles = false;       // Don't generate missing snapshots
            _settings.ExpandUnconstrainedElements = true;   // Force the external type profiles to be expanded (even w/o any diff constraints)

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, _testSource, out expanded);

            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            Assert.IsNotNull(outcome);
            Assert.AreEqual(3, outcome.Issue.Count);

            AssertProfileNotFoundIssue(outcome.Issue[0], Validation.Issue.UNAVAILABLE_NEED_SNAPSHOT, "http://example.org/fhir/StructureDefinition/MyExtensionNoSnapshot");
            AssertProfileNotFoundIssue(outcome.Issue[1], Validation.Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "http://example.org/fhir/StructureDefinition/MyIdentifier");
            AssertProfileNotFoundIssue(outcome.Issue[2], Validation.Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "http://example.org/fhir/StructureDefinition/MyCodeableConcept");
        }

        private static void AssertProfileNotFoundIssue(OperationOutcome.IssueComponent issue, Validation.Issue expected, string profileUrl)
        {
            Assert.IsNotNull(issue);
            Assert.AreEqual(issue.Code, expected.Type);
            Assert.AreEqual(issue.Severity, expected.Severity);
            Assert.AreEqual(issue.Details.Coding[0].Code, expected.Code.ToString());
            Assert.IsNotNull(issue.Extension);
            Assert.IsTrue(issue.Extension.Count == 1);
            Assert.IsTrue(issue.Extension[0].Url == SnapshotGenerator.PROFILE_URL_EXT);
            Assert.AreEqual(issue.GetProfileUrl(), profileUrl);
        }

        //private static void assertProfileInfo(IList<SnapshotProfileInfo> info, string url, SnapshotProfileStatus status)
        //{
        //    Assert.AreEqual(1, info.Count(pi => pi.Url == url & pi.Status == status));
        //}

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

                generateSnapshotAndCompare(original, _testSource);
                count++;
            }

            var duration = DateTime.Now.Subtract(start).TotalMilliseconds;
            var avg = duration / count;
            Debug.WriteLine("Expanded {0} profiles in {1} ms = {2} ms per profile on average.".FormatWith(count, duration, avg));
        }



#if false
        private void forDoc()
        {
            FhirXmlParser parser = new FhirXmlParser(new ParserSettings { AcceptUnknownMembers = true });
            IFhirReader xmlWithPatientData = null;
            var patient = parser.Parse<Patient>(xmlWithPatientData);

            // -----

            ArtifactResolver source = ArtifactResolver.CreateCachedDefault();
            var settings = new SnapshotGeneratorSettings { IgnoreMissingTypeProfiles = true };
            StructureDefinition profile = null;

            var generator = new SnapshotGenerator(source, _settings);
            generator.Generate(profile);
        }
#endif

        private StructureDefinition generateSnapshot(StructureDefinition original, IArtifactSource source)
        {
            // var generator = new SnapshotGenerator(source, _settings);
            if (_generator == null)
            {
                _generator = new SnapshotGenerator(_testSource, _settings);
            }

            var expanded = (StructureDefinition)original.DeepCopy();
            Assert.IsTrue(original.IsExactly(expanded));

            _generator.Update(expanded);

            return expanded;
        }

        private bool generateSnapshotAndCompare(StructureDefinition original, IArtifactSource source)
        {
            StructureDefinition expanded;
            return generateSnapshotAndCompare(original, source, out expanded);
        }

        private bool generateSnapshotAndCompare(StructureDefinition original, IArtifactSource source, out StructureDefinition expanded)
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
            Debug.WriteLineIf(!areEqual, "WARNING: '{0}' Expansion is not equal to original!".FormatWith(original.Name));

            return areEqual;
        }


        private IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
            var testSDs = _testSource.ListConformanceResources().Where(ci => ci.Type == ResourceType.StructureDefinition);

            foreach (var sdInfo in testSDs)
            {
                // [WMR 20160721] Select all profiles in profiles-others.xml
                var fileName = Path.GetFileNameWithoutExtension(sdInfo.Origin);
                if (fileName == "profiles-others")
                {
                    var sd = _testSource.GetStructureDefinition(sdInfo.Canonical);

                    if (sd == null) throw new InvalidOperationException(("Source listed canonical url {0} [source {1}], " +
                        "but could not get structure definition by that url later on!").FormatWith(sdInfo.Canonical, sdInfo.Origin));

                    if (sd.IsConstraint || sd.IsExtension)
                        yield return sd;
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
            var sd = _testSource.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/Questionnaire");
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            var nav = new ElementDefinitionNavigator(sd.Snapshot.Element);

            var generator = new SnapshotGenerator(_testSource, SnapshotGeneratorSettings.Default);

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
            var sd = _testSource.GetStructureDefinition("http://hl7.org/fhir/StructureDefinition/lipidprofile");
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

        private void testExpandElement(string srcProfileUrl, string expandElemPath)
        {
            // Prepare...
            var sd = _testSource.GetStructureDefinition(srcProfileUrl);
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

        private void testExpandElement(StructureDefinition sd, ElementDefinition elem)
        {
            Assert.IsNotNull(elem);
            var elems = sd.Snapshot.Element;
            Assert.IsTrue(elems.Contains(elem));

            var expandElemPath = elem.Path;

            // Test...
            _generator = new SnapshotGenerator(_testSource, _settings);
            var result = _generator.ExpandElement(elems, elem);

            // Verify results
            Debug.WriteLine("\r\nOutput:");
            Debug.Indent();
            Debug.WriteLine(string.Join(Environment.NewLine, result.Where(e => e.Path.StartsWith(expandElemPath)).Select(e => e.Path)));
            Debug.Unindent();

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
                    ? _testSource.GetStructureDefinition(elemProfile)
                    : _testSource.GetStructureDefinitionForCoreType(elemTypeCode);

                Assert.IsNotNull(sdType);
                Assert.IsNotNull(sdType.Snapshot);
                Assert.IsNotNull(sdType.Snapshot.Element);
                Assert.IsTrue(sdType.Snapshot.Element.Count > 0);

                Debug.WriteLine("\r\nType:");
                Debug.Indent();
                Debug.WriteLine(string.Join(Environment.NewLine, sdType.Snapshot.Element.Select(e => e.Path)));
                Debug.Unindent();

                sdType.Snapshot.Rebase(expandElemPath);
                var typeElems = sdType.Snapshot.Element;

                Assert.IsTrue(result.Count == elems.Count + typeElems.Count - 1);
                if (elem.Name == null)
                {
                    Assert.IsTrue(result.Where(e => e.Path.StartsWith(expandElemPath)).Count() == typeElems.Count);
                }
                else
                {
                    // The expanded element represents a slice
                    var nav = new ElementDefinitionNavigator(result);
                    nav.JumpToNameReference(elem.Name);
                    var cnt = 1;
                    Assert.IsTrue(nav.MoveToFirstChild());
                    do
                    {
                        Assert.AreEqual(typeElems[cnt++].Path, nav.Path);
                    } while (nav.MoveToNext());
                    Assert.AreEqual(typeElems.Count, cnt);
                }

                var startPos = result.IndexOf(elem);
                for (int i = 0; i < typeElems.Count; i++)
                {
                    var path = typeElems[i].Path;
                    Assert.IsTrue(result[startPos + i].Path.EndsWith(path, StringComparison.OrdinalIgnoreCase));
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
        private void dumpReferences(StructureDefinition sd, bool differential = false)
        {
            if (sd != null)
            {
                Debug.WriteLine("References for StructureDefinition '{0}' ('{1}')".FormatWith(sd.Name, sd.Url));
                Debug.WriteLine("Base = '{0}'".FormatWith(sd.Base));

                // FhirClient client = new FhirClient("http://fhir2.healthintersections.com.au/open/");
                // var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\snapshot-test\download");
                // if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

                var component = differential ? sd.Differential.Element : sd.Snapshot.Element;
                var profiles = component.EnumerateTypeProfiles().Distinct();

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

        [Conditional("DEBUG")]
        private void dumpBasePaths(StructureDefinition sd)
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
        private void dumpOutcome(OperationOutcome outcome)
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
            if (issue.Diagnostics != null) sb.AppendFormat(" Diagnostics = '{0}'", issue.Diagnostics);
            if (issue.Details != null)
            {
                sb.AppendFormat(" Details: '{0}'", string.Join(" | ", issue.Details.Coding.Select(c => c.Code)));
                if (issue.Details.Text != null) sb.AppendFormat(" : '{0}'", issue.Details.Text);
            }
            var url = issue.GetProfileUrl();
            if (url != null)
            {
                sb.AppendFormat(" Profile: '{0}'", url);
            }
            Debug.Print(sb.ToString());
        }

        // [WMR 20160816] Test custom annotations containing associated base definitions
        class BaseDefAnnotation
        {
            public BaseDefAnnotation(ElementDefinition baseElemDef) { BaseElementDefinition = baseElemDef; }
            public ElementDefinition BaseElementDefinition { get; private set; }
        }

        [TestMethod]
        public void GenerateSnapshotEmitBaseData()
        {
            var source = _testSource;

            var sd = source.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyPatient");
            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.ExpandExternalProfiles = true;
            settings.ExpandUnconstrainedElements = true;
            settings.MergeTypeProfiles = true;
            settings.MarkChanges = true;
            //settings.NormalizeElementBase = true;
            _generator = new SnapshotGenerator(source, settings);

            // [WMR 20160817] Attach custom event handlers
            // Following event is called for each separate (external) profile that is being expanded
            SnapshotBaseProfileHandler profileHandler = (sender, args) =>
            {
                var profile = args.Profile;
                Assert.IsTrue(sd.Url != profile.Url || sd.IsExactly(profile));
                var baseProfile = args.BaseProfile;
                Assert.IsNotNull(baseProfile);
                Debug.WriteLine("[SnapshotBaseProfileHandler] Profile #{0} '{1}' Base = '{2}'".FormatWith(profile.GetHashCode(), profile.Url, profile.Base));
                Debug.Print("[SnapshotBaseProfileHandler] Base Profile #{0} '{1}'".FormatWith(baseProfile.GetHashCode(), baseProfile.Url));
                var rootElem = baseProfile.Snapshot.Element[0];
                Debug.Print("[SnapshotBaseProfileHandler] Base Root element #{0} '{1}'".FormatWith(rootElem.GetHashCode(), rootElem.Path));
                Assert.AreEqual(profile.Base, baseProfile.Url);
            };

            SnapshotElementHandler elementHandler = (sender, args) =>
            {
                var elem = args.Element;
                Assert.IsNotNull(elem);
                var ann = elem.Annotation<BaseDefAnnotation>();
                ElementDefinition baseDef;
                // We want to annotate a reference to the matching base element from the (immediate) base profile.
                // When the snapshot generator expands external profiles, then this handler is called once for each
                // profile in the base hierarchy, starting at the root profile, e.g. Resource => DomainResource => Patient.
                // Each time we recreate the annotation, so the final annotation contains a reference to the immediate base.
                if (ann != null)
                {
                    elem.RemoveAnnotations<BaseDefAnnotation>();
                }
                baseDef = args.BaseElement;
                elem.AddAnnotation(new BaseDefAnnotation(baseDef));
                Debug.Write("[SnapshotElementHandler] #{0} '{1}' - Base: #{2} '{3}'".FormatWith(elem.GetHashCode(), elem.Path, baseDef.GetHashCode(), baseDef.Path));
                Debug.WriteLine(ann != null && ann.BaseElementDefinition != null ? " (old Base: #{0} '{1}')".FormatWith(ann.BaseElementDefinition.GetHashCode(), ann.BaseElementDefinition.Path) : "");
            };

            SnapshotConstraintHandler constraintHandler = (sender, args) =>
            {
                var elem = args.Element as ElementDefinition;
                if (elem != null)
                {
                    var changed = elem.GetChangedByDiff() == true;
                    Debug.Print("[SnapshotConstraintHandler] #{0} '{1}'{2}".FormatWith(elem.GetHashCode(), elem.Path, changed ? " CHANGED!" : null));
                }
            };

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, source, out expanded);

                dumpOutcome(_generator.Outcome);

                assertBaseDefs(expanded, settings);

                var sdBase = source.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
                assertBaseDefs(sdBase, settings);

                var sdExt = source.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");
                assertBaseDefs(sdExt, settings);

                var sdExt1 = source.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension1");
                assertBaseDefs(sdExt1, settings);

                var sdExt2 = source.GetStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension2");
                assertBaseDefs(sdExt2, settings);

            }
            finally
            {
                // Detach event handlers
                _generator.Constraint -= constraintHandler;
                _generator.PrepareElement -= elementHandler;
                _generator.PrepareBaseProfile -= profileHandler;
            }
        }

        private static void assertBaseDefs(StructureDefinition sd, SnapshotGeneratorSettings settings)
        {
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);
            var elems = sd.Snapshot.Element;
            Assert.IsNotNull(elems);
            Assert.IsTrue(elems.Count > 0);

            var isConstraint = sd.ConstrainedType.HasValue;

            Debug.Print("\r\nStructureDefinition '{0}' url = '{1}'", sd.Name, sd.Url);
            Debug.Print("# | Constraint? | Changed? | Element.Path | Element.Base.Path | BaseElement.Path | #Base | Invalid?");
            Debug.Print(new string('=', 100));
            foreach (var elem in elems)
            {
                // Each element should have a valid Base component, unless the profile is a core type/resource definition (no base)
                Assert.IsTrue(!isConstraint || elem.Base != null);

                var ann = elem.Annotation<BaseDefAnnotation>();
                var baseDef = ann != null ? ann.BaseElementDefinition : null;
                Assert.AreNotEqual(elem, baseDef);

                var hasChanges = HasChanges(elem);
                var hasConstraints = false; // baseDef != null || elem.Base != null;
                if (baseDef != null && elem.Base != null)
                {
                    // If normalizing, then elem.Base.Path refers to the defining profile (e.g. DomainResource),
                    // whereas baseDef refers to the immediate base profile (e.g. Patient)
                    Debug.Assert(settings.NormalizeElementBase || ElementDefinitionNavigator.IsCandidateBaseElementPath(elem.Base.Path, baseDef.Path));
                    hasConstraints = HasConstraints(elem, baseDef);
                }
                var isValid = hasChanges == hasConstraints;
                Debug.WriteLine("{0,10}  |  {1}  |  {2,-12}  |  {3,-50}  |  {4,-40}  |  {5,-40}  |  {6,10}  |  {7}",
                    elem.GetHashCode(),
                    hasConstraints ? "+" : "-",
                    GetChangeDescription(elem),
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
        private static bool HasConstraints(ElementDefinition elem, ElementDefinition baseElem)
        {
            var elemClone = (ElementDefinition)elem.DeepCopy();
            var baseClone = (ElementDefinition)baseElem.DeepCopy();

            // Path & Base are expected to differ
            baseClone.Path = elem.Path;
            baseClone.Base = elem.Base;
            
            // Also ignore any Changed extensions on base and diff
            elemClone.ClearAllChangedByDiff();
            baseClone.ClearAllChangedByDiff();

            return !baseClone.IsExactly(elemClone);
        }

        // Returns true if the specified element or any of its' components contain the CHANGED_BY_DIFF_EXT extension
        private static bool HasChanges(ElementDefinition elem)
        {
            return IsChanged(elem)
                || HasChanges(elem.AliasElement)
                || IsChanged(elem.Base)
                || IsChanged(elem.Binding)
                || HasChanges(elem.Code)
                || IsChanged(elem.CommentsElement)
                || HasChanges(elem.ConditionElement)
                || HasChanges(elem.Constraint)
                || IsChanged(elem.DefaultValue)
                || IsChanged(elem.DefinitionElement)
                || IsChanged(elem.Example)
                || HasChanges(elem.Extension)
                || HasChanges(elem.FhirCommentsElement)
                || IsChanged(elem.Fixed)
                || IsChanged(elem.IsModifierElement)
                || IsChanged(elem.IsSummaryElement)
                || IsChanged(elem.LabelElement)
                || HasChanges(elem.Mapping)
                || IsChanged(elem.MaxElement)
                || IsChanged(elem.MaxLengthElement)
                || IsChanged(elem.MaxValue)
                || IsChanged(elem.MeaningWhenMissingElement)
                || IsChanged(elem.MinElement)
                || IsChanged(elem.MinValue)
                || IsChanged(elem.MustSupportElement)
                || IsChanged(elem.NameElement)
                || IsChanged(elem.NameReferenceElement)
                || IsChanged(elem.PathElement)
                || IsChanged(elem.Pattern)
                || HasChanges(elem.RepresentationElement)
                || IsChanged(elem.RequirementsElement)
                || IsChanged(elem.ShortElement)
                || IsChanged(elem.Slicing)
                || HasChanges(elem.Type);
        }

        private static string GetChangeDescription(ElementDefinition element)
        {
            if (IsChanged(element)) { return "Element"; }

            
            if (IsChanged(element.Slicing)) { return "Slicing"; }       // Moved to front
            if (HasChanges(element.Type)) { return "Type"; }            // Moved to front
            if (IsChanged(element.ShortElement)) { return "Short"; }    // Moved to front

            if (HasChanges(element.AliasElement)) { return "Alias"; }
            if (IsChanged(element.Base)) { return "Base"; }
            if (IsChanged(element.Binding)) { return "Binding"; }
            if (HasChanges(element.Code)) { return "Code"; }
            if (IsChanged(element.CommentsElement)) { return "Comments"; }
            if (HasChanges(element.ConditionElement)) { return "Condition"; }
            if (HasChanges(element.Constraint)) { return "Constraint"; }
            if (IsChanged(element.DefaultValue)) { return "DefaultValue"; }
            if (IsChanged(element.DefinitionElement)) { return "Definition"; }
            if (IsChanged(element.Example)) { return "Example"; }
            if (HasChanges(element.Extension)) { return "Extension"; }
            if (HasChanges(element.FhirCommentsElement)) { return "FhirComments"; }
            if (IsChanged(element.Fixed)) { return "Fixed"; }
            if (IsChanged(element.IsModifierElement)) { return "IsModifier"; }
            if (IsChanged(element.IsSummaryElement)) { return "IsSummary"; }
            if (IsChanged(element.LabelElement)) { return "Label"; }
            if (HasChanges(element.Mapping)) { return "Mapping"; }
            if (IsChanged(element.MaxElement)) { return "Max"; }
            if (IsChanged(element.MaxLengthElement)) { return "MaxLength"; }
            if (IsChanged(element.MaxValue)) { return "MaxValue"; }
            if (IsChanged(element.MeaningWhenMissingElement)) { return "MeaningWhenMissing"; }
            if (IsChanged(element.MinElement)) { return "Min"; }
            if (IsChanged(element.MinValue)) { return "MinValue"; }
            if (IsChanged(element.MustSupportElement)) { return "MustSupport"; }
            if (IsChanged(element.NameElement)) { return "Name"; }
            if (IsChanged(element.NameReferenceElement)) { return "NameReference"; }
            if (IsChanged(element.PathElement)) { return "Path"; }
            if (IsChanged(element.Pattern)) { return "Pattern"; }
            if (HasChanges(element.RepresentationElement)) { return "Representation"; }
            if (IsChanged(element.RequirementsElement)) { return "Requirements"; }
            //if (IsChanged(element.ShortElement)) { return "Short"; }
            //if (IsChanged(element.Slicing)) { return "Slicing"; }
            //if (HasChanges(element.Type)) { return "Type"; }
            return string.Empty;
        }

        private static bool HasChanges<T>(IList<T> extendables) where T : IExtendable
        {
            return extendables != null ? extendables.Any(e => IsChanged(e)) : false;
        }

        private static bool IsChanged(IExtendable extendable)
        {
            return extendable!= null && extendable.GetChangedByDiff() == true;
        }

        // [WMR 20160902] NEW
        [TestMethod]
        public void TestExpandCoreResource()
        {
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/BackboneElement");

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/integer");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/positiveInt");

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/HumanName");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Quantity");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/SimpleQuantity");

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Resource");
            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/DomainResource");

            // var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            var sd = _testSource.GetStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Questionnaire");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            // _settings.NormalizeElementBase = true;

            StructureDefinition expanded;
            var result = generateSnapshotAndCompare(sd, _testSource, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            if (!result)
            {
                Debug.Print("Expanded is not exactly equal to original... verifying...");
                result = verifyElementBase(sd, expanded);
            }

            Assert.IsTrue(result);
        }

        // Verify ElementDefinition.Base components
        bool verifyElementBase(StructureDefinition original, StructureDefinition expanded)
        {
            var originalElems = original.Snapshot.Element;
            var expandedElems = expanded.Snapshot.Element;
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
                    //var baseProfile = baseProfileUrl != null ? _testSource.GetStructureDefinition(baseProfileUrl) : null;
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
