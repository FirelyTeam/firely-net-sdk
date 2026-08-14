/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Tests.Model;

/// <summary>
/// Tests for building POCOs for types that have no compiled POCO - most notably logical models, which
/// are identified by their canonical url rather than by a type name.
/// </summary>
[TestClass]
public class LogicalModelToPocoTests
{
    private const string CANONICAL = "http://validationtest.org/fhir/StructureDefinition/SpikeRoot";

    [TestMethod]
    [DataRow(CANONICAL, DisplayName = "type identified by canonical url")]
    [DataRow("SpikeRoot", DisplayName = "type identified by name")]
    [DataRow("spikeRoot", DisplayName = "type identified by name starting with a lowercase letter")]
    public void ComplexRootIsNotBuiltAsAPrimitive(string typeName)
    {
        var root = buildRoot(typeName);

        // A logical model root is a complex type, so it should never end up as a DynamicPrimitive -
        // that would make it a primitive without a value, which FhirPath treats as an empty focus.
        root.Poco.Should().BeOfType<DynamicDataType>();
        root.Poco.TypeName.Should().Be(typeName);
    }

    [TestMethod]
    [DataRow(CANONICAL, DisplayName = "type identified by canonical url")]
    [DataRow("SpikeRoot", DisplayName = "type identified by name")]
    [DataRow("spikeRoot", DisplayName = "type identified by name starting with a lowercase letter")]
    public void CanEvaluateFhirPathOnDynamicallyBuiltPoco(string typeName)
    {
        var root = buildRoot(typeName);
        var compiler = new FhirPathCompiler();
        var ctx = new FhirEvaluationContext();

        evaluate("hook").Should().BeEquivalentTo(["order-sign"]);

        // Operators and functions that propagate empty (=, !=, is, toString()) must not see the root as
        // an empty focus
        evaluate("hook = 'order-sign'").Should().BeEquivalentTo([true]);
        evaluate("hook != 'order-sign'").Should().BeEquivalentTo([false]);
        evaluate("hook is string").Should().BeEquivalentTo([true]);
        evaluate("hook.toString() = 'order-sign'").Should().BeEquivalentTo([true]);
        compiler.Compile("hook = 'order-sign'").IsTrue(root, ctx).Should().BeTrue();

        List<object> evaluate(string expression) =>
            compiler.Compile(expression)(root, ctx).Select(node => ((ITypedElement)node).Value).ToList();
    }

    /// <summary>
    /// Covers how a type without a value in the instance is classified as primitive or complex, based on
    /// what its definition tells us. Note that the SDK's real summary providers filter the primitive
    /// "value" constraint out of <c>GetElements()</c>, so a value child proves a type is a primitive, but
    /// its absence does not prove the opposite.
    /// </summary>
    [TestMethod]
    // A value attribute in the definition is conclusive, whatever the type is called.
    [DataRow("myCode", "value@attr", true, DisplayName = "value attribute, primitive-looking name")]
    [DataRow("MyCode", "value@attr", true, DisplayName = "value attribute, complex-looking name")]
    // Without it, only the children a primitive is allowed to have leave the naming heuristic to decide.
    [DataRow("myCode", "extension", true, DisplayName = "no value attribute, primitive-looking name")]
    [DataRow("myCode", "id,extension", true, DisplayName = "no value attribute, only inherited children")]
    [DataRow("MyCode", "extension", false, DisplayName = "no value attribute, complex-looking name")]
    // A child a primitive cannot have means complex, even when the name suggests otherwise.
    [DataRow("myCode", "extension,hook", false, DisplayName = "child element beyond those of a primitive")]
    // Logical models are identified by canonical url, which is never a primitive type name.
    [DataRow(CANONICAL, "extension", false, DisplayName = "type identified by canonical url")]
    public void ClassifiesValueLessTypeAsPrimitiveOrComplex(string typeName, string children, bool expectPrimitive)
    {
        // Extension-only (so: value-less) instances of a primitive are legal, and this is the only content
        // all the type definitions under test have in common.
        var sourceNode = FhirJsonNode.Parse(
            """{ "extension": { "url": "http://example.org/ext", "valueString": "absent" } }""", typeName);
        var elements = children.Split(',')
            .Select(name => name.EndsWith("@attr")
                ? new ElementSummary("value", "System.String", XmlRepresentation.XmlAttr)
                : new ElementSummary(name, name == "extension" ? "Extension" : "string"))
            .ToArray<IElementDefinitionSummary>();
        var provider = new SingleTypeSummaryProvider(new ComplexSummary(typeName, elements));

        var root = sourceNode.ToTypedElement(provider, typeName).ToPocoNode();

        root.Poco.Should().BeOfType(expectPrimitive ? typeof(DynamicPrimitive) : typeof(DynamicDataType));
        root.Poco.TypeName.Should().Be(typeName);
    }

    private static PocoNode buildRoot(string typeName)
    {
        var sourceNode = FhirJsonNode.Parse("""{ "hook": "order-sign" }""", "SpikeRoot");
        return sourceNode.ToTypedElement(new TestSummaryProvider(typeName), typeName).ToPocoNode();
    }

    /// <summary>
    /// A hand-built type mapping for a logical model with a single "hook" element of type string.
    /// </summary>
    private class TestSummaryProvider(string rootTypeName) : IStructureDefinitionSummaryProvider
    {
        public IStructureDefinitionSummary Provide(string canonical) =>
            canonical == rootTypeName ? new ComplexSummary(rootTypeName, new ElementSummary("hook", "string")) :
            canonical == "string" ? new ComplexSummary("string", new ElementSummary("value", "System.String", XmlRepresentation.XmlAttr)) :
            null;
    }

    /// <summary>
    /// A provider that knows a single hand-built type.
    /// </summary>
    private class SingleTypeSummaryProvider(ComplexSummary summary) : IStructureDefinitionSummaryProvider
    {
        public IStructureDefinitionSummary Provide(string canonical) =>
            canonical == summary.TypeName ? summary : null;
    }

    private class ComplexSummary(string typeName, params IElementDefinitionSummary[] elements) : IStructureDefinitionSummary
    {
        public string TypeName => typeName;
        public bool IsAbstract => false;
        public bool IsResource => false;
        public IReadOnlyCollection<IElementDefinitionSummary> GetElements() => elements;
    }

    private class ElementSummary(
        string elementName,
        string typeName,
        XmlRepresentation representation = XmlRepresentation.XmlElement) : IElementDefinitionSummary
    {
        public string ElementName => elementName;
        public bool IsCollection => false;
        public bool IsRequired => false;
        public bool InSummary => false;
        public bool IsChoiceElement => false;
        public bool IsResource => false;
        public bool IsModifier => false;
        public ITypeSerializationInfo[] Type => [new TypeReference(typeName)];
        public string DefaultTypeName => null;
        public string NonDefaultNamespace => null;
        public XmlRepresentation Representation => representation;
        public int Order => 0;
    }

    private class TypeReference(string referredType) : IStructureDefinitionReference
    {
        public string ReferredType => referredType;
    }
}
