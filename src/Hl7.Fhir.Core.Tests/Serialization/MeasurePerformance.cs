/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
	public class MeasurePerformance
    {
        [TestMethod]
        public void RunPerfTest()
        {
            string xml = TestDataHelper.ReadTestData("TestPatient.xml");
            string json = TestDataHelper.ReadTestData(@"TestPatient.json");

            var once = new FhirXmlParser().Parse<Resource>(xml);

            Stopwatch x = new Stopwatch();

            x.Start();

            for (int i = 0; i < 1000; i++)
            {
                var result = new FhirXmlParser().Parse<Resource>(xml);
            }
            x.Stop();

            Debug.WriteLine(x.ElapsedMilliseconds);
        }
    }
}
