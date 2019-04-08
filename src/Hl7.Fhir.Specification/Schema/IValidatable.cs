using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Implemented by assertions that work on a single ITypedElement.
    /// </summary>
    interface IValidatable
    {
        OperationOutcome Validate(ITypedElement input, ValidationContext vc);
    }
}
