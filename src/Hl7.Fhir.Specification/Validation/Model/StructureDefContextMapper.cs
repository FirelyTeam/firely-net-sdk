using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class StructureDefContextMapper : ITransferMapper<StructureDefinition.ContextComponent, UniStructureDefContext>
    {
        public static readonly StructureDefContextMapper Current = new StructureDefContextMapper();

        private static readonly EnumMapper<StructureDefinition.ExtensionContextType, UniStructureDefContextType> _typeMapper
            = new EnumMapper<StructureDefinition.ExtensionContextType, UniStructureDefContextType>(
                (StructureDefinition.ExtensionContextType.Element, UniStructureDefContextType.Element),
                (StructureDefinition.ExtensionContextType.Extension, UniStructureDefContextType.Extension),
                (StructureDefinition.ExtensionContextType.Fhirpath, UniStructureDefContextType.Fhirpath)
                );

        public void Transfer(MappingContext context, StructureDefinition.ContextComponent source, UniStructureDefContext target)
        {
            target.Expression = source.Expression;
            target.Type = source.Type.Map(context, _typeMapper);
        }

        public void Transfer(MappingContext context, UniStructureDefContext source, StructureDefinition.ContextComponent target)
        {
            target.Expression = source.Expression;
            target.Type = source.Type.Map(context, _typeMapper);
        }
    }
}
