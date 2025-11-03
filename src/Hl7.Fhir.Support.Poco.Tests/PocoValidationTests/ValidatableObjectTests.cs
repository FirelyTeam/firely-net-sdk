using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests
{

    [TestClass]
    public class ValidatableObjectTests
    {
        [TestMethod]
        public void TestCodeOfT()
        {
            var c = new Code<FilterOperator>(null);
            assertValid(c);
            c.Value.Should().BeNull();

            c = new(FilterOperator.DescendentOf);
            assertValid(c);
            c.Value.Should().Be(FilterOperator.DescendentOf);

            c.ObjectValue = null;
            assertValid(c);
            c.Value.Should().BeNull();

            c.ObjectValue = FilterOperator.ChildOf.GetLiteral();
            assertValid(c);
            c.Value.Should().Be(FilterOperator.ChildOf);

            c.ObjectValue = "wrong";
            assertValid(c, errorCode: COVE.INVALID_CODED_VALUE_CODE);
            Assert.ThrowsException<InvalidCastException>(() => c.Value);

            c.ObjectValue = 4;
            assertValid(c, errorCode: COVE.INVALID_CODED_VALUE_CODE);
            Assert.ThrowsException<InvalidCastException>(() => c.Value);
        }


        private static void assertValid(IValidatableObject o, string? errorCode = null)
        {
            var validationResult = o.Validate(new ValidationContext(o));
            if (errorCode is null)
                validationResult.Should().BeEmpty();
            else
            {
                validationResult.Should().AllBeOfType<CodedValidationResult>();
                validationResult.Should().ContainSingle(vr => ((CodedValidationResult)vr).ValidationException.ErrorCode == errorCode);
            }
        }
        
        [TestMethod]
        public void Test()
        {
            var bundle = new Patient{Name = new () {new () {Family = ""}}};
            const string bundleString = """{ "resourceType":"Patient", "name":[{"family":""}] }""";
            var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);
            
            var shouldThrowOnValidate = () => bundle.Validate(true);
            var shouldThrowOnDeserialize = () => _ = JsonSerializer.Deserialize<Bundle>(bundleString, options);
            
            shouldThrowOnValidate.Should().Throw<ValidationException>();
            shouldThrowOnDeserialize.Should().Throw<DeserializationFailedException>();
        }
    }

}