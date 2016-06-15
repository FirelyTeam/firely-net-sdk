﻿/* 
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
	public class PortableDeepMatchTest
#else
    public class DeepMatchTest
#endif
    {
        [TestMethod]
        public void CheckMatchDeepCopied()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();

            Assert.IsTrue(p2.Matches(p));
            Assert.IsTrue(p.Matches(p2));
        }

        [TestMethod]
        public void CheckComparePrimitiveChanged()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();

            // If you set an element to null in the pattern, it need not be set in the source
            p2.Gender = null;
            Assert.IsFalse(p2.Matches(p));
            Assert.IsTrue(p.Matches(p2));
            
            // If both are null, we're fine
            p.Gender = null;
            Assert.IsTrue(p2.Matches(p));
            Assert.IsTrue(p.Matches(p2));

            p2.Contact[0].Relationship[0].Coding[0].System = "http://nu.nl/different";

            Assert.IsFalse(p2.Matches(p));
            Assert.IsFalse(p.Matches(p2));
        }

        [TestMethod]
        public void CheckCompareListChanged()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();

            var rel = (CodeableConcept)p.Contact[0].Relationship[0].DeepCopy();

            p2.Contact[0].Relationship.Add(rel);

            Assert.IsTrue(p2.Matches(p));
            Assert.IsTrue(p2.Matches(p));
        }

    }
}
