/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal static class HttpContentParsers
    {
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";

        /// <summary>
        /// Builds a <see cref="Bundle.ResponseComponent"/> out of the data in a <see cref="HttpResponseMessage"/>.
        /// </summary>
        public static Bundle.ResponseComponent ExtractResponseComponent(this HttpResponseMessage responseMessage)
        {
            var response = new Bundle.ResponseComponent()
            {
                Status = ((int)responseMessage.StatusCode).ToString(),
                LastModified = responseMessage.Content?.Headers.LastModified,
                Location = responseMessage.Headers.Location?.OriginalString ?? responseMessage.Content?.Headers.ContentLocation?.OriginalString,
                Etag = responseMessage.Headers.ETag?.ToString(),
            };

            response.SetHttpHeaders(responseMessage.Headers);
            return response;
        }

        /// <summary>
        /// Add the headers from a <see cref="HttpResponseHeaders"/> as extensions to a <see cref="Bundle.ResponseComponent"/>.
        /// </summary>
        /// <remarks>The headers will be added, even if they we already present on the <see cref="Bundle.ResponseComponent"/>.
        /// </remarks>
        public static void SetHttpHeaders(this Bundle.ResponseComponent interaction, HttpResponseHeaders headers)
        {
            foreach (var header in headers)
            {
                var key = header.Key;
                foreach (var value in header.Value)
                    interaction.AddExtension(EXTENSION_RESPONSE_HEADER, new FhirString($"{key}:{value}"));
            }
        }

        private static readonly char[] HEADERSPLIT = new[] { ':' };

        /// <summary>
        /// Extract the headers founds as extensions on the <see cref="Bundle.RequestComponent"/> and return them
        /// as a <see cref="HttpResponseHeaders"/> collection.
        /// </summary>
        public static HttpResponseHeaders GetHttpHeaders(this Bundle.ResponseComponent interaction)
        {
            var msg = new HttpResponseMessage();
            var extensionValues = interaction.GetExtensions(EXTENSION_RESPONSE_HEADER)
                .Select(e => e.Value).OfType<FhirString>();

            foreach (var extension in extensionValues)
            {
                var pair = extension.Value.Split(HEADERSPLIT,2);
                var key = pair[0];
                var value = pair[1];

                msg.Headers.TryAddWithoutValidation(key, value);
            }

            return msg.Headers;
        }

        /// <summary>
        /// Returns the body, decoded as a string.
        /// </summary>
        /// <returns>The body as a string, or null if the body could not be decoded (e.g. contains invalid Unicode code points).</returns>
        public static async Task<string?> GetBodyAsString(this HttpContent content)
        {
            try
            {
                return await content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch
            {
                return null;
            }
        }

        public static Uri? GetRequestUri(this HttpResponseMessage response) =>
            response?.RequestMessage?.RequestUri;

        public static string? GetVersionFromETag(this HttpResponseMessage response) =>
            response.Headers.ETag?.Tag.Trim('\"');

        /// <summary>
        /// Gets the content type from the header, removing all parameters including character 
        /// encoding and fhirVersion parameter.
        /// </summary>
        public static string? GetContentType(this HttpContent content) =>
            content.Headers.ContentType?.MediaType;

        internal record ResponseData(Bundle.ResponseComponent Response, byte[]? BodyData, string? BodyText, Resource? BodyResource);
             
        /// <summary>
        /// Extract headers into a <see cref="Bundle.ResponseComponent"/>, the body as a byte array, as text and when possible, a parsed resource.
        /// </summary>
        /// <returns>A <see cref="ResponseData"/> with a non-null <see cref="ResponseData.BodyResource"/> if the body contains FHIR data, 
        /// a non-null <see cref="ResponseData.BodyText"/> if the body is valid UTF-8 encoded text and the body data as a byte array, if there is a body.
        /// </returns>
        /// <exception cref="DeserializationFailedException">Thrown when the Content-Type and serialization indicate this is a FHIR payload, but it cannot
        /// be parsed correctly.</exception>
        /// <exception cref="UnsupportedBodyTypeException">Thrown when the Content-Type is not a FHIR serialization or the data is not recognizable as FHIR.</exception>
        /// <remarks>If the status of the response indicates failure, this function will be lenient and return the body data 
        /// instead of throwing an <see cref="UnsupportedBodyTypeException"/> when the content type or content itself is not recognizable as FHIR. This improves
        /// the chances of capturing diagnostic (non-FHIR) bodies returned by the server when an operation fails.</remarks>
        internal static async Task<ResponseData> ExtractResponseData(this HttpResponseMessage message, IFhirSerializationEngine ser)
        {
            var response = message.ExtractResponseComponent();

            var content = message.Content;
            var bodyData = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
            if (bodyData.Length == 0) return new(response, null, null, null);

            var contentType = content.GetContentType();

            // TODO:  We might need to keep an eye on the expected/actual fhirVersion we are receiving to improve
            // the error response in case we are dealing with a server that uses another version of FHIR.
            var serialization = ContentType.GetResourceFormatFromContentType(content.GetContentType());
            var bodyText = await content.GetBodyAsString().ConfigureAwait(false);  // will be null if not parseable as text

            // Depending on whether this is a failure, the server could have set the content-type incorrectly as well,
            // do double check whether we need to take this payload as FHIR data
            var resource = serialization switch
            {
                ResourceFormat.Xml when bodyText is not null && SerializationUtil.ProbeIsXml(bodyText) =>
                            ser.DeserializeFromXml(bodyText),
                ResourceFormat.Json when bodyText is not null && SerializationUtil.ProbeIsJson(bodyText) =>
                            ser.DeserializeFromJson(bodyText),
                (ResourceFormat.Xml or ResourceFormat.Json) when message.IsSuccessStatusCode =>
                    throw new UnsupportedBodyTypeException(
                       $"Endpoint said it returned '{contentType}', but the body is not recognized as either xml or json.", contentType, bodyText),
                ResourceFormat.Unknown when message.IsSuccessStatusCode => throw new UnsupportedBodyTypeException(
                       $"Endpoint returned a body with contentType '{contentType}', " +
                       $"while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?",
                       contentType, bodyText),
                _ => null
            };

            // Sets the Resource.ResourceBase to the location given in the RequestUri of the response message.
            if (resource is not null && message.GetRequestUri()?.OriginalString is string location)
            {
                var ri = new ResourceIdentity(location);
                resource.ResourceBase = ri.HasBaseUri && ri.Form == ResourceIdentityForm.AbsoluteRestUrl
                    ? ResourceIdentity.Build(ri.BaseUri, ri.ResourceType, ri.Id, ri.VersionId)
                    : new Uri(location, UriKind.Absolute);
            }

            return new(response, bodyData, bodyText, resource);
        }
    }

    //internal abstract record ReceivedResponse;

    //internal record ResponseWithNoPayload : ReceivedResponse
    //{
    //    public static ResponseWithNoPayload Instance = new();
    //}

    //internal record ResponseWithFhirPayload(Resource Body) : ReceivedResponse;

    //internal record ResponseWithBinaryPayload(byte[] Body) : ReceivedResponse;

    //internal record ResponseWithNonFhirTextPayload(string Body) : ReceivedResponse;

    //internal record ResponseWithNonFhirDataPayload(byte[] Body) : ReceivedResponse;

}

#nullable restore