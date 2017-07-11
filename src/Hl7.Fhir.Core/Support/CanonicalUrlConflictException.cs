using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support
{
    /// <summary>
    /// The exception that is throw when the artifact resolver encounters conflicting conformance resources with identical canonical urls.
    /// </summary>
    public class CanonicalUrlConflictException : Exception
    {
        /// <summary>Provides information about conflicting conformance resources identified by the same canonical url.</summary>
        public class CanonicalUrlConflict
        {
            public CanonicalUrlConflict(string url, IEnumerable<string> filePaths)
            {
                Url = url;
                FilePaths = filePaths.ToArray();
            }

            /// <summary>The canonical url that identifies multiple conformance resources.</summary>
            public string Url { get; private set; }

            /// <summary>File paths of conflicting conformance resources identified by the same canonical url.</summary>
            public string[] FilePaths { get; private set; }
        }

        public CanonicalUrlConflictException(IEnumerable<CanonicalUrlConflict> conflicts) : base(formatMessage(conflicts))
        {
            Conflicts = conflicts.ToArray();
        }

        public bool IsResolved => Conflicts?.All(c => c.FilePaths.Count() == 1) == true;

        /// <summary>Returns a list of canonical url conflicts.</summary>
        public CanonicalUrlConflict[] Conflicts { get; private set; }

        private readonly static string errorMessage = "Found conflicting Conformance Resource artifacts with the same canonical url identifier.";

        private static string formatMessage(IEnumerable<CanonicalUrlConflict> conflicts)
        {
            StringBuilder sb = new StringBuilder(errorMessage);
            sb.AppendLine();
            sb.AppendLine();
            foreach (var conflict in conflicts)
            {
                sb.Append("Url: ");
                sb.AppendLine(conflict.Url);
                foreach (var file in conflict.FilePaths)
                {
                    sb.Append("   File: ");
                    sb.AppendLine(file);
                }
            }
            return sb.ToString();
        }
    }

}
