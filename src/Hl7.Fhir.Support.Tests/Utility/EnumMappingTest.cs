/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

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

        [TestMethod]
        public void TestCreationNonGeneric()
        {
            Assert.AreEqual("Testee", EnumUtility.GetName(typeof(TestEnum)));
            Assert.AreEqual(TestEnum.Item1, EnumUtility.ParseLiteral("Item1", typeof(TestEnum)));
            Assert.IsNull(EnumUtility.ParseLiteral("Item2", typeof(TestEnum)));
            Assert.AreEqual(TestEnum.Item2, EnumUtility.ParseLiteral("ItemTwo", typeof(TestEnum)));
            Assert.IsNull(EnumUtility.ParseLiteral("iTeM1", typeof(TestEnum)));
            Assert.AreEqual(TestEnum.Item1, EnumUtility.ParseLiteral("iTeM1", typeof(TestEnum), ignoreCase: true));

            Assert.AreEqual("Item1", ((Enum)TestEnum.Item1).GetLiteral());
            Assert.AreEqual("ItemTwo", ((Enum)TestEnum.Item2).GetLiteral());
            Assert.AreEqual("This is item two", TestEnum.Item2.GetDocumentation());
            Assert.AreEqual("http://example.org/test-system", ((Enum)TestEnum.Item2).GetSystem());
            Assert.AreEqual("ItemThree", ((Enum)TestEnum.Item3).GetLiteral());
            Assert.AreEqual("Item3", ((Enum)TestEnum.Item3).GetDocumentation());
            Assert.IsNull(((Enum)TestEnum.Item3).GetSystem());
        }

        [FhirEnumeration("Testee")]
        enum TestEnum
        {
            Item1 = 4,

            [EnumLiteral("ItemTwo", "http://example.org/test-system"), Fhir.Utility.Description("This is item two")]
            Item2,

            [EnumLiteral("ItemThree")]
            Item3
        }



        [TestMethod]
        public void GetInfoFromEnumMember()
        {
            var t = TestAdministrativeGender.Male;

            Assert.AreEqual("male", t.GetLiteral());
            Assert.AreEqual("Male", t.GetDocumentation());
        }


        /// <summary>
        /// The gender of a person used for administrative purposes.
        /// (url: http://hl7.org/fhir/ValueSet/administrative-gender)
        /// </summary>
        [FhirEnumeration("TestAdministrativeGender")]
        public enum TestAdministrativeGender
        {
            /// <summary>
            /// Male<br/>
            /// (system: http://hl7.org/fhir/administrative-gender)
            /// </summary>
            [EnumLiteral("male", "http://hl7.org/fhir/administrative-gender"), Fhir.Utility.Description("Male")]
            Male,
            /// <summary>
            /// Female<br/>
            /// (system: http://hl7.org/fhir/administrative-gender)
            /// </summary>
            [EnumLiteral("female", "http://hl7.org/fhir/administrative-gender"), Fhir.Utility.Description("Female")]
            Female,
            /// <summary>
            /// Other<br/>
            /// (system: http://hl7.org/fhir/administrative-gender)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/administrative-gender"), Fhir.Utility.Description("Other")]
            Other,
            /// <summary>
            /// Unknown<br/>
            /// (system: http://hl7.org/fhir/administrative-gender)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/administrative-gender"), Fhir.Utility.Description("Unknown")]
            Unknown,
        }

        [TestMethod]
        public void EnumParsingPerformance()
        {
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 10000; i++)
                EnumUtility.ParseLiteral<TestAdministrativeGender>("male");

            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.IsLessThan(100, sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void EnumParsingPerformanceNonGeneric()
        {
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 10000; i++)
                EnumUtility.ParseLiteral("male", typeof(TestAdministrativeGender));

            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.IsLessThan(100, sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void TestEnumMapping()
        {
            Assert.AreEqual(TestAdministrativeGender.Male, EnumUtility.ParseLiteral<TestAdministrativeGender>("male"));
            Assert.IsNull(EnumUtility.ParseLiteral<TestAdministrativeGender>("maleX"));
            Assert.AreEqual(X.a, EnumUtility.ParseLiteral<X>("a"));

            Assert.AreEqual("Male", TestAdministrativeGender.Male.GetDocumentation());
            Assert.AreEqual("a", X.a.GetDocumentation()); // default documentation = name of item
        }

        [TestMethod]
        public void TestEnumMappingNonGeneric()
        {
            Assert.AreEqual(TestAdministrativeGender.Male, EnumUtility.ParseLiteral("male", typeof(TestAdministrativeGender)));
            Assert.IsNull(EnumUtility.ParseLiteral("maleX", typeof(TestAdministrativeGender)));
            Assert.AreEqual(X.a, EnumUtility.ParseLiteral("a", typeof(X)));

            Assert.AreEqual("Male", TestAdministrativeGender.Male.GetDocumentation());
            Assert.AreEqual("a", X.a.GetDocumentation()); // default documentation = name of item
        }

        [TestMethod]
        public void EnumLiteralPerformance()
        {
            var result = string.Empty;
            var value = TestAdministrativeGender.Male;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 50_000; i++)
                result = value.GetLiteral();
            sw.Stop();

            Assert.AreEqual("male", result);
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.IsLessThan(500, sw.ElapsedMilliseconds);
        }

        [TestMethod]
        public void EnumLiteralPerformanceNonGeneric()
        {
            var result = string.Empty;
            Enum value = TestAdministrativeGender.Male;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 50_000; i++)
                result = value.GetLiteral();
            sw.Stop();

            Assert.AreEqual("male", result);
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.IsLessThan(500, sw.ElapsedMilliseconds);
        }

        enum X
        {
            a,
            b
        }

        [TestMethod]
        public void NullLiteralHandling()
        {
            EnumUtility.ParseLiteral<TestAdministrativeGender>(null).Should().BeNull();
            EnumUtility.ParseLiteral<TestAdministrativeGender>(null, ignoreCase: true).Should().BeNull();
        }
    }
}
