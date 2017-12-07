using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IBinary
    {
        byte[] Content { get; set; }
        string ContentType { get; set; }
    }
}
