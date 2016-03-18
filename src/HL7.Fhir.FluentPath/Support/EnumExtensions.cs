using System;
using System.ComponentModel;

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
            var attr = e.GetAttributeOnEnum<DescriptionAttribute>();

            if (attr != null)
                return attr.Description;
            else
                return null;
        }

    }
}
