using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    // TODO: keep them as separate entries
    // TODO: Do something with issues
    // TODO: remove duplicates
    public class Trace : IAssertion
    {
        public readonly string Message;
        public readonly Issue Details;

        public Trace(string message, Issue details=null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Details = details;
        }

        public JToken ToJson()
        {
            var trace = new JObject(new JProperty("message", Message));

            if (Details != null)
            {
                trace.Add(new JProperty("code", Details.Code));
                trace.Add(new JProperty("severity", Details.Severity.GetLiteral()));
                trace.Add(new JProperty("category", Details.Type.GetLiteral()));
            }

            return new JProperty("trace", trace);
        }

    }

    //public class TraceDetails
    //{
    //    public readonly string Code;
    //    public readonly IssueSeverity Severity;
    //    public readonly IssueCategory? Category;

    //    public TraceDetails(string code, IssueSeverity severity, IssueCategory? category = null)
    //    {
    //        Code = code ?? throw new ArgumentNullException(nameof(code));
    //        Severity = severity;
    //        Category = category;
    //    }

    //    public enum IssueSeverity
    //    {
    //        Fatal,
    //        Error,
    //        Warning,
    //        Information
    //    }

    //    public enum IssueCategory
    //    {
    //        /// <summary>
    //        /// Content invalid against the specification or a profile.
    //        /// </summary>
    //        Invalid,

    //        BusinessRule,

    //        Incomplete,

    //        NotSupported,

    //        Exception,

    //        CodeInvalid
    //    }
    //}
}