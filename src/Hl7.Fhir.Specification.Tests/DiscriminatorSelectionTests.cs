using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Specification.Navigation.FhirPath;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class DiscriminatorSelectionTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            var f = new ValidationFixture();

            _source = f.Resolver;
        }

        private static IResourceResolver _source = null;

        [TestMethod,Ignore]
        public void ParseValidDiscriminatorExpressions()
        {
            var patientDef = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var schemas = new StructureDefinitionSchemaWalker(new ElementDefinitionNavigator(patientDef), _source);

            schemas.Walk("active");
            schemas.Walk("active.resolve()");
            schemas.Walk("active.extension('http://somewhere.com')");
            schemas.Walk("active.next.next");
            schemas.Walk("resolve()");
            schemas.Walk("ofType('HumanName')");
            schemas.Walk("slice('someSlice')");
        }

        [TestMethod]
        public void ParseInvalidDiscriminatorExpressions()
        {
            var patientDef = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            var schemas = new StructureDefinitionSchemaWalker(new ElementDefinitionNavigator(patientDef), _source);

            eval("45");
            eval("active.resolve('bla')");
            eval("active.extension()");
            eval("active.extension(33)");
            eval("active.extension('arg1', 'arg2')");
            eval("active.slice()");
            eval("active.ofType()");
            eval("active.where(true)");

            void eval(string expr)
            {
                Assert.ThrowsException<DiscriminatorFormatException>(() => schemas.Walk(expr));
            }
        }

    }

}


   