/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class ConceptTests
    {
        [TestMethod]
        public void ConceptConstructor()
        {
            var someCodings = new[] { new P.Code("http://system1", "codeA"), new P.Code("http://system2", "codeB") };
            var sameCodings = new[] { new P.Code("http://system1", "codeA"), new P.Code("http://system2", "codeB") };
            var someOtherCodings = new[] { new P.Code("http://system1", "codeB"), new P.Code("http://system2", "codeC") };

            var newCds = new P.Concept(someCodings);

            Assert.AreEqual(newCds, new P.Concept(someCodings));
            Assert.AreEqual(newCds, new P.Concept(sameCodings));
            Assert.AreNotEqual(newCds, new P.Concept(someOtherCodings));
            Assert.AreNotEqual(newCds, new P.Concept(someCodings, "bla"));
        }

        [TestMethod]
        public void CanConvertToSystemCoding()
        {
            var fhirCode = new Code("atem");
            var systemCode = fhirCode.ToSystemCode();
            Assert.AreEqual("atem", systemCode.Value);
            Assert.IsNull(systemCode.System);

            var fhirCodeOfT = new Code<EnumMappingTest.TestAdministrativeGender>(EnumMappingTest.TestAdministrativeGender.Male);
            systemCode = fhirCodeOfT.ToSystemCode();
            Assert.AreEqual("male", systemCode.Value);
            Assert.AreEqual("http://hl7.org/fhir/administrative-gender", systemCode.System);

            var fhirCoding = new Coding("http://nu.nl", "yes", "positive") { Version = "1.0.0" };
            systemCode = fhirCoding.ToSystemCode();
            verifyCoding(systemCode);

            var fhirCodeableConcept = new CodeableConcept("http://nu.nl", "yes", "positive", "Original text");
            fhirCodeableConcept.Coding[0].Version = "1.0.0";
            var systemConcept = fhirCodeableConcept.ToSystemConcept();
            Assert.AreEqual("Original text", systemConcept.Display);
            Assert.AreEqual(1, systemConcept.Codes.Count);
            verifyCoding(systemConcept.Codes.First());

            static void verifyCoding(P.Code systemCode)
            {
                Assert.AreEqual("yes", systemCode.Value);
                Assert.AreEqual("http://nu.nl", systemCode.System);
                Assert.AreEqual("positive", systemCode.Display);
                Assert.AreEqual("1.0.0", systemCode.Version);
            }
        }

    }
}