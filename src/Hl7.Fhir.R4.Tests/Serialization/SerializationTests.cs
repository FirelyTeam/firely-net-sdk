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

namespace Hl7.Fhir.Tests.Serialization
{
    public partial class SerializationTests
    {
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