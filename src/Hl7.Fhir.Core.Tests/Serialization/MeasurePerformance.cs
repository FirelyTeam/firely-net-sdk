/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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
#if PORTABLE45
	public class PortableMeasurePerformance
#else
	public class MeasurePerformance
#endif
    {
        [TestMethod]
        public void RunPerfTest()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");
            string json = File.ReadAllText(@"TestData\TestPatient.json");
           
            //string xml = Hl7.Fhir.Core.Tests.Properties.TestResources.TestPatientXml;
            //string json = Hl7.Fhir.Core.Tests.Properties.TestResources.TestPatientJson;

 //            var once = FhirParser.ParseResourceFromJson(json);
            var once = FhirParser.ParseResourceFromXml(xml);

            Stopwatch x = new Stopwatch();

            x.Start();

            for (int i = 0; i < 1000; i++)
            {
              //  var result = FhirParser.ParseResourceFromJson(json);
                var result = FhirParser.ParseResourceFromXml(xml);
            }
            x.Stop();

            Debug.WriteLine(x.ElapsedMilliseconds);
        }
    }
}
