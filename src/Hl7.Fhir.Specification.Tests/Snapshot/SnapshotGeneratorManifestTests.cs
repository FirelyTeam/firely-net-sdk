// [WMR 20190808] Enable to serialize the generated snapshot to disk, for debugging purposes
#define SERIALIZE_OUTPUT
// [WMR 20190808] Enable to log the generated and expected snapshot element paths to the console
#define LOG_OUTPUT

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    // SnapshotGenerator unit tests from HL7 FHIR repository, by Grahame
    // Source: https://github.com/hapifhir/org.hl7.fhir.core
    // https://github.com/hapifhir/org.hl7.fhir.core/tree/master/org.hl7.fhir.r5/src/test/resources/snapshot-generation
    // * Index all structures
    // * Process all tests specified in manifest.xml
    // * Each test specifies { id, input file, expected output file, fhirpath rules }
    // * Run SnapshotGenerator on input
    // * Execute fhirpath rules to verify the generated output
    // * Also compare the generated output with expected output

    // How to prevent Debug.Assert from interrupting unit test:
    // http://netitude.bc3tech.net/2013/11/04/keep-debug-assert-windows-from-halting-your-unit-tests/

    //
    // BUG: T16 - Invalid expansion of extension 'ISO-AddressUse'; expands to latitude/longitude...?! WRONG!
    // TODO: Merge pull request for 1067: Invalid root element base
    //

    [TestClass, TestCategory("Snapshot")]
    public class SnapshotGeneratorManifestTests
    {
        const string ManifestPath = @"TestData\snapshot-test\Type Slicing";
        const string ManifestFileName = "manifest.xml";
        //const string ExtensionsPath = @"C:\Users\Michel\.fhir\packages\simplifier.core.r4.extensions-4.0.0\package";

        const string inputFileNameFormat = "{0}-input.xml";
        const string outputFileNameFormat = "{0}-output.xml";
        const string expectedFileNameFormat = "{0}-expected.xml";

        static readonly FhirXmlParsingSettings _fhirXmlParserSettings = new FhirXmlParsingSettings()
        {
            PermissiveParsing = false
        };

        //static readonly FhirJsonParsingSettings _fhirJsonParserSettings = new FhirJsonParsingSettings()
        //{
        //    PermissiveParsing = false
        //};

        static readonly ParserSettings _parserSettings = new ParserSettings()
        {
            PermissiveParsing = false
        };

        static readonly DirectorySourceSettings _dirSourceSettings = new DirectorySourceSettings()
        {
            IncludeSubDirectories = true,
            // Exclude expected output, to prevent canonical url conflicts
            // Also include duplicate input file "t24a", conflicts with "t24a-input"
            Excludes = new string[] { "manifest.xml", "*-expected*", "*-output*", "t24a.xml" },
            FormatPreference = DirectorySource.DuplicateFilenameResolution.PreferXml,
            XmlParserSettings = _fhirXmlParserSettings
        };

        static readonly SnapshotGeneratorSettings _snapGenSettings = new SnapshotGeneratorSettings()
        {
            ForceRegenerateSnapshots = true,
            GenerateSnapshotForExternalProfiles = true
        };

        static readonly SerializerSettings _serializerSettings = new SerializerSettings()
        {
            Pretty = true
        };

        static readonly FhirXmlParser _fhirXmlParser = new FhirXmlParser(_parserSettings);
        static readonly FhirJsonParser _fhirJsonParser = new FhirJsonParser(_parserSettings);
        static readonly FhirXmlSerializer _fhirXmlSerializer = new FhirXmlSerializer(_serializerSettings);

        string _testPath;
        DirectorySource _dirSource;
        IResourceResolver _resolver;
        SnapshotGenerator _snapGen;
        SnapshotGenerationManifest _manifest;
        FhirPathCompiler _fhirPathCompiler;

        [TestInitialize]
        public void TestInitialize()
        {
            FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            var symbols = new SymbolTable();
            symbols.AddStandardFP();
            SnapshotEvaluationContext.AddSymbols(symbols);
            _fhirPathCompiler = new FhirPathCompiler(symbols);


            //
            // TODO
            // Load input from disk, do NOT use DirectorySource
            // SnapshotGenerator only needs access to core resources, and maybe extensions
            // Hide all other input files, to prevent canonical url resolving conflicts


            // Create unfiltered DirectorySource to access input & expected
            // Create filtered DirectorySource to resolve from input only
            // (ignore expected to prevent canonical url resolving conflicts)
            _testPath = Path.Combine(Directory.GetCurrentDirectory(), ManifestPath);

            // Fix known issues in test input (BEFORE initializing the DirectorySource!)
            FixInput();

            _dirSource = new DirectorySource(_testPath, _dirSourceSettings);
            var timingSource = new TimingSource(_dirSource);
            _resolver = new CachedResolver(
                new MultiResolver(
                    new ZipSource("specification.zip"),
                    timingSource
            //,extensionSource
            ));
            //_resolver = new CachedResolver(new ZipSource(ZipSource.SpecificationZipFileName));

            _snapGen = new SnapshotGenerator(_resolver, _snapGenSettings);

            _manifest = ReadManifest();
        }

        // Fix known issues in test input
        void FixInput()
        {
            Fix_t4a();
            Fix_t13();
            Fix_t15();
            Fix_t16();
            Fix_t23();
            Fix_t29();
            Fix_au3();
        }

        // t4a: update id from "t4" to "t4a"
        void Fix_t4a()
        {
            const string id = "t4a";
            Console.WriteLine($"Fix input '{id}'");

            var inputFilePath = Path.Combine(_testPath, string.Format(inputFileNameFormat, id));
            var input = Load(inputFilePath);
            Assert.IsNotNull(input);
            if (input.Id != id)
            {
                input.Id = id;
                Save(inputFilePath, input);
            }

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            var expected = Load(expectedFilePath);
            Assert.IsNotNull(expected);
            if (expected.Id != id)
            {
                expected.Id = id;
                Save(expectedFilePath, expected);
            }
        }

        // T13: insert missing type slice entry elements 'Patient.extension.extension.value[x]'
        void Fix_t13()
        {
            const string id = "t13";
            Console.WriteLine($"Fix input '{id}'");

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            var expected = Load(expectedFilePath);
            Assert.IsNotNull(expected);

            // Note: also fix manifest entry for t13, rule 15 (last); fix element index
            InsertTypeSlicingIntro(expected.Snapshot);
            Save(expectedFilePath, expected);
        }

        // t15: insert missing slice entry elements 'Patient.address.extension.extension.value[x]'
        // before actual type slices 'Patient.address.extension.extension.valueDecimal'
        void Fix_t15()
        {
            const string id = "t15";
            Console.WriteLine($"Fix input '{id}'");

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            var expected = Load(expectedFilePath);
            Assert.IsNotNull(expected);

            InsertTypeSlicingIntro(expected.Snapshot);
            Save(expectedFilePath, expected);
        }

        // t16: Fix invalid slice names, replace illegal period "." characters
        void Fix_t16()
        {
            const string id = "t16";
            Console.WriteLine($"Fix input '{id}'");

            var inputFilePath = Path.Combine(_testPath, string.Format(inputFileNameFormat, id));
            var input = Load(inputFilePath);
            Assert.IsNotNull(input);
            FixSliceNames(input.Differential.Element);
            Save(inputFilePath, input);

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            var expected = Load(expectedFilePath);
            Assert.IsNotNull(expected);
            FixSliceNames(expected.Differential.Element);
            FixSliceNames(expected.Snapshot.Element);
            InsertTypeSlicingIntro(expected.Snapshot);
            Save(expectedFilePath, expected);
        }

        // au3: binding.valueSetReference => binding.valueSet
        // STU3: ElementDefinition.binding.valueSet[x] : { Uri, Reference }
        // R4:   ElementDefinition.binding.valueSet    : Canonical
        void Fix_au3()
        {
            const string id = "au3";
            Console.WriteLine($"Fix input '{id}'");

            var inputFilePath = Path.ChangeExtension(Path.Combine(_testPath, string.Format(inputFileNameFormat, id)), "json");
            FixBindingValueSet(inputFilePath);

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            FixBindingValueSet(expectedFilePath);
        }

        // t23: input diff element order is incorrect, Patient.contact.telecom < Patient.contact.gender
        void Fix_t23()
        {
            const string id = "t23";
            Console.WriteLine($"Fix input '{id}'");

            var inputFilePath = Path.Combine(_testPath, string.Format(inputFileNameFormat, id));
            var input = Load(inputFilePath);
            Assert.IsNotNull(input);
            var elems = input.Differential.Element;
            Assert.AreEqual(5, elems.Count);
            if (elems[3].Path == "Patient.contact.gender" && elems[4].Path == "Patient.contact.telecom")
            {
                Swap(elems, 3, 4);
                Save(inputFilePath, input);
            }
        }

        // t29: Fix StructureDefinition.type = "OperationOutcome" ?! => Should be "Parameters"
        void Fix_t29()
        {
            const string id = "t29";
            Console.WriteLine($"Fix input '{id}'");

            var inputFilePath = Path.Combine(_testPath, string.Format(inputFileNameFormat, id));
            var input = Load(inputFilePath);
            Assert.IsNotNull(input);
            var ParametersTypeName = FHIRAllTypes.Parameters.GetLiteral();
            Assert.AreEqual(ParametersTypeName, input.Differential.Element[0].Path);
            if (input.Type != ParametersTypeName)
            {
                input.Type = ParametersTypeName;
                Save(inputFilePath, input);
            }

            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, id));
            var expected = Load(expectedFilePath);
            Assert.IsNotNull(expected);
            if (expected.Type != ParametersTypeName)
            {
                input.Type = ParametersTypeName;
                Save(expectedFilePath, expected);
            }
        }


        static void FixBindingValueSet(string filePath)
        {
            var original =
                "\"valueSetReference\": {\r\n"
                + "            \"reference\": \"http://hl7.org.au/fhir/ch/v1/ValueSet/ncdhc-observation-category\"\r\n"
                + "          }";
            var replacement = "\"valueSet\": \"http://hl7.org.au/fhir/ch/v1/ValueSet/ncdhc-observation-category\",";

            var input = File.ReadAllText(filePath);
            var corrected = input.Replace(original, replacement);
            if (corrected != input)
            {
                File.WriteAllText(filePath, corrected);
            }
        }

        static void FixSliceNames(List<ElementDefinition> elem) => elem.ForEach(e => FixSliceName(e));

        static void FixSliceName(ElementDefinition elem)
        {
            if (!string.IsNullOrEmpty(elem.SliceName) && elem.SliceName.Contains("."))
            {
                var sliceName = elem.SliceName.Replace(".", "-");
                Console.WriteLine($"Element '{elem.ElementId}': Fix invalid sliceName '{elem.SliceName}' => '{sliceName}'");
                elem.SliceName = sliceName;
            }
        }

        // Insert missing slice entry elements '...value[x]'
        // before actual type slices e.g. '...valueDecimal'
        void InsertTypeSlicingIntro(StructureDefinition.SnapshotComponent snapshot)
        {
            Assert.IsNotNull(snapshot);

            const string VALUE_X = "value[x]";

            var renamedValues = snapshot.Element.Where(e => ElementDefinitionNavigator.IsRenamedChoiceTypeElement(VALUE_X, e.GetNameFromPath()));

            var nav = new ElementDefinitionNavigator(snapshot.Element);
            foreach (var elem in renamedValues)
            {
                Assert.IsTrue(nav.MoveTo(elem));
                if (!nav.MoveToPrevious(VALUE_X))
                {
                    var valueElem = new ElementDefinition(elem.GetParentNameFromPath() + "." + VALUE_X)
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                            {
                                ElementDefinition.DiscriminatorComponent.ForTypeSlice()
                            }
                        }
                    };
                    nav.InsertBefore(valueElem);
                }
            }

            snapshot.Element = nav.ToListOfElements();
        }

        static void Swap<T>(List<T> elems, int x, int y)
        {
            var z = elems[x];
            elems[x] = elems[y];
            elems[y] = z;
        }

        /// <summary>Run all tests</summary>
        [Ignore]
        [TestMethod]
        public async T.Task TestManifest()
        {
            var tests = _manifest.Test;
            Console.WriteLine($"Executing #{tests.Length} tests:");
            for (int i = 0; i < tests.Length; i++)
            {
                var test = tests[i];
                Console.WriteLine($"Executing test {i + 1} of {tests.Length}: {test.Id}");
                await ExecuteTest(test);
            }
        }

        // Individual test methods per test

        [TestMethod] public async T.Task Test_t01() => await ExecuteTest("t1");
        [TestMethod] public async T.Task Test_t02() => await ExecuteTest("t2");
        [TestMethod] public async T.Task Test_t03() => await ExecuteTest("t3");
        [TestMethod] public async T.Task Test_t04() => await ExecuteTest("t4");
        [TestMethod] public async T.Task Test_t04a() => await ExecuteTest("t4a");
        [TestMethod] public async T.Task Test_t05() => await ExecuteTest("t5");
        [TestMethod] public async T.Task Test_t06() => await ExecuteTest("t6");
        [TestMethod] public async T.Task Test_t07() => await ExecuteTest("t7");
        [TestMethod] public async T.Task Test_t08() => await ExecuteTest("t8");
        [TestMethod] public async T.Task Test_t09() => await ExecuteTest("t9");
        [TestMethod] public async T.Task Test_t10() => await ExecuteTest("t10");
        [TestMethod] public async T.Task Test_t11() => await ExecuteTest("t11");
        [TestMethod] public async T.Task Test_t12() => await ExecuteTest("t12");
        [TestMethod] public async T.Task Test_t12a() => await ExecuteTest("t12a");
        [TestMethod] public async T.Task Test_t13() => await ExecuteTest("t13");
        [TestMethod] public async T.Task Test_t14() => await ExecuteTest("t14");

        // FAILS - FIXED
        // Input specifies:
        // - (2x) Patient.address.extension.extension.valueDecimal
        // Expected output contains:
        // - (2x) Patient.address.extension.extension.valueDecimal
        // Generated output contains:
        // - (2x) Patient.address.extension.extension.value[x]
        // - (2x) Patient.address.extension.extension.valueDecimal
        // Note: generated is correct, expected is incorrect
        // SnapGen SHOULD emit [x] element
        // TODO: SnapGen should also emit <slicing> node on type sliced [x] element
        [TestMethod] public async T.Task Test_t15() => await ExecuteTest("t15");

        // FAILS - FIXED
        // Input specifies:
        // - (2x) Patient.address.extension.extension.valueDecimal
        // - (2x) Patient.address.extension.extension.valueDecimal.extension.valueString
        // -      Patient.address.extension 'ISO-AddressUse'
        // Expected output contains:
        // - (2x) Patient.address.extension.extension.valueDecimal
        // - (2x) Patient.address.extension.extension.valueDecimal.extension.valueString
        // -      Patient.address.extension 'ISO-AddressUse'
        // Generated output contains:
        // - (2x) Patient.address.extension.extension.value[x]
        // - (2x) Patient.address.extension.extension.valueDecimal
        // - (2x) Patient.address.extension.extension.valueDecimal.extension.value[x]
        // - (2x) Patient.address.extension.extension.valueDecimal.extension.valueString
        // -      Patient.address.extension 'ISO-AddressUse'
        [TestMethod] public async T.Task Test_t16() => await ExecuteTest("t16");

        [TestMethod] public async T.Task Test_t17() => await ExecuteTest("t17");
        [TestMethod] public async T.Task Test_t18() => await ExecuteTest("t18");
        [TestMethod] public async T.Task Test_t19() => await ExecuteTest("t19");
        //[TestMethod] public async T.Task Test_t20() => await ExecuteTest("t20");
        [TestMethod] public async T.Task Test_t21() => await ExecuteTest("t21");

        // FAILS - FIXED
        // Expected output expands 'validDate' extensions
        // 3 x 4 = 12 elements (.id, .extension, .url, .valueDateTime)
        // Generated output does NOT expand extension children
        // Note: profile does not constrain extension child elements, so why expand?
        [TestMethod] public async T.Task Test_t22() => await ExecuteTest("t22");

        // FAILS - FIXED
        [TestMethod] public async T.Task Test_t23() => await ExecuteTest("t23");
        //[TestMethod] public async T.Task Test_t23a() => await ExecuteTest("t23a");

        //[TestMethod] public async T.Task Test_t24() => await ExecuteTest("t24");
        [TestMethod] public async T.Task Test_t24a() => await ExecuteTest("t24a");
        [TestMethod] public async T.Task Test_t24b() => await ExecuteTest("t24b");

        //[TestMethod] public async T.Task Test_t25() => await ExecuteTest("t25");

        // FAILS!
        // [WMR 20190814] t26-input and t26-expected are exactly equal, only specify differential...?
        [TestMethod, Ignore] public async T.Task Test_t26() => await ExecuteTest("t26");

        [TestMethod] public async T.Task Test_t27() => await ExecuteTest("t27");

        // [WMR 20190814] Fix invalid snapshot, esp. nested extension 'language'
        // Differential: OperationOutcome.issue.details.text.extension.valueString.extension
        // Snapshot:     OperationOutcome.issue.details.text.extension.extension - WRONG!!!
        // [WMR 20190819] Fixed, SnapGen now auto-generates slice name for implicit type slice "valueString"
        [TestMethod] public async T.Task Test_t28() => await ExecuteTest("t28");

        // FAILS - FIXED
        // Fix StructureDefinition.type = "OperationOutcome" ?! => Should be "Parameters"
        [TestMethod] public async T.Task Test_t29() => await ExecuteTest("t29");

        [TestMethod] public async T.Task Test_t29a() => await ExecuteTest("t29a");
        //[TestMethod] public async T.Task Test_t30() => await ExecuteTest("t30");
        [TestMethod] public async T.Task Test_t30b() => await ExecuteTest("t30b");
        [TestMethod] public async T.Task Test_t31() => await ExecuteTest("t31");
        [TestMethod] public async T.Task Test_t32() => await ExecuteTest("t32");
        [TestMethod] public async T.Task Test_t33() => await ExecuteTest("t33");
        [TestMethod] public async T.Task Test_t34() => await ExecuteTest("t34");
        [TestMethod] public async T.Task Test_t35() => await ExecuteTest("t35");
        [TestMethod] public async T.Task Test_t36() => await ExecuteTest("t36");

        // FAILS! TODO
        // Typo in element path: "MedicationRequiest.dosageInstruction.timing.event"
        // Note: "MedicationRequiest" should be "MedicationRequest"
        // SnapGen throws exception WRONG! should report OperationOutcome issue
        [TestMethod] public async T.Task Test_t37() => await ExecuteTest("t37");

        [TestMethod] public async T.Task Test_t38() => await ExecuteTest("t38");
        //[TestMethod] public async T.Task Test_t39() => await ExecuteTest("t39");
        [TestMethod] public async T.Task Test_t40() => await ExecuteTest("t40");
        [TestMethod] public async T.Task Test_t41() => await ExecuteTest("t41");
        [TestMethod] public async T.Task Test_t42() => await ExecuteTest("t42");

        // FAILS! TODO
        [TestMethod] public async T.Task Test_t43() => await ExecuteTest("t43");


        //[TestMethod] public async T.Task Test_t43a() => await ExecuteTest("t43a");

        // Rename element implies type constraint
        // e.g. "valueQuantity" w/o type constraints implies type = Quantity
        [TestMethod] public async T.Task Test_t44() => await ExecuteTest("t44");

        [TestMethod] public async T.Task Test_t45() => await ExecuteTest("t45");
        [TestMethod] public async T.Task Test_samply1() => await ExecuteTest("samply1");
        //[TestMethod] public async T.Task Test_au1() => await ExecuteTest("au1");
        [TestMethod] public async T.Task Test_au2() => await ExecuteTest("au2");

        // FAILS: System.FormatException
        // Type checking the data: Encountered unknown element 'valueSetReference' at location
        // 'StructureDefinition.differential[0].element[2].binding[0].valueSetReference[0]' while parsing
        // STU3: ElementDefinition.binding.valueSet[x] : { Uri, Reference }
        // R4:   ElementDefinition.binding.valueSet    : Canonical
        [TestMethod] public async T.Task Test_au3() => await ExecuteTest("au3");


        [TestMethod] public async T.Task Test_obs1_0() => await ExecuteTest("obs-1");
        [TestMethod] public async T.Task Test_obs1_1() => await ExecuteTest("obs-1-1");

        [Ignore("snapshot generation should fail: issue #1252")]
        [TestMethod] public async T.Task Test_obs1_2() => await ExecuteTest("obs-1-2");

        [Ignore("issue #1253")]
        [TestMethod] public async T.Task Test_obs2_0() => await ExecuteTest("obs-2");
        [Ignore("issue #1253")]
        [TestMethod] public async T.Task Test_obs2_0a() => await ExecuteTest("obs-2a");
        [Ignore("issue #1253")]
        [TestMethod] public async T.Task Test_obs2_0b() => await ExecuteTest("obs-2b");

        [TestMethod] public async T.Task Test_obs2_1() => await ExecuteTest("obs-2-1");
        [TestMethod] public async T.Task Test_obs2_2() => await ExecuteTest("obs-2-2");

        [Ignore("issue #1254")]
        [TestMethod] public async T.Task Test_obs2_3() => await ExecuteTest("obs-2-3");
        [Ignore("issue #1254")]
        [TestMethod] public async T.Task Test_obs3_0() => await ExecuteTest("obs-3");

        [Ignore("issue #1255")]
        [TestMethod] public async T.Task Test_obs4_0() => await ExecuteTest("obs-4");

        [Ignore("issue #1256")]
        [TestMethod] public async T.Task Test_obs5_0() => await ExecuteTest("obs-5");


        async T.Task ExecuteTest(string id) => await ExecuteTest(_manifest.Test.FirstOrDefault(t => t.Id == id));

        async T.Task ExecuteTest(SnapshotGenerationManifestTest test)
        {
            Console.WriteLine($"Executing test: {test.Id}");

            Assert.IsNotNull(test);
            Assert.IsNotNull(test.Id);

            var inputFilePath = Path.Combine(_testPath, string.Format(inputFileNameFormat, test.Id));
            var expectedFilePath = Path.Combine(_testPath, string.Format(expectedFileNameFormat, test.Id));

            var input = Load(test.Id, inputFileNameFormat);
            var expected = test.Fail ? null : Load(test.Id, expectedFileNameFormat);
            Assert.IsTrue(test.Fail || expected.HasSnapshot);

            var output = (StructureDefinition)input.DeepCopy();
            Exception exception = null;
            try
            {
                await _snapGen.UpdateAsync(output);
            }
            catch (Exception ex)
            {
                exception = ex;
                Console.WriteLine($"The {nameof(SnapshotGenerator)} failed with an exception:");
                Console.WriteLine(ex.Message);
            }

            // Some test profiles specify unresolved references
            //Assert.IsNull(_snapGen.Outcome, $"The {nameof(SnapshotGenerator)} reported one or more issues:\r\n" + _snapGen.Outcome?.ToString());
            //Assert.IsTrue(output.HasSnapshot);
            if (!(_snapGen.Outcome is null))
            {
                Console.WriteLine($"The {nameof(SnapshotGenerator)} reported one or more issues:");
                Console.WriteLine(_snapGen.Outcome?.ToString());
            }

#if SERIALIZE_OUTPUT
            // Serialize the generated output to disk, for debugging purposes
            SaveOutput(test.Id, output);
#endif
#if LOG_OUTPUT
            // Log the generated and expected output to the console, for debugging purposes
            if (!(expected is null))
            {
                expected.Snapshot.Element.Log($"Expected snapshot has #{expected.Snapshot.Element.Count} elements:");
                Console.WriteLine();
            }

            if (output.HasSnapshot)
            {
                output.Snapshot.Element.Log($"Generated snapshot has #{output.Snapshot.Element.Count} elements:");
                Console.WriteLine();
            }
#endif

            if (test.Fail)
            {
                Assert.IsFalse(exception is null && _snapGen.Outcome is null, "SnapshotGenerator completed succesfully. Expecting Exception or OperationOutcome issues...");
                //Assert.IsNotNull(_snapGen.Outcome, "SnapshotGenerator completed succesfully. Expecting OperationOutcome issues...");
            }
            else
            {
                Assert.IsNull(exception);
                Assert.AreEqual(0, _snapGen.Outcome?.Fatals ?? 0);
                // Only accept errors (unresolved profile reference...) and warnings
                Assert.IsTrue(output.HasSnapshot);

                // Verify rules against generated snapshot
                var rules = test.Rule;
                if (!(rules is null))
                {
                    var ctx = new SnapshotEvaluationContext(_testPath, _resolver, test.Id, input, output);
                    for (int i = 0; i < rules.Length; i++)
                    {
                        VerifyRule(output, ctx, test, i);
                    }
                }
            }
        }

        void VerifyRule(StructureDefinition output, EvaluationContext ctx, SnapshotGenerationManifestTest test, int i)
        {
            var rule = test.Rule[i];
            Console.WriteLine($"Verify rule {i}: '{rule.Text}'");

            var nav = output.ToTypedElement();
            var expr = _fhirPathCompiler.Compile(rule.FhirPath);
            Assert.IsTrue(expr.Predicate(nav, ctx), $"FAILED Rule {i}: '{rule.Text}'");
        }

        StructureDefinition Load(string id, string fileNameFormat)
        {
            //var path = Path.Combine(Directory.GetCurrentDirectory(), ManifestPath);
            var inputFilePath = Path.Combine(_testPath, string.Format(fileNameFormat, id));
            return Load(inputFilePath);
        }

        static StructureDefinition Load(string filePath)
        {
            //Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                //using (var stream = _dirSource.LoadArtifactByName(filePath))
                using (var stream = File.OpenRead(filePath))
                using (var reader = new XmlTextReader(stream))
                {
                    return _fhirXmlParser.Parse<StructureDefinition>(reader);
                }
            }

            filePath = Path.ChangeExtension(filePath, "json");
            if (File.Exists(filePath))
            {
                //using (var stream = _dirSource.LoadArtifactByName(filePath))
                using (var stream = File.OpenRead(filePath))
                using (var textReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(textReader))
                {
                    return _fhirJsonParser.Parse<StructureDefinition>(reader);
                }
            }

            Assert.Fail($"File not found: '{filePath}'");
            return null;
        }

        [Conditional("SERIALIZE_OUTPUT")]
        static void SaveOutput(string id, StructureDefinition output)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), ManifestPath);
            var outputFilePath = Path.Combine(path, string.Format(outputFileNameFormat, id));
            Console.WriteLine($"Serialize generated output to: '{outputFilePath}'");
            Save(outputFilePath, output);
        }

        static void Save(string filePath, Base output)
        {
            var xml = _fhirXmlSerializer.SerializeToString(output);
            File.WriteAllText(filePath, xml);
        }

        static SnapshotGenerationManifest ReadManifest()
        {
            var fullPath = Path.Combine(ManifestPath, ManifestFileName);
            Assert.IsTrue(File.Exists(fullPath));
            var serializer = new XmlSerializer(typeof(SnapshotGenerationManifest));
            using (var fs = new FileStream(fullPath, FileMode.Open))
            {
                var manifest = (SnapshotGenerationManifest)serializer.Deserialize(fs);
                // Fix known invalid invariants
                FixManifest(manifest);
                return manifest;
            }
        }

        // Fix known invalid invariants in input manifest
        static void FixManifest(SnapshotGenerationManifest manifest)
        {
            // [WMR 20190910] Fix invalid type slice element renaming in snapshot (should be normalized)
            ReplaceTestRule("t13",
                "fixture('t13-output').snapshot.element[16].path = 'Patient.extension.extension.valueCodeableConcept'",
                "fixture('t13-output').snapshot.element[16].path = 'Patient.extension.extension.value[x]'");
            ReplaceTestRule("t13",
                "fixture('t13-output').snapshot.element[21].path = 'Patient.extension.extension.valuePeriod'",
                "fixture('t13-output').snapshot.element[21].path = 'Patient.extension.extension.value[x]'");


            // [WMR 20190819] Expecting +2 "value[x]" elements
            // [MV 20191216] Changed the rule because of technical correction 4.0.1
            // [MV 20191216] Remove the +2 elements:
            //UpdateElementIndices("t13", 16);        // Bump element indices starting at "valueCodeableConcept" (16)
            //UpdateElementIndices("t13", 21 + 1);    // Bump element indices starting at "valuePeriod" (21)

            // [WMR 20190812] Expecting +2 "value[x]" elements
            // [MV 20191216] Changed the rule because of technical correction 4.0.1
            ReplaceTestRule("t15",
                @"fixture('t15-output').snapshot.element.count() = fixture('patient').snapshot.element.count() + 27",
                @"fixture('t15-output').snapshot.element.count() = fixture('patient').snapshot.element.count() + 28");

            // [WMR 20190812] Expecting +2 "value[x]" elements
            // [MV 20191216] Changed the rule because of technical correction 4.0.1
            ReplaceTestRule("t16",
                @"fixture('t16-output').snapshot.element.count() = fixture('t15-output').snapshot.element.count() + 17",
                @"fixture('t16-output').snapshot.element.count() = fixture('t15-output').snapshot.element.count() + 20");

            // [WMR 20190812] Expecting -12 extension child elements
            // Expected output expands 'validDate' extensions
            // 3 x 4 = 12 elements (.id, .extension, .url, .valueDateTime)
            // Note: profile does not constrain extension child elements, so why expand?
            ReplaceTestRule("t22",
                @"fixture('t22-output').snapshot.element.count().trace('t22o') = fixture('patient').snapshot.element.count().trace('t22patient') + 76",
                @"fixture('t22-output').snapshot.element.count().trace('t22o') = fixture('patient').snapshot.element.count().trace('t22patient') + 64");

            // [WMR 20190814] Insert missing test rule for t24a (inherited by t24b)
            var testList = manifest.Test.ToList();
            var idx = testList.FindIndex(t => t.Id == "t24b");
            Assert.IsTrue(idx >= 0);
            testList.Insert(idx, new SnapshotGenerationManifestTest()
            {
                Id = "t24a"
            });
            manifest.Test = testList.ToArray();

            ReplaceTestRule("t24b",
                "fixture('t24b-output').snapshot.element.count().trace('t24bo') = fixture('t24b-include').snapshot.element.count().trace('t24ao')",
                "fixture('t24b-output').snapshot.element.count().trace('t24bo') = fixture('t24a-output').snapshot.element.count().trace('t24ao')");

            void ReplaceTestRule(string id, string originalExpression, string fixedExpression)
            {
                var test = manifest.Test.FirstOrDefault(t => t.Id == id);
                Assert.IsNotNull(test);
                var rule = test.Rule.FirstOrDefault(r => r.FhirPath == originalExpression);
                Assert.IsNotNull(rule);
                rule.FhirPath = fixedExpression;
            }

            //void UpdateElementIndices(string id, int startIndex, int delta = 1)
            //{
            //    var test = manifest.Test.FirstOrDefault(t => t.Id == id);
            //    Assert.IsNotNull(test);
            //    foreach (var rule in test.Rule)
            //    {
            //        var expression = rule.FhirPath;
            //        const string subexpression = ".snapshot.element[";
            //        var start = expression.IndexOf(subexpression) + subexpression.Length - 1;
            //        var end = expression.IndexOf("]", start + 1);
            //        var param = expression.Substring(start + 1, end - start - 1);
            //        if (int.TryParse(param, out var index) && index >= startIndex)
            //        {
            //            var newIndex = (index + delta).ToString();
            //            rule.FhirPath = expression.Substring(0, start + 1) + newIndex + expression.Substring(end);
            //        }
            //    }
            //}

        }

        // Custom context for accessing input & expected result
        class SnapshotEvaluationContext : FhirEvaluationContext
        {
            Dictionary<string, ITypedElement> _aliases;
            string _testPath;

            public SnapshotEvaluationContext(
                string testPath, IResourceResolver resolver, string id,
                StructureDefinition input, StructureDefinition generated) : base(generated.ToTypedElement())
            {
                _testPath = testPath ?? throw new ArgumentNullException(nameof(testPath));
                TestResolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
                if (input is null) { throw new ArgumentNullException(nameof(input)); }
                if (generated is null) { throw new ArgumentNullException(nameof(generated)); }
                Input = input.ToTypedElement();
                Generated = generated.ToTypedElement();
                Id = id ?? throw new ArgumentNullException(nameof(id));
                Assert.AreEqual(id, generated.Id);
                this.Tracer = this.Trace;
            }

            void Trace(string msg, IEnumerable<ITypedElement> elems)
            {
                Console.WriteLine($"[TRACE] {msg}:");
                foreach (var elem in elems)
                {
                    Console.WriteLine($"[TRACE] '{elem.Name}' : {elem.InstanceType}{(elem.HasValue() ? $" = '{elem.Value.ToString()}'" : null)}");
                }
            }

            public string Id { get; }

            public IResourceResolver TestResolver { get; }

            public ITypedElement Input { get; }

            public ITypedElement Generated { get; }

            // Custom FhirPath method implementations

            Dictionary<string, ITypedElement> Aliases => _aliases ?? (_aliases = new Dictionary<string, ITypedElement>());

            void AddAlias(string alias, ITypedElement elem) => Aliases[alias] = elem;

            ITypedElement Alias(string alias) => Aliases[alias];

            ITypedElement Fixture(string name)
            {
                if (name == $"{Id}-input") { return Input; }
                if (name == $"{Id}-output") { return Generated; }

                // Also expose previous inputs & generated outputs, e.g. t16 depends on t15
                if (name.EndsWith("-output") || name.EndsWith("-input"))
                {
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), ManifestPath, name + ".xml");
                    var filePath = Path.Combine(_testPath, name + ".xml");
                    if (!File.Exists(filePath))
                    {
                        filePath = Path.ChangeExtension(filePath, "json");
                    }
                    return Load(filePath).ToTypedElement();
                }

                // Otherwise assume name refers to a core resource, e.g. 'patient'
                // Names are specified in lower case, try to find matching core typename
                var typeName = ModelInfo.FhirTypeToCsType.Keys.FirstOrDefault(key => StringComparer.OrdinalIgnoreCase.Equals(name, key));
                if (!(typeName is null))
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    return TestResolver.FindStructureDefinitionForCoreType(typeName).ToTypedElement();
#pragma warning restore CS0618 // Type or member is obsolete
                }

                Console.WriteLine($"WARNING! Unresolved fixture: '{name}'");
                return null;
            }

            // Add custom FHIRPath methods for unit testing
            public static void AddSymbols(SymbolTable symbols)
            {
                symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("fixture", Fixture);
                symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("aliasAs", AliasAs);
                symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("alias", Alias);
                symbols.Add<ITypedElement, bool, string, EvaluationContext, ITypedElement>("check", Check);
            }

            // Custom FHIRPath methods for unit testing

            public static ITypedElement Fixture(ITypedElement elem, string name, EvaluationContext ctx)
                => ctx is SnapshotEvaluationContext sctx ? sctx.Fixture(name) : null;

            public static ITypedElement AliasAs(ITypedElement elem, string id, EvaluationContext ctx)
            {
                if (ctx is SnapshotEvaluationContext sctx)
                {
                    sctx.AddAlias(id, elem);
                }
                return elem;
            }

            public static ITypedElement Alias(ITypedElement elem, string id, EvaluationContext ctx)
                => ctx is SnapshotEvaluationContext sctx ? sctx.Alias(id) : null;

            public static ITypedElement Check(ITypedElement elem, bool condition, string message, EvaluationContext ctx)
            {
                Assert.IsTrue(condition, $"[CHECK] '{elem.Name}' {message}");
                //if (!condition)
                //{
                //    Console.WriteLine($"[CHECK] '{elem.Name}' {message}");
                //}
                return elem;
            }
        }

        // Serializable classes for parsing manifest.xml

        [Serializable()]
        [DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        [XmlRoot("snapshot-generation-tests", Namespace = "", IsNullable = false)]
        public class SnapshotGenerationManifest
        {
            [XmlElement("test")]
            public SnapshotGenerationManifestTest[] Test { get; set; }
        }

        [Serializable()]
        [DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        public class SnapshotGenerationManifestTest
        {
            [XmlElement("rule")]
            public SnapshotGenerationManifestTestRule[] Rule { get; set; }

            [XmlAttribute("id")]
            public string Id { get; set; }

            [XmlAttribute("register")]
            public string Register { get; set; }

            [XmlAttribute("include")]
            public string Include { get; set; }

            [XmlAttribute("gen")]
            public bool Gen { get; set; }

            //[XmlIgnore()]
            //public bool GenSpecified { get; set; }

            [XmlAttribute("sort")]
            public bool Sort { get; set; }

            //[XmlIgnore()]
            //public bool SortSpecified { get; set; }

            [XmlAttribute("fail")]
            public bool Fail { get; set; }

            //[XmlIgnore()]
            //public bool FailSpecified { get; set; }

        }

        [Serializable()]
        [DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        public class SnapshotGenerationManifestTestRule
        {
            [XmlAttribute("text")]
            public string Text { get; set; }

            [XmlAttribute("fhirpath")]
            public string FhirPath { get; set; }
        }

    }
}