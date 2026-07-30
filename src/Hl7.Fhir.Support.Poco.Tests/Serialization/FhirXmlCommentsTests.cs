using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class FhirXmlCommentsTests
{
    private const string PATIENT_WITH_COMMENTS =
        "<!--before root-->" +
        "<Patient xmlns=\"http://hl7.org/fhir\">" +
            "<!--before id-->" +
            "<id value=\"pat1\" />" +
            "<name>" +
                "<family value=\"Doe\" />" +
                "<!--closing name-->" +
            "</name>" +
            "<!--before second name-->" +
            "<name>" +
                "<family value=\"Roe\" />" +
            "</name>" +
            "<!--closing patient-->" +
        "</Patient>" +
        "<!--after root-->";

    private static Patient parse(bool retainComments) =>
        new FhirXmlDeserializer(new DeserializerSettings { RetainComments = retainComments })
            .Deserialize<Patient>(PATIENT_WITH_COMMENTS);

    private static string serialize(Base poco) =>
        SerializationUtil.WriteXmlToString(w => new FhirXmlSerializer().Serialize(poco, w));

    [TestMethod]
    public void RetainsCommentsAsAnnotations()
    {
        var patient = parse(retainComments: true);

        var onPatient = patient.Annotation<SourceComments>();
        onPatient.Should().NotBeNull();
        onPatient!.CommentsBefore.Should().Equal("before root");
        onPatient.ClosingComments.Should().Equal("closing patient");
        onPatient.DocumentEndComments.Should().Equal("after root");

        // A comment preceding an element is annotated on the POCO for that element...
        patient.IdElement.Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before id");
        patient.Name[1].Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before second name");

        // ...and a comment that is the last content of an element closes that element.
        patient.Name[0].Annotation<SourceComments>()!.ClosingComments.Should().Equal("closing name");

        // The comment closing the first name must not have leaked onto the element following it.
        patient.Name[0].Annotation<SourceComments>()!.CommentsBefore.Should().BeNull();
        patient.Name[1].Annotation<SourceComments>()!.ClosingComments.Should().BeNull();
    }

    [TestMethod]
    public void RetainedCommentsSurviveARoundtrip()
    {
        serialize(parse(retainComments: true)).Should().Be(PATIENT_WITH_COMMENTS);
    }

    [TestMethod]
    public void DoesNotRetainCommentsByDefault()
    {
        var patient = parse(retainComments: false);

        patient.Annotation<SourceComments>().Should().BeNull();
        patient.IdElement.Annotation<SourceComments>().Should().BeNull();
        patient.Name[0].Annotation<SourceComments>().Should().BeNull();

        // Without the setting, the serialized form is unchanged from what it has always been.
        serialize(patient).Should().Be(
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<id value=\"pat1\" />" +
                "<name><family value=\"Doe\" /></name>" +
                "<name><family value=\"Roe\" /></name>" +
            "</Patient>");
    }

    [TestMethod]
    public void WritesCommentsRetainedByTheLegacyParserToo()
    {
        // The serializer writes whatever SourceComments it finds, whoever put them there. The legacy
        // ElementModel parser has always produced this annotation, so POCOs parsed that way roundtrip
        // their comments through the new serializer as well - the same as they do through FhirXmlBuilder.
        var poco = FhirXmlNode.Parse(PATIENT_WITH_COMMENTS).ToPoco<Patient>();

        serialize(poco).Should().Be(PATIENT_WITH_COMMENTS);
    }

    [TestMethod]
    public void RetainsEveryCommentOfTheEdgecasesFile()
    {
        // The hand-written cases above check placement; this checks the setting against a real file, both
        // that it retains every comment in document order, and that it leaves the rest of the data alone.
        var expected = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

        // Ostrich, because this file deliberately contains data the validator objects to - the comments
        // are what is under test here, not the validation.
        var poco = new FhirXmlDeserializer(
                new DeserializerSettings { RetainComments = true }.UsingMode(DeserializationMode.Ostrich))
            .Deserialize<Patient>(expected);

        var actual = serialize(poco);

        commentsOf(actual).Should().Equal(commentsOf(expected));
        XmlAssert.AreSame("edgecases", expected, actual, ignoreSchemaLocation: true);

        static string[] commentsOf(string xml) =>
            SerializationUtil.XDocumentFromXmlText(xml, ignoreComments: false)
                .DescendantNodes().OfType<XComment>().Select(c => c.Value).ToArray();
    }

    [TestMethod]
    public void RetainsCommentsThroughTheStringOverload()
    {
        // The overload that PocoSerializationEngine.DeserializeFromXml uses: it builds its own XmlReader,
        // which has to be told not to drop the comments before the deserializer ever sees them.
        var patient = (Patient)new FhirXmlDeserializer(new DeserializerSettings { RetainComments = true })
            .DeserializeResource(PATIENT_WITH_COMMENTS);

        patient.Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before root");
    }

    [TestMethod]
    public void DropsCommentsOnValuesWrittenAsAnAttribute()
    {
        // Extension.url is written as an attribute, but the source has it as an element, so a comment
        // can end up annotated on a POCO that is serialized into a start tag - where a comment cannot go.
        // Such a comment is dropped rather than corrupting the output.
        const string xml =
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<extension>" +
                    "<!--before url-->" +
                    "<url value=\"http://example.org/x\" />" +
                    "<valueString value=\"a\" />" +
                "</extension>" +
            "</Patient>";

        var patient = new FhirXmlDeserializer(
                new DeserializerSettings { RetainComments = true }.UsingMode(DeserializationMode.Ostrich))
            .Deserialize<Patient>(xml);

        patient.Extension[0].UrlElement.Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before url");

        serialize(patient).Should().Be(
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<extension url=\"http://example.org/x\">" +
                    "<valueString value=\"a\" />" +
                "</extension>" +
            "</Patient>");
    }

    [TestMethod]
    public void RetainsCommentsInNarrativeAndContainedResources()
    {
        const string xml =
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<text>" +
                    "<status value=\"generated\" />" +
                    "<!--before div-->" +
                    "<div xmlns=\"http://www.w3.org/1999/xhtml\">Hi<!--in div--></div>" +
                    "<!--closing text-->" +
                "</text>" +
                "<contained>" +
                    "<!--before contained patient-->" +
                    "<Patient><id value=\"c1\" /></Patient>" +
                    "<!--after contained patient-->" +
                "</contained>" +
            "</Patient>";

        var patient = new FhirXmlDeserializer(new DeserializerSettings { RetainComments = true })
            .Deserialize<Patient>(xml);

        patient.Text.Div.Should().Contain("<!--in div-->", because: "comments inside the narrative are part of its value");
        patient.Text.DivElement.Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before div");
        patient.Text.Annotation<SourceComments>()!.ClosingComments.Should().Equal("closing text");

        // Comments within a resource container are annotated on the resource itself, so they move out of
        // the container when written back out. See DeserializerSettings.RetainComments.
        var contained = patient.Contained[0];
        contained.Annotation<SourceComments>()!.CommentsBefore.Should().Equal("before contained patient");
        contained.Annotation<SourceComments>()!.ClosingComments.Should().Equal("after contained patient");

        serialize(patient).Should().Be(
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<text>" +
                    "<status value=\"generated\" />" +
                    "<!--before div-->" +
                    "<div xmlns=\"http://www.w3.org/1999/xhtml\">Hi<!--in div--></div>" +
                    "<!--closing text-->" +
                "</text>" +
                "<!--before contained patient-->" +
                "<contained>" +
                    "<Patient><id value=\"c1\" /><!--after contained patient--></Patient>" +
                "</contained>" +
            "</Patient>");
    }
}
