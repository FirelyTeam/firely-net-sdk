using Hl7.Fhir.Generation;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    [ApplyInterfaceToClassesOnGenerate]
    public interface IIdentifiable
    {
        List<Identifier> Identifier { get; set; }
    }
}