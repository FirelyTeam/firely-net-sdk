/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class SourceNodeToPocoTests
{
    [TestMethod]
    public void CanConvertSourceNodeToPocoNodeDirectly()
    {
        var patient = createPatientSourceNode();

        var pocoNode = patient.ToPocoNode(ModelInfo.ModelInspector);

        pocoNode.Name.Should().Be("Patient");
        ((ISourceNode)pocoNode).Children("deceasedBoolean").Single().Text.Should().Be("true");
    }

    [TestMethod]
    public void DirectSourceNodeToPocoNodeMatchesTypedElementBridge()
    {
        var patient = SourceNode.Resource("Patient", "Patient",
            SourceNode.Valued("active", "true"),
            SourceNode.Valued("deceasedBoolean", "true"),
            SourceNode.Valued("newField", "hi"),
            SourceNode.Node("newComplex", SourceNode.Valued("child", "hello")));

        var direct = patient.ToPocoNode(ModelInfo.ModelInspector);
        var bridged = patient.ToTypedElement(ModelInfo.ModelInspector).ToPocoNode(ModelInfo.ModelInspector);

        direct.ToJson().Should().Be(bridged.ToJson());
    }

    [TestMethod]
    public void DirectSourceNodeToPocoPreservesPositionAnnotations()
    {
        var integer = SourceNode.Valued("valueInteger", "1");
        integer.AddAnnotation(new PositionInfo(1, 1));

        var poco = integer.ToPoco(ModelInfo.ModelInspector, typeof(Integer));

        poco.Should().BeOfType<Integer>().Which.Value.Should().Be(1);
        poco.Annotation<PositionInfo>().Should().NotBeNull();
    }

    [TestMethod]
    public void DirectSourceNodeToPocoKeepsUnknownMembersInOverflow()
    {
        var patient = SourceNode.Resource("Patient", "Patient",
            SourceNode.Valued("newField", "hi"),
            SourceNode.Node("newComplex", SourceNode.Valued("child", "hello")));

        var poco = patient.ToPoco<Patient>();

        poco.TryGetValue("newField", out var newField).Should().BeTrue();
        newField.Should().BeOfType<DynamicPrimitive>().Which.JsonValue.Should().Be("hi");

        poco.TryGetValue("newComplex", out var newComplex).Should().BeTrue();
        var dynamicType = newComplex.Should().BeOfType<DynamicDataType>().Subject;
        dynamicType.DynamicTypeName.Should().Be("newComplex");
        dynamicType.TryGetValue("child", out var child).Should().BeTrue();
        child.Should().BeOfType<DynamicPrimitive>().Which.JsonValue.Should().Be("hello");
    }

    [TestMethod]
    public void ToPocoReturnsWrappedPocoWhenSourceNodeIsAlreadyPocoNode()
    {
        var patient = new Patient { Active = true };
        ISourceNode source = patient.ToPocoNode(ModelInfo.ModelInspector);

        var poco = source.ToPoco(ModelInfo.ModelInspector);

        poco.Should().BeSameAs(patient);
    }

    [TestMethod]
    public void BuilderReturnsWrappedPocoWhenSourceNodeIsAlreadyPocoNode()
    {
        var patient = new Patient { Active = true };
        ISourceNode source = patient.ToPocoNode(ModelInfo.ModelInspector);

        var poco = new NewPocoBuilder(ModelInfo.ModelInspector).BuildFrom(source, typeof(Resource));

        poco.Should().BeSameAs(patient);
    }

    [TestMethod]
    public void ToPocoReusesCompatiblePocoNodeChildrenInsideMixedSourceTree()
    {
        var name = new HumanName(family: "Doe", given: ["John"]);
        ISourceNode root = new StubSourceNode(
            name: "Patient",
            resourceType: "Patient",
            children:
            [
                name.ToPocoNode(ModelInfo.ModelInspector, "name")
            ]);

        var patient = root.ToPoco<Patient>();

        patient.Name.Should().ContainSingle();
        patient.Name[0].Should().BeSameAs(name);
    }

    private static SourceNode createPatientSourceNode() =>
        SourceNode.Resource("Patient", "Patient",
            SourceNode.Valued("active", "true"),
            SourceNode.Valued("deceasedBoolean", "true"),
            SourceNode.Node("name",
                SourceNode.Valued("family", "Doe"),
                SourceNode.Valued("given", "John")));

    private sealed class StubSourceNode(string name, string? text = null, string? resourceType = null, IEnumerable<ISourceNode>? children = null)
        : ISourceNode, IResourceTypeSupplier
    {
        private readonly IReadOnlyList<ISourceNode> _children = children?.ToList() ?? [];

        public string Name { get; } = name;
        public string Text { get; } = text!;
        public string Location => Name;
        public string? ResourceType { get; } = resourceType;

        public IEnumerable<ISourceNode> Children(string? name = null) =>
            name is null ? _children : _children.Where(c => c.Name == name);
    }
}


