using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IElement : IExtendable
    {
        string TypeName { get; }
        string ElementId { get; set; }
    }
}
