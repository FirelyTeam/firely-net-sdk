using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void AddParameter()
        {
            var q = new Query();
     
            q.AddParameter("testX", "someVal");
            q.AddParameter("testX", "someVal2");
            q.AddParameter("testY", "someVal3");

            var vals = q.GetValues("testX");
            Assert.AreEqual(2, vals.Count());
            Assert.AreEqual("someVal", vals.First());
            Assert.AreEqual("someVal2", vals.Skip(1).First());
            Assert.AreEqual("someVal3", q.GetSingleValue("testY"));

            q.RemoveParameter("testY");
            Assert.IsNull(q.GetSingleValue("testY"));
        }


        [TestMethod]
        public void TestProperties()
        {
            var q = new Query();

            q.QueryName = "special";
            q.Count = 31;
            q.Summary = true;
            q.Sort = Tuple.Create("sorted", SortOrder.Descending);
            q.Include.Add("Patient.name");
            q.Include.Add("Observation.subject");

            Assert.AreEqual("special", q.QueryName);
            Assert.AreEqual(31, q.Count);
            Assert.AreEqual(true, q.Summary);
            Assert.AreEqual(Tuple.Create("sorted", SortOrder.Descending), q.Sort);
            Assert.AreEqual(2, q.Include.Count);
            Assert.AreEqual("Patient.name", q.Include.First());
            Assert.AreEqual("Observation.subject", q.Include.Skip(1).First());

            q.QueryName = "special2";
            q.Count = 32;
            q.Summary = false;
            q.Sort = Tuple.Create("sorted2", SortOrder.Ascending);
            q.Include.Add("Patient.name2");
            q.Include.Remove("Patient.name");
            q.Include.Add("Observation.subject2");

            Assert.AreEqual("special2", q.QueryName);
            Assert.AreEqual(32, q.Count);
            Assert.AreEqual(false, q.Summary);
            Assert.AreEqual(Tuple.Create("sorted2", SortOrder.Ascending), q.Sort);
            Assert.AreEqual(3, q.Include.Count);
            Assert.IsTrue(q.Include.Contains("Patient.name2"));
            Assert.IsFalse(q.Include.Contains("Patient.name"));
            Assert.IsTrue(q.Include.Contains("Observation.subject"));
            Assert.IsTrue(q.Include.Contains("Observation.subject2"));           
        }
    }
}
