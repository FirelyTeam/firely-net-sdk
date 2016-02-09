/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Net;

namespace Hl7.Fhir.Rest
{
    internal class Requester
    {
        public Uri BaseUrl { get; private set; }

        public bool UseFormatParameter { get; set; }
        public ResourceFormat PreferredFormat { get; set; }
        public int Timeout { get; set; }           // In miliseconds
        public Prefer Prefer { get; set; }

        public Requester(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Timeout = 100 * 1000;       // Default timeout is 100 seconds            
            Prefer = Rest.Prefer.ReturnRepresentation;
        }


        public Bundle.EntryComponent LastResult { get; private set; }
        public HttpWebResponse LastResponse { get; private set; }
        public HttpWebRequest LastRequest { get; private set; }
        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }



        public Bundle.EntryComponent Execute(Bundle.EntryComponent interaction)
        {
            if (interaction == null) throw Error.ArgumentNull("interaction");

            byte[] outBody;
            var request = interaction.ToHttpRequest(Prefer, PreferredFormat, UseFormatParameter, out outBody);

#if !PORTABLE45
            request.Timeout = Timeout;
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

                    // If response has an error code which will make it impossible to turn it into bundle entries: convert it into FhirOperationException and bail out...
                    var statusString = ((int)webResponse.StatusCode).ToString();

                    // Do this call after AfterResponse, so AfterResponse will be called, even if exceptions are thrown by ToBundleEntry()
                    try
                    {
                        LastResult = webResponse.ToBundleEntry(inBody);
                        Exception httpException = HttpStatusToException(statusString, LastResult.Resource as OperationOutcome);

                        if (httpException != null)
                        {
                            throw httpException;
                        }
                        else
                        {
                            return LastResult;
                        }
                    }
                    catch(FormatException fe)
                    {
                        // The server responded with HTML code. Still build a FhirOperationException and set a LastResult.
                        // Build a very minimal LastResult
                        var errorResult = new Bundle.EntryComponent();
                        errorResult.Response = new Bundle.ResponseComponent();
                        errorResult.Response.Status = statusString;

                        OperationOutcome operationOutcome = OperationOutcome.ForException(fe, OperationOutcome.IssueType.Invalid);

                        errorResult.Resource = operationOutcome;
                        LastResult = errorResult;

                        Exception httpException = HttpStatusToException(statusString, operationOutcome, true);
                        throw httpException;
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
                var body = HttpUtil.ReadAllFromStream(response.GetResponseStream());

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
        /// <param name="outcome">OperationOutcome to be used informationally in exception message</param>
        /// <param name="forceException">Force the creation of an exception with the information from status and outcome</param>
        /// <returns></returns>
        private static Exception HttpStatusToException(string status, OperationOutcome outcome, bool forceException = false)
        {
            HttpStatusCode statusCode;

            if (!Enum.TryParse<HttpStatusCode>(status, out statusCode))
            {
                // If the status code is unable to be parsed, then report
                // in internal server error
                statusCode = HttpStatusCode.InternalServerError;
            }

            if (status.StartsWith("2") && !forceException)      // 2xx codes - success
            {
                return null;   // success
            }
            else if (status.StartsWith("3") || status.StartsWith("1"))      // 3xx codes - we don't handle them, unless the .NET API did it for us
            {
                return Error.NotSupported("Server returned a status code '{0}', which is not supported by the FhirClient".FormatWith(status));
            }
            else if (status.StartsWith("4") || status.StartsWith("5") || forceException)      // 4xx/5xx codes - client or server error.
            {
                var message = string.Format("Operation was unsuccessful, and returned status {0}.", status);
                if (outcome != null)
                {
                    return new FhirOperationException(message + " OperationOutcome: " + outcome.ToString(), statusCode, outcome);
                }
                else
                {
                    return new FhirOperationException(message, statusCode);
                }
            }
            else
            {
                return Error.NotSupported("Server returned an illegal http status code '{0}', which is not defined by the Http standard".FormatWith(status));
            }
        }
    }
}
