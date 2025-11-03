/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class ModelInspectorMembersTest
    {
        [TestMethod]
        public void TestPrimitiveDataTypeMapping()
        {
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Base64Binary), out var mapping));
            Assert.AreEqual("base64Binary", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count); // id, extension, fhir_comments & value
            var valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.AreEqual("value", valueProp.Name);
            Assert.IsFalse(valueProp.IsCollection);     // don't see byte[] as a collection of byte in FHIR
            Assert.IsTrue(valueProp.RepresentsValueElement);

            Assert.IsTrue(ClassMapping.TryCreate(typeof(Code<SomeEnum>), out mapping));
            Assert.AreEqual("codeOfT<Hl7.Fhir.Tests.Introspection.SomeEnum>", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count); // id, extension, fhir_comments & value
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection);
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(SomeEnum), valueProp.ImplementingType);

            Assert.IsTrue(ClassMapping.TryCreate(typeof(FhirUri), out mapping));
            Assert.AreEqual("uri", mapping.Name);
            Assert.IsTrue(mapping.HasPrimitiveValueMember);
            Assert.AreEqual(3, mapping.PropertyMappings.Count); // id, extension, fhir_comments & value
            valueProp = mapping.PrimitiveValueProperty;
            Assert.IsNotNull(valueProp);
            Assert.IsFalse(valueProp.IsCollection);
            Assert.IsTrue(valueProp.RepresentsValueElement);
            Assert.AreEqual(typeof(string), valueProp.ImplementingType);
        }

        [TestMethod]
        public void TestVersionSpecificMapping()
        {
            Assert.IsTrue(ClassMapping.TryCreate(typeof(Meta), out var mapping, Specification.FhirRelease.DSTU1));
            Assert.IsNull(mapping.FindMappedElementByName("source"));
            var profile = mapping.FindMappedElementByName("profile");
            Assert.IsNotNull(profile);
            Assert.AreEqual(typeof(FhirUri), profile.PropertyTypeMapping.NativeType);

            Assert.IsTrue(ClassMapping.TryCreate(typeof(Meta), out mapping, Specification.FhirRelease.R4));
            Assert.IsNotNull(mapping.FindMappedElementByName("source"));
            profile = mapping.FindMappedElementByName("profile");
            Assert.IsNotNull(profile);
            Assert.AreEqual(typeof(Canonical), profile.PropertyTypeMapping.NativeType);
        }

        [TestMethod]
        public void TestGetByName()
        {
            ClassMapping.TryCreate(typeof(Extension), out var cm).Should().BeTrue();
            cm.FindMappedElementByName("url").Should().NotBeNull();
            cm.FindMappedElementByName("urlx").Should().BeNull();
            cm.FindMappedElementByName("ur").Should().BeNull();
            cm.FindMappedElementByName("value").Should().NotBeNull();

            cm.FindMappedElementByChoiceName("value").Should().NotBeNull();
            cm.FindMappedElementByChoiceName("valu").Should().BeNull();
            cm.FindMappedElementByChoiceName("valueString").Should().NotBeNull();

            var urlChild = cm.FindMappedElementByName("url");
            urlChild.DeclaringClass.Should().Be(cm);
        }

        [TestMethod]
        public void TestPropsWithRedirect()
        {
            Assert.IsTrue(ClassMapping.TryCreate(typeof(TypeWithCodeOfT), out var mapping));

            var propMapping = mapping.FindMappedElementByName("type1");
            Assert.AreEqual(typeof(Code<SomeEnum>), propMapping.ImplementingType);
            Assert.AreEqual(typeof(FhirString), propMapping.PropertyTypeMapping.NativeType);
        }


        [TestMethod]
        public void GetClassMappingForProperty()
        {
            // Canonical.Value - a system primitive
            ClassMapping.TryCreate(typeof(Canonical), out var canonicalMapping);
            canonicalMapping.FindMappedElementByName("value").PropertyTypeMapping.Name.Should().Be("System.String");

            // ConcactPoint.System - a Code<T> -> Code
            ClassMapping.TryCreate(typeof(ContactPoint), out var contactPointMapping);
            contactPointMapping.FindMappedElementByName("system").PropertyTypeMapping.Name.Should().Be("code");

            // ContactPoint.Value - a FhirString
            contactPointMapping.FindMappedElementByName("value").PropertyTypeMapping.Name.Should().Be("string");

            // Extension.Url - a system primitive
            ClassMapping.TryCreate(typeof(Extension), out var extensionMapping);
            extensionMapping.FindMappedElementByName("url").PropertyTypeMapping.Name.Should().Be("System.String");

            // Extension.Value - an abstract DataType
            extensionMapping.FindMappedElementByName("value").PropertyTypeMapping.Name.Should().Be("DataType");

            // Parameters.Parameter - a backbone
            ClassMapping.TryCreate(typeof(Parameters), out var parametersMapping);
            var parameterComponentMapping = parametersMapping.FindMappedElementByName("parameter").PropertyTypeMapping;

            parameterComponentMapping.Name.Should().Be("Parameters#Parameter");
            parameterComponentMapping.FindMappedElementByName("resource").PropertyTypeMapping.Name.Should().Be("Resource");
        }

        [FhirType("TypeWithCodeOfT")]
        public class TypeWithCodeOfT
        {
            [FhirElement("type1")]
            [DeclaredType(Type = typeof(FhirString))]
            public Code<SomeEnum> Type1 { get; set; }
        }

        [FhirType("BindableClass")]
        [Bindable(true)]
        public class BindableClass
        {
        }

        [FhirType("NonBindableClass")]
        [Bindable(false)]
        public class NonBindableClass
        {
        }

        [TestMethod]
        public void IsBindableTest()
        {
            ClassMapping.TryCreate(typeof(BindableClass), out var mapping);
            mapping.Should().NotBeNull();
            mapping.IsBindable.Should().BeTrue();

            ClassMapping.TryCreate(typeof(NonBindableClass), out mapping);
            mapping.Should().NotBeNull();
            mapping.IsBindable.Should().BeFalse();
        }

        [TestMethod]
        public void TestPerformanceOfMapping()
        {
            // just a random list of POCO types available in common
            var typesToTest = new Type[] { typeof(BackboneElement), typeof(BackboneType),
                typeof(Base64Binary), typeof(Canonical), typeof(Element), typeof(FhirString),
                typeof(Extension), typeof(Resource), typeof(Meta), typeof(XHtml) };


            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
                foreach (var testee in typesToTest)
                    createMapping(testee);
            sw.Stop();
            Console.WriteLine($"No props: {sw.ElapsedMilliseconds}");

            sw.Restart();
            for (int i = 0; i < 1000; i++)
                foreach (var testee in typesToTest)
                    createMapping(testee, touchProps: true);
            sw.Stop();
            Console.WriteLine($"With props: {sw.ElapsedMilliseconds}");

            int createMapping(Type t, bool touchProps = false)
            {
                ClassMapping.TryCreate(t, out var mapping);
                return touchProps ? mapping.PropertyMappings.Count : -1;
            }
        }
    }
}
