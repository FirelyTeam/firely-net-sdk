/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Hl7.Fhir.XPath;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class JTokenExtensionTests
    {
        private Newtonsoft.Json.Linq.JObject getPatientExample()
        {
            //var reader = new StringReader(Properties.TestResources.TestPatientJson);
            var json = File.ReadAllText(Path.Combine("TestData", "TestPatient.json"));

            return (JObject)JObject.Parse(json);
        }

        [TestMethod]
        public void TestBasics()
        {
            var root = getPatientExample().AsResourceRoot();

            Assert.IsNotNull(root);
            Assert.AreEqual("Patient", root.Name);

            var children = root.ElementChildren();
                    
            Assert.IsFalse(children.Any(c => c.Name == "resourceType"));
            Assert.AreEqual(1,children.Count(c => c.Name == "identifier"));
            Assert.AreEqual(2,children.Count(c => c.Name == "name"));
            Assert.AreEqual(1, children.Count(c => c.Name == "birthDate"));
        }


        [TestMethod]
        public void TestPrimitives()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var bd = children.Single(c => c.Name == "deceasedBoolean");
            Assert.IsTrue(bd.Value is JObject);     // primitive has been turned into a JObject
            var bdVal = (JObject)bd.Value;
            var prim = bdVal.Properties().Single();
            Assert.IsTrue(prim.IsValueProperty());
            Assert.AreEqual(true, ((JValue)prim.Value).Value);
            Assert.AreEqual(true, bd.PrimitivePropertyValue().Value);
        }

        [TestMethod]
        public void TestComplex()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var bd = children.Single(c => c.Name == "identifier");
            Assert.IsTrue(bd.Value is JObject);     // complex remains a complex
            Assert.IsNotNull(((JObject)bd.Value)["system"]);
        }


        [TestMethod]
        public void TestExtendedProp()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var bd = children.Single(c => c.Name == "birthDate");
            Assert.AreEqual("1974-12", bd.PrimitivePropertyValue().Value);
            Assert.IsNotNull(((JObject)bd.Value)["extension"]);

            var active = (JObject)children.Single(c => c.Name == "active").Value;

            JToken crap;
            Assert.IsFalse(active.TryGetValue("(value)", out crap)); // !!!! there are extensions, but no value
            Assert.IsNotNull(active["extension"]);           
        }

        [TestMethod]
        public void TestExtendedPropArray()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var contact = children.Single(c => c.Name == "contact");
            var name = contact.ElementChildren().Single(c => c.Name == "name");
            var givenNames = name.ElementChildren().Where(c => c.Name == "given");

            Assert.AreEqual(3, givenNames.Count());

            var firstGiven = givenNames.First();
            Assert.AreEqual("Bénédicte", firstGiven.PrimitivePropertyValue().Value);
            Assert.IsNull(((JObject)firstGiven.Value)["extension"]);

            var scndGiven = givenNames.Skip(1).First();
            Assert.AreEqual("Denise", scndGiven.PrimitivePropertyValue().Value);
            Assert.IsNotNull(((JObject)scndGiven.Value)["extension"]);

            var thirdGiven = givenNames.Skip(2).First();
            Assert.AreEqual("Marie", thirdGiven.PrimitivePropertyValue().Value);
            Assert.IsNull(((JObject)thirdGiven.Value)["extension"]);
        }

        [TestMethod]
        public void TestArrays()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var names = children.Where(c => c.Name == "name");
            Assert.AreEqual(2,names.Count());
            Assert.IsTrue(names.All(n => n.Value is JObject));

            // move into first given name
            var name1 = names.First();
            var name1Children = name1.ElementChildren();
            var firstNames = name1Children.Where(c => c.Name == "given");
            Assert.AreEqual(2, firstNames.Count());

            Assert.IsTrue(firstNames.All(n => n.Value is JObject));
            Assert.AreEqual("Peter",firstNames.First().PrimitivePropertyValue().Value);
            Assert.AreEqual("James",firstNames.Skip(1).First().PrimitivePropertyValue().Value);
        }

        [TestMethod]
        public void TestContainedResourceExpansion()
        {
            var root = getPatientExample().AsResourceRoot();
            var children = root.ElementChildren();

            var cont = children.Where(c => c.Name == "contained");
            Assert.AreEqual(2, cont.Count());

            var cont1 = cont.First();
            Assert.IsNotNull(cont1.ElementChildren().SingleOrDefault(c => c.Name == "Binary"));
        }

    }
}
