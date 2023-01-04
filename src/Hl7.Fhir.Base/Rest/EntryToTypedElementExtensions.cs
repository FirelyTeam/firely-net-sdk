#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToTypedEntryExtensions
    {
        /// <summary>
        /// Turns a straight-from-the-write <see cref="EntryResponse"/> into a reponse with a parsed Resource.
        /// </summary>
        /// <exception cref="UnsupportedBodyTypeException">If the body of the received HTTP request is unrecognizable.</exception>
        /// <exception cref="DeserializationFailedException">If the body could not be parsed into a FHIR resource</exception>
        internal static Resource? DecodeBodyToResource(this EntryResponse response, IFhirSerializationEngine ser)
        {
            var body = response.GetBodyAsText();

            if (!string.IsNullOrEmpty(body))
            {
                var (resource, report) = parseResourceAsync(body, response.ContentType, ser);
                return response.IsSuccessful() && report is not null ? throw report : resource;
            }
            else
                return null;
        }

        private static (Resource?, DeserializationFailedException?) parseResourceAsync(string bodyText, string contentType, IFhirSerializationEngine ser)
        {
            var fhirType = ContentType.GetResourceFormatFromContentType(contentType);

            if (fhirType == ResourceFormat.Unknown)
                throw new UnsupportedBodyTypeException(
                    "Endpoint returned a body with contentType '{0}', while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?"
                        .FormatWith(contentType), contentType, bodyText);

            if (!SerializationUtil.ProbeIsJson(bodyText) && !SerializationUtil.ProbeIsXml(bodyText))
                throw new UnsupportedBodyTypeException(
                        "Endpoint said it returned '{0}', but the body is not recognized as either xml or json.".FormatWith(contentType), contentType, bodyText);

            return (fhirType == ResourceFormat.Json)
                 ? (ser.DeserializeFromJson(bodyText, out var jsonErrors),jsonErrors) 
                 : (ser.DeserializeFromXml(bodyText, out var xmlErrors), xmlErrors);
        }
    }
}

#nullable restore