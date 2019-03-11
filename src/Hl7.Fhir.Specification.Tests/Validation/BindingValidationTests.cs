﻿using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Model;
using System.Linq;
using Xunit;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class BindingValidationTests : IClassFixture<ValidationFixture>
    {
        private readonly IResourceResolver _resolver;
        private readonly ITerminologyService _termService;
        private Validator _validator;

        public BindingValidationTests(ValidationFixture fixture)
        {
            _resolver = fixture.Resolver;
            _validator = fixture.Validator;
            _termService = new LocalTerminologyService(_resolver);
        }

        [Fact]
        public void TestValueValidation()
        {
            var ed = new ElementDefinition
            {
                Binding = new ElementDefinition.ElementDefinitionBindingComponent
                {
                    Strength = BindingStrength.Required,
                    ValueSet = "http://hl7.org/fhir/ValueSet/data-absent-reason"
                }
            };

            // Non-bindeable things should succeed
            Element v = new FhirBoolean(true);
            var node = v.ToTypedElement();            
            Assert.True(_validator.ValidateBinding(ed, node).Success);

            v = new Quantity(4.0m, "masked", "http://terminology.hl7.org/CodeSystem/data-absent-reason");  // nonsense, but hey UCUM is not provided with the spec
            node = v.ToTypedElement();
            Assert.True(_validator.ValidateBinding(ed, node).Success);

            v = new Quantity(4.0m, "maskedx", "http://terminology.hl7.org/CodeSystem/data-absent-reason");  // nonsense, but hey UCUM is not provided with the spec
            node = v.ToTypedElement();
            Assert.False(_validator.ValidateBinding(ed,node).Success);

            v = new Quantity(4.0m, "kg");  // sorry, UCUM is not provided with the spec - still validate against data-absent-reason
            node = v.ToTypedElement();
            Assert.False(_validator.ValidateBinding(ed,node).Success);

            v = new FhirString("masked");
            node = v.ToTypedElement();
            Assert.True(_validator.ValidateBinding(ed,node).Success);

            v = new FhirString("maskedx");
            node = v.ToTypedElement();
            Assert.False(_validator.ValidateBinding(ed,node).Success);

            var ic = new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "masked");
            var ext = new Extension { Value = ic };
            node = ext.ToTypedElement();
            Assert.True(_validator.ValidateBinding(ed, node).Success);

            ic.Code = "maskedx";
            node = ext.ToTypedElement();
            Assert.False(_validator.ValidateBinding(ed, node).Success);
        }


        [Fact]
        public void TestCodingValidation()
        {
            var val = new BindingValidator(_termService, "Demo");
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = "http://hl7.org/fhir/ValueSet/data-absent-reason",
                Strength = BindingStrength.Required
            };

            var c = new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "not-a-number");
            var result = val.ValidateBinding(c, binding);
            Assert.True(result.Success);

            c.Code = "NaNX";
            result = val.ValidateBinding(c, binding);
            Assert.False(result.Success);

            c.Code = "not-a-number";
            binding.Strength = null;
            result = val.ValidateBinding(c, binding);
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);  // missing binding strength

            c.Display = "Not a Number (NaN)";
            binding.Strength = BindingStrength.Required;
            result = val.ValidateBinding(c, binding);
            Assert.True(result.Success);

            c.Display = "Not a NumberX";
            result = val.ValidateBinding(c, binding);
            Assert.True(result.Success);        // local terminology service treats incorrect displays as warnings (GH#624)

            // But this won't, it's also a composition, but without expansion - the local term server won't help you here
            var binding2 = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = "http://hl7.org/fhir/ValueSet/substance-code",
                Strength = BindingStrength.Required
            };

            c = new Coding("http://snomed.info/sct", "160244002");
            result = val.ValidateBinding(c, binding2);
            Assert.True(result.Success);
            Assert.NotEmpty(result.Where(type: OperationOutcome.IssueType.NotSupported));
        }

        [Fact]
        public void TestEmptyIllegalAndLegalCode()
        {
            var val = new BindingValidator(_termService, "Demo");

            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = "http://hl7.org/fhir/ValueSet/data-absent-reason",
                Strength = BindingStrength.Required
            };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding(null,null,"Just some display text"));

            // First, with no code at all in a CC
            var result = val.ValidateBinding(cc, binding);
            Assert.False(result.Success);
            Assert.Contains("No code found in instance", result.ToString());

            // Now with no code + illegal code
            cc.Coding.Add(new Coding("urn:oid:1.2.3.4.5", "16", "Here's a code"));
            result = val.ValidateBinding(cc, binding);
            Assert.False(result.Success);
            Assert.Contains("None of the Codings in the CodeableConcept were valid for the binding", result.ToString());

            // Now, add a third valid code according to the binding.
            cc.Coding.Add(new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "asked-unknown"));
            result = val.ValidateBinding(cc, binding);
            Assert.True(result.Success);
        }

        [Fact]
        public void TestCodeableConceptValidation()
        {
            var val = new BindingValidator(_termService, "Demo");

            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = "http://hl7.org/fhir/ValueSet/data-absent-reason",
                Strength = BindingStrength.Required

            };

            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "not-a-number"));
            cc.Coding.Add(new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "not-asked"));

            var result = val.ValidateBinding(cc, binding);
            Assert.True(result.Success);

            cc.Coding.First().Code = "NaNX";
            result = val.ValidateBinding(cc, binding);
            Assert.True(result.Success);

            cc.Coding.Skip(1).First().Code = "did-ask";
            result = val.ValidateBinding(cc, binding);
            Assert.False(result.Success);

            //EK 2017-07-6 No longer reports warnings when failing a preferred binding
            binding.Strength = BindingStrength.Preferred;
            result = val.ValidateBinding(cc, binding);
            Assert.True(result.Success);
            Assert.Equal(0, result.Warnings);
        }      
    }
}
