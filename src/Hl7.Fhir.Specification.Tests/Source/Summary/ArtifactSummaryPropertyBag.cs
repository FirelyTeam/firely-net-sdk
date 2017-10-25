using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public class ArtifactSummaryPropertyBag
    {
        Dictionary<string, object> _props = new Dictionary<string, object>();

        internal ArtifactSummaryPropertyBag() { }

        public object this[string key]
        {
            get { return _props.TryGetValue(key, out object result) ? result : null; }
            set { _props[key] = value; }
        }
    }
}
