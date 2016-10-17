using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Vs;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Source
{
    public class VSExpansionTests : IClassFixture<ResolverFixture>
    {            
        private IResourceResolver _resolver;

        public VSExpansionTests(ResolverFixture fixture)
        {
            _resolver = fixture.Resolver;
        }

        [Fact]
        public void ExpansionOfDefine()
        {
            var issueTypeVs = _resolver.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/issue-type").DeepCopy() as ValueSet;
            Assert.False(issueTypeVs.HasExpansion);

            // Wipe the version so we don't have to update our tests all the time
            issueTypeVs.CodeSystem.Version = null;

            var expander = new ValueSetExpander();

            expander.Expand(issueTypeVs);

            Assert.True(issueTypeVs.HasExpansion);
            var id = issueTypeVs.Expansion.Identifier;
            Assert.NotNull(id);
            Assert.False(issueTypeVs.Expansion.Parameter.Any(c => c.Name == "version"));

            Assert.True(issueTypeVs.CodeInExpansion("security", "http://hl7.org/fhir/issue-type"));
            Assert.True(issueTypeVs.CodeInExpansion("expired", "http://hl7.org/fhir/issue-type"));
            Assert.Equal(29, issueTypeVs.CountCodes());
            Assert.Equal(issueTypeVs.CountCodes(), issueTypeVs.Expansion.Total);

            var trans = issueTypeVs.FindInExpansion("transient", "http://hl7.org/fhir/issue-type");
            Assert.NotNull(trans);
            Assert.NotNull(trans.FindCode("exception"));

            // Now, make this a versioned system
            issueTypeVs.CodeSystem.Version = "3.14";
            expander.Expand(issueTypeVs);
            Assert.NotEqual(id, issueTypeVs.Expansion.Identifier);
            Assert.Equal(29, issueTypeVs.CountCodes());

            var versionParam = issueTypeVs.Expansion.Parameter.Single(c => c.Name == "version");
            Assert.Equal("http://hl7.org/fhir/ValueSet/issue-type?version=3.14", ((FhirUri)versionParam.Value).Value);
        }
    }
}
