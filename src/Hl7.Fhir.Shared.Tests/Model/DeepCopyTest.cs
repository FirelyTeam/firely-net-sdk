﻿/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Hl7.Fhir.Tests.TestDataHelper;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class DeepCopyTest
    {
        [TestMethod]
        public async Task CheckCopyAllFields()
        {
            string xml = ReadTestData("TestPatient.xml");

            var p = await new FhirXmlParser().ParseAsync<Patient>(xml);
            var p2 = (Patient)p.DeepCopy();
            var xml2 = await new FhirXmlSerializer().SerializeToStringAsync(p2);
            XmlAssert.AreSame("TestPatient.xml", xml, xml2);
        }

        [TestMethod]
        public void CollectionDeepCopySemantics()
        {
            var p1 = new Patient();
            var p2 = new Patient();

            var patients = new List<Patient> { p1, p2 };
            var patientsCopy = patients.DeepCopy();

            patients.Remove(p1);
            Assert.AreEqual(2, patientsCopy.Count());
        }

        [TestMethod]
        public void CheckCopyPerformance()
        {
            string xml = ReadTestData("TestPatient.xml");

            var p = new FhirXmlParser().Parse<Patient>(xml);
            var sw = new Stopwatch();

            sw.Start();
            for (var loop = 0; loop < 1000; loop++)
            {
                p.DeepCopy();
            }
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }
}
