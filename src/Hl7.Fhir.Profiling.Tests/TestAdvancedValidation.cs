/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.IO;
using System.Linq;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestAdvancedValidation
    {
        static Specification lipidSpec;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            lipidSpec = Factory.GetLipidSpec();
            
        }

        [TestMethod]
        public void CorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.xml");
            Report report = lipidSpec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }

        [TestMethod]
        public void IncorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.wrong.xml");
            Report report = lipidSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Value, Status.Failed));
        }

       

        [TestMethod]
        public void Slicing()
        {
            // todo: Resolving does not resolve custom profiles (for testing)
            // therefore these profiles are loaded the old way.

            var resource = FhirFile.LoadResource("TestData\\lipid.slice.valid.xml");
            Report report = lipidSpec.Validate(resource);
            Assert.IsTrue(report.IsValid);


            resource = FhirFile.LoadResource("TestData\\lipid.slice.invalid.xml");
            report = lipidSpec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(4, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.IsTrue(report.Contains(Group.Slice, Status.Failed));

            
            resource = FhirFile.LoadResource("TestData\\lipid.slice.invalid.extra.xml");
            report = lipidSpec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
        }
    }
}
