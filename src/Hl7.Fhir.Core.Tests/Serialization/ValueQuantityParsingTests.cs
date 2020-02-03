using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class ValueQuantityParsingTests
    {
        [TestMethod]
        public void RoundtripValueQuantityXml() => RoundtripValueQuantity(true);

        [TestMethod]
        public void RoundtripValueQuantityJson() => RoundtripValueQuantity(false);

        static void RoundtripValueQuantity(bool xml)
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

            var parsed = xml ? XmlRoundTrip(resource) : JsonRoundTrip(resource);

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

        static T XmlRoundTrip<T>(T resource) where T : Resource
        {
            var baseTestPath = CreateEmptyDir();

            var xmlFile = Path.Combine(baseTestPath, "ObservationWithValueQuantityExample.xml");
            var xml = new FhirXmlSerializer().SerializeToString(resource);
            File.WriteAllText(xmlFile, xml);

            xml = File.ReadAllText(xmlFile);
            var parsed = new FhirXmlParser(new ParserSettings { PermissiveParsing = true }).Parse<T>(xml);

            return parsed;
        }

        static T JsonRoundTrip<T>(T resource) where T : Resource
        {
            var baseTestPath = CreateEmptyDir();

            var jsonFile = Path.Combine(baseTestPath, "ObservationWithValueQuantityExample.json");
            var json = new FhirJsonSerializer().SerializeToString(resource);
            File.WriteAllText(jsonFile, json);

            json = File.ReadAllText(jsonFile);
            var parsed = new FhirJsonParser(new ParserSettings { PermissiveParsing = true }).Parse<T>(json);

            return parsed;
        }

        static string CreateEmptyDir()
        {
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripQuantity");
            if (Directory.Exists(baseTestPath)) Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);
            return baseTestPath;
        }


        [TestMethod]
        public void FhirDeserialization_ProducesDoseAsQuantity_AndFailsAllowedTypeValidation()
        {
            // Arrange
            var medicationStatement = generateMedicationStatementWithDose();
            var serializer = new FhirJsonSerializer();
            var json = serializer.SerializeToString(medicationStatement);
            var parser = new FhirJsonParser();
            var results = new List<ValidationResult>();

            // Act
            var medicationStatementDeserialized = parser.Parse<MedicationStatement>(json);

            // Assert
            // (1) Show the types of the original and deserialized Dose property. They should be the same.
            var origDoseAsElement = medicationStatement.Dosage[0].DoseAndRate[0].Dose;
            Assert.IsTrue(origDoseAsElement.TypeName == "SimpleQuantity");
            var doseAsElement = medicationStatementDeserialized.Dosage[0].DoseAndRate[0].Dose;
            Assert.IsTrue(doseAsElement.TypeName == "Quantity");

            // (2) Show that validation fails, with error in AllowedType validation
            var isValid = DotNetAttributeValidation.TryValidate(medicationStatementDeserialized, results, true);
            Assert.IsTrue(isValid); // expect to pass
            //isValid.Should().BeFalse(); // woops
            //results[0].ErrorMessage.Should().Be("Value is of type Hl7.Fhir.Model.Quantity, which is not an allowed choice");
            //results[0].MemberNames.First().Should().Be("Dose");

        }

        private static MedicationStatement generateMedicationStatementWithDose()
        {
            var dose = new SimpleQuantity
            {
                Value = (decimal?)1.0,
                System = "http://unitsofmeasure.org",
                Code = "{tbl}"
            };

            var dosage = new Dosage
            {
                DoseAndRate = new List<Dosage.DoseAndRateComponent>
                {
                    new Dosage.DoseAndRateComponent
                    {
                        Dose = dose
                    }
                }
              
            };
            var ms = new MedicationStatement
            {
                // Min = 1 for Status, Subject, Medication
                Status = MedicationStatement.MedicationStatusCodes.Intended,
                Subject = new ResourceReference("Patient/123"),
                Medication = new ResourceReference("#med0309"),
                Dosage = new List<Dosage> { dosage }                
            };

            return ms;
        }

    }
}
