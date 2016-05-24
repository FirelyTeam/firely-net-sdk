using Hl7.Fhir.Introspection;
using System;
using System.ComponentModel;

namespace Hl7.Fhir.Support
{
    public static class EnumUtility
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

        public static T? ParseLiteral<T>(string rawValue) where T : struct
        {
            if (!typeof(T).IsEnum()) Error.Argument("Type argument is not an enumeration");

            foreach (var enumValue in ReflectionHelper.FindEnumFields(typeof(T)))
            {
                var attr = ReflectionHelper.GetAttribute<EnumLiteralAttribute>(enumValue);
                if (attr != null)
                {
                    if (attr.Literal == rawValue)
                    {
                        return (T)enumValue.GetValue(null);
                    }
                }
            }

            return null;
        }
    }
}
