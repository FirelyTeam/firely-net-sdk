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

namespace Hl7.Fhir.Test.Search
{
    [TestClass]
    public class QueryTests
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
            var vals = paramlist.GetValues("testX");
            Assert.AreEqual(2, vals.Count());
            Assert.AreEqual("someVal", vals.First());
            Assert.AreEqual("someVal2", vals.Skip(1).First());
            
            Assert.AreEqual("someVal3", paramlist.GetSingleValue("testXY"));

            paramlist.Remove("testXY");
            Assert.IsNull(paramlist.GetSingleValue("testXY"));
            Assert.AreEqual(2, paramlist.GetValues("testX").Count());
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
            Assert.AreEqual(Tuple.Create("sorted2", SortOrder.Ascending), q.Sort.Single());
            Assert.AreEqual(3, q.Include.Count);
            Assert.IsTrue(q.Include.Contains("Patient.name2"));
            Assert.IsFalse(q.Include.Contains("Patient.name"));
            Assert.IsTrue(q.Include.Contains("Observation.subject"));
            Assert.IsTrue(q.Include.Contains("Observation.subject2"));
        }
    }
}
