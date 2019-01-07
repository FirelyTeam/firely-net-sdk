using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Core.Tests.ElementModel
{
    [TestClass]
    public class PocoNavigatorTests
    {
        [TestMethod]
        public void PocoTypedElementPocoRoundtrip()
        {
            var patient = new Patient();
            var actual = patient.ToTypedElement().ToPoco<Patient>();
            Assert.IsNotNull(actual, "Roundtrip POCO -> ITypedElement -> POCO should succeed.");
        }
    }
}
