/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
        public void CheckAllSearchFhirPathExpressions()
        {
            int errorsFound = 0;
            foreach (var item in ModelInfo.SearchParameters)
            {
                string expression = item.Expression;
                if (string.IsNullOrEmpty(expression))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Search parameter {0}.{1} ({2}) has no expression",
                        item.Resource, item.Name, item.Type.ToString()));
                    continue;
                }
                if (expression.Contains(" or "))
                {
                    System.Diagnostics.Trace.WriteLine(String.Format("Search parameter {0}.{1} ({2}) should not contain an 'or' statement",
                        item.Resource, item.Name, item.Type.ToString()));
                    System.Diagnostics.Trace.WriteLine("\t" + item.Expression);
                    errorsFound++;
                }
            }
            Assert.AreEqual(0, errorsFound, "Invalid FhirPath expression in search parameters");
        }

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
            q.Sort.Add(("sorted", SortOrder.Descending));
            q.Include.Add(("Patient.name", IncludeModifier.None));
            q.Include.Add(("Observation.subject", IncludeModifier.Recurse));
            q.Elements.Add("field1");

            Assert.AreEqual("special", q.Query);
            Assert.AreEqual(31, q.Count);
            Assert.AreEqual(SummaryType.Data, q.Summary);
            Assert.AreEqual(("sorted", SortOrder.Descending), q.Sort.Single());
            Assert.AreEqual(2, q.Include.Count);
            Assert.AreEqual(("Patient.name", IncludeModifier.None), q.Include.First());
            Assert.AreEqual(("Observation.subject", IncludeModifier.Recurse), q.Include.Skip(1).First());
            Assert.AreEqual(1, q.Elements.Count);
            Assert.AreEqual("field1", q.Elements.First());
            
            q.Query = "special2";
            q.Count = 32;
            q.Summary = SummaryType.True;
            q.Sort.Add(("sorted2", SortOrder.Ascending));
            q.Include.Add(("Patient.name2", IncludeModifier.None));
            q.Include.Remove(("Patient.name", IncludeModifier.None));
            q.Include.Add(("Observation.subject2", IncludeModifier.Iterate));
            q.Elements.Add("field2");

            Assert.AreEqual("special2", q.Query);
            Assert.AreEqual(32, q.Count);
            Assert.AreEqual(SummaryType.True, q.Summary);
            Assert.AreEqual(2,q.Sort.Count);
            Assert.AreEqual(("sorted2", SortOrder.Ascending), q.Sort.Skip(1).Single());
            Assert.AreEqual(3, q.Include.Count);
            Assert.IsTrue(q.Include.Contains(("Patient.name2", IncludeModifier.None)));
            Assert.IsFalse(q.Include.Contains(("Patient.name", IncludeModifier.None)));
            Assert.IsTrue(q.Include.Contains(("Observation.subject", IncludeModifier.Recurse)));
            Assert.IsTrue(q.Include.Contains(("Observation.subject2", IncludeModifier.Iterate)));
            Assert.AreEqual(2, q.Elements.Count);
            Assert.AreEqual("field1", q.Elements.First());
            Assert.AreEqual("field2", q.Elements.Skip(1).First());
        }

        [TestMethod]
        public void ParamOrderHasDefault()
        {
            var q = new SearchParams();

            q.Add("_sort","birthdate,name,-active");
            Assert.AreEqual(3, q.Sort.Count());
            Assert.AreEqual(("birthdate", SortOrder.Ascending), q.Sort.First());
            Assert.AreEqual(("name", SortOrder.Ascending), q.Sort.Skip(1).First());
            Assert.AreEqual(("active", SortOrder.Descending), q.Sort.Skip(2).First());
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
            Assert.IsTrue(q.Include.Contains(("Patient.managingOrganization", IncludeModifier.None)));
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
            q.Sort.Add(("sorted", SortOrder.Descending));
            q.Sort.Add(("sorted2", SortOrder.Ascending));
            q.Include.Add(("Patient.name", IncludeModifier.None));
            q.Include.Add(("Observation.subject", IncludeModifier.None));
            q.Elements.Add("field1");
            q.Elements.Add("field2");

            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include=Observation.subject&_sort=-sorted%2Csorted2&_summary=text&_elements=field1%2Cfield2", output);
        }

        [TestMethod]
        public void ParseAndSerializeParams()
        {
            var q = new SearchParams();
            q.Add("_query", "special");
            q.Add("_count", "31");
            q.Add("_summary", "data");
            q.Add("_sort", "-sorted,sorted2");
            q.Add("_include", "Patient.name");
            q.Add("_include:iterate", "Observation.subject");
            q.Add("image:missing", "true");
            q.Add("_elements", "field1,field2");
            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_query=special&_count=31&_include=Patient.name&_include%3Aiterate=Observation.subject&_sort=-sorted%2Csorted2&_summary=data&_elements=field1%2Cfield2&image%3Amissing=true", output);

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
        public void ParseAndSerializeSortParams()
        {
            var q = new SearchParams();
      
            q.Add("_sort", "-sorted,sorted2,_lastUpdated");
      
            var output = q.ToUriParamList().ToQueryString();
            Assert.AreEqual("_sort=-sorted%2Csorted2%2C_lastUpdated", output);

            var q2 = SearchParams.FromUriParamList(UriParamList.FromQueryString(output));
            Assert.AreEqual(q.Query, q2.Query);     
            CollectionAssert.AreEquivalent(q.Sort.ToList(), q2.Sort.ToList()); 
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
        public void AddConstructorSearchParams()
        {
            var q =  new SearchParams("_id", "123");
            Assert.AreEqual(new Tuple<string, string>("_id", "123"), q.Parameters.FirstOrDefault()); 
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

            // Make sure we no longer accept DSTU2-style _sort
            var formatException = AssertThrows<FormatException>(() => q.Add("_sort:desc", "x"));
            Assert.AreEqual("Invalid _sort: encountered DSTU2 (modifier) based sort, please change to newer format", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort", ",x,"));
            Assert.AreEqual("Invalid _sort: must be a list of non-empty element names", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort", "a,,b"));
            Assert.AreEqual("Invalid _sort: must be a list of non-empty element names", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort", "+x"));
            Assert.AreEqual("Invalid _sort: must be a list of element names, optionally prefixed with '-'", formatException.Message);

            formatException = AssertThrows<FormatException>(() => q.Add("_sort", String.Empty));
            Assert.AreEqual("Invalid _sort: value cannot be empty", formatException.Message);
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

        [TestMethod]
        public void FormatExceptionOnSingleDashSortParam()
        {
            var q = new SearchParams();
            var formatException = AssertThrows<FormatException>(() => q.Add("_sort", "-"));
            Assert.AreEqual("Invalid _sort: one of the values is just a single '-', an element name must be provided", formatException.Message);            
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

        [TestMethod]
        public void CheckManualFixesOfTemplateModelInfo()
        {
            //Manualy removed target of EpisodeOfCare from searchparameter DiagnosticReport.encounter
            //Commit: 7a61694eb476619b65387341644c83200ef4d3dd
            var sp = ModelInfo.SearchParameters.Where(s => s.Resource == "DiagnosticReport" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp);
            Assert.IsTrue(sp.Path.Contains("DiagnosticReport.context"));           
            Assert.IsFalse(sp.Target.Contains(ResourceType.EpisodeOfCare));

            //Manualy removed this target from more occurances of the same searchparameter
            //Commit: 3b071d478ff3cb744cb6668ac8512dc7362e6737
            var sp2 = ModelInfo.SearchParameters.Where(s => s.Resource == "DocumentReference" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp2);
            Assert.IsFalse(sp2.Target.Contains(ResourceType.EpisodeOfCare));
           
            var sp3 = ModelInfo.SearchParameters.Where(s => s.Resource == "RiskAssessment" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp3);
            Assert.IsFalse(sp3.Target.Contains(ResourceType.EpisodeOfCare));
        
            var sp4 = ModelInfo.SearchParameters.Where(s => s.Resource == "List" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp4);
            Assert.IsFalse(sp4.Target.Contains(ResourceType.EpisodeOfCare));

            var sp5 = ModelInfo.SearchParameters.Where(s => s.Resource == "VisionPrescription" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp5);
            Assert.IsFalse(sp5.Target.Contains(ResourceType.EpisodeOfCare));

            var sp6 = ModelInfo.SearchParameters.Where(s => s.Resource == "ProcedureRequest" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp6);
            Assert.IsFalse(sp6.Target.Contains(ResourceType.EpisodeOfCare));

            var sp7 = ModelInfo.SearchParameters.Where(s => s.Resource == "Flag" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp7);
            Assert.IsFalse(sp7.Target.Contains(ResourceType.EpisodeOfCare));

            var sp8 = ModelInfo.SearchParameters.Where(s => s.Resource == "Observation" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp8);
            Assert.IsFalse(sp8.Target.Contains(ResourceType.EpisodeOfCare));

            var sp9 = ModelInfo.SearchParameters.Where(s => s.Resource == "NutritionOrder" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp9);
            Assert.IsFalse(sp9.Target.Contains(ResourceType.EpisodeOfCare));

            var sp10 = ModelInfo.SearchParameters.Where(s => s.Resource == "Composition" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp10);
            Assert.IsFalse(sp10.Target.Contains(ResourceType.EpisodeOfCare));

            //These occurances of the searchparameter do have the EpisodeOfCare as target
            var sp11 = ModelInfo.SearchParameters.Where(s => s.Resource == "DeviceRequest" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp11);
            Assert.IsTrue(sp11.Target.Contains(ResourceType.EpisodeOfCare));

            var sp12 = ModelInfo.SearchParameters.Where(s => s.Resource == "Procedure" && s.Name == "encounter").FirstOrDefault();
            Assert.IsNotNull(sp12);
            Assert.IsTrue(sp12.Target.Contains(ResourceType.EpisodeOfCare));

        }
    }
}
