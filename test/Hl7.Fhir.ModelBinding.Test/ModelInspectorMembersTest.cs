using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.ModelBinding.Test
{
    [TestClass]
    public class ModelInspectorMembersTest
    {
        [TestMethod]
        public void TestElementMapping()
        {
            var inspector = new ModelInspector();

            inspector.Inspect(typeof(TestResource));

            var mappedType = inspector.GetMappedClassForResource("Test");

            
        }

        [FhirResource("Test")]
        public class TestResource : Resource
        {
            public Address.AddressUse? Use { get; set; }

            public Element Poly { get; set; }

            public HumanName Name { get; set; }

            public int? Age { get; set; }

            public string Comments { get; set; }

            public FhirDecimal Salary { get; set; }

            public Code<HumanName.NameUse> NameUse { get; set; }

            public List<ResourceReference> References { get; set; }
            public List<Element> MultiPoly { get; set; }
            public List<int?> MultiAge { get; set; }
            public List<Address.AddressUse> MultiUse { get; set; }

            [NotMapped]
            public string Garbage { get; set; }
        }
    }
}
