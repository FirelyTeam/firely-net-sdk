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
        class TimingSource : IConformanceSource
        {
            IConformanceSource _source;
            TimeSpan _duration = TimeSpan.Zero;

            public TimingSource(IConformanceSource source) { _source = source; }

            public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null) 
                => measureDuration(() => _source.FindConceptMaps(sourceUri, targetUri));

            public NamingSystem FindNamingSystem(string uniqueid) => measureDuration(() => _source.FindNamingSystem(uniqueid));

            public ValueSet FindValueSetBySystem(string system) => measureDuration(() => _source.FindValueSetBySystem(system));

            public IEnumerable<string> ListResourceUris(ResourceType? filter = default(ResourceType?)) => _source.ListResourceUris(filter);
                // => measureDuration(() => _source.ListResourceUris(filter));

            public Resource ResolveByCanonicalUri(string uri) => measureDuration(() => _source.ResolveByCanonicalUri(uri));

            public Resource ResolveByUri(string uri) => measureDuration(() => _source.ResolveByUri(uri));

            T measureDuration<T>(Func<T> f)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var result = f();
                sw.Stop();
                _duration += sw.Elapsed;
                return result;
            }

            public TimeSpan Duration => _duration;

            public void Reset() { _duration = TimeSpan.Zero; }

            public void ShowDuration(int count, TimeSpan totalDuration)
            {
                var totalMs = totalDuration.TotalMilliseconds;
                var resolverMs = _duration.TotalMilliseconds;
                var resolverFraction = resolverMs / totalMs;
                var snapshotMs = totalMs - resolverMs;
                var snapshotFraction = snapshotMs / totalMs;
                // Debug.Print($"Generated {count} snapshots in {totalMs} ms = {sourceMs} ms (resolver) + {snapshotMs} (snapshot) ({perc:2}%), on average {avg} ms per snapshot.");
                Console.WriteLine($"Generated {count} snapshots in {totalMs} ms = {resolverMs} ms (resolver) ({resolverFraction:P0}) + {snapshotMs} (snapshot) ({snapshotFraction:P0}).");
                var totalAvg = totalMs / count;
                var resolverAvg = resolverMs / count;
                var snapshotAvg = snapshotMs / count;
                Console.WriteLine($"Average per resource: {totalAvg} = {resolverAvg} ms (resolver) + {snapshotAvg} ms (snapshot)");
            }

        }

        SnapshotGenerator _generator;
        IResourceResolver _testResolver;
        TimingSource _source;

        readonly SnapshotGeneratorSettings _settings = new SnapshotGeneratorSettings()
        {
            // Throw on unresolved profile references; must include in TestData folder
            ExpandExternalProfiles = true,
            ForceExpandAll = true,
            MarkChanges = false,
            GenerateElementIds = false // STU3
        };

        [TestInitialize]
        public void Setup()
        {
            var dirSource = new DirectorySource("TestData/snapshot-test", includeSubdirectories: true);
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
            var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-research-authorization");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-legal-case");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/us-core-religion");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/string-translation");

            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

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

            var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyObservation2");

            Assert.IsNotNull(sd);

            // dumpReferences(sd);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

            dumpOutcome(_generator.Outcome);
            dumpBasePaths(expanded);
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


        [TestMethod]
        public void TestExpandAllComplexElements()
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
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-patient");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-encounter");

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

        // Helper class to verify results
        class ElementVerifier
        {
            IList<ElementDefinition> _elements;
            ElementDefinition _current;
            SnapshotGeneratorSettings _settings;
            int _pos;

            public ElementVerifier(StructureDefinition sd, SnapshotGeneratorSettings settings)
            {
                Assert.IsNotNull(sd);
                Assert.IsTrue(sd.HasSnapshot);
                _settings = settings;
                _elements = sd.Snapshot.Element;
                _pos = 0;
                var ann = sd.Annotation<OriginInformation>();
                Debug.Print($"Assert structure: url = '{sd.Url}' - origin = '{ann.Origin}'");
            }

            public ElementVerifier(IList<ElementDefinition> elements)
            {
                _elements = elements;
                _pos = 0;
            }

            // Find first element with matching path
            // Continue at the final element position from the last call to this method (or 0)
            // Search matching element in forward direction
            // Optionally verify specified name / id / fixed value
            public void VerifyElement(string path, string name = null, string elementId = null, Element fixedValue = null)
            {
                var elements = _elements;
                while (_pos < _elements.Count)
                {
                    var element = _current = elements[_pos++];
                    if (element.Path == path)
                    {
                        if (name != null)
                        {
                            Assert.AreEqual(name, element.Name, $"Invalid element name. Expected = '{name}', actual = '{element.Name}'.");
                        }
                        if (_settings.GenerateElementIds && elementId != null)
                        {
                            Assert.AreEqual(elementId, element.ElementId, $"Invalid elementId. Expected = '{elementId}', actual = '{element.ElementId}'.");
                        }
                        if (fixedValue != null)
                        {
                            Assert.IsTrue(element.Fixed != null && fixedValue.IsExactly(element.Fixed), $"Invalid fixed value. Expected = '{fixedValue}', actual = '{element.Fixed}'.");
                        }
                        return;
                    }
                }
                Assert.Fail($"No matching element found for path '{path}'");
            }

            public void AssertSlicing(IEnumerable<string> discriminator, ElementDefinition.SlicingRules? rules, bool? ordered)
            {
                var slicing = Current.Slicing;
                Assert.IsNotNull(slicing);
                Assert.IsTrue(discriminator.SequenceEqual(slicing.Discriminator), $"Invalid discriminator for element with path '{Current.Path}' - Expected: '{string.Join(",", discriminator)}' Actual: '{string.Join(",", slicing.Discriminator)}' ");
                Assert.AreEqual(slicing.Rules, rules);
                Assert.AreEqual(slicing.Ordered, ordered);
            }

            public ElementDefinition Current => _current;
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
            Assert.IsFalse(sd.Snapshot.Element.Any(e => e.Path == "Patient.deceased[x]"));  // Snapshot only contains renamed element constraint
            assertContainsElement(sd, "Patient.deceasedDateTime", null, "Patient.deceasedDateTime");

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
            verifier.AssertSlicing(new string[] { "type.value[x]" }, ElementDefinition.SlicingRules.Open, null);
            // "researchAuth/grandfatheredResAuth" represents a reslice of the base extension "researchAuth" (0...*)
            verifier.VerifyElement("Patient.extension", "researchAuth/grandfatheredResAuth", "Patient.extension:researchAuth/grandfatheredResAuth");
            verifier.VerifyElement("Patient.extension.extension", null, "Patient.extension:researchAuth/grandfatheredResAuth.extension");
            verifier.AssertSlicing(new string[] { "url" }, ElementDefinition.SlicingRules.Open, false);
            // The reslice "researchAuth/grandfatheredResAuth" has a child element constraint on "type.value[x]"
            // Therefore the complex extension is fully expanded (child extensions: type, flag, date)
            verifier.VerifyElement("Patient.extension.extension", "type", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type");
            verifier.VerifyElement("Patient.extension.extension.url", "type.url", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.url", new FhirUri("type"));
            // Child constraints on "type.value[x]" merged from differential
            verifier.VerifyElement("Patient.extension.extension.value[x]", "researchAuth/grandfatheredResAuth.type.value[x]", "Patient.extension:researchAuth/grandfatheredResAuth.extension:type.value[x]");
            verifier.VerifyElement("Patient.extension.extension", "flag", "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag");
            verifier.VerifyElement("Patient.extension.extension.url", "flag.url", "Patient.extension:researchAuth/grandfatheredResAuth.extension:flag.url", new FhirUri("flag"));
            verifier.VerifyElement("Patient.extension.extension", "date", "Patient.extension:researchAuth/grandfatheredResAuth.extension:date");
            verifier.VerifyElement("Patient.extension.extension.url", "date.url", "Patient.extension:researchAuth/grandfatheredResAuth.extension:date.url", new FhirUri("date"));
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
            settings.ExpandExternalProfiles = false;       
            _generator = new SnapshotGenerator(_testResolver, settings);

            StructureDefinition expanded;
            generateSnapshotAndCompare(sd, out expanded);

            var outcome = _generator.Outcome;
            dumpOutcome(outcome);

            Assert.IsNotNull(outcome);
            Assert.AreEqual(3, outcome.Issue.Count);

            assertProfileNotFoundIssue(outcome.Issue[0], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyMissingExtension");
            // Note: the extension reference to MyExtensionNoSnapshot should not generate an Issue,
            // as the profile only needs to merge the extension definition root element (no full expansion)
            assertProfileNotFoundIssue(outcome.Issue[1], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyIdentifier");
            assertProfileNotFoundIssue(outcome.Issue[2], Issue.UNAVAILABLE_REFERENCED_PROFILE, "http://example.org/fhir/StructureDefinition/MyCodeableConcept");
        }

        static void assertProfileNotFoundIssue(OperationOutcome.IssueComponent issue, Issue expected, string profileUrl)
        {
            Assert.IsNotNull(issue);
            Assert.AreEqual(expected.Type, issue.Code);
            Assert.AreEqual(expected.Severity, issue.Severity);
            Assert.AreEqual(expected.Code.ToString(), issue.Details.Coding[0].Code);
            Assert.IsNotNull(issue.Extension);
            Assert.AreEqual(profileUrl, issue.Diagnostics);
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

        // Unit tests for DifferentialTreeConstructor

        [TestMethod]
        public void TestDifferentialTree()
        {
            var e = new List<ElementDefinition>();

            e.Add(new ElementDefinition() { Path = "A.B.C1" });
            e.Add(new ElementDefinition() { Path = "A.B.C1", Name="C1-A" }); // First slice of A.B.C1
            e.Add(new ElementDefinition() { Path = "A.B.C2" });
            e.Add(new ElementDefinition() { Path = "A.B", Name="B-A" }); // First slice of A.B
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
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "A" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.use" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "B/1" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.type" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "B/2" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier.period.start" });
            elements.Add(new ElementDefinition() { Path = "Patient.identifier", Name = "C/1" });

            var tree = DifferentialTreeConstructor.MakeTree(elements);
            Assert.IsNotNull(tree);
            Debug.Print(string.Join(Environment.NewLine, tree.Select(e => $"{e.Path} : '{e.Name}'")));

            Assert.AreEqual(10, tree.Count);
            var verifier = new ElementVerifier(tree);

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
            var tree = DifferentialTreeConstructor.MakeTree(sd.Differential.Element);
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
                Debug.Print("Element.Id | Element.Path | Element.Base.Path");
                Debug.Print(new string('=', 100));
                foreach (var elem in sd.Snapshot.Element)
                {
                    Debug.WriteLine("{0}  |  {1}  |  {2}", elem.ElementId, elem.Path, elem.Base != null ? elem.Base.Path : null);
                }
                // Debug.Unindent();
            }
        }

        [Conditional("DEBUG")]
        void dumpOutcome(OperationOutcome outcome)
        {
            if (outcome != null)
            {
                Debug.Print("===== OperationOutcome: {0} issues", outcome.Issue.Count);
                for (int i = 0; i < outcome.Issue.Count; i++)
                {
                    dumpIssue(outcome.Issue[i], i);
                }
                Debug.Print("==================================");
            }
        }

        [Conditional("DEBUG")]
        private void dumpIssue(OperationOutcome.IssueComponent issue, int index)
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

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/daf-condition");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.com/fhir/StructureDefinition/patient-with-extensions");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/sdc-questionnaire");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/shareablevalueset");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/qicore-goal");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-guidanceartifact");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyLocation");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyPatient");
            // var sd = _testResolver.FindStructureDefinition(@"http://example.org/fhir/StructureDefinition/MyExtension1");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Element");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Patient");
            var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Extension");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Meta");
            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/Money");

            // var sd = _testResolver.FindStructureDefinition(@"http://hl7.org/fhir/StructureDefinition/cqif-basic-guidance-action");


            Assert.IsNotNull(sd);
            // dumpReferences(sd);

            var settings = new SnapshotGeneratorSettings(_settings);
            settings.MarkChanges = true;
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


        // [WMR 20160816] Test custom annotations containing associated base definitions
        class BaseDefAnnotation
        {
            public BaseDefAnnotation(ElementDefinition baseElemDef) { BaseElementDefinition = baseElemDef; }
            public ElementDefinition BaseElementDefinition { get; private set; }
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

        void constraintHandler(object sender, SnapshotConstraintEventArgs e)
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
                    Debug.Assert(elem.Base == null || ElementDefinitionNavigator.IsCandidateBaseElementPath(elem.Base.Path, baseDef.Path));
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

            // Id, Path & Base are expected to differ
            baseClone.ElementId = elem.ElementId;
            baseClone.Path = elem.Path;
            baseClone.Base = elem.Base;

            // Also ignore any Changed extensions on base and diff
            elemClone.RemoveAllChangedByDiff();
            baseClone.RemoveAllChangedByDiff();

            var result = !baseClone.IsExactly(elemClone);
            return result;
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

        static bool hasChanges<T>(IList<T> extendables) where T : IExtendable => extendables != null ? extendables.Any(e => isChanged(e)) : false;

        static bool isChanged(IExtendable extendable) => extendable != null && extendable.GetChangedByDiff() == true;

        [TestMethod]
        public void TestExpandCoreArtifacts()
        {
            // testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Element");
            // testExpandResource(@"http://hl7.org/fhir/StructureDefinition/BackboneElement");
            testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Extension");

            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/integer");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/positiveInt");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/string");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/code");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/id");

            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Meta");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/HumanName");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Quantity");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/SimpleQuantity");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Money");

            // testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Resource");
            // testExpandResource(@"http://hl7.org/fhir/StructureDefinition/DomainResource");

            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Basic");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Patient");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/Questionnaire");
            //testExpandResource(@"http://hl7.org/fhir/StructureDefinition/AuditEvent");

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
            var source = new DirectorySource("TestData/snapshot-test", false);
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
            var dirSource = new DirectorySource("TestData/validation", includeSubdirectories: false);
            var sd = dirSource.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-reslice-ek");
            Assert.IsNotNull(sd);

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
            var dirSource = new DirectorySource("TestData/snapshot-test/extensions", includeSubdirectories: false);
            var uris = dirSource.ListResourceUris(ResourceType.StructureDefinition);
            foreach (var uri in uris)
            {
                var sd = dirSource.FindStructureDefinition(uri);
                if (sd.IsExtension)
                {
                    if (sd.Differential.Element.Any(e => e.Path.StartsWith("Extension.extension.", StringComparison.Ordinal)))
                    {
                        var orgInfo = sd.Annotation<OriginInformation>();
                        Debug.WriteLine($"{uri} : '{orgInfo?.Origin}'");
                    }
                }
            }
        }

        // [WMR 20161207] TODO
        // Handle type slicing
        [TestMethod]
        public void TestTypeSlicing()
        {
            var sd = new StructureDefinition()
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
                        ,new ElementDefinition("Observation.valueString")
                        {
                            Type = new List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.String }
                            }
                        }
                    }
                }
            };

            var resources = new Resource[] { sd };
            var resolver = new InMemoryResourceResolver(resources);
            var multiResolver = new MultiResolver(_testResolver, resolver);
            var _generator = new SnapshotGenerator(multiResolver);
            StructureDefinition expanded = null;

            generateSnapshotAndCompare(sd, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Debug.Print("[1] Observation.value slice:");
            var elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value"));
            foreach (var elem in elems)
            {
                Debug.Print(elem.Path);
            }

            var nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsTrue(nav.MoveToNext("valueString"));

            sd.Differential.Element.Add(
                new ElementDefinition("Observation.valueCodeableConcept")
                {
                    Type = new List<ElementDefinition.TypeRefComponent>()
                    {
                        new ElementDefinition.TypeRefComponent() { Code = FHIRDefinedType.CodeableConcept }
                    }
                }
            );

            generateSnapshotAndCompare(sd, out expanded);
            Assert.IsNotNull(expanded);
            Assert.IsTrue(expanded.HasSnapshot);

            Debug.Print("[2] Observation.value slice:");
            elems = expanded.Snapshot.Element.Where(e => e.Path.StartsWith("Observation.value"));
            foreach (var elem in elems)
            {
                Debug.Print(elem.Path);
            }

            nav = new ElementDefinitionNavigator(expanded);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(nav.Path, "Observation");
            Assert.IsTrue(nav.MoveToChild("value[x]"));
            Assert.IsTrue(nav.MoveToNext("valueString"));
            Assert.IsTrue(nav.MoveToNext("valueCodeableConcept"));
        }

    }

    class InMemoryResourceResolver : IResourceResolver
    {
        ILookup<string, Resource> _resources;

        public InMemoryResourceResolver(IEnumerable<Resource> resources)
        {
            _resources = resources.OfType<IConformanceResource>().ToLookup(r => r.Url, r => r as Resource);
        }

        public Resource ResolveByCanonicalUri(string uri) => _resources[uri].FirstOrDefault();

        public Resource ResolveByUri(string uri) => _resources[uri].FirstOrDefault();
    }
}
