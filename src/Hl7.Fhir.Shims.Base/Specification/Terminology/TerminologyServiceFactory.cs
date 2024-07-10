/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


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
#if STU3
                    MimeTypeTerminologyService.MIMETYPE_VALUESET_STU3
#else
                    MimeTypeTerminologyService.MIMETYPE_VALUESET_R4_AND_UP
#endif       
                }
            };

            var languageRoutingSettings = new TerminologyServiceRoutingSettings(new LanguageTerminologyService())
            {
                PreferredValueSets = [LanguageTerminologyService.LANGUAGE_VALUESET]
            };

            var localTermRoutingSettings = new TerminologyServiceRoutingSettings(new LocalTerminologyService(coreResourceResolver, expanderSettings))
            {
                PreferredValueSets = new string[]
                {
                    "http://hl7.org/fhir/ValueSet/*"
                }
            };

            return new MultiTerminologyService(mimeTypeRoutingSettings, languageRoutingSettings, localTermRoutingSettings);
        }

    }
}

#nullable restore
