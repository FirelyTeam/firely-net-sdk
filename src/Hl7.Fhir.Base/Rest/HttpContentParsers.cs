/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal static class HttpContentParsers
    {
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";

        public static Bundle.ResponseComponent ExtractResponseComponent(this HttpResponseMessage responseMessage)
        {
            var response = new Bundle.ResponseComponent()
            {
                Status = ((int)responseMessage.StatusCode).ToString(),
                LastModified = responseMessage.Content?.Headers.LastModified,
                Location = responseMessage.Headers.Location?.OriginalString ?? responseMessage.Content?.Headers.ContentLocation?.OriginalString,
                Etag = responseMessage.GetVersionFromETag(),
            };

            setHeaders(response, responseMessage.Headers);

            return response;
        }


        private static void setHeaders(Bundle.ResponseComponent interaction, HttpResponseHeaders headers)
        {
            foreach (var header in headers)
            {
                //TODO: check multiple values for a header??
                var key = header.Key;
                var value = header.Value.FirstOrDefault();

                interaction.AddExtension(EXTENSION_RESPONSE_HEADER, new FhirString(key + ":" + value));
            }
        }

        /// <summary>
        /// Returns the body, decoded as a string.
        /// </summary>
        /// <returns>The body as a string, or null if the body could not be decoded (e.g. contains invalid Unicode code points).</returns>
        public static async Task<string?> GetBodyAsString(this HttpContent content)
        {
            try
            {
                return await content.ReadAsStringAsync();
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
        /// Gets the content type from the header, removing parameters.
        /// </summary>
        /// <remarks>Note that this also removes the fhirVersion parameter.</remarks>
        public static string? GetContentType(this HttpContent content) =>
            content.Headers.ContentType?.MediaType;

        internal record ResponseComponents(Bundle.ResponseComponent Response, byte[]? BodyData, string? BodyText, Resource? BodyResource);

        internal static async Task<ResponseComponents> ExtractResponseComponents(this HttpResponseMessage responseMessage, IFhirSerializationEngine ser)
        {
            var response = responseMessage.ExtractResponseComponent();

            if (responseMessage.Content is not null)
            {
                var bodyData = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                var bodyText = await responseMessage.Content.GetBodyAsString().ConfigureAwait(false);

                var responseBody = await responseMessage.GetBodyFromContent(ser).ConfigureAwait(false);
                var bodyResource = responseBody switch
                {
                    ResponseWithoutBody => null,
                    ResponseWithNonFhirPayload => null,
                    ResponseWithFhirPayload rwfp => rwfp.Body,
                    ResponseWithBinaryPayload =>
                        throw new NotImplementedException("Cannot handle binary resources yet."),
                    _ =>
                        throw new InvalidOperationException("Unexpected ReceivedResponse subclass.")
                };

                return new(response, bodyData, bodyText, bodyResource);
            }
            else
                return new(response, null, null, null);
        }

        // TODO:  We might need to keep an eye on the expected/actual fhirVersion we are receiving to improve
        // the error messages when a parse failure occurs.
        /// <summary>
        /// Tries to extract the body, returning information about success and the kind of body encountered.
        /// </summary>
        /// <returns>A <see cref="ResponseWithFhirPayload"/> if the body contains FHIR data, <see cref="ResponseWithNonFhirPayload"/> if the body
        /// data was not recognized or parseable, a <see cref="ResponseWithBinaryPayload"/> if the server returned binary data
        /// and <see cref="ResponseWithoutBody"/> if there was no body at all.</returns>
        /// <exception cref="DeserializationFailedException">When the Content-Type and serialization indicate this is a FHIR payload, but it cannot
        /// be parsed correctly.</exception>

        public static Task<ReceivedResponse> GetBodyFromContent(this HttpResponseMessage message, IFhirSerializationEngine ser) =>
            message.IsSuccessStatusCode ? getBodyOnSuccess(message, ser) : getBodyOnFailure(message, ser);

        /// <summary>
        /// Tries to extract the body, with the assumption that the operation has failed.
        /// </summary>
        /// <returns>A <see cref="ResponseWithFhirPayload"/> if the body contains FHIR data, <see cref="ResponseWithNonFhirPayload"/> if the body
        /// data was not recognized or parseable, and <see cref="ResponseWithoutBody"/> if there was no body at all.</returns>
        /// <exception cref="DeserializationFailedException">When the Content-Type and serialization indicate this is a FHIR payload, but it cannot
        /// be parsed correctly.</exception>
        /// <exception cref="UnsupportedBodyTypeException">if the Content-Type is not a FHIR serialization or the data is not recognizable as FHIR.</exception>
        private static async Task<ReceivedResponse> getBodyOnFailure(HttpResponseMessage message, IFhirSerializationEngine ser)
        {
            if (message.Content is null) return new ResponseWithoutBody(false);
            var content = message.Content;

            var serialization = ContentType.GetResourceFormatFromContentType(content.GetContentType());
            var bodyText = await content.ReadAsStringAsync();

            // If this is a failure, the server could have set the content-type incorrectly as well,
            // do double check whether we need to take this payload as FHIR data
            return serialization switch
            {
                ResourceFormat.Xml when SerializationUtil.ProbeIsFhirXml(bodyText) =>
                            new ResponseWithFhirPayload(false, setLocation(ser.DeserializeFromXml(bodyText), message)),
                ResourceFormat.Json when SerializationUtil.ProbeIsFhirJson(bodyText) =>
                            new ResponseWithFhirPayload(false, setLocation(ser.DeserializeFromJson(bodyText), message)),
                _ => new ResponseWithNonFhirPayload(false, bodyText)
            };
        }

        /// <summary>
        /// Tries to extract the body, with the assumption that the operation has succeeded.
        /// </summary>
        /// <returns>A <see cref="ResponseWithFhirPayload"/> if the body contains FHIR data, <see cref="ResponseWithoutBody"/> when there was no body.</returns>
        /// <exception cref="DeserializationFailedException">When the Content-Type and serialization indicate this is a FHIR payload, but it cannot
        /// be parsed correctly.</exception>
        /// <exception cref="UnsupportedBodyTypeException">if the Content-Type is not a FHIR serialization or the data is not recognizable as FHIR.</exception>
        private static async Task<ReceivedResponse> getBodyOnSuccess(HttpResponseMessage message, IFhirSerializationEngine ser)
        {
            if (message.Content is null) return new ResponseWithoutBody(true);
            var content = message.Content;

            var contentType = content.GetContentType();
            var serialization = ContentType.GetResourceFormatFromContentType(contentType);
            var bodyText = await content.ReadAsStringAsync();

            return serialization switch
            {
                ResourceFormat.Xml when SerializationUtil.ProbeIsFhirXml(bodyText) =>
                            new ResponseWithFhirPayload(true, setLocation(ser.DeserializeFromXml(bodyText), message)),
                ResourceFormat.Json when SerializationUtil.ProbeIsFhirJson(bodyText) =>
                            new ResponseWithFhirPayload(true, setLocation(ser.DeserializeFromJson(bodyText), message)),
                ResourceFormat.Unknown => throw new UnsupportedBodyTypeException(
                    $"Endpoint returned a body with contentType '{contentType}', " +
                    $"while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?",
                    contentType, bodyText),
                _ => throw new UnsupportedBodyTypeException(
                        $"Endpoint said it returned '{contentType}', but the body is not recognized as either xml or json.", contentType, bodyText)
            };
        }

        // Sets the Resource.ResourceBase to the location given in the RequestUri of the response message.
        private static Resource setLocation(Resource r, HttpResponseMessage response)
        {
            if (response.GetRequestUri()?.OriginalString is string location)
            {
                r.ResourceBase = new ResourceIdentity(location).BaseUri;
            }

            return r;
        }
    }

    internal abstract record ReceivedResponse(bool Success);

    internal record ResponseWithoutBody(bool Success) : ReceivedResponse(Success);

    internal record ResponseWithFhirPayload(bool Success, Resource Body) : ReceivedResponse(Success);

    internal record ResponseWithBinaryPayload(bool Success, byte[] Body) : ReceivedResponse(Success);

    internal record ResponseWithNonFhirPayload(bool Success, string Body) : ReceivedResponse(Success);

}

#nullable restore