/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Legacy;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class OperationsTests

    {
        string testEndpoint = FhirClientTests.testEndpoint.OriginalString;

        [TestMethod] 
        [TestCategory("IntegrationTest")]
        public void InvokeTestPatientGetEverything()
        {
            var client = new LegacyFhirClient(testEndpoint);
            patientGetEverything(client);            
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeTestPatientGetEverythingHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                patientGetEverything(client);
            }
        }

        private void patientGetEverything(BaseFhirClient client)
        {
            var start = new FhirDateTime(2014, 11, 1);
            var end = new FhirDateTime(2015, 1, 1);
            var par = new Parameters().Add("start", start).Add("end", end);
            var bundle = (Bundle)client.InstanceOperation(ResourceIdentity.Build("Patient", "example"), "everything", par);
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = client.FetchPatientRecord(ResourceIdentity.Build("Patient", "example"), start, end);
            Assert.IsTrue(bundle2.Entry.Any());
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeExpandExistingValueSet()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            expandExistingValueset(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeExpandExistingValueSetHttpClient()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                expandExistingValueset(client);
            };            
        }

        private static void expandExistingValueset(BaseFhirClient client)
        {
            var vs = client.ExpandValueSet(ResourceIdentity.Build("ValueSet", "administrative-gender"));
            Assert.IsTrue(vs.Expansion.Contains.Any());
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeExpandParameterValueSet()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            expandParameterValueSet(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeExpandParameterValueSetHttpClient ()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                expandParameterValueSet(client);
            }            
        }

        private static void expandParameterValueSet(BaseFhirClient client)
        {
            var vs = client.Read<ValueSet>("ValueSet/administrative-gender");
            var vsX = client.ExpandValueSet(vs);

            Assert.IsTrue(vsX.Expansion.Contains.Any());
        }

        // [WMR 20170927] Chris Munro
        // https://chat.fhir.org/#narrow/stream/implementers/subject/How.20to.20expand.20ValueSets.20with.20the.20C.23.20FHIR.20API.3F
        //[TestMethod]
        //[TestCategory("IntegrationTest")]
        //[Ignore]
        //public void TestExpandValueSet()
        //{
        //    const string endpoint = @"https://stu3.simplifier.net/open/";
        //    var location = new FhirUri("https://stu3.simplifier.net/open/ValueSet/043d233c-4ecf-4802-a4ac-75d82b4291c2");
        //    var client = new FhirClient(endpoint);
        //    var expandedValueSet = client.ExpandValueSet(location, null);
        //}

        /// <summary>
        /// http://hl7.org/fhir/valueset-operations.html#lookup
        /// </summary>
        [TestMethod]  // Server returns internal server error
        [TestCategory("IntegrationTest")]
        public void InvokeLookupCoding()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            lookupCoding(client);
        }

        [TestMethod] // Server returns internal server error
        [TestCategory("IntegrationTest")]
        public void InvokeLookupCodingHttpClient()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                lookupCoding(client);
            }
        }

        private static void lookupCoding(BaseFhirClient client)
        {
            var coding = new Coding("http://hl7.org/fhir/administrative-gender", "male");

            var expansion = client.ConceptLookup(coding: coding);

            // Assert.AreEqual("AdministrativeGender", expansion.GetSingleValue<FhirString>("name").Value); // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod] // Server returns internal server error
        [TestCategory("IntegrationTest")]
        public void InvokeLookupCode()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            lookUpCode(client);
        }

        [TestMethod] // Server returns internal server error
        [TestCategory("IntegrationTest")]
        public void InvokeLookupCodeHttpClient()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                lookUpCode(client);
            };            
        }

        private static void lookUpCode(BaseFhirClient client)
        {
            var expansion = client.ConceptLookup(code: new Code("male"), system: new FhirUri("http://hl7.org/fhir/administrative-gender"));

            //Assert.AreEqual("male", expansion.GetSingleValue<FhirString>("name").Value);  // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeById()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            validateCodeById(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeByIdHttpClient()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                validateCodeById(client);
            }
        }

        private static void validateCodeById(BaseFhirClient client)
        {
            var coding = new Coding("http://snomed.info/sct", "4322002");

            var result = client.ValidateCode("c80-facilitycodes", coding: coding, @abstract: new FhirBoolean(false));
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeByCanonical()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            validateCodeByCanonical(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeByCanonicalHttpClient ()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                validateCodeByCanonical(client);
            }            
        }


        private static void validateCodeByCanonical(BaseFhirClient client)
        {
            var coding = new Coding("http://snomed.info/sct", "4322002");

            var result = client.ValidateCode(url: new FhirUri("http://hl7.org/fhir/ValueSet/c80-facilitycodes"),
                  coding: coding, @abstract: new FhirBoolean(false));
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeWithVS()
        {
            var client = new LegacyFhirClient(FhirClientTests.TerminologyEndpoint);
            validateCodeWithVS(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeValidateCodeWithVSHttpClient()
        {
            using (var client = new FhirClient(FhirClientTests.TerminologyEndpoint))
            {
                validateCodeWithVS(client);
            };           
        }

        private static void validateCodeWithVS(BaseFhirClient client)
        {
            var coding = new Coding("http://snomed.info/sct", "4322002");

            var vs = client.Read<ValueSet>("ValueSet/c80-facilitycodes");
            Assert.IsNotNull(vs);

            var result = client.ValidateCode(valueSet: vs, coding: coding);
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeResourceValidation()
        {
            var client = new LegacyFhirClient(testEndpoint);
            validateResource(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void InvokeResourceValidationHttpClient()
        {
            using (var client = new FhirClient(testEndpoint))
            {
                validateResource(client);
            }            
        }

        private static void validateResource(BaseFhirClient client)
        {
            var pat = client.Read<Patient>("Patient/pat1");
            var vresult = client.ValidateResource(pat, null,
                new FhirUri("http://hl7.org/fhir/StructureDefinition/Patient"));
            Assert.IsTrue(vresult.Success);          
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task InvokeTestPatientGetEverythingAsync()
        {
            string _endpoint = "https://api.hspconsortium.org/rpineda/open";
            var client = new LegacyFhirClient(_endpoint);
            await patientEverythingAsync(client).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task InvokeTestPatientGetEverythingAsyncHttpClient()
        {
            string _endpoint = "https://api.hspconsortium.org/rpineda/open";
            using (var client = new FhirClient(_endpoint))
            {
                await patientEverythingAsync(client).ConfigureAwait(false);
            }           
        }

        private static async System.Threading.Tasks.Task patientEverythingAsync(BaseFhirClient client)
        {
            var start = new FhirDateTime(2014, 11, 1);
            var end = new FhirDateTime(2020, 1, 1);
            var par = new Parameters().Add("start", start).Add("end", end);

            var bundleTask = client.InstanceOperationAsync(ResourceIdentity.Build("Patient", "SMART-1288992"), "everything", par);
            var bundle2Task = client.FetchPatientRecordAsync(ResourceIdentity.Build("Patient", "SMART-1288992"), start, end);

            await bundleTask.ConfigureAwait(false);
            await bundle2Task.ConfigureAwait(false);

            var bundle = (Bundle)bundleTask.Result;
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = (Bundle)bundle2Task.Result;
            Assert.IsTrue(bundle2.Entry.Any());
        }
    }
}
