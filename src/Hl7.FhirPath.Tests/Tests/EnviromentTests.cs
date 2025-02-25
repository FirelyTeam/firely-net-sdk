using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace HL7.FhirPath.Tests.Tests;

[TestClass]
public class EnviromentTests
{
    [TestMethod]
    public void TestEnvironment()
    {
        var compiler = new FhirPathCompiler();
        var expr = compiler.Compile("%var = 1");
        
        expr.IsTrue(PocoNode.ForAnyPrimitive(true), new EvaluationContext { Environment = new Dictionary<string, IEnumerable<PocoNode>> {{ "var", PocoNode.ForPrimitive<Integer>(1) }}} ).Should().BeTrue();
        expr.IsTrue(PocoNode.ForAnyPrimitive(true), new EvaluationContext { Environment = new Dictionary<string, IEnumerable<PocoNode>> {{ "var", PocoNode.ForPrimitive<Integer>(2)}}} ).Should().BeFalse();
    }
}