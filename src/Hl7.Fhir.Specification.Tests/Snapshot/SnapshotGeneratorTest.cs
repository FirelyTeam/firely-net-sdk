/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableSnapshotGeneratorTest
#else
    public class SnapshotGeneratorTest
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
            GenerateElementIds = false // STU3
        };

        [TestInitialize]
        public void Setup()
        {
            FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            var dirSource = new DirectorySource("TestData/snapshot-test", new DirectorySourceSettings { IncludeSubDirectories = true });
            _source = new TimingSource(dirSource);
            _testResolver = new CachedResolver(_source);
        }

        // [WMR 20160718] Generate snapshot for extension definition fails with exception:
        // System.ArgumentException: structure is not a constraint or extension

#if false
        [TestMethod]
        public void FindDerivedExtensions()
        {
            var sdUris = _source.ListResourceUris(ResourceType.StructureDefinition);
            foreach (var uri in sdUris)
            {
                var sd = _source.FindStructureDefinition(uri);
                if (sd.ConstrainedType == FHIRDefinedType.Extension && sd.Base != "http://hl7.org/fhir/StructureDefinition/Extension")
                {
                    var origin = sd.Annotation<OriginInformation>();
                    Debug.Print($"Derived extension: uri = '{uri}' origin = '{origin?.Origin}'");
                }
            }

            // var sdInfo = testSD.Annotation<OriginInformation>();
        }
