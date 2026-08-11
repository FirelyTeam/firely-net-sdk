/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Tests.Introspection
{
    /// <summary>
    /// Builds a small custom model, expressed purely in terms of ClassMappings and
    /// PropertyMappings (as an external user, e.g. a server hosting custom resources, would):
    /// a custom datatype, and a custom resource with a backbone element and a choice element
    /// for which one of the choices is the custom datatype.
    /// </summary>
    internal static class CustomTestModel
    {
        public const string DATATYPE_CANONICAL = "http://example.org/fhir/StructureDefinition/MyCustomDatatype";
        public const string RESOURCE_CANONICAL = "http://example.org/fhir/StructureDefinition/MyCustomResource";

        public static ModelInspector Create()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var stringMapping = inspector.FindClassMapping("string")!;
            var booleanMapping = inspector.FindClassMapping("boolean")!;
            var integerMapping = inspector.FindClassMapping("integer")!;

            var datatype = new ClassMapping(inspector, "MyCustomDatatype", typeof(DynamicDataType), dt =>
            [
                new PropertyMapping(dt, "unit", typeof(FhirString), [stringMapping]) { Order = 10 },
                new PropertyMapping(dt, "score", typeof(Integer), [integerMapping]) { Order = 20 }
            ])
            {
                Canonical = DATATYPE_CANONICAL,
                IsAbstract = false
            };

            ClassMapping component = null!;
            component = new ClassMapping(inspector, "MyCustomResource.component", typeof(DynamicDataType), c =>
            [
                new PropertyMapping(c, "flag", typeof(FhirBoolean), [booleanMapping]) { Order = 10 },
                new PropertyMapping(c, "data", typeof(DynamicDataType), [datatype]) { Order = 20 }
            ])
            {
                IsBackboneType = true
            };

            var resource = new ClassMapping(inspector, "MyCustomResource", typeof(DynamicResource), r =>
            [
                new PropertyMapping(r, "identifier", typeof(FhirString), [stringMapping])
                {
                    Order = 10,
                    IsMandatoryElement = true,
                    ValidationAttributes = [new CardinalityAttribute { Min = 1 }]
                },
                new PropertyMapping(r, "component", typeof(List<DynamicDataType>), [component]) { Order = 20 },
                new PropertyMapping(r, "value", typeof(DataType), [stringMapping, datatype]) { Order = 30 }
            ])
            {
                Canonical = RESOURCE_CANONICAL
            };

            inspector.ClassMappings.Add(datatype);
            inspector.ClassMappings.Add(component);
            inspector.ClassMappings.Add(resource);

            return inspector;
        }

        public static DynamicResource CreateValidInstance(ModelInspector inspector)
        {
            var datatype = inspector.FindClassMapping("MyCustomDatatype")!;
            var component = inspector.FindClassMapping("MyCustomResource.component")!;
            var resource = inspector.FindClassMapping("MyCustomResource")!;

            var data = (DynamicDataType)datatype.CreateInstance();
            data.SetValue("unit", new FhirString("kg"));
            data.SetValue("score", new Integer(5));

            var comp = (DynamicDataType)component.CreateInstance();
            comp.SetValue("flag", new FhirBoolean(true));
            comp.SetValue("data", data);

            var choiceValue = (DynamicDataType)datatype.CreateInstance();
            choiceValue.SetValue("unit", new FhirString("g"));
            choiceValue.SetValue("score", new Integer(3));

            var instance = (DynamicResource)resource.CreateInstance();
            instance.SetValue("identifier", new FhirString("id-1"));
            instance.SetValue("component", new List<DynamicDataType> { comp });
            instance.SetValue("value", choiceValue);

            return instance;
        }
    }

    [TestClass]
    public class CustomClassMappingTests
    {
        [TestMethod]
        public void CustomMappingsExposeCorrectStructureDefinitionSummaries()
        {
            var inspector = CustomTestModel.Create();

            var resource = inspector.FindClassMapping("MyCustomResource");
            resource.Should().NotBeNull();
            inspector.FindClassMappingByCanonical(CustomTestModel.RESOURCE_CANONICAL).Should().BeSameAs(resource);
            inspector.Provide("MyCustomResource").Should().BeSameAs(resource);

            // Custom mappings must not hijack the type-based lookup for the shared dynamic types.
            inspector.FindClassMapping(typeof(DynamicResource))!.Name.Should().Be("DynamicResource");
            inspector.FindClassMapping(typeof(DynamicDataType))!.Name.Should().Be("DynamicDataType");

            var summary = (IStructureDefinitionSummary)resource!;
            summary.TypeName.Should().Be("MyCustomResource");
            summary.IsAbstract.Should().BeFalse();
            summary.IsResource.Should().BeTrue();

            var elements = summary.GetElements();
            elements.Select(e => e.ElementName).Should().Equal("identifier", "component", "value");

            var identifier = elements.Single(e => e.ElementName == "identifier");
            identifier.IsRequired.Should().BeTrue();
            identifier.Type.Single().Should().BeAssignableTo<IStructureDefinitionReference>()
                .Which.ReferredType.Should().Be("string");

            var component = elements.Single(e => e.ElementName == "component");
            component.IsCollection.Should().BeTrue();
            var componentSummary = component.Type.Single().Should().BeAssignableTo<IStructureDefinitionSummary>().Subject;
            componentSummary.Should().BeSameAs(inspector.FindClassMapping("MyCustomResource.component"));
            componentSummary.TypeName.Should().Be("BackboneElement");
            componentSummary.IsAbstract.Should().BeTrue();
            componentSummary.GetElements().Select(e => e.ElementName).Should().Equal("flag", "data");

            var choice = elements.Single(e => e.ElementName == "value");
            choice.IsChoiceElement.Should().BeTrue();
            // Type references are always by type name (never by canonical), since consumers
            // match them against e.g. the type suffix of a choice element.
            choice.Type.Cast<IStructureDefinitionReference>().Select(t => t.ReferredType)
                .Should().Equal("string", "MyCustomDatatype");
        }

        [TestMethod]
        public void CustomChoicePropertyExposesFhirTypeMappings()
        {
            var inspector = CustomTestModel.Create();
            var resource = inspector.FindClassMapping("MyCustomResource")!;

            var choice = resource.FindMappedElementByName("value")!;
            choice.Choice.Should().Be(ChoiceType.DatatypeChoice);
            choice.FhirTypeMappings.Select(tm => tm.Name).Should().Equal("string", "MyCustomDatatype");

            // The legacy .NET type based view degrades to the native (dynamic) types.
            choice.FhirType.Should().Equal(typeof(FhirString), typeof(DynamicDataType));

            // A name-based AllowedTypes validation has been synthesized from the mappings.
            choice.ValidationAttributes.OfType<AllowedTypesAttribute>().Single()
                .TypeNames.Should().Equal("string", "MyCustomDatatype");
        }

        [TestMethod]
        public void ReflectedPropertyExposesFhirTypeMappings()
        {
            var inspector = CustomTestModel.Create();

            var coding = inspector.FindClassMapping("Coding")!;
            var system = coding.FindMappedElementByName("system")!;
            system.FhirTypeMappings.Single().Should().BeSameAs(inspector.FindClassMapping("uri"));
            system.FhirType.Should().Equal(typeof(FhirUri));

            // The open choice on Extension.value still presents itself as a "DataType" reference.
            var extension = inspector.FindClassMapping("Extension")!;
            var value = (IElementDefinitionSummary)extension.FindMappedElementByName("value")!;
            value.Type.Single().Should().BeAssignableTo<IStructureDefinitionReference>()
                .Which.ReferredType.Should().Be("DataType");
        }

        [TestMethod]
        public void CustomResourceRoundtripsThroughJson()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            var json = new BaseFhirJsonSerializer(inspector).SerializeToString(instance);

            json.Should().Contain("\"resourceType\":\"MyCustomResource\"");
            json.Should().Contain("\"valueMyCustomDatatype\":");

            var parsed = new BaseFhirJsonDeserializer(inspector).DeserializeResource(json);
            assertParsedCustomResource(parsed);

            new BaseFhirJsonSerializer(inspector).SerializeToString(parsed).Should().Be(json);
        }

        [TestMethod]
        public void CustomResourceRoundtripsThroughXml()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            var xml = new BaseFhirXmlSerializer(inspector).SerializeToString(instance);

            xml.Should().Contain("<MyCustomResource");
            xml.Should().Contain("<valueMyCustomDatatype");

            var parsed = new BaseFhirXmlDeserializer(inspector).DeserializeResource(xml);
            assertParsedCustomResource(parsed);

            new BaseFhirXmlSerializer(inspector).SerializeToString(parsed).Should().Be(xml);
        }

        private static void assertParsedCustomResource(Resource parsed)
        {
            var custom = parsed.Should().BeOfType<DynamicResource>().Subject;
            custom.DynamicTypeName.Should().Be("MyCustomResource");

            custom.TryGetValue("value", out var choiceValue).Should().BeTrue();
            choiceValue.Should().BeOfType<DynamicDataType>()
                .Which.DynamicTypeName.Should().Be("MyCustomDatatype");

            custom.TryGetValue("component", out var components).Should().BeTrue();
            var component = ((IEnumerable<Base>)components!).Single();
            component.TryGetValue("data", out var data).Should().BeTrue();
            data.Should().BeOfType<DynamicDataType>()
                .Which.DynamicTypeName.Should().Be("MyCustomDatatype",
                    "a non-choice element of a custom type should keep the custom type identity when parsed");
        }

        [TestMethod]
        public void MandatoryElementsAreDerivedFromTheAttributesTheMappingWasBuiltWith()
        {
            var inspector = CustomTestModel.Create();
            var resource = inspector.FindClassMapping("MyCustomResource")!;

            // "identifier" is the only element of the custom resource declared with a minimum cardinality,
            // so it is the only one the validator needs to check the presence of.
            resource.MandatoryElements.Select(pm => pm.Name).Should().Equal("identifier");
            var identifier = resource.FindMappedElementByName("identifier")!;
            identifier.MandatoryCardinality.Should().BeEquivalentTo(identifier.ValidationAttributes);

            var withoutIdentifier = (DynamicResource)resource.CreateInstance();
            withoutIdentifier.SetValue("value", new FhirString("something"));
            validate(withoutIdentifier, resource, inspector).Should().ContainSingle()
                .Which.Should().Be(CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

            validate(CustomTestModel.CreateValidInstance(inspector), resource, inspector).Should().BeEmpty();
        }

        [TestMethod]
        public void MandatoryElementMetadataIsASnapshotOfTheMappingsAttributes()
        {
            // The metadata on a mapping is derived from its validation attributes when the mapping is
            // constructed - IsMandatoryElement has always worked that way, and MandatoryElements/
            // MandatoryCardinality do too. Mutating an attribute after handing it to a mapping is
            // therefore not supported, and not observed by the validator: this test pins that contract.
            var inspector = CustomTestModel.Create();
            var stringMapping = inspector.FindClassMapping("string")!;
            var cardinality = new CardinalityAttribute { Min = 0, Max = 1 };

            var resource = new ClassMapping(inspector, "MyMutableResource", typeof(DynamicResource), r =>
            [
                new PropertyMapping(r, "identifier", typeof(FhirString), [stringMapping])
                {
                    Order = 10,
                    ValidationAttributes = [cardinality]
                },
                new PropertyMapping(r, "note", typeof(FhirString), [stringMapping]) { Order = 20 }
            ])
            {
                Canonical = "http://example.org/fhir/StructureDefinition/MyMutableResource"
            };
            inspector.ClassMappings.Add(resource);

            var identifier = resource.FindMappedElementByName("identifier")!;
            identifier.MandatoryCardinality.Should().BeEmpty();

            cardinality.Min = 1;

            identifier.MandatoryCardinality.Should().BeEmpty();
            identifier.IsMandatoryElement.Should().BeFalse();
            resource.MandatoryElements.Should().BeEmpty();

            var withoutIdentifier = (DynamicResource)resource.CreateInstance();
            withoutIdentifier.SetValue("note", new FhirString("something"));
            validate(withoutIdentifier, resource, inspector).Should()
                .NotContain(CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE,
                    "'identifier' was not mandatory when the mapping was built");
        }

        private static IReadOnlyList<string> validate(Base instance, ClassMapping mapping, ModelInspector inspector) =>
            FhirAttributeValidator.Default
                .ValidateObject(instance, mapping,
                    new PocoValidationContext(instance, inspector, () => "", 0, 0, NarrativeValidationKind.FhirXhtml))
                .Select(e => e.ErrorCode)
                .ToList();

        [TestMethod]
        public void TypedElementOnCustomPocoExposesCustomTypes()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            var typed = instance.ToTypedElementLegacy(inspector);

            typed.InstanceType.Should().Be("MyCustomResource");

            var component = typed.Children("component").Single();
            component.InstanceType.Should().Be("BackboneElement");
            component.Children("data").Single().InstanceType.Should().Be("MyCustomDatatype",
                "non-choice elements should take their instance type from the declared custom mapping");

            typed.Children("value").Single().InstanceType.Should().Be("MyCustomDatatype",
                "choice elements should take their instance type from the instance's dynamic type");
        }
    }
}
