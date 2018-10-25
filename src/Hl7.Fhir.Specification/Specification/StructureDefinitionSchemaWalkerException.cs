using System;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionSchemaWalkerException : InvalidOperationException
    {
        public StructureDefinitionSchemaWalkerException()
        {
        }

        public StructureDefinitionSchemaWalkerException(string message) : base(message)
        {
        }

        public StructureDefinitionSchemaWalkerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
