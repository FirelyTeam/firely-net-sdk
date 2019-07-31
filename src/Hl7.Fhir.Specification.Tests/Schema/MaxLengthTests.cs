using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.ElementModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests.Schema
{

    [TestClass]
    public class MaxLengthTests
    {

        [TestMethod]
        public void MyTestMethod()
        {
            var validatable = new MaxLength(10);

            var node = (new Model.FhirString("12345678901")).ToTypedElement();

            var outcome = validatable.Validate(node, null);
        }

    }
}
