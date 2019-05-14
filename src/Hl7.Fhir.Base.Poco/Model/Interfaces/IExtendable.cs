using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IExtendable
    {
        List<Extension> Extension { get; set; }
    }

    public interface IModifierExtendable : IExtendable
    {
        List<Extension> ModifierExtension { get; set; }
    }
}

