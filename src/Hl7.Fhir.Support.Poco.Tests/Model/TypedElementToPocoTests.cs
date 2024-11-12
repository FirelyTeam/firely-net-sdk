using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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

        poco.Active.Should().Be(true);
        poco.BirthDate.Should().Be("2000-01-01");
        poco.Name.Should().HaveCount(1);
        poco.Name[0].Family.Should().Be("Doe");
        poco.Name[0].Given.Should().HaveCount(1).And.Contain("John");
        poco.Contact.Should().HaveCount(1);
        poco.Contact[0].Name.Family.Should().Be("Doe");
        poco.Contact[0].Name.Given.Should().HaveCount(1).And.Contain("Jane");
        poco.Contact[0].Relationship.Should().HaveCount(1);
        poco.Contact[0].Relationship[0].Coding.Should().HaveCount(1);
        poco.Contact[0].Relationship[0].Coding[0].System.Should().Be("http://nu.nl");
        poco.Contact[0].Relationship[0].Coding[0].Code.Should().Be("relation");
    }

    private T toPoco<T>(T source) where T : Base, new()
    {
        var te = source.ToTypedElement();
        return toPoco<T>(te);
    }

    private T toPoco<T>(ITypedElement source) where T : Base, new()
    {
        // Construct a demo STU3 model inspector
        var builder = new PocoBuilderNew(ModelInfo.ModelInspector);
        var built = (T)builder.BuildFrom(source);

        built.Should().BeOfType<T>();

        return (T)built;
    }
}