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
        public void VerifyNestedTypeDeterminiation()
        {
            Assert.IsFalse(isNested(typeof(Patient)));
            Assert.IsFalse(isNested(typeof(Timing)));
            Assert.IsFalse(isNested(typeof(FhirBoolean)));
            Assert.IsFalse(isNested(typeof(Element)));
            Assert.IsFalse(isNested(typeof(BackboneElement)));
            Assert.IsFalse(isNested(typeof(Base)));
            Assert.IsFalse(isNested(typeof(DataType)));
            Assert.IsFalse(isNested(typeof(Meta)));
            Assert.IsFalse(isNested(typeof(Narrative)));

            Assert.IsTrue(isNested(typeof(Patient.ContactComponent)));
            Assert.IsTrue(isNested(typeof(DataRequirement.CodeFilterComponent)));

            bool isNested(Type testee)
            {
                _ = ClassMapping.TryCreate(testee, out var cm);
                return cm.IsNestedType;
            }
        }
    }
}
