using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class IssueXMLParserTests
    {
        [TestMethod]
        public void TestXMLParseringWithUpperCase()
        {
            var testInputUpper = File.ReadAllText(Path.Combine("TestData", "TestInput_Uppercase.xml"));

            var result = FhirXmlNode.Parse(testInputUpper);

            Assert.IsNotNull(result);

            //xml with only uppercase elements have no childs after parsing
            Assert.IsFalse(result.Children().Any());
        }

        [TestMethod]
        public void TestXMLParseringWithLowerCase()
        {
            var testInputUpper = File.ReadAllText(Path.Combine("TestData", "TestInput_Lowercase.xml"));

            var result = FhirXmlNode.Parse(testInputUpper);

            Assert.IsNotNull(result);

            //same xml but with lowercase elements. Now the sourcenode is filled with the correct children
            Assert.IsTrue(result.Children().Any());

            //the test_id property is parsed correctly
            Assert.AreEqual("123456", result.Children().Where(n => n.Name == "test_ID").First().Text);

            //also the case is parsed correctly 
            Assert.AreEqual("PAT1234", result.Children().Where(n => n.Name == "case").First().Children().Where(n => n.Name == "patient_ID").First().Text);

        }
    }


}
