using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IPrimitive : IElement
    {
        object ObjectValue { get; set; }
        string ToString();
    }
}
