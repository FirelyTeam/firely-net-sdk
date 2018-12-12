using System;

namespace Hl7.Fhir.Specification.Schema
{
    internal class InvalidValidationContextException : Exception
    {
        public InvalidValidationContextException(string message) : base(message)
        {
        }

        public InvalidValidationContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
