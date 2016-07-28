/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
#if PORTABLE45
	public class PortableModelTests
#else
	public class ModelTests
#endif
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
        public void OperationOutcomeLocation()
        {
            OperationOutcome oo = new OperationOutcome();
            oo.Issue.Add(new OperationOutcome.IssueComponent()
            {
                Location = new string[] { "yes" }
            });
            Assert.AreEqual(1, oo.Issue[0].Location.Count());
        }

        [TestMethod]
        public void DateTimeHandling()
        {
            FhirDateTime dt = new FhirDateTime("2010-01-01");
            Assert.AreEqual("2010-01-01", dt.Value);

            FhirDateTime dt2 = new FhirDateTime(1972, 11, 30, 15, 10);
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


        [TestMethod]
        public void Uri_Canonical()
        {
            var identifier = new Identifier("http://nhi.health.nz", "123");
            Assert.AreEqual("123", identifier.Value);
            Assert.AreEqual("http://nhi.health.nz", identifier.System);
        }
       

        [TestMethod]
        public void SimpleValueSupport()
        {
            Conformance c = new Conformance();

            Assert.IsNull(c.Experimental);
            c.Experimental = true;
            Assert.IsTrue(c.Experimental.GetValueOrDefault());
            Assert.IsNotNull(c.Experimental);
            Assert.IsTrue(c.ExperimentalElement.Value.GetValueOrDefault());

            c.PublisherElement = new FhirString("Furore");
            Assert.AreEqual("Furore", c.Publisher);
            c.Publisher = null;
            Assert.IsNull(c.PublisherElement);
            c.Publisher = "Furore";
            Assert.IsNotNull(c.PublisherElement);

            c.Format = new string[] { "json", "xml" };
            Assert.IsNotNull(c.FormatElement);
            Assert.AreEqual(2, c.FormatElement.Count);
            Assert.AreEqual("json", c.FormatElement.First().Value);

            c.FormatElement = new List<Code>();
            c.FormatElement.Add(new Code("csv"));
            Assert.IsNotNull(c.Format);
            Assert.AreEqual(1, c.Format.Count());
        }


        [TestMethod]
        public void ExtensionManagement()
        {
            Patient p = new Patient();
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
            var me = p.AddExtension("http://fhir.org/ext/ext-test3", new FhirString("bla"), isModifier:true);
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


        //[TestMethod]
        //public void FindContainedResource()
        //{
        //    var cPat1 = new Patient() { Id = "pat1" };
        //    var cPat2 = new Patient() { Id = "pat2" };
        //    var pat = new Patient();

        //    pat.Contained = new List<Resource> { cPat1, cPat2 };

        //    var rref = new ResourceReference() { Reference = "#pat2" };

        //    Assert.IsNotNull(pat.FindContainedResource(rref));
        //    Assert.IsNotNull(pat.FindContainedResource(rref.Url));
            
        //    rref.Reference = "#pat3";
        //    Assert.IsNull(pat.FindContainedResource(rref));
        //}

        [TestMethod]
        public void TestListDeepCopy()
        {
            var x = new List<Patient>();
            x.Add(new Patient());
            x.Add(new Patient());

            var y = new List<Patient>(x.DeepCopy());
            Assert.IsTrue(x[0] is Patient);
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
            var enumValues = Enum.GetValues(typeof(FHIRDefinedType));
            for (int i = 0; i < enumValues.Length; i++)
            {
                var type = (FHIRDefinedType)i;
                var typeName = ModelInfo.FhirTypeToFhirTypeName(type);
                var type2 = ModelInfo.FhirTypeNameToFhirType(typeName);
                Assert.IsTrue(type2.HasValue);
                Assert.AreEqual(type, type2, String.Format("Failed: '{0}' != '{1}' ?!", type, type2));
                var typeName2 = ModelInfo.FhirTypeToFhirTypeName(type2.Value);
                Assert.AreEqual(typeName, typeName2, String.Format("Failed: '{0}' != '{1}' ?!", typeName, typeName2));
		    }
		}

        [TestMethod]
        public void TestStringValueInterface()
        {
            IStringValue sv = new FhirString("test");
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

            sv = new FhirDateTime(DateTime.Now);
            Assert.IsNotNull(sv);
            sv.Value = "20161201 23:59:00";
            Assert.AreEqual(sv.Value, "20161201 23:59:00");

    }

        [TestMethod]
        public void TestIntegerValueInterface()
        {
            INullableIntegerValue iv = new Integer(null);
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


    }
}
