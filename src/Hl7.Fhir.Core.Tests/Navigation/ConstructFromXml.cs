using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hl7.Fhir.Navigation
{
    [TestClass]
    public class ConstructFromXml
    {
        [TestMethod]
        public void ConstructTestPatient()
        {
            var tpXml = File.ReadAllText("TestData\\TestPatient.xml");
            var tree = TreeConstructor.FromXml(tpXml);

            Console.WriteLine(LinkedTreeTest.RenderTree(tree));
        }

    }
}
