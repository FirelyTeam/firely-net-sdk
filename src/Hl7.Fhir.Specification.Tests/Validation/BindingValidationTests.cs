using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System.Linq;
using Xunit;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class BindingValidationTests : IClassFixture<ValidationFixture>
    {
        private readonly IAsyncResourceResolver _resolver;
        private readonly ITerminologyService _termService;

        public BindingValidationTests(ValidationFixture fixture)
        {
            _resolver = fixture.AsyncResolver;
            _termService = new LocalTerminologyService(_resolver);
        }

        [Fact]
        public void TestValueValidation()
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                Strength = BindingStrength.Required,
                ValueSet = new ResourceReference("http://hl7.org/fhir/ValueSet/data-absent-reason")
            };

            var validator = binding.ToValidatable("http://example.org/fhir/StructureDefitition/fhir#text.path");
            var vc = new ValidationContext() { TerminologyService = _termService };
            // Non-bindeable things should succeed
            Element v = new FhirBoolean(true);
            var node = v.ToTypedElement();
            Assert.True(validator.Validate(node, vc).Success);

            v = new Quantity(4.0m, "masked", "http://hl7.org/fhir/data-absent-reason");  // nonsense, but hey UCUM is not provided with the spec
            node = v.ToTypedElement();
            Assert.True(validator.Validate(node, vc).Success);

            v = new Quantity(4.0m, "maskedx", "http://hl7.org/fhir/data-absent-reason");  // nonsense, but hey UCUM is not provided with the spec
            node = v.ToTypedElement();
            Assert.False(validator.Validate(node, vc).Success);

            v = new Quantity(4.0m, "kg");  // sorry, UCUM is not provided with the spec - still validate against data-absent-reason
            node = v.ToTypedElement();
            Assert.False(validator.Validate(node, vc).Success);

            v = new FhirString("masked");
            node = v.ToTypedElement();
            Assert.True(validator.Validate(node, vc).Success);

            v = new FhirString("maskedx");
            node = v.ToTypedElement();
            Assert.False(validator.Validate(node, vc).Success);

            var ic = new Coding("http://hl7.org/fhir/data-absent-reason", "masked");
            var ext = new Extension { Value = ic };
            node = ext.ToTypedElement();
            Assert.True(validator.Validate(node, vc).Success);

            ic.Code = "maskedx";
            node = ext.ToTypedElement();
            Assert.False(validator.Validate(node, vc).Success);
        }


        [Fact]
        public void TestCodingValidation()
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new ResourceReference("http://hl7.org/fhir/ValueSet/data-absent-reason"),
                Strength = BindingStrength.Required
            };

            var val = binding.ToValidatable();
            var vc = new ValidationContext() { TerminologyService = _termService };

            var c = new Coding("http://hl7.org/fhir/data-absent-reason", "NaN");
            var result = val.Validate(c.ToTypedElement(), vc);
            Assert.True(result.Success);

            c.Code = "NaNX";
            result = val.Validate(c.ToTypedElement(), vc);
            Assert.False(result.Success);
            c.Code = "NaN";

            c.Display = "Not a Number";
            binding.Strength = BindingStrength.Required;
            result = val.Validate(c.ToTypedElement(), vc);
            Assert.True(result.Success);

            c.Display = "Not a NumberX";
            result = val.Validate(c.ToTypedElement(), vc);
            Assert.True(result.Success);        // local terminology service treats incorrect displays as warnings (GH#624)

            // But this won't, it's also a composition, but without expansion - the local term server won't help you here
            var binding2 = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new FhirUri("http://hl7.org/fhir/ValueSet/substance-code"),
                Strength = BindingStrength.Required
            };

            var val2 = binding2.ToValidatable();

            c = new Coding("http://snomed.info/sct", "160244002");
            result = val2.Validate(c.ToTypedElement(), vc);
            Assert.True(result.Success);
            Assert.NotEmpty(result.Where(type: OperationOutcome.IssueType.NotSupported));
        }

        [Fact]
        public void TestEmptyIllegalAndLegalCode()
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new ResourceReference("http://hl7.org/fhir/ValueSet/data-absent-reason"),
                Strength = BindingStrength.Preferred
            };

            var val = binding.ToValidatable();
            var vc = new ValidationContext() { TerminologyService = _termService };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding(null, null, "Just some display text"));

            // First, no code at all should be ok with a preferred binding
            var result = val.Validate(cc.ToTypedElement(), vc);
            Assert.True(result.Success);

            // Now, switch to a required binding
            binding.Strength = BindingStrength.Required;
            val = binding.ToValidatable();

            // Then, with no code at all in a CC with a required binding
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.False(result.Success);
            Assert.Contains("No code found in", result.ToString());

            // Now with no code + illegal code
            cc.Coding.Add(new Coding("urn:oid:1.2.3.4.5", "16", "Here's a code"));
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.False(result.Success);
            Assert.Contains("None of the Codings in the CodeableConcept were valid for the binding", result.ToString());

            // Now, add a third valid code according to the binding.
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "asked"));
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.True(result.Success);
        }


        [Fact]
        public void TestCodeableConceptValidation()
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new ResourceReference("http://hl7.org/fhir/ValueSet/data-absent-reason"),
                Strength = BindingStrength.Required
            };

            var val = binding.ToValidatable();
            var vc = new ValidationContext() { TerminologyService = _termService };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "NaN"));
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "not-asked"));

            var result = val.Validate(cc.ToTypedElement(), vc);
            Assert.True(result.Success);

            cc.Coding.First().Code = "NaNX";
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.True(result.Success);

            cc.Coding.Skip(1).First().Code = "did-ask";
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.False(result.Success);

            //EK 2017-07-6 No longer reports warnings when failing a preferred binding
            binding.Strength = BindingStrength.Preferred;
            var val2 = binding.ToValidatable();
            result = val2.Validate(cc.ToTypedElement(), vc);
            Assert.True(result.Success);
            Assert.Equal(0, result.Warnings);
        }

        [Fact]
        public void TestValidationErrorMessageForCodings()
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new ResourceReference("http://hl7.org/fhir/ValueSet/address-type"),
                Strength = BindingStrength.Required
            };

            var val = binding.ToValidatable();
            var vc = new ValidationContext() { TerminologyService = _termService };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://non-existing.code.system", "01.015"));
          
            var result = val.Validate(cc.ToTypedElement(), vc);
            Assert.False(result.Success);
            Assert.True(result.Issue.Count == 1);
            Assert.StartsWith("Code '01.015' from system 'http://non-existing.code.system' does not exist in valueset 'http://hl7.org/fhir/ValueSet/address-type'", result.Issue[0].Details.Text);

            cc.Coding.Add(new Coding("http://another-non-existing.code.system", "01.016"));
            result = val.Validate(cc.ToTypedElement(), vc);
            Assert.False(result.Success);
            Assert.True(result.Issue.Count == 1);
            Assert.StartsWith("None of the Codings in the CodeableConcept were valid for the binding. Details follow.\r\n" +
                              "Code '01.015' from system 'http://non-existing.code.system' does not exist in valueset 'http://hl7.org/fhir/ValueSet/address-type'\r\n\r\n" +
                              "Code '01.016' from system 'http://another-non-existing.code.system' does not exist in valueset 'http://hl7.org/fhir/ValueSet/address-type'",
                              result.Issue[0].Details.Text);

        }
    }
}
