using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class EntryToTypedEntryExtensions
    {
        /// <inheritdoc cref="ToTypedEntryResponseAsync(EntryResponse, IStructureDefinitionSummaryProvider)" />
        public static TypedEntryResponse ToTypedEntryResponse(this EntryResponse response, IStructureDefinitionSummaryProvider provider)
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
                result.TypedElement = parseResource(body, response.ContentType, provider, response.IsSuccessful());
            }

            return result;
        }

        public static async Task<TypedEntryResponse> ToTypedEntryResponseAsync(this EntryResponse response, IStructureDefinitionSummaryProvider provider)
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
                result.TypedElement = await parseResourceAsync(body, response.ContentType, provider, response.IsSuccessful()).ConfigureAwait(false);
            }

            return result;
        }

        private static ITypedElement parseResource(string bodyText, string contentType, IStructureDefinitionSummaryProvider provider, bool throwOnFormatException)
        {
            if (bodyText == null) throw Error.ArgumentNull(nameof(bodyText));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

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
                return (fhirType == ResourceFormat.Json)
                    ? FhirJsonNode.Parse(bodyText).ToTypedElement(provider)
                    : FhirXmlNode.Parse(bodyText).ToTypedElement(provider);
            }
            catch (Exception ex) when (ex is FormatException && !throwOnFormatException ||
                                       ex is InvalidOperationException)
            {
                return null;
            }
        }

        private static async Task<ITypedElement> parseResourceAsync(string bodyText, string contentType, IStructureDefinitionSummaryProvider provider, bool throwOnFormatException)
        {
            if (bodyText == null) throw Error.ArgumentNull(nameof(bodyText));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

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
                return (fhirType == ResourceFormat.Json)
                     ? (await FhirJsonNode.ParseAsync(bodyText).ConfigureAwait(false)).ToTypedElement(provider)
                     : (await FhirXmlNode.ParseAsync(bodyText).ConfigureAwait(false)).ToTypedElement(provider);
            }
            catch (Exception ex) when (ex is FormatException && !throwOnFormatException ||
                                       ex is InvalidOperationException)
            {
                return null;
            }
        }
    }

    public class UnsupportedBodyTypeException : Exception
    {
        public string BodyType { get; set; }

        public string Body { get; set; }
        public UnsupportedBodyTypeException(string message, string mimeType, string body) : base(message)
        {
            BodyType = mimeType;
            Body = body;
        }
    }
}
