/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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
    internal enum InteractionType
    {
        Search,
        Unspecified,
        Read,
        VRead,
        Update,
        Delete,
        Create,
        Capabilities,
        History,
        Operation,
        Transaction
    }

    public class Request
    {
        public Model.HTTPVerb Method { get; set; }
        public string Url { get; set; }
        public string IfMatch { get; set; }
        public string IfNoneMatch { get; set; }
        public DateTimeOffset? IfModifiedSince { get; set; }
        public string IfNoneExist { get; set; }
        public Model.Resource Resource { get; set; }
        internal InteractionType? Interaction { get; set; }

        public bool IsPostOrPut()
        {
            return Method == Model.HTTPVerb.POST || Method == Model.HTTPVerb.PUT;
        }

        public HttpWebRequest ToHttpRequest(
            Uri baseUrl,
            Model.Version version,
            string fhirVersion,
            Prefer bodyPreference,
            ResourceFormat format,
            bool useFormatParameter,
            bool CompressRequestBody,
            out byte[] body
        )
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", Method, Url);

            body = null;

            if (Resource != null && !(Method == Model.HTTPVerb.POST || Method == Model.HTTPVerb.PUT))
                throw Utility.Error.InvalidOperation("Cannot have a body on an Http " + Method.ToString());

            // Create an absolute uri when the interaction.Url is relative.
            var uri = new Uri(Url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                uri = HttpUtil.MakeAbsoluteToBase(uri, baseUrl);
            }
            var location = new RestUrl(uri);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = (HttpWebRequest)HttpWebRequest.Create(location.Uri);
            request.Method = Method.ToString();
            setAgent(request, ".NET FhirClient for FHIR " + fhirVersion);

            if (!useFormatParameter)
                request.Accept = Hl7.Fhir.Rest.ContentType.BuildContentType(format, version, forBundle: false);

            if (IfMatch != null) request.Headers["If-Match"] = IfMatch;
            if (IfNoneMatch != null) request.Headers["If-None-Match"] = IfNoneMatch;
#if NETSTANDARD1_1
            if (IfModifiedSince != null) request.Headers["If-Modified-Since"] = IfModifiedSince.Value.UtcDateTime.ToString();
#else
            if (IfModifiedSince != null) request.IfModifiedSince = IfModifiedSince.Value.UtcDateTime;
#endif
            if (IfNoneExist != null) request.Headers["If-None-Exist"] = IfNoneExist;

            if (Method == Model.HTTPVerb.POST || Method == Model.HTTPVerb.PUT)
            {
                request.Headers["Prefer"] = bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation";
            }

            if (Resource != null)
            {
                bool searchUsingPost =
                     Method == HTTPVerb.POST
                     && Interaction == InteractionType.Search
                     && Resource is Parameters;

                setBodyAndContentType(request, version, Resource, format, CompressRequestBody, searchUsingPost, out body);
            }
            // PCL doesn't support setting the length (and in this case will be empty anyway)
#if !NETSTANDARD1_1
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

        private static void setBodyAndContentType(HttpWebRequest request, Model.Version version, Model.Resource data, ResourceFormat format, bool CompressRequestBody, bool searchUsingPost, out byte[] body)
        {
            if (data == null) throw Utility.Error.ArgumentNull(nameof(data));

            if (data is Model.IBinary bin)
            {
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
                    new Serialization.FhirXmlSerializer(version).SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False) :
                    new Serialization.FhirJsonSerializer(version).SerializeToBytes(data, summary: Fhir.Rest.SummaryType.False);

                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, body);
                request.ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, version, forBundle: false);
            }
        }
    }
}
