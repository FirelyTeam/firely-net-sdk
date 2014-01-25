using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests
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
        public void DateTimeHandling()
        {
            FhirDateTime dt = new FhirDateTime("2010-01-01");
            Assert.AreEqual("2010-01-01", dt.Value);

            FhirDateTime dt2 = new FhirDateTime(1972, 11, 30, 15, 10);
            Assert.IsTrue(dt2.Value.StartsWith("1972-11-30T15:10"));
        }

        [TestMethod]
        public void ConstructEntry()
        {
            var pe = new ResourceEntry<Patient>(new Uri("http://www.nu.nl/fhir/Patient/1"), DateTimeOffset.Now, new Patient());
            Assert.IsNotNull(pe.Id);
            Assert.IsNotNull(pe.Title);
            Assert.IsNotNull(pe.LastUpdated);
            Assert.IsNotNull(pe.Resource);
            ModelValidator.Validate(pe);

            var b = new Bundle("A test feed", DateTimeOffset.Now);
            b.AuthorName = "Ewout";

            Assert.IsNotNull(pe.Id);
            Assert.IsNotNull(pe.Title);
            Assert.IsNotNull(pe.LastUpdated);
            b.Entries.Add(pe);
            ModelValidator.Validate(b);
        }



        [TestMethod]
        public void SimpleValueSupport()
        {
            Conformance c = new Conformance();

            Assert.IsNull(c.AcceptUnknown);
            c.AcceptUnknown = true;
            Assert.IsTrue(c.AcceptUnknown.GetValueOrDefault());
            Assert.IsNotNull(c.AcceptUnknownElement);
            Assert.IsTrue(c.AcceptUnknownElement.Value.GetValueOrDefault());

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
            Uri u1 = new Uri("http://fhir.org/ext/ext-test");
            Assert.IsNull(p.GetExtension(u1));

            Extension newEx = p.SetExtension(u1, new FhirBoolean(true));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.AddExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Ewout"));
            Assert.AreSame(newEx, p.GetExtension(u1));

            p.RemoveExtension(u1);
            Assert.IsNull(p.GetExtension(u1));

            p.SetExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Ewout Kramer"));
            var ew = p.GetExtensions(new Uri("http://fhir.org/ext/ext-test2"));
            Assert.AreEqual(1, ew.Count());

            p.AddExtension(new Uri("http://fhir.org/ext/ext-test2"), new FhirString("Wouter Kramer"));

            ew = p.GetExtensions(new Uri("http://fhir.org/ext/ext-test2"));
            Assert.AreEqual(2, ew.Count());

        }

      

        [TestMethod]
        public void TypedResourceEntry()
        {
            var pe = new ResourceEntry<Patient>();

            pe.Resource = new Patient();

            ResourceEntry e = pe;

            Assert.AreEqual(pe.Resource, e.Resource);

            e.Resource = new CarePlan();

            try
            {
                var c = pe.Resource;
                Assert.Fail("Should have bombed");
            }
            catch (InvalidCastException)
            {
                // pass
            }
        }    
    }
}
