using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Xml.Linq;

namespace Hl7.Fhir.Support.Poco.Tests
{
    /// <summary>
    /// The FHIR specification does not allow objects/elements without content, so a structure that turns out
    /// to be empty - either because the POCO carries nothing, or because a <see cref="SerializationFilter"/>
    /// removed everything in it - must not be written at all.
    /// </summary>
    [TestClass]
    public class EmptyStructurePruningTests
    {
        private static readonly BaseFhirJsonSerializer JSON = new(ModelInfo.ModelInspector);
        private static readonly BaseFhirXmlSerializer XML = new(ModelInfo.ModelInspector);

        /// <summary>
        /// Serializes indented and compares against the expected value, ignoring newline style on both sides.
        /// </summary>
        /// <remarks>Neither side is reliably "\n" or "\r\n": the writer uses <see cref="Environment.NewLine"/>,
        /// and the raw string literals below carry whatever the checkout gave this file - which for a ".cs
        /// text" file under core.autocrlf differs between a developer machine and the build agent.</remarks>
        private static void shouldSerializeIndentedAs(Base instance, string expected) =>
            normalizeNewlines(JSON.SerializeToString(instance, pretty: true))
                .Should().Be(normalizeNewlines(expected));

        private static string normalizeNewlines(string value) => value.Replace("\r\n", "\n");

        [TestMethod]
        public void OmitsEmptyComplexMember()
        {
            var patient = new Patient { Name = [new HumanName()] };

            JSON.SerializeToString(patient).Should().Be("""{"resourceType":"Patient"}""");
            XML.SerializeToString(patient).Should().NotContain("<name");
        }

        /// <summary>
        /// An empty object nested inside another empty object must not keep its parent alive: both go, in a
        /// single pass.
        /// </summary>
        [TestMethod]
        public void CollapsesNestedEmptyObjects()
        {
            var patient = new Patient
            {
                Contact = [new Patient.ContactComponent { Name = new HumanName { Period = new Period() } }]
            };

            JSON.SerializeToString(patient).Should().Be("""{"resourceType":"Patient"}""");

            var xml = XML.SerializeToString(patient);
            xml.Should().NotContain("<contact").And.NotContain("<name").And.NotContain("<period");
        }

        /// <summary>
        /// Dropping a member must not disturb the separators around the members that do survive - the failure
        /// mode of a serializer that writes the opening brace before it knows whether anything follows.
        /// </summary>
        [TestMethod]
        public void KeepsSiblingsIntactWhenDroppingAMemberInTheMiddle()
        {
            var patient = new Patient
            {
                ActiveElement = new FhirBoolean(true),
                MaritalStatus = new CodeableConcept(),        // empty: dropped
                BirthDateElement = new Date("1974-12-25")
            };

            JSON.SerializeToString(patient).Should()
                .Be("""{"resourceType":"Patient","active":true,"birthDate":"1974-12-25"}""");

            // Same in pretty-printed form, where a stray separator or indent would show up too.
            shouldSerializeIndentedAs(patient,
                """
                {
                  "resourceType": "Patient",
                  "active": true,
                  "birthDate": "1974-12-25"
                }
                """);
        }

        /// <summary>
        /// The '_elementName' property holding a primitive's id/extensions is itself an object, and so must be
        /// left out when the filter removes all of them. Regression test for the empty "_birthDate": {} that
        /// summarized output used to contain.
        /// </summary>
        [TestMethod]
        public void OmitsPrimitiveExtensionObjectEmptiedByFilter()
        {
            var patient = new Patient
            {
                BirthDateElement = new Date("1974-12-25")
                {
                    Extension = [new Extension("http://example.org/birthTime", new FhirString("14:35"))]
                }
            };

            // Without a filter the extensions survive, so '_birthDate' is written.
            JSON.SerializeToString(patient).Should().Contain("_birthDate");

            // 'Extension' is not in the summary, so everything inside '_birthDate' is removed - and with it
            // the '_birthDate' property itself.
            var summarized = JSON.SerializeToString(patient, filterFactory: SerializationFilter.ForSummary);
            summarized.Should().NotContain("_birthDate").And.Contain("\"birthDate\"");
        }

        /// <summary>
        /// Indentation used to be wrong for '_elementName' objects, because they were serialized into a
        /// separate buffer that knew nothing about the depth it would be spliced into.
        /// </summary>
        [TestMethod]
        public void IndentsPrimitiveExtensionObjectAtItsOwnDepth()
        {
            var patient = new Patient
            {
                Contact =
                [
                    new Patient.ContactComponent
                    {
                        Name = new HumanName
                        {
                            FamilyElement = new FhirString("Doe") { ElementId = "a1" }
                        }
                    }
                ]
            };

            shouldSerializeIndentedAs(patient,
                """
                {
                  "resourceType": "Patient",
                  "contact": [
                    {
                      "name": {
                        "family": "Doe",
                        "_family": {
                          "id": "a1"
                        }
                      }
                    }
                  ]
                }
                """);
        }