#endif

        [TestMethod]
        public void GenerateExtensionSnapshot()
        {
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-research-authorization");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-legal-case");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/us-core-religion");
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
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

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

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

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        public void GenerateRepeatedSnapshot()
        {
            // [WMR 20161005] This generated exceptions in an early version of the snapshot generator (fixed)

            StructureDefinition expanded;
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/measurereport");
            generateSnapshotAndCompare(sd, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);

            sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/clinicaldocument");
            generateSnapshotAndCompare(sd, out expanded);
            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }


        [TestMethod, Ignore]
        public void TestFullyExpandCorePatient_Old()
        {
            // [WMR 20161005] This simulates custom Forge post-processing logic
            // i.e. perform a regular snapshot expansion, then explicitly expand all complex elements (esp. those without any differential constraints)

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(sd);
            generateSnapshot(sd);
            Assert.IsTrue(sd.HasSnapshot);
            var elems = sd.Snapshot.Element;
            Assert.AreEqual("Patient.identifier", elems[9].Path);
            Assert.AreEqual("Patient.active", elems[10].Path);

            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            var expanded = fullyExpand(sd.Snapshot.Element, issues);

            Assert.IsNotNull(expanded);

            var tempPath = Path.GetTempPath();
            var sdSave = (StructureDefinition)sd.DeepCopy();
            sdSave.Snapshot.Element = expanded.ToList();
            File.WriteAllText(Path.Combine(tempPath, "snapshotgen-dest.xml"), new FhirXmlSerializer().SerializeToString(sdSave));

            foreach (var elem in expanded)
            {
                Debug.WriteLine("{0}  |  {1}", elem.Path, elem.Base != null ? elem.Base.Path : null);
            }

            int i = expanded.FindIndex(e => e.Path == "Patient.identifier");
            Assert.IsTrue(i > -1);
            // Assert.AreEqual("Patient.identifier", expanded[++i].Path);
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

        // [WMR 20170424] For debugging SnapshotBaseComponentGenerator
        [TestMethod]
        public void TestFullyExpandCoreOrganization()
        {
            // [WMR 20161005] This simulates custom Forge post-processing logic
            // i.e. perform a regular snapshot expansion, then explicitly expand all complex elements (esp. those without any differential constraints)

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Organization");
            var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Organization);
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
        IList<ElementDefinition> fullyExpand(IList<ElementDefinition> elements, List<OperationOutcome.IssueComponent> issues)
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
                if (_generator.Outcome != null)
                {
                    issues.AddRange(_generator.Outcome.Issue);
                }

                Debug.Print($"[{nameof(fullyExpand)}] " + nav.Path);
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
            var type = element.PrimaryType();
            var typeCode = type?.Code;
            return typeCode.HasValue
                   && element.Type.Count == 1
                   // [WMR 20170424] WRONG! Must expand BackboneElements
                   // && typeCode != FHIRAllTypes.BackboneElement.GetLiteral()
                   // [WMR 20180111] WRONG! Must also expand resource types, e.g. Bundle.entry.resource
                   // && ModelInfo.IsDataType(typeCode.Value)
                   && isComplexDataTypeOrResource(typeCode.Value)
                   && (
                        // Only expand extension elements with a custom name or profile
                        // Do NOT expand the core Extension.extension element, as this will trigger infinite recursion
                        typeCode.Value != FHIRDefinedType.Extension
                        || type.Profile.Any()
                        || element.Name != null
                   );
        }

        static bool isComplexDataTypeOrResource(FHIRDefinedType type) => !ModelInfo.IsPrimitive(type);

        // [WMR 20180115] OBSOLETE - See TestFullyExpandCorePatient
        [Ignore]
        [TestMethod]
        public void TestCorePatientExpandAllWithEvent()
        {
            // [WMR 20170105] New - hook new BeforeExpand event in order to force full expansion of all complex elements
            // Note: BeforeExpandElement is only raised for diff constraints, not for all snapshot elements...!
            // => Cannot use this to fully expand a sparse diff
            // => first generate regular snapshot, then re-run on result to expand all

            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/PatientWithCustomIdentifier");

            Assert.IsNotNull(sd);

            // generateSnapshot(sd);
            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.BeforeExpandElement += beforeExpandElementHandler;
            StructureDefinition expanded = null;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                _generator.BeforeExpandElement -= beforeExpandElementHandler;
            }

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            var elems = expanded.Snapshot.Element;

            //foreach (var elem in elems)
            //{
            //    Debug.WriteLine("{0}  |  {1}", elem.Path, elem.Base?.Path);
            //}
            Debug.WriteLine("Patient snapshot:");
            dumpBaseElems(elems);

            // [WMR 20180115] Problem: beforeExpandElementHandler also causes full expansion of referenced external profiles...
            // WRONG! Recursive calls should generate a regular snapshot
            var sdIdentifier = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Identifier);
            Assert.IsNotNull(sdIdentifier);
            Assert.IsTrue(sdIdentifier.HasSnapshot);
            Debug.WriteLine("Identifier snapshot:");
            dumpBaseElems(sdIdentifier.Snapshot.Element);

            int i = elems.FindIndex(e => e.Path == "Patient.identifier");
            Assert.IsTrue(i > -1);
            // Assert.AreEqual("Patient.identifier", expanded[++i].Path);
            Assert.AreEqual("Patient.identifier.id", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.extension", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.use", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.id", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.extension", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.id", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.extension", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.system", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.version", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.code", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.display", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.coding.userSelected", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.type.text", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.system", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.value", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.period", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.period.id", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.period.extension", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.period.start", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.period.end", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.id", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.extension", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.reference", elems[++i].Path);
            Assert.AreEqual("Patient.identifier.assigner.display", elems[++i].Path);

            for (int j = 1; j < elems.Count; j++)
            {
                if (isExpandableElement(elems[j]))
                {
                    verifyExpandElement(elems[j], elems, elems);
                }
            }
        }

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
            Assert.AreEqual(304, fullElems.Count);
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
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
            }

            Assert.IsNotNull(snapshot);
            Assert.IsTrue(snapshot.HasSnapshot);

            var snapElems = snapshot.Snapshot.Element;
            Debug.WriteLine($"Default snapshot: {snapElems.Count} elements");
            dumpBaseElems(snapElems);
            Assert.AreEqual(60, snapElems.Count);

            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            var fullElems = fullyExpand(snapElems, issues);
            Debug.WriteLine($"Full expansion: {fullElems.Count} elements");
            dumpBaseElems(fullElems);
            Assert.AreEqual(334, fullElems.Count);

            // Expecting issues about missing external extension definitions
            dumpIssues(issues);
            Assert.AreEqual(13, issues.Count);

            // Verify
            for (int j = 1; j < fullElems.Count; j++)
            {
                if (isExpandableElement(fullElems[j]))
                {
                    verifyExpandElement(fullElems[j], fullElems, fullElems);
                }
            }
        }


        // [WMR 20180115] OBSOLETE - See TestFullyExpandCoreOrganizationNL
        [Ignore]
        [TestMethod]
        public void TestCoreOrganizationNL()
        {
            // core-organization-nl references extension core-address-nl
            // BUG: expanded extension child elements have incorrect .Base.Path ...?!
            // e.g. Organization.address.type - Base = Organization.address.use
            // Fixed by adding conditional to copyChildren

            var sd = _testResolver.FindStructureDefinition(@"http://fhir.nl/fhir/StructureDefinition/nl-core-organization");
            Assert.IsNotNull(sd);

            _generator = new SnapshotGenerator(_testResolver, _settings);
            _generator.PrepareElement += elementHandler;
            _generator.BeforeExpandElement += beforeExpandElementHandler;
            StructureDefinition expanded = null;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
                _generator.BeforeExpandElement -= beforeExpandElementHandler;
            }

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            var elems = expanded.Snapshot.Element;

            foreach (var elem in elems)
            {
                Debug.WriteLine("{0}  |  {1}", elem.Path, elem.Base?.Path);
            }

            for (int j = 1; j < elems.Count; j++)
            {
                // [WMR 20170306] Problem: isExpandableElement now receives the already merged snapshot element
                // Result may now be different than before, e.g. because type has been merged
                // HACK: Explicitly exclude Organization.type (no child constraints in diff)

                if (isExpandableElement(elems[j])
                    && elems[j].Path != "Organization.type")
                {
                    verifyExpandElement(elems[j], elems, elems);
                }
            }
        }

        // [WMR 20180115] Obsolete - full expansion via BeforeExpandElement event is flawed...
        // Instead, call the fullyExpand() method
        void beforeExpandElementHandler(object sender, SnapshotExpandElementEventArgs e)
        {
            // [WMR 20180115] Issue: we only want to fully expand the top-level profile
            // Snapshot generator may recurse to generate dependent snapshots
            // However for these external profiles, we want to generate a regular snapshot

            // Attempt: inspect the current snapshot recursion stack
            // Problem: inlined complex types no longer get fully expanded
            // Generator would need to re-enumerate child elements introduced by external profiles

            //if (sender is SnapshotGenerator gen && gen.RecursionDepth > 1)
            //{
            //    Debug.WriteLine($"[beforeExpandElementHandler] #{e.Element.GetHashCode()} '{e.Element.Path}' | HasChildren = {e.HasChildren} | MustExpand = {e.MustExpand} | RecursionDepth = {gen.RecursionDepth}");
            //    return;
            //}

            var isExpandable = isExpandableElement(e.Element);

            Debug.WriteLine($"[beforeExpandElementHandler] #{e.Element.GetHashCode()} '{e.Element.Path}' | HasChildren = {e.HasChildren} | MustExpand = {e.MustExpand} => {isExpandable}");

            // Never clear flag if already set by snapshot generator...!
            e.MustExpand |= isExpandable;
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
                generateSnapshotAndCompare(sd, out expanded);
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
        public void GenerateDerivedProfileSnapshot()
        {
            // [WMR 20161005] Verify that the snapshot generator supports profiles on profiles

            // cqif-guidanceartifact profile is derived from cqif-knowledgemodule
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-patient");
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-encounter");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

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
            var matches = elements.Element.Where(e => e.Path == path && e.Name == name).ToArray();
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
            StructureDefinition expanded = null;
            var structure = _testResolver.FindStructureDefinition(url);
            Assert.IsNotNull(structure);
            Assert.IsTrue(structure.HasSnapshot);
            preprocessor?.Invoke(structure);
            generateSnapshotAndCompare(structure, out expanded);
            dumpOutcome(_generator.Outcome);
            return expanded;
        }

        static void ensure(StructureDefinition structure, ElementDefinition insertBefore, params ElementDefinition[] inserts)
            => ensure(structure.Differential.Element, insertBefore, inserts);

        static void ensure(List<ElementDefinition> elements, ElementDefinition insertBefore, params ElementDefinition[] inserts)
        {
            var idx = elements.FindIndex(e => e.Path == insertBefore.Path && e.Name == insertBefore.Name);
            Assert.AreNotEqual(-1, idx, $"Warning! insertBefore element is missing. Path = '{insertBefore.Path}', Name = '{insertBefore.Name}'.");
            foreach (var insert in inserts)
            {
                var idx2 = elements.FindIndex(e => e.Path == insert.Path && e.Name == insert.Name);
                Assert.AreEqual(-1, idx2, $"Warning! insert element is already present. Path = '{insert.Path}', Name = '{insert.Name}'.");
            }
            elements.InsertRange(idx, inserts);
        }

        [TestMethod]
        // [Ignore]
        public void GeneratePatientWithExtensionsSnapshot()
        {
            // [WMR 20161005] Very complex set of examples by Chris Grenz
            // https://github.com/chrisgrenz/FHIR-Primer/blob/master/profiles/patient-extensions-profile.xml
            // Manually downgraded from FHIR v1.4.0 to v1.0.2

            StructureDefinition sd;
            ElementVerifier verifier;

            _settings.GenerateElementIds = true;

            // http://example.com/fhir/StructureDefinition/patient-legal-case
            // http://example.com/fhir/StructureDefinition/patient-legal-case-lead-counsel

            // Verify complex extension used by patient-with-extensions profile
            // patient-research-authorization-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/StructureDefinition/patient-research-authorization");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Extension.extension", null, "Extension.extension");
            verifier.VerifyElement("Extension.extension", "type", "Extension.extension:type");
            verifier.VerifyElement("Extension.extension.url", "type.url", "Extension.extension:type.url", new FhirUri("type"));
            verifier.VerifyElement("Extension.extension", "flag", "Extension.extension:flag");
            verifier.VerifyElement("Extension.extension.url", "flag.url", "Extension.extension:flag.url", new FhirUri("flag"));
            verifier.VerifyElement("Extension.extension", "date", "Extension.extension:date");
            verifier.VerifyElement("Extension.extension.url", "date.url", "Extension.extension:date.url", new FhirUri("date"));
            verifier.VerifyElement("Extension.url", null, "Extension.url", new FhirUri(sd.Url));

            // Basic Patient profile that references a set of extensions
            // patient-extensions-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-with-extensions");
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.extension", null, "Patient.extension");
            verifier.VerifyElement("Patient.extension", "doNotCall", "Patient.extension:doNotCall");
            verifier.VerifyElement("Patient.extension", "legalCase", "Patient.extension:legalCase");
            verifier.VerifyElement("Patient.extension.valueBoolean", "legalCase.valueBoolean", "Patient.extension:legalCase.valueBoolean");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.valueBoolean.extension");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", "legalCase.valueBoolean.leadCounsel", "Patient.extension:legalCase.valueBoolean.extension:leadCounsel");
            verifier.VerifyElement("Patient.extension", "religion", "Patient.extension:religion");
            verifier.VerifyElement("Patient.extension", "researchAuth", "Patient.extension:researchAuth");

            // Each of the following profiles is derived from the previous profile

            // patient-name-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-name-slice"
                , structure => ensure(structure,
                     new ElementDefinition() { Path = "Patient.name.use", Name = "maidenName.use" },
                     // Add named parent slicing entry
                     new ElementDefinition() { Path = "Patient.name", Name = "maidenName" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.name", null, "Patient.name");
            verifier.VerifyElement("Patient.name", "officialName", "Patient.name:officialName");
            verifier.VerifyElement("Patient.name.text", "officialName.text", "Patient.name:officialName.text");
            verifier.VerifyElement("Patient.name.family", "officialName.family", "Patient.name:officialName.family");
            verifier.VerifyElement("Patient.name.given", "officialName.given", "Patient.name:officialName.given");
            verifier.VerifyElement("Patient.name.use", "officialName.use", "Patient.name:officialName.use");
            Assert.AreEqual((verifier.Current.Fixed as Code)?.Value, "official");
            verifier.VerifyElement("Patient.name", "maidenName", "Patient.name:maidenName");
            verifier.VerifyElement("Patient.name.use", "maidenName.use", "Patient.name:maidenName.use");
            Assert.AreEqual((verifier.Current.Fixed as Code)?.Value, "maiden");
            verifier.VerifyElement("Patient.name.family", "maidenName.family", "Patient.name:maidenName.family");

            // patient-telecom-slice-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-telecom-slice"
                , structure => ensure(structure,
                     new ElementDefinition() { Path = "Patient.telecom.system", Name = "workEmail.system" },
                     // Add named parent slicing entry
                     new ElementDefinition() { Path = "Patient.telecom", Name = "workEmail" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.telecom", null, "Patient.telecom");
            verifier.VerifyElement("Patient.telecom", "homePhone", "Patient.telecom:homePhone");
            verifier.VerifyElement("Patient.telecom.system", "homePhone.system", "Patient.telecom:homePhone.system", new Code("phone"));
            verifier.VerifyElement("Patient.telecom.use", "homePhone.use", "Patient.telecom:homePhone.use", new Code("home"));
            verifier.VerifyElement("Patient.telecom", "mobilePhone", "Patient.telecom:mobilePhone");
            verifier.VerifyElement("Patient.telecom.system", "mobilePhone.system", "Patient.telecom:mobilePhone.system", new Code("phone"));
            verifier.VerifyElement("Patient.telecom.use", "mobilePhone.use", "Patient.telecom:mobilePhone.use", new Code("mobile"));
            verifier.VerifyElement("Patient.telecom", "homeEmail", "Patient.telecom:homeEmail");
            verifier.VerifyElement("Patient.telecom.system", "homeEmail.system", "Patient.telecom:homeEmail.system", new Code("email"));
            verifier.VerifyElement("Patient.telecom.use", "homeEmail.use", "Patient.telecom:homeEmail.use", new Code("home"));
            verifier.VerifyElement("Patient.telecom", "workEmail", "Patient.telecom:workEmail");
            verifier.VerifyElement("Patient.telecom.system", "workEmail.system", "Patient.telecom:workEmail.system", new Code("email"));
            verifier.VerifyElement("Patient.telecom.use", "workEmail.use", "Patient.telecom:workEmail.use", new Code("work"));
            verifier.VerifyElement("Patient.telecom", "pager", "Patient.telecom:pager");
            verifier.VerifyElement("Patient.telecom.system", "pager.system", "Patient.telecom:pager.system", new Code("pager"));

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
                , structure => ensure(structure,
                     new ElementDefinition() { Path = "Patient.identifier.use", Name = "mrn/officialMRN.use" },
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.identifier", Name = "mrn/officialMRN" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.VerifyElement("Patient.identifier", "mrn/officialMRN", "Patient.identifier:mrn/officialMRN");
            verifier.VerifyElement("Patient.identifier.use", "mrn/officialMRN.use", "Patient.identifier:mrn/officialMRN.use", new Code("official"));
            verifier.VerifyElement("Patient.identifier", "mdmId", "Patient.identifier:mdmId");

            // Verify constraints on named slice in base profile
            // patient-identifier-slice-extension-profile.xml
            sd = generateSnapshot(@"http://example.com/fhir/SD/patient-identifier-subslice"
                , structure => ensure(structure,
                     new ElementDefinition() { Path = "Patient.identifier.extension", Name = "mrn.issuingSite" },
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.identifier", Name = "mrn" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.AssertSlicing(new string[] { "system" }, ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.AssertSlicing(new string[] { "use" }, ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier.extension", null, "Patient.identifier:mrn.extension");
            verifier.VerifyElement("Patient.identifier.extension", "mrn.issuingSite", "Patient.identifier:mrn.extension:issuingSite");
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
                , structure => ensure(structure,
                     new ElementDefinition() { Path = "Patient.extension.extension.value[x]", Name = "researchAuth/grandfatheredResAuth.type.value[x]" },
                     // Add named parent reslicing entry
                     new ElementDefinition() { Path = "Patient.extension", Name = "researchAuth/grandfatheredResAuth" },
                     new ElementDefinition() { Path = "Patient.extension.extension", Name = "type" }
                     // new ElementDefinition() { Path = "Patient.extension.extension", Name = "researchAuth/grandfatheredResAuth.type" }
                 )
            );
            verifier = new ElementVerifier(sd, _settings);
            verifier.VerifyElement("Patient.extension", null, "Patient.extension");
            verifier.VerifyElement("Patient.extension", "doNotCall", "Patient.extension:doNotCall");
            verifier.VerifyElement("Patient.extension", "legalCase", "Patient.extension:legalCase");
            verifier.VerifyElement("Patient.extension.valueBoolean", "legalCase.valueBoolean", "Patient.extension:legalCase.valueBoolean");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", null, "Patient.extension:legalCase.valueBoolean.extension");
            verifier.VerifyElement("Patient.extension.valueBoolean.extension", "legalCase.valueBoolean.leadCounsel", "Patient.extension:legalCase.valueBoolean.extension:leadCounsel");
            verifier.VerifyElement("Patient.extension", "religion", "Patient.extension:religion");
            verifier.VerifyElement("Patient.extension", "researchAuth", "Patient.extension:researchAuth");
            // Note: in the original snapshot, the "researchAuth" complex extension slice is fully expanded (child extensions: type, flag, date)
            // However this is not necessary, as there are no child constraints on the extension

            // [WMR 20161216] TODO: Merge slicing entry
            verifier.AssertSlicing(new string[] { "type.value[x]" }, ElementDefinition.SlicingRules.Open, null);

            // [WMR 20161208] TODO...

            // "researchAuth/grandfatheredResAuth" represents a reslice of the base extension "researchAuth" (0...*)
            verifier.VerifyElement("Patient.extension", "researchAuth/grandfatheredResAuth", "Patient.extension:researchAuth/grandfatheredResAuth");

            // [WMR 20161216] TODO: Merge slicing entry
            verifier.VerifyElement("Patient.extension.extension", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension");
            verifier.AssertSlicing(new string[] { "url" }, ElementDefinition.SlicingRules.Open, false);

            // The reslice "researchAuth/grandfatheredResAuth" has a child element constraint on "type.value[x]"
            // Therefore the complex extension is fully expanded (child extensions: type, flag, date)
            verifier.VerifyElement("Patient.extension.extension", "type", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.url", new FhirUri("type"));
            // Child constraints on "type.value[x]" merged from differential
            verifier.VerifyElement("Patient.extension.extension.value[x]", "researchAuth/grandfatheredResAuth.type.value[x]", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.value[x]");
            verifier.VerifyElement("Patient.extension.extension", "flag", "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag.url", new FhirUri("flag"));
            verifier.VerifyElement("Patient.extension.extension", "date", "Patient.extension:researchAuth/grandfatheredResAuth.extension:date");
            verifier.VerifyElement("Patient.extension.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension:date.url", new FhirUri("date"));
            verifier.VerifyElement("Patient.extension.url", null, "Patient.extension:researchAuth/grandfatheredResAuth.url", new FhirUri(@"http://example.com/fhir/StructureDefinition/patient-research-authorization"));

            // Slices inherited from base profile with url http://example.com/fhir/SD/patient-identifier-subslice
            verifier.VerifyElement("Patient.identifier", null, "Patient.identifier");
            verifier.AssertSlicing(new string[] { "system" }, ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier", "mrn", "Patient.identifier:mrn");
            verifier.AssertSlicing(new string[] { "use" }, ElementDefinition.SlicingRules.Open, null);
            verifier.VerifyElement("Patient.identifier.extension", null, "Patient.identifier:mrn.extension");
            verifier.VerifyElement("Patient.identifier.extension", "mrn.issuingSite", "Patient.identifier:mrn.extension:issuingSite");
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
            Assert.AreEqual(FHIRDefinedType.Extension, extensionType.Code);
            Assert.IsNotNull(extensionType.Profile);
            var extDefUrl = extensionType.Profile.FirstOrDefault();
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/MyLocationExtension", extDefUrl);
            var ext = _testResolver.FindStructureDefinition(extDefUrl);
            Assert.IsNotNull(ext);
            Assert.IsNull(ext.Snapshot);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
        }

        [TestMethod]
        // [Ignore]
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

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

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

        static void assertIssue(OperationOutcome.IssueComponent issue, Issue expected, string diagnostics = null)
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
        }

        // [WMR 20160721] Following profiles are not yet handled (TODO)
        //      private readonly string[] skippedProfiles =
        //      {
        //	// Differential defines constraint on MedicationOrder.reason[x]
        //	// Snapshot renames this element to MedicationOrder.reasonCodeableConcept - is this mandatory?
        //	// @"http://hl7.org/fhir/StructureDefinition/gao-medicationorder",
        //};

        [TestMethod]
        [Ignore]
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
            StructureDefinition expanded;
            return generateSnapshotAndCompare(original, out expanded);
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
        }

        // Unit tests for DifferentialTreeConstructor

        [TestMethod]
        public void TestDifferentialTree()
        {
            var e = new List<ElementDefinition>();

            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C1", Name = "C1-A" }); // First slice of A.B.C1
            e.Add(new ElementDefinition() { Path = "A.B.C2" });
            e.Add(new ElementDefinition() { Path = "A.B", Name = "B-A" }); // First slice of A.B
            e.Add(new ElementDefinition() { Path = "A.B.C1.D" });
            e.Add(new ElementDefinition() { Path = "A.D.F" });

            var tree = (new DifferentialTreeConstructor()).MakeTree(e);
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
                var tree = (new DifferentialTreeConstructor()).MakeTree(elements);
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
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "A" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.use" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "B/1" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.type" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "B/2" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.period.start" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "C/1" });

            var tree = (new DifferentialTreeConstructor()).MakeTree(elements);
            Assert.IsNotNull(tree);
            Debug.Print(string.Join(Environment.NewLine, tree.Select(e => $"{e.Path} : '{e.Name}'")));

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

        [TestMethod]
        [Ignore]
        public void DebugDifferentialTree()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/SD/patient-research-auth-reslice");
            Assert.IsNotNull(sd);
            var tree = (new DifferentialTreeConstructor()).MakeTree(sd.Differential.Element);
            Assert.IsNotNull(tree);
            Debug.Print(string.Join(Environment.NewLine, tree.Select(e => $"{e.Path} : '{e.Name}'")));
        }

        // [WMR 20160802] Unit tests for SnapshotGenerator.ExpandElement

        // [WMR 20161005] internal expandElement method is no longer unit-testable; uninitialized recursion stack causes exceptions

        //[TestMethod]
        //public void TestExpandChild()
        //{
        //    var sd = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Questionnaire);
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
                    if (elem.NameReference != null)
                    {
                        // Name reference (not a slice)
                        Assert.IsTrue(nav.JumpToNameReference(elem.NameReference));
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

                    if (!isExpandableElement(elem))
                    {
                        Assert.IsFalse(nav.MoveToFirstChild());
                        return;
                    }

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
                        // Debug.Assert(typeNav.MoveToNext());
                        // [WMR 20170412] Backbone elements can introduce additional child elements
                        if (!typeNav.MoveToNext())
                        {
                            Assert.AreEqual(FHIRDefinedType.BackboneElement, elemTypeCode);
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

        static string formatElementPathName(ElementDefinition elem)
        {
            if (elem == null) { return null; }
            if (!string.IsNullOrEmpty(elem.Name)) return $"{elem.Path}:{elem.Name}";
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
                Debug.WriteLine("Base = '{0}'".FormatWith(sd.Base));
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

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, out expanded);

                dumpOutcome(_generator.Outcome);

                assertBaseDefs(expanded, settings);

                if (sd.Url != ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Element))
                {
                    // Element snapshot should be recursively expanded, as it is the fundamental base profile
                    var sdElem = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Element);
                    Assert.IsNotNull(sdElem);
                    Assert.IsTrue(sdElem.HasSnapshot);
                    Assert.IsTrue(sdElem.Snapshot.IsCreatedBySnapshotGenerator());
                    assertBaseDefs(sdElem, settings);
                }

                if (sd.Url != ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Id))
                {
                    // Id snapshot should not be (re-)generated, as derived profiles don't force expansion
                    var sdId = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Id);
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
            var typeProfileUrl = elem.Type.FirstOrDefault().Profile.FirstOrDefault();
            Assert.IsNotNull(typeProfileUrl);
            Assert.AreEqual(typeProfileUrl, ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Identifier));

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, out expanded);
                dumpOutcome(_generator.Outcome);
                Assert.IsTrue(expanded.HasSnapshot);
                Assert.IsTrue(expanded.Snapshot.IsCreatedBySnapshotGenerator());
                assertBaseDefs(expanded, settings);

                // Verify that the snapshot generator also expanded the referenced core Identifier type profile
                var sdType = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Identifier);
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
                // hasConstraints and hasChanges methods aren't smart enough to detect redundant constraints
                var hasConstraints = SnapshotGeneratorTest.hasConstraints(elem, baseElem);
                Assert.IsTrue(hasConstraints);
                Assert.IsTrue(hasChanges(elem));

                // Verify base annotations on Patient.identifier subtree
                var elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier.")).ToList();
                for (int i = 0; i < elems.Count; i++)
                {
                    elem = elems[i];
                    Assert.IsNotNull(elem);
                    baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                    Assert.IsNotNull(baseElem);
                    hasConstraints = SnapshotGeneratorTest.hasConstraints(elem, baseElem);
                    // Only the .use child element has a profile diff constraint
                    bool isConstrained = elem.Path == "Patient.identifier.use";
                    Assert.AreEqual(isConstrained, hasConstraints);
                    Assert.AreEqual(isConstrained, hasChanges(elem));

                    // Verify that base element annotations reference the associated child element in Core Identifier profile
                    Assert.AreEqual("Patient." + baseElem.Path.Uncapitalize(), elem.Path);
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
            var typeProfileUrl = elem.Type.FirstOrDefault().Profile.FirstOrDefault();
            Assert.IsNotNull(typeProfileUrl);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, out expanded);
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
                var hasConstraints = SnapshotGeneratorTest.hasConstraints(elem, baseElem);
                Assert.IsTrue(hasConstraints);

                // Verify base annotations on Patient.identifier subtree
                var elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Patient.identifier.")).ToList();
                for (int i = 0; i < elems.Count; i++)
                {
                    elem = elems[i];
                    Assert.IsNotNull(elem);
                    baseElem = elem.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
                    Assert.IsNotNull(baseElem);
                    hasConstraints = SnapshotGeneratorTest.hasConstraints(elem, baseElem);
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
            var extensionDefinitionUrl = elem.Type.FirstOrDefault().Profile.FirstOrDefault();
            Assert.IsNotNull(extensionDefinitionUrl);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.GenerateAnnotationsOnConstraints = true;
            _generator = new SnapshotGenerator(source, settings);

            try
            {
                _generator.PrepareBaseProfile += profileHandler;
                _generator.PrepareElement += elementHandler;
                _generator.Constraint += constraintHandler;

                StructureDefinition expanded;
                generateSnapshotAndCompare(sd, out expanded);
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

                Assert.AreEqual("extension", elem.Name);
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
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
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

            Debug.WriteLine("Core Observation:");
            var obs = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation);
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
            Debug.WriteLine("[SnapshotBaseProfileHandler] Profile #{0} '{1}' Base = '{2}'".FormatWith(profile.GetHashCode(), profile.Url, profile.Base));
            Debug.Print("[SnapshotBaseProfileHandler] Base Profile #{0} '{1}'".FormatWith(baseProfile.GetHashCode(), baseProfile.Url));
            var rootElem = baseProfile.Snapshot.Element[0];
            Debug.Print("[SnapshotBaseProfileHandler] Base Root element #{0} '{1}'".FormatWith(rootElem.GetHashCode(), rootElem.Path));
            Assert.AreEqual(profile.Base, baseProfile.Url);
        }

        void elementHandler(object sender, SnapshotElementEventArgs e)
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

            Debug.Write($"[{nameof(SnapshotGeneratorTest)}.{nameof(elementHandler)}] #{elem.GetHashCode()} '{elem.Path}:{elem.Name}' - Base: #{baseDef?.GetHashCode() ?? 0} '{(baseDef?.Path)}' - Base Structure '{baseStruct?.Url}'");
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

            var isConstraint = sd.ConstrainedType.HasValue;

            Debug.Print("\r\nStructureDefinition '{0}' url = '{1}'", sd.Name, sd.Url);
            Debug.Print("# | Constraints? | Changed? | Element.Path | Element.Base.Path | BaseElement.Path | #Base | Invalid?");
            Debug.Print(new string('=', 100));
            foreach (var elem in elems)
            {
                // Each element should have a valid Base component, unless the profile is a core type/resource definition (no base)
                // Assert.IsTrue(!isConstraint || elem.Base != null);

                var ann = elem.Annotation<BaseDefAnnotation>();
                var baseDef = ann != null ? ann.BaseElementDefinition : null;
                Assert.AreNotEqual(elem, baseDef);

                var hasChanges = SnapshotGeneratorTest.hasChanges(elem);
                var hasConstraints = false;
                if (baseDef != null) // && elem.Base != null)
                {
                    // If normalizing, then elem.Base.Path refers to the defining profile (e.g. DomainResource),
                    // whereas baseDef refers to the immediate base profile (e.g. Patient)
                    Debug.Assert(elem.Base == null || ElementDefinitionNavigator.IsCandidateBasePath(elem.Base.Path, baseDef.Path));
                    hasConstraints = SnapshotGeneratorTest.hasConstraints(elem, baseDef);
                }
                var isValid = hasChanges == hasConstraints;
                bool? hasConstraintAnnotations = null;
                if (settings.GenerateAnnotationsOnConstraints)
                {
                    hasConstraintAnnotations = elem.HasDiffConstraintAnnotations();
                    isValid &= hasConstraints == hasConstraintAnnotations;
                }

                Debug.WriteLine("{0,10}  |  {1}  |  {2,-12}  |  {3,-50}  |  {4,-40}  |  {5,-40}  |  {6,10}  |  {7}",
                    elem.GetHashCode(),
                    (hasConstraints ? "+" : "-")
                    + (hasConstraintAnnotations.HasValue ? (hasConstraintAnnotations.Value ? " (+)" : " (-)") : null),
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
        static bool hasConstraints(ElementDefinition elem, ElementDefinition baseElem)
        {
            var elemClone = (ElementDefinition)elem.DeepCopy();
            var baseClone = (ElementDefinition)baseElem.DeepCopy();

            // Id, Path & Base are expected to differ
            baseClone.ElementId = elem.ElementId;
            baseClone.Path = elem.Path;
            baseClone.Base = elem.Base;

            // Also ignore any Changed extensions on base and diff
            elemClone.RemoveAllConstrainedByDiffExtensions();
            baseClone.RemoveAllConstrainedByDiffExtensions();
            elemClone.RemoveAllConstrainedByDiffAnnotations();
            baseClone.RemoveAllConstrainedByDiffAnnotations();

            var result = !baseClone.IsExactly(elemClone);
            return result;
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
        [Ignore]
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

            StructureDefinition expanded;
            var result = generateSnapshotAndCompare(sd, out expanded);

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
                    coreProfileInfo = coreDefs.Select(sd => new ProfileInfo() { Url = sd.Url, Base = sd.Base }).ToArray();
                }
                expandStructuresBasedOn(resolver, coreProfileInfo, null);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
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

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

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
                Debug.Print($"{nav.Path} : '{nav.Current.Name}'");
            } while (nav.MoveToNext("telecom"));
            nav.ReturnToBookmark(bm);

            // Patient.telecom - slicing introduction
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsNotNull(nav.Current.Slicing);

            // Patient.telecom - slice "phone"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "phone");

            // Patient.telecom - slice "email"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "email");

            // Patient.telecom - reslice "email/home"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "email/home");

            // Patient.telecom - reslice "email/work"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "email/work");

            // Patient.telecom - slice "other"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "other");

            // Patient.telecom - reslice "other/home"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "other/home");

            // Patient.telecom - reslice "other/work"
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.Path == "Patient.telecom");
            Assert.IsTrue(nav.Current.Name == "other/work");
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
        [Ignore]
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
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.value[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "@type" },
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        }
                    }
                    ,new ElementDefinition("Observation.value[x]")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void TestTypeSlicing()
        {
            // Create a profile with a type slice: { value[x], value[x] : String }
            var profile = ObservationTypeSliceProfile;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
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
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRDefinedType.String);

            // Add an additional type slice: { value[x], value[x] : String, value[x] : CodeableConcept }
            profile.Differential.Element.Add(
                new ElementDefinition("Observation.value[x]")
                {
                    Type = new List<ElementDefinition.TypeRefComponent>()
                    {
                        new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.CodeableConcept }
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
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRDefinedType.String);
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueCodeableConcept
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRDefinedType.CodeableConcept);
        }

        [TestMethod]
        public void TestMissingDifferential()
        {
            // Create a profile without a differential
            var profile = ObservationTypeSliceProfile;
            profile.Differential = null;

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Dump();
        }

        [TestMethod]
        public void TestUnresolvedBaseProfile()
        {
            // Create a profile with an unresolved base profile reference
            var profile = ObservationTypeSliceProfile;
            profile.Base = "http://example.org/fhir/StructureDefinition/missing";

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsFalse(expanded.HasSnapshot);
            var outcome = _generator.Outcome;
            Assert.IsNotNull(outcome);
            Assert.IsNotNull(outcome.Issue);
            Assert.AreEqual(outcome.Issue.Count, 1);
            assertIssue(outcome.Issue[0], Issue.UNAVAILABLE_REFERENCED_PROFILE, profile.Base);
        }

        static StructureDefinition ObservationTypeResliceProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ObservationTypeSliceProfile.Url,
            Name = "MyDerivedTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyDerivedTestObservation",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.value[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "@type" },
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
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                        }
                    }
                    
                    // Remove existing type slice value[x]: CodeableConcept

                    // Add a new type slice value[x]: Integer
                    ,new ElementDefinition("Observation.value[x]")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
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
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("[1] Observation.value reslice:");

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueString
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRDefinedType.String);
            // Derived profile REMOVES existing CodeableConcept type slice and introduces a new Integer type slice
            // Note: special rules for element types allow removal of inherited collection items
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual(nav.PathName, "value[x]"); // valueCodeableConcept
            Assert.AreEqual(nav.Current.Type.FirstOrDefault()?.Code, FHIRDefinedType.Integer);
        }

        // Choice type constraint, with element renaming
        static StructureDefinition ObservationTypeConstraintProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
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
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
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
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump("Observation.value choice type constraint:");

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsFalse(nav.MoveToChild("value[x]")); // Should also be renamed to valueString in snapshot
            Assert.IsTrue(nav.MoveToChild("valueString"));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRDefinedType.String);
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
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Integer }
                        }
                    }
            );

            var resolver = new InMemoryProfileResolver(profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
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
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRDefinedType.String);

            Assert.IsTrue(nav.MoveToNext("valueInteger"));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual(nav.Current.Type.FirstOrDefault().Code, FHIRDefinedType.Integer);

            Assert.IsNotNull(outcome);
            Assert.AreEqual(1, outcome.Issue.Count);
            assertIssue(outcome.Issue[0], SnapshotGenerator.PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT);
        }

        static StructureDefinition ClosedExtensionSliceObservationProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "MyTestObservation",
            Url = "http://example.org/fhir/StructureDefinition/MyTestObservation",
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
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(profile, out expanded);
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

        [TestMethod()]
        public void TestSlicingEntryWithChilren()
        {
            var sd = _testResolver.FindStructureDefinition(@"http://example.org/StructureDefinition/DocumentComposition");
            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

            dumpOutcome(_generator.Outcome);
            expanded.Snapshot.Element.Dump();

            // Verify that the snapshot includes the merged children of the slice entry element
            var verifier = new ElementVerifier(expanded, _settings);
            verifier.VerifyElement("Composition.section", null);
            verifier.AssertSlicing(new string[] { "code" }, ElementDefinition.SlicingRules.Open, false);
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
            // [WMR 20180115] Obsolete - full expansion via BeforeExpandElement event is flawed...
            //if (expandAll)
            //{
            //    _generator.BeforeExpandElement += beforeExpandElementHandler;
            //}
            try
            {
                generateSnapshotAndCompare(obs, out expanded);
            }
            finally
            {
                _generator.PrepareElement -= elementHandler;
                //if (expandAll)
                //{
                //    _generator.BeforeExpandElement -= beforeExpandElementHandler;
                //}
            }

            dumpOutcome(_generator.Outcome);

            var elems = expanded.Snapshot.Element;
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

            // Verify that the snapshot contains three extension elements 
            var obsExtensions = elems.Where(e => e.Path == "Observation.extension").ToList();
            Assert.IsNotNull(obsExtensions);
            Assert.AreEqual(4, obsExtensions.Count); // 1 extension slice + 3 extensions

            var extSliceElem = obsExtensions[0];
            Assert.IsNotNull(extSliceElem);
            Assert.IsNotNull(extSliceElem.Slicing);
            Assert.AreEqual("url", extSliceElem.Slicing.Discriminator.FirstOrDefault());

            var labelExtElem = obsExtensions[1];
            Assert.IsNotNull(labelExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/ObservationLabelExtension", labelExtElem.Type.FirstOrDefault().Profile.FirstOrDefault());

            var locationExtElem = obsExtensions[2];
            Assert.IsNotNull(locationExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/ObservationLocationExtension", locationExtElem.Type.FirstOrDefault().Profile.FirstOrDefault());

            var otherExtElem = obsExtensions[3];
            Assert.IsNotNull(otherExtElem);
            Assert.AreEqual(@"http://example.org/fhir/StructureDefinition/SomeOtherExtension", otherExtElem.Type.FirstOrDefault().Profile.FirstOrDefault());

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
            var coreExtension = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension);
            Assert.IsNotNull(coreExtension);
            Assert.IsTrue(coreExtension.HasSnapshot);
            var coreExtensionRootElem = coreExtension.Snapshot.Element[0];

            var labelExtRootElem = labelExt.Differential.Element[0];
            Assert.AreEqual(1, labelExtElem.Min);                                           // Explicit Observation profile constraint
            Assert.AreEqual(labelExtRootElem.Max, labelExtElem.Max);                        // Inherited from external ObservationLabelExtension root element
            Assert.AreEqual(coreExtensionRootElem.Definition, labelExtElem.Definition);     // Inherited from Observation.extension base element
            Assert.AreEqual(labelExtRootElem.Comments, labelExtElem.Comments);              // Inherited from external ObservationLabelExtension root element
            verifyProfileExtensionBaseElement(labelExtElem);

            var locationExtRootElem = locationExt.Differential.Element[0];
            Assert.AreEqual(0, locationExtElem.Min);                                        // Inherited from external ObservationLabelExtension root element
            Assert.AreEqual("1", locationExtElem.Max);                                      // Explicit Observation profile constraint
            Assert.AreEqual(coreExtensionRootElem.Definition, locationExtElem.Definition);  // Inherited from Observation.extension base element
            Assert.AreEqual(locationExtRootElem.Comments, locationExtElem.Comments);        // Inherited from external ObservationLocationExtension root element
            verifyProfileExtensionBaseElement(locationExtElem);

            // Last (unresolved) extension element should have been merged with Observation.extension
            var coreObservation = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation);
            Assert.IsNotNull(coreObservation);
            Assert.IsTrue(coreObservation.HasSnapshot);
            var coreObsExtensionElem = coreObservation.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.extension");
            Assert.IsNotNull(coreObsExtensionElem);
            Assert.AreEqual(1, otherExtElem.Min);                                           // Explicit Observation profile constraint
            Assert.AreEqual(coreObsExtensionElem.Max, otherExtElem.Max);                    // Inherited from Observation.extension base element
            Assert.AreEqual(coreObsExtensionElem.Definition, otherExtElem.Definition);      // Inherited from Observation.extension base element
            Assert.AreEqual(coreObsExtensionElem.Comments, otherExtElem.Comments);          // Inherited from Observation.extension base element
            verifyProfileExtensionBaseElement(coreObsExtensionElem);
        }

        void verifyProfileExtensionBaseElement(ElementDefinition extElem)
        {
            var baseElem = extElem.Annotation<BaseDefAnnotation>().BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(baseElem.Short, extElem.Short);
            Assert.AreEqual(baseElem.Definition, extElem.Definition);
            Assert.AreEqual(baseElem.Comments, extElem.Comments);
            Assert.IsTrue(baseElem.Alias.SequenceEqual(extElem.Alias));
        }

        // [WMR 20170213] New - issue reported by Marten - cannot slice Organization.type ?
        // Specifically, snapshot generator drops the slicing component from the slice entry element
        // Explanation: Organization.type is not a list (max = 1) and not a choice type => slicing is not allowed!
        [TestMethod]
        [Ignore]
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

            dumpOutcome(_generator.Outcome);

            var elems = expanded.Snapshot.Element;
            elems.Dump();
            //dumpBaseElems(elems);

            // TODO: Verify slice

        }

        // [WMR 2017024] NEW: Test for bug with snapshot expansion of ElementDefinition.Binding (reported by NHS)
        // If the diff constrains only Binding.Strength, then snapshot also contains only Binding.Strength - WRONG!
        // Expected: snapshot contains inherited properties from base, i.e. description, valueSetUri/valueSetReference
        [TestMethod]
        public void TestElementBinding()
        {
            var sd = new StructureDefinition()
            {
                ConstrainedType = FHIRDefinedType.Encounter,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Encounter),
                Name = "MyTestEncounter",
                Url = "http://example.org/fhir/StructureDefinition/MyTestEncounter",
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

                            Binding = new ElementDefinition.BindingComponent()
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
            _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(sd, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            var profileElem = expanded.Snapshot.Element.FirstOrDefault(e => e.Path == "Encounter.type");
            Assert.IsNotNull(profileElem);
            var profileBinding = profileElem.Binding;
            Assert.IsNotNull(profileBinding);

            Assert.AreEqual(BindingStrength.Preferred, profileBinding.Strength);

            var sdEncounter = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Encounter);
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
                ConstrainedType = FHIRDefinedType.Location,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Location),
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
                ConstrainedType = FHIRDefinedType.Flag,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Flag),
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
                                Discriminator = new string[] { "url" },
                                Rules = ElementDefinition.SlicingRules.Open
                            }
                        },
                        new ElementDefinition("Flag.extension")
                        {
                            Name = "geopositions",
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRDefinedType.Extension,
                                    // INVALID - Map extension element to non-extension definition
                                    Profile = new string[] { sdLocation.Url }
                                }

                            }
                        }
                    }

                }
            };

            var resolver = new InMemoryProfileResolver(sdLocation, sdFlag);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver, _settings);
            // [WMR 20180115] Obsolete - full expansion via BeforeExpandElement event is flawed...
            // _generator.BeforeExpandElement += beforeExpandElementHandler;
            StructureDefinition expanded = null;
            //try
            //{
            generateSnapshotAndCompare(sdFlag, out expanded);
            //}
            //finally
            //{
            //    _generator.BeforeExpandElement -= beforeExpandElementHandler;
            //}

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

        // Verify extension constraint on choice type element w/o type slice
        [TestMethod]
        public void TestZibProcedure()
        {
            var sd = _testResolver.FindStructureDefinition("http://nictiz.nl/fhir/StructureDefinition/zib-Procedure");
            Assert.IsNotNull(sd);
            assertContainsElement(sd.Differential, "Procedure.request.extension", "RequestedBy");

            StructureDefinition expanded = null;
            generateSnapshotAndCompare(sd, out expanded);
            dumpOutcome(_generator.Outcome);

            Assert.IsTrue(expanded.HasSnapshot);
            expanded.Snapshot.Element.Dump();

            // Verify that the snapshot contains the extension on Procedure.request (w/o type slice)
            assertContainsElement(expanded.Snapshot, "Procedure.request.extension", "RequestedBy");
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
            ConstrainedType = FHIRDefinedType.Patient,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
            Name = "MySlicedPatient",
            Url = "http://example.org/fhir/StructureDefinition/MySlicedPatient",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "system" },
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1
                    }
                    ,new ElementDefinition("Patient.identifier")
                    {
                        Name = "bsn",
                        Min = 1,
                        Max = "1"
                    }
                    ,new ElementDefinition("Patient.identifier")
                    {
                        Name = "ehr_id",
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
            _generator = new SnapshotGenerator(multiResolver);
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

            var corePatientProfile = _testResolver.FindStructureDefinition(profile.Base);
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
            Assert.AreEqual(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());
            assertNamedSliceBaseElement(corePatientIdentifierElem, nav.Current);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());

            // [WMR 20170711] Disregard ElementDefinition.Base
            // Assert.AreEqual(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsTrue(isAlmostExactly(corePatientIdentifierElem, GetBaseElementAnnotation(nav.Current)));

            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
        }

        // [WMR 20170420] NEW
        // Verify the base element annotation of a named slice
        // Note: named slices receive a customized base element: Min = 0, Slicing = null
        static void assertNamedSliceBaseElement(ElementDefinition original, ElementDefinition namedSlice)
        {
            var sliceBase = GetBaseElementAnnotation(namedSlice);
            var clone = (ElementDefinition)sliceBase.DeepCopy();
            clone.Min = 0;
            clone.Slicing = null;
            clone.Base = original.Base;
            Assert.IsTrue(clone.IsExactly(original));
        }

        static StructureDefinition NationalPatientProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Patient,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
            Name = "MyNationalPatient",
            Url = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Comments = "NationalPatientProfile"
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
            ConstrainedType = FHIRDefinedType.Patient,
            Base = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Name = "SlicedNationalPatientProfile",
            Url = "http://example.org/fhir/StructureDefinition/SlicedNationalPatientProfile",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "system" },
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1,
                        // Append to comment inherited from base
                        Comments = "...SlicedNationalPatientProfile"
                    }
                    // Slice: bsn
                    ,new ElementDefinition("Patient.identifier")
                    {
                        Name = "bsn",
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
                        Name = "ehr_id",
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
            _generator = new SnapshotGenerator(multiResolver);
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

            var nationalPatientProfile = resolver.FindStructureDefinition(profile.Base);
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
            Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);
            // Slice entry should inherit Comments from base element, merged with diff constraints
            Assert.AreEqual("NationalPatientProfile\r\nSlicedNationalPatientProfile", nav.Current.Comments);
            // Slice entry should also inherit constraints on child elements from base element
            var bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            // Should be merged with diff constraints on child elements
            Assert.AreEqual((nav.Current.Fixed as FhirUri).Value, "http://example.org/fhir/ValueSet/bsn");
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

