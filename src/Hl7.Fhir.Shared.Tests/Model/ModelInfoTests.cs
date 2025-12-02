/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class ModelInfoTests
    {
        [TestMethod]
        public void TestModelInfoTypeSelectors()
        {
            Assert.IsTrue(ModelInfo.IsKnownResource("Patient"));
            Assert.IsFalse(ModelInfo.IsKnownResource("Identifier"));
            Assert.IsFalse(ModelInfo.IsKnownResource("code"));

            Assert.IsFalse(ModelInfo.IsDataType("Patient"));
            Assert.IsTrue(ModelInfo.IsDataType("Identifier"));
            Assert.IsFalse(ModelInfo.IsDataType("code"));

            Assert.IsFalse(ModelInfo.IsPrimitive("Patient"));
            Assert.IsFalse(ModelInfo.IsPrimitive("Identifier"));
            Assert.IsTrue(ModelInfo.IsPrimitive("code"));

            Assert.IsTrue(ModelInfo.IsReference("Reference"));
            Assert.IsFalse(ModelInfo.IsReference("Patient"));
        }

        [TestMethod]
        public void TestFhirTypeToFhirTypeName()
        {
            var enumValues = Enum.GetValues(typeof(FHIRAllTypes));
            for (int i = 0; i < enumValues.Length; i++)
            {
                var type = (FHIRAllTypes)i;
                var typeName = ModelInfo.FhirTypeToFhirTypeName(type);
                var type2 = ModelInfo.FhirTypeNameToFhirType(typeName);
                Assert.IsTrue(type2.HasValue);
                Assert.AreEqual(type, type2, String.Format("Failed: '{0}' != '{1}' ?!", type, type2));
                var typeName2 = ModelInfo.FhirTypeToFhirTypeName(type2.Value);
                Assert.AreEqual(typeName, typeName2, String.Format("Failed: '{0}' != '{1}' ?!", typeName, typeName2));
            }
        }
     
        [TestMethod]
        public void TestSubclassInfo()
        {
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Resource, FHIRAllTypes.Patient));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.DomainResource, FHIRAllTypes.Patient));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Patient, FHIRAllTypes.Patient));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Observation, FHIRAllTypes.Patient));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Element, FHIRAllTypes.Patient));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Resource, FHIRAllTypes.Bundle));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.DomainResource, FHIRAllTypes.Bundle));

            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Element, FHIRAllTypes.HumanName));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Element, FHIRAllTypes.Patient));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Element, FHIRAllTypes.Oid));

            // FHIR: Canonical derives from Uri (not from String), but as gForge cofirmed Url and Canonical cannot be used as substitutes for Uri
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Uri, FHIRAllTypes.Canonical));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Uri, FHIRAllTypes.Url));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.String, FHIRAllTypes.Canonical));

            // FHIR: Money derives from Element, not Quantity.
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(FHIRAllTypes.Quantity, FHIRAllTypes.Money));
        }

        [TestMethod]
        public void TestSubclassInfoByType()
        {
            testTrue(typeof(Resource), typeof(Patient));
            testTrue(typeof(DomainResource), typeof(Patient));
            testTrue(typeof(Patient), typeof(Patient));
            testFalse(typeof(Observation), typeof(Patient));
            testFalse(typeof(Element), typeof(Patient));
            testTrue(typeof(Resource), typeof(Bundle));
            testFalse(typeof(DomainResource), typeof(Bundle));

            testTrue(typeof(Element), typeof(HumanName));
            testFalse(typeof(Element), typeof(Patient));
            testTrue(typeof(Element), typeof(Oid));
            testFalse(typeof(FhirString), typeof(Markdown));
            testFalse(typeof(Integer), typeof(UnsignedInt));

            static void testTrue(Type super, Type sub) =>
                Assert.IsTrue(ModelInfo.IsInstanceTypeFor(super, sub));

            static void testFalse(Type super, Type sub) =>
                Assert.IsFalse(ModelInfo.IsInstanceTypeFor(super, sub));
        }

        [TestMethod]
        public void ParseFhirTypeName()
        {
            Assert.AreEqual(FHIRAllTypes.Markdown, ModelInfo.FhirTypeNameToFhirType("markdown"));
            Assert.IsNull(ModelInfo.FhirTypeNameToFhirType("Markdown"));
            Assert.AreEqual(FHIRAllTypes.Organization, ModelInfo.FhirTypeNameToFhirType("Organization"));
        }

        // [WMR 20181025] Issue #746
        [TestMethod]
        public void TestIsCoreModelTypeUri()
        {
            Assert.IsTrue(ModelInfo.IsCoreModelTypeUri(new Uri("http://hl7.org/fhir/StructureDefinition/Patient")));
            Assert.IsTrue(ModelInfo.IsCoreModelTypeUri(new Uri("http://hl7.org/fhir/StructureDefinition/string")));

            Assert.IsFalse(ModelInfo.IsCoreModelTypeUri(new Uri("http://example.org/fhir/StructureDefinition/Patient")));
            Assert.IsFalse(ModelInfo.IsCoreModelTypeUri(new Uri("/StructureDefinition/Patient", UriKind.Relative)));
            Assert.IsFalse(ModelInfo.IsCoreModelTypeUri(new Uri("Patient", UriKind.Relative)));
        }


        [TestMethod]
        public void TestNonGeneratedHierarchy()
        {
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(typeof(Quantity), typeof(Age)));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(typeof(DataType), typeof(Age)));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(typeof(Integer), typeof(UnsignedInt)));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(typeof(PrimitiveType), typeof(UnsignedInt)));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(typeof(FhirString), typeof(Code)));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(typeof(PrimitiveType), typeof(Code)));
            Assert.IsFalse(ModelInfo.IsInstanceTypeFor(typeof(FhirUri), typeof(Uuid)));
            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(typeof(PrimitiveType), typeof(Uuid)));
        }

        // [WMR 20190413] NEW

        IEnumerable<Type> FhirCsTypes => ModelInfo.FhirCsTypeToString.Keys;

        [TestMethod]
        public void TestIsConformanceResource()
        {
            // Verify that ModelInfo.IsConformanceResource overloads returns true for all types that implement IConformanceResource
            foreach (var type in FhirCsTypes)
            {
                //var supportsInterface = typeof(IConformanceResource).IsAssignableFrom(type);
                var supportsInterface = type.CanBeTreatedAsType(typeof(IConformanceResource));
                Assert.AreEqual(supportsInterface, ModelInfo.IsConformanceResource(type));
                var typeName = ModelInfo.GetFhirTypeNameForType(type);
                Assert.AreEqual(supportsInterface, ModelInfo.IsConformanceResource(typeName));
                var typeFlag = ModelInfo.FhirTypeNameToFhirType(typeName);
                Assert.AreEqual(supportsInterface, ModelInfo.IsConformanceResource(typeFlag));
            }
        }

        [TestMethod]
        public void TestIsPrimitive()
        {
            // Verify that ModelInfo.IsPrimitive overloads returns true for all types derived from Primitive
            foreach (var type in FhirCsTypes)
            {
                var isPrimitive = type.CanBeTreatedAsType(typeof(PrimitiveType));
                var typeName = ModelInfo.GetFhirTypeNameForType(type);
                Assert.IsNotNull(typeName);
                Assert.AreEqual(isPrimitive, ModelInfo.IsPrimitive(type));
                Assert.AreEqual(isPrimitive, ModelInfo.IsPrimitive(typeName));
            }
        }

        [TestMethod]
        public void TestIsDataType()
        {
            // Verify that ModelInfo.IsDataType returns true for all types derived from Element but not from Primitive
            foreach (var type in FhirCsTypes)
            {
                if (type == typeof(Base)) continue;

                var isDataType = type.CanBeTreatedAsType(typeof(Element)) && !type.CanBeTreatedAsType(typeof(PrimitiveType)); var typeName = ModelInfo.GetFhirTypeNameForType(type);
                Assert.IsNotNull(typeName);
                Assert.AreEqual(isDataType, ModelInfo.IsDataType(type), type.Name);
                Assert.AreEqual(isDataType, ModelInfo.IsDataType(typeName));
            }
        }

        [TestMethod]
        public void TestIsReference()
        {
            // Verify that ModelInfo.IsReference overloads returns true for type (Resource)Reference
            foreach (var type in FhirCsTypes)
            {
                var isReference = type == typeof(ResourceReference);
                var typeName = ModelInfo.GetFhirTypeNameForType(type);
                Assert.IsNotNull(typeName);
                Assert.AreEqual(isReference, ModelInfo.IsReference(type));
                Assert.AreEqual(isReference, ModelInfo.IsReference(typeName));
            }
        }

        [TestMethod]
        public void TestTypeHierarchy()
        {
            // Verify ModelInfo methods are in agreement with the actual type hierarchy
            // - ModelInfo.IsInstanceTypeFor
            // - ModelInfo.IsCoreSuperType
            var types = ModelInfo.FhirCsTypeToString.Keys;
            foreach (var type in types)
            {
                Assert.IsTrue(ModelInfo.IsCoreModelType(type));
                var typeName = ModelInfo.GetFhirTypeNameForType(type);
                Assert.IsNotNull(typeName);
                Assert.IsTrue(ModelInfo.IsCoreModelType(typeName));

                if (!ModelInfo.IsCoreSuperType(type))
                {
                    var baseType = type.BaseType;
                    while (!ModelInfo.IsCoreSuperType(baseType))
                    {
                        // Skip intermediate abstract types, e.g. Primitive<T>
                        if (ModelInfo.IsCoreModelType(baseType))
                        {
                            var baseTypeName = ModelInfo.GetFhirTypeNameForType(baseType);
                            Assert.IsNotNull(baseTypeName);

                            Assert.IsTrue(ModelInfo.IsInstanceTypeFor(baseTypeName, typeName));
                        }
                        baseType = baseType.BaseType;
                    }
                }
            }
        }

        [TestMethod]
        public void TestCheckMinorVersionCompatibilityWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => ModelInfo.CheckMinorVersionCompatibility(null));
        }

        [TestMethod]
        public void TestHumanNameFluentInitializers()
        {
            var pat = new Patient();
            pat.Name.Add(new HumanName().WithPrefix("Mr.").WithGiven("Henry").AndFamily("Levin").WithSuffix("The 7th"));

            pat.Name.FirstOrDefault().Prefix.Should().ContainSingle("Mr.");
            pat.Name.FirstOrDefault().Given.Should().ContainSingle("Henry");
            pat.Name.FirstOrDefault().Family.Should().Be("Levin");
            pat.Name.FirstOrDefault().Suffix.Should().ContainSingle("The 7th");
        }
    }
}
