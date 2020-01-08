﻿using System;
using System.Linq;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class TypedElementOnSourceNodeTests
    {
        private readonly IStructureDefinitionSummaryProvider provider = new PocoStructureDefinitionSummaryProvider();

        [TestMethod]
        public void TestExceptionComplexTypeValue()
        {
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":\"Invalid\"}";
            var bundle = FhirJsonNode.Parse(bundleJson);
            var typedBundle = bundle.ToTypedElement(provider, "Bundle");

            try
            {
                var value = typedBundle.Children("entry").First().Value;
                Assert.Fail("Should have thrown on .Value as complex types can't have a value");
            }
            catch (StructuralTypeException)
            {
            }
        }
    }
}
