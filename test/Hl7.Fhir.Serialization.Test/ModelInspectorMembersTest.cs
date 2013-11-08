using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization.Test
{
    [TestClass]
    public class ModelInspectorMembersTest
    {
        [TestMethod]
        public void TestElementMapping()
        {
            var inspector = new ModelInspector();

            inspector.Import(typeof(Resource).Assembly);
            inspector.Import(typeof(TestResource));
            inspector.Process();

            var mappedType = inspector.FindClassMappingForResource("Test");
            Assert.IsNotNull(mappedType);

            //var member = mappedType.FindMappedPropertyForElement("use");
            //Assert.IsTrue(member.IsEnumeratedType);
            //Assert.IsFalse(member.IsPolymorhic);
            //Assert.AreEqual(typeof(Address.AddressUse), member.MappedPropertyType.ImplementingType);

            var member = mappedType.FindMappedPropertyForElement("poly");
            Assert.IsTrue(member.IsPolymorhic);
            Assert.IsNull(member.MappedPropertyType);
            Assert.AreEqual(typeof(Element),member.PolymorphicBase);

            member = mappedType.FindMappedPropertyForElement("naturalName");
            Assert.IsNotNull(member);
            Assert.AreEqual("HumanName", member.MappedPropertyType.Name);

            member = mappedType.FindMappedPropertyForElement("nameuse");
            Assert.IsTrue(member.IsCodeOfTProperty);
            Assert.AreEqual(typeof(HumanName.NameUse), member.CodeOfTEnumType);

            member = mappedType.FindMappedPropertyForElement("references");
            Assert.IsTrue(member.MayRepeat);
            Assert.AreEqual("ResourceReference", member.MappedPropertyType.Name);

            member = mappedType.FindMappedPropertyForElement("multipoly");
            Assert.IsTrue(member.MayRepeat);
            Assert.IsTrue(member.IsPolymorhic);
            Assert.AreEqual(typeof(Resource), member.PolymorphicBase);

            member = mappedType.FindMappedPropertyForElement("garbage");
            Assert.IsNull(member);
        }


        [FhirResource("Test")]
        public class TestResource
        {
          //  public Address.AddressUse? Use { get; set; }

            public Element Poly { get; set; }

            [FhirElement("NaturalName")]
            public HumanName Name { get; set; }

          //  public int? Age { get; set; }

          //  public string Comments { get; set; }

             public Code<HumanName.NameUse> NameUse { get; set; }

            public List<ResourceReference> References { get; set; }
            public List<Resource> MultiPoly { get; set; }
         //   public List<int?> MultiAge { get; set; }
         //   public List<Address.AddressUse> MultiUse { get; set; }

            [NotMapped]
            public string Garbage { get; set; }
        }
    }
}