        /// <summary>
        /// The 'elementName' and '_elementName' arrays of a repeating primitive must stay the same length, so
        /// entries without content become nulls - but only once the array holds something at all.
        /// </summary>
        [TestMethod]
        public void KeepsPrimitiveArraysAligned()
        {
            // Leading and trailing placeholder: only the middle entry has an id.
            var name = new HumanName
            {
                GivenElement =
                [
                    new FhirString("Anne"),
                    new FhirString("Marie") { ElementId = "a3" },
                    new FhirString("Claire")
                ]
            };

            JSON.SerializeToString(name).Should().Be(
                """{"given":["Anne","Marie","Claire"],"_given":[null,{"id":"a3"},null]}""");

            // No entry has id/extensions, so the '_given' array is not written at all rather than being
            // filled with nothing but nulls.
            var plain = new HumanName { GivenElement = [new FhirString("Anne"), new FhirString("Marie")] };
            JSON.SerializeToString(plain).Should().Be("""{"given":["Anne","Marie"]}""");
        }

        /// <summary>
        /// Alignment must survive a filter emptying one entry but not another: the emptied entry becomes a
        /// placeholder, it does not disappear and shift the ones after it.
        /// </summary>
        [TestMethod]
        public void KeepsPrimitiveArraysAlignedWhenFilterEmptiesAnEntry()
        {
            var name = new HumanName
            {
                GivenElement =
                [
                    // Nothing but an extension, which the filter below removes.
                    new FhirString("Anne") { Extension = [new Extension("http://example.org/x", new FhirString("1"))] },
                    new FhirString("Marie") { ElementId = "keep-me" }
                ]
            };

            JSON.SerializeToString(name, filterFactory: () => new DropExtensionsFilter())
                .Should().Be("""{"given":["Anne","Marie"],"_given":[null,{"id":"keep-me"}]}""");

            // The same the other way round: here the array is already open by the time the filter empties an
            // entry, so the placeholder has to be written out rather than dropped as a trailing one.
            var reversed = new HumanName
            {
                GivenElement =
                [
                    new FhirString("Anne") { ElementId = "keep-me" },
                    new FhirString("Marie") { Extension = [new Extension("http://example.org/x", new FhirString("1"))] }
                ]
            };

            JSON.SerializeToString(reversed, filterFactory: () => new DropExtensionsFilter())
                .Should().Be("""{"given":["Anne","Marie"],"_given":[{"id":"keep-me"},null]}""");
        }

        /// <summary>
        /// Removes all 'extension' members, so that an element carrying nothing else empties out - which is
        /// what the built-in summary filters do to primitive extensions, without also removing the ids we
        /// need to tell the surviving entries apart.
        /// </summary>
        private class DropExtensionsFilter : SerializationFilter
        {
            public override void EnterObject(object value, ClassMapping mapping) { }
            public override void LeaveObject(object value, ClassMapping mapping) { }
            public override void LeaveMember(string name, object value, PropertyMapping mapping) { }
            public override bool TryEnterMember(string name, object value, PropertyMapping mapping) =>
                name != "extension";
        }

        /// <summary>
        /// Pruning stops at the root: callers are promised a document, and an empty payload would break
        /// anything that parses the result.
        /// </summary>
        [TestMethod]
        public void WritesTheRootEvenWhenEmpty()
        {
            JSON.SerializeToString(new HumanName()).Should().Be("{}");
            XML.SerializeToString(new HumanName()).Should().Contain("<HumanName");
            XML.SerializeToString(new HumanName(), rootName: "name").Should().Contain("<name");

            // Same when it is the filter that empties the instance out.
            var patient = new Patient { Text = new Narrative { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">x</div>" } };
            var summarized = JSON.SerializeToString(patient, filterFactory: SerializationFilter.ForCount);
            summarized.Should().Be("""{"resourceType":"Patient"}""");
        }

        /// <summary>
        /// A lone primitive is wrapped in an object with a pseudo-property 'value' (see issue 3286); that
        /// wrapper is a root too.
        /// </summary>
        [TestMethod]
        public void WritesTheRootOfALonePrimitiveEvenWhenEmpty()
        {
            JSON.SerializeToString(new FhirBoolean()).Should().Be("{}");
            JSON.SerializeToString(new FhirBoolean(true)).Should().Be("""{"value":true}""");
        }

        /// <summary>
        /// A resource always writes 'resourceType' in Json, so it is never empty - including a dynamic
        /// resource without a type name of its own, which falls back to "DynamicResource" just as the Xml
        /// serializer has always done for its element name.
        /// </summary>
        [TestMethod]
        public void AlwaysWritesResourceTypeIncludingForAnUnnamedDynamicResource()
        {
            JSON.SerializeToString(new DynamicResource()).Should().Be("""{"resourceType":"DynamicResource"}""");
            XML.SerializeToString(new DynamicResource()).Should().Contain("<DynamicResource");

            JSON.SerializeToString(new DynamicResource { DynamicTypeName = "Patient" })
                .Should().Be("""{"resourceType":"Patient"}""");
        }

