/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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

            var test = SearchParams.FromUriParamList(uriParams).ToUriParamList(Model.Version.R4);

            Assert.AreEqual("1", test.SingleValue("_id"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersInclude()
        {
            var uriParams = parseParams("_include=Subject");

            var test = SearchParams.FromUriParamList(uriParams);

            Assert.IsTrue(test.Include.Any(t => t.Item1 == "Subject"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersIterativeInclude()
        {
            var uriParams = parseParams("_include=Claim:encounter&_include:iterate=Encounter:based-on&_include:iterate=Appointment:practitioner&_include=Claim:facility&_include=Claim:detail-udi&_include:iterate=Device:location");

            var test = SearchParams.FromUriParamList(uriParams);

            Assert.AreEqual(3,test.Include.Count);
            Assert.AreEqual("Claim:encounter", test.Include[0].Item1);
            Assert.AreEqual(2, test.Include[0].Item2.Count);
            Assert.AreEqual("Encounter:based-on", test.Include[0].Item2[0]);
            Assert.AreEqual("Appointment:practitioner", test.Include[0].Item2[1]);
            Assert.AreEqual("Claim:facility", test.Include[1].Item1);
            Assert.AreEqual("Claim:detail-udi", test.Include[2].Item1);
            Assert.AreEqual(1, test.Include[2].Item2.Count);
            Assert.AreEqual("Device:location", test.Include[2].Item2[0]);
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersChain()
        {
            var uriParams = parseParams("Observation.Subject.name=Teun");

            var test = SearchParams.FromUriParamList(uriParams);

            Assert.AreEqual("Teun", test.ToUriParamList(Model.Version.R4).SingleValue("Observation.Subject.name"));
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
