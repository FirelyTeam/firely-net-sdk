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

    }
}
