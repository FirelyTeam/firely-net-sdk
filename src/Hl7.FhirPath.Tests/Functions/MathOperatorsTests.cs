﻿using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Hl7.FhirPath.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace HL7.FhirPath.Tests.Functions
{
    [TestClass]
    public class MathOperatorsTests
    {
        [TestMethod]
        public void Sqrt()
        {
            (-1.0m).Sqrt().Should().BeNull();

            4.0m.Sqrt().Should().BeOfType(typeof(decimal));
        }

        [TestMethod]
        public void Power()
        {
            (-1.0m).Power(0.5m).Should().BeNull();

            2m.Power(2m).Should().BeOfType(typeof(decimal));
        }
    }


}
#nullable restore