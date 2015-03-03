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

        public Action<HttpWebRequest> BeforeRequest { get; set; }
        public Action<WebResponse> AfterRequest { get; set; }

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
            
            if (expect.Select(sc => sc.ToString()).Contains(response.TransactionResponse.Status))
            {
                LastResult = response.TransactionResponse;
                
                bool noBody = response.TransactionResponse.Status == HttpStatusCode.NoContent.ToString();
                if (!noBody && Prefer == Rest.Prefer.ReturnRepresentation && response.Resource == null)
                {
                    if (response.Resource == null) throw Error.InvalidOperation("Operation {0} on {1} expected a body but none was returned", interaction.Transaction.Method,
                                        interaction.Transaction.Url);
                }
                
                if (response.Resource != null && !response.Resource.GetType().CanBeTreatedAsType(typeof(TResource)))
                        throw Error.InvalidOperation("Operation {0} on {1} expected a body of type {2} but a {3} was returned", interaction.Transaction.Method,
                            interaction.Transaction.Url, typeof(TResource).Name, response.Resource.GetType().Name);

                return (TResource)response.Resource;
            }
            else
            {
                reportFailure(response);        // will always throw()
                return null;    // unreachable code
            }
        }


        public TResource Execute<TResource>(Bundle transaction, HttpStatusCode expect) where TResource: Resource
        {
            return Execute<TResource>(transaction, new[] { expect });
        }


        private Bundle.BundleEntryComponent doRequest(Bundle.BundleEntryComponent interaction)
        {
            var request = interaction.ToHttpRequest(Prefer, PreferredFormat, UseFormatParameter);

#if !PORTABLE45
            request.Timeout = Timeout;
#endif

            if (BeforeRequest != null) BeforeRequest(request);

            // Make sure the HttpResponse gets disposed!
            // using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)))
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    var response = webResponse.ToBundleEntry();
                    if (AfterRequest != null) AfterRequest(webResponse);

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


        private static void reportFailure(Bundle.BundleEntryComponent response)
        {
            if (response.Resource != null)
            {
                if (response.Resource is OperationOutcome)
                {
                    var outcome = (OperationOutcome)response.Resource;

                    System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

                    if (outcome.Text != null && !string.IsNullOrEmpty(outcome.Text.Div))
                    {
                        System.Diagnostics.Debug.WriteLine(outcome.Text.Div);
                        System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
                    }

                    if (outcome.Issue != null)
                    {
                        foreach (var issue in outcome.Issue)
                        {
                            System.Diagnostics.Debug.WriteLine("	" + issue.Details);
                        }

                    }

                    System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

                    throw new FhirOperationException("Operation failed with status code " + response.TransactionResponse.Status, outcome);
                }
                else
                    throw new FhirOperationException(String.Format("Operation failed with status code {0}, but returned a {1} resource", response.TransactionResponse.Status, response.Resource.GetType().Name));
            }
            else
                throw new FhirOperationException("Operation failed with status code " + response.TransactionResponse.Status);
        }
    }
}
