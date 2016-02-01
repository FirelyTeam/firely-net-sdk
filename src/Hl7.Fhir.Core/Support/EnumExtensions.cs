using Hl7.Fhir.Introspection;
using System;

namespace Hl7.Fhir.Support
{
    public static class EnumExtensions
    {
        public static string GetLiteral(this Enum e)
        {
            var attr = e.GetAttributeOnEnum<EnumLiteralAttribute>();

            if (attr != null)
                return attr.Literal;
            else
                return null;
        }

        public static string GetDocumentation(this Enum e)
        {
            var attr = e.GetAttributeOnEnum<EnumDocumentationAttribute>();

            if (attr != null)
                return attr.Documentation;
            else
                return null;
        }

    }
}
