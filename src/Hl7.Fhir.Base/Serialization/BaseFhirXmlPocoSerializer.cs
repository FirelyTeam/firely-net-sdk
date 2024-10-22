/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Serializes the contents of an IReadOnlyDictionary[string,object] according to the rules of FHIR Xml serialization.
    /// </summary>
    /// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/xml.html. Since all POCOs included
    /// in the SDK implement IReadOnlyDictionary, these methods can be used to serialize POCOs to Xml.
    /// </remarks>
    public class BaseFhirXmlPocoSerializer
    {
        /// <summary>
        /// The release of FHIR for which this serializer is configured.
        /// </summary>
        public FhirRelease Release { get; }

        /// <summary>
        /// Construct a new serializer for a specific release of FHIR.
        /// </summary>
        public BaseFhirXmlPocoSerializer(FhirRelease release)
        {
            Release = release;
        }

        /// <summary>
        /// Serializes the given dictionary with FHIR data into Json.
        /// </summary>
        public void Serialize(IReadOnlyDictionary<string, object> members, XmlWriter writer, SerializationFilter? summary = default)
        {
            writer.WriteStartDocument();

            var simulateRoot = ((IScopedNode)members).Parent is not null || members is not Resource;
            if (simulateRoot)
            {
                // Serialization in XML of non-resources is problematic, since there's no root.
                // It's a common usecase though, so "invent" a root that's the name of the element's type.
                var rootElementName = members is Base b ? ((ITypedElement)b).Name : members.GetType().Name;
                writer.WriteStartElement(rootElementName, XmlNs.FHIR);
            }

            serializeInternal(members, writer, summary);

            if (simulateRoot) writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        /// <summary>
        /// Serializes the given dictionary with FHIR data into UTF8 encoded Json.
        /// </summary>
        public string SerializeToString(IReadOnlyDictionary<string, object> members, SerializationFilter? summary = default) =>
            SerializationUtil.WriteXmlToString(w => Serialize(members, w, summary));

        /// <summary>
        /// Serializes the given dictionary with FHIR data into Json, optionally skipping the "value" element.
        /// </summary>
        /// <remarks>Not serializing the "value" element is useful when serializing FHIR primitives into two properties, one
        /// with just the value, and one with the id/extensions.</remarks>
        private void serializeInternal(
            IReadOnlyDictionary<string, object> members,
            XmlWriter writer,
            SerializationFilter? filter)
        {
            if (members is Resource r)
                writer.WriteStartElement(r.TypeName, XmlNs.FHIR);

            // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
            if (!ClassMapping.TryGetMappingForType(members.GetType(), Release, out var mapping) && members is Base)
                throw new InvalidOperationException($"Encountered type {members.GetType()}, which is a support POCO for FHIR, but does not " +
                    $"have sufficient metadata to be used by the serializer.");

            filter?.EnterObject(members, mapping);

            serializeElement(members, writer, filter, mapping);

            filter?.LeaveObject(members, mapping);

            if (members is Resource) writer.WriteEndElement();
        }

        private void serializeElement(IReadOnlyDictionary<string, object> members, XmlWriter writer, SerializationFilter? filter, ClassMapping? mapping)
        {
            // Make sure that elements with attributes are serialized first.
            var orderedMembers = members
                .Select(m => (m, mapping: mapping?.FindMappedElementByName(m.Key)))
                .OrderBy(p => p.mapping?.SerializationHint != XmlRepresentation.XmlAttr);

            foreach (var ((mKey, mValue), propertyMapping) in orderedMembers)
            {
                if (filter?.TryEnterMember(mKey, mValue, propertyMapping) == false)
                    continue;

                var elementName = propertyMapping?.Choice == ChoiceType.DatatypeChoice ?
                            addSuffixToElementName(mKey, mValue) : mKey;

                if (mValue is ICollection coll and not byte[])
                {
                    foreach (var value in coll)
                        serializeMemberValue(elementName, value, writer, filter);
                }
                else
                    serializeMemberValue(elementName, mValue, writer, filter);

                filter?.LeaveMember(mKey, mValue, propertyMapping);
            }
        }

        private static string addSuffixToElementName(string elementName, object elementValue)
        {
            var typeName = elementValue switch
            {
                IEnumerable<Base> ib => ib.FirstOrDefault()?.TypeName,
                Base b => b.TypeName,
                _ => null
            };

            return typeName is null ? elementName : elementName + char.ToUpperInvariant(typeName[0]) + typeName.Substring(1);
        }


        private void serializeMemberValue(string elementName, object value, XmlWriter writer, SerializationFilter? filter)
        {
            if (value is XHtml xhtml)
            {
                writer.WriteRaw(xhtml.Value);
            }
            else if (value is IReadOnlyDictionary<string, object> complex)
            {
                writer.WriteStartElement(elementName, XmlNs.FHIR);
                serializeInternal(complex, writer, filter);
                writer.WriteEndElement();
            }
            else
                SerializePrimitiveValue(elementName, value, writer);
        }

        /// <summary>
        /// Serialize a primitive .NET value that may occur in the POCOs into XML.
        /// </summary>
        /// <remarks>      
        /// To allow for future additions to the POCOs the list of primitives supported here
        /// is larger than the set used by the current POCOs. Note that <c>DateTimeOffset</c>c> and 
        /// <c>byte[]</c> are considered to be "primitive" values here (used as the value in
        /// <see cref="Instant"/> and <see cref="Base64Binary"/>).
        /// </remarks>
        protected virtual void SerializePrimitiveValue(string elementName, object value, XmlWriter writer)
        {
            if (value is null) return;  // Don't write a null property

            var literal = value switch
            {
                int i32 => XmlConvert.ToString(i32),
                uint ui32 => XmlConvert.ToString(ui32),
                long i64 => XmlConvert.ToString(i64),
                ulong ui64 => XmlConvert.ToString(ui64),
                float si => XmlConvert.ToString(si),
                double dbl => XmlConvert.ToString(dbl),
                decimal dec => XmlConvert.ToString(dec),
                // A little note about trimming and whitespaces. The spec says:
                // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading
                // and trailing whitespace when reading attribute values (for XML schema conformance)"
                string s => s.Trim(),
                bool b => XmlConvert.ToString(b),
                DateTimeOffset dto => ElementModel.Types.DateTime.FormatDateTimeOffset(dto),
                byte[] bytes => Convert.ToBase64String(bytes),
                _ => throw new FormatException($"There is no known serialization for type {value.GetType()} into an Xml primitive property value.")
            };

            writer.WriteAttributeString(elementName, ns: null, value: literal);
        }
    }
}

#if NETSTANDARD

file static class KvpExtensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
    {
        key = kvp.Key;
        value = kvp.Value;
    }
}

#endif

#nullable restore