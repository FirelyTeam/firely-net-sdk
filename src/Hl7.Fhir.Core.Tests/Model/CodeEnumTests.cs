/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Utility;

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
            Assert.IsNull(c.Value);

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
