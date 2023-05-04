/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using static Hl7.Fhir.Tests.Rest.FhirClientTests;
using FluentAssertions;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class OperationsTests
    {
        private static readonly string TESTENDPOINT = TestEndpoint.OriginalString;

        [Ignore("FS reports not implemented")]
        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeTestPatientGetEverythingHttpClient()
        {
            using var client = new FhirClient(TESTENDPOINT);

            var start = new FhirDateTime(2014, 11, 1);
            var end = new FhirDateTime(2015, 1, 1);
            var par = new Parameters().Add("start", start).Add("end", end);
            var bundle = await client.InstanceOperationAsync(ResourceIdentity.Build("Patient", PATIENTID), "everything", par) as Bundle;
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = await client.FetchPatientRecordAsync(ResourceIdentity.Build("Patient", PATIENTID), start, end);
            Assert.IsTrue(bundle2.Entry.Any());
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeExpandExistingValueSetHttpClient()
        {
            using var client = new FhirClient(TerminologyEndpoint);

            // expand via instance level operation
            var vs = await client.ExpandValueSetAsync(ResourceIdentity.Build("ValueSet", "administrative-gender"));
            Assert.IsTrue(vs.Expansion.Contains.Any());

            // expand via Canonical URI
            vs = await client.ExpandValueSetAsync(new FhirUri("http://hl7.org/fhir/ValueSet/administrative-gender"));
            Assert.IsTrue(vs.Expansion.Contains.Any());
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeExpandParameterValueSetHttpClient()
        {
            using var client = new FhirClient(FhirClientTests.TerminologyEndpoint);

            var vs = await client.ReadAsync<ValueSet>("ValueSet/administrative-gender");
            var vsX = await client.ExpandValueSetAsync(vs);

            Assert.IsTrue(vsX.Expansion.Contains.Any());
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeLookupCodingHttpClient()
        {
            using var client = new FhirClient(FhirClientTests.TerminologyEndpoint);

            var coding = new Coding("http://hl7.org/fhir/administrative-gender", "male");

            var expansion = await client.ConceptLookupAsync(coding: coding);

            // Assert.AreEqual("AdministrativeGender", expansion.GetSingleValue<FhirString>("name").Value); // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeLookupCodeHttpClient()
        {
            using var client = new FhirClient(FhirClientTests.TerminologyEndpoint);
            var expansion = await client.ConceptLookupAsync(code: new Code("male"), system: new FhirUri("http://hl7.org/fhir/administrative-gender"));

            //Assert.AreEqual("male", expansion.GetSingleValue<FhirString>("name").Value);  // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public void InvokeValidateCodeByIdHttpClient()
        {
            using var client = new FhirClient(FhirClientTests.TerminologyEndpoint);

            var coding = new Coding("http://snomed.info/sct", "4322002");

            var result = client.ValidateCode("c80-facilitycodes", coding: coding, @abstract: new FhirBoolean(false));
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeValidateCodeByCanonicalHttpClient()
        {
            using var client = new FhirClient(FhirClientTests.TerminologyEndpoint);

            var coding = new Coding("http://snomed.info/sct", "4322002");

            var result = await client.ValidateCodeAsync(url: new FhirUri("http://hl7.org/fhir/ValueSet/c80-facilitycodes"),
                  coding: coding, @abstract: new FhirBoolean(false));
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeValidateCodeWithVSHttpClient()
        {
            using var client = new FhirClient(TerminologyEndpoint);

            var coding = new Coding("http://snomed.info/sct", "4322002");

            var vs = await client.ReadAsync<ValueSet>("ValueSet/c80-facilitycodes");
            Assert.IsNotNull(vs);

            var result = client.ValidateCode(valueSet: vs, coding: coding);
            Assert.IsTrue(result.Result?.Value == true);
        }

        [TestMethod]
        [TestCategory("FhirClient"), TestCategory("IntegrationTest")]
        public async Task InvokeResourceValidationHttpClient()
        {
            using var client = new FhirClient(TESTENDPOINT);

            var pat = await client.ReadAsync<Patient>(PATIENTIDEP);
            var vresult = await client.ValidateResourceAsync(pat, null,
                new FhirUri("http://hl7.org/fhir/StructureDefinition/Patient"));
            Assert.IsTrue(vresult.Success);
        }

#if R5
    [Ignore("No R5 version of FS yet - this operation fails when run on R5.")]
#endif
        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task TestOperationEverythingHttpClient()
        {
            using FhirClient client = new FhirClient(TestEndpoint);
            // GET operation $everything without parameters
            var uri = new Uri(PATIENTIDEP, UriKind.Relative);

            var loc = await client.InstanceOperationAsync(uri, "everything", useGet: true);
            Assert.IsNotNull(loc);
        }


        [Ignore("FS returns operation not implemented on these tests")]
        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task TestOperationEverythingWithParamsHttpClient()
        {
            using FhirClient client = new FhirClient(TestEndpoint);
            var uri = new Uri(PATIENTIDEP, UriKind.Relative);

            // POST operation $everything without parameters
            var loc = await client.InstanceOperationAsync(uri, "everything", useGet: false);
            Assert.IsNotNull(loc);

            // GET operation $everything with 1 primitive parameter
            loc = await client.InstanceOperationAsync(uri, "everything", new Parameters().Add("start", new Date(2017, 11)), useGet: true);
            Assert.IsNotNull(loc);

            // GET operation $everything with 1 primitive2token parameter
            loc = await client.InstanceOperationAsync(uri, "everything", new Parameters().Add("start", new Identifier("", "example")), useGet: true);
            Assert.IsNotNull(loc);

            // GET operation $everything with 1 resource parameter
            try
            {
                loc = await client.InstanceOperationAsync(uri, "everything", new Parameters().Add("start", new Patient()), useGet: true);
                Assert.Fail("An InvalidOperationException was expected here");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException), ex.Message);
            }

            // GET operation $everything with 1 complex parameter
            var act = () => client.InstanceOperationAsync(uri, "everything", new Parameters().Add("start", new Annotation() { Text = new Markdown("test") }), useGet: true);
            await act.Should().ThrowAsync<InvalidOperationException>();

            // POST operation $everything with 1 parameter
            loc = await client.InstanceOperationAsync(uri, "everything", new Parameters().Add("start", new Date(2017, 10)), useGet: false);
            Assert.IsNotNull(loc);
        }                
    }
}