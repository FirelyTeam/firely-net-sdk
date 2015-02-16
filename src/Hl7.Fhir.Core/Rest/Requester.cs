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
        private FhirRequest _request;

        public Requester(Uri baseUri)
        {
            _request = new FhirRequest();
            _request.BaseUrl = baseUri;
        }

        public Requester Post()
        {
            _request.Method = "POST";
            return this;
        }

        public Requester Put()
        {
            _request.Method = "PUT";
            return this;
        }

        public Requester Delete()
        {
            _request.Method = "DELETE";
            return this;
        }

        public Requester Options()
        {
            _request.Method = "OPTIONS";
            return this;
        }

        public Requester At(Uri location)
        {
            _request.Path = location;
            return this;
        }

        public Requester Using(ResourceFormat format)
        {
            _request.Format = format;
            return this;
        }

        public Requester WithBody(Resource resource)
        {
            _request.SetBody(resource);
            return this;
        }

        public Requester WithMeta(Meta meta, ResourceFormat format)
        {
            _request.SetMeta(meta);
            return this;
        }

        private List<HttpStatusCode> _expected = new List<HttpStatusCode>();

        public Requester Expect(HttpStatusCode code)
        {
            _expected.Add(code);
            return this;
        }

        public Resource FetchResource<T>(ResourceFormat format) where T:Resource
        {
            return doRequest<T>(_request, _expected, resp => resp.BodyAsResource<T>());
        }

        public ResourceIdentity FetchIdentity(ResourceFormat format)
        {
        }

        public Meta FetchMeta(ResourceFormat format)
        {
        }

        private T doRequest<T>(Func<FhirResponse, T> onSuccess)
        {
            _request.UseFormatParameter = this.UseFormatParam;
            var response = _request.GetResponse(PreferredFormat);

            LastResponseDetails = response;

            if (success.Contains(response.Result))
                return onSuccess(response);
            else
            {
                // Try to parse the body as an OperationOutcome resource, but it is no
                // problem if it's something else, or there is no parseable body at all

                OperationOutcome outcome = null;

                try
                {
                    outcome = response.BodyAsResource<OperationOutcome>();
                }
                catch
                {
                    // failed, so the body does not contain an OperationOutcome.
                    // Put the raw body as a message in the OperationOutcome as a fallback scenario
                    var body = response.BodyAsString();
                    if (!String.IsNullOrEmpty(body))
                        outcome = OperationOutcome.ForMessage(body);
                }

                if (outcome != null)
                {
                    System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

                    if (outcome.Text != null && !string.IsNullOrEmpty(outcome.Text.Div))
                    {
                        System.Diagnostics.Debug.WriteLine(outcome.Text.Div);
                        System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
                    }

                    foreach (var issue in outcome.Issue)
                    {
                        System.Diagnostics.Debug.WriteLine("	" + issue.Details);
                    }
                    System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome);
                }
                else
                {
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result);
                }
            }
        }
    }
}
