/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System.Linq;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Tests
{
    public class JsonAssert
    {
        public static void AreSame(string filename, string expected, string actual, List<string> errors)
        {
            var exp = SerializationUtil.JObjectFromJsonText(expected);
            var act = SerializationUtil.JObjectFromJsonText(actual);

            AreSame(filename, exp, act, errors);
        }

        public static void AreSame(string filename, JToken expected, JToken actual, List<string> errors)
        {
            if (expected.Type != actual.Type)
            {
                errors.Add($"{filename}: Token types are not the same at {actual.Path} (actual: {actual.Type}, expected: {expected.Type})");
                return;
            }

            switch (expected)
            {
                case JValue exV:
                    {
                        JValue acV = (JValue)actual;
                        compareValues(filename, exV.Value, acV.Value, expected.Path, errors);
                        return;
                    }
                case JProperty exP:
                    {
                        JProperty acP = (JProperty)actual;
                        if (exP.Name != acP.Name)
                        {
                            errors.Add($"{filename}: Expected element '{exP.Name}', actual '{acP.Name}' at '{exP.Path}'");
                            return;
                        }
                        AreSame(filename, exP.Value, acP.Value, errors);
                        return;
                    }             
                case JContainer exC:
                    {
                        JContainer acC = (JContainer)actual;
                        areSame(filename, exC, acC, errors);
                        return;
                    }
            }
        }

        private static void areSame(string filename, JContainer expected, JContainer actual, List<string> errors)
        {
            bool isRelevant(JToken t)
            {
                if (t is JProperty p)
                {
                    if (p.Name == "fhir_comments") return false;
                    if(p.Name.StartsWith("_") && p.Value is JObject jo)
                    {
                        if (jo.Count == 1 && jo.ContainsKey("fhir_comments")) return false;
                    }
                    return true;
                }
                else
                    return true;
            }

            var expecteds = expected.Children().Where(c => isRelevant(c));
            var actuals = actual.Children().Where(c => isRelevant(c));

            if(expecteds.First().Type == JTokenType.Property)
            {
                expecteds = expecteds.OrderBy(p => ((JProperty)p).Name);
                actuals = actuals.Cast<JProperty>().OrderBy(p => p.Name);
            }

            var expectedList = expecteds.ToList();
            var actualList = actuals.ToList();

            if (expectedList.Count != actualList.Count)
            {
                errors.Add($"{filename}: Number of elements are not the same in container {expected.Path ?? actual.Path}");
                return;
            }

            for (int elemNr = 0; elemNr < expectedList.Count(); elemNr++)
            {
                var ex = expectedList[elemNr];
                var ac = actualList[elemNr];

                AreSame(filename, ex, ac, errors);
            }
        }

        public static void compareValues(string filename, object exp, object act, string path, List<string> errors)
        {
            if (exp == null && act == null) return;
            else if (exp != null && act != null)
            {
                if(exp.GetType() != act.GetType())
                {
                    errors.Add($"{filename}: The types of the values are not the same at '{path}'");
                    return;
                }

                object expected = exp;
                object actual = act;

                if (exp is string expS)
                {
                    if (expS.TrimStart().StartsWith("<div"))
                    {
                        // Don't check the narrative, namespaces are not correctly generated in DSTU2
                        return;
                    }

                        var actS = (string)act;
                    // Hack for timestamps, binaries and narrative html
                    if (expS.EndsWith("+00:00")) expS = expS.Replace("+00:00", "Z");
                    if (actS.EndsWith("+00:00")) actS = actS.Replace("+00:00", "Z");
                    if (expS.Contains(".000+")) expS = expS.Replace(".000+", "+");
                    if (actS.Contains(".000+")) actS = actS.Replace(".000+", "+");
                    if (expS.Contains(".000Z")) expS = expS.Replace(".000Z", "Z");
                    if (actS.Contains(".000Z")) actS = actS.Replace(".000Z", "Z");
                    actS = actS.Replace(" ", "");
                    actS = actS.Replace("\n", "");
                    actS = actS.Replace("\r", "");
                    expS = expS.Replace(" ", "");
                    expS = expS.Replace("\n", "");
                    expS = expS.Replace("\r", "");

                    expected = expS.Trim();
                    actual = actS.Trim();
                }

                if (!Object.Equals(expected,actual))
                {
                    errors.Add($"{filename}: Values are not equal at '{path}', expected '{expected}', actual '{actual}'");
                    return;
                }
            }
            else
            {
                errors.Add($"{filename}: One of the values (but not both) are null at '{path}'");
                return;
            }
        }
    }
}
