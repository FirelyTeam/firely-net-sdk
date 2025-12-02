/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Tests.Model;

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
        Assert.AreEqual("value", xr.Element("child")?.Value);
    }

    [TestMethod]
    public void OperationOutcomeExpression()
    {
        var oo = new OperationOutcome();
        oo.Issue.Add(new OperationOutcome.IssueComponent()
        {
            Expression = ["this"]
        });
        Assert.AreEqual(1, oo.Issue[0].Expression.Count());
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
        test("http://foo/bar?param1={0}&param2={1}");
        test("http://foo/bar/../bar?param1={0}&param2={1}");
        test("bar?param1={0}&param2={1}");
        test("bar/../bar?param1={0}&param2={1}");
        return;

        static void test(string urlFormat)
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
        }
    }

    [TestMethod]
    public void SimpleValueSupport()
    {
        Bundle b = new();

        Assert.IsNull(b.Total);
        b.Total = 10;
        Assert.AreEqual(10, b.Total.GetValueOrDefault());
        Assert.IsNotNull(b.Total);

        OperationOutcome.IssueComponent ic = new() { DiagnosticsElement = new FhirString("Furore") };
        Assert.AreEqual("Furore", ic.Diagnostics);
        ic.Diagnostics = null;
        Assert.IsNull(ic.DiagnosticsElement);
        ic.Diagnostics = "Furore";
        Assert.IsNotNull(ic.DiagnosticsElement);

        ic.Expression = new string[] { "json", "xml" };
        Assert.IsNotNull(ic.ExpressionElement);
        Assert.HasCount(2, ic.ExpressionElement);
        Assert.AreEqual("json", ic.ExpressionElement.First().Value);

        ic.ExpressionElement = new List<FhirString>();
        ic.ExpressionElement.Add(new FhirString("csv"));
        Assert.IsNotNull(ic.Expression);
        Assert.AreEqual(1, ic.Expression.Count());
    }

    [TestMethod]
    public void ExtensionManagement()
    {
        Patient p = new();
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
        var cPat1 = new Patient() { Id = "pat1" };
        var cPat2 = new Patient() { Id = "pat2" };
        var pat = new Patient
        {
            Contained = [cPat1, cPat2]
        };

        var rref = new ResourceReference() { Reference = "#pat2" };

        Assert.IsNotNull(pat.FindContainedResource(rref), "#pat2 should be in the contained resources");
        Assert.IsNotNull(pat.FindContainedResource("#pat1"), "#pat1 should be in the contained resources");

        rref.Reference = "#pat3";
        Assert.IsNull(pat.FindContainedResource(rref));

        Assert.AreEqual(pat, pat.FindContainedResource("#"));

        var pat2 = new Patient();
        Assert.IsNull(pat2.FindContainedResource("#pat1"));
    }


    [TestMethod]
    public void FindContainedResourceExceptionExpected()
    {
        var cPat1 = new Patient() { Id = "pat1" };
        var cPat2 = new Patient() { Id = "pat2" };
        var pat = new Patient
        {
            Contained = [cPat1, cPat2]
        };

        Assert.Throws<ArgumentNullException>(() => pat.FindContainedResource((ResourceReference)null));
    }

    [TestMethod]
    public void TestListDeepCopy()
    {
        var x = new List<Patient> { new(), new() };

        var y = new List<Patient>(x.DeepCopyInternal());
        Assert.IsNotNull(x[0]);
        Assert.AreNotEqual(x[0], y[0]);
        Assert.AreNotEqual(x[1], y[1]);
    }


    [TestMethod]
    public void TestLazyCreatedLists()
    {
        var p = new Patient();
        p.Name.Add(new HumanName());
    }


    [TestMethod]
    public void TestStringValueInterface()
    {
        IValue<string> sv = new FhirString("test");
        Assert.IsNotNull(sv);
        sv.Value = "string";
        Assert.AreEqual("string", sv.Value);

        sv = new FhirUri("test");
        Assert.IsNotNull(sv);
        sv.Value = "http://example.org";
        Assert.AreEqual("http://example.org", sv.Value);

        sv = new Uuid("test");
        Assert.IsNotNull(sv);
        sv.Value = "550e8400-e29b-41d4-a716-446655440000";
        Assert.AreEqual("550e8400-e29b-41d4-a716-446655440000", sv.Value);

        sv = new Oid("test");
        Assert.IsNotNull(sv);
        sv.Value = "2.16.840.1.113883";
        Assert.AreEqual("2.16.840.1.113883", sv.Value);

        sv = new Markdown("test");
        Assert.IsNotNull(sv);
        sv.Value = "Hello World!";
        Assert.AreEqual("Hello World!", sv.Value);

        sv = new Date();
        Assert.IsNotNull(sv);
        sv.Value = "20161201";
        Assert.AreEqual("20161201", sv.Value);

        sv = new Time();
        Assert.IsNotNull(sv);
        sv.Value = "23:59:00";
        Assert.AreEqual("23:59:00", sv.Value);

        sv = new FhirDateTime(DateTimeOffset.UtcNow);
        Assert.IsNotNull(sv);
        sv.Value = "20161201 23:59:00";
        Assert.AreEqual("20161201 23:59:00", sv.Value);

    }


    [TestMethod]
    public void TestIntegerValueInterface()
    {
        INullableValue<int> iv = new Integer(null);
        Assert.IsNotNull(iv);
        iv.Value = 12345;
        Assert.AreEqual(12345, iv.Value);

        iv = new UnsignedInt(0);
        Assert.IsNotNull(iv);
        iv.Value = 12345;
        Assert.AreEqual(12345, iv.Value);

        iv = new PositiveInt(1);
        Assert.IsNotNull(iv);
        iv.Value = 12345;
        Assert.AreEqual(12345, iv.Value);
    }


    [TestMethod]
    public void ToStringHandlesNullObjectValue()
    {
        var s = new FhirString(null);
        Assert.IsNull(s.ToString());

        var i = new FhirBoolean(null);
        Assert.IsNull(i.ToString());
    }

