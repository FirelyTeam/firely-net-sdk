using Hl7.Fhir.Model.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Support.Tests.Model
{
    [TestClass]
    public class PartialDateTimeTest
    {
        private void AssertOperatorsTest(string leftOperand, string operation, string rightOperand, bool expectedResult)
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

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
        }

        private IEnumerable<(string leftop, string op, string rightop, bool result)> GetTestdata()
        {
            yield return ( "0002", ">", "0001",  true );
            yield return ( "0001", ">", "0001",  false );
            yield return ( "0001", ">=", "0001",  true);
            yield return ( "0001", ">", "0001",  false );
            yield return ("2017-11-21T16:03:12", ">", "2017-11-21T16:03:11",  true );
            yield return ( "2017-11-21T16:03:12+01:00", ">", "2017-11-21T17:03:11+02:00",  true );
            yield return ( "2017-11-21T16:03:11+01:00", "==", "2017-11-21T17:03:11+02:00",  true );
            yield return ( "2018-01-01", "==", "2018-01-01",  true );
            yield return ( "2018-01", "<", "2018-02",  true );
        }

        [TestMethod]
        public void OperatorsTest()
        {
            foreach (var item in GetTestdata())
            {
                AssertOperatorsTest(item.leftop, item.op, item.rightop, item.result);
            }
        }
    }
}
