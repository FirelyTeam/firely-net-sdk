using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Associative container for storing and retrieving artifact summary details by key.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates a new <see cref="ArtifactSummaryDetails"/>
    /// collection for each artifact it has discovered. Then the generator calls all the available
    /// <see cref="ArtifactSummaryDetailsExtractor"/> delegates to allow them to extract various summary
    /// details from the artifact and store the results into the collection. Finally, the generator
    /// creates an <see cref="ArtifactSummary"/> instance from the summary details collection.
    /// </remarks>
    /// <seealso cref="ArtifactSummaryDetailsExtractor"/>.
    /// <seealso cref="ArtifactSummaryGenerator"/>.
    public class ArtifactSummaryDetails : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        Dictionary<string, object> _props = new Dictionary<string, object>();

        /// <summary>Internal ctor.</summary>
        internal ArtifactSummaryDetails() { }

        /// <summary>Gets or sets the property value associated with the specified collection key.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>A property value, or <c>null</c>.</returns>
        public object this[string key]
        {
            get { return _props.TryGetValue(key, out object result) ? result : null; }
            set { _props[key] = value; }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="ArtifactSummaryDetails"/>.</summary>
        /// <returns>A sequence of key/value pairs.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _props.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _props.GetEnumerator();
    }
}
