using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class TokenExtensionsTests
    {
        private IEnumerable<(Model.Identifier id, string expected)> GetIdentifierTestData()
        {
            yield return (new Model.Identifier("system", "1"), "system|1");
            yield return (new Model.Identifier("", "1"), "1");
            yield return (new Model.Identifier(null, "1"), "1");
            yield return (new Model.Identifier("system",""), "system|");
            yield return (new Model.Identifier("system", null), "system|");
        }

        private IEnumerable<(Coding coding, string expected)> GetCodingTestData()
        {
            yield return (new Coding("system", "code"), "system|code");
            yield return (new Coding("", "code"), "code");
            yield return (new Coding(null, "1"), "1");
            yield return (new Coding("system", ""), "system|");
            yield return (new Coding("system", null), "system|");
        }

        private IEnumerable<(Model.DSTU2.ContactPoint contactPoint, string expected)> GetDstu2ContactPointTestData()
        {
            yield return (new Model.DSTU2.ContactPoint(Model.DSTU2.ContactPointSystem.Phone, ContactPointUse.Mobile, "06-12345678"), "Mobile|06-12345678");
            yield return (new Model.DSTU2.ContactPoint(null, null, "contact"), "contact");
            yield return (new Model.DSTU2.ContactPoint(null, ContactPointUse.Home, ""), "Home|");
            yield return (new Model.DSTU2.ContactPoint(null, ContactPointUse.Work, null), "Work|");
        }

        private IEnumerable<(Model.STU3.ContactPoint contactPoint, string expected)> GetStu3ContactPointTestData()
        {
            yield return (new Model.STU3.ContactPoint(Model.STU3.ContactPointSystem.Phone, ContactPointUse.Mobile, "06-12345678"), "Mobile|06-12345678");
            yield return (new Model.STU3.ContactPoint(null, null, "contact"), "contact");
            yield return (new Model.STU3.ContactPoint(null, ContactPointUse.Home, ""), "Home|");
            yield return (new Model.STU3.ContactPoint(null, ContactPointUse.Work, null), "Work|");
        }

        private IEnumerable<(Model.R4.ContactPoint contactPoint, string expected)> GetR4ContactPointTestData()
        {
            yield return (new Model.R4.ContactPoint(Model.R4.ContactPointSystem.Phone, ContactPointUse.Mobile, "06-12345678"), "Mobile|06-12345678");
            yield return (new Model.R4.ContactPoint(null, null, "contact"), "contact");
            yield return (new Model.R4.ContactPoint(null, ContactPointUse.Home, ""), "Home|");
            yield return (new Model.R4.ContactPoint(null, ContactPointUse.Work, null), "Work|");
        }

        private IEnumerable<(CodeableConcept codeableConcept, string expected)> GetCodeableConceptTestData()
        {
            yield return (new CodeableConcept("system", "code"), "system|code");
            yield return (new CodeableConcept("", "code"), "code");
            yield return (new CodeableConcept(null, "1"), "1");
            yield return (new CodeableConcept("system", ""), "system|");
            yield return (new CodeableConcept("system", null), "system|");
        }

        [TestMethod]
        public void IdentifierToTokenTest()
        {
            foreach (var item in GetIdentifierTestData())
            {
                Assert.AreEqual(item.expected, item.id.ToToken());
            }
        }

        [TestMethod]
        public void CodingToTokenTest()
        {
            foreach (var item in GetCodingTestData())
            {
                Assert.AreEqual(item.expected, item.coding.ToToken());
            }
        }

        [TestMethod]
        public void ContactPointToTokenTest()
        {
            foreach (var item in GetDstu2ContactPointTestData())
            {
                Assert.AreEqual(item.expected, item.contactPoint.ToToken());
            }
            foreach (var item in GetStu3ContactPointTestData())
            {
                Assert.AreEqual(item.expected, item.contactPoint.ToToken());
            }
            foreach (var item in GetR4ContactPointTestData())
            {
                Assert.AreEqual(item.expected, item.contactPoint.ToToken());
            }
        }

        [TestMethod]
        public void CodeableConceptToTokenTest()
        {
            foreach (var item in GetCodeableConceptTestData())
            {
                Assert.AreEqual(item.expected, item.codeableConcept.ToToken());
            }
        }
    }
}
