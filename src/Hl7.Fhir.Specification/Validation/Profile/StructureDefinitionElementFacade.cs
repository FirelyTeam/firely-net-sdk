using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class StructureDefinitionElementFacade : IStructureDefinitionElement
    {
        public readonly ElementDefinition ElementDefinition;

        public StructureDefinitionElementFacade(ElementDefinition elementDefinition, IEnumerable<IStructureDefinitionSlice> slices = null)
        {
            ElementDefinition = elementDefinition;
            Slices = new CollectionFacade<IStructureDefinitionSlice, ElementDefinition>(elementDef => new StructureDefinitionSliceFacade(elementDef), slices);
        }

        public ICollectionFacade<IStructureDefinitionSlice> Slices { get; }
    }
}
