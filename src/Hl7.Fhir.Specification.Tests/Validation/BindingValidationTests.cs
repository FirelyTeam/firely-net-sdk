using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Model;
using System.Linq;
using Xunit;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Validation
{
    [Trait("Category", "Validation")]
    public class BindingValidationTests : IClassFixture<ValidationFixture>
    {
        private IResourceResolver _resolver;
        private ITerminologyService _termService;

        public BindingValidationTests(ValidationFixture fixture)
        {
            _resolver = fixture.Resolver;

            _termService = new LocalTerminologyServer(_resolver);

        }

        [Fact]
        public void TestCodingValidation()
        {
            var val = new BindingValidator(_termService, "Demo");
            var vsUri = "http://hl7.org/fhir/ValueSet/data-absent-reason";

            var c = new Coding("http://hl7.org/fhir/data-absent-reason", "NaN");
            var result = val.ValidateBinding(c, vsUri, BindingStrength.Required);
            Assert.True(result.Success);

            c.Code = "NaNX";
            result = val.ValidateBinding(c, vsUri, BindingStrength.Required);
            Assert.False(result.Success);

            result = val.ValidateBinding(c, vsUri);
            Assert.True(result.Success);

            c.Code = "NaN";
            c.Display = "Not a Number";
            result = val.ValidateBinding(c, vsUri, BindingStrength.Required);
            Assert.True(result.Success);

            c.Display = "Not a NumberX";
            result = val.ValidateBinding(c, vsUri, BindingStrength.Required);
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);   // Incorrect display
        }

        [Fact]
        public void TestCodeableConceptValidation()
        {
            var val = new BindingValidator(_termService, "Demo");
            var vsUri = "http://hl7.org/fhir/ValueSet/data-absent-reason";

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "NaN"));
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "not-asked"));

            var result = val.ValidateBinding(cc, vsUri, BindingStrength.Required);
            Assert.True(result.Success);

            cc.Coding.First().Code = "NaNX";
            result = val.ValidateBinding(cc, vsUri, BindingStrength.Required);
            Assert.True(result.Success);

            cc.Coding.Skip(1).First().Code = "did-ask";
            result = val.ValidateBinding(cc, vsUri, BindingStrength.Required);
            Assert.False(result.Success);

            result = val.ValidateBinding(cc, vsUri);
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);
        }      
    }
}
