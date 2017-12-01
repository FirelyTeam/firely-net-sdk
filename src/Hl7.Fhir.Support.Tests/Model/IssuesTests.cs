using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Support.Tests.Model
{
    public class IssuesTests
    {
        /// <summary>
        /// See https://github.com/ewoutkramer/fhir-net-api/issues/474
        /// </summary>
        [TestMethod]
        public void Issue474StartdateIs0001_01_01()
        {
            var json = "{ \"resourceType\": \"Patient\", \"active\": true, \"contact\": [{\"organization\": {\"reference\": \"Organization/1\", \"display\": \"Walt Disney Corporation\" }, \"period\": { \"start\": \"0001-01-01\", \"end\": \"2018\" } } ],}";

            var ctx = new ValidationSettings()
            {
                ResourceResolver = ZipSource.CreateValidationSource(),
            };

            var validator = new Validator(ctx);

            var pat = new FhirJsonParser().Parse<Patient>(json);

            var report = validator.Validate(pat);
            Assert.IsTrue(report.Success);
        }
    }
}
