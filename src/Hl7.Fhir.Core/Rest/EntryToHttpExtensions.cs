/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace Hl7.Fhir.Rest
{
    internal static class EntryToHttpExtensions
    {
        public static HttpWebRequest ToHttpRequest(this Bundle.EntryComponent entry, 
            Prefer bodyPreference, ResourceFormat format, bool useFormatParameter, out byte[] body)
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
#if PORTABLE45
            if (interaction.IfModifiedSince != null) request.Headers["If-Modified-Since"] = interaction.IfModifiedSince.Value.UtcDateTime.ToString();
#else
            if (interaction.IfModifiedSince != null) request.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
#endif
            if (interaction.IfNoneExist != null) request.Headers["If-None-Exist"] = interaction.IfNoneExist;

            if (interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT)
            {
                request.Headers["Prefer"] = bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation";
            }

            if (entry.Resource != null) setBodyAndContentType(request, entry.Resource, format, out body);

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
#if PORTABLE45
					System.Reflection.PropertyInfo prop = request.GetType().GetRuntimeProperty("UserAgent");
#else
                    System.Reflection.PropertyInfo prop = request.GetType().GetProperty("UserAgent");
#endif

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


        private static void setBodyAndContentType(HttpWebRequest request, Resource data, ResourceFormat format, out byte[] body)
        {
            if (data == null) throw Error.ArgumentNull("data");

            if (data is Binary)
            {
                var bin = (Binary)data;
                body = bin.Content;
                request.WriteBody(bin.Content);
                request.ContentType = bin.ContentType;
            }
            else
            {
                body = format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeToXmlBytes(data, summary: false) :
                    FhirSerializer.SerializeToJsonBytes(data, summary: false);

                request.WriteBody(body);
                request.ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }
        }

      
    }
}
