/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

// [WMR 20170411] HACK - suppress infinite recursion
// TODO: Properly handle recursive type declarations
// Don't throw exception but emit OperationOutcome issue(s) and continue
#define HACK_STU3_RECURSION

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
using Hl7.Fhir.Utility;
using static Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableSnapshotGeneratorTest
#else
    public class SnapshotGeneratorTest2
#endif
    {
        SnapshotGenerator _generator;
        IResourceResolver _testResolver;
        TimingSource _source;

        readonly SnapshotGeneratorSettings _settings = new SnapshotGeneratorSettings()
        {
            // Throw on unresolved profile references; must include in TestData folder
            GenerateSnapshotForExternalProfiles = true,
            ForceRegenerateSnapshots = true,
            GenerateExtensionsOnConstraints = false,
            GenerateAnnotationsOnConstraints = false,
            GenerateElementIds = true // STU3
        };

        [TestInitialize]
        public void Setup()
        {
            FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            var dirSource = new DirectorySource("TestData/snapshot-test", new DirectorySourceSettings { IncludeSubDirectories = true });
            _source = new TimingSource(dirSource);
            // [WMR 20170810] Order is important!
            // Specify source first to override core defs from
            // TestData\snapshot-test\profiles-resources.xml and profiles-types.xml
            _testResolver = new CachedResolver(
                new MultiResolver(
                    // _source,
                    new ZipSource("specification.zip"),
                    _source));
        }

        [TestMethod]
        public void GenerateExtensionSnapshot()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://fhir.nl/fhir/StructureDefinition/nl-core-address-official");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            Assert.IsNull(_generator.Outcome);

            var elems = expanded.Snapshot.Element;
            Assert.AreEqual(5, elems.Count);
            Assert.AreEqual("Extension", elems[0].Path);
            Assert.AreEqual("Extension.id", elems[1].Path);
            Assert.AreEqual("Extension.extension", elems[2].Path);
            Assert.AreEqual("Extension.url", elems[3].Path);
            Assert.AreEqual(expanded.Url, (elems[3].Fixed as FhirUri)?.Value);
            Assert.AreEqual("Extension.valueBoolean", elems[4].Path);
        }


        [TestMethod]
        public void GenerateSingleSnapshot()
        {
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

            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBasic");

            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyObservation2");

            // [WMR 20161219] Problem: Composition.section element in core resource has name 'section' (b/o name reference)
            // Ambiguous... snapshot generator slicing logic cannot handle this...

            // [WMR 20161222] Example by EK from validator
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/StructureDefinition/DocumentComposition");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Composition");

            // [WMR 20170110] Test problematic extension
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/us-core-direct");

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Account");

            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithExtension");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            // dumpBasePaths(expanded);
            expanded.Snapshot.Element.Dump();
        }

        [TestMethod]
        public void TestChoiceTypeWithMultipleProfileConstraints()
        {
            // [WMR 20161005] The following profile defines several type constraints on Observation.value[x]
            // - Type = Quantity, Profile = WeightQuantity
            // - Type = Quantity, Profile = HeightQuantity
            // - Type = string
            // The snapshot generator should support this without any issues.

            // var tempPath = Path.GetTempPath();
            // var validationTestProfiles = (new Validation.TestProfileArtifactSource()).TestProfiles;
            // var sdHeightQty = validationTestProfiles.FirstOrDefault(s => s.Url == "http://validationtest.org/fhir/StructureDefinition/HeightQuantity");
            // File.WriteAllText(Path.Combine(tempPath, "HeightQuantity.StructureDefinition.xml"), FhirSerializer.SerializeResourceToXml(sdHeightQty));
            // var sdWeightQty = validationTestProfiles.FirstOrDefault(s => s.Url == "http://validationtest.org/fhir/StructureDefinition/WeightQuantity");
            // File.WriteAllText(Path.Combine(tempPath, "WeightQuantity.StructureDefinition.xml"), FhirSerializer.SerializeResourceToXml(sdWeightQty));

            var sd = _testResolver.FindStructureDefinition(@"http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        public void GenerateRepeatedSnapshot()
        {
            // [WMR 20161005] This generated exceptions in an early version of the snapshot generator (fixed)

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/MeasureReport");
            generateSnapshotAndCompare(sd, out var expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/clinicaldocument");
            generateSnapshotAndCompare(sd, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        // [WMR 20170424] For debugging SnapshotBaseComponentGenerator
        [TestMethod]
        public void TestFullyExpandCoreOrganization()
        {
            // [WMR 20161005] This simulates custom Forge post-processing logic
            // i.e. perform a regular snapshot expansion, then explicitly expand all complex elements (esp. those without any differential constraints)

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Organization");
            var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Organization);
            Assert.IsNotNull(sd);
            generateSnapshot(sd);
            Assert.IsTrue(sd.HasSnapshot);
            var elems = sd.Snapshot.Element;

            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            var expanded = fullyExpand(sd.Snapshot.Element, issues);

            Assert.IsNotNull(expanded);
            dumpBaseElems(expanded);

            Assert.IsNull(_generator.Outcome);
        }

        // [WMR 20180115] NEW - Replacement for expandAllComplexElements (OBSOLETE)
        // Expand all elements with complex type and no children
        IList<ElementDefinition> fullyExpand(IList<ElementDefinition> elements, List<OperationOutcome.IssueComponent> issues = null)
        {
            var nav = new ElementDefinitionNavigator(elements);
            // Skip root element
            if (nav.MoveToFirstChild())
            {
                if (_generator == null)
                {
                    _generator = new SnapshotGenerator(_testResolver, _settings);
                }
                fullyExpandElement(nav, issues);
                return nav.Elements;
            }
            return elements;
        }

        // Expand current element if it has a complex type and no children (recursively)
        void fullyExpandElement(ElementDefinitionNavigator nav, List<OperationOutcome.IssueComponent> issues)
        {
            if (nav.HasChildren || (isExpandableElement(nav.Current) && _generator.ExpandElement(nav)))
            {
                if (issues != null && _generator.Outcome != null)
                {
                    issues.AddRange(_generator.Outcome.Issue);
                }

                Debug.Print($"[{nameof(fullyExpandElement)}] " + nav.Path);
                var bm = nav.Bookmark();
                if (nav.MoveToFirstChild())
                {
                    do
                    {
                        fullyExpandElement(nav, issues);
                    } while (nav.MoveToNext());
                    Assert.IsTrue(nav.ReturnToBookmark(bm));
                }
            }
        }

        static bool isExpandableElement(ElementDefinition element)
        {
#if HACK_STU3_RECURSION
            // [WMR 20170328] DEBUG HACK
            // Prevent recursion:
            // - Identifier.assigner : Reference
            // - Reference.identifier : Identifier
            if (element.Path == "Reference.identifier"
                || element.Base?.Path == "Reference.identifier"
                // [WMR 20170424] Added
                || (element.Base?.Path.EndsWith(".reference.identifier") ?? false)
                || (element.Base?.Path == "Identifier.assigner.identifier")
                || (element.Base?.Path.EndsWith(".identifier.assigner.identifier") ?? false)
            )
            {
                Debug.Print($"[{nameof(isExpandableElement)}] RECURSION HACK: skip expansion for element: '{element.Path}'");
                return false;
            }
#endif

            var type = element.PrimaryType();

            if (type == null || element.Type.Select(t => t.Code).Distinct().Count() != 1) { return false; }

            var typeName = type?.Code;
            return !String.IsNullOrEmpty(typeName)
                   // Expand complex datatypes and resources
                   && isComplexDataTypeOrResource(typeName)
                   && (
                        // Only expand extension elements with a custom name or profile
                        // Do NOT expand the core Extension.extension element, as this will trigger infinite recursion
                        typeName != FHIRAllTypes.Extension.GetLiteral()
                        || !string.IsNullOrEmpty(type.Profile)
                        || element.SliceName != null
                   );
        }

        // [WMR 20180116] Returns true for complex datatypes and resources, or false otherwise
        static bool isComplexDataTypeOrResource(string typeName) => !ModelInfo.IsPrimitive(typeName);

        static bool isComplexDataTypeOrResource(FHIRAllTypes type) => !ModelInfo.IsPrimitive(type);


        // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
        [TestMethod]
        public void TestFullyExpandCorePatient()
        {
            // [WMR 20180115] Iteratively expand all complex elements
            // 1. First generate regular snapshot
            // 2. Re-iterate elements, expand complex elements w/o children (recursively)

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(sd);

            StructureDefinition snapshot = null;
            generateSnapshotAndCompare(sd, out snapshot);
            Assert.IsNotNull(snapshot);
            Assert.IsTrue(snapshot.HasSnapshot);

            var snapElems = snapshot.Snapshot.Element;
            Debug.WriteLine($"Default snapshot: {snapElems.Count} elements");
            dumpBaseElems(snapElems);
            Assert.AreEqual(52, snapElems.Count);

            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            var fullElems = fullyExpand(snapElems, issues);
            Debug.WriteLine($"Full expansion: {fullElems.Count} elements");
            dumpBaseElems(fullElems);
            Assert.AreEqual(310, fullElems.Count);
            Assert.AreEqual(issues.Count, 0);

            // Verify
            for (int j = 1; j < fullElems.Count; j++)
            {
                if (isExpandableElement(fullElems[j]))
                {
                    verifyExpandElement(fullElems[j], fullElems, fullElems);
                }
            }
        }

        // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
        // Note: result is different from TestCoreOrganizationNL, contains more elements - correct!
        // Older approach was flawed, e.g. see exclusion for Organization.type
        [TestMethod]
        public void TestFullyExpandNLCoreOrganization()
        {
            // core-organization-nl references extension core-address-nl
            // BUG: expanded extension child elements have incorrect .Base.Path ...?!
            // e.g. Organization.address.type - Base = Organization.address.use
            // Fixed by adding conditional to copyChildren

            var sd = _testResolver.FindStructureDefinition(@"http://fhir.nl/fhir/StructureDefinition/nl-core-organization");
            Assert.IsNotNull(sd);

            StructureDefinition snapshot = null;
            // generateSnapshotAndCompare(sd, out snapshot);

            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out snapshot);

                Assert.IsNotNull(snapshot);
                Assert.IsTrue(snapshot.HasSnapshot);

                var snapElems = snapshot.Snapshot.Element;
                Debug.WriteLine($"Default snapshot: {snapElems.Count} elements");
                dumpBaseElems(snapElems);
                dumpIssues(_generator.Outcome?.Issue);
                Assert.AreEqual(62, snapElems.Count);
                Assert.IsNull(_generator.Outcome);

                var issues = new List<OperationOutcome.IssueComponent>();
                var fullElems = fullyExpand(snapElems, issues);
                Debug.WriteLine($"Full expansion: {fullElems.Count} elements");
                dumpBaseElems(fullElems);
                dumpIssues(issues);
                Assert.AreEqual(347, fullElems.Count);
                Assert.AreEqual(0, issues.Count);

                // Verify
                for (int j = 1; j < fullElems.Count; j++)
                {
                    if (isExpandableElement(fullElems[j]))
                    {
                        verifyExpandElement(fullElems[j], fullElems, fullElems);
                    }
                }
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
        }

        static void beforeExpandElementHandler_DEBUG(object sender, SnapshotExpandElementEventArgs e)
        {
            Debug.Print($"[beforeExpandElementHandler_DEBUG] #{e.Element.GetHashCode()} '{e.Element.Path}' - HasChildren = {e.HasChildren} - MustExpand = {e.MustExpand}");
        }

        [TestMethod]
        public void TestSnapshotRecursionChecker()
        {
            // Following structuredefinition has a recursive element type profile
            // Verify that the snapshot generator detects recursion and aborts with exception

            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyBundle");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            bool exceptionRaised = false;
            try
            {
                generateSnapshotAndCompare(sd, out var expanded);
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

        // [WMR 20170424] Add qicore-encounter.xml (STU3) as separate content file
        // Source: http://build.fhir.org/ig/cqframework/qi-core/StructureDefinition-qicore-encounter.xml.html
        [TestMethod]
        public void GenerateDerivedProfileSnapshot()
        {
            // [WMR 20161005] Verify that the snapshot generator supports profiles on profiles

            // cqif-guidanceartifact profile is derived from cqif-knowledgemodule
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-encounter");
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/us/qicore/StructureDefinition/qicore-encounter");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        void assertContainsElement(StructureDefinition sd, string path, string name = null, string elementId = null)
        {
            Assert.IsNotNull(sd);

            Assert.IsNotNull(sd.Differential);
            Assert.IsNotNull(sd.Differential.Element);
            Assert.IsTrue(sd.Differential.Element.Count > 0);

            // Verify that the differential component contains a matching element
            assertContainsElement(sd.Differential, path, name);
            assertContainsElement(sd.Snapshot, path, name, elementId);
        }

        void assertContainsElement(IElementList elements, string path, string name = null, string elementId = null)
        {
            var label = elements is StructureDefinition.DifferentialComponent ? "differential" : "snapshot";
            Assert.IsNotNull(elements);
            var matches = elements.Element.Where(e => e.Path == path && e.SliceName == name).ToArray();
            var cnt = matches.Length;
            Assert.IsTrue(cnt > 0, $"Expected element is missing from {label} component. Path = '{path}', name = '{name}'.");
            Assert.IsTrue(cnt == 1, $"Found multiple matching elements in {label} component for Path = '{path}', name = '{name}'.");
            var elem = matches[0];
            if (_settings.GenerateElementIds && elementId != null)
            {
                Assert.AreEqual(elementId, elem.ElementId, $"Invalid elementId in {label} component. Expected = '{elementId}', actual = '{elem.ElementId}'.");
            }
        }

        StructureDefinition generateSnapshot(string url, Action<StructureDefinition> preprocessor = null)
        {
            var structure = _testResolver.FindStructureDefinition(url);
            Assert.IsNotNull(structure);
            Assert.IsTrue(structure.HasSnapshot);
            preprocessor?.Invoke(structure);
            generateSnapshotAndCompare(structure, out var expanded);
            dumpOutcome(_generator.Outcome);
            return expanded;
        }

        static void insertElementsBefore(StructureDefinition structure, ElementDefinition insertBefore, params ElementDefinition[] inserts)
            => insertElementsBefore(structure.Differential.Element, insertBefore, inserts);

        static void insertElementsBefore(List<ElementDefinition> elements, ElementDefinition insertBefore, params ElementDefinition[] inserts)
        {
            var idx = elements.FindIndex(e => e.Path == insertBefore.Path && e.SliceName == insertBefore.SliceName);
            Assert.AreNotEqual(-1, idx, $"Warning! insertBefore element is missing. Path = '{insertBefore.Path}', Name = '{insertBefore.SliceName}'.");
            foreach (var insert in inserts)
            {
                var idx2 = elements.FindIndex(e => e.Path == insert.Path && e.SliceName == insert.SliceName);
                Assert.AreEqual(-1, idx2, $"Warning! insert element is already present. Path = '{insert.Path}', Name = '{insert.SliceName}'.");
            }
            elements.InsertRange(idx, inserts);
        }

        static void insertElementsBefore(StructureDefinition structure, string insertBeforePath, int elemIndex, params ElementDefinition[] inserts)
            => insertElementsBefore(structure.Differential.Element, insertBeforePath, elemIndex, inserts);

        static void insertElementsBefore(List<ElementDefinition> elements, string insertBeforePath, int elemIndex, params ElementDefinition[] inserts)
        {
            var idx = -1;
            do
            {
                idx = elements.FindIndex(idx + 1, e => e.Path == insertBeforePath);
                Assert.AreNotEqual(-1, idx, $"Warning! insertBefore element is missing. Path = '{insertBeforePath}', Index = '{elemIndex}'.");
            } while (--elemIndex > 0);

            foreach (var insert in inserts)
            {
                var idx2 = elements.FindIndex(e => e.Path == insert.Path && e.SliceName == insert.SliceName);
                Assert.AreEqual(-1, idx2, $"Warning! insert element is already present. Path = '{insert.Path}', Name = '{insert.SliceName}'.");
            }
            elements.InsertRange(idx, inserts);

        }


        // [WMR 20170412] Fixed
        [TestMethod]
        public void GeneratePatientWithExtensionsSnapshot()
        {
            // [WMR 20161005] Very complex set of examples by Chris Grenz
            // https://github.com/chrisgrenz/FHIR-Primer/blob/master/profiles/patient-extensions-profile.xml
            // Manually downgraded from FHIR v1.4.0 to v1.0.2

            StructureDefinition sd;
            ElementVerifier verifier;

            // [WMR 20170421] Chris Grenz examples define non-standard slice names, e.g. "type.value[x]"
            _settings.GenerateElementIds = true;

            // http://example.com/fhir/StructureDefinition/patient-legal-case
            // http://example.com/fhir/StructureDefinition/patient-legal-case-lead-counsel

            // [WMR 20170424] Corrected element ids

            // Verify complex extension used by patient-with-extensions profile
            // patient-research-authorization-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/StructureDefinition/patient-research-authorization");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Extension.extension", null, "Extension.extension");
            verifier.VerifyElement("Extension.extension", "type", "Extension.extension:type");
            verifier.VerifyElement("Extension.extension.url", null, "Extension.extension:type.url", new FhirUri("type"));
            verifier.VerifyElement("Extension.extension", "flag", "Extension.extension:flag");
            verifier.VerifyElement("Extension.extension.url", null, "Extension.extension:flag.url", new FhirUri("flag"));
            verifier.VerifyElement("Extension.extension", "date", "Extension.extension:date");
            verifier.VerifyElement("Extension.extension.url", null, "Extension.extension:date.url", new FhirUri("date"));
            verifier.VerifyElement("Extension.url", null, null, new FhirUri(sd.Url));

            // Basic Patient profile that references a set of extensions
            // patient-extensions-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.extension", null, "Patient.extension");
            verifier.VerifyElement("Patient.extension", "doNotCall", "Patient.extension:doNotCall");
            verifier.VerifyElement("Patient.extension", "legalCase", "Patient.extension:legalCase");

            // [WMR 20170614] Fixed; element id for type slices is based on original element name ending with "[x]"
            // verifier.VerifyElement("Patient.extension.valueBoolean", null, "Patient.extension:legalCase.valueBoolean");
            // verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.valueBoolean.extension");
            // verifier.VerifyElement("Patient.extension.valueBoolean.extension", "leadCounsel", "Patient.extension:legalCase.valueBoolean.extension:leadCounsel");
            verifier.VerifyElement("Patient.extension.valueBoolean", null, "Patient.extension:legalCase.value[x]");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.value[x].extension");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", "leadCounsel", "Patient.extension:legalCase.value[x].extension:leadCounsel");

            verifier.VerifyElement("Patient.extension", "religion", "Patient.extension:religion");
            verifier.VerifyElement("Patient.extension", "researchAuth", "Patient.extension:researchAuth");

            // Each of the following profiles is derived from the previous profile

            // patient-name-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-name-slice"
                , structure => insertElementsBefore(structure,
                     "Patient.name.use", 2,
                     // Add named parent slicing entry
                     new ElementDefinition() { Path = "Patient.name", SliceName = "maidenName" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.name", null, "Patient.name");
            verifier.VerifyElement("Patient.name", "officialName", "Patient.name:officialName");
            verifier.VerifyElement("Patient.name.text", null, "Patient.name:officialName.text");
            verifier.VerifyElement("Patient.name.family", null, "Patient.name:officialName.family");
            verifier.VerifyElement("Patient.name.given", null, "Patient.name:officialName.given");
            verifier.VerifyElement("Patient.name.use", null, "Patient.name:officialName.use");
            Assert.AreEqual((verifier.CurrentElement.Fixed as Code)?.Value, "official");
            verifier.VerifyElement("Patient.name", "maidenName", "Patient.name:maidenName");
            verifier.VerifyElement("Patient.name.use", null, "Patient.name:maidenName.use");
            Assert.AreEqual((verifier.CurrentElement.Fixed as Code)?.Value, "maiden");
            verifier.VerifyElement("Patient.name.family", null, "Patient.name:maidenName.family");

            // patient-telecom-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-telecom-slice"
                , structure => insertElementsBefore(structure,
                     // new ElementDefinition() { Path = "Patient.telecom.system", SliceName = "workEmail.system" },
                     "Patient.telecom.system", 4,
                     // Add named parent slicing entry
                     new ElementDefinition() { Path = "Patient.telecom", SliceName = "workEmail" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.telecom", null, "Patient.telecom");
            verifier.VerifyElement("Patient.telecom", "homePhone", "Patient.telecom:homePhone");
            verifier.VerifyElement("Patient.telecom.system", null, "Patient.telecom:homePhone.system", new Code("phone"));
            verifier.VerifyElement("Patient.telecom.use", null, "Patient.telecom:homePhone.use", new Code("home"));
            verifier.VerifyElement("Patient.telecom", "mobilePhone", "Patient.telecom:mobilePhone");
            verifier.VerifyElement("Patient.telecom.system", null, "Patient.telecom:mobilePhone.system", new Code("phone"));
            verifier.VerifyElement("Patient.telecom.use", null, "Patient.telecom:mobilePhone.use", new Code("mobile"));
            verifier.VerifyElement("Patient.telecom", "homeEmail", "Patient.telecom:homeEmail");
            verifier.VerifyElement("Patient.telecom.system", null, "Patient.telecom:homeEmail.system", new Code("email"));
            verifier.VerifyElement("Patient.telecom.use", null, "Patient.telecom:homeEmail.use", new Code("home"));
            verifier.VerifyElement("Patient.telecom", "workEmail", "Patient.telecom:workEmail");
            verifier.VerifyElement("Patient.telecom.system", null, "Patient.telecom:workEmail.system", new Code("email"));
            verifier.VerifyElement("Patient.telecom.use", null, "Patient.telecom:workEmail.use", new Code("work"));
            verifier.VerifyElement("Patient.telecom", "pager", "Patient.telecom:pager");
            verifier.VerifyElement("Patient.telecom.system", null, "Patient.telecom:pager.system", new Code("pager"));

            // Original snapshot contains constraints for both deceased[x] and deceasedDateTime - invalid!
            // Generated snapshot merges both constraints to deceasedDateTime type slice
            // patient-deceasedDatetime-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-deceasedDatetime-slice");
            assertContainsElement(sd.Differential, "Patient.deceased[x]");                  // Differential contains a type slice on deceased[x]
            // Assert.IsFalse(sd.Snapshot.Element.Any(e => e.Path == "Patient.deceased[x]"));  // Snapshot only contains renamed element constraint
            // assertContainsElement(sd, "Patient.deceasedDateTime", null, "Patient.deceasedDateTime");
            verifier.VerifyElement("Patient.deceased[x]", null, "Patient.deceased[x]");

            // patient-careprovider-type-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-careprovider-type-slice");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.careProvider", null, "Patient.careProvider");
            verifier.VerifyElement("Patient.careProvider", "organizationCare", "Patient.careProvider:organizationCare");
            verifier.VerifyElement("Patient.careProvider", "practitionerCare", "Patient.careProvider:practitionerCare");

            // Verify re-slicing
            // patient-careprovider-type-reslice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-careprovider-type-reslice");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.careProvider", null, "Patient.careProvider");
            verifier.VerifyElement("Patient.careProvider", "organizationCare", "Patient.careProvider:organizationCare");
            verifier.VerifyElement("Patient.careProvider", "organizationCare/teamCare", "Patient.careProvider:organizationCare/teamCare");
            verifier.VerifyElement("Patient.careProvider", "practitionerCare", "Patient.careProvider:practitionerCare");

            // Identifier Datatype profile
            // patient-mrn-id-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-mrn-id");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Identifier", null, "Identifier");
            verifier.VerifyElement("Identifier.system", null, "Identifier.system", new FhirUri(@"http://example.com/fhir/localsystems/PATIENT-ID-MRN"));

            // Verify inline re-slicing
            // Profile slices identifier and also re-slices the "mrn" slice
            // patient-identifier-profile-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-slice-by-profile"
                , structure => insertElementsBefore(structure,
                     "Patient.identifier.use", 1,
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.identifier", SliceName = "mrn/officialMRN" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.VerifyElement("Patient.identifier", "mrn/officialMRN", "Patient.identifier:mrn/officialMRN");
            verifier.VerifyElement("Patient.identifier.use", null, "Patient.identifier:mrn/officialMRN.use", new Code("official"));
            verifier.VerifyElement("Patient.identifier", "mdmId", "Patient.identifier:mdmId");

            // Verify constraints on named slice in base profile
            // patient-identifier-slice-extension-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-identifier-subslice"
                , structure => insertElementsBefore(structure,
                     "Patient.identifier.extension", 1,
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.identifier", SliceName = "mrn" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.AssertSlicing("system", ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.AssertSlicing("use", ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier.extension", null, "Patient.identifier:mrn.extension");
            verifier.VerifyElement("Patient.identifier.extension", "issuingSite", "Patient.identifier:mrn.extension:issuingSite");
            verifier.VerifyElement("Patient.identifier.use", null, "Patient.identifier:mrn.use");
            verifier.VerifyElement("Patient.identifier.type", null, "Patient.identifier:mrn.type");
            verifier.VerifyElement("Patient.identifier.system", null, "Patient.identifier:mrn.system", new FhirUri(@"http://example.com/fhir/localsystems/PATIENT-ID-MRN"));
            verifier.VerifyElement("Patient.identifier.value", null, "Patient.identifier:mrn.value");
            verifier.VerifyElement("Patient.identifier.period", null, "Patient.identifier:mrn.period");
            verifier.VerifyElement("Patient.identifier.assigner", null, "Patient.identifier:mrn.assigner");
            verifier.VerifyElement("Patient.identifier", "mrn/officialMRN", "Patient.identifier:mrn/officialMRN");
            verifier.VerifyElement("Patient.identifier", "mdmId", "Patient.identifier:mdmId");

            // Verify extension re-slice
            // patient-research-auth-reslice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-research-auth-reslice"
                , structure => insertElementsBefore(structure,
                     // new ElementDefinition() { Path = "Patient.extension.extension.value[x]", SliceName = "researchAuth/grandfatheredResAuth.type.value[x]" },
                     "Patient.extension.extension.value[x]", 1,
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.extension", SliceName = "researchAuth/grandfatheredResAuth" },
                     new ElementDefinition() { Path = "Patient.extension.extension", SliceName = "type" }
                     // new ElementDefinition() { Path = "Patient.extension.extension", Name = "researchAuth/grandfatheredResAuth.type" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);

            verifier.VerifyElement("Patient.extension", null, "Patient.extension");
            verifier.VerifyElement("Patient.extension", "doNotCall", "Patient.extension:doNotCall");
            verifier.VerifyElement("Patient.extension", "legalCase", "Patient.extension:legalCase");


            // [WMR 20170614] Fixed; element id for type slices is based on original element name ending with "[x]"
            // verifier.VerifyElement("Patient.extension.valueBoolean", null, "Patient.extension:legalCase.valueBoolean");
            // verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.valueBoolean.extension");
            // verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.valueBoolean.extension:leadCounsel");
            verifier.VerifyElement("Patient.extension.valueBoolean", null, "Patient.extension:legalCase.value[x]");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.value[x].extension");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.value[x].extension:leadCounsel");

            verifier.VerifyElement("Patient.extension", "religion", "Patient.extension:religion");
            verifier.VerifyElement("Patient.extension", "researchAuth", "Patient.extension:researchAuth");
            // Note: in the original snapshot, the "researchAuth" complex extension slice is fully expanded (child extensions: type, flag, date)
            // However this is not necessary, as there are no child constraints on the extension

            // [WMR 20161216] TODO: Merge slicing entry
            verifier.AssertSlicing("type.value[x]", ElementDefinition.SlicingRules.Open, null);

            // [WMR 20161208] TODO...

            // "researchAuth/grandfatheredResAuth" represents a reslice of the base extension "researchAuth" (0...*)
            verifier.VerifyElement("Patient.extension", "researchAuth/grandfatheredResAuth", "Patient.extension:researchAuth/grandfatheredResAuth");

            // [WMR 20161216] TODO: Merge slicing entry
            verifier.VerifyElement("Patient.extension.extension", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension");
            // [WMR 20170412] Slicing component is inherited from Extension.extension core element definition
            // STU3: Defined as { type = "value", path = "url", ordered = null }
            verifier.AssertSlicing("url", ElementDefinition.SlicingRules.Open, null);

            // The reslice "researchAuth/grandfatheredResAuth" has a child element constraint on "type.value[x]"
            // Therefore the complex extension is fully expanded (child extensions: type, flag, date)
            verifier.VerifyElement("Patient.extension.extension", "type", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.url", new FhirUri("type"));
            // Child constraints on "type.value[x]" merged from differential
            verifier.VerifyElement("Patient.extension.extension.value[x]", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.value[x]");
            verifier.VerifyElement("Patient.extension.extension", "flag", "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag.url", new FhirUri("flag"));
            verifier.VerifyElement("Patient.extension.extension", "date", "Patient.extension:researchAuth/grandfatheredResAuth.extension:date");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:date.url", new FhirUri("date"));
            verifier.VerifyElement("Patient.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.url", new FhirUri(@"http://example.com/fhir/StructureDefinition/patient-research-authorization"));

            // Slices inherited from base profile with url http://example.com/fhir/SD/patient-identifier-subslice
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.AssertSlicing("system", ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.AssertSlicing("use", ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier.extension", null, "Patient.identifier:mrn.extension");
            verifier.VerifyElement("Patient.identifier.extension", null, "Patient.identifier:mrn.extension:issuingSite");
            verifier.VerifyElement("Patient.identifier.use", null, "Patient.identifier:mrn.use");
            verifier.VerifyElement("Patient.identifier.type", null, "Patient.identifier:mrn.type");
            verifier.VerifyElement("Patient.identifier.system", null, "Patient.identifier:mrn.system", new FhirUri(@"http://example.com/fhir/localsystems/PATIENT-ID-MRN"));
            verifier.VerifyElement("Patient.identifier.value", null, "Patient.identifier:mrn.value");
            verifier.VerifyElement("Patient.identifier.period", null, "Patient.identifier:mrn.period");
            verifier.VerifyElement("Patient.identifier.assigner", null, "Patient.identifier:mrn.assigner");
            verifier.VerifyElement("Patient.identifier", "mrn/officialMRN", "Patient.identifier:mrn/officialMRN");
            verifier.VerifyElement("Patient.identifier", "mdmId", "Patient.identifier:mdmId");
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
            Assert.AreEqual(FHIRAllTypes.Extension.GetLiteral(), extensionType.Code);
            Assert.IsNotNull(extensionType.Profile);
            var extDefUrl = extensionType.Profile;
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/MyLocationExtension", extDefUrl);
            var ext = _testResolver.FindStructureDefinition(extDefUrl);
            Assert.IsNotNull(ext);
            Assert.IsNull(ext.Snapshot);

            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        public void GenerateSnapshotIgnoreMissingExternalProfile()
        {
            // [WMR 20161005] Verify that the snapshot generator gracefully handles unresolved external profile references
            // This should generate a partial snapshot and OperationOutcome Issues for each missing dependency.

            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyObservation");
            Assert.IsNotNull(sd);

            dumpReferences(sd, true);

            // Explicitly disable expansion of external snapshots
            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateSnapshotForExternalProfiles = false;
            _generator = new SnapshotGenerator(_testResolver, settings);

            generateSnapshotAndCompare(sd, out var expanded);

            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            Assert.IsNotNull(outcome);
            Assert.AreEqual(3, outcome.Issue.Count);

            assertIssue(outcome.Issue[0], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyMissingExtension");
            // Note: the extension reference to MyExtensionNoSnapshot should not generate an Issue,
            // as the profile only needs to merge the extension definition root element (no full expansion)
            assertIssue(outcome.Issue[1], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyIdentifier");
            assertIssue(outcome.Issue[2], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyCodeableConcept");
        }

        static void assertIssue(OperationOutcome.IssueComponent issue, Issue expected, string diagnostics = null, params string[] location)
        {
            Assert.IsNotNull(issue);
            Assert.AreEqual(expected.Type, issue.Code);
            Assert.AreEqual(expected.Severity, issue.Severity);
            Assert.AreEqual(expected.Code.ToString(), issue.Details.Coding[0].Code);
            Assert.IsNotNull(issue.Extension);
            if (diagnostics != null)
            {
                Assert.AreEqual(diagnostics, issue.Diagnostics);
            }
            if (location != null && location.Length > 0)
            {
                Assert.IsTrue(location.SequenceEqual(issue.Location));
            }
        }

        // [WMR 20160721] Following profiles are not yet handled (TODO)
        //      private readonly string[] skippedProfiles =
        //      {
        //	// Differential defines constraint on MedicationOrder.reason[x]
        //	// Snapshot renames this element to MedicationOrder.reasonCodeableConcept - is this mandatory?
        //	// @"http://hl7.org/fhir/StructureDefinition/gao-medicationorder",
        //};
        [TestMethod, Ignore]
        public void GenerateSnapshot()
        {
            var sw = new Stopwatch();
            int count = 0;
            _source.Reset();
            sw.Start();

            foreach (var original in findConstraintStrucDefs()
            // [WMR 20160721] Skip invalid profiles
            // .Where(sd => !skippedProfiles.Contains(sd.Url))
            )
            {
                // nothing to test, original does not have a snapshot
                if (original.Snapshot == null) continue;

                Debug.WriteLine("Generating Snapshot for " + original.Url);

                generateSnapshotAndCompare(original);
                count++;
            }

            sw.Stop();
            _source.ShowDuration(count, sw.Elapsed);
        }

        StructureDefinition generateSnapshot(StructureDefinition original)
        {
            if (_generator == null)
            {
                _generator = new SnapshotGenerator(_testResolver, _settings);
            }

            var expanded = (StructureDefinition)original.DeepCopy();
            Assert.IsTrue(original.IsExactly(expanded));

            _generator.Update(expanded);

            return expanded;
        }

        bool generateSnapshotAndCompare(StructureDefinition original)
        {
            return generateSnapshotAndCompare(original, out var expanded);
        }

        bool generateSnapshotAndCompare(StructureDefinition original, out StructureDefinition expanded)
        {
            expanded = generateSnapshot(original);

            var areEqual = original.IsExactly(expanded);

            // [WMR 20160803] Always save output to separate file, convenient for debugging
            // if (!areEqual)
            // {
            var tempPath = Path.GetTempPath();
            var xmlSer = new FhirXmlSerializer();
            File.WriteAllText(Path.Combine(tempPath, "snapshotgen-source.xml"), xmlSer.SerializeToString(original));
            File.WriteAllText(Path.Combine(tempPath, "snapshotgen-dest.xml"), xmlSer.SerializeToString(expanded));
            // }

            // Assert.IsTrue(areEqual);
            Debug.WriteLineIf(original.HasSnapshot && !areEqual, "WARNING: '{0}' Expansion ({1} elements) is not equal to original ({2} elements)!".FormatWith(
                original.Name, original.HasSnapshot ? original.Snapshot.Element.Count : 0, expanded.HasSnapshot ? expanded.Snapshot.Element.Count : 0)
            );

            return areEqual;
        }

        IEnumerable<StructureDefinition> findConstraintStrucDefs()
        {
#if true
            if (_source.Source is DirectorySource dirSource)
            {
                //var summaries = dirSource.ListSummaries(ResourceType.StructureDefinition);
                //summaries = summaries.Where(s => Path.GetFileNameWithoutExtension(s.Origin) == "profiles-others");
                var path = Path.GetFullPath(@"TestData\snapshot-test\WMR\profiles-others.xml");
                var summaries = dirSource.ListSummaries(ResourceType.StructureDefinition).FromFile(path);
                foreach (var summary in summaries)
                {
                    var canonical = summary.GetConformanceCanonicalUrl();
                    if (canonical != null)
                    {
                        yield return _source.ResolveByCanonicalUri(canonical) as StructureDefinition;
                    }
                }
            }
#else
            var testSDs = _source.FindAll<StructureDefinition>();

            foreach (var testSD in testSDs)
            {
                // var sdInfo = testSD.Annotation<OriginAnnotation>();
                // [WMR 20160721] Select all profiles in profiles-others.xml
                // var fileName = Path.GetFileNameWithoutExtension(sdInfo.Origin);
                var fileName = Path.GetFileNameWithoutExtension(testSD.GetOrigin());
                if (fileName == "profiles-others")
                {
                    //var sd = _testResolver.FindStructureDefinition(sdInfo.Canonical);

                    //if (sd == null) throw new InvalidOperationException(("Source listed canonical url {0} [source {1}], " +
                    //    "but could not get structure definition by that url later on!").FormatWith(sdInfo.Canonical, sdInfo.Origin));

                    if (testSD.IsConstraint || testSD.IsExtension)
                        yield return testSD;
                }
            }
#endif
        }

        // Unit tests for DifferentialTreeConstructor

        [TestMethod]
        public void TestDifferentialTree()
        {
            var e = new List<ElementDefinition>();

            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C1", SliceName = "C1-A" }); // First slice of A.B.C1
            e.Add(new ElementDefinition() { Path = "A.B.C2" });
            e.Add(new ElementDefinition() { Path = "A.B", SliceName = "B-A" }); // First slice of A.B
            e.Add(new ElementDefinition() { Path = "A.B.C1.D" });
            e.Add(new ElementDefinition() { Path = "A.D.F" });

            var tree = DifferentialTreeConstructor.MakeTree(e);
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
        public void TestDifferentialTreeMultipleRoots()
        {
            var elements = new List<ElementDefinition>();

            elements.Add(new ElementDefinition() { Path = "Patient.identifier" });
            elements.Add(new ElementDefinition() { Path = "Patient" });

            bool exceptionRaised = false;
            try
            {
                var tree = DifferentialTreeConstructor.MakeTree(elements);
            }
            catch (InvalidOperationException ex)
            {
                Debug.Print(ex.Message);
                exceptionRaised = true;
            }
            Assert.IsTrue(exceptionRaised);
        }

        // [WMR 20161012] Advanced unit test for DifferentialTreeConstructor with resliced input
        [TestMethod]
        public void TestDifferentialTreeForReslice()
        {
            var elements = new List<ElementDefinition>();

            elements.Add(new ElementDefinition() { Path = "Patient.identifier" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", SliceName = "A" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.use" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", SliceName = "B/1" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.type" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", SliceName = "B/2" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.period.start" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", SliceName = "C/1" });

            var tree = DifferentialTreeConstructor.MakeTree(elements);
            Assert.IsNotNull(tree);
            Debug.Print(string.Join(Environment.NewLine, tree.Select(e => $"{e.Path} : '{e.SliceName}'")));

            Assert.AreEqual(10, tree.Count);
            var verifier = new ElementVerifier(tree, _settings);

            verifier.VerifyElement("Patient");                      // Added: root element
            verifier.VerifyElement("Patient.identifier");
            verifier.VerifyElement("Patient.identifier", "A");
            verifier.VerifyElement("Patient.identifier.use");
            verifier.VerifyElement("Patient.identifier", "B/1");
            verifier.VerifyElement("Patient.identifier.type");
            verifier.VerifyElement("Patient.identifier", "B/2");
            verifier.VerifyElement("Patient.identifier.period");    // Added: parent element
            verifier.VerifyElement("Patient.identifier.period.start");
            verifier.VerifyElement("Patient.identifier", "C/1");
        }

#if false
        [TestMethod]
        public void DebugDifferentialTree()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/SD/patient-research-auth-reslice");
            Assert.IsNotNull(sd);
            var tree = sd.Differential.MakeTree();
            Assert.IsNotNull(tree);
            Debug.Print(string.Join(Environment.NewLine, tree.Select(e => $"{e.Path} : '{e.SliceName}'")));
        }
#endif

        // [WMR 20160802] Unit tests for SnapshotGenerator.ExpandElement

        // [WMR 20161005] internal expandElement method is no longer unit-testable; uninitialized recursion stack causes exceptions

        //[TestMethod]
        //public void TestExpandChild()
        //{
        //    var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Questionnaire);
        //    Assert.IsNotNull(sd);
        //    Assert.IsNotNull(sd.Snapshot);
        //    var nav = new ElementDefinitionNavigator(sd.Snapshot.Element);
        //
        //    var generator = new SnapshotGenerator(_testResolver, SnapshotGeneratorSettings.Default);
        //
        //    nav.JumpToFirst("Questionnaire.telecom");
        //    Assert.IsTrue(generator.expandElement(nav));
        //    Assert.IsTrue(nav.MoveToChild("period"), "Did not move into complex datatype ContactPoint");
        //
        //    nav.JumpToFirst("Questionnaire.group");
        //    Assert.IsTrue(generator.expandElement(nav));
        //    Assert.IsTrue(nav.MoveToChild("title"), "Did not move into internally defined backbone element Group");
        //}

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
        public void TestExpandElement_QuestionnaireItem()
        {
            // Validate name reference expansion
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Questionnaire", "Questionnaire.item");
        }

        [TestMethod]
        public void TestExpandElement_QuestionnaireItemItem()
        {
            // Validate name reference expansion
            testExpandElement(@"http://hl7.org/fhir/StructureDefinition/Questionnaire", "Questionnaire.item.item");
        }

        [TestMethod]
        public void TestExpandElement_Slice()
        {
            // Resolve lipid profile from profile-others.xml
            var sd = _testResolver.FindStructureDefinition("http://hl7.org/fhir/StructureDefinition/lipidprofile");
            Assert.IsNotNull(sd);
            Assert.IsNotNull(sd.Snapshot);

            // DiagnosticReport.result is sliced
            var nav = new ElementDefinitionNavigator(sd.Snapshot.Element);

            // [WMR 20170711] Fix non-standard element id's in source (capitalization)
            // Standardized element ids are preferred, but not mandatory; so the profile is not invalid
            // Nonetheless fix this first, so we can call common assertion methods
            var elem = sd.Snapshot.Element.FirstOrDefault(e => e.ElementId == "DiagnosticReport.result:cholesterol");
            Assert.IsNotNull(elem);
            elem.ElementId = elem.Path + ElementIdGenerator.ElementIdSliceNameDelimiter + elem.SliceName;
            Assert.AreEqual("DiagnosticReport.result:Cholesterol", elem.ElementId);
            elem = sd.Snapshot.Element.FirstOrDefault(e => e.ElementId == "DiagnosticReport.result:triglyceride");
            elem.ElementId = elem.Path + ElementIdGenerator.ElementIdSliceNameDelimiter + elem.SliceName;
            Assert.IsNotNull(elem);
            elem.ElementId = elem.Path + ElementIdGenerator.ElementIdSliceNameDelimiter + elem.SliceName;
            Assert.AreEqual("DiagnosticReport.result:Triglyceride", elem.ElementId);

            // Move to slicing entry
            nav.JumpToFirst("DiagnosticReport.result");
            Assert.IsNotNull(nav.Current.Slicing);

            // Move to first (named) slice
            nav.MoveToNext();
            Assert.AreEqual(nav.Path, "DiagnosticReport.result");
            Assert.IsNotNull(nav.Current.SliceName);

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

            // [WMR 20170614] NEW: ExpandElement should maintain the existing element ID...!
            var orgId = elem.ElementId;

            var result = _generator.ExpandElement(elems, elem);

            dumpOutcome(_generator.Outcome);
            Assert.IsNull(_generator.Outcome);

            Assert.AreEqual(orgId, elem.ElementId);

            // Verify results
            verifyExpandElement(elem, elems, result);
        }

        void verifyExpandElement(ElementDefinition elem, IList<ElementDefinition> elems, IList<ElementDefinition> result)
        {
            var expandElemPath = elem.Path;

            // Debug.WriteLine("\r\nOutput:");
            // Debug.WriteLine(string.Join(Environment.NewLine, result.Where(e => e.Path.StartsWith(expandElemPath)).Select(e => e.Path)));

            Assert.IsNotNull(elem.Type);
            var elemType = elem.Type.FirstOrDefault();
            var nameRef = elem.ContentReference;
            if (elemType != null)
            {
                // Validate type profile expansion
                var elemTypeCode = elemType.Code;
                Assert.IsNotNull(elemTypeCode);

                var elemProfile = elemType.Profile;
                var sdType = elemProfile != null && elemTypeCode != FHIRAllTypes.Reference.GetLiteral()
                    ? _testResolver.FindStructureDefinition(elemProfile)
                    : _testResolver.FindStructureDefinitionForCoreType(elemTypeCode);

                // [WMR 20170220] External type profile may not be available
                // Assert.IsNotNull(sdType);
                if (sdType != null)
                {
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
                    if (elem.ContentReference != null)
                    {
                        // Name reference (not a slice)
                        Assert.IsTrue(nav.JumpToNameReference(elem.ContentReference));
                        var cnt = 1;
                        Assert.IsTrue(nav.MoveToFirstChild());
                        do
                        {
                            Assert.AreEqual(typeElems[cnt++].Path, nav.Path);
                        } while (nav.MoveToNext());
                        Assert.AreEqual(typeElems.Count, cnt);
                    }

                    nav.Reset();
                    Assert.IsTrue(nav.MoveTo(elem));

#if HACK_STU3_RECURSION
                    if (!isExpandableElement(elem))
                    {
                        Assert.IsFalse(nav.MoveToFirstChild());
                        return;
                    }
#endif

                    Assert.IsTrue(nav.MoveToFirstChild());
                    var typeNav = new ElementDefinitionNavigator(typeElems);
                    Assert.IsTrue(typeNav.MoveTo(typeNav.Elements[0]));
                    Assert.IsTrue(typeNav.MoveToFirstChild());
                    do
                    {
                        var path = typeNav.Path;
                        Assert.IsTrue(nav.Path.EndsWith(path, StringComparison.OrdinalIgnoreCase));
                        if (!nav.MoveToNext())
                        {
                            Debug.Assert(!typeNav.MoveToNext());
                            break;
                        }
                        // [WMR 20170412] Backbone elements can introduce additional child elements
                        if (!typeNav.MoveToNext())
                        {
                            Assert.AreEqual(FHIRAllTypes.BackboneElement.GetLiteral(), elemTypeCode);
                            break;
                        }

                    } while (true);
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
                // [WMR 20170412] Also handle grand children
                var srcPos = nav.OrdinalPosition.Value;
                var cnt = nav.Elements.Count;
                do
                {
                    Debug.WriteLine(nav.Path);
                    var srcPath = elems[srcPos++].Path.Substring(prefix.Length);
                    var tgtPath = result[++pos].Path.Substring(expandElemPath.Length);
                    Assert.AreEqual(srcPath, tgtPath);
                } while (srcPos < cnt);
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
                Debug.WriteLine("BaseDefinition = '{0}'".FormatWith(sd.BaseDefinition));

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
            return elements.SelectMany(e => e.Type).Select(t => t.Profile).Distinct();
        }

        static string formatElementPathName(ElementDefinition elem)
        {
            if (elem == null) { return null; }
            if (!string.IsNullOrEmpty(elem.SliceName)) return $"{elem.Path}:{elem.SliceName}";
            return elem.Path;
        }

        [Conditional("DEBUG")]
        static void dumpBaseElems(IEnumerable<ElementDefinition> elements)
        {
            Debug.Print(string.Join(Environment.NewLine,
                elements.Select(e =>
                {
                    var bea = e.Annotation<BaseDefAnnotation>();
                    var be = bea != null ? bea.BaseElementDefinition : null;
                    //return "  #{0,-8} {1} '{2}' - {3} => #{4,-8} {5} '{6}' - {7}"
                    //    .FormatWith(
                    //        e.GetHashCode(),
                    //        e.Path,
                    //        e.Name,
                    //        e?.Base?.Path,
                    //        (int?)be?.GetHashCode(),
                    //        be?.Path,
                    //        be?.Name,
                    //        be?.Base?.Path
                    //    );

                    return be != null ?
                        $"  #{e.GetHashCode(),-8} {formatElementPathName(e)} | {e.Base?.Path} <== #{be.GetHashCode(),-8} {formatElementPathName(be)} | {be.Base?.Path}"
                      : $"  #{e.GetHashCode(),-8} {formatElementPathName(e)} | {e.Base?.Path}"; 
                })
            ));
        }

        [Conditional("DEBUG")]
        void dumpBasePaths(StructureDefinition sd)
        {
            if (sd != null && sd.Snapshot != null)
            {
                Debug.WriteLine("StructureDefinition '{0}' ('{1}')".FormatWith(sd.Name, sd.Url));
                Debug.WriteLine("BaseDefiniton = '{0}'".FormatWith(sd.BaseDefinition));
                // Debug.Indent();
                Debug.Print("Element.Id | Element.Path | Element.Base.Path");
                Debug.Print(new string('=', 100));
                foreach (var elem in sd.Snapshot.Element)
                {
                    Debug.WriteLine("{0}  |  {1}  |  {2}", elem.ElementId, elem.Path, elem.Base?.Path);
                }
                // Debug.Unindent();
            }
        }

        [Conditional("DEBUG")]
        void dumpOutcome(OperationOutcome outcome) => dumpIssues(outcome?.Issue);

        [Conditional("DEBUG")]
        void dumpIssues(List<OperationOutcome.IssueComponent> issues)
        {
            if (issues != null && issues.Count > 0)
            {
                Debug.WriteLine("===== {0} issues", issues.Count);
                for (int i = 0; i < issues.Count; i++)
                {
                    dumpIssue(issues[i], i);
                }
                Debug.WriteLine("==================================");
            }
        }

        [Conditional("DEBUG")]
        void dumpIssue(OperationOutcome.IssueComponent issue, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("* Issue #{0}: Severity = '{1}' Code = '{2}'", index, issue.Severity, issue.Code);
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
            // Verify that the SnapshotGenerator events provide stable references to associated base ElementDefinition instances.
            // If two different profile elements have the same type, then the PrepareElement event should provide the exact same
            // reference to the associated base element. The same target ElementDefinition instance should also be contained in
            // the external type profile.

            var source = _testResolver;
            Assert.IsNotNull(source);

            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = source.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/shareablevalueset");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyLocation");
            // var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyPatient");
            // var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension1");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/CarePlan");

            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Meta");
            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Money");

            // var sd = source.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-basic-guidance-action");

            // var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithExtension");
            // var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithCustomIdentifier");

            var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/CustomIdentifier");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            var settings = new SnapshotGeneratorSettings(_settings);
            // settings.GenerateExtensionsOnConstraints = true;
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                generateSnapshotAndCompare(sd, out var expanded);

                dumpOutcome(_generator.Outcome);

                assertBaseDefs(expanded, settings);

                if (sd.Url != ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Element))
                {
                    // Element snapshot should be recursively expanded, as it is the fundamental base profile
                    var sdElem = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Element);
                    Assert.IsNotNull(sdElem);
                    Assert.IsTrue(sdElem.HasSnapshot);
                    Assert.IsTrue(sdElem.Snapshot.IsCreatedBySnapshotGenerator());
                    assertBaseDefs(sdElem, settings);
                }

                if (sd.Url != ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Id))
                {
                    // Id snapshot should not be (re-)generated, as derived profiles don't force expansion
                    var sdId = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Id);
                    Assert.IsNotNull(sdId);
                    Assert.IsTrue(sdId.HasSnapshot);
                    Assert.IsFalse(sdId.Snapshot.IsCreatedBySnapshotGenerator());
                    // Re-generate the snapshot and verify base references
                    generateSnapshotAndCompare(sdId, out expanded);
                    assertBaseDefs(expanded, settings);
                }

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
                _generator.Constraint -= constraintHandler;
                _generator.PrepareElement -= elementHandler;
                _generator.PrepareBaseProfile -= profileHandler;
            }
        }

        [TestMethod]
        public void TestBaseAnnotations_ExplicitCoreTypeProfile()
        {
            // Verify processing of explicit core element type profile in differential
            // e.g. if the differential specifies explicit core type profile url
            // Example: Patient.identifier type = { Code : Identifier, Profile : "http://hl7.org/fhir/StructureDefinition/Identifier" } }
            // Snapshot generator should ignore this, i.e. NOT treat this as a constraint

            var source = _testResolver;
            Assert.IsNotNull(source);
            var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithExplicitCoreIdentifierProfile");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            // Patient.identifier should reference the default core Identifier type profile
            var elem = sd.Differential.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(elem);
            var typeProfileUrl = elem.Type.FirstOrDefault().Profile;
            Assert.IsNotNull(typeProfileUrl);
            Assert.AreEqual(typeProfileUrl, ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Identifier));

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                generateSnapshotAndCompare(sd, out var expanded);
                dumpOutcome(_generator.Outcome);
                Assert.IsTrue(expanded.HasSnapshot);
                Assert.IsTrue(expanded.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(expanded, settings);

                // Verify that the snapshot generator also expanded the referenced core Identifier type profile
                var sdType = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Identifier);
                Assert.IsNotNull(sdType);
                Assert.IsTrue(sdType.HasSnapshot);
                Assert.IsTrue(sdType.Snapshot.IsCreatedBySnapshotGenerator());

                // Verify the snapshot expansion of the Patient.identifier element
                elem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
                Assert.IsNotNull(elem);
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Assert.IsNotNull(baseElem);
                Assert.AreEqual(elem.Path, baseElem.Path); // Base = core Patient.identifier element
                // Note: diff elem is not exactly equal to base elem (due to reduntant type profile constraint)
                var hasConstraints = !SnapshotGeneratorTest2.isAlmostExactly(elem, baseElem, false);
                Assert.IsTrue(hasConstraints);
                // Check: re-assert while ignoring the redundant type profile constraint
                Assert.IsTrue(SnapshotGeneratorTest2.isAlmostExactly(elem, baseElem, true));

                Assert.IsTrue(hasChanges(elem));

                // Verify base annotations on Patient.identifier subtree
                var elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier.")).ToList();
                for (int i = 0; i < elems.Count; i++)
                {
                    elem = elems[i];
                    Assert.IsNotNull(elem);
                    baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                    Assert.IsNotNull(baseElem);
                    hasConstraints = !SnapshotGeneratorTest2.isAlmostExactly(elem, baseElem);
                    // Only the .use child element has a profile diff constraint
                    bool isConstrained = elem.Path == "Patient.identifier.use";

                    // [WMR 20170713] Changed
                    // Assert.AreEqual(isConstrained, hasConstraints);
                    Assert.AreEqual(isConstrained || elem.IsExtension(), hasConstraints);

                    var elemHasChanges = hasChanges(elem);
                    Assert.AreEqual(isConstrained, elemHasChanges);

                    // Verify that base element annotations reference the associated child element in Core Identifier profile
                    // [WMR 20170501] OBSOLETE
                    // Assert.AreEqual("Patient." + baseElem.Path.Uncapitalize(), elem.Path);
                    Debug.WriteLine($"*** elem.Path = '{elem.Path}' baseElem.Path = '{baseElem.Path}' ");
                }

            }
            finally
            {
                // Detach event handlers
                _generator.Constraint -= constraintHandler;
                _generator.PrepareElement -= elementHandler;
                _generator.PrepareBaseProfile -= profileHandler;
            }
        }

        [TestMethod]
        public void TestBaseAnnotations_CustomTypeProfile()
        {
            // Verify generated base annotations for a profile that references an external element type profile
            // e.g. Patient profile with a custom Identifier profile on the Patient.identifier element

            var source = _testResolver;
            Assert.IsNotNull(source);
            var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithCustomIdentifier");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            // Patient.identifier should reference an external type profile
            var elem = sd.Differential.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(elem);
            var typeProfileUrl = elem.Type.FirstOrDefault().Profile;
            Assert.IsNotNull(typeProfileUrl);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                generateSnapshotAndCompare(sd, out var expanded);
                dumpOutcome(_generator.Outcome);
                Assert.IsTrue(expanded.HasSnapshot);
                Assert.IsTrue(expanded.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(expanded, settings);

                // Verify that the snapshot generator also expanded the referenced external custom Identifier type profile
                var sdType = source.FindStructureDefinition(typeProfileUrl);
                Assert.IsNotNull(sdType);
                Assert.IsTrue(sdType.HasSnapshot);
                Assert.IsTrue(sdType.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(sdType, settings);

                // Verify the snapshot expansion of the Patient.identifier element
                elem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
                Assert.IsNotNull(elem);
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Assert.IsNotNull(baseElem);
                Assert.AreEqual(elem.Path, baseElem.Path); // Base = core Patient.identifier element
                // Note: diff elem is not exactly equal to base elem (due to reduntant type profile constraint)
                // hasConstraints and hasChanges methods aren't smart enough to detect redundant constraints
                var hasConstraints = !SnapshotGeneratorTest2.isAlmostExactly(elem, baseElem);
                Assert.IsTrue(hasConstraints);

                // Verify base annotations on Patient.identifier subtree
                var elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier.")).ToList();
                for (int i = 0; i < elems.Count; i++)
                {
                    elem = elems[i];
                    Assert.IsNotNull(elem);
                    baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                    Assert.IsNotNull(baseElem);
                    hasConstraints = !SnapshotGeneratorTest2.isAlmostExactly(elem, baseElem);
                    // Only the .use child element has a profile diff constraint
                    bool isConstrained = elem.Path == "Patient.identifier.use" || elem.Path == "Patient.identifier.value";
                    Assert.AreEqual(isConstrained, hasConstraints);
                    Assert.AreEqual(isConstrained, hasChanges(elem));

                    // Verify that base element annotations reference the associated child element in custom Identifier profile
                    // Assert.AreEqual("Patient." + baseElem.Path.Uncapitalize(), elem.Path);

                    // Verify correct base element annotations
                    // Should point to rebased custom type element (same path)
                    Assert.AreEqual(baseElem.Path, elem.Path);
                }

                // Verify specific element constraints
                // Patient.identifier.use::min is overriden by patient profile
                elem = elems.FirstOrDefault(e => e.Path == "Patient.identifier.use");
                Assert.IsNotNull(elem);
                Assert.AreEqual(1, elem.Min);
                Assert.IsTrue(elem.HasDiffConstraintAnnotations());
                Assert.IsTrue(elem.MinElement.IsConstrainedByDiff());

                // Patient.identifier.value::short is overriden by patient profile
                elem = elems.FirstOrDefault(e => e.Path == "Patient.identifier.value");
                Assert.IsNotNull(elem);
                Assert.AreEqual("A custom identifier value", elem.Short);
                Assert.IsTrue(elem.HasDiffConstraintAnnotations());
                Assert.IsTrue(elem.ShortElement.IsConstrainedByDiff());

                // Patient.identifier.system::min is inherited from custom type profile, not overriden by patient profile
                elem = elems.FirstOrDefault(e => e.Path == "Patient.identifier.system");
                Assert.IsNotNull(elem);
                Assert.AreEqual(1, elem.Min);
                Assert.IsFalse(elem.HasDiffConstraintAnnotations());
                Assert.IsFalse(elem.MinElement.IsConstrainedByDiff());

            }
            finally
            {
                // Detach event handlers
                _generator.Constraint -= constraintHandler;
                _generator.PrepareElement -= elementHandler;
                _generator.PrepareBaseProfile -= profileHandler;
            }
        }

        [TestMethod]
        public void TestBaseAnnotations_InlineExtension()
        {
            // Verify generated base annotations for a profile that references an external extension definition profile

            var source = _testResolver;
            Assert.IsNotNull(source);
            var sd = source.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithExtension");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            // Patient profile should reference an external extension definition, fetch the url
            var elem = sd.Differential.Element.FirstOrDefault(e => e.Path == "Patient.extension" && e.Slicing == null);
            Assert.IsNotNull(elem);
            var extensionDefinitionUrl = elem.Type.FirstOrDefault().Profile;
            Assert.IsNotNull(extensionDefinitionUrl);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                generateSnapshotAndCompare(sd, out var expanded);
                dumpOutcome(_generator.Outcome);
                Assert.IsTrue(expanded.HasSnapshot);
                Assert.IsTrue(expanded.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(expanded, settings);

                // Verify that the snapshot generator also expanded the referenced external extension definition
                var sdExtension = source.FindStructureDefinition(extensionDefinitionUrl);
                Assert.IsNotNull(sdExtension);
                Assert.IsTrue(sdExtension.HasSnapshot);
                Assert.IsTrue(sdExtension.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(sdExtension, settings);

                // Verify correct merging of inline profile constraints overriding the extension definition
                var nav = new ElementDefinitionNavigator(expanded);
                Assert.IsTrue(nav.MoveToFirstChild());
                Assert.IsTrue(nav.MoveToFirstChild());
                Assert.IsTrue(nav.MoveToNext("extension"));
                Assert.IsNotNull(nav.Current.Slicing);  // Extension slicing entry
                Assert.IsTrue(nav.MoveToNext("extension"));
                elem = nav.Current;
                Assert.IsNull(elem.Slicing);    // First extension
                Assert.AreEqual(elem.PrimaryTypeProfile(), extensionDefinitionUrl);

                Assert.AreEqual("extension", elem.SliceName);
                Assert.AreEqual("1", elem.Max); // Inline profile constraint overriding the extension definition
                Assert.IsTrue(elem.MaxElement.IsConstrainedByDiff());
                Assert.IsTrue(elem.HasDiffConstraintAnnotations());
                Assert.IsTrue(elem.IsConstrainedByDiff());
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Assert.IsNotNull(baseElem);
                Assert.AreEqual("*", baseElem.Max);             // Verify that max property is not inherited from base element = Extension root element
                Assert.AreEqual(baseElem.Short, elem.Short);    // Verify that short property is inherited
                Assert.IsFalse(elem.ShortElement.IsConstrainedByDiff());
                // Profile overrides the definition property of the extension definition root element 
                Assert.AreNotEqual(baseElem.Definition, elem.Definition);
                Assert.IsTrue(elem.DefinitionElement.IsConstrainedByDiff());

                Assert.IsTrue(nav.MoveToFirstChild());

                Assert.IsTrue(nav.MoveToNext("url"));
                elem = nav.Current;
                Assert.IsFalse(elem.HasDiffConstraintAnnotations());
                var uri = elem.Fixed as FhirUri;
                Assert.IsNotNull(uri);
                Assert.AreEqual(extensionDefinitionUrl, uri.Value);

                Assert.IsTrue(nav.MoveToNext("valueString"));
                elem = nav.Current;
                Assert.AreEqual(1, elem.Min);            // Inline profile constraint overriding the extension definition
                Assert.IsTrue(elem.MinElement.IsConstrainedByDiff());
                Assert.IsTrue(elem.HasDiffConstraintAnnotations());
                baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Assert.IsNotNull(baseElem);
                Assert.AreEqual(0, baseElem.Min);               // Verify that min property is not inherited from base element = Extension.valueString
                Assert.AreEqual(baseElem.Short, elem.Short);    // Verify that short property is inherited
                Assert.IsFalse(elem.ShortElement.IsConstrainedByDiff());
                Assert.AreEqual(baseElem.Definition, elem.Definition);    // Verify that definition property is inherited
                Assert.IsFalse(elem.DefinitionElement.IsConstrainedByDiff());
            }
            finally
            {
                // Detach event handlers
                _generator.Constraint -= constraintHandler;
                _generator.PrepareElement -= elementHandler;
                _generator.PrepareBaseProfile -= profileHandler;
            }
        }

        // [WMR 20170714] NEW
        // Annotated Base Element for backbone elements is not included in base structuredefinition ?

        static StructureDefinition MyTestObservation => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.component")
                    {
                        Min = 1
                    },
                }
            }
        };

        [TestMethod]
        public void TestBaseAnnotations_BackboneElement()
        {
            var sd = MyTestObservation;
            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);

            Debug.WriteLine("Core Observation:");
            var obs = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation);
            Assert.IsNotNull(obs);
            Assert.IsTrue(obs.HasSnapshot);
            dumpBaseElems(obs.Snapshot.Element);

            Debug.WriteLine("Derived Observation:");
            // dumpElements(expanded.Snapshot.Element);
            dumpBaseElems(expanded.Snapshot.Element);

            assertBaseDefs(expanded, _settings);

            // Additional check: verify that all annotated base element references
            // point to existing instances in the base profile snapshot
            var elems = expanded.Snapshot.Element;
            var baseElems = obs.Snapshot.Element;
            for (int i = 0; i < elems.Count; i++)
            {
                var elem = elems[i];
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Assert.IsTrue(baseElems.Contains(baseElem));
            }
        }


        // [WMR 20160816] Test custom annotations containing associated base definitions
        class BaseDefAnnotation
        {
            public BaseDefAnnotation(ElementDefinition baseElemDef, StructureDefinition baseStructDef)
            {
                BaseElementDefinition = baseElemDef;
                BaseStructureDefinition = baseStructDef;
            }
            public ElementDefinition BaseElementDefinition { get; private set; }
            public StructureDefinition BaseStructureDefinition { get; private set; }
        }

        static ElementDefinition GetBaseElementAnnotation(ElementDefinition elemDef)
        {
            return elemDef?.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
        }

        void profileHandler(object sender, SnapshotBaseProfileEventArgs e)
        {
            var profile = e.Profile;
            // Assert.IsTrue(sd.Url != profile.Url || sd.IsExactly(profile));
            var baseProfile = e.BaseProfile;
            Assert.IsNotNull(baseProfile);
            Debug.WriteLine("[SnapshotBaseProfileHandler] Profile #{0} '{1}' BaseDefinition = '{2}'".FormatWith(profile.GetHashCode(), profile.Url, profile.BaseDefinition));
            Debug.Print("[SnapshotBaseProfileHandler] Base Profile #{0} '{1}'".FormatWith(baseProfile.GetHashCode(), baseProfile.Url));
            var rootElem = baseProfile.Snapshot.Element[0];
            Debug.Print("[SnapshotBaseProfileHandler] Base Root element #{0} '{1}'".FormatWith(rootElem.GetHashCode(), rootElem.Path));
            Assert.AreEqual(profile.BaseDefinition, baseProfile.Url);
        }

        static void elementHandler(object sender, SnapshotElementEventArgs e)
        {
            var elem = e.Element;
            Assert.IsNotNull(elem);

            // Assert.IsNotNull(elem.Base);

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
            elem.AddAnnotation(new BaseDefAnnotation(baseDef, baseStruct));

            Debug.Write($"[{nameof(SnapshotGeneratorTest)}.{nameof(elementHandler)}] #{elem.GetHashCode()} '{elem.Path}:{elem.SliceName}' - Base: #{baseDef?.GetHashCode() ?? 0} '{(baseDef?.Path)}' - Base Structure '{baseStruct?.Url}'");
            Debug.WriteLine(ann?.BaseElementDefinition != null ? $" (old Base: #{ann.BaseElementDefinition.GetHashCode()} '{ann.BaseElementDefinition.Path}')" : "");
        }

        void constraintHandler(object sender, SnapshotConstraintEventArgs e)
        {
            var elem = e.Element as ElementDefinition;
            if (elem != null)
            {
                var changed = elem.IsConstrainedByDiff();
                Debug.Assert(!_settings.GenerateAnnotationsOnConstraints || changed);
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

            var isConstraint = sd.Derivation == StructureDefinition.TypeDerivationRule.Constraint;

            Debug.Print("\r\nStructureDefinition '{0}' url = '{1}'", sd.Name, sd.Url);
            Debug.Print("# | Constraints? | Changed? | Element.Path | Element.Base.Path | BaseElement.Path | #Base | Redundant?");
            Debug.Print(new string('=', 100));
            foreach (var elem in elems)
            {
                // Each element should have a valid Base component, unless the profile is a core type/resource definition (no base)
                // Assert.IsTrue(!isConstraint || elem.Base != null);

                var ann = elem.Annotation<BaseDefAnnotation>();
                var baseDef = ann != null ? ann.BaseElementDefinition : null;
                Assert.AreNotEqual(elem, baseDef);

                var hasChanges = SnapshotGeneratorTest2.hasChanges(elem);
                var isNotExactly = false;
                if (baseDef != null) // && elem.Base != null)
                {
                    // If normalizing, then elem.Base.Path refers to the defining profile (e.g. DomainResource),
                    // whereas baseDef refers to the immediate base profile (e.g. Patient)
                    Debug.Assert(elem.Base == null || ElementDefinitionNavigator.IsCandidateBasePath(elem.Base.Path, baseDef.Path)
                        // [WMR 20170713] Added, e.g. Patient.identifier.use <=> code
                        || !baseDef.Path.Contains(".")
                        );
                    isNotExactly = !SnapshotGeneratorTest2.isAlmostExactly(elem, baseDef);
                }
                // var isValid = hasChanges == isNotExactly;
                var isRedundant = hasChanges && !isNotExactly;
                bool? hasConstraintAnnotations = null;
                if (settings.GenerateAnnotationsOnConstraints)
                {
                    hasConstraintAnnotations = elem.HasDiffConstraintAnnotations();
                    //isValid &= isNotExactly == hasConstraintAnnotations;
                    isRedundant |= !isNotExactly && (hasConstraintAnnotations == true);
                }

                Debug.WriteLine("{0,10}  |  {1}  |  {2,-12}  |  {3,-50}  |  {4,-40}  |  {5,-40}  |  {6,10}  |  {7}",
                    elem.GetHashCode(),
                    (isNotExactly ? "+" : "-")
                    + (hasConstraintAnnotations.HasValue ? (hasConstraintAnnotations.Value ? " (+)" : " (-)") : null),
                    getChangeDescription(elem),
                    elem.Path,
                    elem.Base != null ? elem.Base.Path : null,
                    baseDef != null ? baseDef.Path : null,
                    baseDef != null ? baseDef.GetHashCode().ToString() : null,
                    // !isValid ? "!!!" : ""
                    isRedundant ? "(redundant)" : ""
                );
                //Assert.IsTrue(baseDef == null || isValid);
                // Debug.Assert(baseDef == null || isValid);
            }
        }

        // Utility function to compare element and base element
        // Path, Base and CHANGED_BY_DIFF_EXT extension are excluded from comparison
        // Returns true if the element has no other constraints on base
        static bool isAlmostExactly(ElementDefinition elem, ElementDefinition baseElem, bool ignoreTypeProfile = false)
        {
            var elemClone = (ElementDefinition)elem.DeepCopy();
            var baseClone = (ElementDefinition)baseElem.DeepCopy();

            // Id, Path & Base are expected to differ
            baseClone.ElementId = elem.ElementId;
            baseClone.Path = elem.Path;
            baseClone.Base = elem.Base;

            // [WMR 20170713] Added
            if (ignoreTypeProfile)
            {
                Debug.Assert(elem.Type.Count > 0);
                Debug.Assert(baseClone.Type.Count > 0);
                baseClone.Type[0].Profile = elem.Type[0].Profile;
            }

            // Also ignore any Changed extensions on base and diff
            elemClone.RemoveAllConstrainedByDiffExtensions();
            baseClone.RemoveAllConstrainedByDiffExtensions();
            elemClone.RemoveAllConstrainedByDiffAnnotations();
            baseClone.RemoveAllConstrainedByDiffAnnotations();

            var result = baseClone.IsExactly(elemClone);
            return result;
        }

        // Returns true if the specified element or any of its' components contain the CHANGED_BY_DIFF_EXT extension
        static bool hasChanges(ElementDefinition elem)
        {
            return isChanged(elem)
                || hasChanges(elem.AliasElement)
                || isChanged(elem.Base)
                || isChanged(elem.Binding)
                || hasChanges(elem.Code)
                || isChanged(elem.CommentElement)
                || hasChanges(elem.ConditionElement)
                || hasChanges(elem.Constraint)
                || isChanged(elem.DefaultValue)
                || isChanged(elem.DefinitionElement)
                || hasChanges(elem.Example)
                || hasChanges(elem.Extension)
             //   || hasChanges(elem.FhirCommentsElement)
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
                || isChanged(elem.SliceNameElement)
                || isChanged(elem.ContentReferenceElement)
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
            if (isChanged(element.CommentElement)) { return "Comment"; }
            if (hasChanges(element.ConditionElement)) { return "Condition"; }
            if (hasChanges(element.Constraint)) { return "Constraint"; }
            if (isChanged(element.DefaultValue)) { return "DefaultValue"; }
            if (isChanged(element.DefinitionElement)) { return "Definition"; }
            if (hasChanges(element.Example)) { return "Example"; }
            if (hasChanges(element.Extension)) { return "Extension"; }
            //if (hasChanges(element.FhirCommentsElement)) { return "FhirComments"; }
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
            if (isChanged(element.SliceNameElement)) { return "SliceName"; }
            if (isChanged(element.ContentReferenceElement)) { return "ContentReference"; }
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

        static bool hasChanges<T>(IList<T> elements) where T : Element => elements != null ? elements.Any(e => isChanged(e)) : false;
        static bool isChanged(Element elem) => elem != null && elem.IsConstrainedByDiff();

        [TestMethod]
        public void TestExpandCoreElement()
        {
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Element");
        }

        [TestMethod]
        public void TestExpandCoreBackBoneElement()
        {
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/BackboneElement");
        }

        [TestMethod]
        public void TestExpandCoreExtension()
        {
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Extension");
        }

        [TestMethod]
        public void TestExpandCoreArtifacts()
        {

            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/integer");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/positiveInt");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/string");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/code");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/id");

            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Meta");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/HumanName");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Quantity");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/SimpleQuantity");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Money");

            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Resource");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/DomainResource");

            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Basic");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Patient");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Questionnaire");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/AuditEvent");

            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Organization");
        }

        [TestMethod]
        public void TestExpandAllCoreTypes()
        {
            // Generate snapshots for all core types, in the original order as they are defined
            // The Snapshot Generator should recursively process any referenced base/type profiles (e.g. Element, Extension)
            var coreArtifactNames = ModelInfo.FhirCsTypeToString.Values;
            var coreTypeUrls = coreArtifactNames.Where(t => !ModelInfo.IsKnownResource(t)).Select(t => "http://hl7.org/fhir/StructureDefinition/" + t).ToArray();
            testExpandResources(coreTypeUrls.ToArray());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void TestExpandAllCoreResources()
        {
            // Generate snapshots for all core resources, in the original order as they are defined
            // The Snapshot Generator should recursively process any referenced base/type profiles (e.g. data types)
            var coreResourceUrls = ModelInfo.SupportedResources.Select(t => "http://hl7.org/fhir/StructureDefinition/" + t);
            testExpandResources(coreResourceUrls.ToArray());
        }

        void testExpandResources(string[] profileUris)
        {
            var sw = new Stopwatch();
            int count = profileUris.Length;
            _source.Reset();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                testExpandResource(profileUris[i]);
            }

            sw.Stop();
            _source.ShowDuration(count, sw.Elapsed);
        }

        bool testExpandResource(string url)
        {
            Debug.Print("[testExpandResource] url = '{0}'", url);
            var sd = _testResolver.FindStructureDefinition(url);
            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            var result = generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            if (!result)
            {
                Debug.Print("Expanded is not exactly equal to original... verifying...");
                result = verifyElementBase(sd, expanded);
            }

            // Core artifact snapshots are incorrect, e.g. url snapshot is missing extension element
            //Assert.IsTrue(result);

            return result;
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

        [TestMethod]
        public void TestExpandCoreTypesByHierarchy()
        {
            // [WMR 20160912] Expand all core data types
            // Start at root types without a base (Element, Extension), then recursively expand derived types

            var result = true;
            var source = new DirectorySource("TestData/snapshot-test");
            var resolver = new CachedResolver(source); // IMPORTANT!

            _generator = new SnapshotGenerator(resolver, _settings);
            _generator.PrepareElement += elementHandler;

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
                    coreProfileInfo = coreDefs.Select(sd => new ProfileInfo() { Url = sd.Url, BaseDefinition = sd.BaseDefinition }).ToArray();
                }
                expandStructuresBasedOn(resolver, coreProfileInfo, null);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            Assert.IsTrue(result);
        }

        struct ProfileInfo { public string Url; public string BaseDefinition; }

        void expandStructuresBasedOn(IResourceResolver resolver, ProfileInfo[] profileInfo, string baseUrl)
        {
            var derivedStructures = profileInfo.Where(pi => pi.BaseDefinition == baseUrl);
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
            Debug.Print("Profile: '{0}' : '{1}'".FormatWith(sd.Url, sd.BaseDefinition));
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
            var isConstraint = expanded.Derivation == StructureDefinition.TypeDerivationRule.Constraint;
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

                var rootElemName = expandedElems[0].Path;

                //var baseProfileUrl = expanded.Base;
                //var baseProfile = baseProfileUrl != null ? _testResolver.FindStructureDefinition(baseProfileUrl) : null;
                //var baseRootElemName = baseProfile != null && baseProfile.Snapshot != null ? baseProfile.Snapshot.Element[0].Path : null;
                //if (expandedElems.Count > 0 && baseRootElemName != null)
                //{
                //    verified &= verifyBasePath(expandedElems[0], originalElems[0], baseRootElemName);
                //}

                if (expanded.Kind == StructureDefinition.StructureDefinitionKind.PrimitiveType)
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
                else if (expanded.Kind == StructureDefinition.StructureDefinitionKind.ComplexType)
                {
                    // TODO: verify that this is correct (I think so given the others in this context)
                    verified &= verifyBasePath(expandedElems[1], originalElems[1], "Element.id");
                    verified &= verifyBasePath(expandedElems[2], originalElems[2], "Element.extension");
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

                if (isConstraint)
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

        // [WMR 20161207] NEW
        // Verify reslicing order
        [TestMethod]
        public void TestReslicingOrder()
        {
            var dirSource = new DirectorySource("TestData/validation");
            var sd = dirSource.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.IsNotNull(sd);

            //Patient.telecom : ''
            //Patient.telecom : 'phone'
            //Patient.telecom : 'email'
            //Patient.telecom : 'email/home'
            //Patient.telecom : 'email/work'
            //Patient.telecom : 'other'
            //Patient.telecom : 'other/home'
            //Patient.telecom : 'other/work'

            // Verify original differential - defines reslicing
            Debug.Print("Verify differential...");
            var diffNav = ElementDefinitionNavigator.ForDifferential(sd);
            assertPatientTelecomReslice(diffNav);

            generateSnapshotAndCompare(sd, out var expanded);

            Debug.Print("Verify snapshot...");
            var snapNav = ElementDefinitionNavigator.ForSnapshot(expanded);
            assertPatientTelecomReslice(snapNav);
        }

        void assertPatientTelecomReslice(ElementDefinitionNavigator nav)
        {
            Assert.IsTrue(nav.MoveToFirstChild());  // Patient

            if (ElementDefinitionNavigator.IsRootPath(nav.Path))
            {
                Assert.IsTrue(nav.MoveToChild("telecom"));
            }

            var bm = nav.Bookmark();
            do
            {
                Debug.Print($"{nav.Path} : '{nav.Current.SliceName}'");
            } while (nav.MoveToNext("telecom"));
            nav.ReturnToBookmark(bm);

            // Patient.telecom - slicing introduction
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsNotNull(nav.Current.Slicing);

            // Patient.telecom - slice "phone"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "phone");

            // Patient.telecom - slice "email"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "email");

            // Patient.telecom - reslice "email/home"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "email/home");

            // Patient.telecom - reslice "email/work"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "email/work");

            // Patient.telecom - slice "other"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "other");

            // Patient.telecom - reslice "other/home"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "other/home");

            // Patient.telecom - reslice "other/work"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.SliceName == "other/work");
        }


        // [WMR 20161207] DEBUGGING
        // List all complex extensions that are available in the TestData folder

        // http://hl7.org/fhir/StructureDefinition/cqif-basic-codeSystem : 'TestData/snapshot-test/extensions\extension-cqif-basic-codesystem.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-contributor : 'TestData/snapshot-test/extensions\extension-cqif-basic-contributor.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-data : 'TestData/snapshot-test/extensions\extension-cqif-basic-data.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-guidance-action : 'TestData/snapshot-test/extensions\extension-cqif-basic-guidance-action.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-guidance-trigger : 'TestData/snapshot-test/extensions\extension-cqif-basic-guidance-trigger.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-library : 'TestData/snapshot-test/extensions\extension-cqif-basic-library.canonical.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-model : 'TestData/snapshot-test/extensions\extension-cqif-basic-model.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-parameter : 'TestData/snapshot-test/extensions\extension-cqif-basic-parameter.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-relatedResource : 'TestData/snapshot-test/extensions\extension-cqif-basic-relatedresource.xml'
        // http://hl7.org/fhir/StructureDefinition/cqif-basic-valueSet : 'TestData/snapshot-test/extensions\extension-cqif-basic-valueset.xml'
        // http://hl7.org/fhir/StructureDefinition/encounter-relatedCondition : 'TestData/snapshot-test/extensions\extension-encounter-relatedcondition.xml'
        // http://hl7.org/fhir/StructureDefinition/family-member-history-genetics-parent : 'TestData/snapshot-test/extensions\extension-family-member-history-genetics-parent.xml'
        // http://hl7.org/fhir/StructureDefinition/gao-extension-item : 'TestData/snapshot-test/extensions\extension-gao-extension-item.canonical.xml'
        // http://hl7.org/fhir/StructureDefinition/goal-target : 'TestData/snapshot-test/extensions\extension-goal-target.xml'
        // http://hl7.org/fhir/StructureDefinition/patient-clinicalTrial : 'TestData/snapshot-test/extensions\extension-patient-clinicaltrial.xml'
        // http://hl7.org/fhir/StructureDefinition/patient-nationality : 'TestData/snapshot-test/extensions\extension-patient-nationality.xml'
        // http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause : 'TestData/snapshot-test/extensions\extension-qicore-adverseevent-cause.xml'
        // http://hl7.org/fhir/StructureDefinition/questionnaire-enableWhen : 'TestData/snapshot-test/extensions\extension-questionnaire-enablewhen.xml'

        [TestMethod]
        public void FindComplexTestExtensions()
        {
            Debug.WriteLine("Complex extension in TestData folder:");
            var dirSource = new DirectorySource("TestData/snapshot-test/extensions");
            var uris = dirSource.ListResourceUris(ResourceType.StructureDefinition);
            foreach (var uri in uris)
            {
                var sd = dirSource.FindStructureDefinition(uri);
                if (sd.IsExtension)
                {
                    if (sd.Differential.Element.Any(e => e.Path.StartsWith("Extension.extension.", StringComparison.Ordinal)))
                    {
                        // var orgInfo = sd.Annotation<OriginAnnotation>();
                        // Debug.WriteLine($"{uri} : '{orgInfo?.Origin}'");
                        Debug.WriteLine($"{uri} : '{sd.GetOrigin()}'");
                    }
                }
            }
        }

        // Ewout: type slices cannot contain renamed elements!
        static StructureDefinition ObservationTypeSliceProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.value[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            // Discriminator = new string[] { "@type" },
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Type }
                                }.ToList(),
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        }
                    }
                    ,new ElementDefinition("Observation.value[x]")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                        }
                    }
                }
            }
        };

        [Conditional("DEBUG")]
        void dumpElements(IEnumerable<ElementDefinition> elements, string header = null) => dumpElements(elements.ToList(), header);

        [Conditional("DEBUG")]
        void dumpElements(List<ElementDefinition> elements, string header = null)
        {
            Debug.WriteLineIf(!string.IsNullOrEmpty(header), header);
            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                Debug.Write(elem.Path);
                Debug.WriteIf(elem.Path != null, " '" + elem.SliceName + "'");
                if (elem.Slicing != null)
                {
                    Debug.Write(" => sliced on: " + string.Join(" | ", elem.Slicing.Discriminator.Select(p => p?.Path)));
                }
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TestTypeSlicing()
        {
            // Create a profile with a type slice: { value[x], value[x] : String }
            var profile = ObservationTypeSliceProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("[1] Observation.value slice:");

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueString
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRAllTypes.String.GetLiteral());

            // Add an additional type slice: { value[x], value[x] : String, value[x] : CodeableConcept }
            profile.Differential.Element.Add(
                new ElementDefinition("Observation.value[x]")
                {
                    Type = new List<ElementDefinition.TypeRefComponent>()
                    {
                        new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.CodeableConcept.GetLiteral() }
                    }
                }
            );

            generateSnapshotAndCompare(profile, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("[2] Observation.value slice:");

            nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueString
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRAllTypes.String.GetLiteral());
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueCodeableConcept
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRAllTypes.CodeableConcept.GetLiteral());
        }

        [TestMethod]
        public void TestMissingDifferential()
        {
            // Create a profile without a differential
            var profile = ObservationTypeSliceProfile;
            profile.Differential = null;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Dump();
        }

        [TestMethod]
        public void TestUnresolvedBaseProfile()
        {
            // Create a profile with an unresolved base profile reference
            var profile = ObservationTypeSliceProfile;
            profile.BaseDefinition = "http://example.org/fhir/StructureDefinition/missing";

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsFalse(expanded.HasSnapshot);
            var outcome = _generator.Outcome;
            Assert.IsNotNull(outcome);
            Assert.IsNotNull(outcome.Issue);
            Assert.AreEqual(outcome.Issue.Count, 1);
            assertIssue(outcome.Issue[0], Issue.UNAVAILABLE_REFERENCED_PROFILE, profile.BaseDefinition);
        }

        static StructureDefinition ObservationTypeResliceProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ObservationTypeSliceProfile.Url,
            Name = "MyDerivedTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyDerivedTestObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.value[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            // Discriminator = new string[] { "@type" },
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Type }
                                }.ToList(),
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        }
                    }
                    // Constraint on existing type slice value[x] : String
                    ,new ElementDefinition("Observation.value[x]")
                    {
                        Max = "1", // New constraint
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                        }
                    }
                    
                    // Remove existing type slice value[x]: CodeableConcept

                    // Add a new type slice value[x]: Integer
                    ,new ElementDefinition("Observation.value[x]")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
                        }
                    },
                }
            }
        };

        [TestMethod]
        public void TestTypeReslicing()
        {
            // Create a derived profile from a base profile with a type slice
            var profile = ObservationTypeResliceProfile;
            var baseProfile = ObservationTypeSliceProfile;

            var resources = new IConformanceResource[] { profile, baseProfile };
            var resolver = new InMemoryProfileResolver(resources);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("[1] Observation.value reslice:");

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueString
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRAllTypes.String.GetLiteral());
            // Derived profile REMOVES existing CodeableConcept type slice and introduces a new Integer type slice
            // Note: special rules for element types allow removal of inherited collection items
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueCodeableConcept
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRAllTypes.Integer.GetLiteral());
        }

        // Choice type constraint, with element renaming
        static StructureDefinition ObservationTypeConstraintProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    // No slicing introduction
                    // Only single element is allowed (this is NOT a slice!)
                    // Element is renamed
                    new ElementDefinition("Observation.valueString")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.String.GetLiteral() }
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestChoiceTypeConstraint()
        {
            // Create a profile with a choice type constraint: value[x] => valueString
            var profile = ObservationTypeConstraintProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("Observation.value choice type constraint:");

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsFalse(nav.MoveToChild("value[x]")); // Should also be renamed to valueString in snapshot
            Assert.IsTrue(nav.MoveToChild("valueString"));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRAllTypes.String.GetLiteral());
        }

        [TestMethod]
        public void TestInvalidChoiceTypeConstraints()
        {
            // Create a profile with multiple (invalid!) choice type constraint: value[x] => { valueString, valueInteger }
            var profile = ObservationTypeConstraintProfile;
            profile.Differential.Element.Add(
                    new ElementDefinition("Observation.valueInteger")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Integer.GetLiteral() }
                        }
                    }
            );

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("Observation.value choice type constraint:");
            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsFalse(nav.MoveToChild("value[x]")); // Should also be renamed to valueString in snapshot
            Assert.IsTrue(nav.MoveToChild("valueString"));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRAllTypes.String.GetLiteral());

            Assert.IsTrue(nav.MoveToNext("valueInteger"));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRAllTypes.Integer.GetLiteral());

            Assert.IsNotNull(outcome);
            // [WMR 20170810] Fixed, now also expecting issue about invalid slice name on SimpleQuantity root element
            //Assert.AreEqual(1, outcome.Issue.Count);
            // assertIssue(outcome.Issue[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT);
            Assert.AreEqual(2, outcome.Issue.Count);
            assertIssue(outcome.Issue[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT);
            assertIssue(outcome.Issue[1], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT);
        }

        static StructureDefinition ClosedExtensionSliceObservationProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.extension")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Rules = ElementDefinition.SlicingRules.Closed
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestEmptyClosedExtensionSlice()
        {
            var profile = ClosedExtensionSliceObservationProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            // dumpElements(expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.extension")), "Observation.extension constraint:");
            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            var elem = expanded.Snapshot.Element.Find(e => e.Path == "Observation.extension");
            Assert.IsNotNull(elem);
            Assert.IsNotNull(elem.Slicing);
            Assert.AreEqual(ElementDefinition.SlicingRules.Closed, elem.Slicing.Rules);
        }

        [TestMethod]
        public void TestSlicingEntryWithChilren()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/StructureDefinition/DocumentComposition");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out var expanded);

            dumpOutcome(_generator.Outcome);
            expanded.Snapshot.Element.Dump();

            // Verify that the snapshot includes the merged children of the slice entry element
            var verifier = new ElementVerifier(expanded, _settings);
            verifier.VerifyElement("Composition.section", null);
            verifier.AssertSlicing("code", ElementDefinition.SlicingRules.Open, false);
            verifier.VerifyElement("Composition.section.title", null);
            verifier.VerifyElement("Composition.section.code", null);
            Assert.IsNotNull(verifier.CurrentElement.Binding);
            Assert.AreEqual(BindingStrength.Required, verifier.CurrentElement.Binding.Strength);
            Assert.AreEqual("http://example.org/ValueSet/SectionTitles", (verifier.CurrentElement.Binding.ValueSet as ResourceReference)?.Reference);
        }

        [TestMethod]
        public void TestObservationProfileWithExtensions() => testObservationProfileWithExtensions(false);

        [TestMethod]
        public void TestObservationProfileWithExtensions_ExpandAll() => testObservationProfileWithExtensions(true);

        void testObservationProfileWithExtensions(bool expandAll)
        {
            // Same as TestObservationProfileWithExtensions, but with full expansion of all complex elements (inc. extensions!)

            // var obs = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyCustomObservation");
            var obs = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyCustomObservation3");
            Assert.IsNotNull(obs);

            StructureDefinition expanded;
            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            List<ElementDefinition> elems;
            try
            {
                generateSnapshotAndCompare(obs, out expanded);

                dumpOutcome(_generator.Outcome);

                elems = expanded.Snapshot.Element;
                elems.Dump();
                Debug.WriteLine($"Default snapshot: {elems.Count} elements");
                dumpBaseElems(elems);
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                if (expandAll)
                {
                    elems = fullyExpand(elems, issues).ToList();
                    Debug.WriteLine($"Fully expanded: {elems.Count} elements");
                }
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            // Verify that the snapshot contains three extension elements 
            var obsExtensions = elems.Where(e => e.Path == "Observation.extension").ToList();
            Assert.IsNotNull(obsExtensions);
            Assert.AreEqual(4, obsExtensions.Count); // 1 extension slice + 3 extensions

            var extSliceElem = obsExtensions[0];
            Assert.IsNotNull(extSliceElem);
            Assert.IsNotNull(extSliceElem.Slicing);
            Assert.AreEqual("url", extSliceElem.Slicing.Discriminator.FirstOrDefault().Path);

            var labelExtElem = obsExtensions[1];
            Assert.IsNotNull(labelExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/ObservationLabelExtension", labelExtElem.Type.FirstOrDefault().Profile);

            var locationExtElem = obsExtensions[2];
            Assert.IsNotNull(locationExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/ObservationLocationExtension", locationExtElem.Type.FirstOrDefault().Profile);

            var otherExtElem = obsExtensions[3];
            Assert.IsNotNull(otherExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/SomeOtherExtension", otherExtElem.Type.FirstOrDefault().Profile);

            var labelExt = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/ObservationLabelExtension");
            Assert.IsNotNull(labelExt);
            if (expandAll) { Assert.AreEqual(true, labelExt.HasSnapshot); }

            var locationExt = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/ObservationLocationExtension");
            Assert.IsNotNull(locationExt);
            if (expandAll) { Assert.AreEqual(true, locationExt.HasSnapshot); }

            // Third extension element maps to an unresolved extension definition
            var otherExt = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/SomeOtherExtension");
            Assert.IsNull(otherExt);

            // Now verify the snapshot
            // First two extension elements should have been merged from the snapshot root Extension element of the associated extension definition 
            var coreExtension = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Extension);
            Assert.IsNotNull(coreExtension);
            Assert.IsTrue(coreExtension.HasSnapshot);
            var coreExtensionRootElem = coreExtension.Snapshot.Element[0];

            var labelExtRootElem = labelExt.Differential.Element[0];
            Assert.AreEqual(1, labelExtElem.Min);                                           // Explicit Observation profile constraint
            Assert.AreEqual(labelExtRootElem.Max, labelExtElem.Max);                        // Inherited from external ObservationLabelExtension root element
            Assert.AreEqual(coreExtensionRootElem.Definition, labelExtElem.Definition);     // Inherited from Observation.extension base element
            Assert.AreEqual(labelExtRootElem.Comment, labelExtElem.Comment);              // Inherited from external ObservationLabelExtension root element
            verifyProfileExtensionBaseElement(labelExtElem);

            var locationExtRootElem = locationExt.Differential.Element[0];
            Assert.AreEqual(0, locationExtElem.Min);                                        // Inherited from external ObservationLabelExtension root element
            Assert.AreEqual("1", locationExtElem.Max);                                      // Explicit Observation profile constraint
            Assert.AreEqual(coreExtensionRootElem.Definition, locationExtElem.Definition);  // Inherited from Observation.extension base element
            Assert.AreEqual(locationExtRootElem.Comment, locationExtElem.Comment);        // Inherited from external ObservationLocationExtension root element
            verifyProfileExtensionBaseElement(locationExtElem);

            // Last (unresolved) extension element should have been merged with Observation.extension
            var coreObservation = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation);
            Assert.IsNotNull(coreObservation);
            Assert.IsTrue(coreObservation.HasSnapshot);
            var coreObsExtensionElem = coreObservation.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.extension");
            Assert.IsNotNull(coreObsExtensionElem);
            Assert.AreEqual(1, otherExtElem.Min);                                           // Explicit Observation profile constraint
            Assert.AreEqual(coreObsExtensionElem.Max, otherExtElem.Max);                    // Inherited from Observation.extension base element
            Assert.AreEqual(coreObsExtensionElem.Definition, otherExtElem.Definition);      // Inherited from Observation.extension base element
            Assert.AreEqual(coreObsExtensionElem.Comment, otherExtElem.Comment);          // Inherited from Observation.extension base element
            verifyProfileExtensionBaseElement(coreObsExtensionElem);
        }

        void verifyProfileExtensionBaseElement(ElementDefinition extElem)
        {
            var baseElem = extElem.Annotation<BaseDefAnnotation>().BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(baseElem.Short, extElem.Short);
            Assert.AreEqual(baseElem.Definition, extElem.Definition);
            Assert.AreEqual(baseElem.Comment, extElem.Comment);
            Assert.IsTrue(baseElem.Alias.SequenceEqual(extElem.Alias));
        }

#if false
        // [WMR 20170213] New - issue reported by Marten - cannot slice Organization.type ?
        // Specifically, snapshot generator drops the slicing component from the slice entry element
        // Explanation: Organization.type is not a list (max = 1) and not a choice type => slicing is not allowed!
        [TestMethod]
        public void TestOrganizationTypeSlice()
        {
            var org = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MySlicedOrganization");
            Assert.IsNotNull(org);

            StructureDefinition expanded;
            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(org, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            //dumpOutcome(_generator.Outcome);

            //var elems = expanded.Snapshot.Element;
            //elems.Dump();
            //dumpBaseElems(elems);

            // TODO: Verify slice

        }
#endif

        // [WMR 2017024] NEW: Test for bug with snapshot expansion of ElementDefinition.Binding (reported by NHS)
        // If the diff constrains only Binding.Strength, then snapshot also contains only Binding.Strength - WRONG!
        // Expected: snapshot contains inherited properties from base, i.e. description, valueSetUri/valueSetReference
        [TestMethod]
        public void TestElementBinding()
        {
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Encounter.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Encounter),
                Name = "MyTestEncounter",
                Url = "http://example.org/fhir/StructureDefinition/MyTestEncounter",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Encounter.type")
                        {

                            // Default binding on Encounter.type:
                            //
                            // <binding>
                            //   <strength value="example" />
                            //   <description value="The type of encounter" />
                            //   <valueSetReference>
                            //     <reference value="http://hl7.org/fhir/ValueSet/encounter-type" />
                            //   </valueSetReference>
                            // </binding>

                            Binding = new ElementDefinition.ElementDefinitionBindingComponent()
                            {
                                // Constrain strength from Example to Preferred
                                Strength = BindingStrength.Preferred
                            }
                        }
                    }

                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sd, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var profileElem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Encounter.type");
            Assert.IsNotNull(profileElem);
            var profileBinding = profileElem.Binding;
            Assert.IsNotNull(profileBinding);

            Assert.AreEqual(BindingStrength.Preferred, profileBinding.Strength);

            var sdEncounter = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Encounter);
            Assert.IsNotNull(sdEncounter);
            Assert.IsTrue(sdEncounter.HasSnapshot);

            var baseElem = sdEncounter.Snapshot.Element.FirstOrDefault(e => e.Path == "Encounter.type");
            Assert.IsNotNull(baseElem);
            var baseBinding = baseElem.Binding;
            Assert.IsNotNull(baseBinding);

            Assert.AreEqual(BindingStrength.Example, baseBinding.Strength);

            Assert.AreEqual(baseBinding.Description, profileBinding.Description);
            Assert.IsTrue(baseBinding.ValueSet.IsExactly(profileBinding.ValueSet));
        }

        // [WMR 2017024] NEW: Snapshot generator should reject profile extensions mapped to a StructureDefinition that is not an Extension definition.
        // Reported by Thomas Tveit Rosenlund: https://simplifier.net/Velferdsteknologi2/FlagVFT (geoPositions)
        // Don't expand; emit outcome issue
        [TestMethod]
        public void TestInvalidProfileExtensionTarget()
        {
            var sdLocation = new StructureDefinition()
            {
                Type = FHIRAllTypes.Location.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Location),
                Name = "MyTestLocation",
                Url = "http://example.org/fhir/StructureDefinition/MyTestLocation",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition()
                        {
                            Path = "Location.partOf",
                            Max = "0"
                        }
                    }
                }
            };

            var sdFlag = new StructureDefinition()
            {
                Type = FHIRAllTypes.Flag.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Flag),
                Name = "MyTestFlag",
                Url = "http://example.org/fhir/StructureDefinition/MyTestFlag",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Flag.extension")
                        {
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                // Discriminator = new string[] { "url" },
                                Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Value, Path = "url" }
                                }.ToList(),
                                Rules = ElementDefinition.SlicingRules.Open
                            }
                        },
                        new ElementDefinition("Flag.extension")
                        {
                            SliceName = "geopositions",
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
                                    // INVALID - Map extension element to non-extension definition
                                    Profile = sdLocation.Url
                                }

                            }
                        }
                    }

                }
            };

            var resolver = new InMemoryProfileResolver(sdLocation, sdFlag);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sdFlag, out var expanded);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            expanded.Snapshot.Element.Dump();

            // Expecting a single outcome issue
            dumpOutcome(_generator.Outcome);
            Assert.IsNotNull(_generator.Outcome);
            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            Assert.IsNotNull(issues);
            Assert.AreEqual(1, issues.Count);
            assertIssue(issues[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var elems = expanded.Snapshot.Element;
            issues = new List<OperationOutcome.IssueComponent>();
            elems = expanded.Snapshot.Element = fullyExpand(elems, issues).ToList();
            Debug.WriteLine($"Fully expanded: {elems.Count} elements");

            expanded.Snapshot.Element.Dump();

            // Full expansion should also generate same outcome issue
            Assert.AreEqual(1, issues.Count);
            assertIssue(issues[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE);
        }

        // [WMR 20170306] Verify that the snapshot generator determines and merges the correct base element for slices
        // * Slice entry is based on associated element in base profile with same path (and name)
        //   Slice entry inherits constraints from base element; can only further constrain
        //   Note: Base element may be a slice entry itself, or a named slice (in case of reslicing)
        // * Named slices are based on associated element in base profile with same path and parent slice name (same name as preceding slice entry)
        //   Same base element as preceding slice entry, but without the slicing component and with min = 0 (per definition for named slices, as they can be optional)

        //
        // Example:
        //
        // Patient (base profile)
        // - Patient.identifier
        //
        // MyPatient : Patient (user profile)
        // - Patient.identifier (slice entry)     => Patient.identifier (in Base)
        // - Patient.identifier:A                 => Patient.identifier (in Base)
        // - Patient.identifier:A/1               => Patient.identifier (in Base)
        // - Patient.identifier:A/2               => Patient.identifier (in Base)
        // - Patient.identifier:B                 => Patient.identifier (in Base)
        //
        // DerivedPatient : MyPatient (derived user profile)
        // - Patient.identifier (slice entry)     => Patient.identifier (slice entry) in MyPatient
        // - Patient.identifier:A                 => Patient.identifier:A in MyPatient
        // - Patient.identifier:A/1               => Patient.identifier:A/1 in MyPatient
        // - Patient.identifier:A/2               => Patient.identifier:A/2 in MyPatient
        // - Patient.identifier:A/3               => Patient.identifier:A in MyPatient
        // - Patient.identifier:B (reslice entry) => Patient.identifier:B in MyPatient
        // - Patient.identifier:B/1               => Patient.identifier:B in MyPatient
        // - Patient.identifier:B/2               => Patient.identifier:B in MyPatient
        // - Patient.identifier:C                 => Patient.identifier in MyPatient

        static StructureDefinition SlicedPatientProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "MySlicedPatient",
            Url = "http://example.org/fhir/StructureDefinition/MySlicedPatient",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            // Discriminator = new string[] { "system" },
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Value, Path = "system" }
                                }.ToList(),
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1
                    }
                    ,new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "bsn",
                        Min = 1,
                        Max = "1"
                    }
                    ,new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "ehr_id",
                        Max = "2"
                    }
                }
            }
        };

        [TestMethod]
        public void TestSliceBase_SlicedPatient()
        {
            var profile = SlicedPatientProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            StructureDefinition expanded = null;

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var identifierConstraints = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier"));

            identifierConstraints.Dump("Constraints on Patient.identifier:");

            var corePatientProfile = _testResolver.FindStructureDefinition(profile.BaseDefinition);
            Assert.IsNotNull(corePatientProfile);
            Assert.IsTrue(corePatientProfile.HasSnapshot);
            var corePatientIdentifierElem = corePatientProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(corePatientIdentifierElem);
            Debug.Print($"Base: #{corePatientIdentifierElem.GetHashCode()} '{corePatientIdentifierElem.Path}'");

            dumpBaseElems(identifierConstraints);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());

            // Verify slice entry
            Assert.IsTrue(nav.MoveToChild("identifier"));

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Empty for elements introduced by core Patient profile, esp. corePatientIdentifierElem
            // Assert.AreEqual(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
        }

        static StructureDefinition NationalPatientProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "MyNationalPatient",
            Url = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Comment = "NationalPatientProfile"
                    },
                    new ElementDefinition("Patient.identifier.system")
                    {
                        Min = 1
                    }
                }
            }
        };

        static StructureDefinition SlicedNationalPatientProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Name = "SlicedNationalPatientProfile",
            Url = "http://example.org/fhir/StructureDefinition/SlicedNationalPatientProfile",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            // Discriminator = new string[] { "system" },
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Value, Path = "system" }
                                }.ToList(),
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1,
                        // Append to comment inherited from base
                        Comment = "...SlicedNationalPatientProfile"
                    }
                    // Slice: bsn
                    ,new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "bsn",
                        Min = 1,
                        Max = "1"
                    },
                    new ElementDefinition("Patient.identifier.system")
                    {
                        Fixed = new FhirUri("http://example.org/fhir/ValueSet/bsn")
                    },
                    // Slice: ehr_id
                    new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "ehr_id",
                        Max = "2",
