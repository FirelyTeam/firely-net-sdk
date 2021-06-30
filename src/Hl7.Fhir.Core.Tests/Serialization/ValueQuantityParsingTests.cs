using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class ValueQuantityParsingTests
    {
        [TestMethod]
        public async Tasks.Task RoundtripValueQuantityXml() => await RoundtripValueQuantity(true);

        [TestMethod]
        public async Tasks.Task RoundtripValueQuantityJson() => await RoundtripValueQuantity(false);

        static async Tasks.Task RoundtripValueQuantity(bool xml)
        {
            var resource = new StructureDefinition()
            {
                Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new System.Collections.Generic.List<ElementDefinition>()
                    {
                        new ElementDefinition("Observation.valueQuantity")
                        {
                            Type = new System.Collections.Generic.List<ElementDefinition.TypeRefComponent>()
                            {
                                new ElementDefinition.TypeRefComponent()
                                {
                                    Code = FHIRAllTypes.Quantity.GetLiteral()
                                }
                            },
                            Example = new System.Collections.Generic.List<ElementDefinition.ExampleComponent>()
                            {
                                new ElementDefinition.ExampleComponent()
                                {
                                    Label = "Example Quantity",
                                    Value = new Quantity(42.0M, "kg", "http://ucom.org/example")
                                }
                            }
                        }
                    }
                }
            };

            var orgExample = resource.Differential.Element[0].Example[0];
            Assert.AreEqual("Quantity", orgExample.Value.TypeName);

            var parsed = xml ? await XmlRoundTripAsync(resource) : await JsonRoundTrip(resource);

            Assert.IsNotNull(parsed);
            Assert.IsNotNull(parsed.Differential?.Element);
            Assert.AreEqual(1, parsed.Differential.Element.Count);
            var examples = parsed.Differential.Element[0].Example;
            Assert.IsNotNull(examples);
            Assert.AreEqual(1, examples.Count);
            var example = examples[0];
            Assert.IsNotNull(example);
            Assert.AreEqual(orgExample.Label, example.Label);
            Assert.IsNotNull(example.Value);
            Assert.AreEqual("Quantity", example.Value.TypeName);
        }

        async static Tasks.Task<T> XmlRoundTripAsync<T>(T resource) where T : Resource
        {
            var baseTestPath = CreateEmptyDir();

            var xmlFile = Path.Combine(baseTestPath, "ObservationWithValueQuantityExample.xml");
            var xml = await new FhirXmlSerializer().SerializeToStringAsync(resource);
            await File.WriteAllTextAsync(xmlFile, xml);

            xml = await File.ReadAllTextAsync(xmlFile);
            var parsed = await new FhirXmlParser(new ParserSettings { PermissiveParsing = true }).ParseAsync<T>(xml);

            return parsed;
        }

        static async Tasks.Task<T> JsonRoundTrip<T>(T resource) where T : Resource
        {
            var baseTestPath = CreateEmptyDir();

            var jsonFile = Path.Combine(baseTestPath, "ObservationWithValueQuantityExample.json");
            var json = await new FhirJsonSerializer().SerializeToStringAsync(resource);
            await File.WriteAllTextAsync(jsonFile, json);

            json = await File.ReadAllTextAsync(jsonFile);
            var parsed = await new FhirJsonParser(new ParserSettings { PermissiveParsing = true }).ParseAsync<T>(json);

            return parsed;
        }

        static string CreateEmptyDir()
        {
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripQuantity");
            if (Directory.Exists(baseTestPath)) Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);
            return baseTestPath;
        }    
    }
}
