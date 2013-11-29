using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
    public class ModelInspectorMembersTest
    {
        [TestMethod]
        public void TestPrimitiveDataTypeMapping()
        {
            var mapping = ClassMapping.Create(typeof(Base64Binary));
            Assert.AreEqual("base64Binary", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count);
            var valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.AreEqual("value", valueProp.Name);
            Assert.IsFalse(valueProp.IsCollection);     // don't see byte[] as a collection of byte in FHIR
            Assert.IsTrue(valueProp.RepresentsValueElement);

            mapping = ClassMapping.Create(typeof(Code<Address.AddressUse>));
            Assert.AreEqual("codeOfT<Hl7.Fhir.Model.Address+AddressUse>", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count);
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection);
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(Address.AddressUse),valueProp.ElementType);

            mapping = ClassMapping.Create(typeof(FhirUri));
            Assert.AreEqual("uri", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count);
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection); 
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(Uri),valueProp.ElementType);
        }
    }
}
