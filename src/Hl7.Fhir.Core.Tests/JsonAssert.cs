/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;

namespace Hl7.Fhir.Tests
{
    public class JsonAssert
    {
        public static void AreSame(JObject expected, JObject actual)
        {
            areSame(expected.Root, actual.Root);
        }

        public static void AreSame(string expected, string actual)
        {
            JObject exp = FhirParser.JObjectFromJson(expected);
            JObject act = FhirParser.JObjectFromJson(actual);

            AreSame(exp, act);
        }

        private static void areSame(JToken left, JToken right)
        {
            if ((left.Type == JTokenType.Integer && right.Type == JTokenType.Float) ||
                (left.Type == JTokenType.Float && right.Type == JTokenType.Integer))
            {
                JValue leftVal = (JValue)left;
                JValue rightVal = (JValue)right;

                Assert.AreEqual(leftVal.ToString(), rightVal.ToString());
                // Bug in json.net, will sometimes convert to integer instead of float
                return;
            }

            if (left.Type != right.Type)
                throw new AssertFailedException("Token type is not the same at " + right.Path);

            if (left.Type == JTokenType.Array)
            {
                var la = (JArray)left;
                var ra = (JArray)right;

                if(la.Count != ra.Count)
                    throw new AssertFailedException("Array size is not the same at " + right.Path);

                for(var i=0; i<la.Count; i++)
                    areSame(la[i],ra[i]);
            }

            else if (left.Type == JTokenType.Object)
            {
                var lo = (JObject)left;
                var ro = (JObject)right;

                foreach (var lMember in lo)
                {
                    JToken rMember;

                    // Hack, some examples have empty arrays or objects, these are illegal and will result in missing members after round-tripiing
                    if (lMember.Value is JArray && ((JArray)lMember.Value).Count == 0) continue;
                    if (lMember.Value is JObject && ((JObject)lMember.Value).Count == 0) continue;

                    if (!ro.TryGetValue(lMember.Key, out rMember) || rMember == null)
                        throw new AssertFailedException(String.Format("Expected member {0} not found in actual at " + left.Path, lMember.Key));

                    areSame(lMember.Value, rMember);
                }

                foreach (var rMember in ro)
                {
                    JToken lMember;

                    if (!lo.TryGetValue(rMember.Key, out lMember))
                        throw new AssertFailedException(String.Format("Actual has unexpected extra member {0} at " + right.Path, rMember.Key));
                }

            }

            else if (left.Type == JTokenType.String)
            {
                var lValue = left.ToString();
                var rValue = right.ToString();

                if (lValue.TrimStart().StartsWith("<div"))
                {
                    // Correct incorrect examples
                    lValue.Trim();

                    if (lValue.StartsWith("<div>"))
                        lValue = "<div xmlns='http://www.w3.org/1999/xhtml'>" + lValue.Substring(5);

                    var leftDoc = FhirParser.XDocumentFromXml(lValue);
                    var rightDoc = FhirParser.XDocumentFromXml(rValue);

                    XmlAssert.AreSame(leftDoc, rightDoc);
                }
                else
                {
                    // Hack for timestamps
                    if (lValue.EndsWith("+00:00")) lValue = lValue.Replace("+00:00", "Z");
                    if (rValue.EndsWith("+00:00")) rValue = rValue.Replace("+00:00", "Z");
                    if (lValue.Contains(".000+")) lValue = lValue.Replace(".000+", "+");
                    if (rValue.Contains(".000+")) rValue = rValue.Replace(".000+", "+");

                    Assert.AreEqual(lValue, rValue);
                }
            }

            else
            {
                if (!JToken.DeepEquals(left, right))
                    throw new AssertFailedException(String.Format("Values not the same at " + left.Path));
            }
        }

      
    }
}
