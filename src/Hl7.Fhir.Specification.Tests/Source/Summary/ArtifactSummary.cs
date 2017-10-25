using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Represents summary information extracted from a FHIR artifact.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ArtifactSummary : IArtifactSummaryDetailsProvider
    {
        // Available to derived classes
        protected readonly ArtifactSummaryDetailsCollection _details;

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance for the specified collection of summary details.</summary>
        /// <param name="details">A collection of summary details extracted from the artifact.</param>
        public ArtifactSummary(ArtifactSummaryDetailsCollection details) { _details = details; }

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance to represent an error that occured while processing an artifact.</summary>
        /// <param name="details">A collection of summary details extracted from the artifact.</param>
        /// <param name="error">An exception that occured while processing the artifact.</param>
        public ArtifactSummary(ArtifactSummaryDetailsCollection details, Exception error) : this(details) { Error = error; }

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance to represent an error that occured while processing an artifact.</summary>
        /// <param name="details">A collection of summary details extracted from the artifact.</param>
        /// <param name="error">An exception that occured while processing the artifact.</param>
        /// <returns></returns>
        public static ArtifactSummary FromException(ArtifactSummaryDetailsCollection details, Exception error) => new ArtifactSummary(details, error);

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance to represent an error that occured while processing an artifact.</summary>
        /// <param name="origin">The original location of the artifact on disk.</param>
        /// <param name="error">An exception that occured while processing the artifact.</param>
        /// <returns></returns>
        public static ArtifactSummary FromException(string origin, Exception error)
        {
            var props = new ArtifactSummaryDetailsCollection();
            props[ArtifactSummaryDetails.OriginKey] = origin;
            return new ArtifactSummary(props, error);
        }

        /// <summary>Returns information about errors that occured while processing the artifact.</summary>
        public Exception Error { get; }

        /// <summary>Indicates if any errors occured while processing the artifact.</summary>
        /// <remarks>If <c>true</c>, then the <see cref="Error"/> property returns detailed error information.</remarks>
        public bool IsFaulted => Error != null; // cf. Task

        /// <summary>Returns the summary details associated with the specified key, or <c>null</c>.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>An object value, or <c>null</c>.</returns>
        public object this[string key] => _details[key];

        /// <summary>The original location of the associated artifact.</summary>
        public string Origin => _details.Origin();

        /// <summary>
        /// Opaque value that represents the position of the artifact within the container.
        /// Allows the <see cref="DirectorySource"/> to retrieve and deserialize the associated artifact.
        /// </summary>
        public string Position => _details.Position();

        /// <summary>The resource uri.</summary>
        /// <remarks>The <see cref="IElementNavigator"/> returns a generated value for resources that are not bundle entries.</remarks>
        public string ResourceUri => _details.ResourceUri();

        /// <summary>Returns the type name of the resource.</summary>
        public string ResourceType => _details.ResourceType();

        /// <summary>Returns the type of the resource.</summary>
        public ResourceType? Type => ModelInfo.FhirTypeNameToResourceType(ResourceType);

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual string DebuggerDisplay
            => $"{GetType().Name} for {ResourceType} | Origin: {Origin}"
                + (IsFaulted ? $" | Error: {Error.Message}" : string.Empty);
    }
}
