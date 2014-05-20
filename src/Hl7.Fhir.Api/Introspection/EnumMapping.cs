/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    public class EnumMapping
    {
        // Symbolic name of the enumeration
        public string Name { get; private set; }

        // .NET enumeration type
        public Type EnumType { get; private set; }

        private Dictionary<string, Enum> _literalToEnum = new Dictionary<string, Enum>();
        private Dictionary<Enum, string> _enumToLiteral = new Dictionary<Enum, string>();

        public string GetLiteral(Enum value)
        {
            if (_enumToLiteral.ContainsKey(value))
                return _enumToLiteral[value];
            else
                return null;
        }

        public Enum ParseLiteral(string literal)
        {
            if (_literalToEnum.ContainsKey(literal))
                return _literalToEnum[literal];
            else
                return null;
        }

        public bool ContainsLiteral(string literal)
        {
            return _literalToEnum.ContainsKey(literal);
        }

        public static EnumMapping Create(Type enumType)
        {
            if (enumType == null) throw Error.ArgumentNull("enumType");
            if (!enumType.IsEnum()) throw Error.Argument("enumType", "Type {0} is not an enumerated type", enumType.Name);

            var result = new EnumMapping();

            result.Name = getEnumName(enumType);
            result.EnumType = enumType;
            result._enumToLiteral = new Dictionary<Enum, string>();
            result._literalToEnum = new Dictionary<string, Enum>();

            foreach(var enumValue in ReflectionHelper.FindEnumFields(enumType))
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
