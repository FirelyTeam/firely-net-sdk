using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hl7.FhirPath;
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

        /// <summary>
        /// This tests will prove that FhirPath PocoElement node value extraction throws cast exceptions when the item does not have a value 
        /// (such as when only having an extension on the property)
        /// See also PR 829
        /// </summary>
        [TestMethod]
        public void EmptyValueShouldNotThrowExceptions()
        {
            var appointment = new Appointment();

            appointment.PriorityElement = new UnsignedInt(null);
            appointment.PriorityElement.AddExtension("http://example.com/myExtension", new FhirBoolean(false));
            var actual = appointment.ToTypedElement();

            var prio = actual.Scalar("priority");
            Assert.IsNull(prio);
        }
    }
}
