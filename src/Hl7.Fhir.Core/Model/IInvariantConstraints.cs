using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IInvariantConstraints
    {
        List<ElementDefinition.ConstraintComponent> InvariantConstraints { get; set; }
    }
}
