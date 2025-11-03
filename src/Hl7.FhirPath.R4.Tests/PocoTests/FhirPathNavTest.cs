/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Tests;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Xunit;

namespace Hl7.FhirPath.R4.Tests
{
    public class FhirPathNavTest
    {
        public PocoNode getTestData()
        {
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");
            var engine = FhirSerializationEngineFactory.Ostrich(ModelInfo.ModelInspector);
            return engine.DeserializeFromXml(tpXml).ToPocoNode();
        }

        [Fact]
        public void TestNavigation()
        {
            var values = getTestData();

            var r = values.Navigate("Patient");

            var result = values.Navigate("Patient").Navigate("identifier").Navigate("use");
            Assert.Equal(3, result.Count());
            Assert.Equal("usual", result.First().GetValue());
        }

        [Fact]
        public void TestNavigationALTERNATIVE()
        {
            var values = getTestData();

            var result = values.Navigate("Patient").Navigate("identifier").Navigate("use");
            Assert.Equal(3, result.Count());
            Assert.Equal("usual", (string)result.First().GetValue());
        }

    }
}