/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A filter that instructs the serializers which parts of a tree to serialize.
    /// </summary>
    public abstract class SerializationFilter
    {
        /// <summary>
        /// The serializer calls this function when it starts serializing a complex object.
        /// </summary>
        public abstract void EnterObject(object value, ClassMapping? mapping);

        /// <summary>
        /// The serializer calls this function when it needs to serialize the subtree contained in an element.
        /// When this function return false, the subtree will not be serialized.
        /// </summary>
        public abstract bool TryEnterMember(string name, object value, PropertyMapping? mapping);

        /// <summary>
        /// The serializer calls this function when it is done serializing the subtree for an element.
        /// </summary>
        public abstract void LeaveMember(string name, object value, PropertyMapping? mapping);

        /// <summary>
        /// The serializer calls this function when it is done serializing a complex object.
        /// </summary>
        public abstract void LeaveObject(object value, ClassMapping? mapping);

        /// <summary>
        /// Construct a new filter that conforms to the `_summary=true` summarized form.
        /// </summary>
        public static SerializationFilter ForSummary() => new BundleFilter(new ElementMetadataFilter() { IncludeInSummary = true });

        /// <summary>
        /// Construct a new filter that conforms to the `_summary=text` summarized form.
        /// </summary>
        public static SerializationFilter ForText() => new BundleFilter(new TopLevelFilter(
            new ElementMetadataFilter()
            {
                IncludeNames = new[] { "text", "id", "meta" },
                IncludeMandatory = true
            }));

        /// <summary>
        /// Construct a new filter that conforms to the `_summary=data` summarized form.
        /// </summary>
        public static SerializationFilter ForData() => new BundleFilter(new TopLevelFilter(
          new ElementMetadataFilter()
          {
              IncludeNames = new[] { "text" },
              Invert = true
          }));

        /// <summary>
        /// Construct a new filter that conforms to the `_elements=...` summarized form.
        /// </summary>
        public static SerializationFilter ForElements(string[] elements) => new BundleFilter(new TopLevelFilter(
          new ElementMetadataFilter()
          {
              IncludeNames = elements,
              IncludeMandatory = true
          }));
    }
}

#nullable restore
