/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Profiling;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestBasicValidation
    {
        static Specification spec;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = Factory.GetPatientSpec(expand: true, online: false);
        }

        [TestMethod]
        public void ValidResource()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.Valid.xml");
            Report report = spec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }
        
        [TestMethod]
        public void ValueSet_UnknownValue()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.ErrorUse.xml");
            Report report = spec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Coding, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooMuch()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.CardinalityPlus.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooLittle()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.CardinalityMinus.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void Constraint()
        {
            // <constraint value="f:name or f:telecom or f:address or f:organization"/>
            var resource = FhirFile.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        [TestMethod]
        public void WrongRootElement()
        {
            var resource = FhirFile.LoadResource("TestData\\invalidroot.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Structure, Status.Unknown));
        }

        [TestMethod]
        public void NamespaceXHtml()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.Narrative.correct.xml");
            Report report = spec.Validate(resource);
            //report.Errors.ToConsole();

            Assert.IsTrue(report.IsValid);

            // In this narrative node, the div element does not contain a xhtml namespace and should not be found by the validator
            resource = FhirFile.LoadResource("TestData\\Patient.Narrative.wrong.xml");
            report = spec.Validate(resource);
            //report.Errors.ToConsole();

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void OtherProfile()
        {
            var spec = Factory.GetOtherSpec(true, false, "http://someserver.nl/Profile/lipid.profile.xml#ldlCholesterol");

            var resource = FhirFile.LoadResource("TestData\\ldlcholesterol-correct.xml");
            var report = spec.Validate(resource);
        }
    }
}
