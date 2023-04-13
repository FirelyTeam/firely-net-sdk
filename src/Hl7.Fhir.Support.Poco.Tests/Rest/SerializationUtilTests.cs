/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Utility;
using FluentAssertions;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class SerializationUtilTests
    {
        [TestMethod]
        [DynamicData(nameof(GetXmlData), DynamicDataSourceType.Method)]
        public void IsXml(string xml, bool isXml, bool isFhirXml)
        {
            SerializationUtil.ProbeIsXml(xml).Should().Be(isXml);
            SerializationUtil.ProbeIsFhirXml(xml).Should().Be(isFhirXml);
        }

        public static IEnumerable<object[]> GetXmlData()
        {
            yield return new object[] { "<start><field value='3' /></start>", true, false };
            yield return new object[] { "  <start>  <field value='3' />  </start>  ", true, false };
            yield return new object[] { "<Patient><field value='3' /></Patient>", true, false };
            yield return new object[] { """
                        <Patient>
                            <field value='3' />
                        </Patient>
                        """, true, false };
            yield return new object[] { """<Unknown><active value="true" /></Unknown>""", true, false };            
            yield return new object[] { "<!DOCTYPE html><html lang=en><head /></html>", true, false };
            yield return new object[] { "crap", false, false };
            yield return new object[] { "crap <hi />", false, false };
            yield return new object[] { "<Something xmlns='http://hl7.org/fhir'><element value='3' /></Something>", true, true };
            yield return new object[] { "<f:Something xmlns:f='http://hl7.org/fhir'><f:element value='3' /></f:Something>", true, true };
            yield return new object[] { "<f:Something xmlns:f=\"http://hl7.org/fhir\"><f:element value='3' /></f:Something>", true, true };
            yield return new object[] { """{ "element": "value" }""", false, false };
        }

        [TestMethod]
        [DynamicData(nameof(GetJsonData), DynamicDataSourceType.Method)]
        public void IsJson(string json, bool isJson, bool isFhirJson)
        {
            SerializationUtil.ProbeIsJson(json).Should().Be(isJson);
            SerializationUtil.ProbeIsFhirJson(json).Should().Be(isFhirJson);
        }

        public static IEnumerable<object[]> GetJsonData()
        {
            yield return new object[] { """{ "element": "value" }""", true, false };
            yield return new object[] { """  { "element": "value"}  """, true, false };
            yield return new object[] { """{} """, true, false };
            yield return new object[] { """{ }""", true, false };
            yield return new object[] { """{ crap }""", false, false };
            yield return new object[] { "crap", false, false };
            yield return new object[] { """crap { "element": true }""", false, false };
            yield return new object[] { """{ "element": "value", "resourceType": "Patient" }""", true, true };
            yield return new object[] { """{"resourceType": "Parameters",  "parameter": [ { "name": "result", "valueString": "connected"}]  }""", true, true };
            yield return new object[] { """
                {
                    "resourceType": "Parameters",  
                    "parameter": [ { "name": "result", "valueString": "connected"}]  
                }
                """, true, true };
            yield return new object[] { "<start><field value='3' /></start>", false, false };
        }
    }
}
