using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Hl7.Fhir.XPath;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class XsltContextTests
    {
        private XPathNavigator testDoc()
        {
            var doc = new XmlDocument();

            doc.LoadXml("<f:parent xmlns:f='http://hl7.org/fhir'><f:child val='2' /><f:child val='3' /><f:child val='3' /></f:parent>");

            return doc.CreateNavigator();
        }


        [TestMethod]
        public void TestUppercase()
        {
            var d = testDoc();
            var mgr = FhirNamespaceManager.CreateManager(d);

            var expr = d.Compile("upper-case('hoi!')");
            expr.SetContext(mgr);
            Assert.AreEqual("HOI!", d.Evaluate(expr));
        }

        [TestMethod]
        public void TestLowercase()
        {
            var d = testDoc();
            var mgr = FhirNamespaceManager.CreateManager(d);

            var expr = d.Compile("lower-case('Hoi!')");
            expr.SetContext(mgr);
            Assert.AreEqual("hoi!", d.Evaluate(expr));
        }

        [TestMethod]
        public void TestExists()
        {
            var d = testDoc();
            var mgr = FhirNamespaceManager.CreateManager(d);
            Assert.AreEqual(3, d.Select("/f:parent/f:child", mgr).Count);

            var p = d.SelectSingleNode("/f:parent", mgr);
            var expr = p.Compile("exists(f:child)");
            expr.SetContext(mgr);
            Assert.IsTrue((bool)p.Evaluate(expr));

            expr = p.Compile("not(exists(f:childX))");
            expr.SetContext(mgr);            
            Assert.IsTrue((bool)p.Evaluate(expr));
        }

        [TestMethod]
        public void TestDistinct()
        {
            var d = testDoc();
            var mgr = FhirNamespaceManager.CreateManager(d);

            var p = d.SelectSingleNode("/f:parent", mgr);
            var expr = p.Compile("count(distinct-values(f:child/@val))");
            expr.SetContext(mgr);
            Assert.AreEqual(2, Convert.ToInt32(p.Evaluate(expr)));
        }


        [TestMethod]
        public void TestSyntaxError()
        {
            var d = testDoc();
            var mgr = FhirNamespaceManager.CreateManager(d);

            var p = d.SelectSingleNode("/f:parent", mgr);

            try
            {
                var expr = p.Compile("not(descendant-or-self::*/@*[not(name(.)=('abbr','accesskey', 'align', 'alt', 'axis'))])");
                expr.SetContext(mgr);

                p.Evaluate(expr);
                Assert.Fail();
            }
            catch { }
        }
        

        //[TestMethod]
        //public void TestForError()
        //{
        //    var d = testDoc();
        //    var mgr = FhirNamespaceManager.CreateManager(d);

        //    var p = d.SelectSingleNode("/f:parent", mgr);
        //    var expr = p.Compile("exists(for $id in f:contained/*/@id return $id[not(descendant::f:reference/@value=concat('#', $id))])");
        //    expr.SetContext(mgr);
        //    Assert.IsTrue((bool)p.Evaluate(expr));
        //}        
    }
}