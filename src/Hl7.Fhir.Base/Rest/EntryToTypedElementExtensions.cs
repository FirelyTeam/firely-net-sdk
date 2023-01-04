#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.Net.Mime;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToTypedEntryExtensions
    {
        /// <summary>
        /// Turns a straight-from-the-write <see cref="EntryResponse"/> into a reponse with a parsed Resource.
        /// </summary>
        /// <exception cref="UnsupportedBodyTypeException">If the body of the received HTTP request is unrecognizable.</exception>
        /// <exception cref="DeserializationFailedException">If the body of the received HTTP request cannot be parsed to a resource.</exception>
        internal static Resource? DecodeBodyToResource(string body, string contentType, IFhirSerializationEngine ser)
        {
            var fhirType = ContentType.GetResourceFormatFromContentType(contentType);

            if (fhirType == ResourceFormat.Unknown)
                throw new UnsupportedBodyTypeException(
                    "Endpoint returned a body with contentType '{0}', while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?"
                .FormatWith(contentType), contentType, body);

            if (!SerializationUtil.ProbeIsJson(body) && !SerializationUtil.ProbeIsXml(body))
                throw new UnsupportedBodyTypeException(
                "Endpoint said it returned '{0}', but the body is not recognized as either xml or json.".FormatWith(contentType), contentType, body);

            return (fhirType == ResourceFormat.Json)
                 ? ser.DeserializeFromJson(body)
                 : ser.DeserializeFromXml(body);
        }
    }
}

#nullable restore