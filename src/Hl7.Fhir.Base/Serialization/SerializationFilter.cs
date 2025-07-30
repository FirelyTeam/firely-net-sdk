/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using System;

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
        [Obsolete("Use CreateSummaryFactory() instead to ensure thread-safety when reusing JsonSerializerOptions instances. This method will be removed in a future version.")]
        public static SerializationFilter ForSummary() => CreateSummaryFactory()();

        /// <summary>
        /// Construct a new filter that conforms to the `_summary=text` summarized form.
        /// </summary>
        [Obsolete("Use CreateTextFactory() instead to ensure thread-safety when reusing JsonSerializerOptions instances. This method will be removed in a future version.")]
        public static SerializationFilter ForText() => CreateTextFactory()();

        [Obsolete("Use CreateCountFactory() instead to ensure thread-safety when reusing JsonSerializerOptions instances. This method will be removed in a future version.")]
        public static SerializationFilter ForCount() => CreateCountFactory()();

        /// <summary>
        /// Construct a new filter that conforms to the `_summary=data` summarized form.
        /// </summary>
        [Obsolete("Use CreateDataFactory() instead to ensure thread-safety when reusing JsonSerializerOptions instances. This method will be removed in a future version.")]
        public static SerializationFilter ForData() => CreateDataFactory()();

        /// <summary>
        /// Construct a new filter that conforms to the `_elements=...` summarized form.
        /// </summary>
        [Obsolete("Use CreateElementsFactory() instead to ensure thread-safety when reusing JsonSerializerOptions instances. This method will be removed in a future version.")]
        public static SerializationFilter ForElements(string[] elements) => CreateElementsFactory(elements)();

        /// <summary>
        /// Create a factory function that produces new filter instances conforming to the `_summary=true` summarized form.
        /// Using this factory ensures thread-safety when reusing JsonSerializerOptions instances.
        /// </summary>
        public static Func<SerializationFilter> CreateSummaryFactory()
        {
            return () => new BundleFilter(new ElementMetadataFilter() { IncludeInSummary = true });
        }

        /// <summary>
        /// Create a factory function that produces new filter instances conforming to the `_summary=text` summarized form.
        /// Using this factory ensures thread-safety when reusing JsonSerializerOptions instances.
        /// </summary>
        public static Func<SerializationFilter> CreateTextFactory()
        {
            return () => new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter()
                {
                    IncludeNames = new[] { "text", "id", "meta" },
                    IncludeMandatory = true
                }));
        }

        /// <summary>
        /// Create a factory function that produces new filter instances conforming to the `_summary=count` summarized form.
        /// Using this factory ensures thread-safety when reusing JsonSerializerOptions instances.
        /// </summary>
        public static Func<SerializationFilter> CreateCountFactory()
        {
            return () => new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter()
                {
                    IncludeMandatory = true,
                    IncludeNames = new[] { "id", "total", "link" }
                }));
        }

        /// <summary>
        /// Create a factory function that produces new filter instances conforming to the `_summary=data` summarized form.
        /// Using this factory ensures thread-safety when reusing JsonSerializerOptions instances.
        /// </summary>
        public static Func<SerializationFilter> CreateDataFactory()
        {
            return () => new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter()
                {
                    IncludeNames = new[] { "text" },
                    Invert = true
                }));
        }

        /// <summary>
        /// Create a factory function that produces new filter instances conforming to the `_elements=...` summarized form.
        /// Using this factory ensures thread-safety when reusing JsonSerializerOptions instances.
        /// </summary>
        public static Func<SerializationFilter> CreateElementsFactory(string[] elements)
        {
            return () => new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter()
                {
                    IncludeNames = elements,
                    IncludeMandatory = true
                }));
        }
    }
}

#nullable restore
