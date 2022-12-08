using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class DeepMatchTest
    {
        [TestMethod]
        public void MatchingEmptyRepeatingPattern()
        {
            var concept = new CodeableConcept
            {
                Coding = new List<Coding>
                {
                    new Coding
                    {
                        Code = "foobar"
                    }
                }
            };

            var pattern = new CodeableConcept();

            Assert.IsTrue(concept.Matches(pattern));
        }
    }

}
