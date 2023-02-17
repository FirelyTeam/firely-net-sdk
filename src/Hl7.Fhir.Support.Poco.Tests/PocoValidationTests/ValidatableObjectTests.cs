using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
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
            assertValid(c, e: COVE.INVALID_CODED_VALUE);
            Assert.ThrowsException<InvalidCastException>(() => c.Value);

            c.ObjectValue = 4;
            assertValid(c, e: COVE.INVALID_CODED_VALUE);
            Assert.ThrowsException<InvalidCastException>(() => c.Value);
        }


        private static void assertValid(IValidatableObject o, CodedValidationException? e = null)
        {
            var validationResult = o.Validate(new ValidationContext(o));
            if (e is null)
                validationResult.Should().BeEmpty();
            else
            {
                validationResult.Should().AllBeOfType<CodedValidationResult>();
                validationResult.Should().ContainSingle(vr => ((CodedValidationResult)vr).ValidationException.ErrorCode == e.ErrorCode);
            }
        }
    }

}
