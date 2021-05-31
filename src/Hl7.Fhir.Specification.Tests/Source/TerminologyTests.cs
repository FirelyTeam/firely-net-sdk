using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    public class TerminologyTests : IClassFixture<ValidationFixture>
    {
        private readonly IAsyncResourceResolver _resolver;
        private static Uri _externalTerminologyServerEndpoint = new("https://ontoserver.csiro.au/stu3-latest");

        public TerminologyTests(ValidationFixture fixture, Xunit.Abstractions.ITestOutputHelper _)
        {
            _resolver = fixture.AsyncResolver;
        }

        [Fact]
        public async T.Task ExpansionOfWholeSystem()
        {
            var issueTypeVs = (await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/issue-type")).DeepCopy() as ValueSet;
            Assert.False(issueTypeVs.HasExpansion);

            // Wipe the version so we don't have to update our tests all the time
            // issueTypeVs.CodeSystem.Version = null;

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });

            await expander.ExpandAsync(issueTypeVs);

            Assert.True(issueTypeVs.HasExpansion);
            var id = issueTypeVs.Expansion.Identifier;
            Assert.NotNull(id);

            //TODO: Re-enable test after we fix merging expansiom parameters
            //Assert.False(issueTypeVs.Expansion.Parameter.Any(c => c.Name == "version"));

            Assert.True(issueTypeVs.CodeInExpansion("security", "http://hl7.org/fhir/issue-type"));
            Assert.True(issueTypeVs.CodeInExpansion("expired", "http://hl7.org/fhir/issue-type"));
            Assert.Equal(31, issueTypeVs.Expansion.Contains.CountConcepts());
            Assert.Equal(issueTypeVs.Expansion.Contains.CountConcepts(), issueTypeVs.Expansion.Total);

            var trans = issueTypeVs.FindInExpansion("transient", "http://hl7.org/fhir/issue-type");
            Assert.NotNull(trans);
            Assert.NotNull(trans.FindCode("exception"));

            // Now, make this a versioned system
            issueTypeVs.Version = "3.14";
            await expander.ExpandAsync(issueTypeVs);
            Assert.NotEqual(id, issueTypeVs.Expansion.Identifier);
            Assert.Equal(31, issueTypeVs.Expansion.Total);

            //var versionParam = issueTypeVs.Expansion.Parameter.Single(c => c.Name == "version");
            //Assert.Equal("http://hl7.org/fhir/ValueSet/issue-type?version=3.14", ((FhirUri)versionParam.Value).Value);
        }


        [Fact]
        public async T.Task ExpansionOfComposeInclude()
        {
            var testVs = (await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/marital-status")).DeepCopy() as ValueSet;
            Assert.False(testVs.HasExpansion);

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });
            await expander.ExpandAsync(testVs);
            Assert.Equal(11, testVs.Expansion.Total);
        }


        [Fact]
        public async T.Task ExpansionOfComposeImport()
        {
            var testVs = (await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/FHIR-version")).DeepCopy() as ValueSet;
            Assert.False(testVs.HasExpansion);

            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });
            expander.Settings.MaxExpansionSize = 2;

            await Assert.ThrowsAsync<ValueSetExpansionTooBigException>(async () => await expander.ExpandAsync(testVs));

            expander.Settings.MaxExpansionSize = 50;
            await expander.ExpandAsync(testVs);
            Assert.Equal(22, testVs.Expansion.Total);
        }

        [Fact]
        public async T.Task TestIncludeDesignation()
        {
            var testVs = (await _resolver.ResolveByCanonicalUriAsync("http://hl7.org/fhir/ValueSet/animal-genderstatus")).DeepCopy() as ValueSet;
            Assert.False(testVs.HasExpansion);
            var expander = new ValueSetExpander(new ValueSetExpanderSettings { ValueSetSource = _resolver });

            //Import codes from codesystem
            await expander.ExpandAsync(testVs);
            Assert.DoesNotContain(testVs.Expansion.Contains, c => c.Designation.Any());

            expander.Settings.IncludeDesignations = true;
            await expander.ExpandAsync(testVs);

            Assert.Contains(testVs.Expansion.Parameter, p => p.Name == "includeDesignations" && (p.Value as FhirBoolean).Value == true);
            Assert.Contains(testVs.Expansion.Contains, c => c.Designation.Any(d => d.Language == "nl" && d.Value == "gesteriliseerd"));

            //compose codes
            testVs = new ValueSet
            {
                Compose = new ValueSet.ComposeComponent
                {
                    Include = new List<ValueSet.ConceptSetComponent>
                    {
                        new ValueSet.ConceptSetComponent
                        {
                            System = "http://hl7.org/fhir/v3/NullFlavor",
                            Concept = new List<ValueSet.ConceptReferenceComponent>
                            {

                                new ValueSet.ConceptReferenceComponent
                                {
                                    Code = "UNK",
                                    Display = "unknown",
                                    Designation = new List<ValueSet.DesignationComponent>
                                    {
                                        new ValueSet.DesignationComponent
                                        {
                                            Language = "nl",
                                            Value = "onbekend"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            expander.Settings.IncludeDesignations = false;
            await expander.ExpandAsync(testVs);
            Assert.DoesNotContain(testVs.Expansion.Contains, c => c.Designation.Any());
            expander.Settings.IncludeDesignations = true;
            await expander.ExpandAsync(testVs);

            Assert.Contains(testVs.Expansion.Parameter, p => p.Name == "includeDesignations" && (p.Value as FhirBoolean).Value == true);
            Assert.Contains(testVs.Expansion.Contains, c => c.Designation.Any(d => d.Language == "nl" && d.Value == "onbekend"));
        }

        [Fact]
        public async T.Task TestPropertyRetrieval()
        {
            var testCs = await _resolver.FindCodeSystemAsync("http://hl7.org/fhir/item-type");

            var conceptGroup = testCs.Concept.Single(c => c.Code == "group");
            var conceptQuestion = testCs.Concept.Single(c => c.Code == "question");

            Assert.False(conceptGroup.ListConceptProperties(testCs, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).Any());
            Assert.True(conceptQuestion.ListConceptProperties(testCs, CodeSystem.CONCEPTPROPERTY_NOT_SELECTABLE).Any());
        }


        private void testService(ITerminologyService svc)
        {
            var vsUrl = "http://hl7.org/fhir/ValueSet/data-absent-reason";
#pragma warning disable CS0618 // Type or member is obsolete
            var result = svc.ValidateCode(vsUrl, code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason");
            Assert.True(result.Success);

            result = svc.ValidateCode(vsUrl, code: "NaNX", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason");
            Assert.False(result.Success);

            result = svc.ValidateCode(vsUrl, code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason",
                display: "Not a Number (NaN)");
            Assert.True(result.Success);

            // The spec is not clear on the behaviour of incorrect displays - so don't test it here
            //result = svc.ValidateCode(vsUrl, code: "NaN", system: "http://hl7.org/fhir/data-absent-reason",
            //    display: "Not any Number");
            //Assert.True(result.Success);

            result = svc.ValidateCode("http://terminology.hl7.org/ValueSet/v3-AcknowledgementDetailCode", code: "_AcknowledgementDetailNotSupportedCode",
                system: "http://terminology.hl7.org/CodeSystem/v3-AcknowledgementDetailCode");
            Assert.True(result.Success);

            Assert.Throws<FhirOperationException>(() => svc.ValidateCode("http://hl7.org/fhir/ValueSet/crappy", code: "4322002", system: "http://snomed.info/sct"));

            var coding = new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "not-a-number");
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.True(result.Success);

            coding.Display = "Not a Number (NaN)";
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.True(result.Success);

            coding.Code = "NaNX";
            result = svc.ValidateCode(vsUrl, coding: coding);
            Assert.False(result.Success);
            coding.Code = "NaN";

            var cc = new CodeableConcept("http://terminology.hl7.org/CodeSystem/data-absent-reason", "NaNX", "Not a Number");
            result = svc.ValidateCode(vsUrl, codeableConcept: cc);
            Assert.False(result.Success);

            cc.Coding.Add(new Coding("http://terminology.hl7.org/CodeSystem/data-absent-reason", "asked-unknown"));
            result = svc.ValidateCode(vsUrl, codeableConcept: cc);
#pragma warning restore CS0618 // Type or member is obsolete
            DebugDumpOutputXml(result);

            Assert.True(result.Success);
        }

        private void DebugDumpOutputXml(Base fragment)
        {

#if DUMP_OUTPUT
            // commented out, since this will fill up the CI build's output log
            var doc = System.Xml.Linq.XDocument.Parse(new Serialization.FhirXmlSerializer().SerializeToString(fragment));
            output.WriteLine(doc.ToString(System.Xml.Linq.SaveOptions.None));
#endif
        }

        [Fact]
        public void LocalTSDisplayIncorrectAsWarning()
        {
            var svc = new LocalTerminologyService(_resolver);

            var vsUrl = "http://hl7.org/fhir/ValueSet/data-absent-reason";
#pragma warning disable CS0618 // Type or member is obsolete
            var result = svc.ValidateCode(vsUrl, code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason",
                display: "Not a Number (NaN)");
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(result.Success);
            Assert.Equal(0, result.Warnings);

#pragma warning disable CS0618 // Type or member is obsolete
            result = svc.ValidateCode(vsUrl, code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason",
                        display: "Certainly Not a Number");
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);
        }

        [Fact]
        public async void LocalTSDisplayIncorrectAsMessage()
        {
            var svc = new LocalTerminologyService(_resolver);
            var inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/data-absent-reason")
                .WithCode(code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason", display: "Not a Number (NaN)");

            var result = await svc.ValueSetValidateCode(inParams);

            Assert.True(result.GetSingleValue<FhirBoolean>("result")?.Value);
            Assert.Null(result.GetSingleValue<FhirString>("message"));

            inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/data-absent-reason")
                .WithCode(code: "not-a-number", system: "http://terminology.hl7.org/CodeSystem/data-absent-reason", display: "Certainly Not a Number");

            result = await svc.ValueSetValidateCode(inParams);

            Assert.True(result.GetSingleValue<FhirBoolean>("result")?.Value);
            Assert.NotNull(result.GetSingleValue<FhirString>("message"));
        }

        [Fact]
        public void LocalTermServiceValidateCodeTest()
        {
            var svc = new LocalTerminologyService(_resolver);

            // Do common tests for service
            testService(svc);

            // This is a valueset with a compose - not supported locally normally, but it has been expanded in the zip, so this will work
#pragma warning disable CS0618 // Type or member is obsolete
            var result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/yesnodontknow", code: "Y", system: "http://terminology.hl7.org/CodeSystem/v2-0136");
            Assert.True(result.Success);

            // This test is not always correctly done by the external services, so copied here instead
            result = svc.ValidateCode("http://terminology.hl7.org/ValueSet/v3-AcknowledgementDetailCode", code: "_AcknowledgementDetailNotSupportedCode",
                    system: "http://terminology.hl7.org/ValueSet/v3-AcknowledgementDetailCode", @abstract: false);
            Assert.False(result.Success);

            // And one that will specifically fail on the local service, since it's too complex too expand - the local term server won't help you here
            Assert.Throws<FhirOperationException>(() => svc.ValidateCode("http://hl7.org/fhir/ValueSet/substance-code", code: "1166006", system: "http://snomed.info/sct"));
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Fact]
        public async void LocalTermServiceValidateCodeWithParamsTest()
        {
            var svc = new LocalTerminologyService(_resolver);

            // This is a valueset with a compose - not supported locally normally, but it has been expanded in the zip, so this will work
            var inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/yesnodontknow")
                .WithCode(code: "Y", system: "http://terminology.hl7.org/CodeSystem/v2-0136");

            var result = await svc.ValueSetValidateCode(inParams);
            Assert.True(result.GetSingleValue<FhirBoolean>("result")?.Value);

            // This test is not always correctly done by the external services, so copied here instead
            inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://terminology.hl7.org/ValueSet/v3-AcknowledgementDetailCode")
                .WithCode(code: "_AcknowledgementDetailNotSupportedCode", system: "http://terminology.hl7.org/ValueSet/v3-AcknowledgementDetailCodee")
                .WithAbstract(false);

            result = await svc.ValueSetValidateCode(inParams);

            Assert.False(result.GetSingleValue<FhirBoolean>("result")?.Value);

            // And one that will specifically fail on the local service, since it's too complex too expand - the local term server won't help you here
            inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/substance-code")
                .WithCode(code: "1166006", system: "http://snomed.info/sct");
            await Assert.ThrowsAsync<FhirOperationException>( async () => await svc.ValueSetValidateCode(inParams));

        }       

        [Fact]
        public async T.Task LocalTermServiceValidateCodeWithoutSystemOrContext()
        {
            var svc = new LocalTerminologyService(_resolver);
            var inParams = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                {
                    new Parameters.ParameterComponent
                    {
                        Name = "code",
                        Value = new Code("DE")
                    },                  
                }
            };

            await Assert.ThrowsAsync<FhirOperationException>(async () => await svc.ValueSetValidateCode(inParams));
            
        }


        [Fact]
        public async T.Task LocalTermServiceUsingDuplicateParameters()
        {
            var svc = new LocalTerminologyService(_resolver);           
            var inParams = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                {
                    new Parameters.ParameterComponent
                    {
                        Name = "code",
                        Value = new Code("DE")
                    },
                     new Parameters.ParameterComponent
                    {
                        Name = "code",
                        Value = new Code("DE")
                    },
                      new Parameters.ParameterComponent
                    {
                        Name = "url",
                        Value = new FhirUri("urn:iso:std:iso:3166")
                    },
                }
            };

            await Assert.ThrowsAsync<FhirOperationException>(async () => await svc.ValueSetValidateCode(inParams));
        }

        [Fact]
        public void TestOperationOutcomes()
        {
            var svc = new LocalTerminologyService(_resolver);

#pragma warning disable CS0618 // obsolete, but used for testing purposes
            var outcome = svc.ValidateCode("http://hl7.org/fhir/ValueSet/administrative-gender", context:"Partient.gender", code: "test");
#pragma warning restore CS0618 

            Assert.NotNull(outcome?.Issue.FirstOrDefault().Details?.Text);

        }


        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceTranslateSimpleTranslate()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new TranslateParameters()
                .WithCode(code: "ACNE", system: "http://hl7.org/fhir/v2/0487")
                .WithTarget("http:/snomed.info/sct");

            var result = await svc.Translate(parameters, "102", useGet: true);

            Assert.NotNull(result);

            bool? isMatch = ((FhirBoolean)result.Parameter.First().Value).Value;
            if (isMatch.HasValue && isMatch.Value)
            {
                Assert.Collection(result.Parameter,
                    param =>
                    {
                        // This is the same parameter were we fetched the isMatch parameter.
                        // Need to include this since Assert.Collection does not skip any elements
                        Assert.Equal("result", param.Name);
                    },
                    param =>
                    {
                        Assert.Equal("match", param.Name);
                        Assert.Collection(param.Part,
                            part =>
                            {
                                Assert.Equal("equivalence", part.Name);
                            },
                            part =>
                            {
                                Assert.Equal("concept", part.Name);
                                Coding concept = (Coding)part.Value;
                                Assert.Equal("http://snomed.info/sct", concept.System);
                                Assert.Equal("309068002", concept.Code);
                            },
                            part =>
                            {
                                Assert.Equal("source", part.Name);
                                Assert.Equal("http://hl7.org/fhir/ConceptMap/102", ((FhirString)part.Value).Value);
                            });
                    });
            }
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceTranslateSimpleAutomap()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new TranslateParameters()
                .WithConceptMap(source: "http://snomed.info/sct?fhir_vs")
                .WithCode(code: "90260006", system: "http://snomed.info/sct")
                .WithTarget("http://hl7.org/fhir/ValueSet/substance-category");

            var result = await svc.Translate(parameters, useGet: true);

            Assert.NotNull(result);
            var param1 = result.Parameter.FirstOrDefault();
            Assert.Equal("result", param1.Name);
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceLookupPropertiesDisplayAndInactiveStatus()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new LookupParameters()
                .WithCode(code: "45313011000036107", system: "http://snomed.info/sct", version: "http://snomed.info/sct/32506021000036107/version/20160630")
                .WithProperties(new[] { "inactive", "display" });

            var result = await svc.Lookup(parameters);

            Assert.NotNull(result);

            var paramDisplay = result.Parameter.Find(p => p.Name == "display");
            Assert.NotNull(paramDisplay);
            Assert.IsType<FhirString>(paramDisplay.Value);
            Assert.Equal("teriparatide 20 microgram injection, 2.4 mL cartridge", ((FhirString)paramDisplay.Value).Value);

            var paramProperty = result.Parameter.Find(p => p.Name == "property");
            Assert.NotNull(paramProperty);
            Assert.Collection(paramProperty.Part,
                part =>
                {
                    Assert.Equal("code", part.Name);
                    Assert.IsType<Code>(part.Value);
                    Assert.Equal("inactive", ((Code)part.Value).Value);
                },
                part =>
                {
                    Assert.Equal("valueBoolean", part.Name);
                    Assert.IsType<FhirBoolean>(part.Value);
                    Assert.Equal(true, ((FhirBoolean)part.Value).Value);
                });
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceLookupInactiveStatus()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new LookupParameters()
                .WithCode(code: "45313011000036107", system: "http://snomed.info/sct", version: "http://snomed.info/sct/32506021000036107/version/20160630")
                .WithProperties(new[] { "inactive" });

            var result = await svc.Lookup(parameters, true);

            Assert.NotNull(result);

            var parameter = result.Parameter.Find(p => p.Name == "property");
            Assert.NotNull(parameter);

            Assert.Collection(parameter.Part,
                part =>
                {
                    Assert.Equal("code", part.Name);
                    Assert.IsType<Code>(part.Value);
                    Assert.Equal("inactive", ((Code)part.Value).Value);
                },
                part =>
                {
                    Assert.Equal("valueBoolean", part.Name);
                    Assert.IsType<FhirBoolean>(part.Value);
                    Assert.Equal(true, ((FhirBoolean)part.Value).Value);
                });
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceLookupSNOMEDCode()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new LookupParameters()
                .WithCode(code: "263495000", system: "http://snomed.info/sct");

            var result = await svc.Lookup(parameters);

            Assert.NotNull(result);
            Assert.True(result.Parameter.Count > 0);
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceExpandExplicitValueSet()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var result = await svc.Expand(null, "education-levels") as ValueSet;
            Assert.NotNull(result);
            Assert.True(result.Expansion.Contains.Count > 0, "Expected more than 0 items.");
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceExpandImplicitValueSetWithFilter()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new ExpandParameters()
                .WithValueSet(url: "http://snomed.info/sct?fhir_vs=refset/142321000036106")
                .WithFilter("met")
                .WithPaging(count: 10);

            var result = await svc.Expand(parameters) as ValueSet;

            Assert.NotNull(result);
            // Exactly 10 items all starting with 'met'.
            Assert.Collection(result.Expansion.Contains,
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                },
                item =>
                {
                    Assert.StartsWith("met", item.Display, StringComparison.OrdinalIgnoreCase);
                });
        }

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceSubsumesConceptASubsumesConceptB()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new SubsumesParameters()
                .WithCode(codeA: "235856003", codeB: "3738000", system: "http://snomed.info/sct", version: "http://snomed.info/sct/32506021000036107/version/20160430")
                .Build();

            var result = await svc.Subsumes(parameters);

            Assert.NotNull(result);
            var paramOutcome = result.Parameter.Find(p => p.Name == "outcome");
            Assert.NotNull(paramOutcome);
            Assert.IsType<Code>(paramOutcome.Value);
            Assert.Equal("subsumes", ((Code)paramOutcome.Value).Value);
        }

        

        [Fact(), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceClosureExample()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            // Step 1
            var parametersStep1 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52");

            var resultStep1 = await svc.Closure(parametersStep1) as ConceptMap;

            Assert.NotNull(resultStep1);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep1.Name);
            Assert.Equal("1", resultStep1.Version);

            // Step 2
            var conceptStep2 = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "22298006",
            };
            var parametersStep2 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep2 });

            var resultStep2 = await svc.Closure(parametersStep2) as ConceptMap;

            Assert.NotNull(resultStep2);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep2.Name);
            Assert.Equal("2", resultStep2.Version);

            // Step 3
            var conceptStep3 = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "128599005",
            };
            var parametersStep3 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep3 });

            var resultStep3 = await svc.Closure(parametersStep3) as ConceptMap;

            Assert.NotNull(resultStep3);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep3.Name);
            Assert.Equal("3", resultStep3.Version);
            Assert.Collection(resultStep3.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        });
                });

            // Step 4
            var conceptStep4A = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "301095005",
            };
            var conceptStep4B = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "298705000",
            };
            var conceptStep4C = new Coding
            {
                System = "http://snomed.info/sct",
                Code = "282729004",
            };
            var parametersStep4 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithConcepts(new List<Coding> { conceptStep4A, conceptStep4B, conceptStep4C });

            var resultStep4 = await svc.Closure(parametersStep4) as ConceptMap;

            Assert.NotNull(resultStep4);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep4.Name);
            Assert.Equal("4", resultStep4.Version);
            Assert.Collection(resultStep4.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("128599005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("301095005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("282729004", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        });
                });

            var parametersStep5 = new ClosureParameters("9214d56c-032e-4f87-a003-e515f7386a52")
                .WithVersion("0");

            var resultStep5 = await svc.Closure(parametersStep5) as ConceptMap;

            Assert.NotNull(resultStep5);
            Assert.Equal("9214d56c-032e-4f87-a003-e515f7386a52", resultStep5.Name);
            Assert.Equal("4", resultStep5.Version);
            Assert.Collection(resultStep5.Group,
                group =>
                {
                    Assert.Equal("http://snomed.info/sct", group.Source);
                    Assert.Equal("http://snomed.info/sct", group.Target);
                    Assert.Collection(group.Element,
                        element =>
                        {
                            Assert.Equal("298705000", element.Code);
                        },
                        element =>
                        {
                            Assert.Equal("22298006", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("128599005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("301095005", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        },
                        element =>
                        {
                            Assert.Equal("282729004", element.Code);
                            Assert.Collection(element.Target,
                                target =>
                                {
                                    Assert.Equal("128599005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("301095005", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                },
                                target =>
                                {
                                    Assert.Equal("298705000", target.Code);
                                    Assert.Equal(ConceptMapEquivalence.Subsumes, target.Equivalence);
                                });
                        });
                });
        }

        [Fact(Skip = "Don't want to run these kind of integration tests anymore"), Trait("TestCategory", "IntegrationTest")]
        public async void ExternalServiceValidateCodeTest()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var svc = new ExternalTerminologyService(client);

            var parameters = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/substance-code")
                .WithCode(code: "1166006", system: "http://snomed.info/sct");

            var outParams = await svc.ValueSetValidateCode(parameters);
            var result = outParams.GetSingleValue<FhirBoolean>("result");
            Assert.NotNull(result);
            Assert.True(result.Value);
        }       

        [Fact(Skip = "Don't want to run these kind of integration tests anymore"), Trait("TestCategory", "IntegrationTest")]
        public void FallbackServiceValidateCodeTest()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var external = new ExternalTerminologyService(client);
            var local = new LocalTerminologyService(_resolver);
            var svc = new FallbackTerminologyService(local, external);

            testService(svc);

            // Now, this should fall back
#pragma warning disable CS0618 // Type or member is obsolete
            var result = svc.ValidateCode("http://hl7.org/fhir/ValueSet/substance-code", code: "1166006", system: "http://snomed.info/sct");
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(result.Success);
        }

        [Fact(Skip = "Don't want to run these kind of integration tests anymore"), Trait("TestCategory", "IntegrationTest")]
        public async void FallbackServiceValidateCodeWithParamsTest()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var external = new ExternalTerminologyService(client);
            var local = new LocalTerminologyService(_resolver);
            var svc = new FallbackTerminologyService(local, external);

            // Now, this should fall back
            var inParams = new ValidateCodeParameters()
                .WithValueSet(url: "http://hl7.org/fhir/ValueSet/substance-code")
                .WithCode(code: "1166006", system: "http://snomed.info/sct");

            var result = await svc.ValueSetValidateCode(inParams);
            Assert.True(result.GetSingleValue<FhirBoolean>("result")?.Value);
        }

        [Fact(Skip = "Don't want to run these kind of integration tests anymore"), Trait("TestCategory", "IntegrationTest")]
        public async T.Task FallbackServiceValidateCodeTestWithVS()
        {
            var client = new FhirClient(_externalTerminologyServerEndpoint);
            var service = new ExternalTerminologyService(client);
            var vs = await _resolver.FindValueSetAsync("http://hl7.org/fhir/ValueSet/substance-code");
            Assert.NotNull(vs);

            // Override the canonical with something the remote server cannot know
            vs.Url = "http://furore.com/fhir/ValueSet/testVS";
            var local = new LocalTerminologyService(new IKnowOnlyMyTestVSResolver(vs));
            var fallback = new FallbackTerminologyService(local, service);

            // Now, this should fall back to external + send our vs (that the server cannot know about)
#pragma warning disable CS0618 // Type or member is obsolete
            var result = fallback.ValidateCode("http://furore.com/fhir/ValueSet/testVS", code: "1166006", system: "http://snomed.info/sct");
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(result.Success);
        }       

        private class IKnowOnlyMyTestVSResolver : IAsyncResourceResolver
        {
            public ValueSet _myOnlyVS;

            public IKnowOnlyMyTestVSResolver(ValueSet vs)
            {
                _myOnlyVS = vs;
            }

            public async Task<Resource> ResolveByCanonicalUriAsync(string uri)
            {
                return await T.Task.FromResult(uri == _myOnlyVS.Url) ? _myOnlyVS : null;
            }

            public Task<Resource> ResolveByUriAsync(string uri) => throw new NotImplementedException();
        }

    }
}

