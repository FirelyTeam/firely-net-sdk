using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Implemented by assertions that work on a single node (IElementNavigator)
    /// </summary>
    /// <remarks>
    /// Examples are fixed, binding, working on a single IElementNavigator.Value, and
    /// children, working on the children of a single IElementNavigator
    /// </remarks>
    public interface IValidatable
    {
        Assertions Validate(IElementNavigator input, ValidationContext vc);
    }
}

