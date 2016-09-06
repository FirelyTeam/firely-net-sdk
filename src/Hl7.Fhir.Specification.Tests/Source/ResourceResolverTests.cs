/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Specification.Source;
using System.Net;
using Hl7.Fhir.Support;
using System.Xml.Linq;
using System.IO;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
#if PORTABLE45
	public class PortableResourceResolverTests
#else
    public class ResolverTests
#endif
    {
#if !PORTABLE45
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static IResourceResolver source = null;
        
        [TestMethod]
        public void ResolveByCanonicalFromZip()
        {
            var extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));
        }

        [TestMethod]
        public void ResolveByUriFromZip()
        {
            var extDefn = source.ResolveByUri("http://hl7.org/fhir/StructureDefinition/data-absent-reason");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Patient");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(StructureDefinition));

            extDefn = source.ResolveByUri("http://hl7.org/fhir/NamingSystem/tx-rxnorm");
            Assert.IsNotNull(extDefn);
            Assert.IsInstanceOfType(extDefn, typeof(NamingSystem));
        }


        [TestMethod]
        public void RetrieveWebArtifact()
        {
            var wa = new WebResolver();

            var artifact = wa.ResolveByUri("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/Observation");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Observation", ((StructureDefinition)artifact).Name);

            var ci = artifact.Annotation<OriginInformation>();
            Assert.AreEqual("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/Observation", ci.Origin);
        }

        private class TestFhirClient : Rest.FhirClient
        {
            private int _status = 0;

            public int Status
            {
                get { return _status; }
                private set { _status = value; }
            }

            public TestFhirClient(Uri endpoint) : base(endpoint) { Status = 1; }

            protected override void BeforeRequest(HttpWebRequest rawRequest, byte[] body)
            {
                Status = 2;
                base.BeforeRequest(rawRequest, body);
            }

            protected override void AfterResponse(HttpWebResponse webResponse, byte[] body)
            {
                Status = 3;
                base.AfterResponse(webResponse, body);
            }
        }

        [TestMethod]
        public void RetrieveWebArtifactCustomFhirClient()
        {
            TestFhirClient client = null;

            var wa = new WebResolver(id => client = new TestFhirClient(id));

            Assert.IsNull(client);

            var artifact = wa.ResolveByUri("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/Flag");

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Status, 3);

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }

        [TestMethod]
        public void RetrieveArtifactMulti()
        {
            var resolver = new MultiResolver(source, new WebResolver());

            var vs = resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var artifact = resolver.ResolveByUri("http://fhir-dev.healthintersections.com.au/open/StructureDefinition/flag");

            Assert.IsNotNull(artifact);
            Assert.IsTrue(artifact is StructureDefinition);
            Assert.AreEqual("Flag", ((StructureDefinition)artifact).Name);
        }


        [TestMethod]
        public void TestSourceCaching()
        {
            var src = new CachedResolver(new MultiResolver(ZipSource.CreateValidationSource(), new WebResolver()));

            Stopwatch sw1 = new Stopwatch();

            // Ensure looking up a failed endpoint repeatedly does not cost much time
            sw1.Start();
            src.ResolveByUri("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            src.ResolveByUri("http://some.none.existant.address.nl/fhir/StructureDefinition/bla");
            sw2.Stop();

            Debug.WriteLine("sw2 {0}, sw1 {1}", sw2.ElapsedMilliseconds, sw1.ElapsedMilliseconds);
            Assert.IsTrue(sw2.ElapsedMilliseconds <= sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);

            // Now try an existing artifact
            sw1.Restart();
            src.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            sw1.Stop();

            sw2.Restart();
            src.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw1.ElapsedMilliseconds && sw2.ElapsedMilliseconds < 100);
        }


        [TestMethod]
        public void TestSetupIsOnce()
        {
            var fa = ZipSource.CreateValidationSource();

            var sw = new Stopwatch();
            sw.Start();
            var vs = fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw.Stop();

            var sw2 = new Stopwatch();
            sw2.Start();
            var vs2 = fa.ResolveByCanonicalUri("http://hl7.org/fhir/v2/vs/0292");
            sw2.Stop();

            Assert.IsTrue(sw2.ElapsedMilliseconds < sw.ElapsedMilliseconds);
            Debug.WriteLine(String.Format("First time {0}, second time {1}", sw.ElapsedMilliseconds, sw2.ElapsedMilliseconds));
        }

        //[TestMethod]
        //public void RemoveNarrativeFromSpecFiles()
        //{
        //    var files = Directory.EnumerateFiles(@"C:\Git\fhir-net-api\src\Hl7.Fhir.Specification\data", "*.xml");

        //    foreach(var file in files)
        //    {
        //        var xdoc = XDocument.Load(file);
        //        var narrative = xdoc.Elements(XmlNs.XFHIR + "Bundle").Elements(XmlNs.XFHIR + "entry").Elements(XmlNs.XFHIR + "resource")
        //                .Elements().Elements(XmlNs.XFHIR + "text").Elements(XmlNs.XHTMLNS + "div");
        //        foreach(var narrativeElement in narrative)
        //        {
        //            narrativeElement.RemoveNodes();
        //            narrativeElement.Add(new XElement(XmlNs.XHTMLNS + "p",
        //                new XText("The narrative has been removed to reduce the size of the distribution of the Hl7.Fhir.Specification library")));
        //        }

        //        xdoc.Save(file);
        //    }
        //}
    }
#endif
}