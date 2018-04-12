/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using System.Linq;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class ReferenceExtensionsTest
    {
        [TestMethod]
        public void GetAbsoluteUri()
        {
            var r = new ResourceReference { Reference = "Patient/4" };
            Assert.AreEqual("http://someserver.org/fhir/Patient/4", r.GetAbsoluteUriForReference("http://someserver.org/fhir/Observation/5").ToString());

            r.Reference = "http://otherserver.org/fhir/Patient/4";
            Assert.AreEqual("http://otherserver.org/fhir/Patient/4", r.GetAbsoluteUriForReference("http://someserver.org/fhir/Observation/5").ToString());
            Assert.AreEqual("http://otherserver.org/fhir/Patient/4", r.GetAbsoluteUriForReference("urn:uuid:d0dd51d3-3ab2-4c84-b697-a630c3e40e7a").ToString());

            r.Reference = "urn:uuid:d0dd51d3-3ab2-4c84-b697-a630c3e40e7a";
            Assert.AreEqual("urn:uuid:d0dd51d3-3ab2-4c84-b697-a630c3e40e7a", r.GetAbsoluteUriForReference("http://someserver.org/fhir/Observation/5").ToString());

            try
            {
                r.Reference = "Patient/4";
                var dummy = r.GetAbsoluteUriForReference("urn:uuid:d0dd51d3-3ab2-4c84-b697-a630c3e40e7a");
                Assert.Fail();
            }
            catch
            { }        
        }    
    }
}