#pragma warning disable CS0618 // Type or member is obsolete
    [TestMethod]
    public void ChildrenHandlesExceptionCasesCorrectly()
    {
        //"Narrative.div" => $"new FhirString({info.PropertyName}.Value)",
        //         "Element.id" => $"new FhirString({info.PropertyName})",
        //         "Extension.url" => $"new FhirUri({info.PropertyName})",
        var n = new Narrative("<p>bla</p>") { ElementId = "id1", Status = Narrative.NarrativeStatus.Additional };

        n.Children().ToList().IsExactly([new FhirString("id1"), new Code<Narrative.NarrativeStatus>(Narrative.NarrativeStatus.Additional), new FhirString("<p>bla</p>") ])
            .Should().BeTrue();

        var e = new Extension("http://nu.nl", new FhirBoolean(true));
        e.Children().ToList().IsExactly([new FhirUri("http://nu.nl"), new FhirBoolean(true)]).Should().BeTrue();
    }

    [TestMethod]
    public void TestChildren_EmptyResource()
    {
        var oo = new OperationOutcome();
        var children = oo.Children().ToArray();
        Base[] expected = [];
        Assert.IsTrue(expected.SequenceEqual(children));
    }

    [TestMethod]
    public void TestChildren_EmptyDataType()
    {
        var timing = new Identifier();
        var children = timing.Children().ToArray();
        Base[] expected = [];
        Assert.IsTrue(expected.SequenceEqual(children));
    }

    [TestMethod]
    public void TestChildren_Patient()
    {
        var patient = new Patient
        {
            Name =
            {
                new HumanName()
                {
                    Given = ["John"],
                    Family = "Doe"
                },
                new HumanName()
                {
                    Given = ["Alias"],
                    Family = "Alternate"
                }
            },
            Address =
            {
                new Address()
                {
                    City = "Amsterdam",
                    Line = ["Rokin"]
                }
            }
        };
        var children = patient.Children().ToArray();
        Base[] expected =
        [
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
            patient.Address[0]
            // patient.MaritalStatus,
            // patient.MultipleBirth,
            // patient.Photo = empty collection
            // patient.Contact = empty collection
            // patient.Animal,
            // patient.Communication = empty collection
            // patient.CareProvider = empty collection
            // patient.ManagingOrganization
            // patient.Link = empty collection
        ];
        Assert.IsTrue(expected.SequenceEqual(children));

        var name = patient.Name[0];
        children = name.Children().ToArray();
        expected =
        [
            // ===== Element elements =====
            // name.Extension = empty collection

            // ===== HumanName elements =====
            // name.UseElement,
            // name.TextElement,
            name.FamilyElement,
            name.GivenElement[0]
            // name.Period
        ];
        Assert.IsTrue(expected.SequenceEqual(children));

        var address = patient.Address[0];
        children = address.Children().ToArray();
        expected =
        [
            // ===== Element elements =====
            // name.Extension = empty collection

            // ===== Address elements =====
            // address.UseElement,
            // address.TypeElement,
            // address.TextElement,
            address.LineElement[0],
            address.CityElement
            // address.DistrictElement,
            // address.StateElement,
            // address.PostalCodeElement,
            // address.CountryElement,
            // address.Period
        ];
        Assert.IsTrue(expected.SequenceEqual(children));
    }
}

#pragma warning restore CS0618 // Type or member is obsolete