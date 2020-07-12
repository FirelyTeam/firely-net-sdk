/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Diagnostics;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.ElementModel;
using System.IO;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.FhirPath;
using Hl7.FhirPath;
using System.Linq;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class FhirPathExtensionTest
    {
        ITypedElement _bundleElement;
        Bundle _parsed;

        [TestInitialize]
        public void SetupSource()
        {
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

            _parsed = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            _bundleElement = new ScopedNode(_parsed.ToTypedElement());
        }


        [TestMethod]
        public void TestResolve()
        {
            var statement = "Bundle.entry.where(fullUrl = 'http://example.org/fhir/Patient/e')" +
                         ".resource.managingOrganization.resolve().id";

            var result = _bundleElement.Select(statement);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("orgY", result.First().Value);

            //var resultPoco = _parsed.Select(statement).SingleOrDefault() as Organization;
            //Assert.IsNotNull(resultPoco);
            //Assert.AreEqual("orgY", resultPoco.Id);
        }

        [TestMethod]
        public void TestResolve2()
        {
            var statement = "'http://example.org/doesntexist'.resolve().id";
            var called = false;
            var result = _bundleElement.Select(statement, new FhirEvaluationContext() { ElementResolver = resolver });
            Assert.IsTrue(called);

            ITypedElement resolver(string url)
            {
                called = true;
                return null;
            }
        }

        [TestMethod]
        public void TestIndexVarCollection()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var patientNode = FhirXmlNode.Parse(xml);
            var patient = patientNode.ToTypedElement(new PocoStructureDefinitionSummaryProvider());

            // Call $index on root
            var result = patient.Select("$index");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(0).ToList(), result.ToList());

            // Call $index on child
            result = patient.Children("identifier").ElementAt(2).Select("$index");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(2).ToList(), result.ToList());

            // Call $index on nested child
            result = patient.Select("contained[0].name[1]").FirstOrDefault().Select("$index");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(1).ToList(), result.ToList());

            // Call $index on primitive
            result = patient.Select("name[0].use.select($index)");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(0).ToList(), result.ToList());

            // Call $index on element with specific index
            result = patient.Select("identifier[0].select($index)");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(0).ToList(), result.ToList());

            result = patient.Select("identifier[2].select($index)");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEqual(ElementNode.CreateList(0).ToList(), result.ToList());

            // Call $index on input collection
            result = patient.Select("identifier.select($index)");
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(ElementNode.CreateList(0, 1, 2).ToList(), result.ToList());

            // Test $index in where selection
            result = patient.Select("$this.identifier.where($index = 1)");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Patient.identifier[1]", result.FirstOrDefault().Location);
            
            result = patient.Select("$this.children().where($index <= 10)");
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.Count());
            Assert.IsTrue(result.Select(e => e.Location).Distinct().Count() == result.Select(e => e.Location).Count()); // Make sure that all locations are unique

            // Test $index in all selection
            result = patient.Select("$this.children().all($index <= 10)");
            Assert.IsNotNull(result);
            Assert.IsFalse((bool)result.FirstOrDefault().Value);

            result = patient.Select("$this.children().all($index <= 100)");
            Assert.IsNotNull(result);
            Assert.IsTrue((bool)result.FirstOrDefault().Value);

            // Test $index in any selection
            result = patient.Select("$this.identifier.any($index = 1)");
            Assert.IsNotNull(result);
            Assert.IsTrue((bool)result.FirstOrDefault().Value);

            result = patient.Select("$this.identifier.any($index = 3)");
            Assert.IsNotNull(result);
            Assert.IsFalse((bool)result.FirstOrDefault().Value);

            // Test $index in repeat selection
            result = patient.Select("$this.children().repeat(children()[$index])");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any()); // Test if we are matching some arbitrary children in the contained resource
        }
    }
}
