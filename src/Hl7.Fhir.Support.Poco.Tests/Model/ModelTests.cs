/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void ValidateElementAssertions()
        {
            XElement xr = new XElement("root",
                        new XElement("child", "value"),
                        new XElement("child", "value2"));

            Assert.IsNull(xr.Element("childx"));
            Assert.AreEqual(0, xr.Elements("childx").Count());
            Assert.AreEqual("value", xr.Element("child").Value);
        }

        [TestMethod]
        public void OperationOutcomeExpression()
        {
            OperationOutcome oo = new OperationOutcome();
            oo.Issue.Add(new OperationOutcome.IssueComponent()
            {
                Expression = new string[] { "this" }
            });
            Assert.AreEqual(1, oo.Issue[0].Expression.Count());
        }

        [TestMethod]
        public void DateTimeHandling()
        {
            FhirDateTime dt = new FhirDateTime("2010-01-01");
            Assert.AreEqual("2010-01-01", dt.Value);

            FhirDateTime dt2 = new FhirDateTime(1972, 11, 30, 15, 10, 0, TimeSpan.Zero);
            Assert.IsTrue(dt2.Value.StartsWith("1972-11-30T15:10"));
            Assert.AreNotEqual(dt2.Value, "1972-11-30T15:10");


            FhirDateTime dtNoMs = new FhirDateTime("2014-12-11T00:00:00+11:00");
            Assert.AreEqual("2014-12-11T00:00:00+11:00", dtNoMs.Value);

            FhirDateTime dtWithMs = new FhirDateTime("2014-12-11T00:00:00.000+11:00");
            Assert.AreEqual("2014-12-11T00:00:00.000+11:00", dtWithMs.Value);

            var stamp = new DateTimeOffset(1972, 11, 30, 15, 10, 0, TimeSpan.Zero);
            dt = new FhirDateTime(stamp);
            Assert.IsTrue(dt.Value.EndsWith("+00:00"));
        }

        //Added for issue #1498
        [TestMethod]
        public void TestTryToDateTimeOffset()
        {
            var fdt = new FhirDateTime(new DateTimeOffset(2021, 3, 18, 12, 22, 35, 999, new TimeSpan(-4, 0, 0)));
            Assert.AreEqual("2021-03-18T12:22:35.999-04:00", fdt.Value);

            Assert.IsTrue(fdt.TryToDateTimeOffset(out var dto1));
            Assert.AreEqual("2021-03-18T12:22:35.9990000-04:00", dto1.ToString("o"));

            fdt = new FhirDateTime(new DateTimeOffset(2021, 3, 18, 12, 22, 35, 999, new TimeSpan(4, 0, 0)));
            Assert.AreEqual("2021-03-18T12:22:35.999+04:00", fdt.Value);

            Assert.IsTrue(fdt.TryToDateTimeOffset(out var dto2));
            Assert.AreEqual("2021-03-18T12:22:35.9990000+04:00", dto2.ToString("o"));

            fdt = new FhirDateTime("2021-03-18T12:22:35.999Z");
            Assert.IsTrue(fdt.TryToDateTimeOffset(out var dto3));
            Assert.AreEqual("2021-03-18T12:22:35.9990000+00:00", dto3.ToString("o"));

            fdt = new FhirDateTime("2021-03-18T12:22:35.1234+04:00");
            Assert.IsTrue(fdt.TryToDateTimeOffset(out var dto4));
            Assert.AreEqual("2021-03-18T12:22:35.1234000+04:00", dto4.ToString("o"));

            fdt = new FhirDateTime("2021-03-18T12:22:35.999");
            Assert.AreEqual("2021-03-18T12:22:35.999", fdt.Value);
            Assert.IsFalse(fdt.TryToDateTimeOffset(out var dto5));
        }

        [TestMethod]
        public void TodayTests()
        {
            var todayLocal = Date.Today();
            Assert.AreEqual(DateTimeOffset.Now.ToString("yyy-MM-dd"), todayLocal.Value);

            var todayUtc = Date.UtcToday();
            Assert.AreEqual(DateTimeOffset.UtcNow.ToString("yyy-MM-dd"), todayUtc.Value);
        }

        [TestMethod]
        public void TestInstantFromUtc()
        {
            Instant ins5 = Instant.FromDateTimeUtc(2011, 3, 4, 16, 45, 33);
            Assert.AreEqual(new DateTimeOffset(2011, 3, 4, 16, 45, 33, TimeSpan.Zero), ins5.Value);
        }


        [TestMethod]
        public void Uri_Canonical()
        {
            var identifier = new Identifier("http://nhi.health.nz", "123");
            Assert.AreEqual("123", identifier.Value);
            Assert.AreEqual("http://nhi.health.nz", identifier.System);
        }


        [TestMethod]
        public void TestBundleLinkEncoding()
        {
            Action<string> test = (urlFormat) =>
            {
                var param1 = "baz/123";
                var param2 = "qux:456";
                var manuallyEncodedUrl = string.Format(urlFormat, "baz%2F123", "qux%3A456");
                var uriEncodedUrl = string.Format(urlFormat, Uri.EscapeDataString(param1), Uri.EscapeDataString(param2));
                Assert.AreEqual(manuallyEncodedUrl, uriEncodedUrl);
                var uri = new Uri(manuallyEncodedUrl, UriKind.RelativeOrAbsolute);
                var bundle = new Bundle { SelfLink = uri };
                if (uri.IsAbsoluteUri)
                {
                    Assert.AreEqual(uri.AbsoluteUri, bundle.SelfLink.AbsoluteUri);
                }
                else
                {
                    Assert.AreEqual(uri.OriginalString, bundle.SelfLink.OriginalString);
                }
            };

            test("http://foo/bar?param1={0}&param2={1}");
            test("http://foo/bar/../bar?param1={0}&param2={1}");
            test("bar?param1={0}&param2={1}");
            test("bar/../bar?param1={0}&param2={1}");
        }

        [TestMethod]
        public void SimpleValueSupport()
        {
            Bundle b = new();

            Assert.IsNull(b.Total);
            b.Total = 10;
            Assert.AreEqual(10, b.Total.GetValueOrDefault());
            Assert.IsNotNull(b.Total);

            OperationOutcome.IssueComponent ic = new();

            ic.DiagnosticsElement = new FhirString("Furore");
            Assert.AreEqual("Furore", ic.Diagnostics);
            ic.Diagnostics = null;
            Assert.IsNull(ic.DiagnosticsElement);
            ic.Diagnostics = "Furore";
            Assert.IsNotNull(ic.DiagnosticsElement);

            ic.Expression = new string[] { "json", "xml" };
            Assert.IsNotNull(ic.ExpressionElement);
            Assert.AreEqual(2, ic.ExpressionElement.Count);
            Assert.AreEqual("json", ic.ExpressionElement.First().Value);

            ic.ExpressionElement = new List<FhirString>();
            ic.ExpressionElement.Add(new FhirString("csv"));
            Assert.IsNotNull(ic.Expression);
            Assert.AreEqual(1, ic.Expression.Count());
        }

        [TestMethod]
        public void ExtensionManagement()
        {
            TestPatient p = new();
            var u1 = "http://fhir.org/ext/ext-test";
            Assert.IsNull(p.GetExtension("http://fhir.org/ext/ext-test"));

            Extension newEx = p.SetExtension(u1, new FhirBoolean(true));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.AddExtension("http://fhir.org/ext/ext-test2", new FhirString("Ewout"));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.RemoveExtension(u1);
            Assert.IsNull(p.GetExtension(u1));

            p.SetExtension("http://fhir.org/ext/ext-test2", new FhirString("Ewout Kramer"));
            var ew = p.GetExtensions("http://fhir.org/ext/ext-test2");
            Assert.AreEqual(1, ew.Count());

            p.AddExtension("http://fhir.org/ext/ext-test2", new FhirString("Wouter Kramer"));

            ew = p.GetExtensions("http://fhir.org/ext/ext-test2");
            Assert.AreEqual(2, ew.Count());

            Assert.AreEqual(0, p.ModifierExtension.Count());
            var me = p.AddExtension("http://fhir.org/ext/ext-test3", new FhirString("bla"), isModifier: true);
            Assert.AreEqual(1, p.ModifierExtension.Count());
            Assert.AreEqual(me, p.GetExtension("http://fhir.org/ext/ext-test3"));
            Assert.AreEqual(me, p.GetExtensions("http://fhir.org/ext/ext-test3").Single());
            Assert.AreEqual(3, p.AllExtensions().Count());

            var code = new Code("test");
            p.AddExtension("http://fhir.org/ext/code", code);
            Assert.AreEqual(code, p.GetExtensionValue<Code>("http://fhir.org/ext/code"));

            var text = new FhirString("test");
            p.AddExtension("http://fhir.org/ext/string", text);
            Assert.AreEqual(text, p.GetExtensionValue<FhirString>("http://fhir.org/ext/string"));

            var fhirbool = new FhirBoolean(true);
            p.AddExtension("http://fhir.org/ext/bool", fhirbool);
            Assert.AreEqual(fhirbool, p.GetExtensionValue<FhirBoolean>("http://fhir.org/ext/bool"));

        }


        [TestMethod]
        public void RecognizeContainedReference()
        {
            var rref = new ResourceReference() { Reference = "#patient2223432" };

            Assert.IsTrue(rref.IsContainedReference);

            rref.Reference = "http://somehwere.nl/Patient/1";
            Assert.IsFalse(rref.IsContainedReference);

            rref.Reference = "Patient/1";
            Assert.IsFalse(rref.IsContainedReference);
        }


        [TestMethod]
        public void FindContainedResource()
        {
            var cPat1 = new TestPatient() { Id = "pat1" };
            var cPat2 = new TestPatient() { Id = "pat2" };
            var pat = new TestPatient
            {
                Contained = new List<Resource> { cPat1, cPat2 }
            };

            var rref = new ResourceReference() { Reference = "#pat2" };

            Assert.IsNotNull(pat.FindContainedResource(rref), "#pat2 should be in the contained resources");
            Assert.IsNotNull(pat.FindContainedResource("#pat1"), "#pat1 should be in the contained resources");

            rref.Reference = "#pat3";
            Assert.IsNull(pat.FindContainedResource(rref));

            Assert.AreEqual(pat, pat.FindContainedResource("#"));

            var pat2 = new TestPatient();
            Assert.IsNull(pat2.FindContainedResource("#pat1"));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindContainedResourceExceptionExpected()
        {
            var cPat1 = new TestPatient() { Id = "pat1" };
            var cPat2 = new TestPatient() { Id = "pat2" };
            var pat = new TestPatient
            {
                Contained = new List<Resource> { cPat1, cPat2 }
            };

            pat.FindContainedResource((ResourceReference)null);
        }

        [TestMethod]
        public void TestListDeepCopy()
        {
            var x = new List<TestPatient>();
            x.Add(new TestPatient());
            x.Add(new TestPatient());

            var y = new List<TestPatient>(x.DeepCopy());
            Assert.IsTrue(x[0] is TestPatient);
            Assert.AreNotEqual(x[0], y[0]);
            Assert.AreNotEqual(x[1], y[1]);
        }


        [TestMethod]
        public void TestLazyCreatedLists()
        {
            var p = new TestPatient();
            p.Name.Add(new TestHumanName());
        }


        [TestMethod]
        public void TestStringValueInterface()
        {
            IValue<string> sv = new FhirString("test");
            Assert.IsNotNull(sv);
            sv.Value = "string";
            Assert.AreEqual(sv.Value, "string");

            sv = new FhirUri("test");
            Assert.IsNotNull(sv);
            sv.Value = "http://example.org";
            Assert.AreEqual(sv.Value, "http://example.org");

            sv = new Uuid("test");
            Assert.IsNotNull(sv);
            sv.Value = "550e8400-e29b-41d4-a716-446655440000";
            Assert.AreEqual(sv.Value, "550e8400-e29b-41d4-a716-446655440000");

            sv = new Oid("test");
            Assert.IsNotNull(sv);
            sv.Value = "2.16.840.1.113883";
            Assert.AreEqual(sv.Value, "2.16.840.1.113883");

            sv = new Markdown("test");
            Assert.IsNotNull(sv);
            sv.Value = "Hello World!";
            Assert.AreEqual(sv.Value, "Hello World!");

            sv = new Date();
            Assert.IsNotNull(sv);
            sv.Value = "20161201";
            Assert.AreEqual(sv.Value, "20161201");

            sv = new Time();
            Assert.IsNotNull(sv);
            sv.Value = "23:59:00";
            Assert.AreEqual(sv.Value, "23:59:00");

            sv = new FhirDateTime(DateTimeOffset.UtcNow);
            Assert.IsNotNull(sv);
            sv.Value = "20161201 23:59:00";
            Assert.AreEqual(sv.Value, "20161201 23:59:00");

        }


        [TestMethod]
        public void TestIntegerValueInterface()
        {
            INullableValue<int> iv = new Integer(null);
            Assert.IsNotNull(iv);
            iv.Value = 12345;
            Assert.AreEqual(iv.Value, 12345);

            iv = new UnsignedInt(0);
            Assert.IsNotNull(iv);
            iv.Value = 12345;
            Assert.AreEqual(iv.Value, 12345);

            iv = new PositiveInt(1);
            Assert.IsNotNull(iv);
            iv.Value = 12345;
            Assert.AreEqual(iv.Value, 12345);
        }

        [TestMethod]
        public void TestChildren_EmptyPatient()
        {
            var patient = new TestPatient();
            var children = patient.Children.ToArray();
            Base[] expected = { };
            Assert.IsTrue(expected.SequenceEqual(children));
        }

        [TestMethod]
        public void TestChildren_EmptyHumanName()
        {
            var timing = new TestHumanName();
            var children = timing.Children.ToArray();
            Base[] expected = { };
            Assert.IsTrue(expected.SequenceEqual(children));
        }


        [TestMethod]
        public void ToStringHandlesNullObjectValue()
        {
            var s = new FhirString(null);
            Assert.IsNull(s.ToString());

            var i = new FhirBoolean(null);
            Assert.IsNull(i.ToString());
        }

        [TestMethod]
        public void TestChildren_Patient()
        {
            var patient = new TestPatient()
            {
                Name =
                {
                    new TestHumanName()
                    {
                        Given = new string[] { "John" },
                        Family = "Doe"
                    },
                     new TestHumanName()
                    {
                        Given = new string[] { "Alias" },
                        Family = "Alternate"
                    }
                },
                Address =
                {
                    new TestAddress()
                    {
                        City = "Amsterdam",
                        Line = new string[] { "Rokin" }
                    }
                }
            };
            var children = patient.Children.ToArray();
            Base[] expected =
            {
                // ===== Resource elements =====
                // patient.IdElement, patient.Meta, patient.ImplicitRulesElement, patient.LanguageElement,
                
                // ===== DomainResource elements =====
                // patient.Text,
                // patient.Contained = empty collection
                // patient.Extension = empty collection
                // patient.ModifierExtension = empty collection

                // ===== Patient elements =====
                // patient.Identifier = empty collection
                // patient.ActiveElement,
                patient.Name[0],
                patient.Name[1],
                // patient.Telecom = empty collection
                // patient.GenderElement,
                // patient.BirthDateElement,
                // patient.Deceased,
                patient.Address[0],
                // patient.MaritalStatus,
                // patient.MultipleBirth,
                // patient.Photo = empty collection
                // patient.Contact = empty collection
                // patient.Animal,
                // patient.Communication = empty collection
                // patient.CareProvider = empty collection
                // patient.ManagingOrganization
                // patient.Link = empty collection
            };
            Assert.IsTrue(expected.SequenceEqual(children));

            var name = patient.Name[0];
            children = name.Children.ToArray();
            expected = new Base[]
            {
                // ===== Element elements =====
                // name.Extension = empty collection

                // ===== HumanName elements =====
                // name.UseElement,
                // name.TextElement,
                name.FamilyElement,
                name.GivenElement[0],
                // name.Period
            };
            Assert.IsTrue(expected.SequenceEqual(children));

            var address = patient.Address[0];
            children = address.Children.ToArray();
            expected = new Base[]
            {
                // ===== Element elements =====
                // name.Extension = empty collection

                // ===== Address elements =====
                // address.UseElement,
                // address.TypeElement,
                // address.TextElement,
                address.LineElement[0],
                address.CityElement,
                // address.DistrictElement,
                // address.StateElement,
                // address.PostalCodeElement,
                // address.CountryElement,
                // address.Period
            };
            Assert.IsTrue(expected.SequenceEqual(children));
        }
    }
}
