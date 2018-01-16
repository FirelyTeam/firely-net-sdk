using Hl7.Fhir.ElementModel;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Implemented by assertions that work on groups of nodes
    /// </summary>
    /// <remarks>
    /// Examples are subgroups, ref, minItems, slice
    /// </remarks>
    public interface IGroupValidatable
    {
        List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc);
    }
}



