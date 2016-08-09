using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Hl7.Fhir.Introspection
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


        private static Dictionary<Type, EnumMapping> _cache = new Dictionary<Type, EnumMapping>();
        private static Object _cacheLock = new Object();


        public static object ParseLiteral(string rawValue, Type enumType)
        {
            EnumMapping fieldInfo = null;

            lock (_cacheLock)
            {
                if (!_cache.TryGetValue(enumType, out fieldInfo))
                {
                    fieldInfo = EnumMapping.Create(enumType);
                    _cache.Add(enumType, fieldInfo);
                }
            }

            return (object)fieldInfo.ParseLiteral(rawValue);
            
            //foreach (var enumValue in fieldInfo)
            //{
            //    var attr = ReflectionHelper.GetAttribute<EnumLiteralAttribute>(enumValue);
            //    if (attr != null)
            //    {
            //        if (attr.Literal == rawValue)
            //        {
            //            return (T)enumValue.GetValue(null);
            //        }
            //    }
            //}

            //return null;
        }

        public static T? ParseLiteral<T>(string rawValue) where T : struct
        {
            return (T?)ParseLiteral(rawValue, typeof(T));
        }

        internal class EnumMapping
        {
            // Symbolic name of the enumeration
            public string Name { get; private set; }

            // .NET enumeration type
            public Type EnumType { get; private set; }

            private Dictionary<string, Enum> _literalToEnum = new Dictionary<string, Enum>();
            private Dictionary<Enum, string> _enumToLiteral = new Dictionary<Enum, string>();

            public string GetLiteral(Enum value)
            {
                // [WMR 20160421] Optimization
                //if (_enumToLiteral.ContainsKey(value))
                //    return _enumToLiteral[value];
                //else
                //    return null;
                string result;
                _enumToLiteral.TryGetValue(value, out result);
                return result;
            }

            public Enum ParseLiteral(string literal)
            {
                // [WMR 20160421] Optimization
                //if (_literalToEnum.ContainsKey(literal))
                //    return _literalToEnum[literal];
                //else
                //    return null;
                Enum result;
                _literalToEnum.TryGetValue(literal, out result);
                return result;
            }

            public bool ContainsLiteral(string literal)
            {
                return _literalToEnum.ContainsKey(literal);
            }

            public static EnumMapping Create(Type enumType)
            {
                if (enumType == null) throw Error.ArgumentNull("enumType");
                if (!enumType.IsEnum()) throw Error.Argument("enumType", "Type {0} is not an enumerated type".FormatWith(enumType.Name));

                var result = new EnumMapping();

                result.Name = getEnumName(enumType);
                result.EnumType = enumType;
                result._enumToLiteral = new Dictionary<Enum, string>();
                result._literalToEnum = new Dictionary<string, Enum>();

                foreach (var enumValue in ReflectionHelper.FindEnumFields(enumType))
                {
                    var attr = ReflectionHelper.GetAttribute<EnumLiteralAttribute>(enumValue);

                    string literal = enumValue.Name;
                    if (attr != null) literal = attr.Literal;

                    Enum value = (Enum)enumValue.GetValue(null);

                    result._enumToLiteral.Add(value, literal);
                    result._literalToEnum.Add(literal, value);
                }

                return result;
            }

            public static bool IsMappableEnum(Type t)
            {
                return t.IsEnum() && ReflectionHelper.GetAttribute<FhirEnumerationAttribute>(t) != null;
            }


            private static string getEnumName(Type t)
            {
                var attr = ReflectionHelper.GetAttribute<FhirEnumerationAttribute>(t);

                if (attr != null)
                    return attr.BindingName;
                else
                    return t.Name;
            }
        }

    }
}
