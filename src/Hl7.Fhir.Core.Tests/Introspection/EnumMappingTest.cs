/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System.Diagnostics;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
#if PORTABLE45
	public class PortableEnumMappingTest
#else
	public class EnumMappingTest
#endif
    {
        [TestMethod]
        public void TestCreation()
        {
            EnumUtility.EnumMapping mapping = EnumUtility.EnumMapping.Create(typeof(TestEnum));

            Assert.AreEqual("Testee", mapping.Name);
            Assert.AreEqual(typeof(TestEnum), mapping.EnumType);
            Assert.IsTrue(mapping.ContainsLiteral("Item1"));
            Assert.IsFalse(mapping.ContainsLiteral("Item2"));
            Assert.IsFalse(mapping.ContainsLiteral("iTeM1"));
            Assert.IsTrue(mapping.ContainsLiteral("ItemTwo"));

            Assert.AreEqual(TestEnum.Item2, mapping.ParseLiteral("ItemTwo"));
            Assert.AreEqual(TestEnum.Item1, mapping.ParseLiteral("Item1"));
            Assert.AreEqual("ItemTwo", mapping.GetLiteral(TestEnum.Item2));
            Assert.AreEqual("Item1", mapping.GetLiteral(TestEnum.Item1));
        }



        [TestMethod]
        public void GetInfoFromEnumMember()
        {
            var t = FHIRDefinedType.Markdown;

            Assert.AreEqual("markdown", t.GetLiteral());
            Assert.AreEqual("markdown", t.GetDocumentation());
        }

        [TestMethod]
        public void ParseFhirTypeName()
        {
            Assert.AreEqual(FHIRDefinedType.Markdown, ModelInfo.FhirTypeNameToFhirType("markdown"));
            Assert.IsNull(ModelInfo.FhirTypeNameToFhirType("Markdown"));
            Assert.AreEqual(FHIRDefinedType.Organization, ModelInfo.FhirTypeNameToFhirType("Organization"));
        }


        [TestMethod]
        public void EnumParsingPerformance()
        {
            var sw = new Stopwatch();
            sw.Start();

            for(var i=0; i < 10000; i++)
                EnumUtility.ParseLiteral<AdministrativeGender>("male");

            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.IsTrue(sw.ElapsedMilliseconds < 100);
        }

        [TestMethod]
        public void TestEnumMapping()
        {
            Assert.AreEqual(AdministrativeGender.Male, EnumUtility.ParseLiteral<AdministrativeGender>("male"));
            Assert.IsNull(EnumUtility.ParseLiteral<AdministrativeGender>("maleX"));
            Assert.AreEqual(X.a, EnumUtility.ParseLiteral<X>("a"));

            Assert.AreEqual("Male",AdministrativeGender.Male.GetDocumentation());
            Assert.IsNull(X.a.GetDocumentation());
        }


        enum X
        {
            a,
            b
        }

        [FhirEnumeration("Testee")]
        enum TestEnum
        {
            Item1 = 4,

            [EnumLiteral("ItemTwo")]
            Item2
        }
    }
}
