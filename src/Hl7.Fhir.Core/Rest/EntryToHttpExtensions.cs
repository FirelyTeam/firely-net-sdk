/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using System.Collections.Generic;
using System.Net.Http;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToHttpExtensions
    {
        public static HttpWebRequest ToHttpRequest(this Bundle.EntryComponent entry, 
            Prefer bodyPreference, ResourceFormat format, bool useFormatParameter, bool CompressRequestBody, out byte[] body)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", entry.Request.Method, entry.Request.Url);

            var interaction = entry.Request;
            body = null;

            if (entry.Resource != null && !(interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT))
                throw Error.InvalidOperation("Cannot have a body on an Http " + interaction.Method.ToString());

            var location = new RestUrl(interaction.Url);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = (HttpWebRequest)HttpWebRequest.Create(location.Uri);
            request.Method = interaction.Method.ToString();
            setAgent(request, ".NET FhirClient for FHIR " + Model.ModelInfo.Version);

            if (!useFormatParameter)
                request.Accept = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);

            if (interaction.IfMatch != null) request.Headers["If-Match"] = interaction.IfMatch;
            if (interaction.IfNoneMatch != null) request.Headers["If-None-Match"] = interaction.IfNoneMatch;
#if DOTNETFW
            if (interaction.IfModifiedSince != null) request.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
#else
            if (interaction.IfModifiedSince != null) request.Headers["If-Modified-Since"] = interaction.IfModifiedSince.Value.UtcDateTime.ToString();
#endif
            if (interaction.IfNoneExist != null) request.Headers["If-None-Exist"] = interaction.IfNoneExist;

            if (interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT)
            {
                request.Headers["Prefer"] = bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation";
            }
            
            if (entry.Resource != null)
            {
                bool searchUsingPost =
                    interaction.Method == Bundle.HTTPVerb.POST
                    && (entry.HasAnnotation<TransactionBuilder.InteractionType>()
                    && entry.Annotation<TransactionBuilder.InteractionType>() == TransactionBuilder.InteractionType.Search)
                    && entry.Resource is Parameters;

                setBodyAndContentType(request, entry.Resource, format, CompressRequestBody, searchUsingPost, out body);
            }
            // PCL doesn't support setting the length (and in this case will be empty anyway)
#if DOTNETFW
            else
                request.ContentLength = 0;
#endif
            return request;
        }


        /// <summary>
        /// Flag to control the setting of the User Agent string (different platforms have different abilities)
        /// </summary>
        public static bool SetUserAgentUsingReflection = true;
        public static bool SetUserAgentUsingDirectHeaderManipulation = true;

        private static void setAgent(HttpWebRequest request, string agent)
        {
            bool userAgentSet = false;
            if (SetUserAgentUsingReflection)
            {
                try
                {
					System.Reflection.PropertyInfo prop = request.GetType().GetRuntimeProperty("UserAgent");

                    if (prop != null)
                        prop.SetValue(request, agent, null);
                    userAgentSet = true;
                }
                catch (Exception)
                {
                    // This approach doesn't work on this platform, so don't try it again.
                    SetUserAgentUsingReflection = false;
                }
            }
            if (!userAgentSet && SetUserAgentUsingDirectHeaderManipulation)
            {
                // platform does not support UserAgent property...too bad
                try
                {
                    request.Headers[HttpRequestHeader.UserAgent] = agent;
                }
                catch (ArgumentException)
                {
                    SetUserAgentUsingDirectHeaderManipulation = false;
                }
            }
        }


        private static void setBodyAndContentType(HttpWebRequest request, Resource data, ResourceFormat format, bool CompressRequestBody, bool searchUsingPost, out byte[] body)
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

                request.ContentType = "application/x-www-form-urlencoded";
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
