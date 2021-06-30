using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class IssuesTests
    {
        /// <summary>
        /// See https://github.com/FirelyTeam/firely-net-sdk/issues/474
        /// </summary>
        [TestMethod]
        public async Tasks.Task Issue474StartdateIs0001_01_01()
        {
            var json = "{ \"resourceType\": \"Patient\", \"active\": true, \"contact\": [{\"organization\": {\"reference\": \"Organization/1\", \"display\": \"Walt Disney Corporation\" }, \"period\": { \"start\": \"0001-01-01\", \"end\": \"2018\" } } ],}";

            var ctx = new ValidationSettings()
            {
                ResourceResolver = ZipSource.CreateValidationSource(),
            };

            var validator = new Validator(ctx);

            var pat = await new FhirJsonParser().ParseAsync<Patient>(json);

            var report = validator.Validate(pat);
            Assert.IsTrue(report.Success);
        }       
    }
}
