using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class MarkdownMapper : IAssignMapper<Markdown, string>
    {
        public static readonly MarkdownMapper Current = new MarkdownMapper();

        public string Map(MappingContext context, Markdown source)
        {
            return source?.Value;
        }

        public Markdown Map(MappingContext context, string source)
        {
            if (source is null)
            {
                return null;
            }

            return new Markdown(source);
        }
    }
}
