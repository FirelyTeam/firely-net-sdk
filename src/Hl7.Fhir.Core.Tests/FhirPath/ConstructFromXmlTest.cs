/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Test.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Test.FhirPath
{
    [TestClass]
    public class ConstructFromXmlTest
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
