/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace Hl7.Fhir.Utility
{
    public static class EnumUtility
    {
        public static string GetLiteral(this Enum e)
        {
            var attr = e.GetAttributeOnEnum<EnumLiteralAttribute>();
            return attr?.Literal ?? e.ToString();
        }

        public static string GetSystem(this Enum e)
        {
            var attr = e.GetAttributeOnEnum<EnumLiteralAttribute>();
            return attr?.System;
        }

        public static string GetDocumentation(this Enum e)
        {
            var attr = e.GetAttributeOnEnum<DescriptionAttribute>();
            return attr?.Description ?? e.ToString();
        }

        private static Dictionary<Type, EnumMapping> _cache = new Dictionary<Type, EnumMapping>();
        private static Object _cacheLock = new Object();

        public static object ParseLiteral(string rawValue, Type enumType, bool ignoreCase = false)
        {
            return GetEnumMapping(enumType).ParseLiteral(rawValue, ignoreCase);
        }

        public static T? ParseLiteral<T>(string rawValue, bool ignoreCase = false) where T : struct
        {
            return (T?)ParseLiteral(rawValue, typeof(T), ignoreCase);
        }

		public static string GetName( Type enumType )
		{
			return GetEnumMapping(enumType).Name;
		}

		public static string GetName<T>() where T : struct
		{
			return GetName(typeof(T));
		}

		private static EnumMapping GetEnumMapping( Type enumType )
		{
			EnumMapping fieldInfo = null;

			lock ( _cacheLock )
			{
				if ( !_cache.TryGetValue( enumType, out fieldInfo ) )
				{
					fieldInfo = EnumMapping.Create( enumType );
					_cache.Add( enumType, fieldInfo );
				}
			}

			return fieldInfo;
		}

		internal class EnumMapping
        {
            // Symbolic name of the enumeration
            public string Name { get; private set; }

            // .NET enumeration type
            public Type EnumType { get; private set; }

            private Dictionary<string, Enum> _literalToEnum = new Dictionary<string, Enum>();
            private Dictionary<string, Enum> _lowercaseLiteralToEnum = new Dictionary<string, Enum>();
            private Dictionary<Enum, string> _enumToLiteral = new Dictionary<Enum, string>();

            public string GetLiteral(Enum value)
            {
                _enumToLiteral.TryGetValue(value, out string result);
                return result;
            }

            public Enum ParseLiteral(string literal, bool ignoreCase)
            {
                Enum result;
                if (ignoreCase)
                {
                    _lowercaseLiteralToEnum.TryGetValue(literal.ToLowerInvariant(), out result);
                }
                else
                {
                    _literalToEnum.TryGetValue(literal, out result);
                }
                return result;
            }

            public bool ContainsLiteral(string literal) => _literalToEnum.ContainsKey(literal);

            public static EnumMapping Create(Type enumType)
            {
                if (enumType == null) throw Error.ArgumentNull("enumType");
                if (!enumType.IsEnum()) throw Error.Argument("enumType", "Type {0} is not an enumerated type".FormatWith(enumType.Name));

                var result = new EnumMapping()
                {
                    Name = getEnumName(enumType),
                    EnumType = enumType,
                    _enumToLiteral = new Dictionary<Enum, string>(),
                    _literalToEnum = new Dictionary<string, Enum>(),
                    _lowercaseLiteralToEnum = new Dictionary<string, Enum>()
                };

                foreach (var enumValue in ReflectionHelper.FindEnumFields(enumType))
                {
                    var attr = ReflectionHelper.GetAttribute<EnumLiteralAttribute>(enumValue);

                    string literal = enumValue.Name;
                    if (attr != null) literal = attr.Literal;

                    Enum value = (Enum)enumValue.GetValue(null);

                    result._enumToLiteral.Add(value, literal);
                    result._literalToEnum.Add(literal, value);
                    result._lowercaseLiteralToEnum.Add(literal.ToLowerInvariant(), value);
                }

                return result;
            }

            public static bool IsMappableEnum(Type t)
            {
                return t.IsEnum() &&  t.GetTypeInfo().GetCustomAttribute<FhirEnumerationAttribute>() != null;
            }


            private static string getEnumName(Type t)
            {
                var attr = t.GetTypeInfo().GetCustomAttribute<FhirEnumerationAttribute>();

                if (attr != null)
                    return attr.BindingName;
                else
                    return t.Name;
            }
        }
    }
}
