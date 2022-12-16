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
    /// A <see cref="SerializationFilter"/> that passes the elements of the root resource
    /// Bundle untouched, but applies the filter in <see cref="ChildFilter"/> to all nested
    /// resources.
    /// </summary>
    /// <remarks>Used for creating summaries of search results, where the root Bundle is the transport
    /// and should not itself be summarized or filtered.</remarks>
    public class BundleFilter : SerializationFilter
    {
        /// <summary>
        /// Creates a new filter, given the filter for its nested resources (e.g. in Bundle.entry).
        /// </summary>
        /// <param name="childFilter"></param>
        public BundleFilter(SerializationFilter childFilter)
        {
            ChildFilter = childFilter ?? throw new ArgumentNullException(nameof(childFilter));
        }

        /// <summary>
        /// The filter applies to the nested resources in the Bundle.
        /// </summary>
        public SerializationFilter ChildFilter { get; }

        private string? _rootResourceType;
        private int _nestingDepth = -1;

        private bool inRootBundle => _nestingDepth == 0 && _rootResourceType == "Bundle";

        /// <inheritdoc />
        public override void EnterObject(object value, ClassMapping? mapping)
        {
            if (mapping?.IsResource == true)
            {
                if (_nestingDepth == -1) _rootResourceType = mapping.Name;
                _nestingDepth += 1;
            }

            if (!inRootBundle)
                ChildFilter.EnterObject(value, mapping);
        }

        /// <inheritdoc />
        public override bool TryEnterMember(string name, object value, PropertyMapping? mapping) =>
            inRootBundle || ChildFilter.TryEnterMember(name, value, mapping);

        /// <inheritdoc />
        public override void LeaveMember(string name, object value, PropertyMapping? mapping)
        {
            if (!inRootBundle) ChildFilter.LeaveMember(name, value, mapping);
        }

        /// <inheritdoc />
        public override void LeaveObject(object value, ClassMapping? mapping)
        {
            if (!inRootBundle)
                ChildFilter.LeaveObject(value, mapping);

            if (mapping?.IsResource == true) _nestingDepth -= 1;
        }
    }
}

#nullable restore
