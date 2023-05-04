using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            /*
            assertValidUsingValidator(c);
            c.Value.Should().BeNull();

            c = new(FilterOperator.DescendentOf);
            assertValidUsingValidator(c);
            c.Value.Should().Be(FilterOperator.DescendentOf);

            c.ObjectValue = null;
            assertValidUsingValidator(c);
            c.Value.Should().BeNull();

            c.ObjectValue = FilterOperator.ChildOf.GetLiteral();
            assertValidUsingValidator(c);
            c.Value.Should().Be(FilterOperator.ChildOf);
            */

            c.ObjectValue = "wrong";
            assertValidUsingValidator(c, e: COVE.INVALID_CODED_VALUE);
            Assert.ThrowsException<InvalidCastException>(() => c.Value);

            c.ObjectValue = 4;
            assertValidUsingValidator(c, e: COVE.INVALID_CODED_VALUE);
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

        private static void assertValidUsingValidator(IValidatableObject o, CodedValidationException? e = null)
        {
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(o, new ValidationContext(o), results, true);

            if (e is null)
            {
                valid.Should().BeTrue();
                results.Should().BeEmpty();
            }
            else
            {
                valid.Should().BeFalse();
                results.Should().AllBeOfType<CodedValidationResult>();
                results.Should().ContainSingle(vr => ((CodedValidationResult)vr).ValidationException.ErrorCode == COVE.POSITIVEINT_LITERAL_INVALID.ErrorCode);
            }
        }

        [TestMethod]
        public void PositiveInt()
        {
            var p = new PositiveInt(null);
            assertValidUsingValidator(p);
            p.Value.Should().BeNull();

            p = new PositiveInt(-12);
            assertValidUsingValidator(p, e: COVE.POSITIVEINT_LITERAL_INVALID);

            p = new PositiveInt(0);
            assertValidUsingValidator(p, e: COVE.POSITIVEINT_LITERAL_INVALID);

            p = new PositiveInt(22);
            assertValidUsingValidator(p);
            p.Value.Should().Be(22);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var p = new Parameters().Add("name", new PositiveInt(121));
            assertValidUsingValidator(p);

        }
    }

}
