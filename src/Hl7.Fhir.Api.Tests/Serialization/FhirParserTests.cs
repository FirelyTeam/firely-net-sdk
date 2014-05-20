/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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
using Hl7.Fhir.Search;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class FhirParserTests
    {
        [TestMethod]
        public void TestParseQueryFromUriParametersSimple()
        {
            string resource = "Patient";
            var uriParams = parseParams("_id=1");

            Query test = FhirParser.ParseQueryFromUriParameters(resource, uriParams);

            Assert.AreEqual("Patient", test.ResourceType);
            Assert.AreEqual("1", test.GetSingleValue("_id"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersInclude()
        {
            string resource = "Observation";
            var uriParams = parseParams("_include=Subject");

            Query test = FhirParser.ParseQueryFromUriParameters(resource, uriParams);

            Assert.AreEqual("Observation", test.ResourceType);
            Assert.IsTrue(test.Includes.Contains("Subject"));
        }

        [TestMethod]
        public void TestParseQueryFromUriParametersChain()
        {
            string resource = "Observation";
            var uriParams = parseParams("Observation.Subject.name=Teun");

            Query test = FhirParser.ParseQueryFromUriParameters(resource, uriParams);

            Assert.AreEqual("Observation", test.ResourceType);
            Extension paramTeun = test.Parameter.SingleWithName("Observation.Subject.name");
            Assert.AreEqual("Teun", Query.ExtractParamValue(paramTeun));
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
