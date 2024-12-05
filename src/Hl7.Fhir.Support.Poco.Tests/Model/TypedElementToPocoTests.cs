using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ET=Hl7.Fhir.ElementModel.Types;

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

    public static IEnumerable<object[]> PrimitiveTestData
    {
        get
        {
            var dtNow = ET.DateTime.Now();
            var timeNow = ET.Time.Now();
            var dateToday = ET.Date.Today();

            return
            [
                [typeof(FhirString), "hi!", null],
                [typeof(Integer), 42, null],
                [typeof(Integer64), 42L, null],
                [typeof(FhirBoolean), true, null],
                [typeof(FhirDecimal), 3.14m, null],
                [typeof(DynamicPrimitive), 3.14, null],
                [typeof(FhirDateTime), dtNow, dtNow.ToString()],
                [typeof(Time), timeNow, timeNow.ToString()],
                [typeof(Date), dateToday, dateToday.ToString()],
            ];
        }
    }

    [TestMethod]
    [DynamicData(nameof(PrimitiveTestData))]
    public void GuessesCorrectPrimitive(Type t, object dynamicValue, string objectValue)
    {
        ITypedElement subject = new SinglePrimitiveElementNode(new DynamicPrimitive{ DynamicTypeName = "DoesNotExist", ObjectValue = dynamicValue });
        var poco = toPoco(subject);
        poco.Should().BeOfType(t);
        (poco as PrimitiveType)!.ObjectValue.Should().Be(objectValue ?? dynamicValue);
    }

    [TestMethod]
    public void ParsesCodeOfT()
    {
        var subject = new Narrative { Status = Narrative.NarrativeStatus.Generated };
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

        subject.SetValue("newField", new FhirString("hi"));
        subject.SetValue("newDynamicField", new DynamicPrimitive() { ObjectValue = "hi3" });
        subject.SetValue("newListField", new List<FhirString> { new("hi1"), new("hi2") });

        var subjectRt = toPoco(subject);
        subjectRt.TryGetValue("newField", out var newField).Should().BeTrue();
        newField.Should().BeOfType<FhirString>().Which.Value.Should().Be("hi");

        subjectRt.TryGetValue("newDynamicField", out var newDynamicField).Should().BeTrue();
        newDynamicField.Should().BeOfType<DynamicPrimitive>().Which.ObjectValue.Should().Be("hi3");

        subjectRt.TryGetValue("newListField", out var newListField).Should().BeTrue();
        newListField.Should().BeOfType<List<FhirString>>().Which
            .Should().BeEquivalentTo([new FhirString("hi1"), new FhirString("hi2")]);
    }

    private T toPoco<T>(T source) where T : Base, new()
    {
        var poco = toPoco(source.ToElementNode());
        return poco.Should().BeOfType<T>().Subject;
    }

    private Base toPoco(ITypedElement source)
    {
        // Construct a demo STU3 model inspector
        var builder = new NewPocoBuilder(ModelInfo.ModelInspector);
        return builder.BuildFrom(source);
    }
}