#if false
                        // Re-slice the ehr-id
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "use" },
                            Ordered = true,
                            Rules = ElementDefinition.SlicingRules.Closed
                        }
#endif
                    },
#if false
                    // Reslice: ehr-id/temp
                    new ElementDefinition("Patient.identifier")
                    {
                        Name = "ehr_id/temp",
                        Max = "1",
                    },
                    new ElementDefinition("Patient.identifier.use")
                    {
                        // Fixed = new Code<Identifier.IdentifierUse>(Identifier.IdentifierUse.Temp)
                        Fixed = new Code("temp")
                    }
#endif
                }
            }
        };

        [TestMethod]
        public void TestSliceBase_SlicedNationalPatient()
        {
            var baseProfile = NationalPatientProfile;
            var profile = SlicedNationalPatientProfile;

            var resolver = new InMemoryProfileResolver(baseProfile, profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            StructureDefinition expanded = null;

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var identifierConstraints = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier"));

            identifierConstraints.Dump("Constraints on Patient.identifier:");

            var nationalPatientProfile = resolver.FindStructureDefinition(profile.BaseDefinition);
            Assert.IsNotNull(nationalPatientProfile);
            Assert.IsTrue(nationalPatientProfile.HasSnapshot);
            var nationalPatientIdentifierElem = nationalPatientProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(nationalPatientIdentifierElem);
            Debug.Print($"Base: #{nationalPatientIdentifierElem.GetHashCode()} '{nationalPatientIdentifierElem.Path}'");

            dumpBaseElems(identifierConstraints);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());

            // Verify slice entry
            Assert.IsTrue(nav.MoveToChild("identifier"));

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);
            // Slice entry should inherit Comments from base element, merged with diff constraints
            Assert.AreEqual("NationalPatientProfile\r\nSlicedNationalPatientProfile", nav.Current.Comment);
            // Slice entry should also inherit constraints on child elements from base element
            var bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            // Should be merged with diff constraints on child elements
            Assert.AreEqual((nav.Current.Fixed as FhirUri).Value, "http://example.org/fhir/ValueSet/bsn");
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

