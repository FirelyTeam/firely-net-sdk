using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TestRestUrl
    {
        [TestMethod]
        public void CreateFromEndPoint()
        {
            Endpoint endpoint = new Endpoint("http://localhost/fhir");
            RestUrl resturi;

            resturi = endpoint.Collection("patient");
            Assert.AreEqual("http://localhost/fhir/patient", resturi.AsString);

            resturi = endpoint.Resource("patient", "1");
            Assert.AreEqual("http://localhost/fhir/patient/1", resturi.AsString);

            resturi = endpoint.Resource("patient", "1");
            Assert.AreEqual("http://localhost/fhir/patient/1", resturi.AsString);
        }

        [TestMethod]
        public void Query()
        {
            Endpoint endpoint = new Endpoint("http://localhost/fhir/");
            RestUrl resturi;
            
            resturi = endpoint.Search("organization").Param("family", "Johnson").Param("given", "William");
            Assert.AreEqual("http://localhost/fhir/organization/_search?family=Johnson&given=William", resturi.AsString);
        }

       
    }
}
