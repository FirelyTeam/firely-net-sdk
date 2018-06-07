using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Introspection
{
    public static class SerializationInfoTestHelpers
    {
        //[TestMethod]
        //public void TestResourceInfo()
        //{
        //    var ip = new PocoModelMetadataProvider();
        //    Assert.IsTrue(ip.IsResource("Patient"));
        //    Assert.IsTrue(ip.IsResource("DomainResource"));
        //    Assert.IsTrue(ip.IsResource("Resource"));
        //    Assert.IsFalse(ip.IsResource("Identifier"));
        //}

        public static void TestCanLocateTypes(IModelMetadataProvider provider)
        {
            // Try getting a resource
            tryGetType("Patient");

            // Try getting an abstract resource
            tryGetType("DomainResource", isAbstract:true);
            tryGetType("Resource", isAbstract: true);

            // Try a complex datatype
            tryGetType("HumanName");

            // Try getting an abstract datatype
            tryGetType("Element", isAbstract: true);

            // Try a primitive
            tryGetType("string");

            // Try constrained quantities
            tryGetType("SimpleQuantity", "Quantity");
            tryGetType("Distance", "Quantity");

            // The weird xhtml datatype
            tryGetType("xhtml");

            void tryGetType(string typename, string baseTypeName=null, bool isAbstract = false)
            {
                var si = provider.GetSerializationInfoForStructure(typename);
                Assert.IsNotNull(si);
                Assert.AreEqual(baseTypeName ?? typename, si.TypeName);
                Assert.AreEqual(isAbstract, si.IsAbstract);
            }
        }

        public static void TestCanGetElements(IModelMetadataProvider provider)
        {
            var p = provider.GetSerializationInfoForStructure("Patient");

            // Simple element
            checkType(p, "active", false, "boolean");

            // Simple element (repeating)
            checkType(p,"identifier", true, "Identifier");

            // Backbone element (repeating)
            var bbe = checkBBType(p,"contact", true);

            // Navigate into the backbone element
            checkType(bbe, "relationship", true, "CodeableConcept");

            // Choice type
            checkType(p, "deceased", false, "boolean", "dateTime");

            // Get base elements
            checkType(p, "text", false, "Narrative");
            checkType(p, "contained", true, "Resource");
            checkType(p, "extension", true, "Extension");
            checkType(p, "id", false, "id");
            checkType(p, "meta", false, "Meta");

            // Should not have the special "value" attribute
            Assert.IsFalse(p.GetChildren().Any(c => c.ElementName == "value"));

            var b = provider.GetSerializationInfoForStructure("Bundle");
            checkType(b, "total", false, "unsignedInt");
            checkType(b, "type", false, "code");
        }

        private static void checkType(IComplexTypeSerializationInfo parent, string ename, bool mayRepeat, params string[] types)
        {
            var child = parent.GetChildren().SingleOrDefault(c => c.ElementName == ename);
            Assert.IsNotNull(child);
            Assert.AreEqual(types.Count() > 1, child.IsChoiceElement);
            Assert.AreEqual(mayRepeat, child.MayRepeat);
            Assert.IsTrue(child.Type.All(t => t is ITypeReference));
            CollectionAssert.AreEqual(types, child.Type
                .Cast<ITypeReference>()
                .Select(t => t.TypeName).ToArray());
        }

        private static IComplexTypeSerializationInfo checkBBType(IComplexTypeSerializationInfo parent, string ename, bool mayRepeat)
        {
            var child = parent.GetChildren().SingleOrDefault(c => c.ElementName == ename);

            Assert.IsNotNull(child);
            Assert.AreEqual(mayRepeat, child.MayRepeat);
            var result = child.Type.Single() as IComplexTypeSerializationInfo;
            Assert.AreEqual("BackboneElement", result.TypeName);
            Assert.IsNotNull(result);

            return result;
        }


        public static void TestSpecialTypes(IModelMetadataProvider provider)
        {           
            // Narrative.div
            var div = provider.GetSerializationInfoForStructure("Narrative");
            Assert.IsNotNull(div);
            checkType(div, "div", false, "xhtml");

            // Element.id
            checkType(div, "id", false, "id");

            var ext = provider.GetSerializationInfoForStructure("Extension");

            // Extension.url
            checkType(ext, "url", false, "uri");
        }
    }
}
