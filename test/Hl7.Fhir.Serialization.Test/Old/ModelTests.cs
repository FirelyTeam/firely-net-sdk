using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void OIDandUUIDUrls()
        {
            //var oidUrl = "urn:oid:1.2.3";
            //var illOidUrl = "urn:oid:datmagdusniet";
            //var uuidUrl = "urn:uuid:a5afddf4-e880-459b-876e-e4591b0acc11";
            //var illUuidUrl = "urn:uuid:ooknietgoed";
            //var oidWithZero = "urn:oid:1.2.0.3.4";

            //FhirUri uri = FhirUri.ParseValue(oidUrl);
            //Assert.AreEqual(0,uri.Validate().Count);

            //uri = FhirUri.Parse(illOidUrl);
            //Assert.AreEqual(1, uri.Validate().Count);

            //uri = FhirUri.Parse(uuidUrl);
            //Assert.AreEqual(0, uri.Validate().Count);

            //uri = FhirUri.Parse(illUuidUrl);
            //Assert.AreEqual(1, uri.Validate().Count);

            //Assert.IsTrue(Uri.Equals(new Uri("http://nu.nl"), new Uri("http://nu.nl")));

            //uri = FhirUri.Parse(oidWithZero);
            //Assert.AreEqual(0, uri.Validate().Count);
        }


        [TestMethod]
        public void ValidateResourceWithIncorrectElement()
        {
            FhirDateTime dt = new FhirDateTime();

            dt.Value = "Ewout Kramer";

            Observation o = new Observation { Applies = dt };
            DiagnosticReport rep = new DiagnosticReport();
            rep.Contained = new List<Resource> { o };

            var errors = dt.Validate();

            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void ValidateElementAssertions()
        {
            XElement xr = new XElement("root",
                        new XElement("child", "value"),
                        new XElement("child", "value2"));

            Assert.IsNull(xr.Element("childx"));
            Assert.AreEqual(0,xr.Elements("childx").Count());
            Assert.AreEqual("value",xr.Element("child").Value);
        }

        [TestMethod]
        public void ValidateEncodingNameAssertions()
        {
            Assert.AreEqual(Encoding.UTF8.HeaderName, Encoding.UTF8.WebName);
        }

        //[TestMethod]
        //public void VerifyCastOperators()
        //{
        //    FhirBoolean b = true;
        //    FhirBoolean bn = (bool?)null;
        //    FhirBoolean bn2 = new FhirBoolean(null);
        //    FhirBoolean bn3 = new FhirBoolean(false);

        //    Assert.AreEqual(true, b.Value);
        //    Assert.IsNull(bn);

        //    bool rb = (bool)b;
        //    Assert.AreEqual(true, rb);

        //    bool? rbn = (bool?)b;
        //    Assert.AreEqual(true, rbn);

        //    bool? rbn2 = (bool?)bn;
        //    Assert.IsFalse(rbn2.HasValue);
        //    Assert.IsNull(rbn2);

        //    try
        //    {
        //        bool rb2 = (bool)bn;
        //        Assert.Fail();
        //    }
        //    catch (InvalidCastException)
        //    {
        //    }
        //}


        [TestMethod]
        public void EnumParsing()
        {
            //var c = Code<Address.AddressUse>.Parse("temp");
            //Assert.AreEqual(Address.AddressUse.Temp, c.Value);
            //Assert.AreEqual("temp", c.ToString());

            //var f = Code<Quantity.QuantityCompararator>.Parse(">");
            //Assert.AreEqual(Quantity.QuantityCompararator.GreaterThan, f.Value);
            //Assert.AreEqual(">", f.ToString());
        }



        [TestMethod]
        public void DateTimeHandling()
        {
            //FhirDateTime dt = FhirDateTime.Parse("2010-01-01");

            //Assert.AreEqual("2010-01-01", dt.ToString());

            //FhirDateTime dt2 = new FhirDateTime(1972, 11, 30, 15, 10);
            //Assert.IsTrue(dt2.ToString().StartsWith("1972-11-30T15:10"));

            //dt = FhirDateTime.Parse("2013-08-18T00:00:00.000+12:00");
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

        [TestMethod]
        public void ResourceListFiltering()
        {
            var rl = new List<BundleEntry>();

            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/@1"), SelfLink = new Uri("http://x.com/@1/history/@1") });
            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/@1"), SelfLink = new Uri("http://x.com/@1/history/@2") });
            rl.Add(new ResourceEntry<CarePlan> { Id = new Uri("http://x.com/@2"), SelfLink = new Uri("http://x.com/@2/history/@1") });
            rl.Add(new DeletedEntry() { Id = new Uri("http://x.com/@2"), SelfLink = new Uri("http://x.com/@2/history/@2") });

            var tr = rl.ByResourceType<Patient>();
            Assert.AreEqual(2, tr.Count());
            var tr2 = rl.ByResourceType<CarePlan>();
            Assert.AreEqual(1, tr2.Count());

            var ur = rl.ById(new Uri("http://x.com/@1"));
            Assert.AreEqual(2, ur.Count());
            Assert.AreEqual(2, ur.ByResourceType<Patient>().Count());

            Assert.IsNotNull(ur.BySelfLink(new Uri("http://x.com/@1/history/@1")));
            Assert.IsNotNull(rl.BySelfLink(new Uri("http://x.com/@2/history/@2")));
        }
    }
}
