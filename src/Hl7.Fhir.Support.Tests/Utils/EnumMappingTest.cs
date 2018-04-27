/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Diagnostics;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Support.Tests.Utils
{
    [TestClass]
    public class EnumMappingTest
    {
        [TestMethod]
        public void TestCreation()
        {
            Assert.AreEqual("Testee", EnumUtility.GetName<TestEnum>());
            Assert.AreEqual(TestEnum.Item1, EnumUtility.ParseLiteral<TestEnum>("Item1"));
            Assert.IsNull(EnumUtility.ParseLiteral<TestEnum>("Item2"));
            Assert.AreEqual(TestEnum.Item2, EnumUtility.ParseLiteral<TestEnum>("ItemTwo"));
            Assert.IsNull(EnumUtility.ParseLiteral<TestEnum>("iTeM1"));
            Assert.AreEqual(TestEnum.Item1, EnumUtility.ParseLiteral<TestEnum>("iTeM1", ignoreCase: true));

            Assert.AreEqual("Item1", TestEnum.Item1.GetLiteral());
            Assert.AreEqual("ItemTwo", TestEnum.Item2.GetLiteral());
            Assert.AreEqual("This is item two", TestEnum.Item2.GetDocumentation());
            Assert.AreEqual("http://example.org/test-system", TestEnum.Item2.GetSystem());
            Assert.AreEqual("ItemThree", TestEnum.Item3.GetLiteral());
            Assert.AreEqual("Item3", TestEnum.Item3.GetDocumentation());
            Assert.IsNull(TestEnum.Item3.GetSystem());
        }

        [FhirEnumeration("Testee")]
        enum TestEnum
        {
            Item1 = 4,

            [EnumLiteral("ItemTwo", "http://example.org/test-system"), Utility.Description("This is item two")]
            Item2,

            [EnumLiteral("ItemThree")]
            Item3
        }



        [TestMethod]
        public void GetInfoFromEnumMember()
        {
            var t = FHIRDefinedType.Markdown;

            Assert.AreEqual("markdown", t.GetLiteral());
            Assert.AreEqual("markdown", t.GetDocumentation());
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
            Assert.AreEqual("a", X.a.GetDocumentation()); // default documentation = name of item
        }


        enum X
        {
            a,
            b
        }
    }
}
