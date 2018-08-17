/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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
            var bundleXml = File.ReadAllText("TestData\\bundle-contained-references.xml");

            _parsed = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            _bundleElement = new ScopedNode(_parsed.ToTypedElement());
        }


        [TestMethod]
        public void TestResolve()
        {
            var statement = "Bundle.entry.where(fullUrl = 'http://example.org/fhir/Patient/e')" +
                         ".resource.managingOrganization.resolve().id";

            var result = _bundleElement.ToElementNavigator().Select(statement);
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
            var result = _bundleElement.ToElementNavigator().Select(statement, new FhirEvaluationContext() { Resolver = resolver });
            Assert.IsTrue(called);

            IElementNavigator resolver(string url)
            {
                called = true;
                return null;
            }
        }
    }
}
