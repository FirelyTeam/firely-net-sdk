using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath
{
    public class TypeInfo
    {
        public static readonly TypeInfo Boolean = new TypeInfo("boolean");
        public static readonly TypeInfo String = new TypeInfo("string");
        public static readonly TypeInfo Integer = new TypeInfo("integer");
        public static readonly TypeInfo Decimal = new TypeInfo("decimal");
        public static readonly TypeInfo DateTime = new TypeInfo("datetime");
        public static readonly TypeInfo Time = new TypeInfo("time");
        public static readonly TypeInfo Any = new TypeInfo("any");

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
                default:
                    var result = new TypeInfo(typeName);
                    result.IsBuiltin = true;
                    return result;
            }
        }

        public string Name { get; protected set; }

        public bool IsBuiltin { get; private set; }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            if (obj == null || GetType() != obj.GetType())
                return false;

            return Name == ((TypeInfo)obj).Name;
        }

        public static TypeInfo ForNativeType(Type nativeType)
        {
            if (nativeType == typeof(bool))
                return TypeInfo.Boolean;
            else if (nativeType == typeof(string))
                return TypeInfo.String;
            else if (nativeType == typeof(long))
                return TypeInfo.Integer;
            else if (nativeType == typeof(decimal))
                return TypeInfo.Decimal;
            else if (nativeType == typeof(PartialDateTime))
                return TypeInfo.DateTime;
            else if (nativeType == typeof(PartialTime))
                return TypeInfo.Time;
            else if (nativeType == typeof(IEnumerable<ITypedElement>))
                return TypeInfo.Any;
            else if (nativeType == typeof(ITypedElement))
                return TypeInfo.Any;
            else
                throw Error.Argument("nativeType", "Native type '{0}' is not mappable to a FhirPath type".FormatWith(nativeType.Name));
        }

        public bool MapsToNative(Type t)
        {
            if (this == TypeInfo.Boolean && t == typeof(bool))
                return true;
            else if (this == TypeInfo.String && t == typeof(string))
                return true;
            else if (this == TypeInfo.Integer && t == typeof(long))
                return true;
            else if (this == TypeInfo.Decimal && t == typeof(decimal))
                return true;
            else if (this == TypeInfo.DateTime && t == typeof(PartialDateTime))
                return true;
            else if (this == TypeInfo.Time && t == typeof(PartialTime))
                return true;
            else if (this == TypeInfo.Any && t == typeof(object))
                return true;
            else
                return false;
        }

        public bool Equals(TypeInfo typeRef)
        {
            if (Object.ReferenceEquals(this, typeRef))
                return true;

            if (typeRef == null)
                return false;

            return Name == typeRef.Name;
        }

        public override int GetHashCode()
       {
            return Name.GetHashCode();
        }

        public static bool operator ==(TypeInfo a, TypeInfo b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return a.Name == b.Name;
        }

        public static bool operator !=(TypeInfo a, TypeInfo b)
        {
            return !(a == b);
        }
    } 
}