/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.WebApi
{
    public static class HttpRequestFhirExtensions
    {
        /// <summary>
        /// Get the name from the User Principal (if no name, the ID is returned from the claims)
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string GetUserName(this System.Security.Principal.IPrincipal User)
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(User.Identity.Name))
                    return User.Identity.Name;

                // read additional details from the identity claims
                var ci = User.Identity as System.Security.Claims.ClaimsIdentity;
                if (ci != null)
                {
                    var claim = ci.Claims.Where(c => c.Type == "name").FirstOrDefault();
                    if (claim != null && !string.IsNullOrEmpty(claim.Value))
                        return claim.Value;
                    claim = ci.Claims.Where(c => c.Type == "sub").FirstOrDefault();
                    if (claim != null)
                        return claim.Value;
                }

                // Fallback to old code
                return "(other)";
            }
            return null;
        }

        public static void SaveBody(this HttpRequestMessage request, string contentType, byte[] data)
        {
            Binary b = new Binary { Content = data, ContentType = contentType };

            request.Properties.Add(Const.UNPARSED_BODY, b);
        }

        public static Binary GetBody(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(Const.UNPARSED_BODY))
                return request.Properties[Const.UNPARSED_BODY] as Binary;
            else
                return null;
        }


        /// <summary>
        /// Temporary hack!
        /// Adds a resourceEntry to the request property bag. To be picked up by the MediaTypeFormatters for adding http headers.
        /// </summary>
        /// <param name="entry">The resource entry with information to generate headers</param>
        /// <remarks> 
        /// The SendAsync is called after the headers are set. The SetDefaultHeaders have no access to the content object.
        /// The only solution is to give the information through the Request Property Bag.
        /// </remarks>
        
        public static void SaveEntry(this HttpRequestMessage request, Resource entry)
        {
            request.Properties.Add(Const.RESOURCE_ENTRY, entry);
        }

        public static Resource GetEntry(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(Const.RESOURCE_ENTRY))
                return request.Properties[Const.RESOURCE_ENTRY] as Resource;
//            if (request.Properties.ContainsKey(Const.UNPARSED_BODY))
//                return request.Properties[Const.UNPARSED_BODY] as Resource;
            return null;
        }

        public static HttpResponseMessage ResourceResponse(this HttpRequestMessage request, Resource entry, HttpStatusCode? code=null)
        {
            request.SaveEntry(entry);

            HttpResponseMessage msg;
            
            if(code != null)
                msg = request.CreateResponse<Resource>(code.Value,entry);
            else
                msg = request.CreateResponse<Resource>(entry);

            // msg.Headers.SetFhirTags(entry.Tags);
            return msg;
        }


        public static HttpResponseMessage StatusResponse(this HttpRequestMessage request, Resource entry, HttpStatusCode code)
        {
            request.SaveEntry(entry);
            HttpResponseMessage msg = request.CreateResponse(code);
            msg.Headers.Location = entry.ResourceIdentity();
            return msg;
        }

        public static DateTimeOffset? GetDateParameter(this HttpRequestMessage request, string name)
        {
            string param = request.GetParameter(name);
            if (param == null) return null;
            var t = new Hl7.Fhir.Model.FhirDateTime(param);
            return t.ToDateTimeOffset();
        }

        public static int? GetIntParameter(this HttpRequestMessage request, string name)
        {
            string s = request.GetParameter(name);
            int n;
            return (int.TryParse(s, out n)) ? n : (int?)null;
        }

        public static bool? GetBooleanParameter(this HttpRequestMessage request, string name)
        {
            string s = request.GetParameter(name);           
            if(s == null) return null;

            bool b;
            try
            {
                b = PrimitiveTypeConverter.ConvertTo<bool>(s.ToLower());
                return b;
            }
            catch
            {
            }

            if (bool.TryParse(s.ToLower(), out b))
                return b;
            return (bool?)null;
        }
    }
}