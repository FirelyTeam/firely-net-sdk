using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    class ValidationContext
    {
        public ITerminologyService TerminologyService;

        public IExceptionSource ExceptionSink;
    }
}
