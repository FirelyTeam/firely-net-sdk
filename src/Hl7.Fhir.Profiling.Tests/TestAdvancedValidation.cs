/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.Source;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestAdvancedValidation
    {
        static SpecificationWorkspace spec;

        [ClassInitialize] 
        public static void Init(TestContext context)
        {
            SpecificationProvider provider = SpecificationProvider.CreateOffline(new FileArtifactSource("TestData"));
            SpecificationBuilder builder = new SpecificationBuilder(provider);

            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://disk/Profile/lipid.profile.xml#lipidPanel");
            builder.Expand();

            spec =  builder.ToSpecification();
        }

        [TestMethod,Ignore]
        public void CorrectFixedValue()
        {
            var resource = Factory.LoadResource("TestData\\lipid.fixvalue.xml");
            Report report = spec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }

        [TestMethod,Ignore]
        public void IncorrectFixedValue()
        {
            var resource = Factory.LoadResource("TestData\\lipid.fixvalue.wrong.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Value, Status.Failed));
        }



        [TestMethod,Ignore]
        public void SlicingValid()
        {
            var resource = Factory.LoadResource("TestData\\lipid.slice.valid.xml");
            Report report = spec.Validate(resource);
            Assert.IsTrue(report.IsValid);
        }


        [TestMethod,Ignore]
        public void SlicingInvalid()
        {
            var resource = Factory.LoadResource("TestData\\lipid.slice.invalid.xml");
            Report report = spec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(4, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.IsTrue(report.Contains(Group.Slice, Status.Failed));

        }
    }
}
