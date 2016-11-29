﻿/* 
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
            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();

            var zip = ZipFile.OpenRead(examplesZip);
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
#if !NETCore
                                Trace.WriteLine("\t" + item.Details.Coding[0].Code + ": " + item.Details.Text);
#endif
                            }
#if !NETCore
                            Trace.WriteLine("-------------------------");
                            Trace.WriteLine(FhirSerializer.SerializeResourceToXml(resource));
                            Trace.WriteLine("-------------------------");
#endif
                        }
                        if (outcome.Issue.Count != 0)
                        {
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
            }
            Assert.AreEqual(0, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void ValidateInvariantAllExamplesWithOtherConstraints()
        {
#if NETCore
            string examplesZip = $@"{AppContext.BaseDirectory}\TestData\examples.zip";
            string profiles = $@"{AppContext.BaseDirectory}\TestData\profiles-others.xml";
#else
            string examplesZip = @"TestData\examples.zip";
            string profiles = @"TestData\profiles-others.xml";
           
#endif
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
                    if (invariantCache.ContainsKey(resource.ConstrainedType.ToString()))
                    {
                        cacheForResource = invariantCache[resource.ConstrainedType.ToString()];
                    }
                    else
                    {
                        cacheForResource = new List<ElementDefinition.ConstraintComponent>();
                        invariantCache.Add(resource.ConstrainedType.ToString(), cacheForResource);
                    }

                    // read the invariants for elements in the differential
                    foreach (var ed in resource.Differential.Element)
                    {
                        foreach (var constraint in ed.Constraint)
                        {
                            var ext = constraint.GetExtensionValue<FhirString>("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
                            if (ext == null)
                                continue;
                            string expression = ext.Value;
                            string parentPath = ed.Path;
                            if (parentPath.Contains("."))
                            {
                                // This expression applied to a backbone element, so need to give it scope
                                expression = parentPath.Replace(resource.ConstrainedType.ToString() + ".", "") + ".all(" + expression + ")";
                                ext.Value = expression;
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

            var zip = ZipFile.OpenRead(examplesZip);
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
                        if (entry.Name.EndsWith("dataelement-example(gender).xml"))
                            continue;


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
                            string expression = item.GetExtensionValue<FhirString>("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression").Value;
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
                            foreach (var item in outcome.Issue)
                            {
                                if (!failedInvariantCodes.ContainsKey(item.Details.Coding[0].Code))
                                    failedInvariantCodes.Add(item.Details.Coding[0].Code, 1);
                                else
                                    failedInvariantCodes[item.Details.Coding[0].Code]++;
#if !NETCore
                                Trace.WriteLine("\t" + item.Details.Coding[0].Code + ": " + item.Details.Text);
                                Trace.WriteLine("\t" + item.Diagnostics);
#endif
                            }
#if !NETCore
                            Trace.WriteLine("-------------------------");
                            Trace.WriteLine(FhirSerializer.SerializeResourceToXml(resource));
                            Trace.WriteLine("-------------------------");
#endif
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
            }
            Assert.AreEqual(2, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));
        }
    }
}
