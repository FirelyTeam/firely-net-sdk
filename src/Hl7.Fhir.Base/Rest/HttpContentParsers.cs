/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Net;
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
                if (extension.Value is not null)
                {
                    var pair = extension.Value.Split(HEADERSPLIT, 2);
                    var key = pair[0];
                    var value = pair[1];

                    msg.Headers.TryAddWithoutValidation(key, value);
                }
            }

            return msg.Headers;
        }

        /// <summary>
        /// Returns the body, decoded as a string.
        /// </summary>
        /// <exception cref="UnsupportedBodyTypeException">Will throw when the body contains invalid Unicode and thus cannot be decoded as a string.</exception>
        public static async Task<string> GetBodyAsString(this HttpContent content)
        {
            try
            {
                return await content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch
            {
                throw new UnsupportedBodyTypeException("The endpoint returned text in the body that is not parseable as Unicode.", content.GetContentType(), null);
            }
        }

        public static Uri? GetRequestUri(this HttpResponseMessage response) =>
            response?.RequestMessage?.RequestUri;

        public static string? GetVersionFromETag(this HttpResponseMessage response) =>
            response.Headers.ETag?.Tag.Trim('\"');

        public static void SetVersionFromETag(this HttpResponseMessage response, string versionId)
        {
            response.Headers.ETag = new EntityTagHeaderValue($"\"{versionId}\"", isWeak: true);
        }

        public static string? GetSecurityContext(this HttpResponseMessage response)
        {
            var success = response.Headers.TryGetValues(HttpUtil.SECURITYCONTEXT, out var secContexts);
            return success ? secContexts!.First() : null;  // Ignore the others if there are multiple. Sorry.
        }

        public static void SetSecurityContext(this HttpResponseMessage response, string context)
        {
            response.Headers.TryAddWithoutValidation(HttpUtil.SECURITYCONTEXT, context);
        }

        /// <summary>
        /// Gets the content type from the header, removing all parameters including character 
        /// encoding and fhirVersion parameter.
        /// </summary>
        public static string? GetContentType(this HttpContent content) =>
            content.Headers.ContentType?.MediaType;

        /// <summary>
        /// Represents the result of parsing and validating the data coming in from the response.
        /// </summary>
        /// <param name="Response">Headers relevant to FHIR, as a <see cref="Bundle.ResponseComponent"/>.</param>
        /// <param name="BodyData">The unparsed data from the response. Maybe null if the response had no body.</param>
        /// <param name="BodyText">The data from the response, decoded to text. Maybe null if the response did not contain UTF-8 decodable data.</param>
        /// <param name="BodyResource">The data from the response, decoded as a resource. Maybe null if the response was not a (correct) FHIR payload.</param>
        /// <param name="Issue">An issue encountered while parsing or validating.</param>
        internal record ResponseData(Bundle.ResponseComponent Response, byte[]? BodyData, string? BodyText, Resource? BodyResource, Exception? Issue);

        /// <summary>
        /// Extract headers into a <see cref="Bundle.ResponseComponent"/>, the body as a byte array, as text and when possible, a parsed resource.
        /// </summary>
        /// <returns>A <see cref="ResponseData"/> with a non-null <see cref="ResponseData.BodyResource"/> if the body contains FHIR data, 
        /// a non-null <see cref="ResponseData.BodyText"/> if the body is valid UTF-8 encoded text and the body data as a byte array, if there is a body.
        /// <see cref="ResponseData.Issue"/> will be a <see cref="DeserializationFailedException" /> when the Content-Type and serialization indicate this 
        /// is a FHIR payload, but it cannot be parsed correctly or a <see cref="UnsupportedBodyTypeException" /> when the Content-Type is not a FHIR
        /// serialization or the data is not recognizable as FHIR.</returns>
        /// <remarks>If the status of the response indicates failure, this function will be lenient and return the body data 
        /// instead of throwing an <see cref="UnsupportedBodyTypeException"/> when the content type or content itself is not recognizable as FHIR. This improves
        /// the chances of capturing diagnostic (non-FHIR) bodies returned by the server when an operation fails.</remarks>
        internal static async Task<ResponseData> ExtractResponseData(this HttpResponseMessage message, IFhirSerializationEngine ser, bool expectBinaryProtocol, FhirRelease fhirRelease)
        {
            var component = message.ExtractResponseComponent();
            var data = await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var result = new ResponseData(component, data.Length > 0 ? data : null, null, null, null);

            // If there is no data, we're done.
            if (data.Length == 0) return result;

            // If this is not binary data, try to capture the body as text
            if (!expectBinaryProtocol)
                result = result with { BodyText = await message.Content.GetBodyAsString().ConfigureAwait(false) };

            var usesFhirFormat = ContentType.GetResourceFormatFromContentType(message.Content.GetContentType()) != ResourceFormat.Unknown;

            try
            {
                var resource = message switch
                {
                    { IsSuccessStatusCode: true } when expectBinaryProtocol && !usesFhirFormat => await ReadBinaryDataFromMessage(message, fhirRelease).ConfigureAwait(false),
                    { IsSuccessStatusCode: true } => await ReadResourceFromMessage(message.Content, ser).ConfigureAwait(false),
                    { IsSuccessStatusCode: false } => await ReadOutcomeFromMessage(message.Content, ser).ConfigureAwait(false),
                };

                result = result with { BodyResource = resource };
            }
            catch (Exception e)
            {
                result = result with { Issue = e };
            }

            // Sets the Resource.ResourceBase to the location given in the RequestUri of the response message.
            if (result.BodyResource is not null && (message.Headers.Location?.OriginalString ?? message.GetRequestUri()?.OriginalString) is { } location)
            {
                var ri = new ResourceIdentity(location);
                result.BodyResource.ResourceBase = ri.HasBaseUri && ri.Form == ResourceIdentityForm.AbsoluteRestUrl
                    ? ri.BaseUri
                    : new Uri(location, UriKind.RelativeOrAbsolute);
            }

            return result;
        }

        /// <summary>
        /// Interprets the response as a response on a Binary endpoint where the server will stream the data
        /// in its native format, not packaged as a FHIR Binary resource.
        /// </summary>
        /// <returns>A <see cref="Binary"/> resource containing the streamed binary data. The resource's
        /// metadata will be retrieved from the appropriate HTTP headers.</returns>
        public static async Task<Binary> ReadBinaryDataFromMessage(this HttpResponseMessage message, FhirRelease fhirRelease)
        {
            var result = new Binary();

            if (fhirRelease == FhirRelease.STU3)
                result.Content = await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            else
                result.Data = await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            result.ContentType = message.Content.GetContentType();
            result.SecurityContext = message.GetSecurityContext() is { } reference ? new ResourceReference(reference) : null;
            result.Meta ??= new();
            result.Meta.LastUpdated = message.Content.Headers.LastModified;
            result.Meta.VersionId = message.GetVersionFromETag();

            // If the request indicates a Binary endpoint, try to get the id from the url
            if (message.GetRequestUri() is { } uri && HttpUtil.IsBinaryEndpoint(uri))
                result.Id = new ResourceIdentity(uri).Id;

            return result;
        }

        /// <summary>
        /// Interprets the response as a success result with a FHIR resource in the body.
        /// </summary>
        /// <returns>The resource, or <c>null</c> when the body is empty.</returns>
        /// <exception cref="UnsupportedBodyTypeException">When the body was not recognized as FHIR xml/json unicode text.</exception>
        /// <exception cref="DeserializationFailedException">When the serialization engine reported parse errors.</exception>
        public static async Task<Resource?> ReadResourceFromMessage(this HttpContent content, IFhirSerializationEngine ser)
        {
            var contentType = content.GetContentType();
            var bodyText = await content.GetBodyAsString().ConfigureAwait(false);
            if (bodyText.Length == 0) return null;

            return ContentType.GetResourceFormatFromContentType(contentType) switch
            {
                ResourceFormat.Xml when bodyText is not null && SerializationUtil.ProbeIsXml(bodyText) =>
                            ser.DeserializeFromXml(bodyText),
                ResourceFormat.Json when bodyText is not null && SerializationUtil.ProbeIsJson(bodyText) =>
                            ser.DeserializeFromJson(bodyText),
                ResourceFormat.Xml or ResourceFormat.Json =>
                            throw new UnsupportedBodyTypeException(
                       $"Endpoint said it returned '{contentType}', but the body is not recognized as either xml or json.", contentType, bodyText),
                ResourceFormat.Unknown =>
                            throw new UnsupportedBodyTypeException(
                       $"Endpoint returned a body with contentType '{contentType}', " +
                       $"while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?",
                       contentType, bodyText),
                var unsupported => throw new InvalidOperationException($"Cannot handle the specified resource format {unsupported}")
            };
        }

        /// <summary>
        /// Tries to retrieve a FHIR resource from the body of a failed request.
        /// </summary>
        /// <returns>A resource if it found one, or <c>null</c> if the body was empty or not recognizable as a FHIR resource.</returns>
        /// <exception cref="DeserializationFailedException">Thrown when the body contains FHIR xml/json, but the serialization engine reported parse errors.</exception>
        public static async Task<Resource?> ReadOutcomeFromMessage(this HttpContent content, IFhirSerializationEngine ser)
        {
            var contentType = content.GetContentType();
            string? bodyText;

            try
            {
                bodyText = await content.GetBodyAsString().ConfigureAwait(false);
            }
            catch
            {
                // It's not even unicode, let's stop here.
                return null;
            }

            // Empty body, return null
            if (bodyText.Length == 0) return null;

            return ContentType.GetResourceFormatFromContentType(contentType) switch
            {
                ResourceFormat.Xml when bodyText is not null && SerializationUtil.ProbeIsFhirXml(bodyText) =>
                            ser.DeserializeFromXml(bodyText),
                ResourceFormat.Json when bodyText is not null && SerializationUtil.ProbeIsFhirJson(bodyText) =>
                            ser.DeserializeFromJson(bodyText),
                _ => default
            };
        }


        /// <summary>
        /// Somewhat quircky, but the useful UnsupportedBodyTypeException is never thrown to the user, instead it is
        /// packaged inside a FhirOperationException. We're sticking to the design here for backwards-compatibility reasons.
        /// </summary>
        internal static ResponseData TranslateUnsupportedBodyTypeException(this ResponseData responseData, HttpStatusCode status)
        {
            if (responseData.Issue is UnsupportedBodyTypeException ubte)
            {
                OperationOutcome operationOutcome = OperationOutcome.ForException(ubte, OperationOutcome.IssueType.Invalid);
                return responseData with { Issue = FhirOperationException.BuildFhirOperationException(status, operationOutcome) };
            }
            else
                return responseData;
        }

        /// <summary>
        /// The new <see cref="IFhirSerializationEngine"/> interface expects all implementations to throw
        /// <see cref="DeserializationFailedException"/>. If the serialization engine is the "old" engine, 
        /// it will have packaged its exception as an DeserializationFailedException to comply to this new
        /// contract. So, if the FhirClient is configured to use the old (original) serializers, we should 
        /// unpack the original FormatException.
        /// </summary>
        internal static ResponseData TranslateLegacyParserException(this ResponseData responseData, string? suggestedVersionOnParseError)
        {
            if (responseData.Issue is DeserializationFailedException dfe)
            {
                // Call this helper on the IFhirSerializationEngine implementation of the old parser, to find out
                // if this is indeed a legacy exception.
                var isLegacyException = ElementModelSerializationEngine.TryUnpackElementModelException(dfe, out var legacyException);

                // Here, we augment the original exception with extra text if a parsing failure occurred, and we have not checked
                // whether we are actually talking to a server using the samen version of FHIR as us. This worked fine with
                // the ElementModel parsing exceptions, but you cannot do that with the DeserializationFailedException,
                // so that will be thrown as-is.
                return isLegacyException switch
                {
                    true when suggestedVersionOnParseError is not null =>
                        responseData with
                        {
                            Issue =
                            new StructuralTypeException(legacyException!.Message + Environment.NewLine +
                                    $"Are you connected to a FHIR server with FHIR version {suggestedVersionOnParseError}? " +
                                    "Try the FhirClientSetting.VerifyFhirVersion to ensure that you are connected to a FHIR server with the correct FHIR version.")
                        },
                    true =>
                        responseData with { Issue = legacyException },
                    false => responseData
                };
            }
            else
                return responseData;
        }
    }
}

#nullable restore