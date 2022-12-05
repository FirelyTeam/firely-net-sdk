/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.ElementModel
{
    public class TypedElementSettings
    {
        /// <summary>
        /// Ways to handle missing type information for an element.
        /// </summary>
        public enum TypeErrorMode
        {
            /// <summary>
            /// Raise an error when no type information is available.
            /// </summary>
            Report,

            /// <summary>
            /// Ignore the element, it will not be represented in the tree.
            /// </summary>
            Ignore,

            /// <summary>
            /// The element will be represented in the tree, without type information.
            /// </summary>
            Passthrough
        }

        /// <summary>
        /// Determines how to proceed when an element is encountered for which there is no type information available.
        /// </summary>
        public TypeErrorMode ErrorMode { get; set; } // = TypeErrorMode.Report;

        /// <summary>
        /// Allow to parse a FHIR dateTime values into an element of type date.
        /// </summary>
        /// <remarks>
        /// Needed for backward compatibility with old parser for resources which were saved and considered valid in the past.
        /// </remarks>>
        [Obsolete("Needed for backward compatibility with old parser for resources which were saved and considered valid in the past. " +
            "Should not be used in new code.")]
        public bool TruncateDateTimeToDate { get; set; }

        /// <summary>Default constructor. Creates a new <see cref="TypedElementSettings"/> instance with default property values.</summary>
        public TypedElementSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="TypedElementSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public TypedElementSettings(TypedElementSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="TypedElementSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(TypedElementSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.ErrorMode = ErrorMode;
#pragma warning disable CS0618 // Type or member is obsolete
            other.TruncateDateTimeToDate = TruncateDateTimeToDate;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>Creates a new <see cref="TypedElementSettings"/> object that is a copy of the current instance.</summary>
        public TypedElementSettings Clone() => new TypedElementSettings(this);

        /// <summary>Creates a new <see cref="TypedElementSettings"/> instance with default property values.</summary>
        public static TypedElementSettings CreateDefault() => new TypedElementSettings();
    }
}