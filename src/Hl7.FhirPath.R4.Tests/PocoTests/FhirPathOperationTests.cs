using Hl7.Fhir.Model;
using Hl7.Fhir.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Core.Tests.Model
{
    [TestClass]
    public class FhirPathOperationTests
    {
        [TestMethod]
        public void AsOperationCanCastToUri()
        {
            var conceptMap = new ConceptMap
            {
                Status = PublicationStatus.Draft,
                Source = new FhirUri("http://ValueSet.fhir.org/test"),
            };
            var values = conceptMap.Select("ConceptMap.source.as(uri)");
            var value = values.FirstOrDefault();
            Assert.AreEqual("uri", value.TypeName);

            var values2 = conceptMap.Select("(ConceptMap.source as uri)");
            var value2 = values2.FirstOrDefault();
            Assert.AreEqual("uri", value2.TypeName);
        }

        [TestMethod]
        public void Issue_1643()
        {
            var poco = new ConceptMap
            {
                Identifier = new Identifier("system", "value"),
                Source = new FhirUri("http://example.com"),
                DateElement = FhirDateTime.Now()
            };

            Assert.AreEqual("Identifier", poco.Select("ConceptMap.identifier").First().TypeName);
            Assert.AreEqual("Identifier", poco.Select("ConceptMap.identifier as Identifier").First().TypeName);

            Assert.AreEqual("dateTime", poco.Select("ConceptMap.date").First().TypeName);
            Assert.AreEqual("dateTime", poco.Select("ConceptMap.date as dateTime").First().TypeName);

            Assert.AreEqual("uri", poco.Select("ConceptMap.source").First().TypeName);
            Assert.AreEqual("uri", poco.Select("ConceptMap.source as uri").First().TypeName);
        }
    }
}
