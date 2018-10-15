/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToHttpExtensions
    {
        public static HttpRequestMessage ToHttpRequestMessage(this Bundle.EntryComponent entry, Uri baseUrl,
            Prefer bodyPreference, ResourceFormat format, bool useFormatParameter, bool CompressRequestBody)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", entry.Request.Method, entry.Request.Url);

            var interaction = entry.Request;

            if (entry.Resource != null && !(interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT))
                throw Error.InvalidOperation("Cannot have a body on an Http " + interaction.Method.ToString());

            var uri = new Uri(interaction.Url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                uri = HttpUtil.MakeAbsoluteToBase(uri, baseUrl);
            }
            var location = new RestUrl(uri);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = new HttpRequestMessage(getMethod(interaction.Method), location.Uri);

            if (!useFormatParameter)
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType.BuildContentType(format, forBundle: false)));

            if (interaction.IfMatch != null) request.Headers.Add("If-Match", interaction.IfMatch);
            if (interaction.IfNoneMatch != null) request.Headers.Add("If-None-Match", interaction.IfNoneMatch);
            if (interaction.IfModifiedSince != null) request.Headers.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
            if (interaction.IfNoneExist != null) request.Headers.Add("If-None-Exist", interaction.IfNoneExist);

            if (interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT)
            {
                request.Headers.Add("Prefer", bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation" );
            }

            if (entry.Resource != null)
            {
                bool searchUsingPost =
                   interaction.Method == Bundle.HTTPVerb.POST
                   && (entry.HasAnnotation<TransactionBuilder.InteractionType>()
                   && entry.Annotation<TransactionBuilder.InteractionType>() == TransactionBuilder.InteractionType.Search)
                   && entry.Resource is Parameters;
                setBodyAndContentType(request, entry.Resource, format, CompressRequestBody, searchUsingPost);
            }


            return request;
        }

        /// <summary>
        /// Converts bundle http verb to corresponding <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="verb"><see cref="Bundle.HTTPVerb"/> specified by input bundle.</param>
        /// <returns><see cref="HttpMethod"/> corresponding to verb specified in input bundle.</returns>
        private static HttpMethod getMethod(Bundle.HTTPVerb? verb)
        {
            switch (verb)
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

        private static void setBodyAndContentType(HttpRequestMessage request, Resource data, ResourceFormat format, bool CompressRequestBody, bool searchUsingPost)
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
            else if (searchUsingPost)
            {
                IDictionary<string, string> bodyParameters = new Dictionary<string, string>();
                foreach(Parameters.ParameterComponent parameter in ((Parameters)data).Parameter)
                {
                    bodyParameters.Add(parameter.Name, parameter.Value.ToString());
                }
                if (bodyParameters.Count > 0)
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(bodyParameters);
                    body = content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                }
                else
                {
                    body = null;
                }

                contentType = "application/x-www-form-urlencoded";
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

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        }
    }
}
