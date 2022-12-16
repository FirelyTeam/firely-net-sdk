/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160802] NEW

    /// <summary>Represents a reference to an element type profile.</summary>
    /// <remarks>Useful to parse complex profile references of the form "canonicalUrl#Type.elementName".</remarks>
    internal class ProfileReference
    {
        private ProfileReference(string url)
        {
            if (url == null) { throw new ArgumentNullException(nameof(url)); }

            var parts = url.Split('#');

            if (parts.Length == 1)
            {
                // Just the canonical, no '#' present
                CanonicalUrl = parts[0];
                ElementName = null;
            }
            else
            {
                // There's a '#', so both or just the element are present
                CanonicalUrl = parts[0].Length > 0 ? parts[0] : null;
                ElementName = parts[1].Length > 0 ? parts[1] : null;
            }
        }

        /// <summary>Initialize a new <see cref="ProfileReference"/> instance from the specified url.</summary>
        /// <param name="url">A resource reference to a profile.</param>
        /// <returns>A new <see cref="ProfileReference"/> structure.</returns>
        public static ProfileReference Parse(string url) => new(url);

        /// <summary>Returns the canonical url of the profile.</summary>
        public string? CanonicalUrl { get; }

        /// <summary>Returns an optional profile element name, if included in the reference.</summary>
        public string? ElementName { get; }

        /// <summary>Returns <c>true</c> if the profile reference includes an element name, <c>false</c> otherwise.</summary>
        public bool IsComplex => ElementName is not null;

        /// <summary>
        /// Returns <c>true</c> of the profile reference includes a canonical url.
        /// </summary>
        public bool IsAbsolute => CanonicalUrl is not null;
    }
}
