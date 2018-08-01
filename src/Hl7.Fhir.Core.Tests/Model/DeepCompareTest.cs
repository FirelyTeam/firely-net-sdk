/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using static Hl7.Fhir.Tests.TestDataHelper;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class DeepCompareTest
    {
        [TestMethod]
        public void CheckCompareDeepCopied()
        {
            string xml = ReadTestData("TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();

            Assert.IsTrue(p2.IsExactly(p));
        }

        [TestMethod]
        public void CheckComparePrimitiveChanged()
        {
            string xml = ReadTestData("TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
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
            string xml = ReadTestData("TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();

            var rel = (CodeableConcept)p.Contact[0].Relationship[0].DeepCopy();

            p2.Contact[0].Relationship.Add(rel);

            Assert.IsFalse(p2.IsExactly(p));
        }

        [TestMethod]
        public void CheckCompareCodeOfT()
        {
            var a = new Code<AdministrativeGender>(AdministrativeGender.Male);
            var b = new Code<AdministrativeGender>(AdministrativeGender.Female);
            var c = (Code<AdministrativeGender>)a.DeepCopy();

            Assert.IsFalse(a.IsExactly(b));
            Assert.IsFalse(a.Matches(b));
            Assert.IsTrue(a.IsExactly(c));
            Assert.IsTrue(a.Matches(c));
        }
        
    }
}
