/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Tests.Introspection
{
    [TestClass]
    public class FhirPathExtensionTest
    {
        Bundle _bundle;

        [TestInitialize]
        public void SetupSource()
        {
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

            _bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
        }


        [TestMethod]
        public void TestResolve()
        {
            var statement = "Bundle.entry.where(fullUrl = 'http://example.org/fhir/Patient/e')" +
                         ".resource.managingOrganization.resolve().id";

            var result = _bundle.Select(statement);
            result.SingleOrDefault().Should().BeOfType<Id>().Which.Value.Should().Be("orgY");

            //var resultPoco = _parsed.Select(statement).SingleOrDefault() as Organization;
            //Assert.IsNotNull(resultPoco);
            //Assert.AreEqual("orgY", resultPoco.Id);
        }

        [TestMethod]
        public void TestResolve2()
        {
            var statement = "'http://example.org/doesntexist'.resolve().id";
            var called = false;
            var result = _bundle.Select(statement, new FhirEvaluationContext() { ElementResolver = resolver });
            Assert.IsTrue(called);

            IScopedNode resolver(string url)
            {
                called = true;
                return null;
            }
        }

        [TestMethod]
        public void TestResolveList()
        {
            var statement = "Bundle.entry.where(fullUrl = 'http://example.org/fhir/Patient/e')" +
                         ".resource.generalPractitioner.resolve().all($this is Organization)";

            var result = _bundle.Scalar(statement);
            result.Should().BeOfType<bool>().Which.Should().BeTrue();
        }
    }
}
