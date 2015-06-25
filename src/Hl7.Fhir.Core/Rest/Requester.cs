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


        public Bundle.BundleEntryComponent LastResult { get; private set; }
        public HttpWebResponse LastResponse { get; private set; }
        public HttpWebRequest LastRequest { get; private set; }
        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }


        public Bundle.BundleEntryComponent Execute(Bundle transaction, Type expected)
        {
            if (transaction == null) throw Error.ArgumentNull("transaction");
            if (expected == null) throw Error.ArgumentNull("expected");

            var interaction = transaction.Entry.First();

            LastResult = doRequest(interaction);
            var status = LastResult.TransactionResponse.Status;

            if (status.StartsWith("2"))      // 2xx codes - success
            {
                if (LastResult.Resource != null && !LastResult.Resource.GetType().CanBeTreatedAsType(expected))
                {
                    // We have a successful call, but the body is not of the type we expect. As a courtesy, log an OperationOutcome if that has been received
                    var outcome = LastResult.Resource as OperationOutcome;

                    if (outcome != null)
                        throw new FhirOperationException("Operation succeeded, but returned an OperationOutcome: " + outcomeToString(outcome));

                    var message = String.Format("Operation {0} on {1} expected a body of type {2} but a {3} was returned", interaction.Transaction.Method,
                        interaction.Transaction.Url, expected.Name, LastResult.Resource.GetType().Name);
                    throw new FhirOperationException(message);
                }

                // The correct resource type was found (or no body at all)
                return LastResult;
            }
            else if (status.StartsWith("3") || status.StartsWith("1"))
            {
                throw Error.NotSupported("Server returned a status code '{0}', which is not supported by the FhirClient".FormatWith(status));
            }
            else if (status.StartsWith("4") || status.StartsWith("5"))
            {
                var message = String.Format("Operation was unsuccessful, and returned status {0}.", status);

                var outcome = LastResult.Resource as OperationOutcome;
                if (outcome != null)
                   message += " OperationOutcome: " + outcomeToString(outcome);

                throw new FhirOperationException(message);
            }
            else
            {
                throw Error.NotSupported("Server returned an illegal http status code '{0}', which is not defined by the Http standard".FormatWith(status));
            }
        }

        private Bundle.BundleEntryComponent doRequest(Bundle.BundleEntryComponent interaction)
        {
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

                    // Do this call after AfterResponse, so this will be called, even if exceptions are thrown by ToBundleEntry()
                    return webResponse.ToBundleEntry(inBody);
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

        private static string outcomeToString(OperationOutcome outcome)
        {
            if (outcome.Text != null && !string.IsNullOrEmpty(outcome.Text.Div))
            {
                return outcome.Text.Div;
            }

            var text = String.Empty;
            if (outcome.Issue != null)
            {
                foreach (var issue in outcome.Issue)
                {
                    if(!String.IsNullOrEmpty(text))
                        text += " ------------- ";
                }
            }

            return text;
        }
    }
}
