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

                        // vsd-9
                        if (entry.Name.EndsWith("valueset-example-expansion(example-expansion).xml"))
                            continue;


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
            }
            Assert.AreEqual(22, errorCount, String.Format("Failed Validating {0} of {1} examples", errorCount, testFileCount));

            /*
            Validating dataelement-labtestmaster-example(prothrombin).xml failed:
                inv-2: One and only one DataElement.code must have is-data-element-concept set to "true"
                code.extension(%"ext-11179-de-is-data-element-concept").count() = 1
            Validating dataelement-sdc-profile-example(dataelement-sdc-profile-example).xml failed:
                inv-2: One and only one DataElement.code must have is-data-element-concept set to "true"
                code.extension(%"ext-11179-de-is-data-element-concept").count() = 1
            Validating dataelement-sdc-profile-example-de(dataelement-sdc-profile-example-de).xml failed:
                inv-2: One and only one DataElement.code must have is-data-element-concept set to "true"
                code.extension(%"ext-11179-de-is-data-element-concept").count() = 1
            Validating organization-example-f001-burgers(f001).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating organization-example-f201-aumc(f201).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating organization-example-f203-bumc(f203).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating patient-example(example).xml failed:
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all(matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating patient-example-f001-pieter(f001).xml failed:
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all(matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating patient-example-f201-roel(f201).xml failed:
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all(matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating patient-example-us-extensions(us01).xml failed:
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
                inv-1: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all(matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f001-evdb(f001).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f002-pv(f002).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f003-mv(f003).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f004-rb(f004).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f005-al(f005).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f006-rvdb(f006).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f007-sh(f007).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f201-ab(f201).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f202-lm(f202).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f203-jvg(f203).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating practitioner-example-f204-ce(f204).xml failed:
                inv-2: (Zip or Postal Code) SHALL be formatted as 99999[-9999] for US Zip or ZIP +4 codes or as A9A9A9 for Canadian postal codes.
                address.postalCode.all($this.matches('[0-9]{5}(-[0-9]{4}){0,1}'))
            Validating valueset-example-expansion(example-expansion).xml failed:
	            vsd-9: Must have a code if not abstract
	            expansion.contains.all(code.exists() or (abstract = 'true'))

            ------------------
            Validation failed in 22 of 725 examples
            Issues with Invariant: inv-2 (23), inv-1 (12), vsd-9 (1)

                */

        }
    }
}
