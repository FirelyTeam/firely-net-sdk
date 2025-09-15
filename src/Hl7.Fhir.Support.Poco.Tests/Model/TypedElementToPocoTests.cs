/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
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
                [typeof(Integer), 42, null],
                [typeof(Integer64), 42L, "42"],
                [typeof(FhirBoolean), true, null],
                [typeof(FhirDecimal), 3.14m, null],
                [typeof(DynamicPrimitive), 3.14, null],
                [typeof(FhirDateTime), dtNow, dtNow.ToString()],
                [typeof(Time), timeNow, timeNow.ToString()],
                [typeof(Date), dateToday, dateToday.ToString()],
                // strings will be parsed as DynamicPrimitive to allow for detection of string backed primitive types
                // that were built with no type information, but we get the correct type at a later point
                [typeof(DynamicPrimitive), "hi!", null]
            ];
        }
    }

    [TestMethod]
    [DynamicData(nameof(PrimitiveTestData))]
    public void GuessesCorrectPrimitive(Type t, object dynamicValue, string objectValue)
    {
        ITypedElement subject = PocoNodeOrList.Root(new DynamicPrimitive{ DynamicTypeName = "DoesNotExist", JsonValue = dynamicValue });
        var poco = toPoco(subject);
        poco.Should().BeOfType(t);
        (poco as PrimitiveType)!.JsonValue.Should().Be(objectValue ?? dynamicValue);
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
        subject.SetValue("newDynamicField", new DynamicPrimitive() { JsonValue = "hi3" });
        subject.SetValue("newListField", new List<FhirString> { new("hi1"), new("hi2") });

        var subjectRt = toPoco(subject);
        subjectRt.TryGetValue("newField", out var newField).Should().BeTrue();
        newField.Should().BeOfType<FhirString>().Which.Value.Should().Be("hi");

        subjectRt.TryGetValue("newDynamicField", out var newDynamicField).Should().BeTrue();
        newDynamicField.Should().BeOfType<DynamicPrimitive>().Which.JsonValue.Should().Be("hi3");

        subjectRt.TryGetValue("newListField", out var newListField).Should().BeTrue();
        newListField.Should().BeOfType<List<FhirString>>().Which
            .Should().BeEquivalentTo([new FhirString("hi1"), new FhirString("hi2")]);
    }

    [TestMethod]
    public void TurnsRepeatingMarkedAsNonRepeatingIntoList()
    {
        var humanName = ElementNode.Root(ModelInfo.ModelInspector,"HumanName");
        
        humanName.Add(ModelInfo.ModelInspector, "family", "Brown", "string");
        humanName.Add(ModelInfo.ModelInspector, "family", "Brown2", "string");
        
        var poco = humanName.ToPoco<HumanName>();
        var act = () => poco.FamilyElement;
        act.Should().Throw<CodedValidationException>().Which.ErrorCode.Should().Be(CodedValidationException.PROPERTY_TYPE_MISMATCH_CODE);
        poco["family"].Should().BeOfType<List<FhirString>>().Which.Should().HaveCount(2);
    }

    [TestMethod]
    public void DoesNotOverrideInstanceType()
    {
        var humanName = ElementNode.Root(ModelInfo.ModelInspector,"CustomHumanName");
        
        var poco = humanName.ToPoco();
        poco.Should().BeOfType<DynamicDataType>().Which.DynamicTypeName.Should().Be("CustomHumanName");
        poco.ToTypedElement().InstanceType.Should().Be("CustomHumanName");
    }

    [TestMethod] 
    public void RetainsPositionAnnotationsFromOriginalTypedElement()
    {
        var integer = ElementNode.Root(ModelInfo.ModelInspector,"Integer", value: 1);
        integer!.AddAnnotation(new PositionInfo(1, 1));
        
        var poco = integer.ToPoco();
        poco.Should().BeOfType<Integer>().Which.Value.Should().Be(1);
        poco.Annotation<PositionInfo>().Should().NotBeNull();
    }
    
    [TestMethod]
    public void RetainsPositionAnnotationsFromOriginalSourceNode()
    {
        var integer = SourceNode.Valued("valueInteger", "1", []);
        integer.AddAnnotation(new PositionInfo(1, 1));
        var poco = integer.ToPoco(pocoType: typeof(Integer));
        poco.Should().BeOfType<Integer>().Which.Value.Should().Be(1);
        poco.Annotation<PositionInfo>().Should().NotBeNull();
    }

    private T toPoco<T>(T source) where T : Base, new()
    {
        var poco = toPoco(source.ToPocoNode());
        return poco.Should().BeOfType<T>().Subject;
    }

    private Base toPoco(ITypedElement source)
    {
        // Construct a demo STU3 model inspector
        var builder = new NewPocoBuilder(ModelInfo.ModelInspector);
        return builder.BuildFrom(source);
    }
}