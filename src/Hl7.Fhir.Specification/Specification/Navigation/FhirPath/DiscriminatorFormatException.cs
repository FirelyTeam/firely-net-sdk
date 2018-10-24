using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Navigation.FhirPath
{
    public class DiscriminatorFormatException : FormatException
    {
        public DiscriminatorFormatException()
        {
        }

        public DiscriminatorFormatException(string message) : base(message)
        {
        }

        public DiscriminatorFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
