using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests.Schema
{
    [TestClass]
    public class TestSchemaBuilding
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
            converter = new ElementSchemaResolver(source);
        }

        static IConformanceSource source = null;
        static ISchemaResolver converter = null;

        [TestMethod]
        public void BuildSchema()
        {
            var schema = converter.GetSchema(ResourceIdentity.Core(FHIRDefinedType.Patient));
            var json = schema.ToJson();
        }

        [TestMethod]
        public void BuildSchema2()
        {
            var schema = converter.GetSchema(ResourceIdentity.Core(FHIRDefinedType.Uuid));
            var json = schema.ToJson();
        }

    }
}
