using System;

namespace Hl7.Fhir.Generation
{
    /// <summary>
    /// Interfaces marked with this attribute will be applied to classes respecting the interface during the generation process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class ApplyInterfaceToGeneratedClassesAttribute : Attribute
    {
    }
}