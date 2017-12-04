/*
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace Hl7.Fhir.Rest.Http
{
    internal static class EntryToHttpExtensions
    {
        public static HttpRequestMessage ToHttpRequestMessage(this Bundle.EntryComponent entry, 
            SearchParameterHandling? handlingPreference, Prefer? returnPreference, ResourceFormat format, bool useFormatParameter, bool CompressRequestBody)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", entry.Request.Method, entry.Request.Url);

            var interaction = entry.Request;

            if (entry.Resource != null && !(interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT))
                throw Error.InvalidOperation("Cannot have a body on an Http " + interaction.Method.ToString());

            var location = new RestUrl(interaction.Url);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = new HttpRequestMessage(getMethod(interaction.Method), location.Uri);

            if (!useFormatParameter)
                request.Headers.Add("Accept", Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false));

            if (interaction.IfMatch != null) request.Headers.TryAddWithoutValidation("If-Match", interaction.IfMatch);
            if (interaction.IfNoneMatch != null) request.Headers.TryAddWithoutValidation("If-None-Match", interaction.IfNoneMatch);
            if (interaction.IfModifiedSince != null) request.Headers.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
            if (interaction.IfNoneExist != null) request.Headers.TryAddWithoutValidation("If-None-Exist", interaction.IfNoneExist);

            var interactionType = entry.Annotation<TransactionBuilder.InteractionType>();

            if (interactionType == TransactionBuilder.InteractionType.Create && returnPreference != null)
                request.Headers.TryAddWithoutValidation("Prefer", "return=" + PrimitiveTypeConverter.ConvertTo<string>(returnPreference));
            else if (interactionType == TransactionBuilder.InteractionType.Search && handlingPreference != null)
                request.Headers.TryAddWithoutValidation("Prefer", "handling=" + PrimitiveTypeConverter.ConvertTo<string>(handlingPreference));

            if (entry.Resource != null)
                setBodyAndContentType(request, entry.Resource, format, CompressRequestBody);

            return request;
        }

        /// <summary>
        /// Converts bundle http verb to corresponding <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="verb"><see cref="Bundle.HttpVerb"/> specified by input bundle.</param>
        /// <returns><see cref="HttpMethod"/> corresponding to verb specified in input bundle.</returns>
        private static HttpMethod getMethod(Bundle.HTTPVerb? verb)
        {
            switch(verb)
            {
                case Bundle.HTTPVerb.GET:
                    return HttpMethod.Get;
                case Bundle.HTTPVerb.POST:
                    return HttpMethod.Post;
                case Bundle.HTTPVerb.PUT:
                    return HttpMethod.Put;
                case Bundle.HTTPVerb.DELETE:
                    return HttpMethod.Delete;
            }
            throw new HttpRequestException($"Valid HttpVerb could not be found for verb type: [{verb}]");
        }

        private static void setBodyAndContentType(HttpRequestMessage request, Resource data, ResourceFormat format, bool CompressRequestBody)
        {
            if (data == null) throw Error.ArgumentNull(nameof(data));

            byte[] body;
            string contentType;

            if (data is Binary bin)
            {
                body = bin.Content;
                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, bin.Content);
                contentType = bin.ContentType;
            }
            else
            {
                body = format == ResourceFormat.Xml ?
                    new FhirXmlSerializer().SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False) :
                    new FhirJsonSerializer().SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False);

                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, body);
                contentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }

            request.Content = new ByteArrayContent(body);

            // MediaTypeHeaderValue cannot accept a content type that contains charset at the end, so that value must be split out.
            var contentTypeList = contentType.Split(';');
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentTypeList.FirstOrDefault());
            request.Content.Headers.ContentType.CharSet = System.Text.Encoding.UTF8.WebName;
        }


    }
}
