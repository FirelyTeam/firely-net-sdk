using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Language
{
    public class TypeSpecifier : IEquatable<TypeSpecifier>
    {
        public const string SYSTEM_NAMESPACE = "System";
        public const string DOTNET_NAMESPACE = "DotNet";

        // From the Types section in Appendix B of the CQL reference
        public static readonly TypeSpecifier Any = new TypeSpecifier(SYSTEM_NAMESPACE, "Any");
        public static readonly TypeSpecifier Boolean = new TypeSpecifier(SYSTEM_NAMESPACE, "Boolean");
        public static readonly TypeSpecifier Code = new TypeSpecifier(SYSTEM_NAMESPACE, "Code");
        public static readonly TypeSpecifier Concept = new TypeSpecifier(SYSTEM_NAMESPACE, "Concept");
        public static readonly TypeSpecifier Date = new TypeSpecifier(SYSTEM_NAMESPACE, "Date");
        public static readonly TypeSpecifier DateTime = new TypeSpecifier(SYSTEM_NAMESPACE, "DateTime");
        public static readonly TypeSpecifier Decimal = new TypeSpecifier(SYSTEM_NAMESPACE, "Decimal");
        public static readonly TypeSpecifier Integer = new TypeSpecifier(SYSTEM_NAMESPACE, "Integer");
        public static readonly TypeSpecifier Long = new TypeSpecifier(SYSTEM_NAMESPACE, "Long");
        public static readonly TypeSpecifier Quantity = new TypeSpecifier(SYSTEM_NAMESPACE, "Quantity");
        public static readonly TypeSpecifier Ratio = new TypeSpecifier(SYSTEM_NAMESPACE, "Ratio");
        public static readonly TypeSpecifier String = new TypeSpecifier(SYSTEM_NAMESPACE, "String");
        public static readonly TypeSpecifier Time = new TypeSpecifier(SYSTEM_NAMESPACE, "Time");

        // This was added to represent the datatype with a single void element
        public static readonly TypeSpecifier Void = new TypeSpecifier(SYSTEM_NAMESPACE, "Void");

        public static readonly TypeSpecifier[] AllTypes = new[] { Any, Boolean, Code, Concept,
                Date, DateTime, Decimal, Integer, Long, Quantity, Ratio, String, Time };

        /// <summary>
        /// This is the list of supported types for the primitive values in ITypedElement.Value
        /// </summary>
        public static readonly TypeSpecifier[] PrimitiveTypes =
            new[] { Boolean, Code, Date, DateTime, Decimal, Integer, Long, String, Time };


        protected TypeSpecifier(string @namespace, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
        }

        public static TypeSpecifier GetByName(string typeName) => GetByName(SYSTEM_NAMESPACE, typeName);


        public static TypeSpecifier GetByName(string @namespace, string typeName)
        {
            if (@namespace == null) throw new ArgumentNullException(nameof(@namespace));
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));

            TypeSpecifier result = null;

            if (@namespace == SYSTEM_NAMESPACE)
                result = resolveSystemType(typeName);

            return result ?? new TypeSpecifier(@namespace, typeName);

            static TypeSpecifier resolveSystemType(string name)
            {
                return name switch
                {
                    "Any" => Any,
                    "Boolean" => Boolean,
                    "Code" => Code,
                    "Concept" => Concept,
                    "Date" => Date,
                    "DateTime" => DateTime,
                    "Decimal" => Decimal,
                    "Integer" => Integer,
                    "Long" => Long,
                    "Quantity" => Quantity,
                    "String" => String,
                    "Ratio" => Ratio,
                    "Time" => Time,
                    "Void" => Void,
                    _ => null,
                };
            }
        }

        public string Name { get; protected set; }
        public string Namespace { get; protected set; }

        public override string ToString() => FullName;

        public string FullName
        {
            get
            {
                return $"{esc(Namespace)}.{esc(Name)}";
                static string esc(string spec)
                {
                    if (!spec.Contains(".") && !spec.Contains("`")) return spec;

                    spec = spec.Replace("`", "\\`");
                    return $"`{spec}`";
                }
            }
        }

        /// <summary>
        /// Maps a C# type to a known TypeSpecifier.
        /// </summary>
        /// <param name="dotNetType">Value to determine the type for.</param>
        /// <returns></returns>
        public static TypeSpecifier ForNativeType(Type dotNetType)
        {
            if (dotNetType == null) throw new ArgumentNullException(nameof(dotNetType));

            // NOTE: Keep Any.TryConvertToSystemValue, TypeSpecifier.TryGetNativeType and TypeSpecifier.ForNativeType in sync
            if (t<bool>())
                return Boolean;
            else if (t<int>() || t<short>() || t<ushort>() || t<uint>())
                return Integer;
            else if (t<long>() || t<ulong>())
                return Long;
            else if (t<P.Time>())
                return Time;
            else if (t<P.Date>())
                return Date;
            else if (t<P.DateTime>() || t<DateTimeOffset>())
                return DateTime;
            else if (t<float>() || t<double>() || t<decimal>())
                return Decimal;
            else if (t<string>() || t<char>() || t<Uri>())
                return String;
            else if (t<P.Quantity>())
                return Quantity;
            else if (t<P.Ratio>())
                return Ratio;
            else if (t<P.Code>() || dotNetType.CanBeTreatedAsType(typeof(Enum)))
                return Code;
            else if (t<P.Concept>())
                return Concept;
#pragma warning disable IDE0046 // Convert to conditional expression
            else if (t<object>())
#pragma warning restore IDE0046 // Convert to conditional expression
                return Any;
            else
                return GetByName(DOTNET_NAMESPACE, dotNetType.ToString());

            bool t<A>() => dotNetType == typeof(A);

            //TypeSpecifier getDotNetNamespace()
            //{
            //    var tn = dotNetType.ToString();
            //    var pos = tn.LastIndexOf('.');

            //    if (pos == -1) return GetByName(DOTNET_NAMESPACE, dotNetType.ToString());

            //    var ns = tn.Substring(0, pos);
            //    var n = tn.Substring(pos + 1);
            //    return GetByName(DOTNET_NAMESPACE + "." + ns, n);
            //}
        }

        public override bool Equals(object obj) => Equals(obj as TypeSpecifier);
        public bool Equals(TypeSpecifier other) => other != null && Name == other.Name && Namespace == other.Namespace;

        public override int GetHashCode()
        {
            var hashCode = -179327946;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Namespace);
            return hashCode;
        }

        public static bool operator ==(TypeSpecifier left, TypeSpecifier right) => EqualityComparer<TypeSpecifier>.Default.Equals(left, right);
        public static bool operator !=(TypeSpecifier left, TypeSpecifier right) => !(left == right);
    }
}
