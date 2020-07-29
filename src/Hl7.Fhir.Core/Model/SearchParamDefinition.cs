using System;
using System.Diagnostics;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model
{
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class SearchParamDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        private string DebuggerDisplay
        {
            get
            {
                return String.Format("{0} {1} {2} ({3})", Resource, Name, Type, Expression);
            }
        }

        public string Resource { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public SearchParamType Type { get; set; }

        /// <summary>
        /// If this search parameter is a Composite, this array contains 
        /// the list of search parameters the param is a combination of
        /// </summary>
        public string[] CompositeParams { get; set; }

        /// <summary>
        /// One or more paths into the Resource instance that the search parameter 
        /// uses 
        /// </summary>
        public string[] Path { get; set; }

        /// <summary>
        /// The XPath expression for evaluating this search parameter
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// The FHIR Path expresssion that can be used to extract the data
        /// for this search parameter
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// If this is a reference, the possible types of resources that the
        /// parameters references to
        /// </summary>
        public ResourceType[] Target { get; set; }
    }
}
