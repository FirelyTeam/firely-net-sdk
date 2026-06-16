/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Tests.Introspection
{
    /// <summary>
    /// Tests attribute-based validation of instances of custom types, using the custom model
    /// from <see cref="CustomTestModel"/>.
    /// </summary>
    [TestClass]
    public class CustomMappingValidationTests
    {
        [TestMethod]
        public void ValidCustomResourceInstanceValidatesCleanly()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            instance.Validate(inspector).Should().BeEmpty();
        }

        [TestMethod]
        public void ChoiceAcceptsCoreTypeFromTheChoiceList()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            instance.SetValue("value", new FhirString("plain"));
            instance.Validate(inspector).Should().BeEmpty();
        }

        [TestMethod]
        public void ChoiceRejectsOtherRegisteredCustomType()
        {
            var inspector = CustomTestModel.Create();
            var stringMapping = inspector.FindClassMapping("string")!;
            var other = new ClassMapping(inspector, "OtherCustomDatatype", typeof(DynamicDataType), dt =>
            [
                new PropertyMapping(dt, "note", typeof(FhirString), [stringMapping]) { Order = 10 }
            ]);
            inspector.ClassMappings.Add(other);

            var otherInstance = other.CreateInstance();
            otherInstance.SetValue("note", new FhirString("hi"));

            var instance = CustomTestModel.CreateValidInstance(inspector);
            instance.SetValue("value", otherInstance);

            instance.Validate(inspector).Should().ContainSingle()
                .Which.ErrorCode.Should().Be(CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE);
        }

        [TestMethod]
        public void ChoiceRejectsUnregisteredDynamicType()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            instance.SetValue("value", new DynamicDataType { DynamicTypeName = "NotRegistered" });

            // The instance itself will report more issues (it is unregistered, so its contents
            // cannot be valid either way) - we just verify the choice validation rejected it.
            instance.Validate(inspector).Select(e => e.ErrorCode)
                .Should().Contain(CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE);
        }

        [TestMethod]
        public void MissingMandatoryElementIsReported()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            instance.SetValue("identifier", null);

            instance.Validate(inspector).Should().ContainSingle()
                .Which.ErrorCode.Should().Be(CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
        }

        [TestMethod]
        public void UnknownElementOnCustomResourceIsReported()
        {
            var inspector = CustomTestModel.Create();
            var instance = CustomTestModel.CreateValidInstance(inspector);

            instance.SetValue("bogus", new FhirString("nope"));

            instance.Validate(inspector).Should().ContainSingle()
                .Which.ErrorCode.Should().Be(CodedValidationException.UNKNOWN_ELEMENT_CODE);
        }

        [TestMethod]
        public void UnregisteredDynamicResourceIsStillReportedAsUnknown()
        {
            var inspector = CustomTestModel.Create();
            var stray = new DynamicResource { DynamicTypeName = "NotRegisteredResource" };

            stray.Validate(inspector).Select(e => e.ErrorCode)
                .Should().Contain(CodedValidationException.UNKNOWN_RESOURCE_TYPE_CODE);
        }

        [TestMethod]
        public void OpenChoiceAcceptsRegisteredCustomTypeAndOpenTypes()
        {
            var inspector = CustomTestModel.Create();
            var openResource = addOpenChoiceResource(inspector);
            var custom = inspector.FindClassMapping("MyCustomDatatype")!.CreateInstance();
            custom.SetValue("unit", new FhirString("kg"));

            var instance = (DynamicResource)openResource.CreateInstance();
            instance.SetValue("anything", custom);
            instance.Validate(inspector).Should().BeEmpty();

            instance.SetValue("anything", new FhirBoolean(true));
            instance.Validate(inspector).Should().BeEmpty();
        }

        [TestMethod]
        public void OpenChoiceRejectsUnregisteredDynamicType()
        {
            var inspector = CustomTestModel.Create();
            var openResource = addOpenChoiceResource(inspector);

            var instance = (DynamicResource)openResource.CreateInstance();
            instance.SetValue("anything", new DynamicDataType { DynamicTypeName = "NotRegistered" });

            instance.Validate(inspector).Select(e => e.ErrorCode)
                .Should().Contain(CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE);
        }

        [TestMethod]
        public void MixedTypeAndNameBasedAllowedTypesActAsUnion()
        {
            var inspector = CustomTestModel.Create();
            var mixedResource = new ClassMapping(inspector, "MixedChoiceResource", typeof(DynamicResource), r =>
            [
                new PropertyMapping(r, "value", typeof(DataType), [inspector.FindClassMapping("DataType")!])
                {
                    Order = 10,
                    Choice = ChoiceType.DatatypeChoice,
                    ValidationAttributes = [new AllowedTypesAttribute([typeof(FhirString)], ["MyCustomDatatype"])]
                }
            ]);
            inspector.ClassMappings.Add(mixedResource);

            var instance = (DynamicResource)mixedResource.CreateInstance();

            // matched by the .NET type list
            instance.SetValue("value", new FhirString("plain"));
            instance.Validate(inspector).Should().BeEmpty();

            // matched by the type name list
            var custom = inspector.FindClassMapping("MyCustomDatatype")!.CreateInstance();
            custom.SetValue("unit", new FhirString("kg"));
            instance.SetValue("value", custom);
            instance.Validate(inspector).Should().BeEmpty();

            // matched by neither list
            instance.SetValue("value", new FhirBoolean(true));
            instance.Validate(inspector).Should().ContainSingle()
                .Which.ErrorCode.Should().Be(CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE);
        }

        [TestMethod]
        public void ReflectedAllowedTypesValidationIsUnchanged()
        {
            var inspector = CustomTestModel.Create();

            var ext = new Extension { Url = "http://example.org/ext", Value = new FhirBoolean(true) };
            ext.Validate(inspector).Should().BeEmpty();
        }

        private static ClassMapping addOpenChoiceResource(ModelInspector inspector)
        {
            var openResource = new ClassMapping(inspector, "OpenChoiceResource", typeof(DynamicResource), r =>
            [
                new PropertyMapping(r, "anything", typeof(DataType), [inspector.FindClassMapping("DataType")!])
                {
                    Order = 10,
                    Choice = ChoiceType.DatatypeChoice,
                    ValidationAttributes = [new AllowedTypesAttribute(true)]
                }
            ]);
            inspector.ClassMappings.Add(openResource);

            return openResource;
        }
    }
}
