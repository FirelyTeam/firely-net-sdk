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
    public class QueryExtensionTests
    {
        //[TestMethod]
        //public void ManageSearchResult()
        //{
        //    var q = new Query();
        //    q.OrderBy("adsfadf").LimitPageSizeTo(20);

        //    Assert.AreEqual(1, q.Parameter.Count(ext => ext.Url.ToString().Contains("_sort")));
        //    Assert.AreEqual(1, q.GetParameter("_sort:desc").Count());

        //}

        //[TestMethod]
        //public void ReapplySingleParam()
        //{
        //    var q = new Query();
        //    q.Custom("mySearch").OrderBy("adsfadf").OrderBy("q", Hl7.Fhir.Search.SortOrder.Descending)
        //            .LimitPageSizeTo(10).LimitPageSizeTo(20).Custom("miSearch");

        //    Assert.AreEqual(1, q.Parameter.Count(ext => ext.Url.ToString().Contains("_sort")));
        //    Assert.AreEqual(1, q.GetParameter("_sort:desc").Count());

        //    Assert.AreEqual(1, q.Parameter.Count(ext => ext.Url.ToString().Contains("_count")));
        //    Assert.AreEqual(20, q.GetSingleParameter("_count"));

        //    Assert.AreEqual(1, q.Parameter.Count(ext => ext.Url.ToString().Contains("_query")));
        //    Assert.AreEqual("miSearch", q.GetSingleParameter("_query"));
        //}


       
        
    }
}
