/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests.Serialization
{
    public partial class SerializationTests
    {
        [TestMethod]
        public void TestReplacements()
        {
            var expectedJson = """{"resourceType":"Parameters","parameter":[{"name":"Measurement Period","valuePeriod":{"start":"2026-01-01T00:00:00+00:00","end":"2026-12-31T23:59:59.999+00:00"}}]}""";

            var end = new DateTimeOffset(2027, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var p = new Parameters();
            p.Add("Measurement Period", new Period(new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), new(end.AddMilliseconds(-1))));
            var json = p.ToJson();

            var serializer = FhirJsonSerializer.SerializeToString(p);
            Assert.AreEqual(json, serializer);
            Assert.AreEqual(expectedJson, serializer);
        }
        
        /// <summary>
        /// This test verifies that the parser can handle a backbone element that has a property of resourceType
        /// (only found in the ExampleScenario resource in R4 and R4B - used to be in Claim)
        /// </summary>
        [TestMethod]
        public void TestExampleScenarioJsonSerialization()
        {
            var es = new ExampleScenario()
            {
                Name = "test",
                Status = PublicationStatus.Active
            };
            es.Instance.Add(new ExampleScenario.InstanceComponent()
            {
                ResourceId = "brian",
                ResourceType = ResourceType.ExampleScenario,
                Name = "brian"
            });

            string json = FhirJsonSerializer.SerializeToString(es);
            var c2 = new FhirJsonDeserializer().Deserialize<ExampleScenario>(json);
            Assert.AreEqual("brian", c2.Instance[0].Name);
            Assert.AreEqual("ExampleScenario", c2.Instance[0].ResourceTypeElement.JsonValue as string);
        }
    }
}