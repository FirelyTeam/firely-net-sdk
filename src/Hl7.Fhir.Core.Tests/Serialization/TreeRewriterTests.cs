/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableTreeRewriterTests
#else
	public class TreeRewriterTests
#endif
    {
        [TestMethod]
        public void ExpandPrimitive()
        {
            var json = "{ \"primi\" : 4 }";
            var result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual("{\"primi\":{\"value\":4}}",result.ToString(Formatting.None));

            json = @"{ ""primi"" : 4, ""_primi"" : { ""id"" : ""a1"" }}";
            result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual(@"{""primi"":{""value"":4,""id"":""a1""}}", result.ToString(Formatting.None));

            json = @"{ ""_primi"" : { ""id"" : ""a1"" }}";
            result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual(@"{""primi"":{""id"":""a1""}}", result.ToString(Formatting.None));
        }

        [TestMethod]
        public void LeaveComplexUnchanged()
        {
            var json = @"{ ""comp"" : { ""someprop"": 4, ""anotherprop"":4 } }";
            var result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual(@"{""comp"":{""someprop"":4,""anotherprop"":4}}", result.ToString(Formatting.None));
        }

        [TestMethod]
        public void ExpandArrays()
        {
            var json = @"{ ""elem"" : [4, { ""someprop"": 5 }, 6, null], ""noarray"" : 4  }";
            var result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual(@"{""elem"":{""value"":4},""elem[1]"":{""someprop"":5},""elem[2]"":{""value"":6},""noarray"":{""value"":4}}", result.ToString(Formatting.None));

            json = @"{ ""elem"" : [4, { ""someprop"": 5 }, 6, null], ""noarray"" : 4, ""_elem"" : [ { ""id"" :8}, null, null, { ""ext"" : 9 }] }";
            result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            Assert.AreEqual(@"{""elem"":{""value"":4,""id"":8},""elem[1]"":{""someprop"":5},""elem[2]"":{""value"":6},""noarray"":{""value"":4},""elem[3]"":{""ext"":9}}", result.ToString(Formatting.None));

            // Try an unbalanced array
            //json = @"{ ""elem"" : [4, { ""someprop"": 5 }, 6, null], ""noarray"" : 4, ""_elem"" : [ { ""id"" :8}, null, { ""ext"" : 9 }] }";
            //try
            //{
            //    result = JsonTreeRewriter.ExpandComplexObject(JObject.Parse(json));
            //    Assert.Fail();
            //}
            //catch
            //{
            //}
        }
    }
}
