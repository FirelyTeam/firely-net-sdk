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

        [TestMethod]
        public void InvokeTestPatientGetEverything()
        {
            var client = new FhirClient(testEndpoint);
            var start = new FhirDateTime(2014,11,1);
            var end = new FhirDateTime(2015,1,1);
            var par = new Parameters().Add("start", start).Add("end", end);
            var bundle = (Bundle)client.Operation(ResourceIdentity.Build("Patient", "1"), "everything", par);
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = client.FetchPatientRecord(ResourceIdentity.Build("Patient","1"), start, end);
            Assert.IsTrue(bundle2.Entry.Any());
        }

        [TestMethod]
        public void InvokeExpandExistingValueSet()
        {
            var client = new FhirClient(testEndpoint);
            var vs = client.ExpandValueSet(ResourceIdentity.Build("ValueSet","administrative-gender"));
            
            Assert.IsTrue(vs.Expansion.Contains.Any());
        }

        [TestMethod,Ignore]
        public void InvokeExpandParameterValueSet()
        {
            var client = new FhirClient(testEndpoint);

            var vs = client.Read<ValueSet>("ValueSet/101");

            var vsX = client.ExpandValueSet(vs);

            Assert.IsTrue(vs.Expansion.Contains.Any());
        }

        [TestMethod,Ignore]
        public void InvokeLookupCoding()
        {
            var client = new FhirClient(testEndpoint);
            var coding = new Coding("http://hl7.org/fhir/administrative-gender", "male");

            var expansion = client.ConceptLookup(coding);

            Assert.AreEqual("AdministrativeGender", expansion.GetSingleValue<FhirString>("name").Value);
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);               
        }

        [TestMethod,Ignore]
        public void InvokeLookupCode()
        {
            var client = new FhirClient(testEndpoint);
            var expansion = client.ConceptLookup(new Code("male"), new FhirUri("http://hl7.org/fhir/administrative-gender"));

            Assert.AreEqual("AdministrativeGender", expansion.GetSingleValue<FhirString>("name").Value);
            Assert.AreEqual("Male", expansion.GetSingleValue<FhirString>("display").Value);
        }

        [TestMethod]
        public void InvokeResourceValidation()
        {
            var client = new FhirClient(testEndpoint);

            var pat = client.Read<Patient>(ResourceIdentity.Build("Patient", "uslab-example1"));
            var vresult = client.ValidateResource(pat, null, new FhirUri("http://fhir-dev.healthintersections.com.au/open/Profile/patient-uslab-uslabpatient"));

            Assert.IsTrue(vresult.Success());
        }
    }
}
