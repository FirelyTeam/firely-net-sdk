/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Tests
{
    public class JsonAssert
    {
        public static void AreSame(string filename, JObject expected, JObject actual, List<string> errors)
        {
            areSame(filename, expected.Root, actual.Root, errors);
        }

        public static void AreSame(string filename, string expected, string actual, List<string> errors)
        {
            JObject exp = SerializationUtil.JObjectFromReader(SerializationUtil.JsonReaderFromJsonText(expected));
            JObject act = SerializationUtil.JObjectFromReader(SerializationUtil.JsonReaderFromJsonText(actual));

            AreSame(filename, exp, act, errors);
        }

        private static void areSame(string filename, JToken expected, JToken actual, List<string> errors)
        {
            if ((expected.Type == JTokenType.Integer && actual.Type == JTokenType.Float) ||
                (expected.Type == JTokenType.Float && actual.Type == JTokenType.Integer))
            {
                JValue leftVal = (JValue)expected;
                JValue rightVal = (JValue)actual;

                if (leftVal.ToString() != rightVal.ToString())
                {
                    errors.Add(String.Format("Error comparing values in: {0}:{1}, {2} - {3}",
                                filename, expected.Path, leftVal.ToString(), rightVal.ToString()));
                }
                // Assert.AreEqual(leftVal.ToString(), rightVal.ToString());
                // Bug in json.net, will sometimes convert to integer instead of float
                return;
            }

            if (expected.Type != actual.Type)
                throw new AssertFailedException("Token type is not the same at " + actual.Path);

            if (expected.Type == JTokenType.Array)
            {
                var la = (JArray)expected;
                var ra = (JArray)actual;

                if(la.Count != ra.Count)
                    throw new AssertFailedException("Array size is not the same at " + actual.Path);

                for (var i = 0; i < la.Count; i++)
                    areSame(filename, la[i], ra[i], errors);
            }

            else if (expected.Type == JTokenType.Object)
            {
                var lo = (JObject)expected;
                var ro = (JObject)actual;

                foreach (var lMember in lo)
                {
                    JToken rMember;

                    // Hack, some examples have empty arrays or objects, these are illegal and will result in missing members after round-tripiing
                    if (lMember.Value is JArray && ((JArray)lMember.Value).Count == 0) continue;
                    if (lMember.Value is JObject && ((JObject)lMember.Value).Count == 0) continue;

                    if (!ro.TryGetValue(lMember.Key, out rMember) || rMember == null)
                        throw new AssertFailedException(String.Format("Expected member '{0}' not found in actual at " + expected.Path, lMember.Key));

                    areSame(filename, lMember.Value, rMember, errors);
                }

                foreach (var rMember in ro)
                {
                    JToken lMember;

                    if (!lo.TryGetValue(rMember.Key, out lMember))
                        throw new AssertFailedException(String.Format("Actual has unexpected extra member {0} at " + actual.Path, rMember.Key));
                }

            }

            else if (expected.Type == JTokenType.String)
            {
                var lValue = expected.ToString();
                var rValue = actual.ToString();

                if (lValue.TrimStart().StartsWith("<div"))
                {
                    // Don't check the narrative, namespaces are not correctly generated in DSTU2
                    //var leftDoc = SerializationUtil.XDocumentFromXmlText(lValue);
                    //var rightDoc = SerializationUtil.XDocumentFromXmlText(rValue);

                    //XmlAssert.AreSame(filename, leftDoc, rightDoc);
                }
                else
                {
                    XmlAssert.AssertAreTheSame(expected.Path, lValue, rValue);
                }
            }

            else
            {
                if (!JToken.DeepEquals(expected, actual))
                    throw new AssertFailedException(String.Format("Values not the same at " + expected.Path));
            }
        }


    }
}
