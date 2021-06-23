using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

#nullable enable

/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

namespace Hl7.Fhir.Serialization.Poco
{
    /// <summary>
    /// Common utilties for JSON Stream functionality.
    /// </summary>
    public static class JsonStreamUtilities
    {
        public static bool HasValue(object value) =>
            value switch
            {
                null => false,
                string s => !string.IsNullOrWhiteSpace(s),
                _ => true
            };

        // Note: the current generated parsers miss the HasValue(v.ElementId)
        public static bool HasElements(PrimitiveType primitive) =>
            primitive is not null && (primitive.HasExtensions() || HasValue(primitive.ElementId));


        public static void SerializeComplexProperty(string propName, IEnumerable<Base> elements, Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            if (elements?.Any() == true)
            {
                writer.WritePropertyName(propName);
                writer.WriteStartArray();

                foreach (Base element in elements)
                {
                    SerializeJsonResourceDispatcher.DispatchSerializeJson(writer, element, options);
                }

                writer.WriteEndArray();
            }
        }


        public static void SerializeComplexProperty(string propName, Base element, Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            if (element is not null)
            {
                writer.WritePropertyName(propName);
                SerializeJsonResourceDispatcher.DispatchSerializeJson(writer, element, options);
            }
        }

        public static void SerializePrimitiveProperty(string propName, IEnumerable<PrimitiveType> values, Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            if (values?.Any() == true)
            {
                // Determine whether we need both 'elementName' and '_elementName'.
                bool hasValues = values.Any(v => HasValue(v));
                bool hasElements = values.Any(v => HasElements(v));

                if (hasValues)
                {
                    writer.WritePropertyName(propName);
                    writer.WriteStartArray();

                    foreach (var value in values)
                    {
                        if (HasValue(value.ObjectValue))
                        {
                            switch (value.ObjectValue)
                            {
                                //TODO: Include support for Any subclasses (CQL types)?
                                //TODO: precision loss in WriteNumber/ParseNumber (http://hl7.org/fhir/json.html#primitive)?
                                case int i32: writer.WriteNumberValue(i32); break;
                                case uint ui32: writer.WriteNumberValue(ui32); break;
                                case long i64: writer.WriteNumberValue(i64); break;
                                case ulong ui64: writer.WriteNumberValue(ui64); break;
                                case float si: writer.WriteNumberValue(si); break;
                                case double dbl: writer.WriteNumberValue(dbl); break;
                                case decimal dec: writer.WriteNumberValue(dec); break;
                                // So, no trim here. string-based types (like code) should make sure their values are valid,
                                // so do not have trailing spaces. Not something the serializer should worry about? And strings
                                // are allowed to have trailing spaces:
                                // "According to XML schema, leading and trailing whitespace in the value attribute is ignored for the
                                // types boolean, integer, decimal, base64Binary, instant, uri, date, dateTime, oid, and uri. Note that
                                // this means that the schema aware XML libraries give different attribute values to non-schema aware libraries
                                // when reading the XML instances. For this reason, the value attribute for these types SHOULD not have leading
                                // and trailing spaces. String values should only have leading and trailing spaces if they are part of the content
                                // of the value. In JSON and Turtle whitespace in string values is always significant. Primitive types other than
                                // string SHALL NOT have leading or trailing whitespace."
                                case string s: writer.WriteStringValue(s); break;
                                case bool b: writer.WriteBooleanValue(b); break;
                                //TODO: only two types that do not support 100% roundtrippability. Currently necessary for Instant.cs
                                // change Instant to have an ObjectValue of type string?  But then again, why is 'bool' good enough for Boolean,
                                // wouldn't we need to be able to store "treu" or "flase" for real roundtrippability?
                                case DateTimeOffset dto: writer.WriteStringValue(ElementModel.Types.DateTime.FormatDateTimeOffset(dto)); break;
                                case byte[] bytes: writer.WriteStringValue(Convert.ToBase64String(bytes)); break;
                                default:
                                    // TODO: Or just use ToString() ?
                                    throw new FormatException($"There is no know serialization for type {value.ObjectValue.GetType()} into Json.");
                            }
                        }
                        else
                        {
                            writer.WriteNullValue();
                        }
                    }

                    writer.WriteEndArray();
                }

                if (hasElements)
                {
                    writer.WritePropertyName("_" + propName);
                    writer.WriteStartArray();

                    foreach (var value in values)
                    {
                        if (HasElements(value))
                            ((Element)value).SerializeJson(writer, options);
                        else
                            writer.WriteNullValue();
                    }

                    writer.WriteEndArray();
                }
            }
        }

        // TODO: could we rewrite this as simply a JsonConverter<PrimitiveType> ?
        public static void SerializePrimitiveProperty(string propName, PrimitiveType value, Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                if (HasValue(value.ObjectValue))
                {
                    switch (value.ObjectValue)
                    {
                        //TODO: Include support for Any subclasses (CQL types)?
                        //TODO: precision loss in WriteNumber/ParseNumber (http://hl7.org/fhir/json.html#primitive)?
                        case int i32: writer.WriteNumber(propName, i32); break;
                        case uint ui32: writer.WriteNumber(propName, ui32); break;
                        case long i64: writer.WriteNumber(propName, i64); break;
                        case ulong ui64: writer.WriteNumber(propName, ui64); break;
                        case float si: writer.WriteNumber(propName, si); break;
                        case double dbl: writer.WriteNumber(propName, dbl); break;
                        case decimal dec: writer.WriteNumber(propName, dec); break;
                        // So, no trim here. string-based types (like code) should make sure their values are valid,
                        // so do not have trailing spaces. Not something the serializer should worry about? And strings
                        // are allowed to have trailing spaces:
                        // "According to XML schema, leading and trailing whitespace in the value attribute is ignored for the
                        // types boolean, integer, decimal, base64Binary, instant, uri, date, dateTime, oid, and uri. Note that
                        // this means that the schema aware XML libraries give different attribute values to non-schema aware libraries
                        // when reading the XML instances. For this reason, the value attribute for these types SHOULD not have leading
                        // and trailing spaces. String values should only have leading and trailing spaces if they are part of the content
                        // of the value. In JSON and Turtle whitespace in string values is always significant. Primitive types other than
                        // string SHALL NOT have leading or trailing whitespace."
                        case string s: writer.WriteString(propName, s); break;
                        case bool b: writer.WriteBoolean(propName, b); break;
                        //TODO: only two types that do not support 100% roundtrippability. Currently necessary for Instant.cs
                        // change Instant to have an ObjectValue of type string?  But then again, why is 'bool' good enough for Boolean,
                        // wouldn't we need to be able to store "treu" or "flase" for real roundtrippability?
                        case DateTimeOffset dto: writer.WriteString(propName, ElementModel.Types.DateTime.FormatDateTimeOffset(dto)); break;
                        case byte[] bytes: writer.WriteString(propName, Convert.ToBase64String(bytes)); break;
                        default:
                            // TODO: Or just use ToString() ?
                            throw new FormatException($"There is no know serialization for type {value.ObjectValue.GetType()} into Json.");
                    }
                }

                if (HasElements(value))
                {
                    writer.WritePropertyName("_" + propName);
                    ((Element)value).SerializeJson(writer, options);
                }
            }
        }
    }
}

#nullable restore
