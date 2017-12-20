using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Schema.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class TestSerialization
    {
        public class Ann1
        {
            public string X { get; set; }
        }
        public class Ann2
        {
            public string X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        public void SerializeSchemaAnnotations()
        {
            var subject = new SchemaTags()
            {
                new Ann1 { X = "Jan" },
                new Ann1 { X = "Klaas"},
                new Ann2 { X = "Piet", Y = 4 }
            };

            var result = (subject as IJsonSerializable).ToJson().ToString();
        }
    }
}
