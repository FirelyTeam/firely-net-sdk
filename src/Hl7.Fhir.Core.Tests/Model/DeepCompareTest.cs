/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hl7.Fhir.Serialization;
using System.Xml;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
#if PORTABLE45
	public class PortableDeepCompareTest
#else
    public class DeepCompareTest
#endif
    {
        [TestMethod]
        public void CheckCompareDeepCopied()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = (Patient)FhirParser.ParseResourceFromXml(xml);
            var p2 = (Patient)p.DeepCopy();

            Assert.IsTrue(p2.IsExactly(p));
        }

        [TestMethod]
        public void CheckComparePrimitiveChanged()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = (Patient)FhirParser.ParseResourceFromXml(xml);
            var p2 = (Patient)p.DeepCopy();

            p2.ActiveElement.Value = !p2.ActiveElement.Value;
            Assert.IsFalse(p2.IsExactly(p));
            p2.ActiveElement.Value = null;
            Assert.IsFalse(p2.IsExactly(p));

            p2.Contact[0].Relationship[0].Coding[0].System = "http://nu.nl/different";

            Assert.IsFalse(p2.IsExactly(p));
        }

        [TestMethod]
        public void CheckCompareListChanged()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = (Patient)FhirParser.ParseResourceFromXml(xml);
            var p2 = (Patient)p.DeepCopy();

            var rel = (CodeableConcept)p.Contact[0].Relationship[0].DeepCopy();

            p2.Contact[0].Relationship.Add(rel);

            Assert.IsFalse(p2.IsExactly(p));
        }

    }
}
