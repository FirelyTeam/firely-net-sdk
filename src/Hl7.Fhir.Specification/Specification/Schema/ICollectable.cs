using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Tags that this assertion would provide on success.
    /// </summary>
    /// <remarks>
    /// Is a list of SchemaTags, since the assertion (i.e. a slice) may provide multiple
    /// possible outcomes.
    /// </remarks>
    public interface ICollectable
    {
        IEnumerable<Assertions> Collect();
    }
}


