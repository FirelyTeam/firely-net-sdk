using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir
{
    [TestClass]
    public class PocoNavigatorTests
    {

        [TestMethod]
        public void TestPocoPath()
        {
            Patient p = new Patient();

            p.Active = true;
            p.ActiveElement.AddExtension("http://something.org", new FhirBoolean(false));
            p.ActiveElement.AddExtension("http://something.org", new Integer(314));

            var patient = new PocoNavigator(p);

            Assert.AreEqual("Patient", patient.Path);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0]", patient.Path);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0].id[0]", patient.Path);

            patient.MoveToNext();
            IElementNavigator v1 = patient.Clone(); v1.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", v1.Path);

            IElementNavigator v2 = patient.Clone(); v2.MoveToFirstChild();
            Assert.AreEqual("Patient.active[0].extension[1].value[0]", v2.Path);
        }
    }

}
