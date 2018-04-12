/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#if NET_XSD_SCHEMA
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Hl7.Fhir.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class JsonNavigatorTests
    {
        [TestMethod]
        public void TestConstruct()
        {
            var nav = buildNav();

            // At root;
            Assert.AreEqual(XPathNodeType.Root,nav.NodeType);
            Assert.IsFalse(nav.IsEmptyElement);
            Assert.AreEqual(String.Empty, nav.Name);
            Assert.AreEqual(String.Empty, nav.LocalName);
            Assert.AreEqual(String.Empty, nav.NamespaceURI);
            Assert.AreEqual(String.Empty, nav.Prefix);
            Assert.IsTrue(nav.Value.StartsWith("pat12001-05-06Acme Healthcareusualurn:oid:1.2.36.146.595.217.0.112345Organization/1http://hl7.org/fhir/example-do-not-use#recordStatusarchivedPeterJamesofficialChalmersJimusualhttp://hl7.org/fhir/example-do-not-use#Patient.avatar#pic1Duck imageurn:example-do-not-use:pi3.141592653589793http://hl7.org/fhir/v3/AdministrativeGenderMMale1974-12http://hl7.org/fhir/example-do-not-use#DateTime.calendargregoriantruehome534 Erewhon StPleasantVilleVic3999ASKUhttp://hl7.org/fhir/Profileiso-21090#nullFlavor3generated<div xmlns=\"http://www.w3.org/1999/xhtml\">"));
        }


        [TestMethod]
        public void TestRootToChild()
        {
            var nav = buildNav();
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.IsFalse(nav.IsEmptyElement);
            Assert.AreEqual("f:Patient", nav.Name);
            Assert.AreEqual("Patient", nav.LocalName);
            Assert.AreEqual(XmlNs.FHIR , nav.NamespaceURI);
            Assert.AreEqual(JsonXPathNavigator.FHIR_PREFIX, nav.Prefix);
        }

        [TestMethod]
        public void TestMoveToChild()
        {
            var nav = buildNav();
            Assert.IsTrue(nav.MoveToFirstChild());      // Patient
            Assert.IsTrue(nav.MoveToFirstChild());      // identifier
            Assert.IsTrue(nav.MoveToFirstChild());      // period
            Assert.IsTrue(nav.MoveToFirstChild());      // start

            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.IsFalse(nav.IsEmptyElement);
            Assert.AreEqual("f:start", nav.Name);

            Assert.IsFalse(nav.MoveToFirstChild());      // No child
            Assert.IsTrue(nav.MoveToFirstAttribute());  // value attribute
            Assert.AreEqual(XPathNodeType.Attribute, nav.NodeType);
            Assert.AreEqual("2001-05-06", nav.Value);

            Assert.IsFalse(nav.MoveToNextAttribute());       // no other attributes
            Assert.AreEqual(XPathNodeType.Attribute, nav.NodeType);  // should not have moved
            Assert.AreEqual("2001-05-06", nav.Value);

            Assert.IsTrue(nav.MoveToParent());      // move up to start
            Assert.IsTrue(nav.MoveToParent());      // move up to period
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.AreEqual("f:period", nav.Name);

            Assert.IsTrue(nav.MoveToNext());        
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.AreEqual("f:assigner", nav.Name);
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("f:display", nav.Name);
            
            Assert.IsTrue(nav.MoveToParent());      // back to assigner

            Assert.IsTrue(nav.MoveToNext());        // move to use
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.AreEqual("f:use", nav.Name);

            Assert.IsTrue(nav.MoveToNext());        // move to system
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.AreEqual("f:system", nav.Name);

            Assert.IsTrue(nav.MoveToNext());        // move to value
            Assert.AreEqual(XPathNodeType.Element, nav.NodeType);
            Assert.AreEqual("f:value", nav.Name);

            Assert.IsFalse(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToParent());  // identifier
            Assert.IsTrue(nav.MoveToParent());  // whole resource
            Assert.IsTrue(nav.MoveToParent()); // root
            Assert.IsFalse(nav.MoveToParent()); // root
        }


        [TestMethod]
        public void AttributeWithAppendix()
        {
            var nav = buildNav();
            Assert.IsTrue(nav.MoveToFirstChild());      // Patient

            Assert.IsTrue(nav.MoveToChild("contact", XmlNs.FHIR));
            Assert.IsTrue(nav.MoveToChild("name", XmlNs.FHIR));

            Assert.IsTrue(nav.MoveToFirstChild());  // family #1 - null
            Assert.AreEqual("f:family", nav.Name);
            Assert.IsFalse(nav.MoveToFirstAttribute()); // no value (null)
            Assert.IsTrue(nav.MoveToFirstChild());  // extension
            Assert.AreEqual("f:extension", nav.Name);
            nav.MoveToParent();

            Assert.IsTrue(nav.MoveToNext());        // family #2  - du
            Assert.AreEqual("f:family", nav.Name);
            
            Assert.IsTrue(nav.MoveToFirstAttribute()); // @value="du"
            Assert.AreEqual("du", nav.Value);
            Assert.AreEqual("value", nav.Name);
            Assert.IsTrue(nav.MoveToNextAttribute()); // @id="a2"
            Assert.AreEqual("a2", nav.Value);
            Assert.AreEqual("id", nav.Name);
            nav.MoveToParent();

            Assert.IsTrue(nav.MoveToNext());        // family #3  - null
            Assert.IsTrue(nav.MoveToNext());        // family #4  - Marché
            Assert.IsTrue(nav.MoveToNext());        // family #5  - null

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("f:given", nav.Name);
        }

        [TestMethod]
        public void DivXhtmlNamespace()
        {
            var nav = buildNav();
            Assert.IsTrue(nav.MoveToFirstChild());      // Patient

            Assert.IsTrue(nav.MoveToChild("text", XmlNs.FHIR)); // Move to narrative
            Assert.IsTrue(nav.MoveToChild("div", XmlNs.XHTML)); // Move to narrative
            Assert.IsTrue(nav.Value.StartsWith("<div xmlns=\"http://www.w3.org/1999/xhtml\""));

        }

        [TestMethod]
        public void ForwardBackwards()
        {
            var nav = buildNav();
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToFirstChild());   
            Assert.IsTrue(nav.MoveToNext()); 
            Assert.IsTrue(nav.MoveToNext());
            Assert.IsTrue(nav.MoveToPrevious());
            Assert.IsTrue(nav.MoveToPrevious());

            Assert.IsFalse(nav.MoveToPrevious());  // one time too many
        }


        [TestMethod]
        public void CloneIsAtSamePosition()
        {
            Assert.IsNotNull(EqualityComparer<NavigatorState>.Default);

            var nav = buildNav();
            var nav2 = buildNav();
            Assert.IsTrue(nav.IsSamePosition(nav2));

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.IsTrue(nav.MoveToFirstChild()); // nodeA

            // Just checking, it should NOT be the same position
            Assert.IsFalse(nav.IsSamePosition(nav2));
            
            Assert.IsTrue(nav.MoveToNext()); // nodeB[0]
            Assert.IsTrue(nav.MoveToNext()); // nodeB[1]

            // Navigate the same with nav2
            Assert.IsTrue(nav2.MoveToFirstChild());
            Assert.IsTrue(nav2.MoveToFirstChild()); // nodeA
            Assert.IsTrue(nav2.MoveToNext()); // nodeB[0]
            Assert.IsTrue(nav2.MoveToNext()); // nodeB[1]
            
            // Now, they should have arrived at the same position
            Assert.IsTrue(nav.IsSamePosition(nav2));

            var nav3 = new JsonXPathNavigator(nav);
            Assert.IsTrue(nav.IsSamePosition(nav3));
            Assert.IsTrue(nav2.IsSamePosition(nav3));
        }


        [TestMethod]
        public void TestSelect()
        {
            var nav = buildNav();
            var mgr = new XmlNamespaceManager(nav.NameTable);
            mgr.AddNamespace("f", XmlNs.FHIR);
            
            var result = nav.Select("/f:Patient/f:telecom", mgr);
            Assert.AreEqual(2, result.Count);

            var text = nav.SelectSingleNode("/f:Patient/f:telecom[2]/f:use/@value", mgr);
            Assert.AreEqual("work", text.Value);

            text = nav.SelectSingleNode("/f:Patient/f:birthDate/@value", mgr);
            Assert.AreEqual("1974-12", text.Value);

            text = nav.SelectSingleNode("/f:Patient/f:birthDate/f:extension[@url='http://hl7.org/fhir/example-do-not-use#DateTime.calendar']/f:valueCode/@value", mgr);
            Assert.AreEqual("gregorian", text.Value);
        }


        [TestMethod]
        public void TestSelectContained()
        {
            var nav = buildNav();
            var mgr = new XmlNamespaceManager(nav.NameTable);
            mgr.AddNamespace("f", XmlNs.FHIR);

            var contained = nav.Select("/f:Patient/f:contained", mgr);
            Assert.IsNotNull(contained);
            Assert.AreEqual(2, contained.Count);

            var result = nav.SelectSingleNode("/f:Patient/f:contained/f:Organization/f:identifier/f:system/@value", mgr);
            Assert.AreEqual("urn:ietf:rfc:3986", result.Value);

            // Test special contentType attr of Binary
            result = nav.SelectSingleNode("/f:Patient/f:contained/f:Binary/@contentType", mgr);
            Assert.AreEqual("image/gif", result.Value);

            // Test special text node of Binary
            result = nav.SelectSingleNode("/f:Patient/f:contained/f:Binary/text()", mgr);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.StartsWith("R0lGODlhEwARAPcAAAAAAAAA"));
        }

        [TestMethod]
        public void TestTransform()
        { 
            string identityTransform = @"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">" +
                    @"<xsl:template match=""@*|node()"">" +
                    @"<xsl:copy>" +
                            @"<xsl:apply-templates select=""@*""/>" +
                            @"<xsl:apply-templates select=""node()""/>" +
                        @"</xsl:copy>" +
                    @"</xsl:template>" +
                    @"</xsl:stylesheet>";

            IXPathNavigable navCreator = buildNav();
            var nav = navCreator.CreateNavigator();
            Assert.IsNotNull(nav);
            Assert.AreEqual(XPathNodeType.Root, nav.NodeType);

            StringWriter sw = new StringWriter();
            XmlWriter xmlw = XmlWriter.Create(sw);
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(XmlReader.Create(new StringReader(identityTransform)));
            xslt.Transform(navCreator, xmlw);
            xmlw.Flush();

            Assert.IsTrue(sw.ToString().StartsWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                "<f:Patient id=\"pat1\" xmlns:f=\"http://hl7.org/fhir\"><f:identifier><f:period><f:start value=\"2001-05-06\" /></f:period>"+
                "<f:assigner><f:display value=\"Acme Healthcare\" /></f:assigner><f:use value=\"usual\" />"));
        }

        private static JsonXPathNavigator buildNav()
        {
            var json = File.ReadAllText(@"TestData\TestPatient.json");
            var reader = new StringReader(json);
            return new JsonXPathNavigator(new JsonTextReader(reader));
        }
    }
}
#endif