using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath
{
    public class TypeInfo : IEquatable<TypeInfo>
    {
        public static readonly TypeInfo Boolean = new TypeInfo("boolean");
        public static readonly TypeInfo String = new TypeInfo("string");
        public static readonly TypeInfo Integer = new TypeInfo("integer");
        public static readonly TypeInfo Decimal = new TypeInfo("decimal");
        public static readonly TypeInfo DateTime = new TypeInfo("dateTime");
        public static readonly TypeInfo Time = new TypeInfo("time");
        public static readonly TypeInfo Any = new TypeInfo("any");
        public static readonly TypeInfo Void = new TypeInfo("void");

        private TypeInfo(string name)
        {
            Name = name;
        }

        public static TypeInfo ByName(string typeName)
        {
            switch (typeName)
            {
                case "boolean": return TypeInfo.Boolean;
                case "string": return TypeInfo.String;
                case "integer": return TypeInfo.Integer;
                case "decimal": return TypeInfo.Decimal;
                case "datetime": return TypeInfo.DateTime;
                case "time": return TypeInfo.Time;
                case "any": return TypeInfo.Any;
                case "void": return TypeInfo.Void;
                default:
                    var result = new TypeInfo(typeName);
                    return result;
            }
        }

        public string Name { get; protected set; }

        public override string ToString() => Name;

        public override bool Equals(object obj) => Equals(obj as TypeInfo);
        public bool Equals(TypeInfo other) => other != null &&
            Name == other.Name;

        public override int GetHashCode()
        {
            var hashCode = -568888154;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(TypeInfo left, TypeInfo right) => EqualityComparer<TypeInfo>.Default.Equals(left, right);

        public static bool operator !=(TypeInfo left, TypeInfo right) => !(left == right);
    } 
}