using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;
using System;
using System.Linq;
using Xunit;

namespace Hl7.Fhir.Source
{
    public class TerminologyTests : IClassFixture<ValidationFixture>
    {            
        private IResourceResolver _resolver;

        public TerminologyTests(ValidationFixture fixture)
        {
            _resolver = fixture.Resolver;
        }

        [Fact]
        public void ExpansionOfWholeSystem()
        {
            var issueTypeVs = _resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/issue-type").DeepCopy() as ValueSet;
            Assert.False(issueTypeVs.HasExpansion);

            // Wipe the version so we don't have to update our tests all the time
            // issueTypeVs.CodeSystem.Version = null;

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });

            expander.Expand(issueTypeVs);

            Assert.True(issueTypeVs.HasExpansion);
            var id = issueTypeVs.Expansion.Identifier;
            Assert.NotNull(id);

            //TODO: Re-enable test after we fix merging expansiom parameters
            //Assert.False(issueTypeVs.Expansion.Parameter.Any(c => c.Name == "version"));

            Assert.True(issueTypeVs.CodeInExpansion("security", "http://hl7.org/fhir/issue-type"));
            Assert.True(issueTypeVs.CodeInExpansion("expired", "http://hl7.org/fhir/issue-type"));
            Assert.Equal(29, issueTypeVs.Expansion.Contains.CountConcepts());
            Assert.Equal(issueTypeVs.Expansion.Contains.CountConcepts(), issueTypeVs.Expansion.Total);

            var trans = issueTypeVs.FindInExpansion("transient", "http://hl7.org/fhir/issue-type");
            Assert.NotNull(trans);
            Assert.NotNull(trans.FindCode("exception"));

            // Now, make this a versioned system
            issueTypeVs.Version = "3.14";
            expander.Expand(issueTypeVs);
            Assert.NotEqual(id, issueTypeVs.Expansion.Identifier);
            Assert.Equal(29, issueTypeVs.Expansion.Total);

