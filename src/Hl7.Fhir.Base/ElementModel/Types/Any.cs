/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.ElementModel.Types
{
    public abstract class Any
    {
        /// <summary>
        /// Returns the concrete subclass of Any that is used to represent the
        /// type given in parmameter <paramref name="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetSystemTypeByName(string name, out Type? result)
        {
            result = get();
            return result != null;

            Type? get() =>
                name switch
                {
                    "Any" => typeof(Any),
                    "Boolean" => typeof(Boolean),
                    "Code" => typeof(Code),
                    "Concept" => typeof(Concept),
                    "Decimal" => typeof(Decimal),
                    "Integer" => typeof(Integer),
                    "Long" => typeof(Long),
                    "Date" => typeof(Date),
                    "DateTime" => typeof(DateTime),
                    "Ratio" => typeof(Ratio),
                    "Time" => typeof(Time),
                    "Quantity" => typeof(Quantity),
                    "String" => typeof(String),
                    "Void" => typeof(void),
                    _ => null,
                };
        }

        public static object Parse(string value, Type primitiveType)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            return TryParse(value, primitiveType, out var result) ? result! :
                throw new FormatException($"Input string '{value}' was not in a correct format for type '{primitiveType}'.");
        }

        public static bool TryParse(string value, Type primitiveType, out object? parsed)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (!typeof(Any).IsAssignableFrom(primitiveType)) throw new ArgumentException($"Must be a subclass of {nameof(Any)}.", nameof(primitiveType));

            bool success;
            (success, parsed) = parse();
            return success;

            (bool success, object? p) parse()
            {
                if (primitiveType == typeof(Boolean))
                    return (Boolean.TryParse(value, out var p), p?.Value);
                else if (primitiveType == typeof(Code))
                    return (Code.TryParse(value, out var p), p);
                else if (primitiveType == typeof(Concept))
                    return (Concept.TryParse(value, out var p), p);
                else if (primitiveType == typeof(Decimal))
                    return (Decimal.TryParse(value, out var p), p?.Value);
                else if (primitiveType == typeof(Integer))
                    return (Integer.TryParse(value, out var p), p?.Value);
                else if (primitiveType == typeof(Long))
                    return (Long.TryParse(value, out var p), p?.Value);
                else if (primitiveType == typeof(Date))
                    return (Date.TryParse(value, out var p), p);
                else if (primitiveType == typeof(DateTime))
                    return (DateTime.TryParse(value, out var p), p);
                else if (primitiveType == typeof(Time))
                    return (Time.TryParse(value, out var p), p);
                else if (primitiveType == typeof(Ratio))
                    return (Ratio.TryParse(value, out var p), p);
                else if (primitiveType == typeof(Quantity))
                    return (Quantity.TryParse(value, out var p), p);
                else if (primitiveType == typeof(String))
                    return (String.TryParse(value, out var p), p?.Value);
                else
                    return (false, null);
            }
        }

        internal static (bool, T?) DoConvert<T>(Func<T> parser)
        {
            try
            {
                return (true, parser());
            }
            catch (Exception)
            {
                return (false, default);
            }
        }

        /// <summary>
        /// Try to convert a .NET instance to a Cql/FhirPath Any-based type.
        /// </summary>
        public static bool TryConvert(object value, out Any? primitiveValue)
        {
            primitiveValue = conv();
            return primitiveValue != null;

            Any? conv()
            {
                // NOTE: Keep Any.TryConvertToSystemValue, TypeSpecifier.TryGetNativeType and TypeSpecifier.ForNativeType in sync
                switch (value)
                {
                    case Any a:
                        return a;
                    case bool b:
                        return new Boolean(b);
                    case string s:
                        return new String(s);
                    case char c:
                        return new String(new string(c, 1));
                    case int _:
                    case short _:
                    case ushort _:
                    case uint _:
                        return new Integer(System.Convert.ToInt32(value));
                    case long _:
                    case ulong _:
                        return new Long(System.Convert.ToInt64(value));
                    case DateTimeOffset dto:
                        return DateTime.FromDateTimeOffset(dto);
                    case float _:
                    case double _:
                    case decimal _:
                        return new Decimal(System.Convert.ToDecimal(value));
                    case Enum en:
                        return new String(en.GetLiteral());
                    case Uri u:
                        return new String(u.OriginalString);
                    case byte[] bytes:
                        return new String(System.Convert.ToBase64String(bytes));
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Converts a .NET instance to a Cql/FhirPath Any-based type.
        /// </summary>
        public static Any? Convert(object value)
        {
            if (value == null) return null;

            if (TryConvert(value, out var result))
                return result;
            else
                throw new NotSupportedException($"There is no known Cql/FhirPath type corresponding to the .NET type {value.GetType().Name} of this instance (with value '{value}').");
        }

        // some utility methods shared by the subclasses

        protected static ArgumentException NotSameTypeComparison(object me, object? them) =>
            new ArgumentException($"Cannot compare {me} (of type {me.GetType()}) to {them} (of type {them?.GetType()}), because the types differ.");

        protected static readonly ArgumentNullException ArgNullException = new ArgumentNullException();

        protected static Result<T> CannotCastTo<T>(Any from) =>
            new Fail<T>(new InvalidCastException($"Cannot cast value '{from}' of type {from.GetType()} to an instance of type {typeof(T)}."));


        protected static Result<T> propagateNull<T>(object obj, Func<T> a) => obj is null ?
            new Fail<T>(ArgNullException) : new Ok<T>(a());
    }


    public interface ICqlEquatable
    {
        bool? IsEqualTo(Any other);
        bool IsEquivalentTo(Any other);
    }

    public interface ICqlOrderable
    {
        int? CompareTo(Any other);
    }

    public interface ICqlConvertible
    {
        Result<Boolean> TryConvertToBoolean();
        Result<Code> TryConvertToCode();
        Result<Concept> TryConvertToConcept();
        Result<Date> TryConvertToDate();
        Result<DateTime> TryConvertToDateTime();
        Result<Decimal> TryConvertToDecimal();
        Result<Integer> TryConvertToInteger();
        Result<Long> TryConvertToLong();
        Result<Quantity> TryConvertToQuantity();
        Result<Ratio> TryConvertToRatio();
        Result<String> TryConvertToString();
        Result<Time> TryConvertToTime();
    }
}
