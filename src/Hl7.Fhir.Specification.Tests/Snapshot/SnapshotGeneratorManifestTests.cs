using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class SnapshotGeneratorManifestTests
    {
        const string ManifestPath = @"TestData\snapshot-test\Type Slicing";
        const string ManifestFileName = "manifest.xml";
        //const string ExtensionsPath = @"C:\Users\Michel\.fhir\packages\simplifier.core.r4.extensions-4.0.0\package";

        static readonly FhirXmlParsingSettings _fhirXmlParserSettings = new FhirXmlParsingSettings()
        {
            PermissiveParsing = false
        };

        //static readonly FhirJsonParsingSettings _fhirJsonParserSettings = new FhirJsonParsingSettings()
        //{
        //    PermissiveParsing = false
        //};

        ParserSettings _parserSettings = new ParserSettings()
        {
            PermissiveParsing = false
        };

        static readonly DirectorySourceSettings _dirSourceSettings = new DirectorySourceSettings()
        {
            IncludeSubDirectories = true,
            Includes = new string[] { "*.xml", "*.json" },
            FormatPreference = DirectorySource.DuplicateFilenameResolution.PreferXml,
            XmlParserSettings = _fhirXmlParserSettings
        };

        static readonly SnapshotGeneratorSettings _snapGenSettings = new SnapshotGeneratorSettings()
        {
            ForceRegenerateSnapshots = true,
            GenerateSnapshotForExternalProfiles = true
        };

        DirectorySource _dirSource;
        FhirXmlParser _fhirXmlParser;
        FhirJsonParser _fhirJsonParser;
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

            //var path = Path.Combine(TestContext.)
            var dirSource = _dirSource = new DirectorySource(ManifestPath, _dirSourceSettings);
            //var extensionSource = new DirectorySource(ExtensionsPath, _dirSourceSettings);
            var timingSource = new TimingSource(dirSource);
            var resolver = new CachedResolver(
                new MultiResolver(
                    new ZipSource("specification.zip"),
                    timingSource //,extensionSource
            ));

            _fhirXmlParser = new FhirXmlParser(_parserSettings);
            _fhirJsonParser = new FhirJsonParser(_parserSettings);

            _snapGen = new SnapshotGenerator(resolver, _snapGenSettings);

            _manifest = ReadManifest();
        }

        /// <summary>Run all tests</summary>
        [Ignore]
        [TestMethod]
        public void TestManifest()
        {
            var tests = _manifest.Test;
            Console.WriteLine($"Executing #{tests.Length} tests:");
            for (int i = 0; i < tests.Length; i++)
            {
                var test = tests[i];
                Console.WriteLine($"Executing test {i + 1} of {tests.Length}: {test.Id}");
                ExecuteTest(test);
            }
        }

        // Individual test methods per test

        [TestMethod] public void Test_t1() => ExecuteTest("t1");
        [TestMethod] public void Test_t2() => ExecuteTest("t2");
        [TestMethod] public void Test_t3() => ExecuteTest("t3");
        [TestMethod] public void Test_t4() => ExecuteTest("t4");
        [TestMethod] public void Test_t4a() => ExecuteTest("t4a");
        [TestMethod] public void Test_t5() => ExecuteTest("t5");
        [TestMethod] public void Test_t6() => ExecuteTest("t6");
        [TestMethod] public void Test_t7() => ExecuteTest("t7");
        [TestMethod] public void Test_t8() => ExecuteTest("t8");
        [TestMethod] public void Test_t9() => ExecuteTest("t9");
        [TestMethod] public void Test_t10() => ExecuteTest("t10");
        [TestMethod] public void Test_t11() => ExecuteTest("t11");
        [TestMethod] public void Test_t12() => ExecuteTest("t12");
        [TestMethod] public void Test_t12a() => ExecuteTest("t12a");
        [TestMethod] public void Test_t13() => ExecuteTest("t13");
        [TestMethod] public void Test_t14() => ExecuteTest("t14");
        [TestMethod] public void Test_t15() => ExecuteTest("t15");
        [TestMethod] public void Test_t16() => ExecuteTest("t16");
        [TestMethod] public void Test_t17() => ExecuteTest("t17");
        [TestMethod] public void Test_t18() => ExecuteTest("t18");
        [TestMethod] public void Test_t19() => ExecuteTest("t19");
        //[TestMethod] public void Test_t20() => ExecuteTest("t20");
        [TestMethod] public void Test_t21() => ExecuteTest("t21");
        [TestMethod] public void Test_t22() => ExecuteTest("t22");
        [TestMethod] public void Test_t23() => ExecuteTest("t23");
        //[TestMethod] public void Test_t23a() => ExecuteTest("t23a");
        //[TestMethod] public void Test_t24() => ExecuteTest("t24");
        [TestMethod] public void Test_t24b() => ExecuteTest("t24b");
        //[TestMethod] public void Test_t25() => ExecuteTest("t25");
        [TestMethod] public void Test_t26() => ExecuteTest("t26");
        [TestMethod] public void Test_t27() => ExecuteTest("t27");
        [TestMethod] public void Test_t28() => ExecuteTest("t28");
        [TestMethod] public void Test_t29() => ExecuteTest("t29");
        [TestMethod] public void Test_t29a() => ExecuteTest("t29a");
        //[TestMethod] public void Test_t30() => ExecuteTest("t30");
        [TestMethod] public void Test_t31() => ExecuteTest("t31");
        [TestMethod] public void Test_t32() => ExecuteTest("t32");
        [TestMethod] public void Test_t33() => ExecuteTest("t33");
        [TestMethod] public void Test_t34() => ExecuteTest("t34");
        [TestMethod] public void Test_t35() => ExecuteTest("t35");
        [TestMethod] public void Test_t36() => ExecuteTest("t36");
        [TestMethod] public void Test_t37() => ExecuteTest("t37");
        [TestMethod] public void Test_t38() => ExecuteTest("t38");
        //[TestMethod] public void Test_t39() => ExecuteTest("t39");
        [TestMethod] public void Test_t40() => ExecuteTest("t40");
        [TestMethod] public void Test_t41() => ExecuteTest("t41");
        [TestMethod] public void Test_t42() => ExecuteTest("t42");
        [TestMethod] public void Test_t43() => ExecuteTest("t43");
        //[TestMethod] public void Test_t43a() => ExecuteTest("t43a");
        [TestMethod] public void Test_t44() => ExecuteTest("t44");
        [TestMethod] public void Test_t45() => ExecuteTest("t45");
        [TestMethod] public void Test_samply1() => ExecuteTest("samply1");
        //[TestMethod] public void Test_au1() => ExecuteTest("au1");
        [TestMethod] public void Test_au2() => ExecuteTest("au2");
        [TestMethod] public void Test_au3() => ExecuteTest("au3");

        void ExecuteTest(string id) => ExecuteTest(_manifest.Test.FirstOrDefault(t => t.Id == id));

        void ExecuteTest(SnapshotGenerationManifestTest test)
        {
            const string inputFileNameFormat = "{0}-input.xml";
            const string expectedFileNameFormat = "{0}-expected.xml";

            Console.WriteLine($"Executing test: {test.Id}");

            Assert.IsNotNull(test);
            Assert.IsNotNull(test.Id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), ManifestPath);
            var inputFilePath = Path.Combine(path, string.Format(inputFileNameFormat, test.Id));
            var expectedFilePath = Path.Combine(path, string.Format(expectedFileNameFormat, test.Id));

            var input = LoadStructure(inputFilePath);
            var expected = LoadStructure(expectedFilePath);

            var output = (StructureDefinition)input.DeepCopy();
            _snapGen.Update(output);
            Assert.IsNull(_snapGen.Outcome, "The SnapshotGenerator reported one or more issues:\r\n" + _snapGen.Outcome?.ToString());
            Assert.IsTrue(output.HasSnapshot);

            // Verify rules against generated snapshot
            var rules = test.Rule;
            if (!(rules is null))
            {
                var ctx = new SnapshotEvaluationContext(input, expected);
                for (int i = 0; i < rules.Length; i++)
                {
                    VerifyRule(output, ctx, test, i);
                }
            }
        }

        void VerifyRule(StructureDefinition output, EvaluationContext ctx, SnapshotGenerationManifestTest test, int i)
        {
            var rule = test.Rule[i];
            Console.WriteLine($"Verify rule {i}: '{rule.Text}'");

            var nav = output.ToTypedElement();
            var expr = _fhirPathCompiler.Compile(rule.FhirPath);
            Assert.IsTrue(expr.Predicate(nav, ctx));
        }

        StructureDefinition LoadStructure(string filePath)
        {
            //Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath))
            {
                using (var stream = _dirSource.LoadArtifactByName(filePath))
                using (var reader = new XmlTextReader(stream))
                {
                    return _fhirXmlParser.Parse<StructureDefinition>(reader);
                }
            }

            filePath = Path.ChangeExtension(filePath, "json");
            if (File.Exists(filePath))
            {
                using (var stream = _dirSource.LoadArtifactByName(filePath))
                using (var textReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(textReader))
                {
                    return _fhirJsonParser.Parse<StructureDefinition>(reader);
                }
            }

            Assert.Fail("File not found: '{filePath}'");
            return null;
        }

        static SnapshotGenerationManifest ReadManifest()
        {
            var fullPath = Path.Combine(ManifestPath, ManifestFileName);
            Assert.IsTrue(File.Exists(fullPath));
            var serializer = new XmlSerializer(typeof(SnapshotGenerationManifest));
            using (var fs = new FileStream(fullPath, FileMode.Open))
            {
                return (SnapshotGenerationManifest)serializer.Deserialize(fs);
            }
        }
    }

    // Custom context for accessing input & expected result
    class SnapshotEvaluationContext : FhirEvaluationContext
    {
        Dictionary<string, ITypedElement> _aliases;

        public SnapshotEvaluationContext(StructureDefinition input, StructureDefinition expected) : base(input)
        {
            Input = input.ToTypedElement();
            Expected = expected.ToTypedElement();
        }

        public ITypedElement Input { get; }

        public ITypedElement Expected { get; }

        Dictionary<string, ITypedElement> Aliases => _aliases ?? (_aliases = new Dictionary<string, ITypedElement>());

        public void AddAlias(string id, ITypedElement elem) => Aliases[id] = elem;

        public ITypedElement Alias(string id) => Aliases[id];

        // Custom FhirPath method implementations

        public static void AddSymbols(SymbolTable symbols)
        {
            symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("fixture", Fixture);
            symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("aliasAs", AliasAs);
            symbols.Add<ITypedElement, string, EvaluationContext, ITypedElement>("alias", Alias);
        }

        public static ITypedElement Fixture(ITypedElement elem, string id, EvaluationContext ctx)
        {
            if (ctx is SnapshotEvaluationContext sctx)
            {
                if (id.EndsWith("-output")) { return sctx.Expected; }
                if (id == sctx.Input.Name) { return sctx.Input; }
            }
            return null;
        }

        public static ITypedElement AliasAs(ITypedElement elem, string id, EvaluationContext ctx)
        {
            if (ctx is SnapshotEvaluationContext sctx)
            {
                sctx.AddAlias(id, elem);
            }
            return elem;
        }

        public static ITypedElement Alias(ITypedElement elem, string id, EvaluationContext ctx)
        {
            if (ctx is SnapshotEvaluationContext sctx)
            {
                return sctx.Alias(id);
            }
            return null;
        }
    }

    // Serializable class for manifest.xml

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("snapshot-generation-tests", Namespace = "", IsNullable = false)]
    public class SnapshotGenerationManifest
    {

        SnapshotGenerationManifestTest[] _test;

        [XmlElement("test")]
        public SnapshotGenerationManifestTest[] Test
        {
            get => _test;
            set => _test = value;
        }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SnapshotGenerationManifestTest
    {

        SnapshotGenerationManifestTestRule[] _rule;

        bool _gen;
        bool _genSpecified;
        string _id;
        bool _sort;
        bool _sortSpecified;
        bool _fail;
        bool _failSpecified;
        string _register;
        string _include;

        [XmlElement("rule")]
        public SnapshotGenerationManifestTestRule[] Rule
        {
            get => _rule;
            set => _rule = value;
        }

        [XmlAttribute("gen")]
        public bool Gen
        {
            get => _gen;
            set => _gen = value;
        }

        [XmlIgnore()]
        public bool GenSpecified
        {
            get => _genSpecified;
            set => _genSpecified = value;
        }

        [XmlAttribute("id")]
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        [XmlAttribute("sort")]
        public bool Sort
        {
            get => _sort;
            set => _sort = value;
        }

        [XmlIgnore()]
        public bool SortSpecified
        {
            get => _sortSpecified;
            set => _sortSpecified = value;
        }

        [XmlAttribute("fail")]
        public bool Fail
        {
            get => _fail;
            set => _fail = value;
        }

        [XmlIgnore()]
        public bool FailSpecified
        {
            get => _failSpecified;
            set => _failSpecified = value;
        }

        [XmlAttribute("register")]
        public string Register
        {
            get => _register;
            set => _register = value;
        }

        [XmlAttribute("include")]
        public string Include
        {
            get => _include;
            set => _include = value;
        }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SnapshotGenerationManifestTestRule
    {

        string _text;
        string _fhirpath;

        [XmlAttribute("text")]
        public string Text
        {
            get => _text;
            set => _text = value;
        }

        [XmlAttribute("fhirpath")]
        public string FhirPath
        {
            get => _fhirpath;
            set => _fhirpath = value;
        }
    }

}
