/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
#if PORTABLE45
    public class PortableOperationsTests
#else
    public class OperationsTests
#endif

    {
        public const string testEndpoint = "http://fhir-dev.healthintersections.com.au/open";

        [TestMethod] //Server throws error: Access violation at address 000000000129D56C in module 'FHIRServer.exe'. Read of address 0000000000000000
        public void InvokeTestPatientGetEverything()
        {
            var client = new FhirClient(testEndpoint);
            var start = new FhirDateTime(2014,11,1);
            var end = new FhirDateTime(2015,1,1);
            var par = new Parameters().Add("start", start).Add("end", end);
            var bundle = (Bundle)client.InstanceOperation(ResourceIdentity.Build("Patient", "example"), "everything", par);
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = client.FetchPatientRecord(ResourceIdentity.Build("Patient","example"), start, end);
            Assert.IsTrue(bundle2.Entry.Any());
        }

        [TestMethod]
        public void InvokeExpandExistingValueSet()
        {
            var client = new FhirClient(testEndpoint);
            var vs = client.ExpandValueSet(ResourceIdentity.Build("ValueSet","administrative-gender"));
            
            Assert.IsTrue(vs.Expansion.Contains.Any());
        }

        [TestMethod]
        public void InvokeExpandParameterValueSet()
        {
            var client = new FhirClient(testEndpoint);

            var vs = client.Read<ValueSet>("ValueSet/administrative-gender");

            var vsX = client.ExpandValueSet(vs);

            Assert.IsTrue(vsX.Expansion.Contains.Any());
        }

        [TestMethod]
        public void InvokeLookupCoding()
        {
            var client = new FhirClient(testEndpoint);
            var coding = new Coding("http://hl7.org/fhir/administrative-gender", "male");

            var expansion = client.ConceptLookup(coding);

            // Assert.AreEqual("AdministrativeGender", expansion.GetSingleValue<FhirString>("name").Value); // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);               
        }

        [TestMethod]
        public void InvokeLookupCode()
        {
            var client = new FhirClient(testEndpoint);
            var expansion = client.ConceptLookup(new Code("male"), new FhirUri("http://hl7.org/fhir/administrative-gender"));

            //Assert.AreEqual("male", expansion.GetSingleValue<FhirString>("name").Value);  // Returns empty currently on Grahame's server
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod]//returns 500: validation of slices is not done yet.
        public void InvokeResourceValidation()
        {
            var client = new FhirClient(testEndpoint);

            var pat = client.Read<Patient>("Patient/patient-uslab-example1");

            try
            {
                var vresult = client.ValidateResource(pat, null,
                    new FhirUri("http://hl7.org/fhir/StructureDefinition/uslab-patient"));
                Assert.Fail("Should have resulted in 400");
            }
            catch(FhirOperationException fe)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, fe.Status);
                Assert.IsTrue(fe.Outcome.Issue.Where(i => i.Severity == OperationOutcome.IssueSeverity.Error).Any());
            }
        }
    }
}
