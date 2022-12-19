
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToTypedEntryExtensions
    {
        internal static TypedEntryResponse ToTypedEntryResponse(this EntryResponse response, IFhirSerializationEngine ser)
        {
            var result = new TypedEntryResponse
            {
                ContentType = response.ContentType,
                Body = response.Body,
                Etag = response.Etag,
                Headers = response.Headers,
                LastModified = response.LastModified,
                Location = response.Location,
                ResponseUri = response.ResponseUri,
                Status = response.Status
            };

            var body = response.GetBodyAsText();
            if (!string.IsNullOrEmpty(body))
            {
                result.BodyResource = parseResourceAsync(body, response.ContentType, ser, response.IsSuccessful());
            }

            return result;
        }
        
        private static Resource parseResourceAsync(string bodyText, string contentType, IFhirSerializationEngine ser, bool throwOnFormatException)
        {
            var fhirType = ContentType.GetResourceFormatFromContentType(contentType);

            if (fhirType == ResourceFormat.Unknown)
                throw new UnsupportedBodyTypeException(
                    "Endpoint returned a body with contentType '{0}', while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?"
                        .FormatWith(contentType), contentType, bodyText);

            if (!SerializationUtil.ProbeIsJson(bodyText) && !SerializationUtil.ProbeIsXml(bodyText))
                throw new UnsupportedBodyTypeException(
                        "Endpoint said it returned '{0}', but the body is not recognized as either xml or json.".FormatWith(contentType), contentType, bodyText);

            try
            {
                // return (fhirType == ResourceFormat.Json)
                //      ? (await FhirJsonNode.ParseAsync(bodyText).ConfigureAwait(false)).ToTypedElement(provider)
                //      : (await FhirXmlNode.ParseAsync(bodyText).ConfigureAwait(false)).ToTypedElement(provider);
                return (fhirType == ResourceFormat.Json)
                     ? ser.DeserializeFromJson(bodyText) : ser.DeserializeFromXml(bodyText);

            }
            catch (Exception ex) when ((ex is FormatException or DeserializationFailedException && !throwOnFormatException) ||
                                       ex is InvalidOperationException)
            {
                return null;
            }
        }
    }
}
