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
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathSortTests
    {
        // Initialize the test context
        [TestInitialize]
        public void Initialize()
        {
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
        }


        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void TestFhirPathCoalesce1()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var p = new Patient
            {
                Id = "pat1",
                BirthDate = "1990-10-1",
                Active = true,
            };
            var expr = compiler.Compile("coalesce(id)");
            var result = expr(p.ToPocoNode(), new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("pat1", result.First().GetValue());
        }

        [TestMethod]
        public void TestFhirPathCoalesce2()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var p = new Patient
            {
                Id = "pat1",
                BirthDate = "1990-10-1",
                Active = true,
            };
            var expr = compiler.Compile("coalesce(name, id)");
            var result = expr(p.ToPocoNode(), new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("pat1", result.First().GetValue());
        }

        [TestMethod]
        public void TestFhirPathCoalesce3()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var p = new Patient
            {
                Id = "pat1",
                BirthDate = "1990-10-1",
                Active = true,
            };
            var expr = compiler.Compile("coalesce(name, telecom, {}, address, extension, 'five', id, birthDate)");
            var result = expr(p.ToPocoNode(), new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("five", result.ElementAt(0).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortNone()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(1|2|3)");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(3, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSort1()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(1|2|3).sort()");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(3, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSort2()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(3|2|1).sort()");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(3, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSort3()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(3|2|1).sort($this)");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(3, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortDescending1_numeric()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(1|2|3).sort($this desc)");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(3, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(1, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortDescending1_numericOddity()
        {
            // this isn't really using the official syntax,
            // however for numerics the unary - operator works the same as the 'desc' keyword
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("(1|2|3).sort(-$this)");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(3, result.ElementAt(0).GetValue());
            Assert.AreEqual(2, result.ElementAt(1).GetValue());
            Assert.AreEqual(1, result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortDescending2_alpha()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var exprString = "('a'|'b'|'c').sort($this desc)";
            var exprAST = compiler.Parse(exprString);
            var expr = compiler.Compile(exprString);
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("c", result.ElementAt(0).GetValue());
            Assert.AreEqual("b", result.ElementAt(1).GetValue());
            Assert.AreEqual("a", result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortAscending2_alpha()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("('b'|'a'|'c').sort($this asc)");
            var result = expr(null, new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("a", result.ElementAt(0).GetValue());
            Assert.AreEqual("b", result.ElementAt(1).GetValue());
            Assert.AreEqual("c", result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSort4()
        {
            var patient = new Patient() { Id = "pat1"};
            patient.Name.Add(new HumanName() { Family = "Smith", Given = new List<string>() { "Peter", "James" } });
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("Patient.name.given.sort()");
            var result = expr(patient.ToPocoNode(), new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("James", result.ElementAt(0).GetValue());
            Assert.AreEqual("Peter", result.ElementAt(1).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSort5()
        {
            var patient = new Patient() { Id = "pat1" };
            patient.Name.Add(new HumanName() { ElementId = "1", Family = "Smith", Given = new List<string>() { "Peter", "James" } });
            patient.Name.Add(new HumanName() { ElementId = "3", Family = "Pos", Given = new List<string>() { "Belinda" } });
            patient.Name.Add(new HumanName() { ElementId = "2", Family = "Pos", Given = new List<string>() { "Brian", "R" } });
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("Patient.name.sort(family, given.first()).id");
            var result = expr(patient.ToPocoNode(), new FhirEvaluationContext());

            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("3", result.ElementAt(0).GetValue());
            Assert.AreEqual("2", result.ElementAt(1).GetValue());
            Assert.AreEqual("1", result.ElementAt(2).GetValue());
        }

        [TestMethod]
        public void TestFhirPathSortDescendingMinus_alpha()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();
            var expr = compiler.Compile("('a'|'c'|'b').sort(-$this)");
            var result = expr(null, new FhirEvaluationContext());

            result.Select(r => r.GetValue()).Should().Equal("c", "b", "a");
        }

        [TestMethod]
        public void TestFhirPathSortDescendingMinus_multipleKeys()
        {
            var patient = new Patient() { Id = "pat1" };
            patient.Name.Add(new HumanName() { ElementId = "1", Family = "Smith", Given = new List<string>() { "Peter", "James" } });
            patient.Name.Add(new HumanName() { ElementId = "3", Family = "Pos", Given = new List<string>() { "Belinda" } });
            patient.Name.Add(new HumanName() { ElementId = "2", Family = "Pos", Given = new List<string>() { "Brian", "R" } });
            patient.Name.Add(new HumanName() { ElementId = "4", Given = new List<string>() { "Jim" } });
            FhirPathCompiler compiler = new FhirPathCompiler();

            // descending on family (empty first, like the conformance tests expect), then ascending on given
            var result = compiler.Compile("Patient.name.sort(-family, given.first()).id")(patient.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("4", "1", "3", "2");

            // descending on both
            result = compiler.Compile("Patient.name.sort(-family, -given.first()).id")(patient.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("4", "1", "2", "3");
        }

        [TestMethod]
        public void TestFhirPathSortDescendingMinus_quantity()
        {
            var obs1 = new Observation() { Id = "o1", Value = new Quantity(2m, "mg") };
            var obs2 = new Observation() { Id = "o2", Value = new Quantity(5m, "mg") };
            var obs3 = new Observation() { Id = "o3", Value = new Quantity(3m, "mg") };
            var bundle = new Bundle();
            foreach (var o in new[] { obs1, obs2, obs3 })
                bundle.Entry.Add(new Bundle.EntryComponent() { Resource = o });
            FhirPathCompiler compiler = new FhirPathCompiler();

            var result = compiler.Compile("Bundle.entry.resource.sort(-(value as Quantity)).id")(bundle.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("o2", "o3", "o1");

            result = compiler.Compile("Bundle.entry.resource.sort((value as Quantity) desc).id")(bundle.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("o2", "o3", "o1");

            // without a direction qualifier the key is sorted ascending, the same as asc
            result = compiler.Compile("Bundle.entry.resource.sort(value as Quantity).id")(bundle.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("o1", "o3", "o2");

            result = compiler.Compile("Bundle.entry.resource.sort((value as Quantity) asc).id")(bundle.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("o1", "o3", "o2");
        }

        [TestMethod]
        public void TestFhirPathSortDescending_unorderableKeyDoesNotThrow()
        {
            var patient = new Patient() { Id = "pat1" };
            patient.MaritalStatus = new CodeableConcept("http://example.org", "M");
            FhirPathCompiler compiler = new FhirPathCompiler();

            var result = compiler.Compile("Patient.sort(-maritalStatus.coding).id")(patient.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("pat1");
        }

        [TestMethod]
        public void TestFhirPathSortDescending_emptyKeysFirst()
        {
            var patient = new Patient() { Id = "pat1" };
            patient.Name.Add(new HumanName() { ElementId = "1", Family = "Smith" });
            patient.Name.Add(new HumanName() { ElementId = "2" });
            patient.Name.Add(new HumanName() { ElementId = "3", Family = "Pos" });
            FhirPathCompiler compiler = new FhirPathCompiler();

            // Pins current behaviour, which the prototype conformance test testSort10 relies on
            var result = compiler.Compile("Patient.name.sort(family desc).id")(patient.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("2", "1", "3");
            result = compiler.Compile("Patient.name.sort(-family).id")(patient.ToPocoNode(), new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal("2", "1", "3");
        }

        [TestMethod]
        public void TestFhirPathMinusOutsideSortKeyIsStillNegation()
        {
            FhirPathCompiler compiler = new FhirPathCompiler();

            // only a key selector that starts with '-' means descending, a nested '-' still negates the value
            var result = compiler.Compile("(1|2|3).sort(0 + -$this)")(null, new FhirEvaluationContext());
            result.Select(r => r.GetValue()).Should().Equal(3, 2, 1);
        }
    }
}
