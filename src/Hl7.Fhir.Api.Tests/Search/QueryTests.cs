using Hl7.Fhir.Model;
using Hl7.Fhir.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Test.Search
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void RetrieveCriteria()
        {
            var q = new Query()
                .For("Patient").Where("name:exact=ewout").OrderBy("birthDate", SortOrder.Descending)
                .SummaryOnly().Include("Patient.managingOrganization")
                .LimitTo(20);
            Assert.AreEqual(1, q.Criteria.Count());
            Assert.AreEqual("ewout", Query.ExtractParamValue(q.Criteria.First()));
        }

        [TestMethod]
        public void CountSetToNullAndGet()
        {
            var q = new Query();
            q.Count = null;
            Assert.IsFalse(q.Count.HasValue);
        }
    }
}
