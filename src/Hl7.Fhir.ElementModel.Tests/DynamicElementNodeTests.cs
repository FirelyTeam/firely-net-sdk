/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Model;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Introspection;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CSharp.RuntimeBinder;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class DynamicElementNodeTests
    {
        [TestMethod]
        public void DynamicSetters()
        {
            var prov = ModelInfo.GetStructureDefinitionSummaryProvider();
            var pat = ElementNode.Root(prov, "Patient");
            dynamic dynp = pat.Dynamic(prov);

            // manipulating the dynamic modifies the underlying ElementNode
            dynp.active = ElementNode.Root(prov, "boolean", value: false);
            Assert.AreEqual(false, pat["active"].Single().Value);

            // properties that do not exist cannot be retrieved...
            Assert.ThrowsException<RuntimeBinderException>(() => dynp.RandomName);

            // ...though getting it via an indexer would return null
            Assert.IsNull(dynp["RandomName"]);

            // you can set properties to a C# primitive, which is then turned
            // into the corresponding ElementNode
            // Note that C# primitives do not map directly to FHIR primitives,
            // but to the underlying universal system primitives.
            dynp.active = true;
            var patNode = pat["active"].Single();
            Assert.AreEqual(true, patNode.Value);
            Assert.AreEqual("System.Boolean", patNode.InstanceType);

            // you can set properties to a FHIR (Base-derived) type, which
            // is turned into an ElementNode too.
            // Note how even enums can be assigned as well here.
            dynp.contact = new Patient.ContactComponent() { Gender = AdministrativeGender.Female };
            var contactNode = pat["contact"]["gender"].Single();
            Assert.AreEqual("female", contactNode.Value);

            // but this should fail when you assign a random CLR object
            dynp.birthDate = new GenericUriParser(GenericUriParserOptions.Idn);

            // the setters will understand ICollection derived classes though,
            // to be able to set a repeating propery
            var names = new[] { makeName("Ewout", "Kramer"), makeName("Wouter", "Kramer") };
            dynp.name = names;
            var nameNodes = pat["name"]["given"];
            Assert.AreEqual(2, nameNodes.Count);
            CollectionAssert.AreEqual(new[] { "Ewout", "Wouter" }, nameNodes.Select(nn => nn.Value).ToList());

            // to align with the underlying ElementNode, you can set a repeating 
            // node by just assigning a single instance.
            dynp.name = makeName("Ewout", "Kramer");
            Assert.AreEqual(1, pat["name"].Count);

            // though existing items are removed, you do not add anything
            // by setting the property again
            dynp.name = makeName("Wouter", "Kramer");
            Assert.AreEqual(1, pat["name"].Count);

            // finally, just do a few sets to get a sense of the performance
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                dynp.active = true;
                dynp.gender = AdministrativeGender.Male;
                dynp.birthDate = new Date(1972, 11, 30);
            }
            sw.Stop();
            Console.WriteLine($"1000 runs tookk {sw.ElapsedMilliseconds} ms.");

            //Console.WriteLine(pat.ToJson());
            //Console.WriteLine(((ITypedElement)dynp).ToJson());          
            //Console.WriteLine(((ElementNode)dynp).ToJson());

            //List<dynamic> namesCheck = dynp.name;
            //Assert.AreEqual(2,namesCheck.Count);

            //dynp.contact = null;
            //dynp.name = makeName("Ewout", "Kramer");
            //namesCheck = dynp.name; // still a list!
            //Assert.AreEqual(1, namesCheck.Count);
            //Console.WriteLine(pat.ToJson());            

            //ElementNode given = dynp.name[0].given[0];
            //ElementNode given2 = dynp["name"][0].given[0];
            //Assert.AreEqual(given, given2);

            // test ["bla"] accessor

            ////// try the casts
            //var sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            //    var given3 = dynp.name[0].given[0];
            //    string name = given3;
            //    Assert.AreEqual("Ewout", name);
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //FhirBoolean b = dynp.active;
            //Assert.IsTrue(b.Value.Value);
            dynamic makeName(string given, string family)
            {
                dynamic hn = ElementNode.Root(prov, "HumanName").Dynamic(prov);
                hn.use = "usual";
                hn.given = new List<string> { given };
                hn.family = family;
                return hn;
            }

        }


    }
}