#nullable enable

using System;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Checks if codes are valid Mime-Types (urn:ietf:bcp:13)
/// </summary>
public sealed class MimeTypeTerminologyService() : CustomValueSetTerminologyService("MIME type", MIMETYPE_SYSTEM,
    [MIMETYPE_VALUESET_STU3, MIMETYPE_VALUESET_R4_AND_UP])
{
    private const string MIMETYPE_SYSTEM = "urn:ietf:bcp:13";
    public const string MIMETYPE_VALUESET_R4_AND_UP = "http://hl7.org/fhir/ValueSet/mimetypes";
    public const string MIMETYPE_VALUESET_STU3 = "http://www.rfc-editor.org/bcp/bcp13.txt";
    private const string XML_CODE = "xml";
    private const string JSON_CODE = "json";
    private const string TTL_CODE = "ttl";

    //mime-type format: type "/" [tree "."] subtype ["+" suffix]* [";" parameter];
    //FHIR also allows for the following codes: xml, json, ttl
    override protected bool ValidateCodeType(string code)
    {
        //This is a temporary fix until we support additional bindings.
        if (code == XML_CODE || code == JSON_CODE || code == TTL_CODE)
        {
            return true;
        }

        var entries = code.Split(['/'], StringSplitOptions.RemoveEmptyEntries);
        return entries.Length == 2;
    }
}