using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Associative container for artifact summary properties.</summary>
    public class ArtifactSummaryProperties : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        Dictionary<string, object> _props = new Dictionary<string, object>();

        /// <summary>Internal ctor.</summary>
        internal ArtifactSummaryProperties() { }

        /// <summary>Gets or sets the property value associated with the specified collection key.</summary>
        /// <param name="key">A collection key.</param>
        /// <returns>A property value, or <c>null</c>.</returns>
        public object this[string key]
        {
            get { return _props.TryGetValue(key, out object result) ? result : null; }
            set { _props[key] = value; }
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="ArtifactSummaryProperties"/>.</summary>
        /// <returns>A sequence of key/value pairs.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _props.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _props.GetEnumerator();
    }
}
