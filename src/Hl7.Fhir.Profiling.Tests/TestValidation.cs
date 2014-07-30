/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.Profiling;
using Fhir.IO;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Introspection;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestBasicValidation
    {
        static Specification patientSpec;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            patientSpec = Factory.GetPatientSpec();
        }

        [TestMethod]
        public void ValidResource()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.Valid.xml");
            Report report = patientSpec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }
        
        [TestMethod]
        public void ValueSetUnknown()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.ErrorUse.xml");
            Report report = patientSpec.Validate(resource);
            // todo: bugfix ValueSet resolving
            // This validation should fail because the name use "unofficial" does not exist
            
            // However, the ProfileExpander/Resolver is not yet loading ValueSets, so ValueSet validation cannot take place.
            // As a result, the validation reports an unresolved error instead of a coding failed error.

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Coding, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooMuch()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.CardinalityPlus.xml");
            Report report = patientSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooLittle()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.CardinalityMinus.xml");
            Report report = patientSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void Constraint()
        {
            // <constraint value="f:name or f:telecom or f:address or f:organization"/>
            var resource = FhirFile.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = patientSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        [TestMethod]
        public void WrongRootElement()
        {
            var resource = FhirFile.LoadResource("TestData\\invalidroot.xml");
            Report report = patientSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Structure, Status.Unknown));
        }

        

        [TestMethod]
        public void NamespaceXHtml()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.Narrative.correct.xml");
            Report report = patientSpec.Validate(resource);
            report.Errors.ToConsole();

            Assert.IsTrue(report.IsValid);

            // In this narrative node, the div element does not contain a xhtml namespace and should not be found by the validator
            resource = FhirFile.LoadResource("TestData\\Patient.Narrative.wrong.xml");
            report = patientSpec.Validate(resource);
            report.Errors.ToConsole();

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

    }
}
