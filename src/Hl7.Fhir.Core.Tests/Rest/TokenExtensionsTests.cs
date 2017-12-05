using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class TokenExtensionsTests
    {
        private IEnumerable<(Identifier id, string expected)> GetIdentifierTestData()
        {
            yield return (new Identifier("system", "1"), "system|1");
            yield return (new Identifier("", "1"), "1");
            yield return (new Identifier(null, "1"), "1");
            yield return (new Identifier("system",""), "system|");
            yield return (new Identifier("system", null), "system|");
        }

        private IEnumerable<(Coding coding, string expected)> GetCodingTestData()
        {
            yield return (new Coding("system", "code"), "system|code");
            yield return (new Coding("", "code"), "code");
            yield return (new Coding(null, "1"), "1");
            yield return (new Coding("system", ""), "system|");
            yield return (new Coding("system", null), "system|");
        }

        private IEnumerable<(ContactPoint contactPoint, string expected)> GetContactPointTestData()
        {
            yield return (new ContactPoint(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Mobile, "06-12345678"), "Mobile|06-12345678");
            yield return (new ContactPoint(null, null, "contact"), "contact");
            yield return (new ContactPoint(null, ContactPoint.ContactPointUse.Home, ""), "Home|");
            yield return (new ContactPoint(null, ContactPoint.ContactPointUse.Work, null), "Work|");
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
            foreach (var item in GetContactPointTestData())
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
