using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Support;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath.Expressions
{
    public class TypeInfo
    {
        public static readonly TypeInfo Bool = new TypeInfo("bool");
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
                case "bool": return TypeInfo.Bool;
                case "string": return TypeInfo.String;
                case "integer": return TypeInfo.Integer;
                case "decimal": return TypeInfo.Decimal;
                case "datetime": return TypeInfo.DateTime;
                case "time": return TypeInfo.Time;
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