using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HL7.FhirPath.Tests
{
    [TestClass]
    public class OperationsTests
    {
        private static IEnumerable<object[]> EmptyCollectionOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                 {
                     ("({} = {}).empty()", true, false),
                     ("(true > {}).empty()", true, false),
                     ("({} != 'dummy').empty()", true, false),
                 }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> EqualityOperatorTestcases() =>
            new (string expression, bool? expected, bool invalid)[]
                 {
                    ("(@2012 + 1 year) = @2013", true, false),
                    ("(@2012-02 + 1 year) = @2013-02", true, false),
                    ("(@2012-02-13 + 13 month) = @2013-03-13", true, false),
                    ("(@2012 + 1 month) = @2012", true, false),
                    ("(@2012 + 12 month) = @2013", true, false),
                    ("(@2012-02-27 + 5 days) = @2012-03-03", true, false),

                    ("(@2014 + 23 months) = @2015", true, false),
                    ("(@2016 + 365 days) = @2017", true, false),
                    ("(@2016-01 + 29 weeks) = @2016-07", true, false),
                    ("(@2016-01 + 30 weeks) = @2016-08", true, false),
                    ("(@2016-01 + 31 weeks) = @2016-08", true, false),

                    ("(@2012-02-13T10:45:31.1 + 30 minutes) = @2012-02-13T11:15:31.1", true, false),
                    ("(@2012-02-13T10:45:31.1 + 25 hours) = @2012-02-14T11:45:31.1", true, false),
                    ("(@2012-02-13T10:45:31.1 + 25 hours + 9 seconds) = @2012-02-14T11:45:40.1", true, false),
                    ("(@2012-02-13T10:45:31.1 + 13 'ms') = @2012-02-13T10:45:31.113", true, false),
                    ("(@2012-02-13T10:45:31.1 + 10 'ms') = @2012-02-13T10:45:31.110", true, false),

                    // https://hl7.org/fhirpath/#time-valued-quantities
                    ("'1 year'.toQuantity() = 1 'a'", null, false),
                    ("'1 day'.toQuantity() = 1 'd'", true, false),
                    ("'1 second'.toQuantity() = 1 's'", true, false),

                    ("'1 year'.toQuantity() ~ 1 'a'", true, false),
                    ("'1 day'.toQuantity() ~ 1 'd'", true, false),
                    ("'1 second'.toQuantity() ~ 1 's'", true, false),

                    ("@2012 = @2012", true, false),
                    ("@2012 = @2013", false, false),
                    ("(@2012-01 = @2012).empty()", true, false),
                    ("@2012-01-01T10:30 = @2012-01-01T10:30", true, false),
                    ("@2012-01-01T10:30 = @2012-01-01T10:31", false, false),
                    ("(@2012-01-01T10:30:31 = @2012-01-01T10:30).empty()", true, false),
                    ("@2012-01-01T10:30:31.0 = @2012-01-01T10:30:31", true, false),
                    ("@2012-01-01T10:30:31.1 = @2012-01-01T10:30:31", false, false)
                 }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> EquivalenceOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                 {
                    ("@2012 ~ @2012", true, false),
                    ("@2012 ~ @2013", false, false),
                    ("@2012-01 ~ @2012", false, false),
                    ("@2012-01-01T10:30 ~ @2012-01-01T10:30", true, false),
                    ("@2012-01-01T10:30 ~ @2012-01-01T10:31", false, false),
                    ("@2012-01-01T10:30:31 ~ @2012-01-01T10:30", false, false),
                    ("@2012-01-01T10:30:31.0 ~ @2012-01-01T10:30:31", true, false),
                    ("@2012-01-01T10:30:31.1 ~ @2012-01-01T10:30:31", false, false)
                 }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> GreaterThanOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                 {
                    ("10 > 5", true, false),
                    ("10 > 5.0", true, false),
                    ("'abc' > 'ABC'", true, false),
                    ("8 'm' > 4 'm'", true, false),
                    ("(4 'm' > 4 'cm')", true, false),
                    ("@2018-03-01 > @2018-01-01", true, false),
                    ("(@2018-03 > @2018-03-01).empty()", true, false),
                    ("@2018-03-01T10:30:00 > @2018-03-01T10:00:00", true, false),
                    ("(@2018-03-01T10 > @2018-03-01T10:30).empty()", true, false),
                    ("@2018-03-01T10:30:00 > @2018-03-01T10:30:00.0", false, false),
                    ("@T10:30:00 > @T10:00:00", true, false),
                    ("(@T10 > @T10:30).empty()", true, false),
                    ("@T10:30:00 > @T10:30:00.0", false, false)
                 }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> LessThanOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                 {
                    ("10 < 5", false, false),
                    ("10 < 5.0", false, false),
                    ("'abc' < 'ABC'", false, false),
                    ("8 'm' < 4 'm'", false, false),
                    ("(4 'm' < 4 'cm')", false, false),
                    ("@2018-03-01 < @2018-01-01", false, false),
                    ("(@2018-03 < @2018-03-01).empty()", true, false),
                    ("@2018-03-01T10:30:00 < @2018-03-01T10:00:00", false, false),
                    ("(@2018-03-01T10 < @2018-03-01T10:30).empty()", true, false),
                    ("@2018-03-01T10:30:00 < @2018-03-01T10:30:00.0", false, false),
                    ("@T10:30:00 < @T10:00:00", false, false),
                    ("(@T10 < @T10:30).empty()", true, false),
                    ("@T10:30:00 < @T10:30:00.0", false, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> LessOrEqualOperatorTestcases() =>
           new (string expression, bool expected, bool invalid)[]
                {
                    ("10 <= 5", false, false),
                    ("10 <= 5.0", false, false),
                    ("'abc' <= 'ABC'", false, false),
                    ("8 'm' <= 4 'm'", false, false),
                    ("(4 'm' <= 4 'cm')", false, false),
                    ("@2018-03-01 <= @2018-01-01", false, false),
                    ("(@2018-03 <= @2018-03-01).empty()", true, false),
                    ("@2018-03-01T10:30:00 <= @2018-03-01T10:00:00", false, false),
                    ("(@2018-03-01T10 <= @2018-03-01T10:30).empty()", true, false),
                    ("@2018-03-01T10:30:00 <= @2018-03-01T10:30:00.0", true, false),
                    ("@T10:30:00 <= @T10:00:00", false, false),
                    ("(@T10 <= @T10:30).empty()", true, false),
                    ("@T10:30:00 <= @T10:30:00.0", true, false)
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> GreaterOrEqualOperatorTestcases() =>
           new (string expression, bool expected, bool invalid)[]
                {
                    ("10 >= 5", true, false),
                    ("10 >= 5.0", true, false),
                    ("'abc' >= 'ABC'", true, false),
                    ("8 'm' >= 4 'm'", true, false),
                    ("(4 'm' >= 4 'cm')", true, false),
                    ("@2018-03-01 >= @2018-01-01", true, false),
                    ("(@2018-03 >= @2018-03-01).empty()", true, false),
                    ("@2018-03-01T10:30:00 >= @2018-03-01T10:00:00", true, false),
                    ("(@2018-03-01T10 >= @2018-03-01T10:30).empty()", true, false),
                    ("@2018-03-01T10:30:00 >= @2018-03-01T10:30:00.0", true, false),
                    ("@T10:30:00 >= @T10:00:00", true, false),
                    ("(@T10 >= @T10:30).empty()", true, false),
                    ("@T10:30:00 >= @T10:30:00.0", true, false)
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> IsTypeOperatorTestcases() =>
           new (string expression, bool expected, bool invalid)[]
                {
                    ("10 is Integer", true, false),
                    ("10.2 is Decimal", true, false),
                    ("true is Boolean", true, false),
                    ("false is Boolean", true, false),
                    ("'string' is String", true, false),
                    ("@2021-01-26 is Date", true, false),
                    ("@2021-01-26T13:30 is DateTime", true, false),
                    ("@T14:30:14.559 is Time", true, false),
                    ("4 'kg' is Quantity", true, false),
                    // backwards compatible with previous implementations of FhirPath
                    ("10.is(Integer)", true, false),
                    ("10.2.is(Decimal)", true, false),
                    ("true.is(Boolean)", true, false),
                    ("false.is(Boolean)", true, false),
                    ("'string'.is(String)", true, false),
                    ("@2021-01-26.is(Date)", true, false),
                    ("@2021-01-26T13:30.is(DateTime)", true, false),
                    ("@T14:30:14.559.is(Time)", true, false),
                    ("(4 'kg').is(Quantity)", true, false),
               }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        private static IEnumerable<object[]> AsTypeOperatorTestcases() =>
            new (string expression, bool expected, bool invalid)[]
                {
                    ("10 as Integer = 10", true, false),
                    ("{}.empty() as Boolean = true", true, false),
                    ("'abcde'.length() as Integer = 5", true, false),
                    // backwards compatible with previous implementations of FhirPath
                    ("10.as(Integer) = 10", true, false),
                    ("{}.empty().as(Boolean) = true", true, false),
                    ("'abcde'.length().as(Integer) = 5", true, false)
                }.Select(t => new object[] { t.expression, t.expected, t.invalid });

        public static IEnumerable<object[]> AllFunctionTestcases()
        {
            return
                Enumerable.Empty<object[]>()
                .Union(EmptyCollectionOperatorTestcases())
                .Union(EqualityOperatorTestcases())
                .Union(EquivalenceOperatorTestcases())
                .Union(GreaterThanOperatorTestcases())
                .Union(LessThanOperatorTestcases())
                .Union(LessOrEqualOperatorTestcases())
                .Union(GreaterOrEqualOperatorTestcases())
                .Union(IsTypeOperatorTestcases())
                .Union(AsTypeOperatorTestcases())
                ;
        }

        [DataTestMethod]
        [DynamicData(nameof(AllFunctionTestcases), DynamicDataSourceType.Method)]
        public void AssertTestcases(string expression, bool? expected, bool invalid = false)
        {
            ITypedElement dummy = ElementNode.ForPrimitive(true);

            if (invalid)
            {
                Action act = () => dummy.IsBoolean(expression, expected.Value);
                act.Should().Throw<Exception>();
            }
            if (expected is null)
            {
                dummy.Scalar(expression).Should().BeNull();
            }
            else
            {
                dummy.IsBoolean(expression, expected.Value)
                    .Should().BeTrue(because: $"The expression was supposed to result in {expected}.");
            }
        }
    }
}
