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
            q.Summary = SummaryType.Data;
            q.Sort.Add(Tuple.Create("sorted", SortOrder.Descending));
            q.Include.Add("Patient.name");
            q.Include.Add("Observation.subject");
            q.Elements.Add("field1");

            Assert.AreEqual("special", q.Query);
            Assert.AreEqual(31, q.Count);
            Assert.AreEqual(SummaryType.Data, q.Summary);
            Assert.AreEqual(Tuple.Create("sorted", SortOrder.Descending), q.Sort.Single());
            Assert.AreEqual(2, q.Include.Count);
            Assert.AreEqual("Patient.name", q.Include.First());
            Assert.AreEqual("Observation.subject", q.Include.Skip(1).First());
            Assert.AreEqual(1, q.Elements.Count);
            Assert.AreEqual("field1", q.Elements.First());
            
            q.Query = "special2";
            q.Count = 32;
            q.Summary = SummaryType.True;
            q.Sort.Add(Tuple.Create("sorted2", SortOrder.Ascending));
            q.Include.Add("Patient.name2");
            q.Include.Remove("Patient.name");
            q.Include.Add("Observation.subject2");
            q.Elements.Add("field2");

            Assert.AreEqual("special2", q.Query);
            Assert.AreEqual(32, q.Count);
            Assert.AreEqual(SummaryType.True, q.Summary);
            Assert.AreEqual(2,q.Sort.Count);
            Assert.AreEqual(Tuple.Create("sorted2", SortOrder.Ascending), q.Sort.Skip(1).Single());
            Assert.AreEqual(3, q.Include.Count);
            Assert.IsTrue(q.Include.Contains("Patient.name2"));
            Assert.IsFalse(q.Include.Contains("Patient.name"));
            Assert.IsTrue(q.Include.Contains("Observation.subject"));
            Assert.IsTrue(q.Include.Contains("Observation.subject2"));
            Assert.AreEqual(2, q.Elements.Count);
            Assert.AreEqual("field1", q.Elements.First());
            Assert.AreEqual("field2", q.Elements.Skip(1).First());
        }

        [TestMethod]
        public void ParamOrderHasDefault()
        {
            var q = new SearchParams();

            q.Add("_sort", "birthdate");
            q.Add("_sort:asc", "name");
            q.Add("_sort:desc", "active");
            Assert.AreEqual(3, q.Sort.Count());
            Assert.AreEqual(Tuple.Create("birthdate", SortOrder.Ascending), q.Sort.First());
            Assert.AreEqual(Tuple.Create("name", SortOrder.Ascending), q.Sort.Skip(1).First());
            Assert.AreEqual(Tuple.Create("active", SortOrder.Descending), q.Sort.Skip(2).First());
        }


        [TestMethod]
        public void ManageSearchResult()
        {
            var q = new SearchParams()
               .Where("name:exact=ewout").OrderBy("birthDate", SortOrder.Descending)
                .SummaryOnly().Include("Patient.managingOrganization").Select("field1", "field2")
                .LimitTo(20);

            var parameters = q.ToUriParamList();

            var p = parameters.Single("name");
            Assert.AreEqual("name:exact", p.Item1);
            Assert.AreEqual("ewout", p.Item2);

            var o = q.Sort;
            Assert.AreEqual("birthDate", o.First().Item1);
            Assert.AreEqual(SortOrder.Descending, o.First().Item2);

            Assert.AreEqual("field1", q.Elements.First());
            Assert.AreEqual("field2", q.Elements.Skip(1).First());
            Assert.AreEqual(2, q.Elements.Count);

            Assert.AreEqual(q.Summary, SummaryType.True);
            Assert.IsTrue(q.Include.Contains("Patient.managingOrganization"));
            Assert.AreEqual(20, q.Count);
        }

        [TestMethod]
        public void ReapplySingleParam()
        {
            var q = new SearchParams()
                .Custom("mySearch").OrderBy("adsfadf").OrderBy("q", SortOrder.Descending)
                    .LimitTo(10).LimitTo(20).Custom("miSearch").SummaryOnly().DataOnly();

            Assert.AreEqual("miSearch", q.Query);
            Assert.AreEqual(q.Summary, SummaryType.Data);

            var o = q.Sort;
            Assert.AreEqual("adsfadf", o.First().Item1);
            Assert.AreEqual(SortOrder.Ascending, o.First().Item2);
            Assert.AreEqual("q", o.Skip(1).First().Item1);
            Assert.AreEqual(SortOrder.Descending, o.Skip(1).First().Item2);

            Assert.AreEqual(20, q.Count);
        }

        [TestMethod]
        public void SerializeParams()
        {
            var q = new SearchParams();
            q.Query = "special";
            q.Count = 31;
            q.Summary = SummaryType.Text;
            q.Sort.Add(Tuple.Create("sorted", SortOrder.Descending));
            q.Sort.Add(Tuple.Create("sorted2", SortOrder.Ascending));
            q.Include.Add("Patient.name");
            q.Include.Add("Observation.subject");
            q.Elements.Add("field1");
            q.Elements.Add("field2");

            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include=Observation.subject&_sort%3Adesc=sorted&_sort%3Aasc=sorted2&_summary=text&_elements=field1%2Cfield2", output);
        }

        [TestMethod]
        public void ParseAndSerializeParams()
        {
            var q = new SearchParams();
            q.Add("_query", "special");
            q.Add("_count", "31");
            q.Add("_summary", "data");
            q.Add("_sort:desc", "sorted");
            q.Add("_sort:asc", "sorted2");
            q.Add("_include", "Patient.name");
            q.Add("_include", "Observation.subject");
            q.Add("image:missing", "true");
            q.Add("_elements", "field1,field2");
            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include=Observation.subject&_sort%3Adesc=sorted&_sort%3Aasc=sorted2&_summary=data&_elements=field1%2Cfield2&image%3Amissing=true", output);

            var q2 = SearchParams.FromUriParamList(UriParamList.FromQueryString(output));

            Assert.AreEqual(q.Query, q2.Query);
            Assert.AreEqual(q.Count, q2.Count);
            Assert.AreEqual(q.Summary, q2.Summary);
            
            CollectionAssert.AreEquivalent(q.Sort.ToList(), q2.Sort.ToList());
            CollectionAssert.AreEquivalent(q.Include.ToList(), q2.Include.ToList());
            CollectionAssert.AreEquivalent(q.Parameters.ToList(), q2.Parameters.ToList());
            CollectionAssert.AreEquivalent(q.Elements.ToList(), q2.Elements.ToList());
        }
    }
}
