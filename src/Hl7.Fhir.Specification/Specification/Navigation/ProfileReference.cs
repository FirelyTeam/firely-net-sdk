/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160802] NEW

    /// <summary>Represents a reference to an element type profile.</summary>
    /// <remarks>Useful to parse complex profile references of the form "canonicalUrl#elementName".</remarks>
    internal struct ProfileReference
    {
        ProfileReference(string url)
        {
            if (url == null) { throw new ArgumentNullException("url"); }
            var pos = url.IndexOf('#');
            if (pos > 0 && pos < url.Length)
            {
                CanonicalUrl = url.Substring(0, pos);
                ElementName = url.Substring(pos + 1);
            }
            else
            {
                CanonicalUrl = url;
                ElementName = null;
            }
        }

        /// <summary>Initialize a new <see cref="ProfileReference"/> instance from the specified url.</summary>
        /// <param name="url">A resource reference to a profile.</param>
        /// <returns>A new <see cref="ProfileReference"/> structure.</returns>
        public static ProfileReference Parse(string url)  => new ProfileReference(url);

        /// <summary>Returns the canonical url of the profile.</summary>
        public string CanonicalUrl { get; }

        /// <summary>Returns an optional profile element name, if included in the reference.</summary>
        public string ElementName { get; }

        /// <summary>Returns <c>true</c> if the profile reference includes an element name, <c>false</c> otherwise.</summary>
        public bool IsComplex => ElementName != null;
    }
}
