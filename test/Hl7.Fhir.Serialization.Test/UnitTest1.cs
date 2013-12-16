using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class RestTest
    {
        [TestMethod]
        public void CreateFromEndPoint()
        {
            Endpoint endpoint = new Endpoint("http://localhost/fhir");
            
            RestUri uriPatient = endpoint.Collection("patient");
            Assert.AreEqual("http://localhost/fhir/patient", uriPatient.AsString);

            RestUri uriPatients = endpoint.Resource("patient", "1");
            Assert.AreEqual("http://localhost/fhir/patient/1", uriPatients.AsString);
        }

        [TestMethod]
        public void Query()
        {
            Endpoint endpoint = new Endpoint("http://localhost/fhir/");
            RestUri rest = endpoint.Search("organization");
            rest.Parameters.Add("family", "Johnson");
            rest.Parameters.Add("given", "William");
            Assert.AreEqual("http://localhost/fhir/organization/_search?family=Johnson&given=William", rest.Uri);
        }
    }
}
