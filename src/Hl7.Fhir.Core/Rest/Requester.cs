﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.IO.Compression;
using System.Net;

namespace Hl7.Fhir.Rest
{
    internal class Requester
    {
        public Uri BaseUrl { get; private set; }

        public bool UseFormatParameter { get; set; }
        public ResourceFormat PreferredFormat { get; set; }
        public int Timeout { get; set; }           // In milliseconds
        public Prefer Prefer { get; set; }
#if !PORTABLE45 && !NETCore
        /// <summary>
        /// This will do 2 things:
        /// 1. Add the header Accept-Encoding: gzip, deflate
        /// 2. decompress any responses that have Content-Encoding: gzip (or deflate)
        /// </summary>
        public bool PreferCompressedResponses { get; set; }
        /// <summary>
        /// Compress any Request bodies 
        /// (warning, if a server does not handle compressed requests you will get a 415 response)
        /// </summary>
        public bool CompressRequestBody { get; set; }
#endif

        public ParserSettings ParserSettings { get; set; }

        public Requester(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Timeout = 100 * 1000;       // Default timeout is 100 seconds            
            Prefer = Rest.Prefer.ReturnRepresentation;
            ParserSettings = Hl7.Fhir.Serialization.ParserSettings.Default;
        }


        public Bundle.EntryComponent LastResult { get; private set; }
        public HttpWebResponse LastResponse { get; private set; }
        public HttpWebRequest LastRequest { get; private set; }
        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }



        public Bundle.EntryComponent Execute(Bundle.EntryComponent interaction)
        {
            if (interaction == null) throw Error.ArgumentNull("interaction");
            bool compressRequestBody = false;
#if !PORTABLE45 && !NETCore
            compressRequestBody = CompressRequestBody; // PCL doesn't support compression at the moment
#endif

            byte[] outBody;
            var request = interaction.ToHttpRequest(Prefer, PreferredFormat, UseFormatParameter, compressRequestBody, out outBody);

#if !PORTABLE45 && !NETCore
            request.Timeout = Timeout;
            if (PreferCompressedResponses)
            {
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
            }
#endif

            LastRequest = request;
            if (BeforeRequest != null) BeforeRequest(request, outBody);

            // Make sure the HttpResponse gets disposed!
            // using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)))
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    //Read body before we call the hook, so the hook cannot read the body before we do
                    var inBody = readBody(webResponse);

                    LastResponse = webResponse;
                    if (AfterResponse != null) AfterResponse(webResponse,inBody);

                    // Do this call after AfterResponse, so AfterResponse will be called, even if exceptions are thrown by ToBundleEntry()
                    try
                    {
                        LastResult = null;

                        if (webResponse.StatusCode.IsSuccessful())
                        {
                            LastResult = webResponse.ToBundleEntry(inBody, ParserSettings, throwOnFormatException: true);
                            return LastResult;
                        }
                        else
                        {
                            LastResult = webResponse.ToBundleEntry(inBody, ParserSettings, throwOnFormatException: false);
                            throw httpNonSuccessStatusToException(webResponse.StatusCode, LastResult.Resource);
                        }
                    }
                    catch(UnsupportedBodyTypeException bte)
                    {
                        // The server responded with HTML code. Still build a FhirOperationException and set a LastResult.
                        // Build a very minimal LastResult
                        var errorResult = new Bundle.EntryComponent();
                        errorResult.Response = new Bundle.ResponseComponent();
                        errorResult.Response.Status = ((int)webResponse.StatusCode).ToString();

                        OperationOutcome operationOutcome = OperationOutcome.ForException(bte, OperationOutcome.IssueType.Invalid);

                        errorResult.Resource = operationOutcome;
                        LastResult = errorResult;

                        throw buildFhirOperationException(webResponse.StatusCode, operationOutcome);
                    }
                }
                catch (AggregateException ae)
                {
                    //EK: This code looks weird. Is this correct?
                    if (ae.GetBaseException() is WebException)
                    {
                    }
                    throw ae.GetBaseException();
                }
            }
        }

        private static byte[] readBody(HttpWebResponse response)
        {
            if (response.ContentLength != 0)
            {
                byte[] body = null;
                var respStream = response.GetResponseStream();
#if !PORTABLE45 && !NETCore
                if (response.ContentEncoding == "gzip")
                {
                    using (var decompressed = new GZipStream(respStream, CompressionMode.Decompress, true))
                    {
                        body = HttpUtil.ReadAllFromStream(decompressed);
                    }
                }
                else if (response.ContentEncoding == "deflate")
                {
                    using (var decompressed = new DeflateStream(respStream, CompressionMode.Decompress, true))
                    {
                        body = HttpUtil.ReadAllFromStream(decompressed);
                    }
                }
                else
#endif
                {
                    body = HttpUtil.ReadAllFromStream(respStream);
                }
                respStream.Dispose();

                if (body.Length > 0)
                    return body;
                else
                    return null;
            }
            else
                return null;
        }



        /// <summary>
        /// Convert a status code into an exception, or null if everything is fine.
        /// </summary>
        /// <param name="status">HTTP status code</param>
        /// <param name="body">Content delivered by the server, parsed as a FHIR resource</param>
        /// <returns></returns>
        private static Exception httpNonSuccessStatusToException(HttpStatusCode status, Resource body)
        {
            if (status.IsInformational() || status.IsRedirection())      // 1xx and 3xx codes - we don't handle them, unless the .NET API did it for us
            {
                return Error.NotSupported("Server returned a status code '{0}', which is not supported by the FhirClient".FormatWith(status));
            }
            else if (status.IsClientError() || status.IsServerError())      // 4xx/5xx codes - client or server error.
            {
                return buildFhirOperationException(status, body);
            }
            else
            {
                return Error.NotSupported("Server returned an illegal http status code '{0}', which is not defined by the Http standard".FormatWith(status));
            }
        }

        private static Exception buildFhirOperationException(HttpStatusCode status, Resource body)
        {
            var message = string.Format("Operation was unsuccessful, and returned status {0}.", status);
            var outcome = body as OperationOutcome;

            if (outcome != null)
            {
                // Body is an OperationOutcome
                return new FhirOperationException(message + " OperationOutcome: " + outcome.ToString(), status, outcome);
            }
            else if (body != null)
            {
                return new FhirOperationException(message + " Body contains a " + body.TypeName, status);
            }
            else
            {
                return new FhirOperationException(message + " Body is null", status);
            }
        }
    }
}
