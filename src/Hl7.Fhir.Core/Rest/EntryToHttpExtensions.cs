/*
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Net;
using System.Reflection;
using Hl7.Fhir.Utility;
using System.Net.Http;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToHttpExtensions
    {
        public static HttpRequestMessage ToHttpRequest(this Bundle.EntryComponent entry,
            Prefer bodyPreference, ResourceFormat format, bool useFormatParameter, bool CompressRequestBody, HttpMethod method)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", entry.Request.Method, entry.Request.Url);

            var interaction = entry.Request;

            if (entry.Resource != null && !(interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT))
                throw Error.InvalidOperation("Cannot have a body on an Http " + interaction.Method.ToString());

            var location = new RestUrl(interaction.Url);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = new HttpRequestMessage(method, location.Uri);
            request.Headers.Add("User-Agent", ".NET FhirClient for FHIR " + Model.ModelInfo.Version);

            if (!useFormatParameter)
                request.Headers.Add("Accept", Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false));

            if (interaction.IfMatch != null) request.Headers.TryAddWithoutValidation("If-Match", interaction.IfMatch);
            if (interaction.IfNoneMatch != null) request.Headers.TryAddWithoutValidation("If-None-Match", interaction.IfNoneMatch);
            if (interaction.IfModifiedSince != null) request.Headers.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
            if (interaction.IfNoneExist != null) request.Headers.TryAddWithoutValidation("If-None-Exist", interaction.IfNoneExist);

            if (interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT)
            {
                request.Headers.TryAddWithoutValidation("Prefer", bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation");
            }

            if (entry.Resource != null)
                setBodyAndContentType(request, entry.Resource, format, CompressRequestBody, out body);
            // PCL doesn't support setting the length (and in this case will be empty anyway)
#if DOTNETFW
            else
                request.Content.Headers.ContentLength = 0;
#endif
            return request;
        }

        private static void setBodyAndContentType(HttpRequestMessage request, Resource data, ResourceFormat format, bool CompressRequestBody)
        {
            if (data == null) throw Error.ArgumentNull(nameof(data));

            if (data is Binary)
            {
                var bin = (Binary)data;
                body = bin.Content;
                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, bin.Content);
                request.ContentType = bin.ContentType;
            }
            else
            {
                body = format == ResourceFormat.Xml ?
                    new FhirXmlSerializer().SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False) :
                    new FhirJsonSerializer().SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False);

                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, body);
                request.ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }
        }


    }
}
