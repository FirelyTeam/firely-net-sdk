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
using System.Linq;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CSharp.RuntimeBinder;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Serialization;

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
            Assert.ThrowsException<ArgumentException>(() => dynp.birthDate = new GenericUriParser(GenericUriParserOptions.Idn));

            // the setters will understand ICollection derived classes though,
            // to be able to set a repeating propery
            var names = new[] { mn("Ewout", "Kramer"), mn("Wouter", "Kramer") };
            dynp.name = names;
            var nameNodes = pat["name"]["given"];
            Assert.AreEqual(2, nameNodes.Count);
            CollectionAssert.AreEqual(new[] { "Ewout", "Wouter" }, nameNodes.Select(nn => nn.Value).ToList());

            // to align with the underlying ElementNode, you can set a repeating 
            // node by just assigning a single instance.
            dynp.name = (ElementNode)mn("Ewout", "Kramer");
            Assert.AreEqual(1, pat["name"].Count);

            // though existing items are removed, you do not add anything
            // by setting the property again
            dynp.name = mn("Wouter", "Kramer");
            Assert.AreEqual(1, pat["name"].Count);

            // you can add an item by using `+=`
            //     dynp.name += makeName("Ewout", "Kramer");
            // ah, no.....ElementNode does not support adding stuff at random yet

            // finally, just do a few sets to get a sense of the performance
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 3333; i++)
            {
                dynp.active = true;
                dynp.gender = AdministrativeGender.Male;
                dynp.birthDate = new Date(1972, 11, 30);
            }
            sw.Stop();
            Console.WriteLine($"Running 10.000 setters took {sw.ElapsedMilliseconds} ms.");

            dynamic mn(string given, string family)
            {
                dynamic hn = ElementNode.Root(prov, "HumanName").Dynamic(prov);
                hn.use = "usual";
                hn.given = new List<string> { given };
                hn.family = family;
                return hn;
            }
        }

        [TestMethod]
        public void DynamicGetters()
        {
            var prov = ModelInfo.GetStructureDefinitionSummaryProvider();
            var pat = ElementNode.Root(prov, "Patient");
            dynamic dynp = pat.Dynamic(prov);

            // properties that do not exist cannot be retrieved...
            Assert.ThrowsException<RuntimeBinderException>(() => dynp.RandomName);

            // ...though getting it via an indexer would return null
            Assert.IsNull(dynp["RandomName"]);

            // You can always treat the dynamic properties as an ITypedElement
            // (note: this is actually a cast to ITypedElement)
            dynp.active = true;
            ITypedElement activeE = dynp.active;
            Assert.AreEqual(true, activeE.Value);
            Assert.AreEqual("System.Boolean", activeE.InstanceType);

            // This works for complex stuff too, of course
            var maritalStatusData = new CodeableConcept("http://nu.nl", "married", text: "Getrouwd");
            dynp.maritalStatus = maritalStatusData;
            ITypedElement contactE = dynp.maritalStatus;
            var compResult = contactE.IsEqualTo(maritalStatusData.ToTypedElement("maritalStatus"));
            Assert.IsTrue(compResult.Success);

            // the getters will return IReadOnlyCollection<dynamic> for properties that
            // are defined to be collections in the datadefinition
            var nameElements = new[] { mn("Ewout", "Kramer"), mn("Wouter", "Kramer") };
            dynp.name = nameElements;
            IReadOnlyCollection<dynamic> names = dynp.name;
            Assert.AreEqual(2, names.Count);

            // Though, if you can treat them as a collection of ITypedElement too...
            IReadOnlyCollection<ITypedElement> names2 = dynp.name;
            Assert.AreEqual(2, names2.Count);

            // Note that this works, even if you add a single element
            dynp.name = mn("Abel", "Enthoven");
            IReadOnlyCollection<ITypedElement> names3 = dynp.name;
            Assert.AreEqual(1, names3.Count);

            // for properies that do not repeat, you can just go deeper
            ITypedElement displayE = dynp.maritalStatus.text;
            Assert.AreEqual("Getrouwd", displayE.Value);

            // by the way, you can lookup properties by their name
            ITypedElement displayE2 = dynp.maritalStatus["text"];
            Assert.AreEqual("Getrouwd", displayE2.Value);

            // Repeating nodes can be indexed (this is in addition to lookup by name)
            ITypedElement given = dynp.name[0].given[0];
            Assert.AreEqual("Abel", given.Value);
            ITypedElement given2 = dynp["name"][0].given[0];
            Assert.AreEqual(given.Value, given2.Value);

            // finally, just do a few sets to get a sense of the performance
            dynp.active = true;
            dynp.gender = AdministrativeGender.Male;
            dynp.birthDate = new Date(1972, 11, 30);

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 3333; i++)
            {
                _ = dynp.active;
                _ = dynp.gender;
                _ = dynp.birthDate;
            }
            sw.Stop();
            Console.WriteLine($"Running 10.000 getters took {sw.ElapsedMilliseconds} ms.");

            ElementNode mn(string given, string family)
            {
                dynamic hn = ElementNode.Root(prov, "HumanName").Dynamic(prov);
                hn.use = "usual";
                hn.given = new List<string> { given };
                hn.family = family;
                return hn;
            }
        }

        [TestMethod]
        public void DynamicCasts()
        {
            var prov = ModelInfo.GetStructureDefinitionSummaryProvider();
            var pat = ElementNode.Root(prov, "Patient");
            dynamic dynp = pat.Dynamic(prov);

            // You can treat any dynamic as an ITypedElement
            ITypedElement patE = dynp;
            Assert.AreEqual("Patient", patE.InstanceType);

            // But really, they are ElementNode internally.
            // So, if you ever wanted to manipulate them directly using
            // the ElementNode interface, you can.
            ElementNode patEN = dynp;
            patEN.Add(prov, "active", true);

            // Since you are manipulating the underlying data,
            // the dynamic will immediately reflect the changes.
            ITypedElement activeE = dynp.active;
            Assert.AreEqual(true, activeE.Value);

            // simple primitives can be cast to dotnet primitives
            bool activeB = dynp.active;
            Assert.IsTrue(activeB);

            // complex types can be cast to their POCOs
            dynp.name = mn("Abel", "Enthoven");
            HumanName hn = dynp.name[0];
            Assert.AreEqual("Enthoven", hn.Family);

            // finally, just do a few sets to get a sense of the performance
            dynamic tA = dynp.active;
            dynamic tN = dynp.name[0];

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 3300; i++)
            {
                _ = (bool)tA;
                _ = (ITypedElement)tA;
                _ = (HumanName)tN;
                _ = (ElementNode)tN;
            }
            sw.Stop();
            Console.WriteLine($"Running 10.000 conversions took {sw.ElapsedMilliseconds} ms.");


            ElementNode mn(string given, string family)
            {
                dynamic hn = ElementNode.Root(prov, "HumanName").Dynamic(prov);
                hn.use = "usual";
                hn.given = new List<string> { given };
                hn.family = family;
                return hn;
            }
        }


        [TestMethod]
        public void DynamicSerialize()
        {
            var prov = ModelInfo.GetStructureDefinitionSummaryProvider();
            var pat = ElementNode.Root(prov, "Patient");
            dynamic dynp = pat.Dynamic(prov);

            // manipulating the dynamic modifies the underlying ElementNode
            dynp.active = ElementNode.Root(prov, "boolean", value: false);
            dynp.contact = new Patient.ContactComponent() { Gender = AdministrativeGender.Female };
            dynp.name = new[] { mn("Ewout", "Kramer"), mn("Wouter", "Kramer") };
            dynp.maritalStatus = new CodeableConcept("http://nu.nl", "married", text: "Getrouwd");
            dynp.gender = AdministrativeGender.Male;
            dynp.birthDate = new Date(1972, 11, 30);

            var expectedJson = @"{""resourceType"":""Patient"",""active"":false,""contact"":[{""gender"":""female""}]," +
                @"""name"":[{""use"":""usual"",""given"":[""Ewout""],""family"":""Kramer""},{""use"":""usual"",""given"":[""Wouter""],""family"":""Kramer""}]," +
                @"""maritalStatus"":{""coding"":[{""system"":""http://nu.nl"",""code"":""married""}],""text"":""Getrouwd""},""gender"":""male"",""birthDate"":""1972-11-30""}";

            test(pat);
            test((ITypedElement)dynp);
            test((ElementNode)dynp);

            dynamic mn(string given, string family)
            {
                dynamic hn = ElementNode.Root(prov, "HumanName").Dynamic(prov);
                hn.use = "usual";
                hn.given = new List<string> { given };
                hn.family = family;
                return hn;
            }

            void test(ITypedElement te) => Assert.AreEqual(expectedJson, te.ToJson(), te.ToJson());
        }
    }
}