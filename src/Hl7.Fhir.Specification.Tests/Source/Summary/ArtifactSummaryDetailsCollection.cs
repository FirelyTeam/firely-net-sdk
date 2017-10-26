using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Common interface for retrieving artifact summary details by key.</summary>
    /// <remarks>
    /// Implemented by both <see cref="ArtifactSummary"/> and <see cref="ArtifactSummaryDetailsCollection"/>.
    /// Target of common extension methods for retrieving specific summary details.
    /// </remarks>
    public interface IArtifactSummaryDetailsProvider
    {
        /// <summary>Get the summary detail value with the specified key, if it exists, or <c>null</c> otherwise.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>A summary detail value, or <c>null</c>.</returns>
        object this[string key] { get; }
    }

    /// <summary>Associative container for storing and retrieving artifact summary details by key.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates a new <see cref="ArtifactSummaryDetailsCollection"/>
    /// instance for each artifact it has discovered. Then the generator calls all the available
    /// <see cref="ArtifactSummaryDetailsExtractor"/> delegates to allow them to extract various summary
    /// details from the artifact and store the results into the collection. Finally, the generator
    /// creates an <see cref="ArtifactSummary"/> instance from the summary details collection.
    /// </remarks>
    /// <seealso cref="ArtifactSummaryDetailsExtractor"/>.
    /// <seealso cref="ArtifactSummaryGenerator"/>.
    public class ArtifactSummaryDetailsCollection :
        IArtifactSummaryDetailsProvider,
        IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        Dictionary<string, object> _props = new Dictionary<string, object>();

        /// <summary>Internal ctor.</summary>
        internal ArtifactSummaryDetailsCollection() { }

        /// <summary>Gets or sets the property value associated with the specified collection key.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>A property value, or <c>null</c>.</returns>
        public object this[string key]
        {
            get { return _props.TryGetValue(key, out object result) ? result : null; }
            set { _props[key] = value; }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="ArtifactSummaryDetailsCollection"/>.</summary>
        /// <returns>A sequence of key/value pairs.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _props.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _props.GetEnumerator();
    }
}
