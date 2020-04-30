using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class StructureDefinitionFacade: IStructureDefinition
    {
        private readonly StructureDefinition _structureDef;

        public StructureDefinitionFacade(StructureDefinition structureDef)
        {
            _structureDef = structureDef;
            Identifiers = new CollectionFacade<IdentifierFacade, Identifier>(structureDef.Identifier, identifier => new IdentifierFacade(identifier));
            Differential = new StructureDefinitionSchemaFacade(structureDef.Differential.Element, structureDef);
            Snapshot = new StructureDefinitionSchemaFacade(structureDef.Snapshot.Element, structureDef);
        }

        public string Url { get => _structureDef.Url; set => _structureDef.Url = value; }

        public ICollectionFacade<IIdentifier> Identifiers { get; }

        public string Version { get => _structureDef.Version; set => _structureDef.Version = value; }
        public string Name { get => _structureDef.Name; set => _structureDef.Name = value; }
        public string Title { get => _structureDef.Title; set => _structureDef.Title = value; }

        public IStructureDefinitionSchema Differential { get; }

        public IStructureDefinitionSchema Snapshot { get; }

        public void Commit()
        {
            Differential.Commit();
            Snapshot.Commit();
        }
    }
}