        /// <summary>
        /// A null in a list occurs in error situations; Json keeps it as a placeholder (Xml has no way to
        /// represent it and drops it, as it always has).
        /// </summary>
        [TestMethod]
        public void KeepsNullPlaceholdersInComplexArrays()
        {
            var patient = new Patient { Name = [null, new HumanName { FamilyElement = new FhirString("Doe") }] };

            JSON.SerializeToString(patient).Should()
                .Be("""{"resourceType":"Patient","name":[null,{"family":"Doe"}]}""");
        }

        /// <summary>
        /// Xml has no notion of 'resourceType', so an empty resource has genuinely nothing to write and is
        /// pruned like any other element - unlike in Json.
        /// </summary>
        [TestMethod]
        public void OmitsEmptyContainedResourceInXml()
        {
            var patient = new Patient { Contained = [new Patient()] };

            XML.SerializeToString(patient).Should().NotContain("<contained");
            JSON.SerializeToString(patient).Should()
                .Be("""{"resourceType":"Patient","contained":[{"resourceType":"Patient"}]}""");
        }

        /// <summary>
        /// The same pruning applies when it is a filter that empties an element out - the Xml counterpart
        /// of the filtered Json cases above.
        /// </summary>
        [TestMethod]
        public void OmitsXmlElementsMadeEmptyByFilter()
        {
            var patient = new Patient
            {
                MaritalStatus = new CodeableConcept("http://example.org", "married"),
                Contact = [new Patient.ContactComponent { Name = new HumanName { Family = "Doe" } }]
            };

            static SerializationFilter filter() => new TopLevelFilter(
                new ElementMetadataFilter { IncludeNames = ["maritalStatus", "contact"] },
                new ElementMetadataFilter { IncludeNames = ["not-present"] });

            XML.SerializeToString(patient, filterFactory: filter)
                .Should().NotContain("<maritalStatus").And.NotContain("<contact");
        }

        /// <summary>
        /// In Xml a primitive's value and id become attributes, and an attribute is content: the element
        /// carrying it must be written, even when that attribute is all it has.
        /// </summary>
        [TestMethod]
        public void KeepsXmlElementsWithOnlyAttributes()
        {
            var patient = new Patient
            {
                ActiveElement = new FhirBoolean(true),
                BirthDateElement = new Date { ElementId = "date-id" }    // no value, just an id
            };

            var root = XDocument.Parse(XML.SerializeToString(patient)).Root!;

            root.Element(XName.Get("active", XmlNs.FHIR))!.Attribute("value")!.Value.Should().Be("true");
            root.Element(XName.Get("birthDate", XmlNs.FHIR))!.Attribute("id")!.Value.Should().Be("date-id");
        }

        /// <summary>
        /// A retained source comment counts as content: an element holding nothing but its closing comment
        /// is written rather than pruned, since dropping a comment the caller asked us to retain would be
        /// worse than reproducing an element the source data had in that shape to begin with. A comment
        /// that <i>preceded</i> a pruned element is not lost either - it stays behind in the parent.
        /// </summary>
        [TestMethod]
        public void RetainedCommentsCountAsContent()
        {
            var withClosing = new Patient.ContactComponent();
            withClosing.AddAnnotation(new SourceComments { ClosingComments = ["kept"] });
            XML.SerializeToString(new Patient { Contact = [withClosing] })
                .Should().Contain("<contact><!--kept--></contact>");

            var withPreceding = new Patient.ContactComponent();
            withPreceding.AddAnnotation(new SourceComments { CommentsBefore = ["orphaned"] });
            XML.SerializeToString(new Patient { Contact = [withPreceding] })
                .Should().NotContain("<contact").And.Contain("<!--orphaned-->");
        }

        /// <summary>
        /// When the filter empties out every entry of a '_elementName' array, that array holds nothing but
        /// placeholders and is omitted - while the 'elementName' array keeps the placeholders it needs to
        /// position its surviving values.
        /// </summary>
        [TestMethod]
        public void OmitsPrimitiveMetadataArrayWhenFilterEmptiesEveryEntry()
        {
            var name = new HumanName
            {
                GivenElement =
                [
                    new FhirString { ElementId = "first" },
                    new FhirString("middle"),
                    new FhirString { ElementId = "last" }
                ]
            };

            static SerializationFilter filter() => new ElementMetadataFilter { IncludeNames = ["id"], Invert = true };

            JSON.SerializeToString(name, filterFactory: filter)
                .Should().Be("""{"given":[null,"middle",null]}""");
        }
    }
}
