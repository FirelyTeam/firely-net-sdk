﻿/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class CodeEnumTests
    {
        [TestMethod]
        public void SetValueUpdatesRawValue()
        {
            var c = new Code<AdministrativeGender>();
            Assert.IsNull(c.ObjectValue);
            Assert.IsNull(c.Value);

            c = new Code<AdministrativeGender>(AdministrativeGender.Female);
            Assert.AreEqual("female", c.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Female, c.Value);

            c.Value = AdministrativeGender.Unknown;
            Assert.AreEqual("unknown", c.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Unknown, c.Value);
        }


        [TestMethod]
        public void SetRawValueUpdatesValue()
        {
            var c = new Code<AdministrativeGender>(AdministrativeGender.Female);
            c.ObjectValue = "male";
            Assert.AreEqual(AdministrativeGender.Male, c.Value);

            c.ObjectValue = "maleX";
            Assert.ThrowsException<InvalidCastException>(() => c.Value);

            c.Value = AdministrativeGender.Other;
            Assert.AreEqual("other", c.ObjectValue);

            c.ObjectValue = null;
            Assert.IsNull(c.Value);
        }

        [TestMethod]
        public void TestISystemAndCode()
        {
            var c = new Code<AdministrativeGender>(AdministrativeGender.Female) as ISystemAndCode;

            Assert.AreEqual("female", c.Code);
            Assert.AreEqual("http://hl7.org/fhir/administrative-gender", c.System);

            c = new Code<TestEnum>(TestEnum.IHaveNoSystem) as ISystemAndCode;
            Assert.AreEqual("IHaveNoSystem", c.Code);
            Assert.IsNull(c.System);
        }

        [FhirEnumeration("TestEnum")]
        private enum TestEnum
        {
            IHaveNoSystem = 4
        }
    }
}
