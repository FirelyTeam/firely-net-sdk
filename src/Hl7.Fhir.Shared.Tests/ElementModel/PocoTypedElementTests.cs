using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.ElementModel
{
    [TestClass]
    public class PocoTypedElementTests
    {
        [TestMethod]
        public void PocoTypedElementPocoRoundtrip()
        {
            var patient = new Patient();
            var actual = patient.ToTypedElement().ToPoco<Patient>();
            Assert.IsNotNull(actual, "Roundtrip POCO -> ITypedElement -> POCO should succeed.");
        }

        /// <summary>
        /// This tests will prove that FhirPath PocoElement node value extraction throws cast exceptions when the item does not have a value 
        /// (such as when only having an extension on the property)
        /// See also PR 829
        /// </summary>
        [TestMethod]
        public void EmptyValueShouldNotThrowExceptions()
        {
            var patient = new Patient();

            patient.ActiveElement = new FhirBoolean(null);
            patient.ActiveElement.AddExtension("http://example.com/myExtension", new FhirBoolean(false));
            var actual = patient.ToTypedElement();

            var prio = actual.Scalar("active");
            Assert.IsNull(prio);
        }

        [TestMethod]
        public void PocoExtensionTest()
        {
            Patient p = new Patient
            {
                Active = true
            };
            p.ActiveElement.ElementId = "314";
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            p.ActiveElement.AddExtension("http://something.org", new Integer(314));

            Assert.AreEqual(true, p.Scalar("Patient.active.first()"));
            Assert.AreEqual(true, p.Scalar("Patient.active[0]"));
            Assert.AreEqual("314", p.Scalar("Patient.active[0].id[0]"));

            var extensions = p.Select("Patient.active[0].extension");
            Assert.AreEqual(2, extensions.Count());

            var boolValue = p.Select("Patient.active");
            Assert.IsTrue(boolValue.All(bv => bv is FhirBoolean));
        }

        [TestMethod]
        public void TestFpFunctions()
        {
            // FHIR specific function exists on POCO
            var fhirData = new FhirString("hello!");
            Assert.IsTrue(fhirData.IsTrue("hasValue()"));

            // FHIR specific function does not work for ITypedElement extension methods
            var data = ElementNode.ForPrimitive("hello!");
            Assert.ThrowsException<ArgumentException>(() => data.IsTrue("hasValue()"));
        }

        [TestMethod]
        public void PocoHasValueTest()
        {
            Patient p = new Patient();

            Assert.AreEqual(false, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(false, p.Predicate("Patient.active.exists()"));

            p.Active = true;
            Assert.AreEqual(true, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));

            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            Assert.AreEqual(true, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));

            p.ActiveElement = new FhirBoolean();
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            Assert.AreEqual(false, p.Predicate("Patient.active.hasValue()"));
            Assert.AreEqual(true, p.Predicate("Patient.active.exists()"));
        }

        [TestMethod]
        public async Tasks.Task CompareToOtherElementNavigator()
        {
            var json = TestDataHelper.ReadTestData("TestPatient.json");
            var xml = TestDataHelper.ReadTestData("TestPatient.xml");

            var poco = await (new FhirJsonParser()).ParseAsync<Patient>(json);
            var pocoP = poco.ToTypedElement();
            var jsonP = (await FhirJsonNode.ParseAsync(json, settings: new FhirJsonParsingSettings { AllowJsonComments = true }))
                .ToTypedElement(new PocoStructureDefinitionSummaryProvider());
            var xmlP = (await FhirXmlNode.ParseAsync(xml)).ToTypedElement(new PocoStructureDefinitionSummaryProvider());

            doCompare(pocoP, jsonP, "poco<->json");
            doCompare(pocoP, xmlP, "poco<->xml");

            void doCompare(ITypedElement one, ITypedElement two, string what)
            {
                var compare = one.IsEqualTo(two);

                if (compare.Success == false)
                {
                    Debug.WriteLine($"{what}: Difference in {compare.Details} at {compare.FailureLocation}");
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void IncorrectPathInTwoSuccessiveRepeatingMembers()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "issue-444-testdata.xml"));
            var cs = (new FhirXmlParser()).Parse<CapabilityStatement>(xml);
            var nav = cs.ToTypedElement();

            var rest = nav.Children().Where(c => c.Name == "rest").FirstOrDefault();

            Assert.IsNotNull(rest);

            Assert.IsTrue(rest.Location.Contains("CapabilityStatement.rest[0]"));
        }


        [TestMethod]
        public void PocoTypedElementPerformance()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var cs = (new FhirXmlParser()).Parse<Patient>(xml);
            var nav = cs.ToTypedElement();

            TypedElementPerformance(nav);
        }

        private static void TypedElementPerformance(ITypedElement nav)
        {
            // run extraction once to allow for caching
            extract();

            //System.Threading.Thread.Sleep(20000);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 5_000; i++)
            {
                extract();
            }
            sw.Stop();

            Debug.WriteLine($"Navigating took {sw.ElapsedMilliseconds / 5} micros");

            void extract()
            {
                var usual = nav.Children("identifier").First().Children("use").First().Value;
                var phone = nav.Children("telecom").First().Children("system").First().Value;
                var prefs = nav.Children("communication").Where(c => c.Children("preferred").Any(pr => pr.Value is string s && s == "true")).Count();
                var link = nav.Children("link").Children("other").Children("reference");
            }
        }

        [TestMethod]
        public async Tasks.Task ValidateFiveWs()
        {
            var json = TestDataHelper.ReadTestData("test-observation.json");
            var poco = await (new FhirJsonParser()).ParseAsync<Observation>(json);

            var inspector = ModelInfo.ModelInspector;
            var aResourceMapping = inspector.FindClassMapping(typeof(Observation));
            var fiveWsProp = aResourceMapping.PropertyMappings.Where(p => p.FiveWs != null && p.FiveWs.StartsWith("FiveWs.subject")).FirstOrDefault();
            fiveWsProp.Should().NotBeNull("There should be a fiveW mapping for subject");

            var subjectProp = fiveWsProp.GetValue(poco) as ResourceReference;

            Assert.AreEqual(poco.Subject, subjectProp);
        }


        [TestMethod]
        public void CheckTypeOfElementDefinitionMembers()
        {
#if !R5
            var resultType = "string";
#else
            var resultType = "markdown";
#endif
            var ed = new ElementDefinition()
            {
                Mapping = new System.Collections.Generic.List<ElementDefinition.MappingComponent>() {
                            new ElementDefinition.MappingComponent() {
                                Comment = "comment"
                                }
                            },
                Binding = new ElementDefinition.ElementDefinitionBindingComponent()
                {
                    Description = "description",

                },
                Constraint = new System.Collections.Generic.List<ElementDefinition.ConstraintComponent>()
                {
                    new ElementDefinition.ConstraintComponent()
                    {
                        Requirements = "requirements"
                    }
                }
            };
            var element = ed.ToTypedElement();
            element.Select("mapping.comment").First().InstanceType.Should().Be(resultType);
            element.Select("binding.description").First().InstanceType.Should().Be(resultType);
            element.Select("constraint.requirements").First().InstanceType.Should().Be(resultType);
        }

    }
}
