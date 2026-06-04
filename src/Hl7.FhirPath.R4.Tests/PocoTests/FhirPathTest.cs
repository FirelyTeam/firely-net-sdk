/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Vonk.FhirPath.R4.Tests;
using P = Hl7.Fhir.ElementModel.Types;
using ScopedNode = Hl7.Fhir.ElementModel.ScopedNode;

namespace Hl7.Fhir.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathTest
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestFhirPathPolymporphism()
        {
            var patient = new Patient() { Active = false };
            patient.Meta = new Meta() { LastUpdated = new DateTimeOffset(2018, 5, 24, 14, 48, 0, TimeSpan.Zero) };
            var nav = patient.ToTypedElement();

            var result = nav.Select("Resource.meta.lastUpdated");
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(P.DateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
        }

        [TestMethod]
        public void TestFhirPathTrace()
        {
            var patient = new Hl7.Fhir.Model.Patient() { Id = "pat45", Active = false };
            patient.Meta = new Meta() { LastUpdated = new DateTimeOffset(2018, 5, 24, 14, 48, 0, TimeSpan.Zero) };
            var nav = patient.ToTypedElement();

            EvaluationContext ctx = new FhirEvaluationContext();
            var result = nav.Select("Resource.meta.trace('log').lastUpdated", ctx);
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(P.DateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);

            bool traced = false;
            ctx.Tracer = (string name, System.Collections.Generic.IEnumerable<PocoNode> results) =>
            {
                System.Diagnostics.Trace.WriteLine($"{name}");
                Assert.AreEqual("log", name);
                foreach (ITypedElement item in results)
                {
                    var fhirValue = item.Annotation<IFhirValueProvider>();
                    System.Diagnostics.Trace.WriteLine($"--({fhirValue.FhirValue.GetType().Name}): {item.Value} {fhirValue.FhirValue}");
                    Assert.AreEqual(patient.Meta, fhirValue.FhirValue);
                    traced = true;
                }
            };
            result = nav.Select("Resource.meta.trace('log').lastUpdated", ctx);
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(P.DateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
            Assert.IsTrue(traced);

            traced = false;
            ctx.Tracer = (string name, System.Collections.Generic.IEnumerable<PocoNode> results) =>
            {
                System.Diagnostics.Trace.WriteLine($"{name}");
                Assert.IsTrue(name == "id" || name == "log");
                foreach (ITypedElement item in results)
                {
                    var fhirValue = item.Annotation<IFhirValueProvider>();
                    System.Diagnostics.Trace.WriteLine($"--({fhirValue.FhirValue.GetType().Name}): {item.Value} {fhirValue.FhirValue}");
                    traced = true;
                }
            };
            result = nav.Select("Resource.trace('id', id).meta.trace('log', lastUpdated).lastUpdated", ctx);
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(P.DateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
            Assert.IsTrue(traced);
        }

        [TestMethod]
        public void TestFhirPathCombine2()
        {
            var cs = new Hl7.Fhir.Model.CodeSystem() { Id = "pat45" };
            cs.Concept.Add(new CodeSystem.ConceptDefinitionComponent()
            {
                Code = "5",
                Display = "Five"
            });
            var nav = cs.ToTypedElement();

            EvaluationContext ctx = new EvaluationContext();
            var result = nav.Predicate("concept.code.combine($this.descendants().concept.code).isDistinct()", ctx);
            Assert.IsTrue(result);

            cs.Concept.Add(new CodeSystem.ConceptDefinitionComponent()
            {
                Code = "5",
                Display = "Five"
            });
            result = nav.Predicate("concept.code.combine($this.descendants().concept.code).isDistinct()", ctx);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFhirPathCombine()
        {
            var patient = new Patient();

            patient.Identifier.Add(new Identifier("http://nu.nl", "id1"));
            patient.Identifier.Add(new Identifier("http://nu.nl", "id2"));

            var result = patient.Predicate("identifier[0].value.combine($this.identifier[1].value).isDistinct()");
            Assert.IsTrue(result);

            result = patient.Predicate("identifier[0].value.combine($this.identifier[0].value).isDistinct()");
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestImplicitQuantityCast()
        {
            var obs = new Observation { Value = new Hl7.Fhir.Model.Quantity(75m, "kg") };
            Assert.IsTrue(obs.Predicate("Observation.value > 74 'kg'"));
            Assert.IsTrue(obs.Predicate("Observation.value = 75 'kg'"));
            Assert.IsTrue(obs.Predicate("Observation.value ~ 75 'kg'"));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void TestFhirPathScalarInParallel()
        {
            var patient = new Patient();

            patient.Identifier.Add(new Identifier("http://nu.nl", "id1"));

            var element = patient.ToTypedElement();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var actionBlock = new ActionBlock<string>(
                 (input =>
                 {
                     element.Select(input);
                 }),
                 new ExecutionDataflowBlockOptions()
                 {
                     MaxDegreeOfParallelism = 20
                 });


            for (int i = 0; i < 20_000; i++)
            {
                actionBlock.Post($"identifier[{i}]");
            }
            actionBlock.Complete();
            actionBlock.Completion.Wait();

            stopwatch.Stop();

            TestContext.WriteLine($"Select in 20.000 : {stopwatch.ElapsedMilliseconds}ms");
        }

        //[TestMethod]
        //public void TypeInfoAndNativeMatching()
        //{
        //    Assert.True(TypeInfo.Decimal.MapsToNative(typeof(decimal)));
        //    Assert.False(TypeInfo.Decimal.MapsToNative(typeof(long)));
        //    Assert.False(TypeInfo.Any.CanBeCastTo(typeof(long)));
        //}



        [TestMethod]
        [DataRow("entry.first().resource.contained", "contained-1", "patient-1")]
        [DataRow("entry.first().resource.contained.id", "contained-1", "patient-1")]
        [DataRow("entry.first().resource.id", "patient-1", "patient-1")]
        [DataRow("entry.first().resource", "patient-1", "patient-1")]
        [DataRow("Bundle", "bundle-1", "bundle-1")]
        [DataRow("id", "bundle-1", "bundle-1")]
        public void TestFhirPathRootResource(string expression, string resource, string rootResource)
        {
            var bundle = new Bundle() { Type = Bundle.BundleType.Collection, Id = "bundle-1" };
            var patient = new Patient() { Id = "patient-1" };
            var containedPat = new Patient() { Id = "contained-1" };
            patient.Contained.Add(containedPat);
            patient.Link.Add(new Patient.LinkComponent() { Other = new ResourceReference("#contained-1"), Type = Patient.LinkType.Seealso });
            bundle.AddResourceEntry(patient, "http://example.com/patient1");

            var patBundle = new ScopedNode(bundle.ToTypedElement());

            var node = patBundle.Select(expression).FirstOrDefault()!;
            node.Scalar("%resource.id").Should().Be(resource);
            node.Scalar("%rootResource.id").Should().Be(rootResource);
        }

        [TestMethod]
        public void TestBackTick()
        {
            var patient = new Patient() { Text = new Narrative() { Div = "<p>test</p>" } };

            var nav = patient.ToTypedElement();

            var divExists = nav.Scalar("text.`div`.exists()");

            Assert.IsTrue((bool?)divExists);
        }

        [TestMethod]
        public void TestFhirPathInBundle()
        {
            var bundle = new Bundle();
            bundle.AddResourceEntry(new Appointment { Id = "1" }, "http://some.org/Appointment/1");

            var nav = bundle.ToTypedElement().Select("Bundle.entry[0].resource").FirstOrDefault();

            var absolutueInvariantcheck = nav.Scalar("Appointment.cancelationReason.exists() implies(Appointment.status = 'no-show' or Appointment.status = 'cancelled')");
            Assert.IsTrue((bool?)absolutueInvariantcheck);

            var invariantcheck = nav.Scalar("cancelationReason.exists() implies(status = 'no-show' or status = 'cancelled')");
            Assert.IsTrue((bool?)invariantcheck);

        }

        public static IEnumerable<object[]> CastAsDataTypeTestCases() =>
           new (PrimitiveType input, string castToDataType)[]
               {
                   (new FhirBoolean(true), "boolean"),
                   (new FhirDecimal(12), "decimal"),
                   (new FhirString("a string"), "string"),

                   (new Date("2021-07-01"), "date"),
                   (new Time("16:01:12"), "time"),
                   (FhirDateTime.Now(), "dateTime"),
               }.Select(t => new object[] { t.input, t.castToDataType });

        [TestMethod]
        [DynamicData(nameof(CastAsDataTypeTestCases))]
        public void AssertCastAs(PrimitiveType input, string castToDataType)
        {
            var observation = new Observation { Value = input };
            var result = observation.Select($"Observation.value as {castToDataType}");

            var dataType = result?.FirstOrDefault();
            Assert.IsNotNull(dataType, "Select should result an instance");
            Assert.IsInstanceOfType(dataType, input.GetType(), "dataType should be the same type of the input");
        }

        [TestMethod]
        public void TestSelectDifferentQuantities()
        {
            var obs = new Observation()
            {
                Id = "test",
                Status = ObservationStatus.Final,
                Code = new CodeableConcept() { Text = "test" },
                Value = new Quantity() { Value = 1, Code = "%", System = "http://unitsofmeasure.org" },
                Component = new List<Observation.ComponentComponent>()
                {
                    new Observation.ComponentComponent()
                    {
                        Code = new CodeableConcept() { Text = "test"},
                        Value = new Quantity()
                        {
                            Value = 1,
                            Code = "L/min",
                            System = "http://unitsofmeasure.org"
                        }
                    }
                }
            };

            var typedElement = obs.ToTypedElement();
            var result = typedElement.Select("(Observation.value as Quantity) | (Observation.component.value as Quantity)");
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestFhirPathResolve()
        {
            var bundle = new Bundle
            {
                Entry = new List<Bundle.EntryComponent>
        {
            new Bundle.EntryComponent
            {
                FullUrl = $"urn:uuid:123456",
                Resource = new Organization
                {
                    Id = "123456"
                }
            },
            new Bundle.EntryComponent
            {
                FullUrl = $"urn:uuid:555",
                Resource = new Patient
                {
                    ManagingOrganization = new ResourceReference($"urn:uuid:123456")
                }
            }
        }
            };

            var result = bundle.Select("Bundle.entry.where(fullUrl = 'urn:uuid:555').resource.managingOrganization.resolve()");
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        [DynamicData(nameof(MemberOfTestData))]
        public void MemberOfTests(Base poco, string expression, bool? expectedResult)
        {
            var context = new FhirEvaluationContext { TerminologyService = new LocalTerminologyService(resolver: ZipSource.CreateValidationSource()) };

            var result = poco.Scalar(expression, context);

            result.Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> MemberOfTestData()
        {
            // memberOf with single Code objects
            yield return new object[] { new Code("85353-1"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };
            yield return new object[] { new Code("unknown"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", false };
            yield return new object[] { new Code("male"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new Code("female"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new Code("no-idea"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", false };

            // memberOf with single Coding objects
            yield return new object[] { new Coding("http://loinc.org", "85353-1"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };
            yield return new object[] { new Coding("http://unknown.system", "85353-1"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", false };
            yield return new object[] { new Coding("http://loinc.org", "unknown"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", false };

            // memberOf with single CodeableConcept objects
            yield return new object[] { new CodeableConcept("http://loinc.org", "85353-1"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };
            yield return new object[] { new CodeableConcept()
                                            {
                                                Coding = new List<Coding>() {
                                                    new Coding("http://loinc.org", "85353-1"),
                                                    new Coding("http://unknown.system", "a-code")
                                                }
                                            }, "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };

            // memberOf with string objects
            yield return new object[] { new FhirString("85353-1"), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };
            yield return new object[] { new FhirString("male"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new FhirString("female"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new FhirString("no-idea"), "memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", false };

            // memberOf with inline string objects
            yield return new object[] { new FhirBoolean(), "'85353-1'.memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", true };
            yield return new object[] { new FhirBoolean(), "'male'.memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new FhirBoolean(), "'female'.memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", true };
            yield return new object[] { new FhirBoolean(), "'no-idea'.memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", false };

            // memberOf with illegal input datatype
            yield return new object[] { new FhirDecimal(45.1m), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", null };
            yield return new object[] { FhirDateTime.Now(), "memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult')", null };

            // memberOf with unknown valueset
            yield return new object[] { new Code("85353-1"), "memberOf('http://example.com/ValueSet/unknown')", null };
        }

        [TestMethod]
        public void MemberOfTestsWithoutTerminologyService()
        {
            var context = new FhirEvaluationContext();

            Action action = () => new Code("male").Scalar("memberOf('http://hl7.org/fhir/ValueSet/administrative-gender')", context);

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void MemberOfTestWithExampleFromSpecification()
        {
            var context = new FhirEvaluationContext { TerminologyService = new LocalTerminologyService(resolver: ZipSource.CreateValidationSource()) };

            Observation observation = new()
            {
                Component = new()
                {
                    new()
                    {
                        Code = new CodeableConcept("http://loinc.org", "2708-6")
                    },
                    new ()
                    {
                        Code = new CodeableConcept("http://example.com/other-system", "a-code")
                    }
                }
            };

            var result = observation.Select("component.where(code.memberOf('http://hl7.org/fhir/ValueSet/observation-vitalsignresult'))", context);

            result.Should().ContainSingle().Subject
                .Should().BeOfType<Observation.ComponentComponent>()
                .Subject.Code.IsExactly(new CodeableConcept("http://loinc.org", "2708-6")).Should().BeTrue();
        }
        
                
        [TestMethod]
        public void PersistRootOfTypedElement_PocoBase()
        {
            var res = SourceNode.Resource("Bundle", "Bundle",
            SourceNode.Node("link", 
            SourceNode.Valued("url", "test")
            )
            );
            
            var expr = ((ITypedElement)res.ToPoco(ModelInspector.Base).ToPocoNode()).Children("link").First();
            var loc = expr.Select("url").Select(x => x.Location).Single();
            loc.Should().Be("Bundle.link[0].url[0]");
        }
        
        [TestMethod]
        public void PersistRootOfTypedElement_NoTypeInfo()
        {
            var res = SourceNode.Resource("Bundle", "Bundle",
            SourceNode.Node("link", 
            SourceNode.Valued("url", "test")
            )
            );
            
 #pragma warning disable CS0618 // Type or member is obsolete
            var expr = res.ToTypedElementLegacy().Children("link").First();
 #pragma warning restore CS0618 // Type or member is obsolete
            var loc = expr.Select("url").Select(x => x.Location).Single();
            loc.Should().Be("Bundle.link[0].url[0]");
        }
        
        [TestMethod]
        public void PersistRootOfTypedElement_StructuredInformationProvider()
        {
            var res = SourceNode.Resource("Bundle", "Bundle",
            SourceNode.Node("link", 
            SourceNode.Valued("url", "test")
            )
            );

            var expr = res.ToTypedElement(ModelInspector.Base).Children("link").First();
            var loc = expr.Select("url").Select(x => x.Location).Single();
            loc.Should().Be("Bundle.link[0].url[0]");
        }
    }
}