/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
#nullable enable

namespace Hl7.Fhir.Serialization
{
    internal static class SystemTextJsonParsingExtensions
    {
        // While we are waiting for this https://github.com/dotnet/runtime/issues/28482,
        // there's no other option than to just force our way to these valuable properties.
        private static readonly Lazy<Func<JsonReaderState, long>> GETLINENUMBER =
            new(() => Utility.PropertyInfoExtensions.GetField<JsonReaderState, long>("_lineNumber"));
        private static readonly Lazy<Func<JsonReaderState, long>> GETPOSITION =
            new(() => Utility.PropertyInfoExtensions.GetField<JsonReaderState, long>("_bytePositionInLine"));

        internal static string GetRawText(this ref Utf8JsonReader reader)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            return doc.RootElement.GetRawText();
        }

        internal static (long lineNumber, long position) GetLocation(this JsonReaderState state)
        {
            // Note: linenumber/position are 0 based, so adding 1 position here.
            var lineNumber = GETLINENUMBER.Value(state) + 1;
            var position = GETPOSITION.Value(state) + 1;
            return (lineNumber, position);
        }

        internal static (long lineNumber, long position) GetLocation(this ref Utf8JsonReader reader) =>
            reader.CurrentState.GetLocation();

        internal static string GenerateLocationMessage(this ref Utf8JsonReader reader) =>
            GenerateLocationMessage(ref reader, out var _, out var _);

        internal static string GenerateLocationMessage(this ref Utf8JsonReader reader, out long lineNumber, out long position)
        {
            (lineNumber, position) = reader.GetLocation();
            return $"At line {lineNumber}, position {position}.";
        }


        public static bool TryGetNumber(this ref Utf8JsonReader reader, out object? value)
        {
            value = null;

            var gotValue = reader.TryGetDecimal(out var dec) && (value = dec) is { };
            if (!gotValue) gotValue = reader.TryGetUInt64(out var uint64) && (value = uint64) is { };
            if (!gotValue) gotValue = reader.TryGetInt64(out var int64) && (value = int64) is { };
            if (!gotValue) gotValue = reader.TryGetDouble(out var dbl) && dbl.IsNormal() && (value = dbl) is { };

            return gotValue;
        }

#if NETSTANDARD2_0
        internal static bool IsNormal(this float f) => !float.IsNaN(f) && !float.IsInfinity(f);
        internal static bool IsNormal(this double d) => !double.IsNaN(d) && !double.IsInfinity(d);
#else
        internal static bool IsNormal(this float f) => float.IsNormal(f);
        internal static bool IsNormal(this double d) => double.IsNormal(d);
#endif

        public static void Recover(this ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.None:
                    return;
                case JsonTokenType.Null:
                case JsonTokenType.Number or JsonTokenType.String:
                case JsonTokenType.True or JsonTokenType.False:
                    reader.Read();
                    return;
                case JsonTokenType.PropertyName:
                    SkipTo(ref reader, JsonTokenType.PropertyName);
                    return;
                case JsonTokenType.StartArray:
                    SkipTo(ref reader, JsonTokenType.EndArray);
                    reader.Read();
                    return;
                case JsonTokenType.StartObject:
                    SkipTo(ref reader, JsonTokenType.EndObject);
                    reader.Read();
                    return;
                default:
                    throw new InvalidOperationException($"Cannot recover, aborting. Token {reader.TokenType} was unexpected at this point. " +
                        reader.GenerateLocationMessage());
            }
        }

        public static void SkipTo(this ref Utf8JsonReader reader, JsonTokenType tt)
        {
            var depth = reader.CurrentDepth;

            while (reader.Read() && reader.CurrentDepth >= depth)
            {
                if (reader.CurrentDepth == depth && reader.TokenType == tt) break;
            }
        }
    }


}

#nullable restore
#endif