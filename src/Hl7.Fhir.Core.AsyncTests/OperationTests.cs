using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NUnit.Framework;

namespace Hl7.Fhir.Core.AsyncTests
{
    public class OperationTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";
        [Test]
        public async Task InvokeTestPatientGetEverything()
        {
            var client = new FhirClient(_endpoint);
            var start = new FhirDateTime(2014, 11, 1);
            var end = new FhirDateTime(2020, 1, 1);
            var par = new Parameters().Add("start", start).Add("end", end);

            var bundleTask = client.InstanceOperationAsync(ResourceIdentity.Build("Patient", "SMART-1288992"), "everything", par);
            var bundle2Task = client.FetchPatientRecordAsync(ResourceIdentity.Build("Patient", "SMART-1288992"), start, end);

            await Task.WhenAll(bundleTask, bundle2Task);

            var bundle = (Bundle) bundleTask.Result;
            Assert.IsTrue(bundle.Entry.Any());

            var bundle2 = (Bundle)bundle2Task.Result;
            Assert.IsTrue(bundle2.Entry.Any());
        }
    }
}