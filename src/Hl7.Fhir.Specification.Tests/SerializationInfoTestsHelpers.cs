using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace Hl7.Fhir.Serialization.Tests
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

        public static void TestCanLocateTypesByCanonical(IStructureDefinitionSummaryProvider provider)
        {
            // Try getting a resource
            tryGetType("Patient");

            // Try getting an abstract resource
            tryGetType("DomainResource", isAbstract: true);
            tryGetType("Resource", isAbstract: true);

            // Try a complex datatype
            tryGetType("HumanName");

            // Try getting an abstract datatype
            tryGetType("Element", isAbstract: true);

            // Try a primitive
            tryGetType("string");

            // Try constrained quantities
            tryGetType("Money", "Money");
            tryGetType("Distance", "Distance");

            // The weird xhtml datatype
            tryGetType("xhtml");

            void tryGetType(string typename, string baseTypeName = null, bool isAbstract = false)
            {
                var si = provider.Provide("http://hl7.org/fhir/StructureDefinition/" + typename);
                Assert.IsNotNull(si, typename);
                Assert.AreEqual(baseTypeName ?? typename, si.TypeName);
                Assert.AreEqual(isAbstract, si.IsAbstract);
            }
        }

        public static void TestCanLocateTypes(IStructureDefinitionSummaryProvider provider)
        {
            // Try getting a resource
            tryGetType("Patient");

            // Try getting an abstract resource
            tryGetType("DomainResource", isAbstract: true);
            tryGetType("Resource", isAbstract: true);

            // Try a complex datatype
            tryGetType("HumanName");

            // Try getting an abstract datatype
            tryGetType("Element", isAbstract: true);

            // Try a primitive
            tryGetType("string");

            // Try constrained quantities
            tryGetType("Money", "Money");
            tryGetType("Distance", "Distance");

            // The weird xhtml datatype
            tryGetType("xhtml");

            void tryGetType(string typename, string baseTypeName = null, bool isAbstract = false)
            {
                var si = provider.Provide(typename);
                Assert.IsNotNull(si);
                Assert.AreEqual(baseTypeName ?? typename, si.TypeName);
                Assert.AreEqual(isAbstract, si.IsAbstract);
            }
        }

        public static void TestRequiresSnapshot(IStructureDefinitionSummaryProvider provider)
        {
            // provide a structuredefinition from our test source, which does not contain snapshots
            // this should fail. e.g.  // http://validationtest.org/fhir/StructureDefinition/DutchPatient
            var sdWithoutSnapshot = "http://validationtest.org/fhir/StructureDefinition/DutchPatient";

            try
            {
                _ = provider.Provide(sdWithoutSnapshot);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("has no snapshot component"));
            }
        }

        public static void TestValueIsNotAChild(IStructureDefinitionSummaryProvider provider)
        {
            var p = provider.Provide("string");
            var children = p.GetElements();
            Assert.IsFalse(children.Any(c => c.ElementName == "value"));
        }

        public static void TestCanGetElements(IStructureDefinitionSummaryProvider provider)
        {
            var p = provider.Provide("Patient");

            // Simple element (repeating)
            checkType(p, "identifier", true, "Identifier");

            // Simple element
            checkType(p, "active", false, "boolean");

            // Element with multiple reference types
            checkType(p, "generalPractitioner", true, "Reference");

            // Backbone element (repeating)
            var bbe = checkBBType(p, "contact", "BackboneElement", true);

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
            Assert.IsFalse(p.GetElements().Any(c => c.ElementName == "value"));

            var b = provider.Provide("Bundle");
            checkType(b, "total", false, "unsignedInt");
            checkType(b, "type", false, "code");

            // Test types using nameReference
            var q = provider.Provide("Questionnaire");
            var qgroup = checkBBType(q, "item", "BackboneElement", true);
            checkType(qgroup, "linkId", false, "string");
            var qgroupgroup = checkBBType(qgroup, "item", "BackboneElement", true);
            checkType(qgroupgroup, "linkId", false, "string");

            // Backbone elements within datatypes
            var tm = provider.Provide("Timing");
            checkBBType(tm, "repeat", "Element", false);
        }

        private static void checkType(IStructureDefinitionSummary parent, string ename, bool mayRepeat, params string[] types)
        {
            var child = parent.GetElements().SingleOrDefault(c => c.ElementName == ename);
            Assert.IsNotNull(child);
            Assert.AreEqual(types.Count() > 1, child.IsChoiceElement);
            Assert.AreEqual(mayRepeat, child.IsCollection);
            Assert.IsTrue(child.Type.All(t => t is IStructureDefinitionReference));

            if (types.Length == 1 && child.Type.Length == 1)
            {
                Assert.AreEqual(types.Single(), ((IStructureDefinitionReference)child.Type.Single()).ReferredType);
            }
            else
            {
                CollectionAssert.AreEqual(types, child.Type
                    .Cast<IStructureDefinitionReference>()
                    .Select(t => t.ReferredType).ToArray());
            }
        }

        private static IStructureDefinitionSummary checkBBType(IStructureDefinitionSummary parent, string ename, string bbType, bool mayRepeat)
        {
            var child = parent.GetElements().SingleOrDefault(c => c.ElementName == ename);

            Assert.IsNotNull(child);
            Assert.AreEqual(mayRepeat, child.IsCollection);
            var result = child.Type.Single() as IStructureDefinitionSummary;
            Assert.AreEqual(bbType, result.TypeName);
            Assert.IsNotNull(result);

            return result;
        }


        public static void TestSpecialTypes(IStructureDefinitionSummaryProvider provider)
        {
            //string FP_STRING = "http://hl7.org/fhirpath/System.String";
            //string FP_DATETIME = "http://hl7.org/fhirpath/System.DateTime";

            // Element.id
            var elem = provider.Provide("Element");
            checkType(elem, "id", false, "string");

            // xhtml.id
            var xhtml = provider.Provide("xhtml");
            checkType(xhtml, "id", false, "string");

            // Narrative.div
            var div = provider.Provide("Narrative");
            checkType(div, "div", false, "xhtml");
            checkType(div, "id", false, "string");  // also try `id` in a derived element

            // Extension.url
            var ext = provider.Provide("Extension");
            checkType(ext, "url", false, "uri");

            // Resource.id
            var res = provider.Provide("Resource");
            checkType(res, "id", false, "id");

            // Patient.id - derived type
            res = provider.Provide("Patient");
            checkType(res, "id", false, "id");
        }

        public static void TestProvidedOrder(IStructureDefinitionSummaryProvider provider)
        {
            hasCorrectOrder("Patient");
            hasCorrectOrder("DomainResource");
            hasCorrectOrder("HumanName");
            hasCorrectOrder("Element");
            hasCorrectOrder("string");
            hasCorrectOrder("Distance");
            hasCorrectOrder("xhtml");

            void hasCorrectOrder(string typename)
            {
                var si = provider.Provide(typename);
                var children = si.GetElements();
                var max = children.Aggregate(0, (a, i) =>
                    i.Order > a ? i.Order : fail($"Order of {i.ElementName} is out of order"));

                int fail(string message)
                {
                    Assert.Fail(message);
                    return 0;  // will never be reached
                }
            }

        }

        public static void TestXmlRepresetation(IStructureDefinitionSummaryProvider provider)
        {
            var testElements = new XmlRepresentation[] { XmlRepresentation.XmlAttr, XmlRepresentation.TypeAttr, XmlRepresentation.XHtml }; // Elements defined by TestXmlRepresentation (Logical Model)
            var summary = provider.Provide("http://hl7.org/fhir/StructureDefinition/TestXmlRepresentation");
            var summaryElements = summary.GetElements();

            foreach (var element in testElements)
            {
                var representation = summaryElements.Where(e => e.ElementName == element.ToString()).FirstOrDefault().Representation;
                if (!representation.Equals(element))
                {
                    Assert.Fail("Representation is expected to be the same as the name of the test element");
                }
            }
        }
    }
}
