using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Specification.Source;
using Hl7.Fhir.Validation.Schema;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class SchemaConverterTests
    {
        readonly ISchemaResolver _resolver;
        readonly FhirPathCompiler _fpCompiler;

        public SchemaConverterTests()
        {
            _resolver = new ElementSchemaResolver(
                new CachedResolver(
                    new MultiResolver(
                        new ZipSource("specification.zip"),
                        new DirectorySource(@"C:\Users\Marco\Downloads")
                    )
                ));

            var symbolTable = new SymbolTable();
            symbolTable.AddStandardFP();
            symbolTable.AddFhirExtensions();
            _fpCompiler = new FhirPathCompiler(symbolTable);
        }

        private string BigString()
        {
            var sb = new StringBuilder(1024 * 1024);
            for (int i = 0; i < 1024; i++)
            {
                sb.Append("x");
            }

            var sub = sb.ToString();

            sb = new StringBuilder(1024 * 1024);
            for (int i = 0; i < 1024; i++)
            {
                sb.Append(sub);
            }
            sb.Append("more");
            return sb.ToString();
        }


        [TestMethod]
        public async T.Task MyTestMethod()
        {

            var poco = new Patient() { Name = new List<HumanName>() { new HumanName() { Family = BigString() } } };
            var patient = poco.ToTypedElement();

            var schemaElement = await _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/Patient", UriKind.Absolute));
            var json = schemaElement.ToJson().ToString();
            //Debug.WriteLine(json);


            var results = await schemaElement.Validate(new[] { patient }, new ValidationContext() { FhirPathCompiler = _fpCompiler });
            Assert.IsNotNull(results);
            var r = _resolver as ElementSchemaResolver;
            r.DumpCache();
            //Assert.AreEqual(1, results.Count);
            //Assert.AreEqual(0, results[0].Item1.Count);
            //json.ToString(); 

        }

        private string TypedElementAsString(ITypedElement element)
        {
            var json = BuildNode(element);
            return json.ToString();

            JToken BuildNode(ITypedElement elt)
            {
                var result = new JObject
                {
                    { "name", elt.Name },
                    { "type", elt.InstanceType },
                    { "location", elt.Location},
                    { "value", elt.Value?.ToString()},
                    { "definition", elt.Definition == null ? "" : DefintionNode(elt.Definition) }
                };
                result.Add(new JProperty("children", new JArray(elt.Children().Select(c =>
                  BuildNode(c).MakeNestedProp()))));


                return result;
            }

            JToken DefintionNode(IElementDefinitionSummary def)
            {
                var result = new JObject
                {
                    { "elementName", def.ElementName },
                    { "inSummary", def.InSummary },
                    { "isChoiceElement", def.IsChoiceElement },
                    { "isCollection", def.IsCollection },
                    { "isRequired", def.IsRequired },
                    { "isResource", def.IsResource },
                    { "nonDefaultNamespace", def.NonDefaultNamespace },
                    { "order", def.Order }
                };
                return result;
            }

        }

        [TestMethod]
        public async T.Task MyTestMethod2()
        {
            var poco = new HumanName() { Family = BigString() };
            poco.GivenElement.Add(new FhirString(BigString()));
            poco.GivenElement.Add(new FhirString("Maria"));
            poco.Use = HumanName.NameUse.Usual;
            var element = poco.ToTypedElement();

            var eltstring = TypedElementAsString(new ValueTypedElement(element));

            var schemaElement = await _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/HumanName", UriKind.Absolute));

            //var json = schemaElement.ToJson().ToString();

            var results = await schemaElement.Validate(new[] { element }, new ValidationContext());
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.IsFalse(results.Result.IsSuccessful, "HumanName is not valid");

            /*
            string json2 = "";
            foreach (var item in _resolver.GetSchemas())
            {
                json2 = item.ToJson().ToString();
            }
            _resolver.GetSchemas().Select(s => json2 += s.ToJson().ToString());

            var stringSchema = _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/string", UriKind.Absolute));
            json = stringSchema.ToJson().ToString();

            Debug.WriteLine(json);
            //json.ToString();
            */
        }
        [TestMethod]
        public async T.Task TestInstance()
        {
            var instantSchema = await _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/instant", UriKind.Absolute));

            var instantPoco = new Instant(DateTimeOffset.Now);

            var element = instantPoco.ToTypedElement();

            var result = await instantSchema.Validate(new[] { element }, new ValidationContext() { FhirPathCompiler = _fpCompiler });

            Assert.IsTrue(result.Result.IsSuccessful);
        }

        [TestMethod]
        public async T.Task ValidateMaxStringonFhirString()
        {
            var fhirString = new FhirString(BigString()).ToTypedElement();

            var stringSchema = await _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/string", UriKind.Absolute));

            var results = await stringSchema.Validate(new[] { fhirString }, new ValidationContext() { FhirPathCompiler = _fpCompiler });

            Assert.IsNotNull(results);

            var validationResult = results.OfType<ResultAssertion>();

            Assert.AreEqual(1, results.Count);
            var assertResult = results.Result;
            Assert.IsNotNull(assertResult);
            Assert.IsFalse(assertResult.IsSuccessful, "fhirString is not valid");
        }

        [TestMethod]
        public async T.Task ValidateOwnProfile()
        {


            var stringSchema = await _resolver.GetSchema(new Uri("http://example.org/fhir/StructureDefinition/MyHumanName", UriKind.Absolute));

            var json = stringSchema.ToJson().ToString();

            var poco = new HumanName() { Family = "Visser" };
            poco.Period = new Period(new FhirDateTime("2019-09-02"), new FhirDateTime("2019-09-05"));
            poco.GivenElement.Add(new FhirString(BigString()));
            poco.GivenElement.Add(new FhirString("Maria"));

            var results = await stringSchema.Validate(new[] { poco.ToTypedElement() }, new ValidationContext() { FhirPathCompiler = _fpCompiler });

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            var assertResult = results.Result;
            Assert.IsNotNull(assertResult);
            Assert.IsFalse(assertResult.IsSuccessful, "poco should be not valid");
        }

    }

    public class ValueTypedElement : ITypedElement
    {
        private readonly ITypedElement _wrapped;

        public ValueTypedElement(ITypedElement instance)
        {
            _wrapped = instance;
        }

        public string Name => _wrapped.Name;

        public string InstanceType => _wrapped.InstanceType;

        public object Value => _wrapped.Value;

        public string Location => _wrapped.Location;

        public IElementDefinitionSummary Definition => _wrapped.Definition;

        public IEnumerable<ITypedElement> Children(string name = null)
        {
            foreach (var child in _wrapped.Children())
            {
                yield return new ValueTypedElement(child);

            }
            if (_wrapped.InstanceType == "string")
            {
                yield return new ValueElementNode(_wrapped.Value, _wrapped.Location);
            }
        }
    }
}
