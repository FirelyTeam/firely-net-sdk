/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public partial class ModelTests
    {
        [TestMethod]
        public void TestNamingSystemCanonical()
        {
            NamingSystem ns = new NamingSystem();

            Assert.IsNull(ns.Url);
            Assert.IsNull(ns.UrlElement);

            ns.UniqueId.Add(new NamingSystem.UniqueIdComponent { Value = "http://nu.nl" });
            ns.UniqueId.Add(new NamingSystem.UniqueIdComponent { Value = "http://dan.nl", Preferred = true });

            Assert.AreEqual("http://dan.nl", ns.Url);
            Assert.AreEqual("http://dan.nl", ns.UrlElement.Value);

            ns.UniqueId[1].Preferred = false;

            Assert.AreEqual("http://nu.nl", ns.Url);
            Assert.AreEqual("http://nu.nl", ns.UrlElement.Value);
        }

        [TestMethod]
        public void TestCheckMinorVersionCompatibiliy()
        {
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("4.0.1"));
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("4.0"));
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("4.0.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.2.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.1"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.2"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3"));
        }

        //If failed: change the description of the "STN" in the Currency enum of Money.cs from "SC#o TomC) and PrC-ncipe dobra" to "São Tomé and Príncipe dobra".
        [TestMethod]
        public void TestCorrectCurrencyDescription()
        {
            var currency = Money.Currencies.STN;
            currency.GetDocumentation().Should().Be("São Tomé and Príncipe dobra");
        }

        [TestMethod]
        public void ValidatePatientWithDataAbsentExtension()
        {
            // Test for issue #3171 - Patient.Validate(true) throws NullReferenceException 
            // when BirthDate has data-absent-reason extension but no value
            // This reproduces the exact scenario from the original issue #3171 report
            
            var patient = new Patient()
            {
                BirthDateElement = new Date()
                {
                    Extension = new List<Extension>()
                    {
                        new Extension
                        {
                            Url = "http://hl7.org/fhir/StructureDefinition/data-absent-reason",
                            Value = new Code
                            {
                                Value = "unknown"
                            }
                        }
                    }
                }
            };

            // This exact line was failing with "Object reference not set to an instance of an object"
            // in netstandard2.0 and earlier .NET versions, due to GetHashCode() being called
            // on primitive types with null values during validation
            patient.Validate(); // Should not throw NullReferenceException anymore
        }

        [TestMethod]
        public void DateGetHashCodeWithNullValue()
        {
            // Direct test for Date.GetHashCode() with null value - reproduces issue #3171
            var date = new Date();
            // Verify that Value is null
            Assert.IsNull(date.Value);
            
            // This should not throw NullReferenceException
            try
            {
                int hashCode = date.GetHashCode();
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail($"GetHashCode threw NullReferenceException: {ex.Message}");
            }
        }

        [TestMethod]
        public void AllPrimitiveTypesGetHashCodeWithNullValue()
        {
            // Test all primitive types to ensure they handle null values correctly
            var date = new Date();
            var dateTime = new FhirDateTime();
            var instant = new Instant();
            var time = new Time();
            
            // All should have null values
            Assert.IsNull(date.Value);
            Assert.IsNull(dateTime.Value);
            Assert.IsNull(instant.Value);
            Assert.IsNull(time.Value);
            
            // None should throw exceptions when GetHashCode is called
            try
            {
                int hashCode1 = date.GetHashCode();
                int hashCode2 = dateTime.GetHashCode();
                int hashCode3 = instant.GetHashCode();
                int hashCode4 = time.GetHashCode();
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail($"One of the GetHashCode calls threw NullReferenceException: {ex.Message}");
            }
        }
    }
}
