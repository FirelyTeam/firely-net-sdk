using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Support
{
    /// <summary>
    /// The exception that is throw when the artifact resolver encounters conflicting conformance resources with identical canonical urls.
    /// <para>
    /// Obsolete. The FHIR API no longer throws this exception.
    /// Clients should catch the new <see cref="ResolvingConflictException"/> instead.
    /// </para>
    /// </summary>
    /// <seealso cref="ResolvingConflictException"/>
    [Obsolete("This exception is obsolete and has been replaced by the more generic UrlConflictException. The FHIR API longer throws this exception. New clients should catch the UrlConflictException instead.")]
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

        public CanonicalUrlConflictException(IEnumerable<CanonicalUrlConflict> conflicts) : this(conflicts.ToArray())
        {
            //
        }

        public CanonicalUrlConflictException(CanonicalUrlConflict[] conflicts) : base(formatMessage(conflicts))
        {
            Conflicts = conflicts ?? throw Error.ArgumentNull(nameof(conflicts));
        }

        public CanonicalUrlConflictException(CanonicalUrlConflict conflict) : base(formatMessage(conflict))
        {
            if (conflict == null) { throw Error.ArgumentNull(nameof(conflict)); }
            Conflicts = new CanonicalUrlConflict[] { conflict };
        }

        public bool IsResolved => Conflicts?.All(c => c.FilePaths.Count() == 1) == true;

        /// <summary>Returns a list of canonical url conflicts.</summary>
        public CanonicalUrlConflict[] Conflicts { get; private set; }

        readonly static string errorMessage = "Found conflicting Conformance Resource artifacts with the same canonical url identifier.";

        static string formatMessage(params CanonicalUrlConflict[] conflicts)
        {
            if (conflicts != null && conflicts.Length > 0)
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
            return null;
        }
    }

}
