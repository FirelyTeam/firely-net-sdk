/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
#if PORTABLE45
	public class PortableCodeEnumTests
#else
	public class CodeEnumTests
#endif
    {
        [TestMethod]
        public void SetValueUpdatesRawValue()
        {
            var c = new Code<AdministrativeGender>();
            Assert.IsNull(c.RawValue);
            Assert.IsNull(c.Value);

            c = new Code<AdministrativeGender>(AdministrativeGender.Female);
            Assert.AreEqual("female", c.RawValue);
            Assert.AreEqual(AdministrativeGender.Female, c.Value);

            c.Value = AdministrativeGender.Unknown;
            Assert.AreEqual("unknown", c.RawValue);
            Assert.AreEqual(AdministrativeGender.Unknown, c.Value);
        }


        [TestMethod]
        public void SetRawValueUpdatesValue()
        {
            var c = new Code<AdministrativeGender>(AdministrativeGender.Female);
            c.RawValue = "male";
            Assert.AreEqual(AdministrativeGender.Male, c.Value);

            c.RawValue = "maleX";
            Assert.IsNull(c.Value);

            c.Value = AdministrativeGender.Other;
            Assert.AreEqual("other", c.RawValue);

            c.RawValue = null;
            Assert.IsNull(c.Value);
        }

    }
}
