/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FhirPath;
using Hl7.Fhir.FhirPath.InstanceTree;
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
        [TestMethod, TestCategory("FhirPath")]
        public void ConstructTestPatient()
        {
            var tpXml = File.ReadAllText("TestData\\TestPatient.xml");
            var tree = TreeConstructor.FromXml(tpXml);

            Console.WriteLine(LinkedTreeTest.RenderTree(tree));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void CheckTypeDetermination()
        {
            Assert.IsInstanceOfType(new UntypedValue("1").Value, typeof(Int64));
            Assert.IsInstanceOfType(new UntypedValue("true").Value, typeof(Boolean));
            Assert.IsInstanceOfType(new UntypedValue("hi").Value, typeof(String));
            Assert.IsInstanceOfType(new UntypedValue("4.0").Value, typeof(Decimal));
            Assert.IsInstanceOfType(new UntypedValue(PartialDateTime.Now().ToString()).Value, typeof(PartialDateTime));
        }

    }
}
