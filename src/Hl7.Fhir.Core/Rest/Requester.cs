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
        public Uri BaseUrl { get; internal set; }

        public bool UseFormatParameter { get; internal set; }
        public ResourceFormat PreferredFormat { get; internal set; }

        public int Timeout { get; internal set; }           // In miliseconds

        public Action<HttpWebRequest> BeforeRequest { get; internal set; }
        public Action<WebResponse> AfterRequest { get; internal set; }


        public Requester(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            PreferredFormat = ResourceFormat.Xml;
            Timeout = 100 * 1000;       // Default timeout is 100 seconds            
        }


        public Bundle.BundleEntryComponent Execute(Bundle.BundleEntryComponent interaction, IEnumerable<HttpStatusCode> expect, Prefer bodyPreference)
        {
            //TODO: Handle 304 Not Modified

            var response = doRequest(interaction, bodyPreference);

            if (expect.Select(sc => sc.ToString()).Contains(response.TransactionResponse.Status))
                return response;
            else
            {
                reportFailure(response);        // will always throw()
                return null;    // unreachable code
            }
        }



        private Bundle.BundleEntryComponent doRequest(Bundle.BundleEntryComponent interaction, Prefer bodyPreference)
        {
            var request = interaction.ToHttpRequest(bodyPreference, PreferredFormat, UseFormatParameter);

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
