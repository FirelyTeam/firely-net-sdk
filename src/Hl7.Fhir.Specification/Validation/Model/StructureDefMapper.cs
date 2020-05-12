using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    public class StructureDefMapper : ITransferMapper<StructureDefinition, UniStructureDef>
    {
        public static readonly StructureDefMapper Current = new StructureDefMapper();

        private static readonly EnumMapper<PublicationStatus, UniStructureDefStatus> _statusMapper
            = new EnumMapper<PublicationStatus, UniStructureDefStatus>(
                (PublicationStatus.Active, UniStructureDefStatus.Active),
                (PublicationStatus.Draft, UniStructureDefStatus.Draft),
                (PublicationStatus.Retired, UniStructureDefStatus.Retired),
                (PublicationStatus.Unknown, UniStructureDefStatus.Unknown)
                );

        private static readonly EnumMapper<StructureDefinition.StructureDefinitionKind, UniStructureDefKind> _kindMapper
            = new EnumMapper<StructureDefinition.StructureDefinitionKind, UniStructureDefKind>(
                (StructureDefinition.StructureDefinitionKind.ComplexType, UniStructureDefKind.ComplexType),
                (StructureDefinition.StructureDefinitionKind.Logical, UniStructureDefKind.Logical),
                (StructureDefinition.StructureDefinitionKind.PrimitiveType, UniStructureDefKind.PrimitiveType),
                (StructureDefinition.StructureDefinitionKind.Resource, UniStructureDefKind.Resource)
                );

        private static readonly EnumMapper<StructureDefinition.TypeDerivationRule, UniStructureDefDerivation> _derivationMapper
            = new EnumMapper<StructureDefinition.TypeDerivationRule, UniStructureDefDerivation>(
                (StructureDefinition.TypeDerivationRule.Constraint, UniStructureDefDerivation.Constraint),
                (StructureDefinition.TypeDerivationRule.Specialization, UniStructureDefDerivation.Specialization)
                );

        public void Transfer(MappingContext context, StructureDefinition source, UniStructureDef target)
        {
            target.Url = source.Url;
            target.Identifiers = source.Identifier.Map(context, IdentifierMapper.Current);
            target.Version = source.Version;
            target.Name = source.Name;
            target.Title = source.Title;
            target.Status = source.Status.Map(context, _statusMapper);
            target.Experimental = source.Experimental;
            target.Date = source.Date;
            target.Publisher = source.Publisher;
            target.Contacts = source.Contact.Map(context, ContactDetailMapper.Current);
            target.Description = source.Description.Map(context, MarkdownMapper.Current);
            target.UseContexts = source.UseContext.Map(context, UsageContextMapper.Current);
            target.Jurisdictions = source.Jurisdiction.Map(context, CodeableConceptMapper.Current);
            target.Purpose = source.Purpose.Map(context, MarkdownMapper.Current);
            target.Copyright = source.Copyright.Map(context, MarkdownMapper.Current);
            target.Keywords = source.Keyword.Map(context, CodingMapper.Current);
            target.FhirVersion = source.FhirVersion.Map(context, FhirVersionMapper.Current);
            target.Mappings = source.Mapping.Map(context, StructureDefMappingMapper.Current);
            target.Kind = source.Kind.Map(context, _kindMapper);
            target.Abstract = source.Abstract;
            target.ExtensionContexts = source.Context.Map(context, StructureDefContextMapper.Current);
            target.ExtensionContextInvariants = source.ContextInvariant.Map();
            target.Type = source.Type;
            target.BaseDefinition = source.BaseDefinition;
            target.Derivation = source.Derivation.Map(context, _derivationMapper);

            var mapper = new StructureDefElementSchemaMapper(source);
            target.Differential = source.Differential?.Element.Map(context, mapper);
            target.Snapshot = source.Snapshot?.Element.Map(context, mapper);
        }

        public void Transfer(MappingContext context, UniStructureDef source, StructureDefinition target)
        {
            target.Url = source.Url;
            target.Identifier = source.Identifiers.Map(context, IdentifierMapper.Current);
            target.Version = source.Version;
            target.Name = source.Name;
            target.Title = source.Title;
            target.Status = source.Status.Map(context, _statusMapper);
            target.Experimental = source.Experimental;
            target.Date = source.Date;
            target.Publisher = source.Publisher;
            target.Contact = source.Contacts.Map(context, ContactDetailMapper.Current);
            target.Description = source.Description.Map(context, MarkdownMapper.Current);
            target.UseContext = source.UseContexts.Map(context, UsageContextMapper.Current);
            target.Jurisdiction = source.Jurisdictions.Map(context, CodeableConceptMapper.Current);
            target.Purpose = source.Purpose.Map(context, MarkdownMapper.Current);
            target.Copyright = source.Copyright.Map(context, MarkdownMapper.Current);
            target.Keyword = source.Keywords.Map(context, CodingMapper.Current);
            target.FhirVersion = source.FhirVersion.Map(context, FhirVersionMapper.Current);
            target.Mapping = source.Mappings.Map(context, StructureDefMappingMapper.Current);
            target.Kind = source.Kind.Map(context, _kindMapper);
            target.Abstract = source.Abstract;
            target.Context = source.ExtensionContexts.Map(context, StructureDefContextMapper.Current);
            target.ContextInvariant = source.ExtensionContextInvariants.Map();
            target.Type = source.Type;
            target.BaseDefinition = source.BaseDefinition;
            target.Derivation = source.Derivation.Map(context, _derivationMapper);

            var mapper = new StructureDefElementSchemaMapper(target);
            var elements = source.Differential.Map(context, mapper);
            target.Differential = elements is null ? null : new StructureDefinition.DifferentialComponent { Element = elements };
            elements = source.Snapshot.Map(context, mapper);
            target.Snapshot = elements is null ? null : new StructureDefinition.SnapshotComponent { Element = elements };
        }
    }
}
