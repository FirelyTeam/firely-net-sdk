using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Model;
using Hl7.FluentPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    [TestClass]
    public class EnementNodeTests
    {

        [TestMethod]
        public void TestCreateFromNavigator()
        {
            var parser = new FhirJsonParser();
            ElementNode node;
            string json = File.ReadAllText(@"TestData\TestPatient.json");
            var patient = parser.Parse<Patient>(json);

            var nav = new PocoNavigator(patient);
            var root = ElementNodeWriter.CreateElementModel(nav);

            // Root
            Assert.AreEqual("Patient", root.Name);

            // Patient.text.Status
            node = root.Children[1].Children[0];
            Assert.AreEqual("status", node.Name);
            Assert.AreEqual("generated", node.Value);
            Assert.AreEqual("code", node.TypeName);

            // Patient.address.period.start -- mind the arrays when counting!
            node = root.Children[11].Children[7].Children[0];
            Assert.AreEqual("start", node.Name);
            Assert.AreEqual("dateTime", node.TypeName);
            Assert.IsInstanceOfType(node.Value, typeof(PartialDateTime));
            Assert.AreEqual(PartialDateTime.Parse("1974-12-25"), node.Value);

        }
    }
}
