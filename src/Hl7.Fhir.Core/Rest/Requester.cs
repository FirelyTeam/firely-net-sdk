/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal class Requester<TOperationOutcome> where TOperationOutcome : Resource
    {
        public Uri BaseUrl { get; private set; }
        public string FhirVersion { get; private set; }
        public bool UseFormatParameter { get; set; }
        public ResourceFormat PreferredFormat { get; set; }
        public int Timeout { get; set; }           // In milliseconds
        public Prefer Prefer { get; set; }

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

        public ParserSettings ParserSettings { get; set; }

        public Requester(Uri baseUrl, string fhirVersion, Func<Exception, TOperationOutcome> operationOutcomeFromException, Func<byte[], string, Resource> makeBinaryResource)
        {
            BaseUrl = baseUrl;
            FhirVersion = fhirVersion;
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Timeout = 100 * 1000;       // Default timeout is 100 seconds            
            Prefer = Rest.Prefer.ReturnRepresentation;
            ParserSettings = Hl7.Fhir.Serialization.ParserSettings.Default;
            _operationOutcomeFromException = operationOutcomeFromException;
            _makeBinaryResource = makeBinaryResource;
        }


        public Response LastResult { get; private set; }
        public HttpWebResponse LastResponse { get; private set; }
        public HttpWebRequest LastRequest { get; private set; }
        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }

        private readonly Func<Exception, TOperationOutcome> _operationOutcomeFromException;
        private readonly Func<byte[], string, Resource> _makeBinaryResource;

        public Response Execute(Request interaction)
        {
            return ExecuteAsync(interaction).WaitResult();
        }
        public async Task<Response> ExecuteAsync(Request interaction)
        {
            if (interaction == null) throw Error.ArgumentNull(nameof(interaction));
            bool compressRequestBody = false;

            compressRequestBody = CompressRequestBody; // PCL doesn't support compression at the moment

            byte[] outBody;
            var request = interaction.ToHttpRequest(FhirVersion, Prefer, PreferredFormat, UseFormatParameter, compressRequestBody, out outBody);

#if DOTNETFW
            request.Timeout = Timeout;
#endif

            if (PreferCompressedResponses)
            {
                request.Headers["Accept-Encoding"] = "gzip, deflate";
            }

            LastRequest = request;
            if (BeforeRequest != null) BeforeRequest(request, outBody);

            // Write the body to the output
            if (outBody != null)
                request.WriteBody(compressRequestBody, outBody);

            // Make sure the HttpResponse gets disposed!
            using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)).ConfigureAwait(false))
            //using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
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
                            LastResult = Response.FromHttpResponse( webResponse, inBody, ParserSettings, _makeBinaryResource, throwOnFormatException: true);
                            return LastResult;
                        }
                        else
                        {
                            LastResult = Response.FromHttpResponse( webResponse, inBody, ParserSettings, _makeBinaryResource, throwOnFormatException: false);
                            throw buildFhirOperationException(webResponse.StatusCode, LastResult.Resource);
                        }
                    }
                    catch(UnsupportedBodyTypeException bte)
                    {
                        // The server responded with HTML code. Still build a FhirOperationException and set a LastResult.
                        // Build a very minimal LastResult
                        var errorResult = new Response();
                        errorResult.Status = ((int)webResponse.StatusCode).ToString();


                        errorResult.Resource = _operationOutcomeFromException(bte);
                        LastResult = errorResult;

                        throw buildFhirOperationException(webResponse.StatusCode, errorResult.Resource);
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
#if !DOTNETFW
                var contentEncoding = response.Headers["Content-Encoding"];
#else
                var contentEncoding = response.ContentEncoding;
#endif
                if (contentEncoding == "gzip")
                {
                    using (var decompressed = new GZipStream(respStream, CompressionMode.Decompress, true))
                    {
                        body = HttpUtil.ReadAllFromStream(decompressed);
                    }
                }
                else if (contentEncoding == "deflate")
                {
                    using (var decompressed = new DeflateStream(respStream, CompressionMode.Decompress, true))
                    {
                        body = HttpUtil.ReadAllFromStream(decompressed);
                    }
                }
                else
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


        private Exception buildFhirOperationException(HttpStatusCode status, Resource body)
        {
            string message;

            if (status.IsInformational())
                message = $"Operation resulted in an informational response ({status})";
            else if (status.IsRedirection())
                message = $"Operation resulted in a redirection response ({status})";
            else if (status.IsClientError())
                message = $"Operation was unsuccessful because of a client error ({status})";
            else
                message = $"Operation was unsuccessful, and returned status {status}";

            if (body is TOperationOutcome outcome)
                return new FhirOperationException<TOperationOutcome>($"{message}. OperationOutcome: {outcome.ToString()}.", status, outcome);
            else if (body != null)
                return new FhirOperationException<TOperationOutcome>($"{message}. Body contains a {body.TypeName}.", status);
            else
                return new FhirOperationException<TOperationOutcome>($"{message}. Body has no content.", status);
        }
    }
}
