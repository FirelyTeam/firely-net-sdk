using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Specification.Source;
using Hl7.Fhir.Validation.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class SchemaConverterTests
    {
        readonly ISchemaResolver _resolver;

        public SchemaConverterTests()
        {
            _resolver = new ElementSchemaResolver(new CachedResolver(new ZipSource("specification.zip")));
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
        public void MyTestMethod()
        {

            var poco = new Patient() { Name = new List<HumanName>() { new HumanName() { Family = BigString() } } };
            var patient = poco.ToTypedElement();

            var schemaElement = _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/Patient", UriKind.Absolute));
            var results = schemaElement.Validate(new[] { patient }, new ValidationContext());
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(0, results[0].Item1.Count);
            var json = schemaElement.ToJson().ToString();
            Debug.WriteLine(json);
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
                    { "definition", DefintionNode(elt.Definition) }
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
        public void MyTestMethod2()
        {


            var poco = new HumanName() { Family = "a" };
            poco.GivenElement.Add(new FhirString("Marcus"));
            poco.GivenElement.Add(new FhirString("Maria"));
            var element = poco.ToTypedElement();

            var eltstring = TypedElementAsString(element);

            var schemaElement = _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/HumanName", UriKind.Absolute));
            var json = schemaElement.ToJson().ToString();
            var results = schemaElement.Validate(new[] { element }, new ValidationContext());
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            Debug.WriteLine(json);
            //json.ToString();

        }
    }
}
