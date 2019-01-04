using System;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionWalkerException : InvalidOperationException
    {
        public StructureDefinitionWalkerException()
        {
        }

        public StructureDefinitionWalkerException(string message) : base(message)
        {
        }

        public StructureDefinitionWalkerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
