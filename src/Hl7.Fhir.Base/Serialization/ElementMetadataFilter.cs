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
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Filters elements of a resource so its serialization will only contain elements that are
    /// selected by this filter.
    /// </summary>
    public class ElementMetadataFilter : SerializationFilter
    {
        /// <summary>
        /// Inverts the logic of the filter: all elements are included, except those matching the filter.
        /// </summary>
        public bool Invert { get; set; }

        /// <summary>
        /// The list of top-level elements that the filter will include.
        /// </summary>
        public IReadOnlyCollection<string>? IncludeNames { get; set; }

        /// <summary>
        /// Include top-level mandatory elements, including all their children
        /// </summary>
        public bool IncludeMandatory { get; set; } // = false;

        /// <summary>
        /// Include all elements marked "in summary" in the definition of the element
        /// </summary>
        public bool IncludeInSummary { get; set; } // = false;

        /// <summary>
        /// Include all elements marked "is modifier" in the definition of the element
        /// </summary>
        public bool IncludeIsModifier { get; set; } // = false;

        /// <inheritdoc/>
        public override void EnterObject(object value, ClassMapping? mapping)
        {
            // nothing
        }

        /// <inheritdoc/>
        public override void LeaveMember(string name, object value, PropertyMapping? mapping)
        {
            // nothing
        }

        /// <inheritdoc/>
        public override void LeaveObject(object value, ClassMapping? mapping)
        {
            // nothing
        }

        /// <inheritdoc/>
        public override bool TryEnterMember(string name, object value, PropertyMapping? mapping)
        {
            if (IncludeIsModifier)
                throw new NotSupportedException("There is no metadata available to determine whether an element is a modifier (yet).");

            if (mapping is null) return true;

            var included = IncludeInSummary && mapping.InSummary ||
                IncludeMandatory && mapping.IsMandatoryElement ||
                IncludeNames?.Contains(mapping.Name) == true;

            return Invert ? !included : included;
        }
    }
}

#nullable restore
