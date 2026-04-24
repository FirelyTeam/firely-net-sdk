/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class VersionAwarePocoProvTest
    {
        [TestMethod]
        public void TestResourceNameResolving()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            inspector.ImportType(typeof(Way));
            inspector.ImportType(typeof(Way2));

            var way = inspector.FindClassMapping("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual(typeof(Way), way.NativeType);

            var way2 = inspector.FindClassMapping("Way2");
            Assert.IsNotNull(way2);
            Assert.AreEqual(typeof(Way2), way2.NativeType);

            var noway = inspector.FindClassMapping("nonexistent");
            Assert.IsNull(noway);
        }

        [TestMethod]
        public void TestIsBindable()
        {
            ModelInspector.Base.IsBindable("string").Should().BeTrue();
            ModelInspector.Base.IsBindable("uri").Should().BeTrue();
            ModelInspector.Base.IsBindable("Quantity").Should().BeTrue();
            ModelInspector.Base.IsBindable("Extension").Should().BeTrue();
            ModelInspector.Base.IsBindable("Coding").Should().BeTrue();
            ModelInspector.Base.IsBindable("CodeableConcept").Should().BeTrue();
            ModelInspector.Base.IsBindable("CodeableReference").Should().BeTrue();
            ModelInspector.Base.IsBindable("integer").Should().BeFalse();
        }

        [TestMethod]
        public void TestAssemblyInspection()
        {
            var inspector = new ModelInspector(Specification.FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Check for presence of some basic ingredients
            Assert.IsNotNull(inspector.FindClassMapping("Meta"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Code)));
            Assert.IsNotNull(inspector.FindClassMapping("boolean"));

            // Should also have found the abstract classes
            Assert.IsNotNull(inspector.FindClassMapping("Element"));
            Assert.IsNotNull(inspector.FindClassMapping(typeof(Resource)));

            // The open generic Code<> should not be there
            var codeOfT = inspector.FindClassMapping(typeof(Code<>));
            Assert.IsNull(codeOfT);
        }

        [TestMethod]
        public void CanManipulateClassMappingsList()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);

            // Inspect the HL7.Fhir.Model common assembly
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Try to remove a mapping
            var metaMapping = inspector.FindClassMapping("Meta");
            inspector.ClassMappings.Remove(metaMapping);
            inspector.FindClassMapping("Meta").Should().BeNull();

            // And add it back.
            inspector.ClassMappings.Add(metaMapping);
            inspector.FindClassMapping("Meta").Should().NotBeNull();
        }

        /// <summary>
        /// Regression test: types derived from FHIR POCOs (e.g. ValidateCodeParameters which derives
        /// from Parameters) must be importable by ModelInspector even though they do not carry their
        /// own [FhirType] attribute.  The base type's ClassMapping must be returned.
        /// </summary>
        [TestMethod]
        public void FindOrImportClassMappingReturnsMappingForDerivedParametersType()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // ValidateCodeParameters derives from Parameters but has no [FhirType] attribute.
            var mapping = inspector.FindOrImportClassMapping(typeof(ValidateCodeParameters));
            mapping.Should().NotBeNull("a derived FHIR type should fall back to its base type's mapping");
            mapping!.NativeType.Should().Be(typeof(Parameters), "the mapping should belong to the FHIR base type");

            // A second lookup must hit the cache and return the same mapping.
            var mappingAgain = inspector.FindOrImportClassMapping(typeof(ValidateCodeParameters));
            mappingAgain.Should().BeSameAs(mapping);
        }

        /// <summary>
        /// Same as above but for CodeSystemValidateCodeParameters.
        /// </summary>
        [TestMethod]
        public void FindOrImportClassMappingReturnsMappingForDerivedCodeSystemParametersType()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var mapping = inspector.FindOrImportClassMapping(typeof(CodeSystemValidateCodeParameters));
            mapping.Should().NotBeNull();
            mapping!.NativeType.Should().Be(typeof(Parameters));
        }

        /// <summary>
        /// When a base mapping is removed from the inspector, any alias entries that were cached
        /// for derived types must also be removed so that stale lookups are not returned.
        /// </summary>
        [TestMethod]
        public void RemovingBaseMappingAlsoClearsAliasesForDerivedTypes()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            // Prime the alias cache for ValidateCodeParameters → Parameters mapping.
            var alias = inspector.FindOrImportClassMapping(typeof(ValidateCodeParameters));
            alias.Should().NotBeNull();

            // Now remove the Parameters mapping.
            var parametersMapping = inspector.FindClassMapping(typeof(Parameters));
            parametersMapping.Should().NotBeNull();
            inspector.ClassMappings.Remove(parametersMapping!);

            // The alias entry for ValidateCodeParameters must also be gone.
            inspector.FindClassMapping(typeof(ValidateCodeParameters)).Should().BeNull(
                "alias entries for derived types must be cleaned up when the base mapping is removed");
        }

        [TestMethod]
        public void CanImportCustomClassMappingFromStructureDefinitionSummary()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var componentSummary = new TestStructureDefinitionSummary(
                "BackboneElement",
                isAbstract: true,
                isResource: false,
                elements:
                [
                    new TestElementDefinitionSummary("value", [new TestStructureDefinitionReference("string")], order: 10)
                ]);

            var resourceSummary = new TestStructureDefinitionSummary(
                "CustomResource",
                isAbstract: false,
                isResource: true,
                elements:
                [
                    new TestElementDefinitionSummary("identifier", [new TestStructureDefinitionReference("string")], isRequired: true, order: 10),
                    new TestElementDefinitionSummary("component", [componentSummary], isCollection: true, order: 20),
                    new TestElementDefinitionSummary("value", [new TestStructureDefinitionReference("string"), new TestStructureDefinitionReference("boolean")], isChoiceElement: true, order: 30)
                ]);

            var mapping = inspector.Import(resourceSummary, "http://example.org/fhir/StructureDefinition/CustomResource");

            mapping.Name.Should().Be("CustomResource");
            mapping.NativeType.Should().Be(typeof(DynamicResource));
            inspector.FindClassMapping("CustomResource").Should().BeSameAs(mapping);
            inspector.FindClassMappingByCanonical("http://example.org/fhir/StructureDefinition/CustomResource").Should().BeSameAs(mapping);
            inspector.Provide("CustomResource").Should().BeSameAs(mapping);

            var instance = mapping.CreateInstance();
            instance.Should().BeOfType<DynamicResource>();
            ((DynamicResource)instance).DynamicTypeName.Should().Be("CustomResource");

            ((IStructureDefinitionSummary)mapping).TypeName.Should().Be("CustomResource");
            ((IStructureDefinitionSummary)mapping).IsAbstract.Should().BeFalse();

            var identifier = mapping.FindMappedElementByName("identifier");
            identifier.Should().NotBeNull();
            identifier!.IsMandatoryElement.Should().BeTrue();
            identifier.SerializationHint.Should().Be(XmlRepresentation.XmlElement);

            var component = mapping.FindMappedElementByName("component");
            component.Should().NotBeNull();
            component!.IsCollection.Should().BeTrue();
            component.PropertyTypeMapping.Name.Should().Be("CustomResource.component");
            component.PropertyTypeMapping.NativeType.Should().Be(typeof(DynamicDataType));
            ((IStructureDefinitionSummary)component.PropertyTypeMapping).TypeName.Should().Be("BackboneElement");
            ((IStructureDefinitionSummary)component.PropertyTypeMapping).IsAbstract.Should().BeTrue();
            ((IElementDefinitionSummary)component).Type.Single().Should().BeSameAs(component.PropertyTypeMapping);

            var choice = (IElementDefinitionSummary)mapping.FindMappedElementByName("value")!;
            choice.IsChoiceElement.Should().BeTrue();
            choice.Type.Should().OnlyContain(t => t is IStructureDefinitionReference);
            choice.Type.Cast<IStructureDefinitionReference>().Select(t => t.ReferredType)
                .Should().BeEquivalentTo(["string", "boolean"]);
        }

        [TestMethod]
        public void ImportingCustomStructureDefinitionMappingDoesNotOverwriteDynamicRuntimeTypeLookup()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var dynamicResourceMapping = inspector.FindClassMapping(typeof(DynamicResource));
            dynamicResourceMapping.Should().NotBeNull();

            var resourceSummary = new TestStructureDefinitionSummary(
                "AnotherCustomResource",
                isAbstract: false,
                isResource: true,
                elements: []);

            var imported = inspector.Import(resourceSummary);

            inspector.FindClassMapping("AnotherCustomResource").Should().BeSameAs(imported);
            inspector.FindClassMapping(typeof(DynamicResource)).Should().BeSameAs(dynamicResourceMapping,
                "custom mappings should be registered by name/canonical, not by ambiguous runtime type");
        }

        [TestMethod]
        public void ImportingExistingSummaryByCanonicalRegistersCanonicalAlias()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var resourceSummary = new TestStructureDefinitionSummary(
                "CanonicalUpgradeResource",
                isAbstract: false,
                isResource: true,
                elements: []);

            var importedByName = inspector.Import(resourceSummary);
            var importedByCanonical = inspector.Import(resourceSummary, "http://example.org/fhir/StructureDefinition/CanonicalUpgradeResource");

            importedByCanonical.Should().BeSameAs(importedByName);
            inspector.FindClassMapping("CanonicalUpgradeResource").Should().BeSameAs(importedByName);
            inspector.FindClassMappingByCanonical("http://example.org/fhir/StructureDefinition/CanonicalUpgradeResource")
                .Should().BeSameAs(importedByName);
            inspector.Provide("http://example.org/fhir/StructureDefinition/CanonicalUpgradeResource")
                .Should().BeSameAs(importedByName);
        }

        [TestMethod]
        public void CanResolveSummaryPropertyTypeByUrnCanonical()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var referencedSummary = new TestStructureDefinitionSummary(
                "CanonicalDatatype",
                isAbstract: false,
                isResource: false,
                elements: [])
            {
                Canonical = "urn:uuid:0f8fad5b-d9cb-469f-a165-70867728950e"
            };

            inspector.Import(referencedSummary, referencedSummary.Canonical);

            var resourceSummary = new TestStructureDefinitionSummary(
                "UrnReferenceResource",
                isAbstract: false,
                isResource: true,
                elements:
                [
                    new TestElementDefinitionSummary("value", [new TestStructureDefinitionReference(referencedSummary.Canonical)], order: 10)
                ]);

            var mapping = inspector.Import(resourceSummary, "http://example.org/fhir/StructureDefinition/UrnReferenceResource");
            var value = mapping.FindMappedElementByName("value");
            var serializedType = ((IElementDefinitionSummary)value!).Type.Single();

            value.Should().NotBeNull();
            value!.PropertyTypeMapping.Should().BeSameAs(inspector.FindClassMappingByCanonical(referencedSummary.Canonical));
            serializedType.Should().BeAssignableTo<IStructureDefinitionReference>();
            ((IStructureDefinitionReference)serializedType).ReferredType.Should().Be(referencedSummary.Canonical);
        }

        [TestMethod]
        public void SerializingCustomDynamicResourceShouldUseImportedChoiceMapping()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var resourceSummary = new TestStructureDefinitionSummary(
                "CustomChoiceResource",
                isAbstract: false,
                isResource: true,
                elements:
                [
                    new TestElementDefinitionSummary(
                        "value",
                        [new TestStructureDefinitionReference("string"), new TestStructureDefinitionReference("boolean")],
                        isChoiceElement: true,
                        order: 10)
                ]);

            var mapping = inspector.Import(resourceSummary, "http://example.org/fhir/StructureDefinition/CustomChoiceResource");
            var instance = (DynamicResource)mapping.CreateInstance();
            instance.SetValue("value", new FhirBoolean(true));

            var json = new BaseFhirJsonSerializer(inspector).SerializeToString(instance);

            json.Should().Contain("\"resourceType\":\"CustomChoiceResource\"");
            json.Should().Contain("\"valueBoolean\":true",
                "the imported custom ClassMapping declares value[x], so the serializer should suffix the property name using that mapping");
            json.Should().NotContain("\"value\":true",
                "falling back to the generic DynamicResource mapping drops the choice metadata even though DynamicTypeName is correct");
        }

        [TestMethod]
        public void ToTypedElementLegacyShouldExposeImportedCustomDefinition()
        {
            var inspector = new ModelInspector(FhirRelease.STU3);
            inspector.Import(typeof(Resource).GetTypeInfo().Assembly);

            var resourceSummary = new TestStructureDefinitionSummary(
                "LegacyCustomResource",
                isAbstract: false,
                isResource: true,
                elements:
                [
                    new TestElementDefinitionSummary("flag", [new TestStructureDefinitionReference("boolean")], order: 10)
                ]);

            var mapping = inspector.Import(resourceSummary, "http://example.org/fhir/StructureDefinition/LegacyCustomResource");
            var instance = (DynamicResource)mapping.CreateInstance();
            instance.SetValue("flag", new FhirBoolean(true));

            var typed = instance.ToTypedElementLegacy(inspector);

            typed.InstanceType.Should().Be("LegacyCustomResource",
                "legacy POCO-to-typed-element conversion should preserve the imported custom mapping identity instead of falling back to DynamicResource");
            typed.Children("flag").Single().InstanceType.Should().Be("boolean",
                "child definitions should come from the imported custom mapping so custom elements remain navigable");
        }
    }

    internal sealed class TestStructureDefinitionSummary : IStructureDefinitionSummary
    {
        private readonly IReadOnlyCollection<IElementDefinitionSummary> _elements;

        public TestStructureDefinitionSummary(string typeName, bool isAbstract, bool isResource, IReadOnlyCollection<IElementDefinitionSummary> elements)
        {
            TypeName = typeName;
            IsAbstract = isAbstract;
            IsResource = isResource;
            _elements = elements;
        }

        public string TypeName { get; }

        public string Canonical { get; init; }

        public bool IsAbstract { get; }

        public bool IsResource { get; }

        public IReadOnlyCollection<IElementDefinitionSummary> GetElements() => _elements;
    }

    internal sealed class TestElementDefinitionSummary : IElementDefinitionSummary
    {
        public TestElementDefinitionSummary(string elementName, ITypeSerializationInfo[] type, bool isCollection = false,
            bool isRequired = false, bool inSummary = false, bool isChoiceElement = false, bool isResource = false,
            bool isModifier = false, string defaultTypeName = null, string nonDefaultNamespace = null,
            XmlRepresentation representation = XmlRepresentation.XmlElement, int order = 0)
        {
            ElementName = elementName;
            Type = type;
            IsCollection = isCollection;
            IsRequired = isRequired;
            InSummary = inSummary;
            IsChoiceElement = isChoiceElement;
            IsResource = isResource;
            IsModifier = isModifier;
            DefaultTypeName = defaultTypeName;
            NonDefaultNamespace = nonDefaultNamespace;
            Representation = representation;
            Order = order;
        }

        public string ElementName { get; }
        public bool IsCollection { get; }
        public bool IsRequired { get; }
        public bool InSummary { get; }
        public bool IsChoiceElement { get; }
        public bool IsResource { get; }
        public bool IsModifier { get; }
        public ITypeSerializationInfo[] Type { get; }
        public string DefaultTypeName { get; }
        public string NonDefaultNamespace { get; }
        public XmlRepresentation Representation { get; }
        public int Order { get; }
    }

    internal sealed class TestStructureDefinitionReference(string referredType) : IStructureDefinitionReference
    {
        public string ReferredType { get; } = referredType;
    }

    [FhirEnumeration("SomeEnum")]
    public enum SomeEnum { Member, AnotherMember }

    public class ActResource
    {
        [FhirEnumeration("SomeOtherEnum")]
        public enum SomeOtherEnum { Member, AnotherMember }
    }
}
