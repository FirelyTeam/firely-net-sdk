using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using System.Linq;

namespace Hl7.Fhir.Tests.Rest
{
    [TestClass]
    public class PatchExtensionsTest
    {       
         [TestMethod]
        public void TestAddPatchParameter()
        {
            var parameters = new Parameters();

            parameters.AddAddPatchParameter("Patient", "birthdate", new Date("1930-01-01"));
            Assert.IsTrue(parameters.Parameter[0].Part.Any(p => p.Name == "type" && ((Code)p.Value).Value == "add"));
            Assert.IsTrue(parameters.Parameter[0].Part.Any(p => p.Name == "path" && ((FhirString)p.Value).Value == "Patient"));
            Assert.IsTrue(parameters.Parameter[0].Part.Any(p => p.Name == "name" && ((FhirString)p.Value).Value == "birthdate"));
            Assert.IsTrue(parameters.Parameter[0].Part.Any(p => p.Name == "value" && ((Date)p.Value).Value == "1930-01-01"));

            parameters.AddInsertPatchParameter("Patient.name[0].given", new FhirString("Donald"), 1);
            Assert.IsTrue(parameters.Parameter[1].Part.Any(p => p.Name == "type" && ((Code)p.Value).Value == "insert"));
            Assert.IsTrue(parameters.Parameter[1].Part.Any(p => p.Name == "path" && ((FhirString)p.Value).Value == "Patient.name[0].given"));
            Assert.IsTrue(parameters.Parameter[1].Part.Any(p => p.Name == "value" && ((FhirString)p.Value).Value == "Donald"));
            Assert.IsTrue(parameters.Parameter[1].Part.Any(p => p.Name == "index" && ((Integer)p.Value).Value == 1));

            parameters.AddDeletePatchParameter("Patient.maritalStatus");
            Assert.IsTrue(parameters.Parameter[2].Part.Any(p => p.Name == "type" && ((Code)p.Value).Value == "delete"));
            Assert.IsTrue(parameters.Parameter[2].Part.Any(p => p.Name == "path" && ((FhirString)p.Value).Value == "Patient.maritalStatus"));

            parameters.AddReplacePatchParameter("Patient.deceasedBoolean", new FhirBoolean(true));
            Assert.IsTrue(parameters.Parameter[3].Part.Any(p => p.Name == "type" && ((Code)p.Value).Value == "replace"));
            Assert.IsTrue(parameters.Parameter[3].Part.Any(p => p.Name == "path" && ((FhirString)p.Value).Value == "Patient.deceasedBoolean"));
            Assert.IsTrue(parameters.Parameter[3].Part.Any(p => p.Name == "value" && ((FhirBoolean)p.Value).Value == true));

            parameters.AddMovePatchParameter("Patient.name[0].given", 2, 1);
            Assert.IsTrue(parameters.Parameter[4].Part.Any(p => p.Name == "type" && ((Code)p.Value).Value == "move"));
            Assert.IsTrue(parameters.Parameter[4].Part.Any(p => p.Name == "source" && ((Integer)p.Value).Value == 2));
            Assert.IsTrue(parameters.Parameter[4].Part.Any(p => p.Name == "destination" && ((Integer)p.Value).Value == 1));
        }
        
    }

}

