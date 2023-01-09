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

using System;

#nullable enable

namespace Hl7.Fhir.Model
{
    public partial class Canonical : IEquatable<Canonical>
    {
        /// <summary>
        /// Constructs a Canonical based on a given <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri"></param>
        public Canonical(Uri uri) : this(uri?.OriginalString)
        {
            // nothing
        }

        /// <summary>
        /// Constructs a canonical from its components.
        /// </summary>
        public Canonical(string? uri, string? version, string? anchor)
        {
            Value = uri +
                (version is not null ? "|" + version : null) +
                (anchor is not null ? "#" + anchor : null);
        }

        /// <summary>
        /// Deconstructs the canonical into its uri and version.
        /// </summary>
        public void Deconstruct(out string? uri, out string? version, out string? anchor)
        {
            uri = Uri;
            version = Version;
            anchor = Anchor;
        }

        /// <summary>
        /// Converts a string to a canonical.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Canonical(string value) => new(value);

        /// <summary>
        /// Converts a canonical to a string.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string?(Canonical value) => value?.Value;

        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        public static bool IsValidValue(string value) => FhirUri.IsValidValue(value);

        public static readonly Uri FHIR_CORE_PROFILE_BASE_URI = new(@"http://hl7.org/fhir/StructureDefinition/");
        public static Canonical CanonicalUriForFhirCoreType(string typename) => new(FHIR_CORE_PROFILE_BASE_URI + typename);


        /// <summary>
        /// The version string of the canonical (if present).
        /// </summary>
        public string? Version
        {
            get
            {
                return splitCanonical(Value).version;
            }
        }

        /// <summary>
        /// Optional anchor at the end of the canonical, without the '#' prefix.
        /// </summary>
        public string? Anchor
        {
            get
            {
                return splitCanonical(Value).anchor;
            }
        }

        /// <summary>
        /// The uri part of the canonical, which is the canonical without the version indication.
        /// </summary>
        public string? Uri
        {
            get
            {
                return splitCanonical(Value).url;
            }
        }

        /// <summary>
        /// Converts the canonical to a <see cref="System.Uri" />.
        /// </summary>
        /// <returns></returns>
        public Uri ToUri() => new(Value, UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Whether the canonical is a relative or an absolute uri.
        /// </summary>
        public bool IsAbsolute => ToUri().IsAbsoluteUri;

        /// <summary>
        /// Whether the canonical has a version part.
        /// </summary>
        public bool HasVersion => Version is not null;

        /// <summary>
        /// Whether the canonical end with an anchor.
        /// </summary>
        public bool HasAnchor => Anchor is not null;

        private static (string? url, string? version, string? anchor) splitCanonical(string canonical)
        {
            var (rest, a) = splitOff(canonical, '#');
            var (u, v) = splitOff(rest, '|');

            return (u == String.Empty ? null : u, v, a);

            static (string, string?) splitOff(string url, char separator)
            {
                if (url.EndsWith(separator.ToString())) url = url.Substring(0, url.Length - 1);
                var position = url.LastIndexOf(separator);

                return position == -1 ?
                    (url, null)
                    : (url.Substring(0, position), url.Substring(position + 1));
            }
        }

        public bool Equals(Canonical? other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            else
                return Value.Equals(other.Value);
        }
        public static bool operator ==(Canonical obj1, Canonical obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Canonical obj1, Canonical obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object? obj) => Equals(obj as Canonical);


        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

#nullable restore