using Hl7.Fhir.Model;
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
        }

        static IConformanceSource source = null;

        [TestMethod]
        public void BuildSchema()
        {
            var patDef = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var nav = new ElementDefinitionNavigator(patDef);
            nav.MoveToFirstChild();

            var schema = nav.GetSchema();
            var json = schema.ToJson();
        }

        [TestMethod]
        public void BuildSchema2()
        {
            var patDef = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Uuid);
            var nav = new ElementDefinitionNavigator(patDef);
            nav.MoveToFirstChild();

            var schema = nav.GetSchema();
            var json = schema.ToJson();
        }

    }
}
