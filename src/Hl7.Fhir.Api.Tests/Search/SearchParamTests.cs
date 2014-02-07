using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Hl7.Fhir.Search;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class SearchParamTests
    {
        [TestMethod]
        public void ParseCriterium()
        {
            var crit = Criterium.Parse("paramX", "18");
            Assert.AreEqual("paramX", crit.ParamName);
            Assert.AreEqual("18", crit.Operand.ToString());
            Assert.AreEqual(Operator.EQ, crit.Type);

            crit = Criterium.Parse("paramX", ">18");
            Assert.AreEqual("paramX", crit.ParamName);
            Assert.AreEqual("18", crit.Operand.ToString());
            Assert.AreEqual(Operator.GT, crit.Type);

            crit = Criterium.Parse("paramX", "~18");
            Assert.AreEqual("paramX", crit.ParamName);
            Assert.AreEqual("18", crit.Operand.ToString());
            Assert.AreEqual(Operator.APPROX, crit.Type);
        }

        [TestMethod]
        public void HandleNumberParam()
        {
            var p1 = new NumberValue(18);
            Assert.AreEqual("18", p1.ToString());

            var p2 = NumberValue.Parse("18");
            Assert.AreEqual(18M, p2.Value);

            var p3 = NumberValue.Parse("18.00");
            Assert.AreEqual(18.00M, p3.Value);

            var crit = Criterium.Parse("paramX", "18.34");
            var p4 = ((UntypedValue)crit.Operand).AsNumberValue();
            Assert.AreEqual(18.34M, p4.Value);
        }

        [TestMethod]
        public void HandleDateParam()
        {
            var p1 = new DateValue(new DateTimeOffset(1972, 11, 30, 15, 20, 49, TimeSpan.Zero));
            Assert.AreEqual("1972-11-30T15:20:49+00:00", p1.ToString());

            var p2 = DateValue.Parse("1972-11-30T18:45:36");
            Assert.AreEqual("1972-11-30T18:45:36", p2.ToString());

            var crit = Criterium.Parse("paramX", "1972-11-30");
            var p3 = ((UntypedValue)crit.Operand).AsDateValue();
            Assert.AreEqual("1972-11-30", p3.Value);
        }


        [TestMethod]
        public void HandleTokenParam()
        {
            var p1 = new TokenValue("NOK", "http://somewhere.nl/codes");
            Assert.AreEqual("http://somewhere.nl/codes|NOK", p1.ToString());

            var p2 = new TokenValue("y|n", "http://some|where.nl/codes");
            Assert.AreEqual(@"http://some\|where.nl/codes|y\|n", p2.ToString());

            var p3 = new TokenValue("NOK", withNamespace: false);
            Assert.AreEqual("NOK", p3.ToString());

            var p4 = new TokenValue("NOK", withNamespace: true);
            Assert.AreEqual("|NOK", p4.ToString());

            //var p4 = TokenParamValue.FromQueryValue("http://somewhere.nl/codes!NOK");
            //Assert.AreEqual("http://somewhere.nl/codes", p4.Namespace);
            //Assert.AreEqual("NOK", p4.Value);
            //Assert.IsTrue(p4.NamespaceSensitive);

            //var p5 = TokenParamValue.FromQueryValue("!NOK");
            //Assert.AreEqual(null, p5.Namespace);
            //Assert.AreEqual("NOK", p5.Value);
            //Assert.IsTrue(p5.NamespaceSensitive);

            //var p6 = TokenParamValue.FromQueryValue("NOK");
            //Assert.AreEqual(null, p6.Namespace);
            //Assert.AreEqual("NOK", p6.Value);
            //Assert.IsFalse(p6.NamespaceSensitive);
        }


        //[TestMethod]
        //public void HandleReferenceParam()
        //{
        //    var p1 = new ReferenceParamValue("patient", "2");
        //    Assert.AreEqual("patient/2", p1.QueryValue);

        //    var p2 = ReferenceParamValue.FromQueryValue("organization/34");
        //    Assert.AreEqual("34", p2.Id);
        //    Assert.AreEqual("organization", p2.ResourceType);
        //}


        //[TestMethod]
        //public void HandleStringParam()
        //{
        //    var p1 = new StringParamValue("patient");
        //    Assert.AreEqual("\"patient\"", p1.QueryValue);

        //    var p2 = StringParamValue.FromQueryValue("\"organization\"");
        //    Assert.AreEqual("organization", p2.Value);
        //}





        //[TestMethod]
        //public void HandleUntypedParam()
        //{
        //    var p1 = new UntypedParamValue("<=18");
        //    Assert.AreEqual("<=18", p1.Value);
        //    Assert.AreEqual(18, p1.AsIntegerParam().Value);
        //}


        //[TestMethod]
        //public void HandleCombinedParam()
        //{
        //    var p1 = new CombinedParamValue(new TokenParamValue("NOK"), new IntegerParamValue(18));
        //    Assert.AreEqual("NOK$18", p1.QueryValue);

        //    var p2 = CombinedParamValue.FromQueryValue("!NOK$>=18");
        //    Assert.AreEqual(2, p2.Values.Count());
        //    Assert.AreEqual("NOK", ((UntypedParamValue)p2.Values.First()).AsTokenParam().Value);
        //    Assert.AreEqual(18, ((UntypedParamValue)p2.Values.Skip(1).First()).AsIntegerParam().Value);
        //}

        //[TestMethod]
        //public void ParseSearchParam()
        //{
        //    var p1 = new SearchParam("dummy", "exact", new IntegerParamValue(ComparisonOperator.LTE,18),
        //            new CombinedParamValue( new StringParamValue("ewout"), new ReferenceParamValue("patient","1")));
        //    Assert.AreEqual("dummy:exact=<=18,\"ewout\"$patient/1", p1.QueryPair);

        //    var p2 = new SearchParam("name", isMissing:true);
        //    Assert.AreEqual("name:missing=true", p2.QueryPair);

        //    var p3 = SearchParam.FromQueryKeyAndValue("dummy:exact", "<=18,\"ewout\"$patient/1");
        //    Assert.AreEqual("dummy", p3.Name);
        //    Assert.AreEqual("exact", p3.Modifier);
        //    Assert.AreEqual(2, p3.Values.Count());
        //    Assert.AreEqual(18, ((UntypedParamValue)p3.Values.First()).AsIntegerParam().Value);
        //    Assert.AreEqual("\"ewout\"$patient/1", ((UntypedParamValue)p3.Values.Skip(1).First()).AsCombinedParam().QueryValue);
        //}
    }
}