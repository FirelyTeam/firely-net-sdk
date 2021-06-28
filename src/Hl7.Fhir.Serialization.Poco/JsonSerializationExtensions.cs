using Hl7.Fhir.Model;
using System;
using System.Collections;
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
    public static class JsonSerializationExtensions
    {
        public static bool HasValue(object? value) =>
             value switch
             {
                 null => false,
                 string s => !string.IsNullOrWhiteSpace(s),
                 byte[] bs => bs.Length > 0,
                 _ => true
             };

        public static void SerializeObject(IEnumerable<KeyValuePair<string, object>> members, Utf8JsonWriter writer)
        {
            // ResourceType: from IDictionary?

            writer.WriteStartObject();

            foreach (var member in members)
            {
                if (member.Value is PrimitiveType pt)
                    SerializeFhirPrimitive(member.Key, pt, writer);
                else if (member.Value is IEnumerable<PrimitiveType> pts)
                    SerializeFhirPrimitiveList(member.Key, pts, writer);
                else
                {
                    writer.WritePropertyName(member.Key);

                    if (member.Value is ICollection coll && !(member.Value is byte[]))
                    {
                        writer.WriteStartArray(member.Key);

                        foreach (var value in coll)
                            serializeMemberValue(value, writer);

                        writer.WriteEndArray();
                    }
                    else
                        serializeMemberValue(member.Value, writer);
                }
            }

            writer.WriteEndObject();

        }

        private static void serializeMemberValue(object value, Utf8JsonWriter writer)
        {
            if (value is IEnumerable<KeyValuePair<string, object>> complex)
                SerializeObject(complex, writer);
            else
                SerializePrimitiveValue(value, writer);
        }

        // TODO: There's lots of HasValue() everywhere..... can we make the IDictionary implementation promise not to
        // return kvp's unless there is actually a value?  What if the IDictionary is constructed by hand?  Should
        // the IDictionary implementation on POCOs worry about the special cases (empty strings etc), and this serialize
        // be more generic - just not serializing nulls, but follow the IDictionary otherwise (even it that returns empty
        // strings?).
        public static void SerializeFhirPrimitiveList(string elementName, IEnumerable<PrimitiveType> values, Utf8JsonWriter writer)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            // Don't serialize empty collections.
            if (values?.Any() != true) return;

            // We should not write a "elementName" property until we encounter an actual
            // value. If we do, we should "catch up", by creating the property starting 
            // with a json array that contains 'null' for each of the elements we encountered
            // until now that did not have a value id/extensions.
            bool wroteStartArray = false;
            int numNullsMissed = 0;

            foreach (var value in values)
            {
                if (HasValue(value?.ObjectValue))
                {
                    if (!wroteStartArray)
                    {
                        wroteStartArray = true;
                        writeStartArray(elementName, numNullsMissed, writer);
                    }

                    SerializePrimitiveValue(value!.ObjectValue, writer);
                }
                else
                {
                    if (wroteStartArray)
                        writer.WriteNullValue();
                    else
                        numNullsMissed += 1;
                }
            }

            if (wroteStartArray) writer.WriteEndArray();

            // We should not write a "_elementName" property until we encounter an actual
            // id/extension. If we do, we should "catch up", by creating the property starting 
            // with a json array that contains 'null' for each of the elements we encountered
            // until now that did not have id/extensions etc.
            wroteStartArray = false;
            numNullsMissed = 0;

            foreach (var value in values)
            {
                var children = value?.Where(kvp => kvp.Key != "value").ToArray();

                if (children?.Any() == true)
                {
                    if (!wroteStartArray)
                    {
                        wroteStartArray = true;
                        writeStartArray("_" + elementName, numNullsMissed, writer);
                    }

                    writer.WriteStartObject();
                    SerializeObject(children, writer);
                    writer.WriteEndObject();
                }
                else
                {
                    if (wroteStartArray)
                        writer.WriteNullValue();
                    else
                        numNullsMissed += 1;
                }
            }

            if (wroteStartArray) writer.WriteEndArray();
        }

        private static void writeStartArray(string propName, int numNulls, Utf8JsonWriter writer)
        {
            writer.WriteStartArray(propName);

            for (int i = 0; i < numNulls; i++)
                writer.WriteNullValue();
        }


        public static void SerializeFhirPrimitive(string elementName, PrimitiveType value, Utf8JsonWriter writer)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            if (HasValue(value.ObjectValue))
            {
                // Write a property with 'elementName'
                writer.WritePropertyName(elementName);
                SerializePrimitiveValue(value.ObjectValue, writer);
            }

            // TODO: Should the POCO types explicitly or implicitly implement these interfaces?
            // TODO: Implicitly and then have a AsDictionary() and AsEnumerable()?
            // Calling ToList() here since SerializeObject will need to go over
            // all children anyway, and in .NET Core (at leats) ToArray is faster then ToList
            // See https://stackoverflow.com/a/60103725/2370163.
            var children = value.Where(kvp => kvp.Key != "value").ToArray();
            if (children.Any())
            {
                // Write a property with '_elementName'
                writer.WritePropertyName("_" + elementName);
                writer.WriteStartObject();
                SerializeObject(children, writer);
                writer.WriteEndObject();
            }
        }

        public static void SerializePrimitiveValue(object value, Utf8JsonWriter writer)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            switch (value)
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
                    throw new FormatException($"There is no know serialization for type {value.GetType()} into Json.");
            }
        }
    }
}

#nullable restore
