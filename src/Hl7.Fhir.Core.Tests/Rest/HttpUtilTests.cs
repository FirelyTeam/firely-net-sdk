using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Rest
{
    [TestClass]
    public class HttpUtilTests
    {
        [TestMethod]
        public void MakeRelativeFromBaseTest()
        {
            var data = GetTestData();

            foreach (var (baseUrl, url, expectedUrl) in data)
            {
                var result = HttpUtil.MakeRelativeFromBase(url, baseUrl);
                Assert.AreEqual(expectedUrl, result);
            }
        }

        private IEnumerable<(Uri baseUrl, Uri url, Uri expectedUrl)> GetTestData()
        {
            yield return (new Uri("http://example.com/fhir"), new Uri("http://example.com/fhir/Patient/1"), new Uri("Patient/1", UriKind.Relative));
            yield return (new Uri("http://example.com/fhir/"), new Uri("http://example.com/fhir/Patient/1"), new Uri("Patient/1", UriKind.Relative));
            yield return (new Uri("http://example.com/fhir/"), new Uri("http://example.com/fhir/metadata"), new Uri("metadata", UriKind.Relative));
            yield return (new Uri("http://example.com/fhir/"), null, null);
            yield return (new Uri("http://example.com/fhir/"), new Uri("http://otherserver.com/fhir/Patient/1"), new Uri("http://otherserver.com/fhir/Patient/1"));
        }
    }
}
