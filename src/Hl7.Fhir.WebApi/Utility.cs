/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Hl7.Fhir.WebApi
{
    public static class Utility
    {
        #region << OperationOutcome Extensions >>
        static OperationOutcome.IssueSeverity IssueSeverityOf(HttpStatusCode code)
        {
            int range = ((int)code % 100);
            switch (range)
            {
                case 100:
                case 200: return OperationOutcome.IssueSeverity.Information;
                case 300: return OperationOutcome.IssueSeverity.Warning;
                case 400: return OperationOutcome.IssueSeverity.Error;
                case 500: return OperationOutcome.IssueSeverity.Fatal;
                default: return OperationOutcome.IssueSeverity.Information;
            }
        }

        private static void setContentHeaders(HttpResponseMessage response, ResourceFormat format)
        {
            response.Content.Headers.ContentType = FhirMediaType.GetMediaTypeHeaderValue(typeof(Resource), format);
        }

        public static OperationOutcome Init(this OperationOutcome outcome)
        {
            if (outcome.Issue == null)
            {
                outcome.Issue = new List<OperationOutcome.IssueComponent>();
            }
            return outcome;
        }

        public static OperationOutcome Error(this OperationOutcome outcome, Exception exception)
        {
            string message;

            if (exception is FhirServerException)
                message = exception.Message;
            else
                message = string.Format("{0}: {1}", exception.GetType().Name, exception.Message);

            var baseResult = outcome.Error(message);

            // Don't add a stack trace if this is an acceptable logical-level error
            if (!(exception is FhirServerException))
            {
                var stackTrace = new OperationOutcome.IssueComponent();
                stackTrace.Severity = OperationOutcome.IssueSeverity.Information;
                stackTrace.Details = new CodeableConcept(null,null, exception.StackTrace);
                baseResult.Issue.Add(stackTrace);
            }

            return baseResult;
        }

        public static OperationOutcome Error(this OperationOutcome outcome, string message)
        {
            return outcome.AddIssue(OperationOutcome.IssueSeverity.Error, message);
        }

        public static OperationOutcome Message(this OperationOutcome outcome, string message)
        {
            return outcome.AddIssue(OperationOutcome.IssueSeverity.Information, message);
        }

        public static OperationOutcome Message(this OperationOutcome outcome, HttpStatusCode code, string message)
        {
            return outcome.AddIssue(IssueSeverityOf(code), message);
        }

        private static OperationOutcome AddIssue(this OperationOutcome outcome, OperationOutcome.IssueSeverity severity, string message)
        {
            if (outcome.Issue == null) outcome.Init();

            var item = new OperationOutcome.IssueComponent();
            item.Severity = severity;
            item.Details = new CodeableConcept(null, null, message);
            outcome.Issue.Add(item);
            return outcome;
        }

        public static HttpResponseMessage ToHttpResponseMessage(this OperationOutcome outcome, ResourceFormat target, HttpRequestMessage request)
        {
            byte[] data = null;
            if (target == ResourceFormat.Xml)
                data = FhirSerializer.SerializeResourceToXmlBytes((OperationOutcome)outcome);
            else if (target == ResourceFormat.Json)
                data = FhirSerializer.SerializeResourceToJsonBytes((OperationOutcome)outcome);

            HttpResponseMessage response = new HttpResponseMessage();
            //setResponseHeaders(response, target);
            response.Content = new ByteArrayContent(data);
            setContentHeaders(response, target);

            return response;
        }
        #endregion

        #region << WebApi Controller Extensions >>
        public static string CalculateBaseURI(this System.Web.Http.ApiController me, string resourceName)
        {
            Uri ri = me.ControllerContext.Request.RequestUri;
            if (resourceName == "metadata" || resourceName == "${operation}")
            {
                return String.Format("{0}://{1}{2}{3}{4}/",
                    ri.Scheme,
                    ri.Host,
                    ri.IsDefaultPort ? "" : ":" + ri.Port.ToString(),
                    me.ControllerContext.RequestContext.VirtualPathRoot.TrimEnd('/') + '/',
                    me.ControllerContext.RouteData.Route.RouteTemplate.Replace("/metadata", "").Replace("/${operation}", ""));
            }
            if (me.ControllerContext.RouteData.Route.RouteTemplate.Contains("{ResourceName}"))
                resourceName = "{ResourceName}";
            string baseUri = String.Format("{0}://{1}{2}{3}{4}",
                ri.Scheme,
                ri.Host,
                ri.IsDefaultPort ? "" : ":" + ri.Port.ToString(),
                me.ControllerContext.RequestContext.VirtualPathRoot.TrimEnd('/')+'/',
                me.ControllerContext.RouteData.Route.RouteTemplate.Substring(0, me.ControllerContext.RouteData.Route.RouteTemplate.LastIndexOf(resourceName)));
            return baseUri;
        }

        #endregion

        #region << Parameter Extractions Extensions >>
        public static string GetString(this Parameters me, string name)
        {
            var value = me.Parameter.Where(s => s.Name == name).FirstOrDefault();
            if (value == null)
                return null;
            if (value.Value as FhirString != null)
                return ((FhirString)value.Value).Value;
            if (value.Value as FhirUri != null)
                return ((FhirUri)value.Value).Value;
            return null;
        }

        public static Resource GetResource(this Parameters me, string name)
        {
            var value = me.Parameter.Where(s => s.Name == name).FirstOrDefault();
            if (value == null)
                return null;
            return value.Resource;
        }
        #endregion

        public static Uri AppendToQuery(this Uri me, string query)
        {
            if (string.IsNullOrEmpty(me.Query))
                return new Uri(me.OriginalString + "?" + query.TrimStart('?').TrimEnd('&'));
            return new Uri(me.OriginalString + "&" + query.TrimEnd('&'));
        }

        #region << Div Content Formatting helpers >>
        public const string TextDivFieldType = "span";
        public const string TextDivFieldStyle = " style=\"color: gray;\"";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textContent"></param>
        /// <param name="fieldname">The fieldname to be displayed as a prefix</param>
        /// <param name="format">the format string, 
        /// {0} is the HTML element type to use for the field - set at span
        /// {1} is the style to be used in this HTML field span
        /// {2} is the name of the field
        /// </param>
        /// <param name="args"></param>
        static public void AppendFormatFHIRFields(this StringBuilder textContent, string fieldname, string format, params object[] args)
        {
            if (!string.IsNullOrEmpty(fieldname))
                textContent.AppendFormat("		<{0}{1}>{2}:</{0}> ", TextDivFieldType, TextDivFieldStyle, fieldname);
            textContent.AppendFormat(format, args);
            textContent.AppendFormat("<br/>\r\n");
        }

        public static Hl7.Fhir.Model.Narrative CreateNarative(string div)
        {
            var n = new Hl7.Fhir.Model.Narrative();
            if (!string.IsNullOrEmpty(div) && !div.StartsWith("<div", StringComparison.CurrentCultureIgnoreCase))
                n.Div = String.Format("<div xmlns=\"http://www.w3.org/1999/xhtml\">{0}</div>", div);
            else
                n.Div = div;
            return n;
        }

        public static Hl7.Fhir.Model.CodeableConcept CreateCodeableConcept(string system, string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            if (!system.StartsWith("http://"))
                system = "http://" + system;
            var n = new Hl7.Fhir.Model.CodeableConcept(system, code);
            return n;
        }

        public static string[] CreateStringArray(object value)
        {
            if (value == null)
                return null;
            if (value is string)
                return (value as string).Split('\r');
            return null;
        }
        #endregion

        #region << Static Utility functions for extracting common fhir types >>
        public static DateTime? ToDateTime(this FhirDateTime me)
        {
            if (me == null)
                return null;
            DateTime result;
            if (DateTime.TryParse(me.Value, out result))
                return result;
            if (!string.IsNullOrEmpty(me.Value))
            {
                // the date didn't parse, one of the common mistakes
                // with dates is not to include the - symbols
                // so lets put them in and proceed
                if (me.Value.Length == 8 && !me.Value.Contains("-"))
                {
                    string newValue = me.Value.Insert(4, "-").Insert(7, "-");
                    System.Diagnostics.Trace.WriteLine(String.Format("Invalid Date [{0}] was encountered, processing it as though it was [{1}]", me.Value, newValue));
                    if (DateTime.TryParse(newValue, out result))
                        return result;
                }
            }
            return null;
        }

        public static DateTime? ToDateTime(this Date me)
        {
            if (me == null)
                return null;
            DateTime result;
            if (DateTime.TryParse(me.Value, out result))
                return result;
            if (!string.IsNullOrEmpty(me.Value))
            {
                // the date didn't parse, one of the common mistakes
                // with dates is not to include the - symbols
                // so lets put them in and proceed
                if (me.Value.Length == 8 && !me.Value.Contains("-"))
                {
                    string newValue = me.Value.Insert(4, "-").Insert(7, "-");
                    System.Diagnostics.Trace.WriteLine(String.Format("Invalid Date [{0}] was encountered, processing it as though it was [{1}]", me.Value, newValue));
                    if (DateTime.TryParse(newValue, out result))
                        return result;
                }
            }
            return null;
        }

        public static string ToFhirId(this System.Guid me)
        {
            return me.ToString("n");
        }

        public static string ToFhirId(this System.Guid? me)
        {
            if (me.HasValue)
                return me.Value.ToString("n");
            return null;
        }

        public static string ToFhirDate(this System.DateTime me)
        {
            return me.ToString("yyyy-MM-dd");
        }

        public static string ToFhirDate(this System.DateTime? me)
        {
            if (me.HasValue)
                return me.Value.ToString("yyyy-MM-dd");
            return null;
        }

        public static string ToFhirDateTime(this System.DateTime me)
        {
            return me.ToString("yyyy-MM-ddTHH:mm:ss.sss");
        }

        public static string ToFhirDateTime(this System.DateTime? me)
        {
            if (me.HasValue)
                return me.Value.ToString("yyyy-MM-ddTHH:mm:ss.sss");
            return null;
        }
        #endregion

    }
}
