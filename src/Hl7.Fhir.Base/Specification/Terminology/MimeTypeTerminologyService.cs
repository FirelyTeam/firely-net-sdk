#nullable enable

using System;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Checks if codes are valid Mime-Types (urn:ietf:bcp:13)
    /// </summary>
    public sealed class MimeTypeTerminologyService : CodeSystemTerminologyService
    {
        private const string MIMETYPE_SYSTEM = "urn:ietf:bcp:13";
        public const string MIMETYPE_VALUESET_R4_AND_UP = "http://hl7.org/fhir/ValueSet/mimetypes";
        public const string MIMETYPE_VALUESET_STU3 = "http://www.rfc-editor.org/bcp/bcp13.txt";

        public MimeTypeTerminologyService() : base("MIME type", MIMETYPE_SYSTEM, [MIMETYPE_VALUESET_STU3, MIMETYPE_VALUESET_R4_AND_UP])
        {
        }
        
        //mime-type format: type "/" [tree "."] subtype ["+" suffix]* [";" parameter];
        override protected bool ValidateCodeType(string code)
        {
            var entries = code.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return entries.Length == 2;
        }
    }
}

#nullable restore

