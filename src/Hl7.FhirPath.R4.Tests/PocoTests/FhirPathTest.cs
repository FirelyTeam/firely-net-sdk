/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.Primitives;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Hl7.FhirPath.R4.Tests
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
        public void ConvertToBoolean()
        {
            var areTrue = ElementNode.CreateList(true, "TruE", "Yes", "y", "t", "1", "1.0", 1L, 1m, 1.0m).ToList();
            areTrue.ForEach(o => Assert.IsTrue(o.ToBoolean().Value));
            areTrue.ForEach(o => Assert.IsTrue(o.ConvertsToBoolean()));

            var areFalse = ElementNode.CreateList(false, "fAlse", "nO", "N", "f", "0", "0.0", 0L, 0m, 0.0m).ToList();
            areFalse.ForEach(o => Assert.IsFalse(o.ToBoolean().Value));
            areFalse.ForEach(o => Assert.IsTrue(o.ConvertsToBoolean()));

            var wrong = ElementNode.CreateList("truex", "falsx", "not", "tr", -1L, 2L, 2.0m, 1.1m).ToList();
            wrong.ForEach(o => Assert.IsNull(o.ToBoolean()));
            wrong.ForEach(o => Assert.IsFalse(o.ConvertsToBoolean()));
        }

        [TestMethod]
        public void ConvertToInteger()
        {
            var inputs = ElementNode.CreateList(1L, "2", -4L, "-5", "+4", true, false);
            var vals = new[] { 1L, 2L, -4L, -5L, 4L, 1L, 0L };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToInteger(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToInteger()));

            var wrong = ElementNode.CreateList("2.4", "++6", "2,6", "no", "false", DateTimeOffset.Now).ToList();
            wrong.ForEach(c => Assert.IsNull(c.ToInteger()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToInteger()));
        }


        [TestMethod]
        public void ConvertToDecimal()
        {
            var inputs = ElementNode.CreateList(1L, 2m, "2", "3.14", -4.4m, true, false);
            var vals = new[] { 1m, 2m, 2m, 3.14m, -4.4m, 1m, 0m };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToDecimal(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToDecimal()));

            var wrong = ElementNode.CreateList("hi", "++6", "2,6", "no", "false", DateTimeOffset.Now).ToList();
            wrong.ForEach(c => Assert.IsNull(c.ToDecimal()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToDecimal()));
        }

        [TestMethod]
        public void ConvertToDateTime()
        {
            var now = PartialDateTime.Parse("2019-01-11T15:47:00+01:00");
            var inputs = ElementNode.CreateList(new DateTimeOffset(2019, 1, 11, 15, 47, 00, new TimeSpan(1, 0, 0)),
                                "2019-01", "2019-01-11T15:47:00+01:00");
            var vals = new[] { now, PartialDateTime.Parse("2019-01"), now };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToDateTime(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToDateTime()));

            var wrong = ElementNode.CreateList("hi", 2.6m, false, PartialTime.Parse("16:05:49")).ToList();
            wrong.ForEach(c => Assert.IsNull(c.ToDateTime()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToDateTime()));
        }


        [TestMethod]
        public void ConvertToTime()
        {
            var now = PartialTime.Parse("15:47:00+01:00");
            var inputs = ElementNode.CreateList(now, "T12:05:45");
            var vals = new[] { now, PartialTime.Parse("12:05:45") };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToTime(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToTime()));

            var wrong = ElementNode.CreateList(new DateTimeOffset(2019, 1, 11, 15, 47, 00, new TimeSpan(1, 0, 0)),
                "hi", 2.6m, false).ToList();
            wrong.ForEach(c => Assert.IsNull(c.ToTime()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToTime()));
        }

        [TestMethod]
        public void ConvertToString()
        {
            var inputs = ElementNode.CreateList("hoi", 4L, 3.4m, true, false, PartialTime.Parse("15:47:00+01:00"),
                PartialDateTime.Parse("2019-01-11T15:47:00+01:00"));
            var vals = new[] { "hoi", "4", "3.4", "true", "false", "15:47:00+01:00", "2019-01-11T15:47:00+01:00" };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.v, c.i.ToString()));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToString()));

            var wrong = new[] { HumanName.ForFamily("Kramer").WithGiven("Ewout").ToTypedElement() }.ToList();
            wrong.ForEach(c => Assert.IsNull(c.ToStringRepresentation()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToString()));
        }

        [TestMethod]
        public void CheckTypeDetermination()
        {
            var values = ElementNode.CreateList(1, true, "hi", 4.0m, 4.0f, PartialDateTime.Now());


            Test.IsInstanceOfType(values.Item(0).Single().Value, typeof(Int64));
            Test.IsInstanceOfType(values.Item(1).Single().Value, typeof(Boolean));
            Test.IsInstanceOfType(values.Item(2).Single().Value, typeof(String));
            Test.IsInstanceOfType(values.Item(3).Single().Value, typeof(Decimal));
            Test.IsInstanceOfType(values.Item(4).Single().Value, typeof(Decimal));
            Test.IsInstanceOfType(values.Item(5).Single().Value, typeof(PartialDateTime));
        }


        [TestMethod]
        public void TestItemSelection()
        {
            var values = ElementNode.CreateList(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual((Int64)1, values.Item(0).Single().Value);
            Assert.AreEqual((Int64)3, values.Item(2).Single().Value);
            Assert.AreEqual((Int64)1, values.First().Value);
            Assert.IsFalse(values.Item(100).Any());
        }

        [TestMethod]
        public void TypeInfoEquality()
        {
            Assert.AreEqual(TypeInfo.Boolean, TypeInfo.Boolean);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreNotEqual(TypeInfo.Boolean, TypeInfo.String);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreEqual(TypeInfo.ByName("something"), TypeInfo.ByName("something"));
            Assert.AreNotEqual(TypeInfo.ByName("something"), TypeInfo.ByName("somethingElse"));
            Assert.IsTrue(TypeInfo.ByName("something") == TypeInfo.ByName("something"));
            Assert.IsTrue(TypeInfo.ByName("something") != TypeInfo.ByName("somethingElse"));
        }

        [TestMethod]
        public void TestFhirPathPolymporphism()
        {
            var patient = new Patient() { Active = false };
            patient.Meta = new Meta() { LastUpdated = new DateTimeOffset(2018, 5, 24, 14, 48, 0, TimeSpan.Zero) };
            var nav = patient.ToTypedElement();

            var result = nav.Select("Resource.meta.lastUpdated");
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(PartialDateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
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
            Assert.AreEqual(PartialDateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);

            bool traced = false;
            ctx.Tracer = (string name, System.Collections.Generic.IEnumerable<ITypedElement> results) =>
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
            Assert.AreEqual(PartialDateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
            Assert.IsTrue(traced);

            traced = false;
            ctx.Tracer = (string name, System.Collections.Generic.IEnumerable<ITypedElement> results) =>
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
            Assert.AreEqual(PartialDateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
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

    }
}