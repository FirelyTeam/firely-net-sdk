/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

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
