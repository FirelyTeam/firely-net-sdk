using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Support
{
    // [WMR 20180917]
    // Could implement specific subclasses, but does not seem very useful (yet?)
    //   - client that wants to handle all conflict errors can just catch the base class
    //   - client that performs specialized resolver call also knows how to interpret any conflicts

    /// <summary>
    /// The exception that is thrown to report a resource resolving conflict.
    /// <para>
    /// Indicates that the source was unable to resolve a single target resource,
    /// because it found multiple existing resources matching the specified identifier.
    /// </para>
    /// </summary>
    /// <remarks>Generic replacement for the obsolete <seealso cref="CanonicalUrlConflictException"/>.</remarks>
    public class ResolvingConflictException : InvalidOperationException
    {
        #region Factory

        readonly static string ResourceUriConflictErrorMessage = "Found multiple conflicting resources with the same resource uri identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting resources with the same Resource Uri identifier.</summary>
        public static ResolvingConflictException ResourceUriConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(ResourceUriConflictErrorMessage, conflicts);

        readonly static string CanonicalUrlConflictErrorMessage = "Found multiple conflicting conformance resources with the same canonical url identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting naming system resources with the same unique identifier.</summary>
        public static ResolvingConflictException CanonicalUrlConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(CanonicalUrlConflictErrorMessage, conflicts);

        readonly static string NamingSystemUniqueIdConflictErrorMessage = "Found multiple conflicting NamingSystem resources with the same unique identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting NamingSystem resources with the same unique identifier.</summary>
        public static ResolvingConflictException NamingSystemUniqueIdConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(NamingSystemUniqueIdConflictErrorMessage, conflicts);

        readonly static string ValueSetSystemConflictErrorMessage = "Found multiple conflicting ValueSet resources with the same unique system identifier.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting ValueSet resources with the same unique system identifier.</summary>
        public static ResolvingConflictException ValueSetSystemConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(ValueSetSystemConflictErrorMessage, conflicts);

        readonly static string ConceptMapUrlConflictErrorMessage = "Found multiple conflicting ConceptMap resources with the same source or target url.";

        /// <summary>Generate a new <see cref="ResolvingConflictException"/> to report multiple conflicting ConceptMap resources with the same source or target url.</summary>
        public static ResolvingConflictException ConceptMapUrlConflict(IEnumerable<ResolvingConflict> conflicts)
            => new ResolvingConflictException(ConceptMapUrlConflictErrorMessage, conflicts);

        #endregion

        /// <summary>
        /// Provides information about a specific resolving conflict,
        /// as reported by the <see cref="ResolvingConflictException"/>.
        /// </summary>
        public class ResolvingConflict
        {
            /// <summary>Create a new <see cref="ResolvingConflict"/> instance.</summary>
            /// <param name="identifier">An identifier that matches multiple conflicting resources.</param>
            /// <param name="origins">The original locations (e.g. file paths) of the conflicting resources that match the specified <paramref name="identifier"/> value.</param>
            public ResolvingConflict(string identifier, IEnumerable<string> origins)
            {
                Identifier = identifier;
                Origins = origins.ToArray();
            }

            /// <summary>The identifier value matched by multiple conflicting resources.</summary>
            public string Identifier { get; }

            /// <summary>The original locations (e.g. file paths) of conflicting resources that match the specified <see cref="Identifier"/> value.</summary>
            public string[] Origins { get; }
        }

        /// <summary>
        /// Create a new generic <see cref="ResolvingConflictException"/> instance.
        /// <para>
        /// The <see cref="ResolvingConflictException"/> class also provides static
        /// factory methods to create exceptions for specific types of conflicts.
        /// </para>
        /// </summary>
        ResolvingConflictException(string errorMessage, IEnumerable<ResolvingConflict> conflicts)
             : base(formatMessage(errorMessage, conflicts))
        {
            Conflicts = conflicts.ToArray();
        }

        /// <summary>Returns a list of resolving conflicts.</summary>
        public ResolvingConflict[] Conflicts { get; private set; }

        static string formatMessage(string errorMessage, IEnumerable<ResolvingConflict> conflicts)
        {
            if (conflicts != null && conflicts.Any())
            {
                StringBuilder sb = new StringBuilder(errorMessage);
                sb.AppendLine();
                sb.AppendLine();
                foreach (var conflict in conflicts)
                {
                    sb.Append("Url: ");
                    sb.AppendLine(conflict.Identifier);
                    foreach (var file in conflict.Origins)
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