            //var versionParam = issueTypeVs.Expansion.Parameter.Single(c => c.Name == "version");
            //Assert.Equal("http://hl7.org/fhir/ValueSet/issue-type?version=3.14", ((FhirUri)versionParam.Value).Value);
        }


        [Fact]
        public void ExpansionOfComposeInclude()
        {
            var testVs = _resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/marital-status").DeepCopy() as ValueSet;
            Assert.False(testVs.HasExpansion);

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });
            expander.Expand(testVs);
            Assert.Equal(11, testVs.Expansion.Total);
        }


        [Fact]
        public void ExpansionOfComposeImport()
        {
            var testVs = _resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/v3-ObservationMethod").DeepCopy() as ValueSet;
            Assert.False(testVs.HasExpansion);

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });
            expander.Settings.MaxExpansionSize = 50;

            Assert.Throws<ValueSetExpansionTooBigException>( () => expander.Expand(testVs) );

            expander.Settings.MaxExpansionSize = 500;
            expander.Expand(testVs);
            Assert.Equal(304, testVs.Expansion.Total);
        }

        [Fact]
        public void TestPropertyRetrieval()
        {
            var testCs = _resolver.FindCodeSystem("http://hl7.org/fhir/item-type");

            var conceptGroup = testCs.Concept.Single(c=>c.Code == "group");
            var conceptQuestion = testCs.Concept.Single(c => c.Code == "question");

            Assert.False(conceptGroup.ListConceptProperties(testCs, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).Any());
            Assert.True(conceptQuestion.ListConceptProperties(testCs, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).Any());
        }


        private void testService(ITerminologyService svc)
        {
            var vsUrl = "http://hl7.org/fhir/ValueSet/data-absent-reason";
            var result = svc.ValidateCode(vsUrl, code: "NaN", system: "http://hl7.org/fhir/data-absent-reason");
            Assert.True(result.Success);

            result = svc.ValidateCode(vsUrl, code: "NaNX", system: "http://hl7.org/fhir/data-absent-reason");
            Assert.False(result.Success);

            result = svc.ValidateCode(vsUrl, code: "NaN", system: "http://hl7.org/fhir/data-absent-reason", 
                display: "Not a Number");
            Assert.True(result.Success);

            result = svc.ValidateCode(vsUrl, code: "NaN", system: "http://hl7.org/fhir/data-absent-reason", 
                display: "Not any Number");
            Assert.False(result.Success);

            result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/v3-AcknowledgementDetailCode", code: "_AcknowledgementDetailNotSupportedCode",
                system: "http://hl7.org/fhir/v3/AcknowledgementDetailCode");
            Assert.True(result.Success);

            Assert.Throws<ValueSetUnknownException>(() => svc.ValidateCode("http://hl7.org/fhir/ValueSet/crappy", code: "4322002", system: "http://snomed.info/sct"));

            var coding = new Coding("http://hl7.org/fhir/data-absent-reason", "NaN");
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.True(result.Success);

            coding.Display = "Not a Number";
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.True(result.Success);

            coding.Code = "NaNX";
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.False(result.Success);
            coding.Code = "NaN";

            var cc = new CodeableConcept("http://hl7.org/fhir/data-absent-reason", "NaNX", "Not a Number");
            result = svc.ValidateCode(vsUrl, codeableConcept: cc);
            Assert.False(result.Success);

            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "asked"));
            result = svc.ValidateCode(vsUrl, codeableConcept: cc);
            Assert.True(result.Success);
        }

        [Fact]
        public void LocalTermServiceValidateCodeTest()
        {
            var svc = new LocalTerminologyService(_resolver);

            // Do common tests for service
            testService(svc);

            // This is a valueset with a compose - not supported locally normally, but it has been expanded in the zip, so this will work
            var result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/yesnodontknow", code: "Y", system: "http://hl7.org/fhir/v2/0136");
            Assert.True(result.Success);

            // This test is not always correctly done by the external services, so copied here instead
            result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/v3-AcknowledgementDetailCode", code: "_AcknowledgementDetailNotSupportedCode",
                    system: "http://hl7.org/fhir/v3/AcknowledgementDetailCode", @abstract: false);
            Assert.False(result.Success);

            // And one that will specifically fail on the local service, since it's too complex too expand - the local term server won't help you here
            Assert.Throws<ValueSetExpansionTooComplexException>(
                () => svc.ValidateCode("http://hl7.org/fhir/ValueSet/substance-code", code: "1166006", system: "http://snomed.info/sct"));
        }

        [Fact, Trait("TestCategory", "IntegrationTest")]
        public void ExternalServiceValidateCodeTest()
        {
            var client = new FhirClient("http://ontoserver.csiro.au/stu3-latest");
            var svc = new ExternalTerminologyService(client);

            // Do common tests for service
            testService(svc);

            // Any good external service should be able to handle this one
            var result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/substance-code", code: "1166006", system:"http://snomed.info/sct");
            Assert.True(result.Success);
        }

        [Fact, Trait("TestCategory", "IntegrationTest")]
        public void FallbackServiceValidateCodeTest()
        {
            var client = new FhirClient("http://ontoserver.csiro.au/stu3-latest");
            var external = new ExternalTerminologyService(client);
            var local = new LocalTerminologyService(_resolver);
            var svc = new FallbackTerminologyService(local, external);

            testService(svc);

            // Now, this should fall back
            var result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/substance-code", code: "1166006", system: "http://snomed.info/sct");
            Assert.True(result.Success);
        }

        [Fact, Trait("TestCategory", "IntegrationTest")]
        public void FallbackServiceValidateCodeTestWithVS()
        {
            var client = new FhirClient("http://ontoserver.csiro.au/stu3-latest");
            var service = new ExternalTerminologyService(client);
            var vs = _resolver.FindValueSet("http://hl7.org/fhir/ValueSet/substance-code");
            Assert.NotNull(vs);

            // Override the canonical with something the remote server cannot know
            vs.Url = "http://furore.com/fhir/ValueSet/testVS";
            var local = new LocalTerminologyService(new IKnowOnlyMyTestVSResolver(vs));
            var fallback = new FallbackTerminologyService(local, service);

            // Now, this should fall back to external + send our vs (that the server cannot know about)
            var result = fallback.ValidateCode("http://furore.com/fhir/ValueSet/testVS", code: "1166006", system: "http://snomed.info/sct");
            Assert.True(result.Success);
        }

        private class IKnowOnlyMyTestVSResolver : IResourceResolver
        {
            public ValueSet _myOnlyVS;

            public IKnowOnlyMyTestVSResolver(ValueSet vs)
            {
                _myOnlyVS = vs;
            }

            public Resource ResolveByCanonicalUri(string uri) => (uri == _myOnlyVS.Url) ? _myOnlyVS : null;

            public Resource ResolveByUri(string uri) => throw new NotImplementedException();
        }

    }
}

