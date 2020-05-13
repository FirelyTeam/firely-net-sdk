
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    public interface IStructureDefinitionContextComponent : IBackboneElement2
    {
        FhirString ExpressionElement { get; set; }
        Code<StructureDefinitionExtensionContextType> TypeElement { get; set; }
    }

    /// <summary>
    /// How an extension context is interpreted.
    /// (url: http://hl7.org/fhir/ValueSet/extension-context-type)
    /// </summary>
    [FhirEnumeration("ExtensionContextType")]
    public enum StructureDefinitionExtensionContextType
    {
        /// <summary>
        /// The context is all elements that match the FHIRPath query found in the expression.
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("fhirpath", "http://hl7.org/fhir/extension-context-type"), Description("FHIRPath")]
        Fhirpath,

        /// <summary>
        /// The context is any element that has an ElementDefinition.id that matches that found in the expression.
        /// This includes ElementDefinition Ids that have slicing identifiers. The full path for the element is 
        /// [url]#[elementid]. If there is no #, the Element id is one defined in the base specification.
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("element", "http://hl7.org/fhir/extension-context-type"), Description("Element ID")]
        Element,

        /// <summary>
        /// The context is a particular extension from a particular StructureDefinition, and the expression is
        /// just a uri that identifies the extension.
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/extension-context-type"), Description("Extension URL")]
        Extension,
    }

}