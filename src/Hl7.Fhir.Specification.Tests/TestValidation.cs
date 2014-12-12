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
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TestBasicValidation
    {
        static SpecificationWorkspace spec; 
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = SpecificationFactory.Create("http://hl7.org/fhir/Profile/Patient");
        }

        [TestMethod]
        public void ValidResource()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.Valid.xml");
            Report report = spec.Validate(resource);
            var errors = report.Errors;
            Assert.IsTrue(report.IsValid);
        }
        
        [TestMethod]
        public void ValueSet_UnknownValue()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.ErrorUse.xml");
            Report report = spec.Validate(resource);
            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Coding, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooMuch()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.CardinalityPlus.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void CardinalityTooLittle()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.CardinalityMinus.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void UnknownAttribute()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.UnknownAttribute.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Attribute, Status.Unknown));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void ComplexTypeValueAttribute()
        {
            // A complex type should not have a value attribute at the root element
            var resource = TestProvider.LoadResource("TestData\\Patient.ValueAttribute.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Attribute, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }



        [TestMethod]
        public void Constraint()
        {
            // <constraint value="f:name or f:telecom or f:address or f:organization"/>
            var resource = TestProvider.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        [TestMethod]
        public void WrongRootElement()
        {
            var resource = TestProvider.LoadResource("TestData\\invalidroot.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Structure, Status.Unknown));
        }

        [TestMethod]
        public void NamespaceXHtml()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.Narrative.correct.xml");
            Report report = spec.Validate(resource);
            //report.Errors.ToConsole();

            Assert.IsTrue(report.IsValid);

            // In this narrative node, the div element does not contain a xhtml namespace and should not be found by the validator
            resource = TestProvider.LoadResource("TestData\\Patient.Narrative.wrong.xml");
            report = spec.Validate(resource);
            //report.Errors.ToConsole();

            Assert.IsFalse(report.IsValid);
            Assert.IsTrue(report.Contains(Group.Cardinality, Status.Failed));
            Assert.AreEqual(1, report.ErrorCount);
        }

        [TestMethod]
        public void OtherProfile()
        {
            var spec = SpecificationFactory.Create("http://someserver.nl/Profile/lipid.profile.xml#ldlCholesterol");
            var resource = TestProvider.LoadResource("TestData\\ldlcholesterol-correct.xml");
            var report = spec.Validate(resource);
        }
    }
}
