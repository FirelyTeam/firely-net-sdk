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

namespace Hl7.Fhir.Test.Serialization
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
            Stream xmlExample = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Test.TestPatient.xml");
            string xml = new StreamReader(xmlExample).ReadToEnd();
            Stream jsonExample = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Test.TestPatient.json");
            string json = new StreamReader(jsonExample).ReadToEnd();

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
