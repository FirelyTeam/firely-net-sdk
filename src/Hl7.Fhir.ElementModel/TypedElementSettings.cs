/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
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
        public TypeErrorMode ErrorMode;

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
        }

        /// <summary>Creates a new <see cref="TypedElementSettings"/> object that is a copy of the current instance.</summary>
        public TypedElementSettings Clone() => new TypedElementSettings(this);

        /// <summary>Creates a new <see cref="TypedElementSettings"/> instance with default property values.</summary>
        public static TypedElementSettings CreateDefault() => new TypedElementSettings();
    }
}