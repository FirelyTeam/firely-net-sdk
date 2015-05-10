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

        public Action<HttpWebRequest, byte[]> BeforeRequest { get; set; }
        public Action<HttpWebResponse, byte[]> AfterResponse { get; set; }

        public Bundle.BundleEntryTransactionResponseComponent LastResult { get; private set; }

        public Requester(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Timeout = 100 * 1000;       // Default timeout is 100 seconds            
            Prefer = Rest.Prefer.ReturnRepresentation;
        }


        public TResource Execute<TResource>(Bundle transaction, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            //TODO: Handle 304 Not Modified
            var interaction = transaction.Entry.First();

            var response = doRequest(interaction);
            LastResult = response.TransactionResponse;
            
            if (expect.Select(sc => sc.ToString()).Contains(response.TransactionResponse.Status))
            {                
                bool noBody = response.TransactionResponse.Status == HttpStatusCode.NoContent.ToString();
                if (!noBody && Prefer == Rest.Prefer.ReturnRepresentation && response.Resource == null)
                {
                    var message = String.Format("Operation {0} on {1} expected a body but none was returned", interaction.Transaction.Method,
                                interaction.Transaction.Url);
                    throw new FhirOperationException(message);
                }

                if (response.Resource != null && !response.Resource.GetType().CanBeTreatedAsType(typeof(TResource)))
                {
                    if (response.Resource is OperationOutcome)
                    {
                        var outcome = response.Resource as OperationOutcome;
                        reportOutcome(outcome);
                        throw new FhirOperationException("Operation succeeded, but returned an OperationOutcome", outcome); 
                    }
                    else
                    {
                        var message = String.Format("Operation {0} on {1} expected a body of type {2} but a {3} was returned", interaction.Transaction.Method,
                            interaction.Transaction.Url, typeof(TResource).Name, response.Resource.GetType().Name);
                        throw new FhirOperationException(message);
                    }
                }

                return (TResource)response.Resource;
            }
            else
            {
                var message = String.Format("Operation returned unexpected status {0}.", response.TransactionResponse.Status);

                if (response.Resource is OperationOutcome)
                {
                    var outcome = response.Resource as OperationOutcome;
                    var text = reportOutcome(outcome);
                    throw new FhirOperationException(message + " OperationOutcome: " +  text);
                }

                throw new FhirOperationException(message);
            }
        }


        public TResource Execute<TResource>(Bundle transaction, HttpStatusCode expect) where TResource: Resource
        {
            return Execute<TResource>(transaction, new[] { expect });
        }


        private Bundle.BundleEntryComponent doRequest(Bundle.BundleEntryComponent interaction)
        {
            byte[] outBody;
            var request = interaction.ToHttpRequest(Prefer, PreferredFormat, UseFormatParameter, out outBody);

#if !PORTABLE45
            request.Timeout = Timeout;
#endif

            if (BeforeRequest != null) BeforeRequest(request, outBody);

            // Make sure the HttpResponse gets disposed!
            // using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)))
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    //Read body before we call the hook, so the hook cannot read the body before we do
                    var inBody = readBody(webResponse);

                    if (AfterResponse != null) AfterResponse(webResponse,inBody);
                    var response = webResponse.ToBundleEntry(inBody);

                    return response;
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

        private static string reportOutcome(OperationOutcome outcome)
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
