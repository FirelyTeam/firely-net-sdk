/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
	public class SearchParamsTests
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
        public void MinimalParams()
        {
            var q = new SearchParams();

            q.Add("name", "ewout");
            
            // Validate no "Default" search parameters are added except for the ones the user added
            Assert.AreEqual("name=ewout", q.ToUriParamList().ToQueryString());
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

        [TestMethod]
        public void AcceptEmptyGenericParam()
        {
            var q = new SearchParams();
            q.Add("parameter", String.Empty);
            CollectionAssert.AreEquivalent(
                new[] { Tuple.Create("parameter", String.Empty) }, 
                q.Parameters.ToList()
            );
        }

        [TestMethod]
        public void FormatExceptionOnDuplicateOrEmptyQueryParam()
        {
            FormatExceptionOnDuplicateOrEmptyParam("_query");
        }

        [TestMethod]
        public void FormatExceptionOnDuplicateOrEmptyTextParam()
        {
            FormatExceptionOnDuplicateOrEmptyParam("_text");
        }

        [TestMethod]
        public void FormatExceptionOnDuplicateOrEmptyContentParam()
        {
            FormatExceptionOnDuplicateOrEmptyParam("_content");
        }

        [TestMethod]
        public void FormatExceptionOnInvalidCountParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_count", String.Empty));
            Assert.AreEqual("Invalid _count: '' is not a positive integer", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_count", "0"));
            Assert.AreEqual("Invalid _count: '0' is not a positive integer", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_count", "-100"));
            Assert.AreEqual("Invalid _count: '-100' is not a positive integer", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_count", "3.14"));
            Assert.AreEqual("Invalid _count: '3.14' is not a positive integer", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_count", "12zz"));
            Assert.AreEqual("Invalid _count: '12zz' is not a positive integer", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnEmptyIncludeParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_include", String.Empty));
            Assert.AreEqual("Invalid _include value: it cannot be empty", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnEmptyRevIncludeParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_revinclude", String.Empty));
            Assert.AreEqual("Invalid _revinclude value: it cannot be empty", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnInvalidSortParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_sort:", "x"));
            Assert.AreEqual("Invalid _sort: '' is not a recognized sort order", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort:ascz", "x"));
            Assert.AreEqual("Invalid _sort: 'ascz' is not a recognized sort order", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort", String.Empty));
            Assert.AreEqual("Invalid _sort value: it cannot be empty", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort:asc", String.Empty));
            Assert.AreEqual("Invalid _sort value: it cannot be empty", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort:desc", String.Empty));
            Assert.AreEqual("Invalid _sort value: it cannot be empty", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnInvalidSummaryParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_summary", "x"));
            Assert.AreEqual("Invalid _summary: 'x' is not a recognized summary value", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_summary", String.Empty));
            Assert.AreEqual("Invalid _summary: '' is not a recognized summary value", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnDuplicateOrEmptyFilterParam()
        {
            FormatExceptionOnDuplicateOrEmptyParam("_filter");
        }

        [TestMethod]
        public void FormatExceptionOnInvalidContainedParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_contained", "x"));
            Assert.AreEqual("Invalid _contained: 'x' is not a recognized contained value", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_contained", String.Empty));
            Assert.AreEqual("Invalid _contained: '' is not a recognized contained value", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnInvalidContainedTypeParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_containedType", "x"));
            Assert.AreEqual("Invalid _containedType: 'x' is not a recognized containedType value", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_containedType", String.Empty));
            Assert.AreEqual("Invalid _containedType: '' is not a recognized containedType value", formatException.Message);
        }

        [TestMethod]
        public void FormatExceptionOnEmptyElementsParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_elements", String.Empty));
            Assert.AreEqual("Invalid _elements value: it cannot be empty", formatException.Message);
        }

        private void FormatExceptionOnDuplicateOrEmptyParam(string paramName)
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add(paramName, String.Empty));
            Assert.AreEqual(String.Format("Invalid {0} value: it cannot be empty", paramName), formatException.Message);

            q.Add(paramName, "value1");
            formatException = AssertThrows<FormatException>(() => q.Add(paramName, "value2"));
            Assert.AreEqual(String.Format("{0} cannot be specified more than once", paramName), formatException.Message);
        }

        private TException AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                var result = exception as TException;
                if (result == null)
                {
                    Assert.Fail("Expected {0}, actual {1}", typeof(TException), exception.GetType());
                }
                return result;
            }
            Assert.Fail("Should have failed");
            return null;
        }
    }
}