#if false
            // Verify re-slice "ehr_id/temp"
            Assert.IsTrue(nav.MoveToNextSliceAtAnyLevel());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id/temp", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));
#endif
        }

        static StructureDefinition ReslicedNationalPatientProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Name = "ReslicedNationalPatientProfile",
            Url = "http://example.org/fhir/StructureDefinition/ReslicedNationalPatientProfile",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Value, Path = "system" }
                                }.ToList(),
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1,
                        // Append to comment inherited from base
                        Comment = "...SlicedNationalPatientProfile"
                    }
                    // Slice: bsn
                    ,new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "bsn",
                        Min = 1,
                        Max = "1"
                    },
                    new ElementDefinition("Patient.identifier.system")
                    {
                        Fixed = new FhirUri("http://example.org/fhir/ValueSet/bsn")
                    },
                    // Slice: ehr_id
                    new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "ehr_id",
                        Max = "2",

                        // Re-slice the ehr-id
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            // Discriminator = new string[] { "use" },
                            Discriminator = new ElementDefinition.DiscriminatorComponent[]
                                { new ElementDefinition.DiscriminatorComponent
                                    { Type = ElementDefinition.DiscriminatorType.Value, Path = "use" }
                                }.ToList(),
                            Ordered = true,
                            Rules = ElementDefinition.SlicingRules.Closed
                        }
                    },

                    // Reslice: ehr-id/temp
                    new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "ehr_id/temp",
                        Max = "1",
                    },
                    new ElementDefinition("Patient.identifier.use")
                    {
                        // Fixed = new Code<Identifier.IdentifierUse>(Identifier.IdentifierUse.Temp)
                        Fixed = new Code("temp")
                    }
                }
            }
        };

        [TestMethod]
        public void TestSliceBase_ReslicedNationalPatient()
        {
            var baseProfile = NationalPatientProfile;
            var profile = ReslicedNationalPatientProfile;

            var resolver = new InMemoryProfileResolver(baseProfile, profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            StructureDefinition expanded = null;

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var identifierConstraints = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier"));

            identifierConstraints.Dump("Constraints on Patient.identifier:");

            var nationalPatientProfile = resolver.FindStructureDefinition(profile.BaseDefinition);
            Assert.IsNotNull(nationalPatientProfile);
            Assert.IsTrue(nationalPatientProfile.HasSnapshot);
            var nationalPatientIdentifierElem = nationalPatientProfile.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(nationalPatientIdentifierElem);
            Debug.Print($"Base: #{nationalPatientIdentifierElem.GetHashCode()} '{nationalPatientIdentifierElem.Path}'");

            dumpBaseElems(identifierConstraints);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());

            // Verify slice entry
            Assert.IsTrue(nav.MoveToChild("identifier"));

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);
            // Slice entry should inherit Comments from base element, merged with diff constraints
            Assert.AreEqual("NationalPatientProfile\r\nSlicedNationalPatientProfile", nav.Current.Comment);
            // Slice entry should also inherit constraints on child elements from base element
            var bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            // Should be merged with diff constraints on child elements
            Assert.AreEqual((nav.Current.Fixed as FhirUri).Value, "http://example.org/fhir/ValueSet/bsn");
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify re-slice "ehr_id/temp"
            Assert.IsTrue(nav.MoveToFirstReslice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id/temp", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comment);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));
        }

        [TestMethod]
        public void TestSliceBase_PatientTelecomResliceEK()
        {
            var dirSource = new DirectorySource("TestData/validation");
            var source = new TimingSource(dirSource);
            var resolver = new CachedResolver(source);
            var multiResolver = new MultiResolver(resolver, _testResolver);

            var profile = resolver.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.IsNotNull(profile);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateElementIds = true;
            _generator = new SnapshotGenerator(multiResolver, settings);
            StructureDefinition expanded = null;

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Dump();

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());

            // Patient.telecom slice entry
            Assert.IsTrue(nav.MoveToChild("telecom"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual(true, nav.Current.Slicing.Ordered);
            Assert.AreEqual(ElementDefinition.SlicingRules.OpenAtEnd, nav.Current.Slicing.Rules);
            Assert.IsFalse(nav.Current.Slicing.Discriminator.Any());
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("5", nav.Current.Max);

            // Patient.telecom:phone
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("phone", nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
            Assert.IsNull(nav.Current.Slicing);

            // Patient.telecom.system
            var bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("phone", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:email
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("email", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNotNull(nav.Current.Slicing);
            // TODO: BRIAN: Need to check that this is the correct assertion here
            Assert.AreEqual("system|use", string.Join("|", nav.Current.Slicing.Discriminator.Select(s => s.Path)));
            // Assert.AreEqual(1, nav.Current.Slicing.Discriminator.SelectMany(s => s.Type.Value).Count()));
            Assert.AreEqual(ElementDefinition.SlicingRules.Closed, nav.Current.Slicing.Rules);
            // Assert.AreEqual(false, nav.Current.Slicing.Ordered);
            Assert.IsNull(nav.Current.Slicing.Ordered);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("email", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:email/home
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("email/home", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNull(nav.Current.Slicing);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("email", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.MoveToNext("use"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("home", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:email/work
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("email/work", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNull(nav.Current.Slicing);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("email", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.MoveToNext("use"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("work", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:other
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("other", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("3", nav.Current.Max);
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("system|use", string.Join("|", nav.Current.Slicing.Discriminator.Select(p => p.Path)));
            Assert.AreEqual(ElementDefinition.SlicingRules.Open, nav.Current.Slicing.Rules);
            // Assert.AreEqual(false, nav.Current.Slicing.Ordered);
            Assert.IsNull(nav.Current.Slicing.Ordered);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("other", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:other/home
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("other/home", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNull(nav.Current.Slicing);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("other", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.MoveToNext("use"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("home", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Patient.telecom:other/work
            Assert.IsTrue(nav.MoveToNext("telecom"));
            Assert.AreEqual("other/work", nav.Current.SliceName);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNull(nav.Current.Slicing);

            // Patient.telecom.system
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("other", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.MoveToNext("use"));
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("work", (nav.Current.Fixed as Code)?.Value);
            Assert.IsTrue(nav.ReturnToBookmark(bm));
        }

        [TestMethod]
        public void TestElementMappings()
        {
            var profile = _testResolver.FindStructureDefinition("http://example.org/fhir/StructureDefinition/TestMedicationStatement-prescribing");
            Assert.IsNotNull(profile);

            var diffElem = profile.Differential.Element.FirstOrDefault(e => e.Path == "MedicationStatement.informationSource");
            Assert.IsNotNull(diffElem);
            dumpMappings(diffElem);

            StructureDefinition expanded = null;
            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var elems = expanded.Snapshot.Element;
            elems.Dump();

            var elem = elems.FirstOrDefault(e => e.Path == "MedicationStatement.informationSource");
            Assert.IsNotNull(elem);
            dumpMappings(elem);

            // Snapshot element mappings should include all of the differential element mappings
            Assert.IsTrue(diffElem.Mapping.All(dm => elem.Mapping.Any(m => m.IsExactly(dm))));

        }

        static void dumpMappings(ElementDefinition elem) => dumpMappings(elem.Mapping, $"Mappings for {elem.Path}:");

        static void dumpMappings(IList<ElementDefinition.MappingComponent> mappings, string header = null)
        {
            Debug.WriteLineIf(header != null, header);
            foreach (var mapping in mappings)
            {
                Debug.Print($"{mapping.Identity} : {mapping.Map}");
            }
        }

        // Ewout: type slices cannot contain renamed elements!

        static StructureDefinition PatientNonTypeSliceProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "NonTypeSlicePatient",
            Url = "http://example.org/fhir/StructureDefinition/NonTypeSlicePatient",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.deceased[x]")
                    {
                        Min = 1,
                        // Repeat the base element types (no additional constraints)
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.Boolean.GetLiteral() },
                            new ElementDefinition.TypeRefComponent() { Code = FHIRAllTypes.DateTime.GetLiteral() }
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestPatientNonTypeSlice()
        {
            var profile = PatientNonTypeSliceProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            // Force expansion of Patient.deceased[x]
            var nav = ElementDefinitionNavigator.ForDifferential(profile);
            Assert.IsTrue(nav.MoveToFirstChild());
            var result = _generator.ExpandElement(nav);
            profile.Differential.Element.Dump();
            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(result);

            Assert.IsNull(_generator.Outcome);
        }

        // Ewout: type slices cannot contain renamed elements!
        static StructureDefinition ObservationSimpleQuantityProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "NonTypeSlicePatient",
            Url = "http://example.org/fhir/StructureDefinition/ObservationSimpleQuantityProfile",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.valueQuantity")
                    {
                        // Repeat the base element types (no additional constraints)
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                // Constrain Quantity to SimpleQuantity
                                // Code = FHIRDefinedType.Quantity,
                                // Profile = new string[] { ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.SimpleQuantity) }

                                Code = FHIRAllTypes.SimpleQuantity.GetLiteral()
                            },
                        }
                    }
                }
            }
        };

        // [WMR 20170321] NEW
        [TestMethod]
        public void TestSimpleQuantityProfile()
        {
            var profile = ObservationSimpleQuantityProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(profile, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump();
            dumpOutcome(_generator.Outcome);

            var issues = _generator.Outcome?.Issue;
            Assert.AreEqual(1, issues.Count);
            assertIssue(issues[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            issues = new List<OperationOutcome.IssueComponent>();
            var elems = expanded.Snapshot.Element;
            elems = expanded.Snapshot.Element = fullyExpand(elems, issues).ToList();
            // Generator should report same issue as during regular snapshot expansion
            Assert.AreEqual(1, issues.Count);
            assertIssue(issues[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT);

            // Ensure that renamed diff elements override base elements with original names
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            // Snapshot should not contain elements with original name
            Assert.IsFalse(nav.JumpToFirst("Observation.value[x]"));
            // Snapshot should contain renamed elements
            Assert.IsTrue(nav.JumpToFirst("Observation.valueQuantity"));
            Assert.IsNotNull(nav.Current.Type);
            Assert.AreEqual(1, nav.Current.Type.Count);
            Assert.AreEqual(FHIRAllTypes.SimpleQuantity.GetLiteral(), nav.Current.Type[0].Code);

            var type = nav.Current.Type.First();
            Debug.Print($"{nav.Path} : {type.Code} - '{type.Profile}'");
        }

        // [WMR 20170406] NEW
        // Issue reported by Vadim
        // Complex extension:   structure.cdstools-typedstage
        // Referencing Profile: structure.cdstools-basecancer
        // Profile defines constraints on child elements of the complex extension
        // Snapshot generator adds slicing component to Condition.extension.extension.extension:type - WRONG!
        [TestMethod]   // test data needs to be converted from dstu2 -> stu3
        public void TestProfileConstraintsOnComplexExtensionChildren()
        {
            var profile = _testResolver.FindStructureDefinition("https://example.org/fhir/StructureDefinition/cds-basecancer");
            Assert.IsNotNull(profile);

            profile.Differential.Element.Dump("===== Differential =====");

            StructureDefinition expanded = null;
            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
            dumpOutcome(_generator.Outcome);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var elems = expanded.Snapshot.Element;
            elems.Dump("===== Snapshot =====");

            var nav = new ElementDefinitionNavigator(elems);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("Condition", nav.Path);

            // Condition.extension (slicing entry)
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("url", nav.Current.Slicing.Discriminator?.FirstOrDefault()?.Path);
            Assert.IsNull(nav.Current.SliceName);

            // Condition.extension:typedStaging
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("typedStaging", nav.Current.SliceName);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension (slicing entry)
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("url", nav.Current.Slicing.Discriminator?.FirstOrDefault()?.Path);
            Assert.IsNull(nav.Current.SliceName);

            // Condition.extension:typedStaging.extension:summary
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("summary", nav.Current.SliceName);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension:assessment
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("assessment", nav.Current.SliceName);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension:type
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("type", nav.Current.SliceName);
            Assert.IsNull(nav.Current.Slicing); // BUG!

            // Condition.extension:typedStaging.extension:type.valueCodeableConcept
            Assert.IsTrue(nav.MoveToChild("valueCodeableConcept"));
            Assert.IsNotNull(nav.Current.Binding);
            var valueSetReference = nav.Current.Binding.ValueSet as ResourceReference;
            Assert.IsNotNull(valueSetReference);
            Assert.AreEqual(BindingStrength.Required, nav.Current.Binding.Strength);
            Assert.AreEqual("https://example.org/fhir/ValueSet/cds-cancerstagingtype", valueSetReference.Reference);
        }

        // [WMR 20170424] For debugging ElementIdGenerator

        static StructureDefinition TestQuestionnaireProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Questionnaire.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Questionnaire),
            Name = "TestQuestionnaire",
            Url = "http://example.org/fhir/StructureDefinition/MyTestQuestionnaire",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Questionnaire.url")
                    {
                        // Override default element id
                        ElementId = "CustomId"
                    },
                    // Verify that slices receive unique element id
                    new ElementDefinition("Questionnaire.code")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                            {
                                new ElementDefinition.DiscriminatorComponent()
                                {
                                    Type = ElementDefinition.DiscriminatorType.Value,
                                    Path = "system"
                                }
                            }
                        }
                    },
                    new ElementDefinition("Questionnaire.code")
                    {
                        SliceName = "CodeA"
                    },
                    new ElementDefinition("Questionnaire.code")
                    {
                        SliceName = "CodeB"
                    },
                    // cf. BasicValidationTests.ValidateOverNameRef
                    new ElementDefinition("Questionnaire.item.item.type")
                    {
                        Fixed = new Code("decimal")
                    }
                }
            }
        };

        [TestMethod]
        public void TestElementIds_Questionnaire()
        {
#if false // DEBUG
            var coreProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Questionnaire);
            Assert.IsNotNull(coreProfile);
            Debug.WriteLine("Core Questionnaire:");
            foreach (var elem in coreProfile.Differential.Element)
            {
                Debug.WriteLine($"{elem.Path} | {elem.SliceName} | Id = {elem.ElementId} | Ref = {elem.ContentReference}");
            }

            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.Update(coreProfile);
            dumpOutcome(_generator.Outcome);
#endif

            var profile = TestQuestionnaireProfile;
            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            var urlElement = profile.Differential.Element[0];

            _generator.PrepareElement += elementHandler;
            StructureDefinition expanded = null;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);

                Assert.IsNotNull(expanded);
                Assert.IsTrue(expanded.HasSnapshot);
                dumpOutcome(_generator.Outcome);
                Assert.IsNull(_generator.Outcome);

                var elems = expanded.Snapshot.Element;
                Debug.WriteLine($"Default snapshot: #{elems.Count} elements");
                dumpBaseElems(elems);

                // Verify overriden element id in default snapshot
                var elem = elems.FirstOrDefault(e => e.Path == urlElement.Path);
                Assert.IsNotNull(elem);
                Assert.AreEqual(urlElement.ElementId, elem.ElementId);

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                // IMPORTANT: also hook elementHandler event during fullExpansion, to emit (custom) base element annotations
                var issues = new List<OperationOutcome.IssueComponent>();
                elems = expanded.Snapshot.Element = fullyExpand(elems, issues).ToList();
                Assert.AreEqual(0, issues.Count);
                Debug.WriteLine($"Full expansion: #{elems.Count} elements");
                dumpBaseElems(elems);

                // ExpandElement should NOT re-generate the id of the specified element; only for newly expanded children!
                // Verify overriden element id in full expansion
                elem = elems.FirstOrDefault(e => e.Path == urlElement.Path);
                Assert.IsNotNull(elem);
                Assert.AreEqual(urlElement.ElementId, elem.ElementId);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            Debug.WriteLine("Derived Questionnaire:");
            foreach (var elem in expanded.Snapshot.Element)
            {
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Debug.WriteLine($"{elem.Path} | {elem.SliceName} | Id = {elem.ElementId} | Base Id = {baseElem?.ElementId}");
                Assert.IsNotNull(elem.ElementId);
                Assert.IsNotNull(baseElem);
                Assert.IsNotNull(baseElem.ElementId);

                if (elem.Path != urlElement.Path)
                {
                    var equalLength = !elem.Path.StartsWith("Questionnaire.item.item.");
                    assertElementIds(elem, baseElem, equalLength);
                }
            }
        }

        static void assertElementIds(ElementDefinition elem, ElementDefinition baseElem, bool equalLength = true)
        {
            // [WMR 20170614] derived profile may (further) slice the base profile
            // Element id's are not exactly equal, as the diff id's will introduce slice name(s)
            // => Strip slice names from id; path segments should be equal
            var idSegments = ElementIdGenerator.ParseId(elem.ElementId);
            var baseIdSegments = ElementIdGenerator.ParseId(baseElem.ElementId);

            // Determine if the base element has the same root (i.e. represents base profile of the same type)
            // If so, then the element ids should have the same number of segments
            if (equalLength && idSegments.FirstOrDefault() == baseIdSegments.FirstOrDefault())
            {
                Assert.AreEqual(baseIdSegments.Length, idSegments.Length);
            }

            // [WMR 20170710] Leading path segment(s) can differ, e.g. Patient.identifier.id <=> Identifier.id
            var idSegment = ElementIdSegment.Empty;
            var offset = idSegments.Length - baseIdSegments.Length;
            for (int i = 1; i < baseIdSegments.Length; i++)
            {
                idSegment = ElementIdSegment.Parse(idSegments[offset + i]);

                // Verify that the element name matches the base element name
                // Note: element ids of type slices should use original element name ending with "[x]"
                var baseIdSegment = ElementIdSegment.Parse(baseIdSegments[i]);
                Assert.AreEqual(baseIdSegment.ElementName, idSegment.ElementName);

                // If the base element id introduces a slice name, then derived element id should also include it
                // However derived profiles can introduce additional slices
                Assert.IsTrue(baseIdSegment.ElementName == null || idSegment.ElementName == baseIdSegment.ElementName);
            }

            // Verify the last element id segment = "elementName[:sliceName]"
            var basePath = elem.Base?.Path;
            var elemPath = basePath != null && ElementDefinitionNavigator.IsChoiceTypeElement(basePath) ? basePath : elem.Path;

            if (baseIdSegments.Length == 1)
            {
                // [WMR 20170710] initialize idSegment to the last segment
                idSegment = ElementIdSegment.Parse(idSegments[idSegments.Length - 1]);
            }

            Assert.AreEqual(ProfileNavigationExtensions.GetNameFromPath(elemPath), idSegment.ElementName);
            Assert.AreEqual(elem.SliceName, idSegment.SliceName);

        }

        static StructureDefinition TestPatientTypeSliceProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "TestPatientWithTypeSlice",
            Url = "http://example.org/fhir/StructureDefinition/TestPatientWithTypeSlice",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.deceasedDateTime")
                    {
                        SliceName = "deceasedDateTime",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.DateTime.GetLiteral()
                            }
                        }
                    },
                }
            }
        };

        [TestMethod]
        public void TestElementIds_PatientWithTypeSlice()
        {
            var profile = TestPatientTypeSliceProfile;
            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(profile, out var expanded);
                Assert.IsNotNull(expanded);
                Assert.IsTrue(expanded.HasSnapshot);
                dumpOutcome(_generator.Outcome);
                Assert.IsNull(_generator.Outcome);

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
                expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
                dumpIssues(issues);
                Assert.AreEqual(0, issues.Count);

                Debug.WriteLine("Patient with type slice:");
                foreach (var elem in expanded.Snapshot.Element)
                {
                    var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                    Debug.WriteLine($"{elem.Path} | {elem.SliceName} | Id = {elem.ElementId} | Base Id = {baseElem?.ElementId}");
                    Assert.IsNotNull(elem.ElementId);
                    Assert.IsNotNull(baseElem);
                    Assert.IsNotNull(baseElem.ElementId);

                    assertElementIds(elem, baseElem);
                }
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }
        }

        // [WMR 20170616] NEW - Test custom element IDs

        static StructureDefinition TestSlicedPatientWithCustomIdProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "TestSlicedPatientWithCustomIdProfile",
            Url = "http://example.org/fhir/StructureDefinition/TestSlicedPatientWithCustomIdProfile",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        ElementId = "Patient.identifier",
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                            {
                                new ElementDefinition.DiscriminatorComponent()
                                {
                                    Type = ElementDefinition.DiscriminatorType.Value,
                                    Path = "system"
                                }
                            }
                        }
                    },
                    new ElementDefinition("Patient.identifier")
                    {
                        // Slice with custom ElementID
                        ElementId = "CUSTOM",
                        SliceName = "bsn"
                    },
                    new ElementDefinition("Patient.identifier.use")
                    {
                        // Should receive ElementID = "Patient.identifier:bsn.use"
                        Min = 1
                    }
                }
            }
        };

        [TestMethod]
        public void TestElementIds_SlicedPatientWithCustomIdProfile()
        {
            var profile = TestSlicedPatientWithCustomIdProfile;
            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            const string sliceName = "bsn";
            var slice = profile.Differential.Element.FirstOrDefault(e => e.SliceName == sliceName);
            Assert.IsNotNull(slice);

            _generator.PrepareElement += elementHandler;
            StructureDefinition expanded = null;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
                Assert.IsNotNull(expanded);
                Assert.IsTrue(expanded.HasSnapshot);
                dumpOutcome(_generator.Outcome);

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
                expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
                dumpIssues(issues);
                Assert.AreEqual(0, issues.Count);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            var elems = expanded.Snapshot.Element;

            Debug.WriteLine("Sliced Patient with custom element id on slice:");
            foreach (var elem in elems)
            {
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Debug.WriteLine($"{elem.Path} | {elem.SliceName} | Id = {elem.ElementId} | Base Id = {baseElem?.ElementId}");
                Assert.IsNotNull(elem.ElementId);
                Assert.IsNotNull(baseElem);
                Assert.IsNotNull(baseElem.ElementId);

                if (elem.ElementId?.StartsWith("CUSTOM") == true)
                {
                    Assert.AreEqual(elem.SliceName, sliceName);
                }
                else
                {
                    assertElementIds(elem, baseElem);
                }
            }

            // [WMR 20170711] Additional assertions on children of named slice
            var slicePos = elems.FindIndex(e => e.SliceName == "bsn");
            Assert.AreNotEqual(-1, slicePos);
            var elemDef = elems[slicePos];
            Assert.AreEqual("Patient.identifier", elemDef.Path);
            // Verify that the id of all child elements includes parent slice name, i.e. starts with "Patient.identifier:bsn"
            for (var idx = slicePos + 1; idx < elems.Count; idx++)
            {
                elemDef = elems[idx];
                if (!ElementDefinitionNavigator.IsChildPath("Patient.identifier", elemDef.Path)) { break; }
                Assert.IsTrue(elemDef.ElementId.StartsWith("Patient.identifier:bsn"), $"Invalid element id at element #{idx}: {elemDef.ElementId}");
            }

            // [WMR 20170711] Dynamically update the slice name and re-generate ids for the subtree
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst(slice.Path));
            Assert.IsTrue(nav.MoveToNextSliceAtAnyLevel(sliceName));
            slice = nav.Current;
            Assert.AreEqual(slice.SliceName, sliceName);
            slice.SliceName = "CHANGED";
            ElementIdGenerator.Update(nav, true);

            // Verify that the id of all child elements includes updated slice name, i.e. starts with "Patient.identifier:CHANGED"
            for (var idx = slicePos + 1; idx < elems.Count; idx++)
            {
                elemDef = elems[idx];
                if (!ElementDefinitionNavigator.IsChildPath("Patient.identifier", elemDef.Path)) { break; }
                Assert.IsTrue(elemDef.ElementId.StartsWith("Patient.identifier:CHANGED"), $"Invalid element id at element #{idx}: {elemDef.ElementId}");
            }

        }

        [TestMethod]
        public void TestElementIds_SlicedPatientWithCustomIdProfile2()
        {
            var profile = _testResolver.FindStructureDefinition("http://example.org/fhir/StructureDefinition/PatientWithCustomElementIds");
            Assert.IsNotNull(profile);

            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            StructureDefinition expanded = null;
            try
            {
                generateSnapshotAndCompare(profile, out expanded);
                Assert.IsNotNull(expanded);
                Assert.IsTrue(expanded.HasSnapshot);
                dumpOutcome(_generator.Outcome);

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
                expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
                dumpIssues(issues);
                Assert.AreEqual(0, issues.Count);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            Debug.WriteLine("Sliced Patient with custom element id on slice:");
            foreach (var elem in expanded.Snapshot.Element)
            {
                var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                Debug.WriteLine($"{elem.Path} | {elem.SliceName} | Id = {elem.ElementId} | Base Id = {baseElem?.ElementId}");
                Assert.IsNotNull(elem.ElementId);
                Assert.IsNotNull(baseElem);
                Assert.IsNotNull(baseElem.ElementId);

                if (elem.ElementId?.StartsWith("CUSTOM-") != true)
                {
                    assertElementIds(elem, baseElem);
                }
            }
        }


        // [WMR 20170426] NEW - Bug with generating base element annotations for merged external type profiles?
        [TestMethod]
        public void TestPatientWithAddress()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyPatientWithAddress");
            Assert.IsNotNull(sd);

            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            try
            {
                _generator.Update(sd);
                dumpOutcome(_generator.Outcome);

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
                sd.Snapshot.Element = fullyExpand(sd.Snapshot.Element, issues).ToList();
                dumpIssues(issues);
                Assert.AreEqual(0, issues.Count);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            dumpBaseDefId(sd);

            var sdCore = _testResolver.FindStructureDefinitionForCoreType(sd.Type);
            dumpBaseDefId(sdCore);

            // Verify that main profile MyPatientWithAddress inherited
            // constraints from extension profile MyPatientExtension
            var elem = sd.Snapshot.Element.FirstOrDefault(e => e.SliceName == "patientExtension");
            Assert.IsNotNull(elem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/MyPatientExtension", elem.Type[0]?.Profile);
            var sdExt = _testResolver.FindExtensionDefinition(elem.Type[0].Profile);
            Assert.IsNotNull(sdExt);
            var extRootshort = sdExt.Differential.Element[0].Short; // Explicit constraint on ext root
            Assert.IsNotNull(extRootshort);
            Assert.IsTrue(sdExt.HasSnapshot);
            Assert.AreEqual(extRootshort, sdExt.Snapshot.Element[0].Short); // Verify propagation to snapshot
            Assert.AreEqual(extRootshort, elem.Short);  // Verify inherited by referencing profile
            var baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(extRootshort, baseElem.Short);

            // Verify that main profile MyPatientWithAddress inherited
            // constraints from element type profile MyPatientAddress
            elem = sd.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.address");
            Assert.IsNotNull(elem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/MyPatientAddress", elem.Type[0]?.Profile);
            var sdType = _testResolver.FindStructureDefinition(elem.Type[0].Profile);
            Assert.IsNotNull(sdType);
            var typeChildElem = sdType.Snapshot.Element.FirstOrDefault(e => e.Path == "Address.country");
            Assert.IsNotNull(typeChildElem);
            Assert.AreEqual("land", typeChildElem.Alias.FirstOrDefault());

            elem = sd.Snapshot.Element.FirstOrDefault(e => e.Path == "Patient.address.country");
            Assert.IsNotNull(elem);
            Assert.AreEqual("land", elem.Alias.FirstOrDefault());
            baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual("land", baseElem.Alias.FirstOrDefault());
        }

        static void dumpBaseDefId(StructureDefinition sd)
        {
            Debug.Print("===== " + sd.Name);
            Debug.Print($"{"Path".PadRight(50)}| {"Base Path".PadRight(49)}| {"Base StructureDefinition".PadRight(69)}| {"Element Id".PadRight(49)}| {"Base Element Id".PadRight(49)}");
            foreach (var elem in sd.Snapshot.Element)
            {
                var ann = elem.Annotation<BaseDefAnnotation>();
                Assert.IsNotNull(ann);
                var s49 = new string(' ', 49);
                var s69 = new string(' ', 69);
                Debug.Print($"{elem.Path.PadRight(50)}| {ann?.BaseElementDefinition?.Path?.PadRight(49) ?? s49}| {ann?.BaseStructureDefinition?.Url?.PadRight(69) ?? s69}| {elem?.ElementId?.PadRight(49) ?? s49}| {ann?.BaseElementDefinition?.ElementId?.PadRight(49) ?? s49}");
                var elemId = elem.ElementId;
                Assert.IsNotNull(elemId);
                Assert.IsTrue(elem.IsRootElement() ? elemId == sd.Type : elemId.StartsWith(sd.Type + "."));
            }
        }

        // [WMR 20170524] Added to fix bug reported by Stefan Lang

        // [WMR 20170424] For debugging ElementIdGenerator

        const string PatientIdentifierProfileUri = @"http://example.org/fhir/StructureDefinition/PatientIdentifierProfile";
        const string PatientProfileWithIdentifierProfileUri = @"http://example.org/fhir/StructureDefinition/PatientProfileWithIdentifierProfile";
        const string PatientIdentifierTypeValueSetUri = @"http://example.org/fhir/ValueSet/PatientIdentifierTypeValueSet";

        // Identifier profile with valueset binding on child element Identifier.type
        static StructureDefinition PatientIdentifierProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Identifier.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Identifier),
            Name = "PatientIdentifierProfile",
            Url = PatientIdentifierProfileUri,
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Identifier.type")
                    {
                        Min = 1,
                        Binding = new ElementDefinition.ElementDefinitionBindingComponent()
                        {
                            Strength = BindingStrength.Extensible,
                            ValueSet = new ResourceReference(PatientIdentifierTypeValueSetUri)
                        }
                    },
                }
            }
        };

        // Patient profile with type profile constraint on Patient.identifier
        // Snapshot should pick up the valueset binding on Identifier.type
        static StructureDefinition PatientProfileWithIdentifierProfile => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "PatientProfileWithIdentifierProfile",
            Url = PatientProfileWithIdentifierProfileUri,
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Identifier.GetLiteral(),
                                Profile = PatientIdentifierProfileUri
                            }
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestTypeProfileWithChildElementBinding()
        {
            var patientProfile = PatientProfileWithIdentifierProfile;
            var resolver = new InMemoryProfileResolver(patientProfile, PatientIdentifierProfile);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            try
            {
                generateSnapshotAndCompare(patientProfile, out expanded);
            }
            finally
            {
                _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpElements(expanded.Snapshot.Element);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Patient.identifier"));
            Assert.IsNotNull(nav.Current);

            // BUG: binding constraint on Identifier.type is merged onto Patient.identifier...? (parent element!)
            // FIXED [SnapshotGenerator.getSnapshotRootElement] var diffRoot = sd.Differential.GetRootElement();
            Assert.IsNull(nav.Current.Binding);

            // By default, Patient.identifier.type should NOT be included in the generated snapshot
            Assert.IsFalse(nav.MoveToChild("type"));
        }

        static StructureDefinition QuestionnaireResponseWithSlice => new StructureDefinition()
        {
            Type = FHIRAllTypes.QuestionnaireResponse.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.QuestionnaireResponse),
            Name = "QuestionnaireResponseWithSlice",
            Url = @"http://example.org/fhir/StructureDefinition/QuestionnaireResponseWithSlice",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("QuestionnaireResponse.item")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                            {
                                new ElementDefinition.DiscriminatorComponent() { Type = ElementDefinition.DiscriminatorType.Value, Path = "text" }
                            }
                        }
                    },
                    new ElementDefinition("QuestionnaireResponse.item")
                    {
                        SliceName = "Q1"
                    },
                    new ElementDefinition("QuestionnaireResponse.item")
                    {
                        SliceName = "Q2"
                    },
                    new ElementDefinition("QuestionnaireResponse.item.linkid")
                    {
                        Max = "0"
                    },
                }
            }
        };

        // Isue #387
        // https://github.com/FirelyTeam/fhir-net-api/issues/387
        // Cannot reproduce in STU3?
        // [WMR 20170713] Note: in DSTU2, the QuestionnaireResponse core resource definition
        // specifies an example binding on element "QuestionnaireResponse.group.question.answer.value[x]"
        // WITHOUT an actual valueset reference:
        //
        //   <element>
        //     <path value="QuestionnaireResponse.group.question.answer.value[x]"/>
        //     <!-- ... -->
        //     <binding>
        //       <strength value="example"/>
        //       <description value="Code indicating the response provided for a question."/>
        //     </binding>
        //     <!-- ... -->
        //   </element>
        //
        // However in STU3, the core def example binding DOES include a valueset reference.
        [TestMethod]
        public void TestQRSliceChildrenBindings()
        {
            var sd = QuestionnaireResponseWithSlice;
            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpElements(expanded.Snapshot.Element);

            // Verify the inherited example binding on QuestionnaireResponse.item.answer.value[x]
            var answerValues = expanded.Snapshot.Element.Where(e => e.Path == "QuestionnaireResponse.item.answer.value[x]").ToList();
            Assert.AreEqual(3, answerValues.Count);
            foreach (var elem in answerValues)
            {
                var binding = elem.Binding;
                Assert.IsNotNull(binding);
                Assert.AreEqual(BindingStrength.Example, binding.Strength);
                var ValueSetReference = binding.ValueSet as ResourceReference;
                Assert.IsNotNull(ValueSetReference);
                // Assert.AreEqual("http://hl7.org/fhir/ValueSet/questionnaire-answers", ValueSetReference.Url.OriginalString);
                Assert.IsTrue(ValueSetReference.Url.Equals("http://hl7.org/fhir/ValueSet/questionnaire-answers"));
                var bindingNameExtension = binding.Extension.FirstOrDefault(e => e.Url == "http://hl7.org/fhir/StructureDefinition/elementdefinition-bindingName");
                Assert.IsNotNull(bindingNameExtension);
                var bindingNameValue = bindingNameExtension.Value as FhirString;
                Assert.IsNotNull(bindingNameValue);
                Assert.AreEqual("QuestionnaireAnswer", bindingNameValue.Value);
            }
        }

        // For derived profiles, base element annotations are incorrect
        // https://trello.com/c/8h7u2qRa
        // Three layers of derived profiles: MyVitalSigns => VitalSigns => Observation
        // When expanding MyVitalSigns, the annotated base elements also include local diff constraints... WRONG!
        // As a result, Forge will not detect the existing local constraints (no yellow pen, excluded from output).

        static StructureDefinition MyDerivedObservation => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
            Name = "MyDerivedObservation",
            Url = @"http://example.org/fhir/StructureDefinition/MyDerivedObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.method")
                    {
                        Short = "DerivedMethodShort"
                    }
                }
            }
        };

        [TestMethod]
        public void TestDerivedObservation()
        {
            var derivedObs = MyDerivedObservation;
            var resolver = new InMemoryProfileResolver(derivedObs);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            // _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(derivedObs, out expanded);
            }
            finally
            {
                // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
                _generator.PrepareElement -= elementHandler;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            // dumpElements(expanded.Snapshot.Element);
            dumpBaseElems(expanded.Snapshot.Element);

            var derivedMethodElem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(derivedMethodElem);
            Assert.AreEqual("DerivedMethodShort", derivedMethodElem.Short);

            var coreObs = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation);
            Assert.IsTrue(coreObs.HasSnapshot);
            var coreMethodElem = coreObs.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(coreMethodElem);
            Assert.IsNotNull(coreMethodElem.Short);

            var annotation = derivedMethodElem.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(annotation);
            var baseElem = annotation.BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(coreMethodElem.Short, baseElem.Short);
        }

        static StructureDefinition MyMoreDerivedObservation => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = MyDerivedObservation.Url,
            Name = "MyMoreDerivedObservation",
            Url = @"http://example.org/fhir/StructureDefinition/MyMoreDerivedObservation",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.method")
                    {
                        Short = "MoreDerivedMethodShort",
                        Comment = "MoreDerivedMethodComment"
                    },
                    // Include child constraint to force full expansion of .bodySite node
                    // BUG: if we include this element, then the generated base element for .bodySite is incorrect
                    // (includes local constraints, i.e. Min = 1 ... WRONG!)
                    new ElementDefinition("Observation.method.coding.code")
                    {
                        Min = 1
                    },
                }
            }
        };

        [TestMethod]
        public void TestMoreDerivedObservation()
        {
            var derivedObs = MyDerivedObservation;
            var moreDerivedObs = MyMoreDerivedObservation;
            var resolver = new InMemoryProfileResolver(derivedObs, moreDerivedObs);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            // _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(moreDerivedObs, out expanded);
            }
            finally
            {
                // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
                _generator.PrepareElement -= elementHandler;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            // dumpElements(expanded.Snapshot.Element);
            dumpBaseElems(expanded.Snapshot.Element);

            var moreDerivedMethodElem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(moreDerivedMethodElem);
            Assert.AreEqual("MoreDerivedMethodShort", moreDerivedMethodElem.Short);

            Assert.IsTrue(derivedObs.HasSnapshot);
            var derivedMethodElem = derivedObs.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(derivedMethodElem);
            Assert.AreEqual("DerivedMethodShort", derivedMethodElem.Short);

            // MoreDerivedObservation:Observation.method.short is inherited from DerivedObservation:Observation.method.short
            var annotation = moreDerivedMethodElem.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(annotation);
            var baseElem = annotation.BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(derivedMethodElem.Short, baseElem.Short);

            // MoreDerivedObservation:Observation.method.comments is inherited from Core:Observation.method.comments
            var coreObs = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation);
            Assert.IsTrue(coreObs.HasSnapshot);
            var coreMethodElem = coreObs.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(coreMethodElem);
            Assert.IsNotNull(coreMethodElem.Comment);
            Assert.AreEqual(coreMethodElem.Comment, baseElem.Comment);
        }

        // [WMR 20170718] Test for slicing issue
        static StructureDefinition MySlicedDocumentReference => new StructureDefinition()
        {
            Type = FHIRAllTypes.Observation.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.DocumentReference),
            Name = "MySlicedDocumentReference",
            Url = "http://example.org/fhir/StructureDefinition/MySlicedDocumentReference",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("DocumentReference.content")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Description = "TEST"
                            // Min = 1 in core resource definition
                        }
                    },
                    new ElementDefinition("DocumentReference.content")
                    {
                        SliceName = "meta",
                        // Following should be considered as a constraint!
                        // As named slices should always start with Min = 0
                        Min = 1
                    },
                }
            }
        };

        // https://trello.com/c/d7EuVgZI
        // Named slices should never inherit minimum cardinality from base element.
        // Instead, named slice base should always have Min = 0
        // Only slice entry inherits cardinality from base.
        [TestMethod]
        public void TestNamedSliceMinCardinality()
        {
            var sd = MySlicedDocumentReference;
            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            // _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
                _generator.PrepareElement -= elementHandler;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            // dumpElements(expanded.Snapshot.Element);
            dumpBaseElems(expanded.Snapshot.Element);

            var coreProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.DocumentReference);
            Assert.IsNotNull(coreProfile);
            Assert.IsTrue(coreProfile.HasSnapshot);

            // Verify slice entry in snapshot
            var elems = expanded.Snapshot.Element;
            var snapSliceEntry = elems.FirstOrDefault(e => e.Path == "DocumentReference.content");
            Assert.IsNotNull(snapSliceEntry);
            Assert.IsNotNull(snapSliceEntry.Slicing);

            // Verify that slice entry inherits min cardinality from base profile
            var coreElem = coreProfile.Snapshot.Element.FirstOrDefault(e => e.Path == snapSliceEntry.Path);
            Assert.IsNotNull(coreElem);
            Assert.AreEqual(1, coreElem.Min);
            Assert.AreEqual(coreElem.Min, snapSliceEntry.Min);

            // Verify that named slices do NOT inherit min cardinality from base profile
            var diffSlice = sd.Differential.Element.FirstOrDefault(e => e.SliceName != null);
            Assert.IsNotNull(diffSlice);
            var snapSlice = elems.FirstOrDefault(e => e.SliceName == diffSlice.SliceName);
            Assert.IsNotNull(snapSlice);
            Assert.AreEqual(diffSlice.Min, snapSlice.Min);
            var sliceBaseAnn = snapSlice.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(sliceBaseAnn);
            var sliceBase = sliceBaseAnn.BaseElementDefinition;
            Assert.IsNotNull(sliceBase);
            // Verify that slice base always has Min = 0 (not inherited from base profile)
            Assert.AreEqual(0, sliceBase.Min);
        }

        // [WMR 20170718] NEW
        // Accept and handle derived profile constraints on existing slice entry in base profile

        static StructureDefinition MySlicedBasePatient => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "MySlicedBasePatient",
            Url = @"http://example.org/fhir/StructureDefinition/MySlicedBasePatient",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Description = "TEST"
                        }
                    },
                    new ElementDefinition("Patient.identifier")
                    {
                        SliceName = "bsn"
                    }
                }
            }
        };

        static StructureDefinition MyMoreDerivedPatient => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = MySlicedBasePatient.Url,
            Name = "MyMoreDerivedPatient",
            Url = @"http://example.org/fhir/StructureDefinition/MyMoreDerivedPatient",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    // Further constrain existing slice entry
                    new ElementDefinition("Patient.identifier")
                    {
                        Min = 1
                    }
                }
            }
        };

        // https://trello.com/c/Mnn0EBOg
        [TestMethod]
        public void TestConstraintOnSliceEntry()
        {
            var sd = MyMoreDerivedPatient;
            var resolver = new InMemoryProfileResolver(sd, MySlicedBasePatient);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            StructureDefinition expanded = null;
            // _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
                _generator.PrepareElement -= elementHandler;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            // dumpElements(expanded.Snapshot.Element);
            dumpBaseElems(expanded.Snapshot.Element);

            // Snapshot generator should NOT emit any issues
            // * Issue #0: Severity = 'Error' Code = 'Required' Details: '10008' Text : 'Element 'Patient.identifier' defines a slice without a name. Individual slices must always have a unique name, except extensions.' Profile: 'http://example.org/fhir/StructureDefinition/MyMoreDerivedPatient' Path: 'Patient.identifier'
            Assert.IsNull(_generator.Outcome);

            // Verify constraint on slice entry
            var elems = expanded.Snapshot.Element;
            var sliceEntry = elems.FirstOrDefault(e => e.Path == "Patient.identifier");
            Assert.IsNotNull(sliceEntry);
            Assert.IsNotNull(sliceEntry.Slicing);
            Assert.AreEqual(1, sliceEntry.Min);
            var ann = sliceEntry.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(ann);
            Assert.IsNotNull(ann.BaseElementDefinition);
            Assert.AreEqual(0, ann.BaseElementDefinition.Min);
        }

        // [WMR 20170810] https://trello.com/c/KNMYa44V
        [TestMethod]
        public void TestDosage()
        {
            // Note: resolved from TestData\snapshot-test\profiles-types.xml
            var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Dosage);
            _generator = new SnapshotGenerator(_testResolver, _settings);

            generateSnapshotAndCompare(sd, out var expanded);
            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            var elems = expanded.Snapshot.Element;
            dumpElements(elems);
            // dumpBaseElems(elems);

            foreach (var elem in elems)
            {
                Assert.IsNull(elem.SliceName, $"Error! Unexpected slice name '{elem.SliceName}' on element with path '{elem.Path}'");
            }

            // Also verify the expanded snapshot of the referenced SimpleQuantity profile
            sd = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.SimpleQuantity);
            Assert.IsNotNull(sd);
            Assert.IsTrue(sd.HasSnapshot);
            Assert.IsNull(sd.Differential.GetRootElement()?.SliceName);

            // Note: depending on the order of unit tests execution, SimpleQuantity snapshot
            // may not have been fully (re-)generated. The original snapshot (from core ZIP)
            // contains the invalid sliceName. Regenerated snapshot should be corrected.
            if (sd.Snapshot.IsCreatedBySnapshotGenerator())
            {
                Assert.IsNull(sd.Snapshot.GetRootElement()?.SliceName);
            }
        }

        static StructureDefinition MedicationStatementWithSimpleQuantitySlice => new StructureDefinition()
        {
            Type = FHIRAllTypes.MedicationStatement.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.MedicationStatement),
            Name = "MedicationStatementWithSimpleQuantitySlice",
            Url = @"http://example.org/fhir/StructureDefinition/MedicationStatementWithSimpleQuantitySlice",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = ForTypeSlice().ToList()
                        }
                    },
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        SliceName = "doseSimpleQuantity",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Quantity.GetLiteral(),
                                Profile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.SimpleQuantity)
                            }
                        }
                    },
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        SliceName = "dosePeriod",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Period.GetLiteral()
                            }
                        }
                    },

                }
            }
        };

        [TestMethod]
        public void TestSimpleQuantitySlice()
        {
            var sd = MedicationStatementWithSimpleQuantitySlice;
            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);
            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            var elems = expanded.Snapshot.Element;
            dumpElements(elems);
            // dumpBaseElems(elems);

            // Verify there is NO warning about invalid element type constraint
            Assert.IsFalse(_generator.Outcome.Issue.Any(
                i => i.Details.Coding.FirstOrDefault().Code == SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE.Code.ToString())
            );
        }

        // [WMR 20170925] BUG: Stefan Lang - Forge displays both valueString and value[x]
        // https://trello.com/c/XI8krV6j

        const string SL_HumanNameTitleSuffixUri = @"http://example.org/fhir/StructureDefinition/SL-HumanNameTitleSuffix";

        // Extension on complex datatype HumanName
        static StructureDefinition SL_HumanNameTitleSuffix => new StructureDefinition()
        {
            Type = FHIRAllTypes.Extension.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Extension),
            Name = "SL-HumanNameTitleSuffix",
            Url = SL_HumanNameTitleSuffixUri,
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Extension.url")
                    {
                        Fixed = new FhirUri(SL_HumanNameTitleSuffixUri)
                    },
                    // Constrain type to string
                    new ElementDefinition("Extension.valueString")
                    {
                        Short = "NameSuffix",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.String.GetLiteral()
                            }
                        }
                    }
                }
            }
        };

        // Profile on complex datatype HumanName with extension element
        static StructureDefinition SL_HumanNameBasis => new StructureDefinition()
        {
            Type = FHIRAllTypes.HumanName.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.HumanName),
            Name = "SL-HumanNameBasis",
            Url = @"http://example.org/fhir/StructureDefinition/SL-HumanNameBasis",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("HumanName.family.extension")
                    {
                        SliceName = "NameSuffix",
                        Max = "1",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Extension.GetLiteral(),
                                Profile = SL_HumanNameTitleSuffix.Url
                            }
                        }
                    },
                }
            }
        };

        // Profile on Patient referencing custom HumanName datatype profile
        static StructureDefinition SL_PatientBasis => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
            Name = "SL-PatientBasis",
            Url = @"http://example.org/fhir/StructureDefinition/SL-PatientBasis",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.name")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.HumanName.GetLiteral(),
                                Profile = SL_HumanNameBasis.Url
                            }
                        }
                    },
                }
            }
        };


        const string SL_NameSuffixValueSetUri = @"http://fhir.de/ValueSet/deuev/anlage-7-namenszusaetze";

        // Derived profile on Patient
        static StructureDefinition SL_PatientDerived => new StructureDefinition()
        {
            Type = FHIRAllTypes.Patient.GetLiteral(),
            BaseDefinition = SL_PatientBasis.Url,
            Name = "SL-PatientDerived",
            Url = @"http://example.org/fhir/StructureDefinition/SL-PatientDerived",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.name.family.extension")
                    {
                        SliceName = "NameSuffix",
                        MustSupport = true
                    },
                    // WRONG! Derived profiles must maintain name of inherited renamed elements
                    // => SnapshotGenerator should emit a warning
                    // new ElementDefinition("Patient.name.family.extension.value[x]")
                    // CORRECT
                    new ElementDefinition("Patient.name.family.extension.valueString")
                    {
                        Binding = new ElementDefinition.ElementDefinitionBindingComponent()
                        {
                            Strength = BindingStrength.Required,
                            ValueSet = new FhirUri(SL_NameSuffixValueSetUri)
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestPatientDe()
        {
            var sd = SL_PatientDerived;
            var resolver = new InMemoryProfileResolver(sd, SL_PatientBasis, SL_HumanNameBasis, SL_HumanNameTitleSuffix);
            var multiResolver = new MultiResolver(_testResolver, resolver);

            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);
            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            var elems = expanded.Snapshot.Element;
            dumpElements(elems);
            // dumpBaseElems(elems);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            // Verify slice entry
            Assert.IsTrue(nav.JumpToFirst("Patient.name.family.extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            // Verify first extension slice: NameSuffix
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("NameSuffix", nav.Current.SliceName);
            // Verify constraint inherited from base patient profile
            Assert.AreEqual("1", nav.Current.Max);
            // Verify constraint specified by derived patient profile
            Assert.AreEqual(true, nav.Current.MustSupport);
            Assert.IsTrue(nav.MoveToFirstChild());
            // Verify constraints on url child element inherited from extension definition
            Assert.IsTrue(nav.MoveToNext("url"));
            var url = nav.Current.Fixed as FhirUri;
            Assert.IsNotNull(url);
            Assert.AreEqual(SL_HumanNameTitleSuffixUri, url.Value);
            // Verify there are no constraints on value[x]
            Assert.IsFalse(nav.MoveToNext("value[x]"));
            // Verify merged constraints on valueString
            Assert.IsTrue(nav.MoveToNext("valueString"));
            Assert.AreEqual("NameSuffix", nav.Current.Short);
            Assert.AreEqual(1, nav.Current.Type.Count);
            Assert.AreEqual(FHIRAllTypes.String.GetLiteral(), nav.Current.Type[0].Code);
            Assert.IsNotNull(nav.Current.Binding);
            Assert.AreEqual(BindingStrength.Required, nav.Current.Binding.Strength);
            url = nav.Current.Binding.ValueSet as FhirUri;
            Assert.AreEqual(SL_NameSuffixValueSetUri, url?.Value);
        }

        // [WMR 20170927] ContentReference
        // Observation.component.referenceRange => Observation.referenceRange
        // https://trello.com/c/p1RbTjwi
        [TestMethod]
        public void TestObservationComponentReferenceRange()
        {
            var sd = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/ObservationWithComponentReferenceRange",
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
                Type = FHIRAllTypes.Observation.GetLiteral(),
                Name = "ObservationWithComponentReferenceRange",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        // Specify a child constraint on Observation.component.referenceRange
                        // in order to force child element expansion
                        new ElementDefinition("Observation.component.referenceRange.low")
                        {
                            Min = 1,
                            Fixed = new SimpleQuantity()
                            {
                                Value = 1.0m
                            }
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sd, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            // Expecting single issue about invalid sliceName on SimpleQuantity root (error in core spec)
            dumpOutcome(_generator.Outcome);
            // Assert.IsNotNull(_generator.Outcome);
            // Assert.AreEqual(1, _generator.Outcome.Issue.Count);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = new List<OperationOutcome.IssueComponent>();
            expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
            dumpIssues(issues);
            // Assert.AreEqual(1, issues.Count);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            // Verify inherited constraints on Observation.component.referenceRange.low
            Assert.IsTrue(nav.JumpToFirst("Observation.component.referenceRange.low"));
            // Verify inherited cardinality constraint { min = 1 }
            Assert.AreEqual(1, nav.Current.Min);
            // Verify inherited fixed value constraint { fixedDecimal = 1.0 }
            Assert.IsNotNull(nav.Current.Fixed);
            var q = nav.Current.Fixed as SimpleQuantity;
            Assert.IsNotNull(q);
            Assert.AreEqual(1.0m, q.Value);
        }

        // https://trello.com/c/pA4uF7IR
        [TestMethod]
        public void TestInheritedDataTypeProfileExtensions()
        {
            var sdHumanNameExtension = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/HumanNameExtension",
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Extension),
                Type = FHIRAllTypes.Extension.GetLiteral(),
                Name = "HumanNameExtension",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension.valueString")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.String.GetLiteral()
                                }
                            }
                        }
                    }
                }
            };

            var sdHumanName = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/HumanNameWithExtension",
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.HumanName),
                Type = FHIRAllTypes.HumanName.GetLiteral(),
                Name = "HumanNameWithExtension",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("HumanName.extension")
                        {
                            SliceName = "MyExtension",
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Extension.GetLiteral(),
                                    Profile = sdHumanNameExtension.Url
                                }

                            }
                        }
                    }
                }
            };

            var sdBasePatient = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/MyBasePatient",
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
                Type = FHIRAllTypes.Patient.GetLiteral(),
                Name = "MyBasePatient",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.name")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.HumanName.GetLiteral(),
                                    Profile = sdHumanName.Url
                                }
                            }
                        }
                    }
                }
            };

            var sdDerivedPatient = new StructureDefinition()
            {
                Url = "http://example.org/fhir/StructureDefinition/MyDerivedPatient",
                BaseDefinition = sdBasePatient.Url,
                Type = FHIRAllTypes.Patient.GetLiteral(),
                Name = "MyDerivedPatient",
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource
            };

            var resolver = new InMemoryProfileResolver(sdHumanNameExtension, sdHumanName, sdBasePatient, sdDerivedPatient);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sdDerivedPatient, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpOutcome(_generator.Outcome);
            dumpElements(expanded.Snapshot.Element);
            Assert.IsNull(_generator.Outcome);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = new List<OperationOutcome.IssueComponent>();
            expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
            dumpIssues(issues);
            Assert.AreEqual(0, issues.Count);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Patient.name.extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("MyExtension", nav.Current.SliceName);
        }

        // [WMR 20171004] NEW

        // Verify generated outcome issue for incompatible type profile
        // Also verify that choice type element renaming is not affected
        [TestMethod]
        public void TestIncompatibleTypeProfile()
        {
            const string extensionUrl = @"http://example.org/fhir/StructureDefinition/ValueReferenceExtension";
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Extension.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Extension),
                Name = "ValueReferenceExtension",
                Url = extensionUrl,
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension.url")
                        {
                            Fixed = new FhirUri(extensionUrl)
                        },
                        new ElementDefinition("Extension.valueReference")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    // WRONG! Should be TargetProfile
                                    // Expecting outcome issue about incompatible profile
                                    Profile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient)
                                }
                            }
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sd, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpOutcome(_generator.Outcome);
            dumpElements(expanded.Snapshot.Element);

            Assert.IsNotNull(_generator.Outcome);
            Assert.IsNotNull(_generator.Outcome.Issue);
            Assert.AreEqual(1, _generator.Outcome.Issue.Count);

            assertIssue(_generator.Outcome.Issue[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE, extensionUrl, sd.Differential.Element[1].Path);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = new List<OperationOutcome.IssueComponent>();
            expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
            dumpIssues(issues);
            Assert.AreEqual(1, issues.Count);
            assertIssue(issues[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE, extensionUrl, sd.Differential.Element[1].Path);

            // Expecting a single warning about incompatible type profile on element Extension.valueSetReference

            // Verify element renaming is not affected
            // Expecting valueReference in snapshot, not value[x]
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Extension.valueReference"));

            // [WMR 20180723] Changed: SnapshotGenerator.getStructureForTypeRef
            // Snapshot generator now also expands type.profile for resource references,
            // even if incorrect, such as in this unit test (referenced profile is on Patient, not on Reference)

            // Verify expansion of child element valueReference.reference
            // Expect expansion of core type profile for ResourceReference
            // Assert.IsTrue(nav.MoveToChild("reference"));

            // Expect expansion of (incorrect!) core type profile for Patient
            Assert.IsTrue(nav.MoveToChild("photo"));
        }

        // If an element constraint introduces multiple type profiles,
        // then the snapshot generator should not expand profile children.
        // Verify no outcome issue for incompatible type profiles
        // Also verify that choice type element renaming is not affected
        [TestMethod]
        public void TestMultipleIncompatibleTypeProfiles()
        {
            const string extensionUrl = @"http://example.org/fhir/StructureDefinition/ValueReferenceExtension";
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Extension.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Extension),
                Name = "ValueReferenceExtension",
                Url = extensionUrl,
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension.url")
                        {
                            Fixed = new FhirUri(extensionUrl)
                        },
                        new ElementDefinition("Extension.valueReference")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    // WRONG! Should be TargetProfile
                                    // Expecting outcome issue about incompatible profile
                                    Profile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient)
                                },
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    // WRONG! Should be TargetProfile
                                    // Expecting outcome issue about incompatible profile
                                    Profile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation)
                                }
                            }
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sd, out var expanded);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpOutcome(_generator.Outcome);
            dumpElements(expanded.Snapshot.Element);

            // Element specifies multiple type profiles, so snapshot generator will not try to expand
            // Expecting no warnings about incompatible type profiles
            Assert.IsNull(_generator.Outcome);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = new List<OperationOutcome.IssueComponent>();
            expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
            dumpIssues(issues);
            Assert.AreEqual(0, issues.Count);


            // Verify element renaming is not affected
            // Expecting valueReference in snapshot, not value[x]
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Extension.valueReference"));
            // Verify expansion of child element valueReference.reference
            // Expect expansion of core type profile for ResourceReference
            Assert.IsTrue(nav.MoveToChild("reference"));
        }

        // Verify that choice type elements constrained to a single type code are properly renamed,
        // even if there are multiple type options (with same code)
        // https://trello.com/c/OvQFRdCJ
        [TestMethod]
        public void TestExtensionValueReferenceRenaming()
        {
            const string extensionUrl = @"http://example.org/fhir/StructureDefinition/ValueReferenceExtension";
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Extension.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Extension),
                Name = "ValueReferenceExtension",
                Url = extensionUrl,
                Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.ComplexType,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Extension.url")
                        {
                            Fixed = new FhirUri(extensionUrl)
                        },
                        new ElementDefinition("Extension.valueReference")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    TargetProfile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient)
                                },
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    TargetProfile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation)
                                }
                            }
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            generateSnapshotAndCompare(sd, out var expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            dumpOutcome(_generator.Outcome);
            dumpElements(expanded.Snapshot.Element);

            Assert.IsNull(_generator.Outcome);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = new List<OperationOutcome.IssueComponent>();
            expanded.Snapshot.Element = fullyExpand(expanded.Snapshot.Element, issues).ToList();
            dumpIssues(issues);
            Assert.AreEqual(0, issues.Count);

            // Expecting valueReference in snapshot, not value[x]
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Extension.valueReference"));
            // Verify expansion of child element valueReference.reference
            // Expect expansion of core type profile for ResourceReference
            Assert.IsTrue(nav.MoveToChild("reference"));
        }

        [TestMethod]
        public void TestExpandBundleEntryResource()
        {
            // Verify that the snapshot generator is capable of expanding Bundle.entry.resource,
            // if constrained to a resource type

            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Bundle.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Bundle),
                Name = "BundleWithList",
                Url = @"http://example.org/fhir/StructureDefinition/BundleWithList",
                //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
                Kind = StructureDefinition.StructureDefinitionKind.Resource,
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Bundle.entry.resource")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.List.GetLiteral()
                                }
                            }
                        },
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(resolver, _testResolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            Debug.Print("===== Prepare ===== ");
            // Prepare standard snapshots for core Bundle & List

            var sdBundle = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.Bundle);
            Assert.IsNotNull(sdBundle);
            _generator.Update(sdBundle);
            Assert.IsTrue(sdBundle.HasSnapshot);

            var sdList = _testResolver.FindStructureDefinitionForCoreType(FHIRAllTypes.List);
            Assert.IsNotNull(sdList);
            _generator.Update(sdList);
            Assert.IsTrue(sdList.HasSnapshot);

            Debug.Print("===== Generate ===== ");
            // Generate custom snapshot for Bundle profile

            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out var expanded);

                dumpOutcome(_generator.Outcome);
                Assert.IsTrue(expanded.HasSnapshot);
                dumpBaseElems(expanded.Snapshot.Element);

                // Snapshot generator should NOT emit any issues
                Assert.IsNull(_generator.Outcome);

                var elems = expanded.Snapshot.Element;

                // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
                var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
                elems = fullyExpand(elems, issues).ToList();
                Assert.AreEqual(0, issues.Count);

                // Verify that Bundle.entry.resource : List was properly expanded
                var pos = elems.FindIndex(e => e.Path == "Bundle.entry.resource");
                Assert.AreNotEqual(-1, pos);
                var elem = elems[pos];
                Assert.AreEqual(FHIRAllTypes.List.GetLiteral(), elem.Type.FirstOrDefault()?.Code);

                // Verify that expanded child elements of Bundle.entry.resource contains all the elements in List snapshot
                // [WMR 20180115] Full expansion will add additional grand children, not present in defaut List snapshot
                var listElems = sdList.Snapshot.Element;
                for (int i = 1; i < listElems.Count; i++)
                {
                    var listElem = listElems[i];
                    var rebasedPath = ElementDefinitionNavigator.ReplacePathRoot(listElem.Path, "Bundle.entry.resource");
                    // Verify that full Bundle expansion contains the default List snapshot element
                    pos = elems.FindIndex(pos + 1, e => e.Path == rebasedPath);
                    Assert.AreNotEqual(-1, pos);
                }
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

        }

        // [WMR 20180115]
        // https://github.com/FirelyTeam/fhir-net-api/issues/510
        // "Missing diff annotation on ElementDefinition.TypeRefComponent"
        [TestMethod]
        public void TestConstrainedByDiff_Type()
        {
            StructureDefinition sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Patient.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient),
                Name = "MyNationalPatient",
                Url = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.name")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Profile = @"http://fhir.nl/fhir/StructureDefinition/nl-core-humanname"
                                }
                            }
                        },
                        new ElementDefinition("Patient.generalPractitioner")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    TargetProfile = @"http://fhir.nl/fhir/StructureDefinition/nl-core-organization"
                                },
                                new ElementDefinition.TypeRefComponent()
                                {
                                    TargetProfile = @"http://fhir.nl/fhir/StructureDefinition/nl-core-practitioner"
                                }
                            }
                        }
                    }
                }
            };

            // Enable annotations on snapshot elements with diff constraints
            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(_testResolver, settings);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Patient.name"));
            Assert.IsTrue(hasChanges(nav.Current));
            Assert.IsFalse(isChanged(nav.Current));
            Assert.IsTrue(hasChanges(nav.Current.Type));
            foreach (var type in nav.Current.Type)
            {
                Assert.IsTrue(isChanged(type));
            }

            Assert.IsTrue(nav.JumpToFirst("Patient.generalPractitioner"));
            Assert.IsTrue(hasChanges(nav.Current));
            Assert.IsFalse(isChanged(nav.Current)); 
            Assert.IsTrue(hasChanges(nav.Current.Type));
            foreach (var type in nav.Current.Type)
            {
                Assert.IsTrue(isChanged(type));
            }
        }

        [TestMethod]
        public void TestAuPatientWithExtensions()
        {
            // Forge issue: https://trello.com/c/Q13pabzq

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org.au/fhir/StructureDefinition/au-patient");
            Assert.IsNotNull(sd);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);

            // Verify extensions on Patient.birthDate
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Patient.birthDate.extension"));
            // 1. Extension slice intro
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            // 2. Extension: accuracyIndicator
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("accuracyIndicator", nav.Current.SliceName);
            // 3. Extension: birthTime
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("birthTime", nav.Current.SliceName);

            // Verify extensions on Patient.deceased[x]:deceasedDateTime
            Assert.IsTrue(nav.JumpToFirst("Patient.deceased[x]"));
            // 1. Type slice intro
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            // 2. Type slice: deceasedBoolean
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("deceasedBoolean", nav.Current.SliceName);
            // 3. Type slice: deceasedDateTime
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("deceasedDateTime", nav.Current.SliceName);
            // 4. Patient.deceased[x]:deceasedDateTime.extension slice intro
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            // 5. Extension: accuracyIndicator
            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("accuracyIndicator", nav.Current.SliceName);
        }

        // [WMR 20180410] Unit test to investigate issue reported by David McKillop
        [TestMethod]
        public void TestAuPatientDerived()
        {
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Patient.GetLiteral(),
                BaseDefinition = @"http://hl7.org.au/fhir/StructureDefinition/au-patient",
                Name = "AuPatientDerived",
                Url = "http://example.org/fhir/StructureDefinition/AuPatientDerived",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.deceased[x]")
                        {
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceased[x]")
                        {
                            SliceName = "deceasedBoolean",
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceased[x]")
                        {
                            SliceName = "deceasedDateTime",
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceased[x].extension")
                        {
                            SliceName = "accuracyIndicator",
                            MustSupport = true
                        }
                    }
                }

            };

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);
        }

        // [WMR 20180410] Cannot handle invalid (!) choice type element renaming within type slice
        // Exception from ElementMatcher.matchBase - choiceNames.SingleOrDefault()
        // TODO: Gracefully handle multiple matches, emit issue, use first match
        [Ignore]
        [TestMethod]
        public void TestAuPatientDerived2()
        {
            var sd = new StructureDefinition()
            {
                Type = FHIRAllTypes.Patient.GetLiteral(),
                BaseDefinition = @"http://hl7.org.au/fhir/StructureDefinition/au-patient",
                Name = "AuPatientDerived2",
                Url = "http://example.org/fhir/StructureDefinition/AuPatientDerived2",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.deceased[x]")
                        {
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceasedBoolean]")
                        {
                            SliceName = "deceasedBoolean",
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceasedDateTime")
                        {
                            SliceName = "deceasedDateTime",
                            MustSupport = true
                        },
                        new ElementDefinition("Patient.deceasedDateTime.extension")
                        {
                            SliceName = "accuracyIndicator",
                            MustSupport = true
                        }
                    }
                }

            };

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);
        }

        // [WMR 20180410] Add unit tests for content references

        public StructureDefinition QuestionnaireWithNestedItems = new StructureDefinition()
        {
            Type = FHIRAllTypes.Questionnaire.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Questionnaire),
            Name = "QuestionnaireWithNestedItems",
            Url = "http://example.org/fhir/StructureDefinition/QuestionnaireWithNestedItems",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Questionnaire.item.type")
                        {
                            Short = "level 1"
                        },
                        new ElementDefinition("Questionnaire.item.item.type")
                        {
                            Comment = "level 2"
                        }
                    }
            }
        };

        [TestMethod]
        public void TestContentReferenceQuestionnaire()
        {
            var sd = QuestionnaireWithNestedItems;

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Questionnaire.item.type"));
            Assert.AreEqual("level 1" ,nav.Current.Short);

            Assert.IsTrue(nav.JumpToFirst("Questionnaire.item.item.type"));
            Assert.AreEqual("level 2", nav.Current.Comment);
            // Level 2 should NOT inherit constraints from level 1
            Assert.AreNotEqual("level 1", nav.Current.Short);
        }

        [TestMethod]
        public void TestContentReferenceQuestionnaireDerived()
        {
            var sd = new StructureDefinition
            {
                Type = FHIRAllTypes.Questionnaire.GetLiteral(),
                BaseDefinition = QuestionnaireWithNestedItems.Url,
                Name = "QuestionnaireWithNestedItemsDerived",
                Url = "http://example.org/fhir/StructureDefinition/QuestionnaireWithNestedItemsDerived",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Questionnaire.item.type")
                        {
                            Comment = "level 1 *"
                        },
                        new ElementDefinition("Questionnaire.item.item.type")
                        {
                            Short = "level 2 *"
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sd, QuestionnaireWithNestedItems);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);

            // Constraints should be merged separately on each nesting level
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Questionnaire.item.type"));
            Assert.AreEqual("level 1", nav.Current.Short);
            Assert.AreEqual("level 1 *", nav.Current.Comment);

            Assert.IsTrue(nav.JumpToFirst("Questionnaire.item.item.type"));
            Assert.AreEqual("level 2", nav.Current.Comment);
            Assert.AreEqual("level 2 *", nav.Current.Short);
        }

        // [WMR 20180604] Issue #611
        // https://github.com/FirelyTeam/fhir-net-api/issues/611

        [TestMethod]
        public void TestSnapshotForDerivedSlice()
        {
            var sdBase = new StructureDefinition
            {
                Type = FHIRAllTypes.PractitionerRole.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.PractitionerRole),
                Name = "BasePractitionerRole",
                Url = "http://example.org/fhir/StructureDefinition/BasePractitionerRole",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                                {
                                    new ElementDefinition.DiscriminatorComponent()
                                    {
                                        Type = ElementDefinition.DiscriminatorType.Value,
                                        Path = "system"
                                    },
                                },
                            }
                        },
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            SliceName = "foo",
                            Max = "1",
                        },
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            SliceName = "bar",
                            Max = "1",
                        },
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            SliceName = "baz",
                            Max = "1",
                        }
                    }
                }
            };

            var sdDerived = new StructureDefinition()
            {
                Type = FHIRAllTypes.PractitionerRole.GetLiteral(),
                BaseDefinition = sdBase.Url,
                Name = "DerivedPractitionerRole",
                Url = "http://example.org/fhir/StructureDefinition/DerivedPractitionerRole",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            Min = 1,
                        },
                        new ElementDefinition("PractitionerRole.identifier")
                        {
                            SliceName = "bar",
                            Min = 1,
                        }
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(sdBase, sdDerived);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sdDerived, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);

            Assert.IsTrue(nav.JumpToFirst("PractitionerRole.identifier"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);    // Derived profile constraint

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("PractitionerRole.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("foo", nav.Current.SliceName);

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("PractitionerRole.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bar", nav.Current.SliceName);
            Assert.AreEqual(1, nav.Current.Min);    // Derived profile constraint
            Assert.AreEqual("1", nav.Current.Max);  // Base profile constraint

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("PractitionerRole.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("baz", nav.Current.SliceName);

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreNotEqual("PractitionerRole.identifier", nav.Path);

        }


        // [WMR 20180611] New: Forge issue "Only first item in code field for element is saved"
        // Issue: if element in diff specifies multiple codes with only display values,
        // then element in snapshot only contains the first code entry.

        [TestMethod]
        public void TestObservationWithDisplayCodes()
        {
            var sd = new StructureDefinition
            {
                Type = FHIRAllTypes.Observation.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Observation),
                Name = "ObservationWithDisplayCodes",
                Url = "http://example.org/fhir/StructureDefinition/ObservationWithDisplayCodes",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation.code")
                        {
                            Code = new List<Coding>()
                            {
                                new Coding() { Display = "foo" },
                                new Coding() { Display = "bar" }
                            }
                        },
                    }
                }
            };


            var resolver = new InMemoryProfileResolver(sd);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            var generator = _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            // Expecting single issue about invalid slice name on SimpleQuantity root element
            var outcome = generator.Outcome;
            //Assert.IsNull(outcome);
            Assert.AreEqual(1, outcome.Issue.Count);
            assertIssue(outcome.Issue[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsNotNull(nav);
            Assert.IsTrue(nav.JumpToFirst("Observation.code"));
            var elem = nav.Current;
            Assert.IsNotNull(elem);
            // Verify that both codings are included in the snapshot
            Assert.AreEqual(2, elem.Code.Count);
            Assert.AreEqual("foo", elem.Code[0].Display);
            Assert.AreEqual("bar", elem.Code[1].Display);
        }

        [TestMethod]
        public void TestReferenceTargetProfile()
        {
            // Verify that the snapshot generator correctly expands elements with a targetProfile (on ResourceReference itself)
            var ReferenceProfile = new StructureDefinition()
            {
                Type = FHIRAllTypes.Reference.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Reference),
                Name = "MyCustomReference",
                Url = "http://example.org/fhir/StructureDefinition/MyCustomReference",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Reference")
                        {
                            Comment = "CustomReference"
                        },
                        new ElementDefinition("Reference.reference")
                        {
                            Min = 1
                        },
                    }
                }
            };

            var ReportProfile = new StructureDefinition()
            {
                Type = FHIRAllTypes.DiagnosticReport.GetLiteral(),
                BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.DiagnosticReport),
                Name = "MyDiagnosticReport",
                Url = "http://example.org/fhir/StructureDefinition/MyDiagnosticReport",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("DiagnosticReport.imagingStudy")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Reference.GetLiteral(),
                                    Profile = ReferenceProfile.Url,
                                    TargetProfile = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.ImagingStudy)
                                }
                            }
                        },
                        // Add child element constraint to force expansion
                        //new ElementDefinition("DiagnosticReport.imagingStudy.identifier")
                        //{
                        //    Max = "0"
                        //}
                    }
                }
            };

            var resolver = new InMemoryProfileResolver(ReportProfile, ReferenceProfile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            var generator = _generator = new SnapshotGenerator(multiResolver, _settings);

            generateSnapshotAndCompare(ReportProfile, out StructureDefinition expanded);

            dumpOutcome(generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            // Expecting single issue about invalid slice name on SimpleQuantity root element
            var outcome = generator.Outcome;
            Assert.IsNull(outcome);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("DiagnosticReport.imagingStudy"));
            Assert.IsNotNull(nav.Current);
            // Verify that snapshot generator merges constraints from external ReferenceProfile
            Assert.AreEqual("CustomReference", nav.Current.Comment);
            Assert.IsNotNull(nav.Current.Type);
            Assert.AreEqual(1, nav.Current.Type.Count);
            Assert.AreEqual(FHIRAllTypes.Reference.GetLiteral(), nav.Current.Type[0].Code);
            Assert.AreEqual(ReferenceProfile.Url, nav.Current.Type[0].Profile);
            // By default, snapshot generator does not expand children of element DiagnosticReport.imagingStudy
            Assert.IsFalse(nav.HasChildren);

            // Explicitly expand children of element DiagnosticReport.imagingStudy
            Assert.IsTrue(generator.ExpandElement(nav));
            Assert.IsTrue(nav.HasChildren);
            Assert.IsTrue(nav.MoveToChild("reference"));
            Assert.IsNotNull(nav.Current);
            // Verify profile inherits constraint from external targetProfile on Reference
            Assert.AreEqual(1, nav.Current.Min);
        }
    }
}
