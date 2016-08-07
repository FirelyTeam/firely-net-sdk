/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.IO.Compression;
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableValidateAllExamplesTest
#else
    public class ValidateAllExamplesTest
#endif
    {
        [TestMethod]
        [TestCategory("LongRunner")]
        public void ValidateInvariantAllExamples()
        {
            string examplesZip = @"TestData\examples.zip";

            // Create an empty temporary directory for us to dump the intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRValidateAllTestXml");
            if (Directory.Exists(baseTestPath))
                Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);

            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, baseTestPath);
            ZipFile.ExtractToDirectory(examplesZip, baseTestPath);

            Debug.WriteLine(String.Format("Validating files in {0}", baseTestPath));
            var files = Directory.EnumerateFiles(baseTestPath);
            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();
            foreach (string file in files)
            {
                // Verified examples that fail validations
                // dom-3
                if (file.EndsWith("list-example-familyhistory-genetics-profile-annie(prognosis).xml"))
                    continue;
                if (file.EndsWith("questionnaire-sdc-profile-example-loinc(questionnaire-sdc-profile-example-loinc).xml"))
                    continue;
                if (file.EndsWith("questionnaireresponse-example(3141).xml"))
                    continue;

                // vsd-3, vsd-8
                if (file.EndsWith("valueset-ucum-common(ucum-common).xml"))
                    continue;

                testFileCount++;
                // Debug.WriteLine(String.Format("Validating {0}", file));
                var resource = parser.Parse<Resource>(System.IO.File.ReadAllText(file));
                resource.InvariantConstraints = new List<ElementDefinition.ConstraintComponent>();
                resource.AddDefaultConstraints();
                var outcome = new OperationOutcome();
                resource.ValidateInvariants(outcome);
                if (outcome.Issue.Count > 0)
                {
                    Debug.WriteLine(String.Format("Validating {0} failed:", file));
                    foreach (var item in outcome.Issue)
                    {
                        if (!failedInvariantCodes.ContainsKey(item.Details.Coding[0].Code))
                            failedInvariantCodes.Add(item.Details.Coding[0].Code, 1);
                        else
                            failedInvariantCodes[item.Details.Coding[0].Code]++;
                        Trace.WriteLine("\t"+ item.Details.Coding[0].Code + ": " + item.Details.Text);
                    }
                    // Trace.WriteLine(outcome.ToString());
                }
                if (outcome.Issue.Count != 0)
                {
                    errorCount++;
                }
            }
            Debug.WriteLine(String.Format("\r\n------------------\r\nValidation failed in {0} of {1} examples", errorCount, testFileCount));
            if (failedInvariantCodes.Count > 0)
            {
                Debug.Write("Issues with Invariant: ");
                bool b = false;
                foreach (var item in failedInvariantCodes)
                {
                    if (b)
                        Debug.Write(", ");
                    Debug.Write(String.Format("{0} ({1})", item.Key, item.Value));
                    b = true;
                }
            }
            Assert.AreEqual(0, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));
        }

    }
}
