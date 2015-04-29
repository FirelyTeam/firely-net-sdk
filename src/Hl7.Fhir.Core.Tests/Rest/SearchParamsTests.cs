/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Test.Rest
{
    [TestClass]
#if PORTABLE45
	public class PortableSearchParamsTests
#else
	public class SearchParamsTests
#endif    
    {
        [TestMethod]
        public void CountSetToNullAndGet()
        {
            var q = new SearchParams();
            q.Count = null;
            Assert.IsFalse(q.Count.HasValue);
        }


        [TestMethod]
        public void ManipulateParameters()
        {
            var q = new SearchParams();

            q.Add("testX", "someVal");
            q.Add("testX", "someVal2");
            q.Add("testXY", "someVal3");

            var paramlist = q.ToUriParamList();
            var vals = paramlist.Values("testX");
            Assert.AreEqual(2, vals.Count());
            Assert.AreEqual("someVal", vals.First());
            Assert.AreEqual("someVal2", vals.Skip(1).First());
            
            Assert.AreEqual("someVal3", paramlist.SingleValue("testXY"));

            paramlist.Remove("testXY");
            Assert.IsNull(paramlist.SingleValue("testXY"));
            Assert.AreEqual(2, paramlist.Values("testX").Count());
        }

        [TestMethod]
        public void TestProperties()
        {
            var q = new SearchParams();
            q.Query = "special";
            q.Count = 31;
            q.Summary = true;
            q.Sort.Add(Tuple.Create("sorted", SortOrder.Descending));
            q.Include.Add("Patient.name");
            q.Include.Add("Observation.subject");

            Assert.AreEqual("special", q.Query);
            Assert.AreEqual(31, q.Count);
            Assert.AreEqual(true, q.Summary);
            Assert.AreEqual(Tuple.Create("sorted", SortOrder.Descending), q.Sort.Single());
            Assert.AreEqual(2, q.Include.Count);
            Assert.AreEqual("Patient.name", q.Include.First());
            Assert.AreEqual("Observation.subject", q.Include.Skip(1).First());

            q.Query = "special2";
            q.Count = 32;
            q.Summary = false;
            q.Sort.Add(Tuple.Create("sorted2", SortOrder.Ascending));
            q.Include.Add("Patient.name2");
            q.Include.Remove("Patient.name");
            q.Include.Add("Observation.subject2");

            Assert.AreEqual("special2", q.Query);
            Assert.AreEqual(32, q.Count);
            Assert.AreEqual(false, q.Summary);
            Assert.AreEqual(2,q.Sort.Count);
            Assert.AreEqual(Tuple.Create("sorted2", SortOrder.Ascending), q.Sort.Skip(1).Single());
            Assert.AreEqual(3, q.Include.Count);
            Assert.IsTrue(q.Include.Contains("Patient.name2"));
            Assert.IsFalse(q.Include.Contains("Patient.name"));
            Assert.IsTrue(q.Include.Contains("Observation.subject"));
            Assert.IsTrue(q.Include.Contains("Observation.subject2"));
        }

        [TestMethod]
        public void ManageSearchResult()
        {
            var q = new SearchParams()
               .Where("name:exact=ewout").OrderBy("birthDate", SortOrder.Descending)
                .SummaryOnly().Include("Patient.managingOrganization")
                .LimitTo(20);

            var parameters = q.ToUriParamList();

            var p = parameters.Single("name");
            Assert.AreEqual("name:exact", p.Item1);
            Assert.AreEqual("ewout", p.Item2);

            var o = q.Sort;
            Assert.AreEqual("birthDate", o.First().Item1);
            Assert.AreEqual(SortOrder.Descending, o.First().Item2);

            Assert.IsTrue(q.Summary.Value);
            Assert.IsTrue(q.Include.Contains("Patient.managingOrganization"));
            Assert.AreEqual(20, q.Count);
        }

        [TestMethod]
        public void ReapplySingleParam()
        {
            var q = new SearchParams()
                .Custom("mySearch").OrderBy("adsfadf").OrderBy("q", SortOrder.Descending)
                    .LimitTo(10).LimitTo(20).Custom("miSearch").SummaryOnly().SummaryOnly(false);

            Assert.AreEqual("miSearch", q.Query);
            Assert.IsFalse(q.Summary.Value);

            var o = q.Sort;
            Assert.AreEqual("adsfadf", o.First().Item1);
            Assert.AreEqual(SortOrder.Ascending, o.First().Item2);
            Assert.AreEqual("q", o.Skip(1).First().Item1);
            Assert.AreEqual(SortOrder.Descending, o.Skip(1).First().Item2);

            Assert.AreEqual(20, q.Count);

            Assert.IsFalse(q.Summary.Value);
        }

        [TestMethod]
        public void SerializeParams()
        {
            var q = new SearchParams();
            q.Query = "special";
            q.Count = 31;
            q.Summary = true;
            q.Sort.Add(Tuple.Create("sorted", SortOrder.Descending));
            q.Sort.Add(Tuple.Create("sorted2", SortOrder.Ascending));
            q.Include.Add("Patient.name");
            q.Include.Add("Observation.subject");

            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include=Observation.subject&_sort%3Adesc=sorted&_sort%3Aasc=sorted2&_summary=true", output);
        }

        [TestMethod]
        public void ParseAndSerializeParams()
        {
            var q = new SearchParams();
            q.Add("_query", "special");
            q.Add("_count", "31");
            q.Add("_summary", "true");
            q.Add("_sort:desc", "sorted");
            q.Add("_sort:asc", "sorted2");
            q.Add("_include", "Patient.name");
            q.Add("_include", "Observation.subject");

            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include=Observation.subject&_sort%3Adesc=sorted&_sort%3Aasc=sorted2&_summary=true", output);
        }
    }
}
