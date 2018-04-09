/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class ValidateAllExamplesTest
    {
        [TestMethod]
        [TestCategory("LongRunner")]
        public void ValidateInvariantAllExamples()
        {
            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();

            var zip = TestDataHelper.ReadTestZip("examples.zip");
            using (zip)
            {
                foreach (var entry in zip.Entries)
                {
                    Stream file = entry.Open();
                    using (file)
                    {
                        // Verified examples that fail validations
                        // dom-3
                        if (entry.Name.EndsWith("list-example-familyhistory-genetics-profile-annie(prognosis).xml"))
                            continue;
                        if (entry.Name.EndsWith("questionnaire-sdc-profile-example-loinc(questionnaire-sdc-profile-example-loinc).xml"))
                            continue;
                        if (entry.Name.EndsWith("questionnaireresponse-example(3141).xml"))
                            continue;

                        // vsd-3, vsd-8
                        //if (file.EndsWith("valueset-ucum-common(ucum-common).xml"))
                        //    continue;

                        var reader = SerializationUtil.WrapXmlReader(XmlReader.Create(file));
                        var resource = parser.Parse<Resource>(reader);

                        testFileCount++;
                        // Debug.WriteLine(String.Format("Validating {0}", entry.Name));
                        resource.InvariantConstraints = new List<ElementDefinition.ConstraintComponent>();
                        resource.AddDefaultConstraints();
                        var outcome = new OperationOutcome();
                        resource.ValidateInvariants(outcome);
                        if (outcome.Issue.Count > 0)
                        {
                            Debug.WriteLine(String.Format("Validating {0} failed:", entry.Name));
                            foreach (var item in outcome.Issue)
                            {
                                if (!failedInvariantCodes.ContainsKey(item.Details.Coding[0].Code))
                                    failedInvariantCodes.Add(item.Details.Coding[0].Code, 1);
                                else
                                    failedInvariantCodes[item.Details.Coding[0].Code]++;

                                Trace.WriteLine("\t" + item.Details.Coding[0].Code + ": " + item.Details.Text);
                            }

                            Trace.WriteLine("-------------------------");
                            Trace.WriteLine(new FhirXmlSerializer().SerializeToString(resource));
                            Trace.WriteLine("-------------------------");
                        }
                        if (outcome.Issue.Count != 0)
                        {
                            errorCount++;
                        }
                    }
                }
            }

            Console.WriteLine(String.Format("\r\n------------------\r\nValidation failed in {0} of {1} examples", errorCount, testFileCount));
            if (failedInvariantCodes.Count > 0)
            {
                Console.Write("Issues with Invariant: ");
                bool b = false;
                foreach (var item in failedInvariantCodes)
                {
                    if (b)
                        Console.Write(", ");
                    Console.Write(String.Format("{0} ({1})", item.Key, item.Value));
                    b = true;
                }
            }
            Assert.AreEqual(0, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void ValidateInvariantAllExamplesWithOtherConstraints()
        {
           string profiles = TestDataHelper.GetFullPathForExample("profiles-others.xml");

            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();
            List<String> checkedCode = new List<string>();


            Bundle otherSDs;
            Dictionary<string, List<ElementDefinition.ConstraintComponent>> invariantCache = new Dictionary<string, List<ElementDefinition.ConstraintComponent>>();
            using (Stream streamOther = File.OpenRead(profiles))
            {
                otherSDs = new Fhir.Serialization.FhirXmlParser().Parse<Bundle>(SerializationUtil.XmlReaderFromStream(streamOther));
                foreach (StructureDefinition resource in otherSDs.Entry.Select(e => e.Resource).Where(r => r != null && r is StructureDefinition))
                {
                    List<ElementDefinition.ConstraintComponent> cacheForResource;
                    if (invariantCache.ContainsKey(resource.Type.ToString()))
                    {
                        cacheForResource = invariantCache[resource.Type.ToString()];
                    }
                    else
                    {
                        cacheForResource = new List<ElementDefinition.ConstraintComponent>();
                        invariantCache.Add(resource.Type.ToString(), cacheForResource);
                    }

                    // read the invariants for elements in the differential
                    foreach (var ed in resource.Differential.Element)
                    {
                        foreach (var constraint in ed.Constraint)
                        {
                            var ext = constraint.Expression;
                            if (ext == null)
                                continue;
                            string expression = ext;
                            string parentPath = ed.Path;
                            if (parentPath.Contains("."))
                            {
                                // This expression applied to a backbone element, so need to give it scope
                                expression = parentPath.Replace(resource.Type.ToString() + ".", "").Replace("[x]", "") + ".all(" + expression + ")";
                                constraint.Expression = expression;
                            }
                            string key = constraint.Key;
                            if (!string.IsNullOrEmpty(expression))
                            {
                                cacheForResource.Add(constraint);
                            }
                        }
                    }
                }
            }

            var zip = TestDataHelper.ReadTestZip("examples.zip");
            using (zip)
            {
                foreach (var entry in zip.Entries)
                {
                    Stream file = entry.Open();
                    using (file)
                    {
                        // Verified examples that fail validations
                        // dom-3
                        //if (entry.Name.EndsWith("list-example-familyhistory-genetics-profile-annie(prognosis).xml"))
                        //    continue;
                        //if (entry.Name.EndsWith("questionnaire-sdc-profile-example-loinc(questionnaire-sdc-profile-example-loinc).xml"))
                        //    continue;
                        //if (entry.Name.EndsWith("questionnaireresponse-example(3141).xml"))
                        //    continue;
                        //if (entry.Name.EndsWith("dataelement-example(gender).xml"))
                        //    continue;


                        // vsd-3, vsd-8
                        //if (file.EndsWith("valueset-ucum-common(ucum-common).xml"))
                        //    continue;

                        var reader = SerializationUtil.WrapXmlReader(XmlReader.Create(file));
                        var resource = parser.Parse<Resource>(reader);

                        testFileCount++;
                        // Debug.WriteLine(String.Format("Validating {0}", entry.Name));
                        resource.AddDefaultConstraints();
                        if (invariantCache.ContainsKey(resource.ResourceType.ToString()))
                        {
                            resource.InvariantConstraints.AddRange(invariantCache[resource.ResourceType.ToString()]);
                        }
                        var outcome = new OperationOutcome();
                        resource.ValidateInvariants(outcome);
                        // Debug.WriteLine("Key: " + String.Join(", ", resource.InvariantConstraints.Select(s => s.Key)));
                        foreach (var item in resource.InvariantConstraints)
                        {
                            if (checkedCode.Contains(item.Key))
                                continue;
                            checkedCode.Add(item.Key);
                            string expression = item.Expression;
                            if (expression.Contains("[x]"))
                                Debug.WriteLine(String.Format("Expression {0} had an [x] in it '{1}'", item.Key, expression));
                            if (expression.Contains("\"%\""))
                                Debug.WriteLine(String.Format("Expression {0} had an \"%\" in it '{1}'", item.Key, expression));
                            if (expression.Contains("$parent"))
                                Debug.WriteLine(String.Format("Expression {0} had a '$parent' in it '{1}'", item.Key, expression));
                            if (expression.Contains("descendents"))
                                Debug.WriteLine(String.Format("Expression {0} had an 'descendents' in it '{1}'", item.Key, expression));
                            if (expression.Contains("Decimal"))
                                Debug.WriteLine(String.Format("Expression {0} had an 'Decimal' in it '{1}'", item.Key, expression));
                            if (expression.Contains("String"))
                                Debug.WriteLine(String.Format("Expression {0} had an 'String' in it '{1}'", item.Key, expression));
                            if (expression.Contains("Integer"))
                                Debug.WriteLine(String.Format("Expression {0} had an 'Integer' in it '{1}'", item.Key, expression));

                        }
                        // we can skip the US zipcode validations
                        if (outcome.Issue.Where(i => (i.Diagnostics != "address.postalCode.all(matches('[0-9]{5}(-[0-9]{4}){0,1}'))")).Count() > 0)
                        {
                            Debug.WriteLine(String.Format("Validating {0} failed:", entry.Name));
                            if (resource.Meta != null)
                                Debug.WriteLine(String.Format("Reported Profiles: {0}", String.Join(",", resource.Meta.Profile)));
                            foreach (var item in outcome.Issue)
                            {
                                if (!failedInvariantCodes.ContainsKey(item.Details.Coding[0].Code))
                                    failedInvariantCodes.Add(item.Details.Coding[0].Code, 1);
                                else
                                    failedInvariantCodes[item.Details.Coding[0].Code]++;

                                Trace.WriteLine("\t" + item.Details.Coding[0].Code + ": " + item.Details.Text);
                                Trace.WriteLine("\t" + item.Diagnostics);
                            }
                          //  Trace.WriteLine("-------------------------");
                          //  Trace.WriteLine(FhirSerializer.SerializeResourceToXml(resource));
                          //  Trace.WriteLine("-------------------------");
                            // count the issue
                            errorCount++;
                        }
                    }
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
                Debug.WriteLine("");
            }
            // There are 7 example observation resources that don't pass the vital signs profile (and rightly shouldn't)
            Assert.AreEqual(7, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));
        }
    }
}
