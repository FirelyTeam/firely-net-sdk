using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class TypeExtensionTests
    {
        [TestMethod]
        public void VerifyComplexElementDeterminiation()
        {
            Assert.IsFalse(typeof(Patient).RepresentsComplexElementType());
            Assert.IsFalse(typeof(Timing).RepresentsComplexElementType());
            Assert.IsFalse(typeof(FhirBoolean).RepresentsComplexElementType());
            Assert.IsFalse(typeof(Element).RepresentsComplexElementType());
            Assert.IsFalse(typeof(BackboneElement).RepresentsComplexElementType());
            Assert.IsFalse(typeof(Base).RepresentsComplexElementType());
            Assert.IsFalse(typeof(DataType).RepresentsComplexElementType());
            Assert.IsFalse(typeof(Meta).RepresentsComplexElementType());
            Assert.IsFalse(typeof(Narrative).RepresentsComplexElementType());

            Assert.IsTrue(typeof(Patient.ContactComponent).RepresentsComplexElementType());
            Assert.IsTrue(typeof(DataRequirement.CodeFilterComponent).RepresentsComplexElementType());

        }
    }
}
