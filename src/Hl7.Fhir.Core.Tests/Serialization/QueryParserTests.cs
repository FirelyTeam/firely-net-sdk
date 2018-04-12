/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
	public class FhirParserTests
    {
        [TestMethod]
        public void TestParseQueryFromUriParametersSimple()
        {
            var uriParams = parseParams("_id=1");

            var test = SearchParams.FromUriParamList(uriParams).ToUriParamList();

            Assert.AreEqual("1", test.SingleValue("_id"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersInclude()
        {
            var uriParams = parseParams("_include=Subject");

            var test = SearchParams.FromUriParamList(uriParams);

            Assert.IsTrue(test.Include.Contains("Subject"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersChain()
        {
            var uriParams = parseParams("Observation.Subject.name=Teun");

            var test = SearchParams.FromUriParamList(uriParams);

            Assert.AreEqual("Teun", test.ToUriParamList().SingleValue("Observation.Subject.name"));
        }

        private IEnumerable<Tuple<String, String>> parseParams(string uriParams)
        {
            var splitParams = new Char[] { '&' };
            var splitParts = new Char[] { '=' };
            var result = new List<Tuple<String, String>>();
            foreach (var s in uriParams.Split(splitParams))
            {
                var parts = s.Split(splitParts);
                Tuple<String, String> param = null;
                if (parts.Count() >= 1)
                {
                    param = Tuple.Create<String, String>(parts[0], parts.Count() > 1 ? parts[1] : null);
                    result.Add(param);
                }
            }
            return result;
        }
    }
}