#if false
            // Verify re-slice "ehr_id/temp"
            Assert.IsTrue(nav.MoveToNextSliceAtAnyLevel());
            Assert.AreEqual(nationalPatientIdentifierElem, GetBaseElementAnnotation(nav.Current));
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id/temp", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));
#endif
        }

        static StructureDefinition ReslicedNationalPatientProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Patient,
            Base = "http://example.org/fhir/StructureDefinition/MyNationalPatient",
            Name = "ReslicedNationalPatientProfile",
            Url = "http://example.org/fhir/StructureDefinition/ReslicedNationalPatientProfile",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient.identifier")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "system" },
                            Ordered = false,
                            Rules = ElementDefinition.SlicingRules.Open
                        },
                        Min = 1,
                        // Append to comment inherited from base
                        Comments = "...SlicedNationalPatientProfile"
                    }
                    // Slice: bsn
                    ,new ElementDefinition("Patient.identifier")
                    {
                        Name = "bsn",
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
                        Name = "ehr_id",
                        Max = "2",

                        // Re-slice the ehr-id
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "use" },
                            Ordered = true,
                            Rules = ElementDefinition.SlicingRules.Closed
                        }
                    },

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
            _generator = new SnapshotGenerator(multiResolver);
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

            var nationalPatientProfile = resolver.FindStructureDefinition(profile.Base);
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
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("*", nav.Current.Max);
            // Slice entry should inherit Comments from base element, merged with diff constraints
            Assert.AreEqual("NationalPatientProfile\r\nSlicedNationalPatientProfile", nav.Current.Comments);
            // Slice entry should also inherit constraints on child elements from base element
            var bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "bsn"
            Assert.IsTrue(nav.MoveToNextSlice());
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bsn", nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            // Should be merged with diff constraints on child elements
            Assert.AreEqual((nav.Current.Fixed as FhirUri).Value, "http://example.org/fhir/ValueSet/bsn");
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify slice "ehr_id"
            Assert.IsTrue(nav.MoveToNextSlice());
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("2", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));

            // Verify re-slice "ehr_id/temp"
            Assert.IsTrue(nav.MoveToFirstReslice());
            assertNamedSliceBaseElement(nationalPatientIdentifierElem, nav.Current);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("ehr_id/temp", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should inherit Comments from base element
            Assert.AreEqual("NationalPatientProfile", nav.Current.Comments);
            // Named slices should also inherit constraints on child elements from base element
            bm = nav.Bookmark();
            Assert.IsTrue(nav.MoveToChild("system"));
            Assert.AreEqual(nav.Current.Min, 1);
            Assert.IsTrue(nav.ReturnToBookmark(bm));
        }

        // [WMR 20170420] Added
        // Slices should never inherit Minimum Cardinality from base (always zero)

        static StructureDefinition SliceBaseObservationProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "SliceBaseObservation",
            Url = "http://example.org/fhir/StructureDefinition/SliceBaseObservation",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.code.coding")
                    {
                        Min = 1
                    }
                }
            }
        };

        static StructureDefinition SlicedDerivedObservationProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = SliceBaseObservationProfile.Url,
            Name = "SlicedDerivedObservationProfile",
            Url = "http://example.org/fhir/StructureDefinition/SlicedDerivedObservationProfile",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.code.coding")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new string[] { "code" }
                        }
                        // Should inherit Min = 1 from base slice entry
                    },
                    new ElementDefinition("Observation.code.coding")
                    {
                        Name = "MainCoding",
                        Max = "1"
                        // Named slices should NOT inherit Min = 1 from base slice entry !
                    },
                    new ElementDefinition("Observation.code.coding")
                    {
                        Name = "DetaildeCoding"
                        // Named slices should NOT inherit Min = 1 from base slice entry !
                    }
                }
            }
        };

        [TestMethod]
        public void TestNamedSliceMinimumCardinality()
        {
            // Named slices should never inherit minimum cardinality from the base element (always zero)
            // See ElementMatcher.initSliceBase

            var baseProfile = SliceBaseObservationProfile;
            var profile = SlicedDerivedObservationProfile;

            var resolver = new InMemoryProfileResolver(baseProfile, profile);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            _generator = new SnapshotGenerator(multiResolver);
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

            dumpBaseElems(expanded.Snapshot.Element);

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.JumpToFirst("Observation.code.coding"));
            Assert.IsNotNull(nav.Current);
            Assert.IsNotNull(nav.Current.Slicing);
            // Slice entry should inherit Min = 1 from base
            Assert.AreEqual(1, nav.Current.Min);

            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("MainCoding", nav.Current.Name);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should NOT inherit Min = 1 from base slice entry !
            Assert.AreEqual(0, nav.Current.Min);

            // Also verify base element annotation
            var baseElemAnnotation = nav.Current.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(baseElemAnnotation);
            Assert.IsNotNull(baseElemAnnotation.BaseElementDefinition);
            Assert.AreEqual(0, baseElemAnnotation.BaseElementDefinition.Min);

            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("DetaildeCoding", nav.Current.Name);
            Assert.AreEqual("*", nav.Current.Max);
            // Named slices should NOT inherit Min = 1 from base slice entry !
            Assert.AreEqual(0, nav.Current.Min);

            // Also verify base element annotation
            baseElemAnnotation = nav.Current.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(baseElemAnnotation);
            Assert.IsNotNull(baseElemAnnotation.BaseElementDefinition);
            Assert.AreEqual(0, baseElemAnnotation.BaseElementDefinition.Min);
        }

        [TestMethod]
        public void TestNamedSliceMinimumCardinality_FIPHR()
        {
            // Same as above, but with actual profiles from Finnish PHR project

            var profile = _testResolver.FindStructureDefinition(@"http://phr.kanta.fi/StructureDefinition/fiphr-bodytemperature");

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

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.JumpToFirst("Observation.code.coding"));
            Assert.IsNotNull(nav.Current);
            Assert.IsNotNull(nav.Current.Slicing);
            // Slice entry should inherit Min = 1 from base
            Assert.AreEqual(1, nav.Current.Min);

            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("MainCoding", nav.Current.Name);
            Assert.AreEqual("1", nav.Current.Max);
            // Named slices should NOT inherit Min = 1 from base slice entry !
            Assert.AreEqual(0, nav.Current.Min);

            // Also verify base element annotation
            var baseElemAnnotation = nav.Current.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(baseElemAnnotation);
            Assert.IsNotNull(baseElemAnnotation.BaseElementDefinition);
            Assert.AreEqual(0, baseElemAnnotation.BaseElementDefinition.Min);

            Assert.IsTrue(nav.MoveToNextSlice());
            Assert.AreEqual("DetaildeCoding", nav.Current.Name);
            Assert.AreEqual("*", nav.Current.Max);
            // Named slices should NOT inherit Min = 1 from base slice entry !
            Assert.AreEqual(0, nav.Current.Min);

            // Also verify base element annotation
            baseElemAnnotation = nav.Current.Annotation<BaseDefAnnotation>();
            Assert.IsNotNull(baseElemAnnotation);
            Assert.IsNotNull(baseElemAnnotation.BaseElementDefinition);
            Assert.AreEqual(0, baseElemAnnotation.BaseElementDefinition.Min);
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
            Assert.AreEqual("phone", nav.Current.Name);
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
            Assert.AreEqual("email", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("1", nav.Current.Max);
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("system|use", string.Join("|", nav.Current.Slicing.Discriminator));
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
            Assert.AreEqual("email/home", nav.Current.Name);
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
            Assert.AreEqual("email/work", nav.Current.Name);
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
            Assert.AreEqual("other", nav.Current.Name);
            Assert.AreEqual(0, nav.Current.Min);
            Assert.AreEqual("3", nav.Current.Max);
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("system|use", string.Join("|", nav.Current.Slicing.Discriminator));
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
            Assert.AreEqual("other/home", nav.Current.Name);
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
            Assert.AreEqual("other/work", nav.Current.Name);
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
            ConstrainedType = FHIRDefinedType.Patient,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
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
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.Boolean },
                            new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.DateTime }
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
            _generator = new SnapshotGenerator(multiResolver);

            //StructureDefinition expanded = null;
            //generateSnapshotAndCompare(profile, out expanded);

            //_generator.BeforeExpandElement += beforeExpandElementHandler;
            //StructureDefinition expanded = null;
            //try
            //{
            //    generateSnapshotAndCompare(profile, out expanded);
            //}
            //finally
            //{
            //    _generator.BeforeExpandElement -= beforeExpandElementHandler;
            //}
            //Assert.IsNotNull(expanded);
            //Assert.IsTrue(expanded.HasSnapshot);
            //dumpElements(expanded.Snapshot.Element);
            //dumpOutcome(_generator.Outcome);

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
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
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

                                Code = FHIRDefinedType.SimpleQuantity
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
            _generator = new SnapshotGenerator(multiResolver);

            // [WMR 20180115] Obsolete - full expansion via BeforeExpandElement event is flawed...
            //_generator.BeforeExpandElement += beforeExpandElementHandler;
            StructureDefinition expanded = null;
            //try
            //{
            generateSnapshotAndCompare(profile, out expanded);
            //}
            //finally
            //{
            //_generator.BeforeExpandElement -= beforeExpandElementHandler;
            //}
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);
            expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value")).Dump();
            dumpOutcome(_generator.Outcome);

            // Force expansion of Observation.valueQuantity
            //var nav = ElementDefinitionNavigator.ForDifferential(profile);
            //Assert.IsTrue(nav.MoveToFirstChild());
            //var result = _generator.ExpandElement(nav);
            //dumpElements(profile.Differential.Element);
            //dumpOutcome(_generator.Outcome);
            //Assert.IsTrue(result);
            Assert.IsNull(_generator.Outcome);

            // [WMR 20180115] NEW - Use alternative (iterative) approach for full expansion
            var issues = _generator.Outcome?.Issue ?? new List<OperationOutcome.IssueComponent>();
            var elems = expanded.Snapshot.Element;
            elems = expanded.Snapshot.Element = fullyExpand(elems, issues).ToList();
            Assert.AreEqual(0, issues.Count);

            // Ensure that renamed diff elements override base elements with original names
            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            // Snapshot should not contain elements with original name
            Assert.IsFalse(nav.JumpToFirst("Observation.value[x]"));
            // Snapshot should contain renamed elements
            Assert.IsTrue(nav.JumpToFirst("Observation.valueQuantity"));
            Assert.IsNotNull(nav.Current.Type);
            Assert.AreEqual(1, nav.Current.Type.Count);
            // Assert.AreEqual(FHIRDefinedType.SimpleQuantity, nav.Current.Type[0].Code);
            // Assert.AreEqual(FHIRDefinedType.Quantity, nav.Current.Type[0].Code);

            var type = nav.Current.Type.First();
            Debug.Print($"{nav.Path} : {type.Code} - '{type.Profile.FirstOrDefault()}'");
        }

        // [WMR 20170406] NEW
        // Issue reported by Vadim
        // Complex extension:   structure.cdstools-typedstage
        // Referencing Profile: structure.cdstools-basecancer
        // Profile defines constraints on child elements of the complex extension
        // Snapshot generator adds slicing component to Condition.extension.extension.extension:type - WRONG!
        [TestMethod]
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
            elems.Take(55).Dump("===== Snapshot =====");

            var nav = new ElementDefinitionNavigator(elems);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("Condition", nav.Path);

            // Condition.extension (slicing entry)
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("url", nav.Current.Slicing.Discriminator?.FirstOrDefault());
            Assert.IsNull(nav.Current.Name);

            // Condition.extension:typedStaging
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("typedStaging", nav.Current.Name);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension (slicing entry)
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("url", nav.Current.Slicing.Discriminator?.FirstOrDefault());
            Assert.IsNull(nav.Current.Name);

            // Condition.extension:typedStaging.extension:stage
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("stage", nav.Current.Name);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension:stage.extension (slicing entry)
            Assert.IsTrue(nav.MoveToChild("extension"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.AreEqual("url", nav.Current.Slicing.Discriminator?.FirstOrDefault());
            Assert.IsNull(nav.Current.Name);

            // Condition.extension:typedStaging.extension:stage.extension:summary
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("summary", nav.Current.Name);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension:stage.extension:assessment
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("assessment", nav.Current.Name);
            Assert.IsNull(nav.Current.Slicing);

            // Condition.extension:typedStaging.extension:stage.extension:type
            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("type", nav.Current.Name);
            Assert.IsNull(nav.Current.Slicing); // BUG!

            // Condition.extension:typedStaging.extension:stage.extension:type.valueCodeableConcept
            Assert.IsTrue(nav.MoveToChild("valueCodeableConcept"));
            Assert.IsNotNull(nav.Current.Binding);
            Assert.AreEqual(BindingStrength.Required, nav.Current.Binding.Strength);
            var valueSetReference = nav.Current.Binding.ValueSet as ResourceReference;
            Assert.IsNotNull(valueSetReference);
            Assert.AreEqual("https://example.org/fhir/ValueSet/cds-cancerstagingtype", valueSetReference.Reference);

            // [WMR 20170410] Also verify the generated base element reference
            var baseElem = nav.Current.Annotation<BaseDefAnnotation>()?.BaseElementDefinition;
            Assert.IsNotNull(baseElem);
            Assert.AreEqual(baseElem.Path, nav.Current.Path);
            // ElementDefinition.Binding.Strength is constrained
            // ElementDefinition.Binding.ValueSet is inherited
            Assert.IsNotNull(baseElem.Binding);
            Assert.AreEqual(BindingStrength.Preferred, baseElem.Binding.Strength);
            var baseValueSetReference = baseElem.Binding.ValueSet as ResourceReference;
            Assert.IsTrue(valueSetReference.IsExactly(baseValueSetReference));
        }

        const string PatientIdentifierProfileUri = @"http://example.org/fhir/StructureDefinition/PatientIdentifierProfile";
        const string PatientProfileWithIdentifierProfileUri = @"http://example.org/fhir/StructureDefinition/PatientProfileWithIdentifierProfile";
        const string PatientIdentifierTypeValueSetUri = @"http://example.org/fhir/ValueSet/PatientIdentifierTypeValueSet";

        // Identifier profile with valueset binding on child element Identifier.type
        static StructureDefinition PatientIdentifierProfile => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Identifier,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Identifier),
            Name = "PatientIdentifierProfile",
            Url = PatientIdentifierProfileUri,
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Datatype,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Identifier") { } ,
                    new ElementDefinition("Identifier.type")
                    {
                        Min = 1,
                        Binding = new ElementDefinition.BindingComponent()
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
            ConstrainedType = FHIRDefinedType.Patient,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
            Name = "PatientProfileWithIdentifierProfile",
            Url = PatientProfileWithIdentifierProfileUri,
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Patient") { },
                    new ElementDefinition("Patient.identifier")
                    {
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRDefinedType.Identifier,
                                Profile = new string[] { PatientIdentifierProfileUri }
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
            //_generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            try
            {
                generateSnapshotAndCompare(patientProfile, out expanded);
            }
            finally
            {
               // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
           // dumpElements(expanded.Snapshot.Element);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Patient.identifier"));
            Assert.IsNotNull(nav.Current);

            // BUG: binding constraint on Identifier.type is merged onto Patient.identifier...? (parent element!)
            // FIXED [SnapshotGenerator.getSnapshotRootElement] var diffRoot = sd.Differential.GetRootElement();
            Assert.IsNull(nav.Current.Binding);

            // By default, Patient.identifier.type should NOT be included in the generated snapshot
            Assert.IsFalse(nav.MoveToChild("type"));
        }

#if false       // STU3 specific test
        static StructureDefinition QuestionnaireResponseWithSlice => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.QuestionnaireResponse,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.QuestionnaireResponse),
            Name = "QuestionnaireResponseWithSlice",
            Url = @"http://example.org/fhir/StructureDefinition/QuestionnaireResponseWithSlice",
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("QuestionnaireResponse.group.question")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new List<string>() { "text" }
                        }
                    },
                    new ElementDefinition("QuestionnaireResponse.group.question")
                    {
                        Name = "Q1"
                    },
                    new ElementDefinition("QuestionnaireResponse.group.question")
                    {
                        Name = "Q2"
                    },
                    new ElementDefinition("QuestionnaireResponse.group.question.linkid")
                    {
                        Max = "0"
                    },
                }
            }
        };

        // Isue #387
        // https://github.com/ewoutkramer/fhir-net-api/issues/387
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
            // _generator.BeforeExpandElement += beforeExpandElementHandler_DEBUG;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                // _generator.BeforeExpandElement -= beforeExpandElementHandler_DEBUG;
            }

            dumpOutcome(_generator.Outcome);
            Assert.IsTrue(expanded.HasSnapshot);
            // dumpElements(expanded.Snapshot.Element);

            // Verify the inherited example binding on QuestionnaireResponse.item.answer.value[x]
            var answerValues = expanded.Snapshot.Element.Where(e => e.Path == "QuestionnaireResponse.group.group.question.answer.value[x]").ToList();
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
#endif

        // For derived profiles, base element annotations are incorrect
        // https://trello.com/c/8h7u2qRa
        // Three layers of derived profiles: MyVitalSigns => VitalSigns => Observation
        // When expanding MyVitalSigns, the annotated base elements also include local diff constraints... WRONG!
        // As a result, Forge will not detect the existing local constraints (no yellow pen, excluded from output).
        const string MyDerivedObservationUrl = @"http://example.org/fhir/StructureDefinition/MyDerivedObservation";
        const string MyMoreDerivedObservationUrl = @"http://example.org/fhir/StructureDefinition/MyMoreDerivedObservation";

        static StructureDefinition MyDerivedObservation => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Observation),
            Name = "MyDerivedObservation",
            Url = MyDerivedObservationUrl,
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
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

            var coreObs = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation);
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
            ConstrainedType = FHIRDefinedType.Observation,
            Base = MyDerivedObservationUrl,
            Name = "MyMoreDerivedObservation",
            Url = MyMoreDerivedObservationUrl,
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Observation.method")
                    {
                        Short = "MoreDerivedMethodShort",
                        Comments = "MoreDerivedMethodComment"
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
            var coreObs = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation);
            Assert.IsTrue(coreObs.HasSnapshot);
            var coreMethodElem = coreObs.Snapshot.Element.FirstOrDefault(e => e.Path == "Observation.method");
            Assert.IsNotNull(coreMethodElem);
            Assert.IsNotNull(coreMethodElem.Comments);
            Assert.AreEqual(coreMethodElem.Comments, baseElem.Comments);
        }

        // [WMR 20170718] Test for slicing issue
        static StructureDefinition MySlicedDocumentReference => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Observation,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.DocumentReference),
            Name = "MySlicedDocumentReference",
            Url = "http://example.org/fhir/StructureDefinition/MySlicedDocumentReference",
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
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
                        Name = "meta",
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

            var coreProfile = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.DocumentReference);
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
            var diffSlice = sd.Differential.Element.FirstOrDefault(e => e.Name != null);
            Assert.IsNotNull(diffSlice);
            var snapSlice = elems.FirstOrDefault(e => e.Name == diffSlice.Name);
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
            ConstrainedType = FHIRDefinedType.Patient,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
            Name = "MySlicedBasePatient",
            Url = @"http://example.org/fhir/StructureDefinition/MySlicedBasePatient",
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
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
                        Name = "bsn"
                    }
                }
            }
        };

        static StructureDefinition MyMoreDerivedPatient => new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Patient,
            Base = MySlicedBasePatient.Url,
            Name = "MyMoreDerivedPatient",
            Url = @"http://example.org/fhir/StructureDefinition/MyMoreDerivedPatient",
            //Derivation = StructureDefinition.TypeDerivationRule.Constraint,
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
            // dumpElements(expanded.Snapshot.Element);
        }

        [TestMethod]
        public void TestExpandBundleEntryResource()
        {
            // Verify that the snapshot generator is capable of expanding Bundle.entry.resource,
            // if constrained to a resource type

            var sd = new StructureDefinition()
            {
                ConstrainedType = FHIRDefinedType.Bundle,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Bundle),
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
                                    Code = FHIRDefinedType.List
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

            var sdBundle = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Bundle);
            Assert.IsNotNull(sdBundle);
            _generator.Update(sdBundle);
            Assert.IsTrue(sdBundle.HasSnapshot);

            var sdList = _testResolver.FindStructureDefinitionForCoreType(FHIRDefinedType.List);
            Assert.IsNotNull(sdList);
            _generator.Update(sdList);
            Assert.IsTrue(sdList.HasSnapshot);

            Debug.Print("===== Generate ===== ");
            // Generate custom snapshot for Bundle profile

            // Warning: beforeExpandElementHandler expands *all* elements with complex types
            // => First generate regular snapshots for core profiles, before hooking the event

            // [WMR 20180115] Maybe beforeExpandElementHandler can detect recursive calls and bail out?

            StructureDefinition expanded = null;
            // [WMR 20180115] Obsolete - full expansion via BeforeExpandElement event is flawed...
            //_generator.BeforeExpandElement += beforeExpandElementHandler;
            _generator.PrepareElement += elementHandler;
            try
            {
                generateSnapshotAndCompare(sd, out expanded);
            }
            finally
            {
                //_generator.BeforeExpandElement -= beforeExpandElementHandler;
                _generator.PrepareElement -= elementHandler;
            }

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
            Assert.AreEqual(FHIRDefinedType.List, elem.Type.FirstOrDefault()?.Code);

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

        [TestMethod]
        [Ignore]
        public void DumpTypes()
        {
            Debug.WriteLine($"{"Type", -30} {"Resource",-10} {"DataType", -10} {"Primitive", -10} {"!Primitive",-10} {"Complex", -10}");
            foreach (FHIRDefinedType type in Enum.GetValues(typeof(FHIRDefinedType)))
            {
                Debug.WriteLine($"{type, -30} {ModelInfo.IsKnownResource(type),-10} {ModelInfo.IsDataType(type), -10} {ModelInfo.IsPrimitive(type), -10} {!ModelInfo.IsPrimitive(type),-10} {isComplexDataTypeOrResource(type)}");
            }
        }

        // [WMR 20180115]
        // https://github.com/ewoutkramer/fhir-net-api/issues/510
        // "Missing diff annotation on ElementDefinition.TypeRefComponent"
        [TestMethod]
        public void TestConstrainedByDiff_Type()
        {
            StructureDefinition sd = new StructureDefinition()
            {
                ConstrainedType = FHIRDefinedType.Patient,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
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
                                    Profile = new [] { "http://fhir.nl/fhir/StructureDefinition/nl-core-humanname" }
                                }
                            }
                        },
                        new ElementDefinition("Patient.careProvider")
                        // new ElementDefinition("Patient.generalPractitioner") // DSTU3
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    // DSTU3: TargetProfile
                                    Profile = new []
                                    {
                                        "http://fhir.nl/fhir/StructureDefinition/nl-core-organization",
                                        "http://fhir.nl/fhir/StructureDefinition/nl-core-practitioner"
                                    }
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

            // Assert.IsTrue(nav.JumpToFirst("Patient.generalPractitioner"));
            Assert.IsTrue(nav.JumpToFirst("Patient.careProvider"));
            Assert.IsTrue(hasChanges(nav.Current));
            Assert.IsFalse(isChanged(nav.Current)); 
            Assert.IsTrue(hasChanges(nav.Current.Type));
            foreach (var type in nav.Current.Type)
            {
                Assert.IsTrue(isChanged(type));
            }
        }

        // [WMR 20180410] Add unit tests for content references

        public StructureDefinition QuestionnaireWithNestedItems = new StructureDefinition()
        {
            ConstrainedType = FHIRDefinedType.Questionnaire,
            Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Questionnaire),
            Name = "QuestionnaireWithNestedItems",
            Url = "http://example.org/fhir/StructureDefinition/QuestionnaireWithNestedItems",
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("Questionnaire.group.title")
                    {
                        Short = "level 1"
                    },
                    new ElementDefinition("Questionnaire.group.group.title")
                    {
                        Comments = "level 2"
                    }
                }
            }
        };

        [TestMethod]
        public void TestNameReferenceQuestionnaire()
        {
            var sd = QuestionnaireWithNestedItems;

            generateSnapshotAndCompare(sd, out StructureDefinition expanded);

            dumpOutcome(_generator.Outcome);
            dumpBaseElems(expanded.Snapshot.Element);

            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Assert.IsNull(_generator.Outcome);

            var nav = ElementDefinitionNavigator.ForSnapshot(expanded);
            Assert.IsTrue(nav.JumpToFirst("Questionnaire.group.title"));
            Assert.AreEqual("level 1" ,nav.Current.Short);

            Assert.IsTrue(nav.JumpToFirst("Questionnaire.group.group.title"));
            Assert.AreEqual("level 2", nav.Current.Comments);
            // Level 2 should NOT inherit constraints from level 1
            Assert.AreNotEqual("level 1", nav.Current.Short);
        }

        [TestMethod]
        public void TestNameReferenceQuestionnaireDerived()
        {
            var sd = new StructureDefinition
            {
                ConstrainedType = FHIRDefinedType.Questionnaire,
                Base = QuestionnaireWithNestedItems.Url,
                Name = "QuestionnaireWithNestedItemsDerived",
                Url = "http://example.org/fhir/StructureDefinition/QuestionnaireWithNestedItemsDerived",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Questionnaire.group.title")
                        {
                            Comments = "level 1 *"
                        },
                        new ElementDefinition("Questionnaire.group.group.title")
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
            Assert.IsTrue(nav.JumpToFirst("Questionnaire.group.title"));
            Assert.AreEqual("level 1", nav.Current.Short);
            Assert.AreEqual("level 1 *", nav.Current.Comments);

            Assert.IsTrue(nav.JumpToFirst("Questionnaire.group.group.title"));
            Assert.AreEqual("level 2", nav.Current.Comments);
            Assert.AreEqual("level 2 *", nav.Current.Short);
        }

        // [WMR 20180604] Issue #611
        // https://github.com/ewoutkramer/fhir-net-api/issues/611

        [TestMethod]
        public void TestSnapshotForDerivedSlice()
        {
            var sdBase = new StructureDefinition
            {
                ConstrainedType = FHIRDefinedType.Patient,
                Base = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient),
                Name = "BasePatient",
                Url = "http://example.org/fhir/StructureDefinition/BasePatient",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.identifier")
                        {
                            Slicing = new ElementDefinition.SlicingComponent()
                            {
                                Discriminator = new string[] { "system" },
                            }
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "foo",
                            Max = "1",
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "bar",
                            Max = "1",
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "baz",
                            Max = "1",
                        }
                    }
                }
            };

            var sdDerived = new StructureDefinition()
            {
                ConstrainedType = FHIRDefinedType.Patient,
                Base = sdBase.Url,
                Name = "DerivedPatient",
                Url = "http://example.org/fhir/StructureDefinition/DerivedPatient",
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                    {
                        new ElementDefinition("Patient.identifier")
                        {
                            Min = 1,
                        },
                        new ElementDefinition("Patient.identifier")
                        {
                            Name = "bar",
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

            Assert.IsTrue(nav.JumpToFirst("Patient.identifier"));
            Assert.IsNotNull(nav.Current.Slicing);
            Assert.IsNull(nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);    // Derived profile constraint

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("Patient.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("foo", nav.Current.Name);

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("Patient.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("bar", nav.Current.Name);
            Assert.AreEqual(1, nav.Current.Min);    // Derived profile constraint
            Assert.AreEqual("1", nav.Current.Max);  // Base profile constraint

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("Patient.identifier", nav.Path);
            Assert.IsNull(nav.Current.Slicing);
            Assert.AreEqual("baz", nav.Current.Name);

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreNotEqual("Patient.identifier", nav.Path);

        }


    }

}
