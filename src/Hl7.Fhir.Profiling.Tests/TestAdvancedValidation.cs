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
using Hl7.Fhir.Introspection.Source;
using Fhir.Profiling.IO;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestAdvancedValidation
    {
        static Specification spec;

        [ClassInitialize] 
        public static void Init(TestContext context)
        {
            ArtifactResolver resolver = new ArtifactResolver(new FileArtifactSource("TestData"));
            SpecificationProvider provider = new SpecificationProvider(resolver);
            SpecificationBuilder builder = new SpecificationBuilder(provider);

            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add("http://disk/Profile/lipid.profile.expanded.xml");
            builder.Expand();

            spec =  builder.ToSpecification();

            throw new Exception("Ewout: Expanded profile contains structures that refer to the same type");
        }

        [TestMethod]
        public void CorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.xml");
            Report report = spec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }

        [TestMethod]
        public void IncorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.wrong.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Value, Status.Failed));
        }

       

        [TestMethod]
        public void Slicing()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.slice.valid.xml");
            Report report = spec.Validate(resource);
            Assert.IsTrue(report.IsValid);


            resource = FhirFile.LoadResource("TestData\\lipid.slice.invalid.xml");
            report = spec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(4, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.IsTrue(report.Contains(Group.Slice, Status.Failed));

            
            resource = FhirFile.LoadResource("TestData\\lipid.slice.invalid.extra.xml");
            report = spec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
        }
    }
}
