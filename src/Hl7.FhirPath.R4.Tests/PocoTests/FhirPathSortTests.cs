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
    }
}