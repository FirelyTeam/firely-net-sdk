/* 
 * Copyright (c) 2017+ brianpos, Furore and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.WebApi
{
    /// <summary>
    /// This class basically implements
    /// http://hl7.org/fhir/stu3/http.html
    /// </summary>
    [RoutePrefix("")]
    public partial class FhirSTU3Controller : ApiController
    {
        internal ModelBaseInputs GetInputs(string baseUrl)
        {
            return GetInputs(Request, User, new Uri(baseUrl));
        }

        internal static ModelBaseInputs GetInputs(HttpRequestMessage Request, System.Security.Principal.IPrincipal User, Uri baseUrl)
        {
            var cert = Request.GetClientCertificate();
            var inputs = new ModelBaseInputs(
                User,
                cert,
                Request.Method.Method,
                Request.RequestUri,
                baseUrl,
                Request.GetDependencyScope());

            return inputs;
        }

        internal static IFhirSystemServiceSTU3 GetSystemModel(ModelBaseInputs inputs)
        {
            return WebApiConfig._systemService;
        }

        internal static IFhirResourceServiceSTU3 GetResourceModel(string ResourceName, ModelBaseInputs inputs)
        {
            var model = WebApiConfig._systemService.GetResourceService(inputs, ResourceName);

            if (model != null)
                return model;

            throw new FhirServerException(HttpStatusCode.NotFound, "Resource [" + ResourceName + "] is not supported on this server");
        }

        public static Hl7.Fhir.Rest.SummaryType GetSummaryParameter(HttpRequestMessage request)
        {
            string s = request.GetParameter(FhirParameter.SUMMARY);
            if (s == null)
                return Hl7.Fhir.Rest.SummaryType.False;

            switch (s.ToLower())
            {
                case "true": return Hl7.Fhir.Rest.SummaryType.True;
                case "false": return Hl7.Fhir.Rest.SummaryType.False;
                case "text": return Hl7.Fhir.Rest.SummaryType.Text;
                case "data": return Hl7.Fhir.Rest.SummaryType.Data;
                case "count": return Hl7.Fhir.Rest.SummaryType.Count;
                default: return Hl7.Fhir.Rest.SummaryType.False;
            }
        }

        /// <summary>
        /// <http://hl7-fhir.github.io/http.html#transaction>
        /// </summary>
        [HttpPost, Route("")]
        public HttpResponseMessage ProcessBatch(Bundle batch)
        {
            var buri = this.CalculateBaseURI("metadata");
            var Inputs = GetInputs(buri);

            Bundle outcome = GetSystemModel(Inputs).ProcessBatch(Inputs, batch);
            outcome.ResourceBase = new Uri(buri);
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);
            outcome.SetAnnotation<SummaryType>(summary);
            return Request.ResourceResponse(outcome, HttpStatusCode.OK);
        }

        // GET fhir/values
        [HttpOptions, Route("")]
        [HttpGet, Route("metadata")]
        [AllowAnonymous]
        public Hl7.Fhir.Model.CapabilityStatement GetConformance()
        {
            var buri = this.CalculateBaseURI("metadata");
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);
            var Inputs = GetInputs(buri);
            var con = GetSystemModel(Inputs).GetConformance(Inputs, summary);
            con.ResourceBase = new Uri(buri, UriKind.RelativeOrAbsolute);
            con.SetAnnotation<SummaryType>(summary);
            return con;
        }

        [HttpGet, Route("{ResourceName}/{id}")]
        public HttpResponseMessage Get(string ResourceName, string id)
        {
            return Get(ResourceName, id, null);
        }

        // GET fhir/patient/5/_history/4
        //[HttpGet, Route("{type}/{id}/_history/{vid}")]
        [HttpGet, Route("{ResourceName}/{id}/_history/{vid}")]
        public HttpResponseMessage Get(string ResourceName, string id, string vid)
        {
            var buri = this.CalculateBaseURI("{ResourceName}");

            if (!Id.IsValidValue(id))
            {
                throw new FhirServerException(HttpStatusCode.BadRequest, "ID [" + id + "] is not a valid FHIR Resource ID");
            }

            var Inputs = GetInputs(buri);
            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, Inputs);
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            Resource resource = model.Get(id, vid, summary);
            if (resource != null)
            {
                resource.ResourceBase = new Hl7.Fhir.Rest.ResourceIdentity(Request.RequestUri).BaseUri;

                if (resource is DomainResource)
                {
                    DomainResource dr = resource as DomainResource;
                    switch (summary)
                    {
                        case Hl7.Fhir.Rest.SummaryType.False:
                            break;
                        case Hl7.Fhir.Rest.SummaryType.True:
                            // summary doesn't have the text in it.
                            dr.Text = null;
                            // there are no contained references in the summary form
                            dr.Contained = null;

                            // Add in the Meta Tag that indicates that this resource is only a partial
                            resource.Meta.Tag = new List<Coding>
                            {
                                new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED")
                            };
                            break;
                        case Hl7.Fhir.Rest.SummaryType.Text:
                            // what do we need to filter here
                            break;
                        case Hl7.Fhir.Rest.SummaryType.Data:
                            // summary doesn't have the text in it.
                            dr.Text = null;
                            // Add in the Meta Tag that indicates that this resource is only a partial
                            resource.Meta.Tag = new List<Coding>
                            {
                                new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED")
                            };
                            break;
                    }
                }

                if (ResourceName == "Binary")
                {
                    // We need to reset the accepts type so that the correct formatter is used on the way out.
                    string formatParam = this.ControllerContext.Request.GetParameter("_format");
                    if (string.IsNullOrEmpty(formatParam))
                    {
                        this.ControllerContext.Request.Headers.Accept.Clear();
                        this.ControllerContext.Request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue((resource as Binary).ContentType));
                    }
                }

                var msg = Request.ResourceResponse(resource, HttpStatusCode.OK);
                msg.Headers.Location = resource.ResourceIdentity().WithBase(resource.ResourceBase);
                msg.Headers.Add("ETag", String.Format("\"{0}\"", resource.Meta.VersionId));

                if (ResourceName == "Binary")
                {
                    // We need to reset the accepts type so that the correct formatter is used on the way out.
                    string formatParam = this.ControllerContext.Request.GetParameter("_format");
                    if (string.IsNullOrEmpty(formatParam))
                    {
                        msg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = String.Format("fhir_binary_{0}_{1}.{2}",
                              resource.Id,
                              resource.Meta != null ? resource.Meta.VersionId : "0",
                              GetFileExtensionForMimeType((resource as Binary).ContentType))
                        };
                    }
                }

                return msg;
            }

            // this request is a "you wanted what?"
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet, Route("{ResourceName}/{id}/${operation}")]
        public HttpResponseMessage PerformOperation(string ResourceName, string id, string operation)
        {
            var buri = this.CalculateBaseURI("{ResourceName}");
            
            Parameters operationParameters = new Parameters();
            ExtractParametersFromUrl(ref operationParameters, Request.TupledParameters(false));
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));
            var resource = model.PerformOperation(id, operation, operationParameters, summary);
            return PrepareOperationOutputMessage(buri, resource);
        }

        [HttpPost, Route("{ResourceName}/{id}/${operation}")]
        public HttpResponseMessage PerformOperation(string ResourceName, string id, string operation, [FromBody] Parameters operationParameters)
        {
            var buri = this.CalculateBaseURI("{ResourceName}");
            
            ExtractParametersFromUrl(ref operationParameters, Request.TupledParameters(false));
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));
            var resource = model.PerformOperation(id, operation, operationParameters, summary);
            return PrepareOperationOutputMessage(buri, resource);
        }

        [HttpGet, Route("{ResourceName}/${operation}")]
        public HttpResponseMessage PerformOperation(string ResourceName, string operation)
        {
            var buri = this.CalculateBaseURI("{ResourceName}");
            
            Parameters operationParameters = new Parameters();
            ExtractParametersFromUrl(ref operationParameters, Request.TupledParameters(false));
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));
            var resource = model.PerformOperation(operation, operationParameters, summary);
            return PrepareOperationOutputMessage(buri, resource);
        }

        [HttpPost, Route("{ResourceName}/${operation}")]
        public HttpResponseMessage PerformOperation(string ResourceName, string operation, [FromBody] Parameters operationParameters)
        {
            var buri = this.CalculateBaseURI("{ResourceName}");
            
            ExtractParametersFromUrl(ref operationParameters, Request.TupledParameters(false));
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));
            var resource = model.PerformOperation(operation, operationParameters, summary);
            return PrepareOperationOutputMessage(buri, resource);
        }

        private HttpResponseMessage PrepareOperationOutputMessage(string buri, Resource resource)
        {
            if (resource != null)
            {
                resource.ResourceBase = new Uri(buri);

                if (resource is Binary)
                {
                    // We need to reset the accepts type so that the correct formatter is used on the way out.
                    string formatParam = this.ControllerContext.Request.GetParameter("_format");
                    if (string.IsNullOrEmpty(formatParam))
                    {
                        this.ControllerContext.Request.Headers.Accept.Clear();
                        this.ControllerContext.Request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue((resource as Binary).ContentType));
                    }
                }

                var msg = Request.ResourceResponse(resource, HttpStatusCode.OK);
                if (!string.IsNullOrEmpty(resource.Id))
                    msg.Headers.Location = resource.ResourceIdentity().WithBase(resource.ResourceBase);
                else
                    msg.Headers.Location = Request.RequestUri;
                if (resource.Meta != null && !string.IsNullOrEmpty(resource.Meta.VersionId))
                    msg.Headers.Add("ETag", String.Format("\"{0}\"", resource.Meta.VersionId));

                if (resource is Binary)
                {
                    // We need to reset the accepts type so that the correct formatter is used on the way out.
                    string formatParam = this.ControllerContext.Request.GetParameter("_format");
                    if (string.IsNullOrEmpty(formatParam))
                    {
                        msg.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = String.Format("fhir_binary_{0}_{1}.{2}",
                              resource.Id,
                              resource.Meta != null ? resource.Meta.VersionId : "0",
                              GetFileExtensionForMimeType((resource as Binary).ContentType))
                        };
                    }
                }

                return msg;
            }

            // this request is a "you wanted what?"
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost, HttpGet, Route("${operation}")]
        public HttpResponseMessage PerformOperation(string operation, [FromBody] Parameters operationParameters)
        {
            var buri = this.CalculateBaseURI("${operation}");
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            ExtractParametersFromUrl(ref operationParameters, Request.TupledParameters(false));
            var inputs = GetInputs(buri);

            Resource resource = GetSystemModel(inputs).PerformOperation(inputs, operation, operationParameters, summary);
            return PrepareOperationOutputMessage(buri, resource);
        }

        private void ExtractParametersFromUrl(ref Parameters operationParameters, IEnumerable<KeyValuePair<string, string>> enumerable)
        {
            if (operationParameters == null)
                operationParameters = new Parameters();
            foreach (var item in Request.TupledParameters(false))
            {
                operationParameters.Add(item.Key, new FhirString(item.Value));
            }
        }

        private string GetFileExtensionForMimeType(string mimetype)
        {
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimetype, false);
            if (regKey != null && regKey.GetValue("Extension") != null)
                return regKey.GetValue("Extension").ToString();
            return null;
        }

        [HttpGet, Route("{ResourceName}/_search")]
        public Bundle SearchWithOperator(string ResourceName)
        {
            return Search(ResourceName);
        }

        // GET fhir/patient/
        // GET fhir/patient/search?
        //[HttpGet, Route("{type}")]
        //[HttpGet, Route("{type}/_search")]
        [HttpGet, Route("{ResourceName}")]
        public Bundle Search(string ResourceName)
        {
            System.Diagnostics.Trace.WriteLine("GET: " + this.ControllerContext.Request.RequestUri.OriginalString);

            var parameters = Request.TupledParameters(true);
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);
            string sortby = Request.GetParameter(FhirParameter.SORT);
            int pagesize = Request.GetIntParameter(FhirParameter.COUNT) ?? Const.DEFAULT_PAGE_SIZE;
            var includeParams = Request.TupledParameters(false).Where(i => i.Key == "_include");

            var buri = this.CalculateBaseURI("{ResourceName}");
            parameters = parameters.Union(includeParams);

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            Bundle result = model.Search(parameters, pagesize, summary);
            result.ResourceBase = new Uri(buri);

            this.ControllerContext.Request.SaveEntry(result);
            return result;
        }

        // Need the POST form of search going to "{ResourceName}/_search"
        [HttpPost, Route("{ResourceName}/_search")]
        public Bundle SearchByPost(string ResourceName)
        {
            System.Diagnostics.Trace.WriteLine("GET: " + this.ControllerContext.Request.RequestUri.OriginalString);

            var parameters = Request.TupledParameters(true).ToList();
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);
            string sortby = Request.GetParameter(FhirParameter.SORT);
            int pagesize = Request.GetIntParameter(FhirParameter.COUNT) ?? Const.DEFAULT_PAGE_SIZE;

            var buri = this.CalculateBaseURI("{ResourceName}");

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            // Also grab the application/x-www-form-urlencoded content body
            if (Request.Content.IsFormData())
            {
                // This is Bad, talk to Andrew again to find out the library to assist with this.
                var requestData = Request.Content.ReadAsFormDataAsync().Result;
                foreach (var item in requestData.AllKeys)
                {
                    parameters.Add(new KeyValuePair<string, string>(item, requestData[item]));
                }
            }

            Bundle result = model.Search(parameters, pagesize, summary);
            result.ResourceBase = new Uri(buri);

            this.ControllerContext.Request.SaveEntry(result);
            return result;
        }

        // GET fhir/patient/_history
        [HttpGet, Route("{ResourceName}/_history")]
        public Bundle ResourceHistory(string ResourceName)
        {
            System.Diagnostics.Trace.WriteLine("GET: " + this.ControllerContext.Request.RequestUri.OriginalString);

            DateTimeOffset? since = Request.GetDateParameter("_since");
            int pagesize = Request.GetIntParameter(FhirParameter.COUNT) ?? Const.DEFAULT_PAGE_SIZE;
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            var buri = this.CalculateBaseURI("{ResourceName}");

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            // TODO: Locate the till parameter in the history
            Bundle result = model.TypeHistory(since, null, pagesize,  summary);
            result.ResourceBase = new Uri(buri);

            this.ControllerContext.Request.SaveEntry(result);
            return result;
        }

        // GET fhir/patient/5/_history
        //[HttpGet, Route("{type}/{id}/_history/{vid}")]
        [HttpGet, Route("{ResourceName}/{id}/_history")]
        public Bundle InstanceHistory(string ResourceName, string id)
        {
            System.Diagnostics.Trace.WriteLine("GET: " + this.ControllerContext.Request.RequestUri.OriginalString);

            DateTimeOffset? since = Request.GetDateParameter("_since");
            int pagesize = Request.GetIntParameter(FhirParameter.COUNT) ?? Const.DEFAULT_PAGE_SIZE;
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);
            string sortby = Request.GetParameter(FhirParameter.SORT);

            var buri = this.CalculateBaseURI("{ResourceName}");

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            Bundle result = model.InstanceHistory(id, since, null, pagesize, summary);
            result.ResourceBase = new Uri(buri);
            if (result.Total == 0)
            {
                try
                {
                    // Check to see if the item itself exists
                    if (model.Get(id, null, Hl7.Fhir.Rest.SummaryType.True) == null)
                    {
                        // this resource does not exist to have a history
                        throw new FhirServerException(HttpStatusCode.NotFound, "The resource was not found");
                    }
                }
                catch (FhirServerException ex)
                {
                    if (ex.StatusCode != HttpStatusCode.Gone)
                        throw ex;
                }
            }

            this.ControllerContext.Request.SaveEntry(result);
            return result;
        }

        [HttpGet, Route("_history")]
        public Bundle WholeSystemHistory()
        {
            System.Diagnostics.Trace.WriteLine("GET: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("_history");

            DateTimeOffset? since = Request.GetDateParameter("_since");
            int pagesize = Request.GetIntParameter(FhirParameter.COUNT) ?? Const.DEFAULT_PAGE_SIZE;
            Hl7.Fhir.Rest.SummaryType summary = GetSummaryParameter(Request);

            var Inputs = GetInputs(buri);
            var model = GetSystemModel(Inputs);

            Bundle result = model.SystemHistory(Inputs, since, null, pagesize, summary);
            result.ResourceBase = new Uri(buri);

            this.ControllerContext.Request.SaveEntry(result);
            return result;
        }


        // POST fhir/values
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyResource"></param>
        /// <returns></returns>
        /// <remarks>
        /// No need to test for null body, as the filters will throw the issue before it gets here
        /// Unit tests ensure this is the case
        /// </remarks>
        [HttpPost, Route("{ResourceName}", Order = 0)]
        public HttpResponseMessage Post(string ResourceName, [FromBody] Resource bodyResource)
        {
            System.Diagnostics.Trace.WriteLine("POST: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("{ResourceName}");
            

            if (bodyResource == null)
            {
                var oo = new OperationOutcome()
                {
                    Text = Utility.CreateNarative("Validation Error"),
                    //    Contained = new List<Resource> { bodyResource } // Can't attach a null resource!
                };
                oo.Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent()
                    {
                        Details = new CodeableConcept(null, null, "Missing " + ResourceName + " resource POST"),
                        Severity = OperationOutcome.IssueSeverity.Fatal
                    }
                };
                // return oo;
                return Request.CreateResponse(HttpStatusCode.BadRequest, oo);
            }

            if (!String.IsNullOrEmpty(bodyResource.Id))
            {
                var oo = new OperationOutcome()
                {
                    Text = Utility.CreateNarative("Validation Error"),
                    Contained = new List<Resource> { bodyResource }
                };
                oo.Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent()
                    {
                        Details = new CodeableConcept(null, null, "ID must be empty for POST"),
                        Severity = OperationOutcome.IssueSeverity.Fatal
                    }
                };
                // return oo;
                return Request.CreateResponse(HttpStatusCode.BadRequest, oo);
            }

            var Inputs = GetInputs(buri);
            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            var result = model.Create(bodyResource, null, null, null);
            this.ControllerContext.Request.SaveEntry(bodyResource);
            result.ResourceBase = new Uri(buri);
            var actualResource = result;

            ResourceIdentity ri = null;
            if (result is Bundle)
            {
                ri = result.ResourceIdentity(result.ResourceBase);
            }
            else if (!(result is OperationOutcome) && !string.IsNullOrEmpty(result.Id))
            {
                ri = result.ResourceIdentity(result.ResourceBase);
            }

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=operationoutcome")
                {
                    if (!(result is OperationOutcome))
                    {
                        OperationOutcome so = new OperationOutcome()
                        {
                            Text = Utility.CreateNarative("Resource update")
                        };
                        so.Text.Status = Narrative.NarrativeStatus.Generated;
                        so.Issue = new List<OperationOutcome.IssueComponent>
                        {
                            new OperationOutcome.IssueComponent()
                            {
                                Severity = OperationOutcome.IssueSeverity.Information,
                                Code = OperationOutcome.IssueType.Informational,
                                Details = new CodeableConcept(null, null, "Update was completed")
                            }
                        };
                        result = so;
                    }
                }
            }

            HttpResponseMessage returnMessage;
            if (bodyResource.Annotation<CreateOrUpate>() == CreateOrUpate.Create)
                returnMessage = Request.CreateResponse(HttpStatusCode.Created, result);
            else
                returnMessage = Request.CreateResponse(HttpStatusCode.OK, result);

            // Put in the "Location" header
            if (ri != null)
            {
                returnMessage.Headers.Add("Location", ri.OriginalString);
                Request.Properties.Add(Const.ResourceIdentityKey, ri.MakeRelative().OriginalString);
            }
            if (actualResource.Meta != null && !string.IsNullOrEmpty(actualResource.Meta.VersionId))
            {
                returnMessage.Headers.Add("ETag", String.Format("W/\"{0}\"", actualResource.Meta.VersionId));
            }
            if (actualResource.Meta != null && actualResource.Meta.LastUpdated.HasValue)
            {
                returnMessage.Content.Headers.LastModified = actualResource.Meta.LastUpdated.Value;
                // returnMessage.Headers.Add("Last-Modified", actualResource.Meta.LastUpdated.Value.ToString("r"));
            }

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=minimal")
                {
                    returnMessage.Content = null;
                }
            }

            return returnMessage;
        }

        /// <summary>
        /// Support for Conditional Updates
        /// eg. PUT fhir/Patient?identifier=http://temp|43&birthDate=1973-10
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="id"></param>
        /// <param name="bodyResource"></param>
        /// <returns></returns>
        [HttpPut, Route("{ResourceName}")]
        public HttpResponseMessage Put(string ResourceName, [FromBody]Resource bodyResource)
        {
            System.Diagnostics.Trace.WriteLine("PUT: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("{ResourceName}");
            

            if (String.IsNullOrEmpty(this.ControllerContext.Request.RequestUri.Query))
            {
                throw new FhirServerException(HttpStatusCode.BadRequest, "Conditional updates not supported without query parameters");
            }

            if (!String.IsNullOrEmpty(bodyResource.Id))
            {
                // This is a conditional update, so clear the Id on the record
                // so that it doesn't accidentally get processed
                bodyResource.Id = null;
            }

            var Inputs = GetInputs(buri);
            OperationOutcome so = new OperationOutcome();
            if (ResourceName == "AuditEvent")
            {
                // depends on the AuditEvent, if was created by the server, then it will get an unauthorized exception report
                // otherwise externally reported events can be updated!
                //throw new FhirServerException(HttpStatusCode.MethodNotAllowed, "Cannot PUT a AuditEvent, you must POST them");
            }

            // so.Success();
            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            string ifMatch = null;
            var conditionalSearchParams = this.ControllerContext.Request.RequestUri.ParseQueryString().TupledParameters(false);
            if (conditionalSearchParams.Count() > 0)
            {
                ifMatch = this.ControllerContext.Request.RequestUri.Query;
            }

            var result = model.Create(bodyResource, ifMatch, null, null);
            // if (bodyResource is Binary)
            this.ControllerContext.Request.SaveEntry(bodyResource);
            result.ResourceBase = new Uri(buri);
            var actualResource = result;

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=operationoutcome")
                {
                    if (!(result is OperationOutcome))
                    {
                        so = new OperationOutcome()
                        {
                            Text = Utility.CreateNarative("Resource update")
                        };
                        so.Text.Status = Narrative.NarrativeStatus.Generated;
                        so.Issue = new List<OperationOutcome.IssueComponent>
                        {
                            new OperationOutcome.IssueComponent()
                            {
                                Severity = OperationOutcome.IssueSeverity.Information,
                                Code = OperationOutcome.IssueType.Informational,
                                Details = new CodeableConcept(null, null, "Update was completed")
                            }
                        };
                        result = so;
                    }
                }
            }

            HttpResponseMessage returnMessage;
            if (bodyResource.Annotation<CreateOrUpate>() == CreateOrUpate.Create)
                returnMessage = Request.CreateResponse(HttpStatusCode.Created, result);
            else
                returnMessage = Request.CreateResponse(HttpStatusCode.OK, result);

            // Put in the "Location" header
            if (actualResource is Bundle)
            {
                returnMessage.Headers.Add("Location", result.ResourceIdentity(actualResource.ResourceBase).OriginalString);
                Request.Properties.Add(Const.ResourceIdentityKey, actualResource.ResourceIdentity().MakeRelative().OriginalString);
            }
            else if (!(actualResource is OperationOutcome) && !string.IsNullOrEmpty(actualResource.Id))
            {
                returnMessage.Headers.Add("Location", actualResource.ResourceIdentity(actualResource.ResourceBase).OriginalString);
                Request.Properties.Add(Const.ResourceIdentityKey, actualResource.ResourceIdentity().MakeRelative().OriginalString);
            }
            if (actualResource.Meta != null && !string.IsNullOrEmpty(actualResource.Meta.VersionId))
            {
                returnMessage.Headers.Add("ETag", String.Format("W/\"{0}\"", actualResource.Meta.VersionId));
            }
            if (actualResource.Meta != null && actualResource.Meta.LastUpdated.HasValue)
            {
                returnMessage.Content.Headers.LastModified = actualResource.Meta.LastUpdated.Value;
                // returnMessage.Headers.Add("Last-Modified", actualResource.Meta.LastUpdated.Value.ToString("r"));
            }

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=minimal")
                {
                    returnMessage.Content = null;
                }
            }

            return returnMessage;
        }


        // PUT fhir/Patient/5
        [HttpPut, Route("{ResourceName}/{id}")]
        public HttpResponseMessage Put(string ResourceName, string id, [FromBody]Resource bodyResource)
        {
            System.Diagnostics.Trace.WriteLine("PUT: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("{ResourceName}");
            

            if (!Id.IsValidValue(id))
            {
                throw new FhirServerException(HttpStatusCode.BadRequest, "ID [" + id + "] is not a valid FHIR Resource ID");
            }

            var Inputs = GetInputs(buri);

            bodyResource.Id = id;
            if (ResourceName == "AuditEvent")
            {
                // depends on the AuditEvent, if was created by the server, then it will get an unauthorized exception report
                // otherwise externally reported events can be updated!
                //throw new FhirServerException(HttpStatusCode.MethodNotAllowed, "Cannot PUT a AuditEvent, you must POST them");
            }

            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            var result = model.Create(bodyResource, null, null, null);
            this.ControllerContext.Request.SaveEntry(bodyResource);
            result.ResourceBase = new Uri(buri);
            var actualResource = result;

            ResourceIdentity ri = null;
            if (result is Bundle)
            {
                ri = result.ResourceIdentity(result.ResourceBase);
            }
            else if (!(result is OperationOutcome) && !string.IsNullOrEmpty(result.Id))
            {
                ri = result.ResourceIdentity(result.ResourceBase);
            }

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=operationoutcome")
                {
                    if (!(result is OperationOutcome))
                    {
                        OperationOutcome so = new OperationOutcome()
                        {
                            Text = Utility.CreateNarative("Resource update")
                        };
                        so.Text.Status = Narrative.NarrativeStatus.Generated;
                        so.Issue = new List<OperationOutcome.IssueComponent>
                        {
                            new OperationOutcome.IssueComponent()
                            {
                                Severity = OperationOutcome.IssueSeverity.Information,
                                Code = OperationOutcome.IssueType.Informational,
                                Details = new CodeableConcept(null, null, "Update was completed")
                            }
                        };
                        result = so;
                    }
                }
            }

            HttpResponseMessage returnMessage;
            if (bodyResource.Annotation<CreateOrUpate>() == CreateOrUpate.Create)
                returnMessage = Request.CreateResponse(HttpStatusCode.Created, result);
            else
                returnMessage = Request.CreateResponse(HttpStatusCode.OK, result);

            // Put in the "Location" header
            if (ri != null)
            {
                returnMessage.Headers.Add("Location", ri.OriginalString);
                Request.Properties.Add(Const.ResourceIdentityKey, ri.MakeRelative().OriginalString);
            }
            if (actualResource.Meta != null && !string.IsNullOrEmpty(actualResource.Meta.VersionId))
            {
                returnMessage.Headers.Add("ETag", String.Format("W/\"{0}\"", actualResource.Meta.VersionId));
            }
            if (actualResource.Meta != null && actualResource.Meta.LastUpdated.HasValue)
            {
                // returnMessage.Headers.CacheControl.
                returnMessage.Content.Headers.LastModified = actualResource.Meta.LastUpdated.Value;
            }

            // Check the prefer header
            if (Request.Headers.Contains("Prefer"))
            {
                string preferHeader = Request.Headers.GetValues("Prefer").FirstOrDefault();
                if (preferHeader != null && preferHeader.ToLower() == "return=minimal")
                {
                    returnMessage.Content = null;
                }
            }

            return returnMessage;
        }

        // DELETE fhir/values/5
        [HttpDelete, Route("{ResourceName}/{id}")]
        public HttpResponseMessage Delete(string ResourceName, string id)
        {
            System.Diagnostics.Trace.WriteLine("DELETE: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("{ResourceName}");
            

            var Inputs = GetInputs(buri);

            if (ResourceName == "AuditEvent")
            {
                // depends on the AuditEvent, if was created by the server, then it will get an unauthorized exception report
                // otherwise externally reported events can be updated!
                //throw new FhirServerException(HttpStatusCode.MethodNotAllowed, "Cannot DELETE a AuditEvent");
            }
            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            string deletedIdentity = model.Delete(id, null);
            Request.Properties.Add(Const.ResourceIdentityKey, deletedIdentity);

            var msg = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(deletedIdentity))
            {
                ResourceIdentity ri = new ResourceIdentity(deletedIdentity);
                msg.Headers.Add("ETag", String.Format("W/\"{0}\"", ri.VersionId));
            }
            return msg;
            // for an OperationOutcome return would need to return accepted
        }

        // DELETE fhir/Patient?identifier=http://example.org/demo|34
        [HttpDelete, Route("{ResourceName}")]
        public HttpResponseMessage Delete(string ResourceName)
        {
            System.Diagnostics.Trace.WriteLine("DELETE: " + this.ControllerContext.Request.RequestUri.OriginalString);
            var buri = this.CalculateBaseURI("{ResourceName}");

            var Inputs = GetInputs(buri);

            if (Request.TupledParameters(false).Count() == 0)
            {
                var oo = new OperationOutcome()
                {
                    Text = Utility.CreateNarative("Precondition Error")
                };
                oo.Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent()
                    {
                        Details = new CodeableConcept(null, null, "Conditionally deleting a " + ResourceName + " requires parameters"),
                        Severity = OperationOutcome.IssueSeverity.Fatal
                    }
                };
                // return oo;
                return Request.CreateResponse(HttpStatusCode.BadRequest, oo);
            }

            if (ResourceName == "AuditEvent")
            {
                // depends on the AuditEvent, if was created by the server, then it will get an unauthorized exception report
                // otherwise externally reported events can be updated!
                //throw new FhirServerException(HttpStatusCode.MethodNotAllowed, "Cannot DELETE a AuditEvent");
            }
            IFhirResourceServiceSTU3 model = GetResourceModel(ResourceName, GetInputs(buri));

            string ifMatch = Request.RequestUri.Query;
            if (ifMatch.StartsWith("?"))
                ifMatch = ifMatch.Substring(1);
            string deletedIdentity = model.Delete(null, ifMatch);
            Request.Properties.Add(Const.ResourceIdentityKey, deletedIdentity);

            var msg = Request.CreateResponse(HttpStatusCode.NoContent);
            if (!string.IsNullOrEmpty(deletedIdentity))
            {
                ResourceIdentity ri = new ResourceIdentity(deletedIdentity);
                msg.Headers.Add("ETag", String.Format("W/\"{0}\"", ri.VersionId));
            }
            return msg;
            // for an OperationOutcome return would need to return accepted
        }
    }
}
