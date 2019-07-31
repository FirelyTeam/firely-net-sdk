using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Specification.Source;
using Hl7.Fhir.Validation.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class SchemaConverterTests
    {
        readonly ISchemaResolver _resolver;

        public SchemaConverterTests()
        {
            _resolver = new ElementSchemaResolver(new CachedResolver(new ZipSource("specification.zip")));
        }

        private string BigString()
        {
            var sb = new StringBuilder(1024 * 1024);
            for (int i = 0; i < 1024; i++)
            {
                sb.Append("x");
            }

            var sub = sb.ToString();

            sb = new StringBuilder(1024 * 1024);
            for (int i = 0; i < 1024; i++)
            {
                sb.Append(sub);
            }
            sb.Append("more");
            return sb.ToString();
        }


        [TestMethod]
        public void MyTestMethod()
        {

            var patient = new Patient() { Name = new List<HumanName>() { new HumanName() { Family = BigString() } } };
            var element = patient.ToTypedElement();

            var schemaElement = _resolver.GetSchema(new Uri("http://hl7.org/fhir/StructureDefinition/Patient", UriKind.Absolute));
            var results = schemaElement.Validate(new[] { element }, new ValidationContext());
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(0, results[0].Item1.Count);
            var json = schemaElement.ToJson().ToString();
            json.ToString();

        }
    }
}
