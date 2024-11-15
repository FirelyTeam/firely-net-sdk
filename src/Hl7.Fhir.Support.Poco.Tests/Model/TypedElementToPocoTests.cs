using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class TypedElementToPocoTests
{
    [TestMethod]
    public void ParsesPrimitive()
    {
        var subject = new FhirBoolean(true);
        subject.AddExtension("http://nu.nl", new FhirString("hoi"));
        subject.AddExtension("http://dan.nl", new FhirString("hi"));
        subject.ElementId = "314";

        var poco = toPoco(subject);

        poco.Value.Should().Be(true);
        poco.ElementId.Should().Be("314");
        poco.Extension.Should().HaveCount(2);
        poco.Extension[0].Url.Should().Be("http://nu.nl");
        poco.Extension[0].Value.Should().BeOfType<FhirString>().Which.Value.Should().Be("hoi");
    }

    [TestMethod]
    public void ParsesCodeOfT()
    {
        var subject = new Narrative() { Status = Narrative.NarrativeStatus.Generated };
        var poco = toPoco(subject);

        poco.Status.Should().Be(Narrative.NarrativeStatus.Generated);
    }


    [TestMethod]
    public void ParsesChoiceType()
    {
        var subject = new Patient { Deceased = new FhirBoolean(true) };
        var poco = toPoco(subject);

        poco.Deceased.Should().BeOfType<FhirBoolean>().Which.Value.Should().Be(true);
    }

    [TestMethod]
    public void ParsesCovariantList()
    {
        var subject = new Patient { Contained = [new Observation()]};
        var poco = toPoco(subject);

        poco.Contained.Should().HaveCount(1);
        poco.Contained[0].Should().BeOfType<Observation>();
    }

    [TestMethod]
    public void ParsesCovariantCodedList()
    {
        var subject = new Questionnaire { SubjectTypeElement = [new Code<ResourceType>(ResourceType.Binary)] };
        var poco = toPoco(subject);

        poco.SubjectTypeElement.Should().HaveCount(1);
        poco.SubjectTypeElement[0].Should().BeOfType<Code<ResourceType>>().Which.Value.Should().Be(ResourceType.Binary);
    }

    [TestMethod]
    public void ParsesResourceWithBackbone()
    {
        var subject = new Patient
            {
                Active = true,
                BirthDate = "2000-01-01",
                Name = [new HumanName(family: "Doe", given: ["John"])],
                Contact = [new Patient.ContactComponent
                {
                    Name = new HumanName(family: "Doe", given: ["Jane"]),
                    Relationship = [new CodeableConcept("http://nu.nl", "relation")]
                }]
            };

        var poco = toPoco(subject);
        poco.IsExactly(subject).Should().BeTrue();
    }


    [TestMethod]
    public void ParsesResourceWithOverflow()
    {
        var subject = new Patient();

        var subjectDict = subject.AsDictionary();
        subjectDict.Add("newField", new FhirString("hi"));
        subjectDict.Add("newDynamicField", new DynamicPrimitive() { ObjectValue = "hi3" });
        subjectDict.Add("newListField", new List<FhirString> { new("hi1"), new("hi2") });

        var dict = toPoco(subject).AsDictionary();
        dict.TryGetValue("newField", out var newField).Should().BeTrue();
        newField.Should().BeOfType<FhirString>().Which.Value.Should().Be("hi");

        dict.TryGetValue("newDynamicField", out var newDynamicField).Should().BeTrue();
        newDynamicField.Should().BeOfType<DynamicPrimitive>().Which.ObjectValue.Should().Be("hi3");

        dict.TryGetValue("newListField", out var newListField).Should().BeTrue();
        newListField.Should().BeOfType<List<FhirString>>().Which.Should().BeEquivalentTo(
            [new FhirString("hi1"), new FhirString("hi2")]);
    }

    private T toPoco<T>(T source) where T : Base, new()
    {
        // Construct a demo STU3 model inspector
        var builder = new PocoBuilderNew(ModelInfo.ModelInspector);
        var built = (T)builder.BuildFrom(source);

        built.Should().BeOfType<T>();

        return built;
    }
}