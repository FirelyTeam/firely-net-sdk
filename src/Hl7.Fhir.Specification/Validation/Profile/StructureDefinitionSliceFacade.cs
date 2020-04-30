using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class StructureDefinitionSliceFacade : IStructureDefinitionSlice
    {
        public readonly ElementDefinition ElementDefinition;

        public StructureDefinitionSliceFacade(ElementDefinition elementDefinition, IEnumerable<IStructureDefinitionElement> elements = null)
        {
            ElementDefinition = elementDefinition;
            Elements = new CollectionFacade<IStructureDefinitionElement, ElementDefinition>(elementDef => new StructureDefinitionElementFacade(elementDef), elements);
        }

        public ICollectionFacade<IStructureDefinitionElement> Elements { get; }
    }
}
