#nullable enable

using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Specification.Terminology
{
    public static class TerminologyServiceFactory
    {
        /// <summary>
        /// Creates a MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets.
        /// </summary>
        /// <param name="coreResourceResolver">Resource resolves to resolve FHIR core artifacts</param>
        /// <param name="expanderSettings">ValueSet expansion settings</param>
        /// <returns>A MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets</returns>
        public static MultiTerminologyService CreateDefaultForCore(IAsyncResourceResolver coreResourceResolver, ValueSetExpanderSettings? expanderSettings = null)
        {
            var mimeTypeRoutingSettings = new TerminologyServiceRoutingSettings(new MimeTypeTerminologyService())
            {
                PreferredValueSets = new string[]
                {
                    "http://www.rfc-editor.org/bcp/bcp13.txt"
                }
            };

            var localTermRoutingSettings = new TerminologyServiceRoutingSettings(new LocalTerminologyService(coreResourceResolver, expanderSettings))
            {
                PreferredValueSets = new string[]
                {
                    "http://hl7.org/fhir/ValueSet/"
                }
            };

            return new MultiTerminologyService(mimeTypeRoutingSettings, localTermRoutingSettings);
        }

    }
}

#nullable restore