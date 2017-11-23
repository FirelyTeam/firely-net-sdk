using Hl7.Fhir.Model.Primitives;
using System.Collections.Generic;
using Xunit;

namespace Hl7.Fhir.Support.Tests.Model
{
    public class PartialDateTimeTest
    {
        [Theory]
        [MemberData(nameof(GetTestdata))]
        public void OperatorsTest(string leftOperand, string operation, string rightOperand, bool expectedResult)
        {
            PartialDateTime l, r;

            l = PartialDateTime.Parse(leftOperand);
            r = PartialDateTime.Parse(rightOperand);

            bool? result = null;

            switch (operation)
            {
                case ">":
                    result = l > r;
                    break;
                case ">=":
                    result = l >= r;
                    break;
                case "<":
                    result = l < r;
                    break;
                case "<=":
                    result = l <= r;
                    break;
                case "==":
                    result = l == r;
                    break;
                case "!=":
                    result = l != r;
                    break;

                default:
                    break;
            }

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        private static IEnumerable<object[]> GetTestdata()
        {
            return new List<object[]>()
            {
                new object[]{ "0002", ">", "0001",  true },
                new object[]{ "0001", ">", "0001",  false },
                new object[]{ "0001", ">=", "0001",  true},
                new object[]{ "0001", ">", "0001",  false },
                new object[]{ "2017-11-21T16:03:12", ">", "2017-11-21T16:03:11",  true },
                new object[]{ "2017-11-21T16:03:12+01:00", ">", "2017-11-21T17:03:11+02:00",  true },
                new object[]{ "2017-11-21T16:03:11+01:00", "==", "2017-11-21T17:03:11+02:00",  true },
                new object[]{ "2018-01-01", "==", "2018-01-01",  true },
                new object[]{ "2018-01", "<", "2018-02",  true },
            };
        }
    }
}
