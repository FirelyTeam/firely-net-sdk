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
        public readonly string Location;

        public Trace(string message, string location=null, Issue details=null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Location = location;
            Details = details;
        }

        public JToken ToJson()
        {
            var result = new JObject("trace", new JProperty("message", Message));

            if(Location != null)
                result.Add(new JProperty("location", Location));

            if (Details != null)
            {
                result.Add(new JProperty("code", Details.Code));
                result.Add(new JProperty("severity", Details.Severity.GetLiteral()));
                result.Add(new JProperty("category", Details.Type.GetLiteral()));
            }

            return result;
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