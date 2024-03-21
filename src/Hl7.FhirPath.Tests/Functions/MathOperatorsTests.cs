using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Hl7.FhirPath.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace HL7.FhirPath.Tests.Functions
{
    [TestClass]
    public class MathOperatorsTests
    {
        [TestMethod]
        public void Sqrt()
        {
            (-1.0m).Sqrt().Should().BeNull();

            4.0m.Sqrt().Should().BeOfType(typeof(decimal));
        }

        [TestMethod]
        public void Power()
        {
            (-1.0m).Power(0.5m).Should().BeNull();

            2m.Power(2m).Should().BeOfType(typeof(decimal));
        }

        private static IEnumerable<object[]> QuantityAddOperations() =>
           new (string expression, bool expected, bool invalid)[]
                {
                     ("25 'kg' + 5 'kg' = 30 'kg'", true, false),
                     ("3 '[in_i]' + 2 '[in_i]' = 5 '[in_i]'", true, false),
                     ("3 'm' + 0 'cm' = 300 'cm'", true, false),
                     ("(3 'm' + 0 'kg').empty()", true, false),
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> QuantitySubstractOperations() =>
          new (string expression, bool expected, bool invalid)[]
               {
                     ("25 'kg' - 500 'g' = 24500 'g'", true, false),
                     ("25 'kg' - 25001 'g' = -1 'g'", true, false),
                     ("1 '[in_i]' - 2 'cm' = 0.005400 'm'", true, false),
                     ("(3 '[in_i]' - 0 'kg').empty()", true, false),
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> QuantityMultiplyOperations() =>
          new (string expression, bool expected, bool invalid)[]
               {
                     ("25 'km' * 20 'cm' = 5000 'm2'", true, false),
                     ("2 'cm' * 2 'm' = 0.040 'm2'", true, false),
                     ("2 'cm' * 9 'kg' = 180 'g.m'", true, false),
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> QuantityDivideOperations() =>
          new (string expression, bool expected, bool invalid)[]
               {
                     ("14.4 'km' / 2 'h' = 2 'm.s-1'", true, false),
                     ("9 'm2' / 3 'm' = 3 'm'", true, false),
                     ("6 'm' / 3 'm' = 2 '1'", true, false),
                     ("(3 'm' / 0 'cm').empty()", true, false),
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> AllQuantityOperations()
        {
            return
                Enumerable.Empty<object[]>()
                .Union(QuantityAddOperations())
                .Union(QuantitySubstractOperations())
                .Union(QuantityMultiplyOperations())
                .Union(QuantityDivideOperations())
                ;
        }

        [DataTestMethod]
        [DynamicData(nameof(AllQuantityOperations), DynamicDataSourceType.Method)]
        public void AssertTestcases(string expression, bool expected, bool invalid = false)
        {
            ITypedElement dummy = ElementNode.ForPrimitive(true);

            if (invalid)
            {
                Action act = () => dummy.IsBoolean(expression, expected);
                act.Should().Throw<Exception>();
            }
            else
            {
                dummy.IsBoolean(expression, expected)
                    .Should().BeTrue(because: $"The expression was supposed to result in {expected}.");
            }
        }
    }


}
#nullable restore