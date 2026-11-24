/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Analysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathAnalyzerTests
    {
        private static readonly FhirPathAnalyzer _analyzer = new(ModelInfo.ModelInspector);

        private static FhirPathAnalysisResult analyze(string expression, string inputTypes = null, bool checkOrdered = false)
        {
            var types = inputTypes?.Split('|');
            return _analyzer.Analyze(expression, new FhirPathAnalysisContext
            {
                InputTypes = types,
                ResourceTypes = types,
                CheckOrderedFunctions = checkOrdered,
            });
        }

        private static void valid(string expression, string inputTypes = null, bool checkOrdered = false)
        {
            var result = analyze(expression, inputTypes, checkOrdered);
            result.Diagnostics.Should().BeEmpty($"'{expression}' on {inputTypes ?? "(unknown)"} is valid");
        }

        private static void invalid(string expression, string inputTypes, string code, bool checkOrdered = false)
        {
            var result = analyze(expression, inputTypes, checkOrdered);
            result.Diagnostics.Select(d => d.Code).Should().Contain(code, $"'{expression}' on {inputTypes ?? "(unknown)"} is invalid");
        }

        [TestMethod]
        public void ChecksPaths()
        {
            valid("Patient.name.given", "Patient");
            valid("name.given", "Patient");
            valid("Patient.contact.name.family", "Patient");    // backbone elements
            valid("Patient.name.given.extension.url", "Patient");
            valid("Patient.active.value", "Patient");

            invalid("name.given1", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Patient.name.given1", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Patient.contact.nam", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Encounter.name", "Patient", FhirPathDiagnostic.WRONG_TYPE_NAME);
        }

        [TestMethod]
        public void ChecksChoiceElements()
        {
            valid("Observation.value.unit", "Observation");     // Quantity has a unit
            valid("Observation.value.ofType(Quantity).unit", "Observation");
            valid("(Observation.value as Quantity).unit", "Observation");
            valid("Observation.value.as(Quantity).unit", "Observation");

            invalid("Observation.valueQuantity.unit", "Observation", FhirPathDiagnostic.CHOICE_NAME);
            invalid("(Observation.value as Period).unit", "Observation", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Observation.value.ofType(Period).unit", "Observation", FhirPathDiagnostic.UNKNOWN_ELEMENT);
        }

        [TestMethod]
        public void UnknownTypesNeverCauseErrors()
        {
            valid("name.given1");                                       // unknown input
            valid("%unknownVariable.whatever");
            valid("Bundle.entry.resource.whatever", "Bundle");         // Resource is abstract
            valid("Patient.contained.whatever", "Patient");
            valid("Patient.extension.value.unit", "Patient");          // open type: any data type
            valid("Patient.descendants().whatever", "Patient");
            valid("Patient.unknownFunction().whatever", "Patient");
            valid("Bundle.entry.resource.ofType(Patient).name.given", "Bundle");
            invalid("Bundle.entry.resource.ofType(Patient).name.given1", "Bundle", FhirPathDiagnostic.UNKNOWN_ELEMENT);
        }

        [TestMethod]
        public void ChecksInsideIteratingFunctions()
        {
            valid("Patient.name.where(given = 'x')", "Patient");
            valid("Patient.name.select(given)", "Patient");
            valid("Patient.name.all($this.given.exists())", "Patient");
            invalid("Patient.name.where(given1 = 'x')", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Patient.name.exists(given1)", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
            invalid("Patient.name.select(given).nam", "Patient", FhirPathDiagnostic.UNKNOWN_ELEMENT);
        }

        [TestMethod]
        public void UnreachableCodeIsNotAnError()
        {
            valid("Observation.value.ofType(Period).ofType(Quantity).unit", "Observation");
            valid("$this.ofType(dateTime).toString()", "Period");
            valid("{}.whatever");
        }

        [TestMethod]
        public void ChecksToString()
        {
            valid("$this.toString()", "dateTime");
            valid("$this.toString()", "Quantity");
            valid("$this.toString()", "dateTime|Period");   // only an error if none of the types allow it
            valid("'a'.toString()");
            invalid("$this.toString()", "Period", FhirPathDiagnostic.INVALID_FOCUS_TYPE);
            invalid("$this.toString()", "Period|Timing", FhirPathDiagnostic.INVALID_FOCUS_TYPE);
        }

        [TestMethod]
        public void ChecksIifCriterion()
        {
            valid("iif(true, 1, 2)");
            valid("iif(Patient.active, 1, 2)", "Patient");
            valid("iif({}, 1, 2)");
            valid("iif(%unknown, 1, 2)");
            invalid("iif('not a boolean', 1, 2)", null, FhirPathDiagnostic.INVALID_ARGUMENT_TYPE);
            invalid("iif(Patient.name, 1, 2)", "Patient", FhirPathDiagnostic.INVALID_ARGUMENT_TYPE);
        }

        [TestMethod]
        public void ChecksOrderedFunctionsWhenAsked()
        {
            valid("Patient.children().skip(1)", "Patient");     // not checked by default
            valid("Patient.name.skip(1)", "Patient", checkOrdered: true);
            valid("Patient.children().where(true).count()", "Patient", checkOrdered: true);
            invalid("Patient.children().skip(1)", "Patient", FhirPathDiagnostic.UNORDERED_COLLECTION, checkOrdered: true);
            invalid("Patient.descendants().first()", "Patient", FhirPathDiagnostic.UNORDERED_COLLECTION, checkOrdered: true);
            invalid("Patient.children()[0]", "Patient", FhirPathDiagnostic.UNORDERED_COLLECTION, checkOrdered: true);
        }

        [TestMethod]
        // guards that make the guarded operand unreachable, or leave a type that allows toString()
        [DataRow("$this is dateTime implies $this.toString().length() > 1", "Period|Timing")]
        [DataRow("$this.is(dateTime) implies $this.toString().length() > 1", "Period")]
        [DataRow("($this is dateTime or $this is instant) implies $this.toString().length() > 1", "Period")]
        [DataRow("($this is dateTime and %resource.exists()) implies $this.toString().length() > 1", "Period")]
        [DataRow("$this is Period or $this.toString().length() > 1", "Period")]
        [DataRow("$this is Period or $this is Timing or $this.toString().length() > 1", "Period|Timing")]
        [DataRow("($this is Period).not() implies $this.toString().length() > 1", "Period")]
        [DataRow("iif($this is dateTime, $this.toString().length() > 1, true)", "Period")]
        [DataRow("iif($this is Period, true, $this.toString().length() > 1)", "Period")]
        [DataRow("$this is Period implies ($this is dateTime implies $this.toString().length() > 1)", "Period|Timing")]
        [DataRow("($this as dateTime).toString().length() > 1", "Period")]
        [DataRow("$this is string or $this.whatever.exists()", "code")]  // code is a string
        public void TypeGuardsNarrowThis(string expression, string inputTypes) => valid(expression, inputTypes);

        [TestMethod]
        [DataRow("$this is Period implies $this.toString().length() > 1", "Period|Timing")]
        [DataRow("$this is Period and $this.toString().length() > 1", "Period|Timing")]
        [DataRow("$this is Period or $this.toString().length() > 1", "Period|Timing")]
        [DataRow("$this is dateTime xor $this.toString().length() > 1", "Period")]        // xor does not guard
        [DataRow("($this is dateTime or %resource.exists()) implies $this.toString().length() > 1", "Period")]
        [DataRow("($this is Period).not() implies $this.toString().length() > 1", "Period|Timing")]
        [DataRow("iif($this is Period, $this.toString().length() > 1, true)", "Period")]
        [DataRow("$this is Element implies $this.toString().length() > 1", "Period")]    // supertype
        [DataRow("$this.start is dateTime implies $this.toString().length() > 1", "Period")]  // not a test on $this
        [DataRow("$this is dateTime implies %resource.name.where($this.toString() = 'x').exists()", "Patient")]  // new $this
        public void TypeGuardsDoNotOverNarrow(string expression, string inputTypes) =>
            invalid(expression, inputTypes, FhirPathDiagnostic.INVALID_FOCUS_TYPE);

        [TestMethod]
        public void ReportsResultTypesAndLocation()
        {
            analyze("Observation.effective", "Observation").ResultTypes.Should().BeEquivalentTo("dateTime", "Period", "Timing", "instant");
            analyze("Patient.name.given.count()", "Patient").ResultTypes.Should().BeEquivalentTo("System.Integer");
            analyze("Patient.contained", "Patient").ResultTypes.Should().BeEquivalentTo("Resource");
            analyze("%unknown", "Patient").ResultTypes.Should().BeNull();

            var diagnostic = analyze("Patient.name.given1", "Patient").Diagnostics.Single();
            diagnostic.Location.Should().NotBeNull();
            diagnostic.Location.LinePosition.Should().Be(14);
            diagnostic.Message.Should().Contain("given1");
        }

        [TestMethod]
        public void CompilingDoesNotAnalyze()
        {
            // Analysis is opt-in: compiling and evaluating an invalid path keeps working as before.
            var patient = new Patient();
            patient.Select("Patient.name.given1").Should().BeEmpty();
        }
    }
}
