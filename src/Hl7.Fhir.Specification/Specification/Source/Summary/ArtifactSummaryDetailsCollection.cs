using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source.Summary
{
    /// <summary>Common interface for retrieving artifact summary details by key.</summary>
    /// <remarks>
    /// Implemented by <see cref="ArtifactSummaryDetailsCollection"/> as well as <see cref="ArtifactSummary"/>.
    /// Allows common extension methods for retrieving specific summary details.
    /// </remarks>
    public interface IArtifactSummaryDetailsProvider
    {
        /// <summary>Get the summary detail value with the specified key, if it exists, or <c>null</c> otherwise.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>A summary detail value, or <c>null</c>.</returns>
        object this[string key] { get; }
    }

    /// <summary>Stores and retrieves artifact summary details by key.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates a new <see cref="ArtifactSummaryDetailsCollection"/>
    /// instance for each input artifact it encounters. Then the generator calls all the default and custom
    /// <see cref="ArtifactSummaryDetailsExtractor"/> delegates to allow them to extract various summary
    /// details from the artifact and store the results into the collection. Finally, the generator
    /// creates an <see cref="ArtifactSummary"/> instance from the initialized collection.
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

        /// <summary>Gets or sets the property value associated with the specified key.</summary>
        /// <param name="key">The key of the value to get or set.</param>
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
