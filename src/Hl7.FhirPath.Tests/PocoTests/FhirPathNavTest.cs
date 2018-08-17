/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Hl7.FhirPath;
using Hl7.FhirPath.Functions;
using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.FhirPath;

namespace Hl7.FhirPath.Tests
{
    public class FhirPathNavTest
    {
        public IElementNavigator getTestData()
        {
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");
            // var tree = TreeConstructor.FromXml(tpXml);
            // var navigator = new TreeNavigator(tree);
            // return navigator;

            var patient = (new FhirXmlParser()).Parse<Hl7.Fhir.Model.Patient>(tpXml);
            return patient.ToElementNavigator();
        }

        [Fact]
        public void TestNavigation()
        {
            var values = getTestData();

            var r = values.Navigate("Patient");

            var result = values.Navigate("Patient").Navigate("identifier").Navigate("use");
            Assert.Equal(3, result.Count()); 
            Assert.Equal("usual", result.First().Value);
        }

        [Fact]
        public void TestNavigationALTERNATIVE()
        {
            var values = getTestData();

            var result = values.Navigate("Patient").Navigate("identifier").Navigate("use");
            Assert.Equal(3, result.Count());
            Assert.Equal("usual", (string)result.First().Value);
        }

    }
}