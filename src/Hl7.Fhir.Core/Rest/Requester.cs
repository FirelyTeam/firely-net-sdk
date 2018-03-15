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
    internal class Requester : IRequester
    {
        public Uri BaseUrl { get; private set; }

        public FhirClientSettings Settings { get; set; }

        public Requester(Uri baseUrl, FhirClientSettings settings)
        {
            BaseUrl = baseUrl;
            Settings = settings;
        }

        public Bundle.EntryComponent LastResult { get; private set; }
        public HttpStatusCode? LastStatusCode => LastResponse?.StatusCode;
        public HttpWebResponse LastResponse { get; private set; }
        public HttpWebRequest LastRequest { get; private set; }
        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }

        public async Task<Bundle.EntryComponent> ExecuteAsync(Bundle.EntryComponent interaction)
        {
            if (interaction == null) throw Error.ArgumentNull(nameof(interaction));
            bool compressRequestBody = false;

            compressRequestBody = Settings.CompressRequestBody; // PCL doesn't support compression at the moment

            var request = interaction.ToHttpRequest(Settings.PreferredParameterHandling, Settings.PreferredReturn, Settings.PreferredFormat, Settings.UseFormatParameter, compressRequestBody, out byte[] outBody);

#if DOTNETFW
            request.Timeout = Settings.Timeout;
#endif

            if (Settings.PreferCompressedResponses)
            {
                request.Headers["Accept-Encoding"] = "gzip, deflate";
            }

            LastRequest = request;
            BeforeRequest?.Invoke(request, outBody);

            // Write the body to the output
            if (outBody != null)
                request.WriteBody(compressRequestBody, outBody);

            // Make sure the HttpResponse gets disposed!
            using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Settings.Timeout)).ConfigureAwait(false))
            //using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    //Read body before we call the hook, so the hook cannot read the body before we do
                    var inBody = readBody(webResponse);

                    LastResponse = webResponse;
                    AfterResponse?.Invoke(webResponse, inBody);

                    // Do this call after AfterResponse, so AfterResponse will be called, even if exceptions are thrown by ToBundleEntry()
                    try
                    {
                        LastResult = null;

                        if (webResponse.StatusCode.IsSuccessful())
                        {
                            LastResult = webResponse.ToBundleEntry(inBody, Settings.ParserSettings, throwOnFormatException: true);
                            return LastResult;
                        }
                        else
                        {
                            LastResult = webResponse.ToBundleEntry(inBody, Settings.ParserSettings, throwOnFormatException: false);
                            throw HttpUtil.BuildFhirOperationException(webResponse.StatusCode, LastResult.Resource);
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

                        throw HttpUtil.BuildFhirOperationException(webResponse.StatusCode, operationOutcome);
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


    }
}
