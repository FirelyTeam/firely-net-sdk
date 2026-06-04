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

using P = Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Model;

public partial class Canonical
{
    /// <summary>
    /// The base uri for FHIR core profiles, which is "http://hl7.org/fhir/StructureDefinition/".
    /// </summary>
    public static readonly Uri FHIR_CORE_PROFILE_BASE_URI = new(ResourceIdentity.CORE_BASE_URL);

    /// <summary>
    /// Constructs a Canonical based on a given <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri"></param>
    public Canonical(Uri uri) : this(uri.OriginalString)
    {
        // nothing
    }

    /// <summary>
    /// Constructs a canonical from its components.
    /// </summary>
    public Canonical(string? uri, string? version, string? fragment = null)
    {
        if ((uri is not null) && uri.IndexOfAny(['|', '#']) != -1)
            throw Error.Argument(nameof(uri), "cannot contain version/fragment data");

        if ((version is not null) && version.IndexOfAny(['|', '#']) != -1)
            throw Error.Argument(nameof(version), "cannot contain version/fragment data");

        if ((fragment is not null) && fragment.IndexOfAny(['|', '#']) != -1)
            throw Error.Argument(nameof(fragment), "already contains version/fragment data");

        Value = uri +
                (version is not null ? "|" + version : null) +
                (fragment is not null ? "#" + fragment : null);
    }

    /// <summary>
    /// Deconstructs the canonical into its uri and version.
    /// </summary>
    public void Deconstruct(out string? uri, out string? version, out string? fragment)
    {
        uri = Uri;
        version = Version;
        fragment = Fragment;
    }

    /// <summary>
    /// Converts a string to a canonical.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Canonical(string? value) => new(value);

    /// <summary>
    /// Converts a canonical to a string.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator string?(Canonical? value) => value?.Value;

    /// <summary>
    /// Validates the JsonValue.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context) =>
        JsonValue switch
        {
            null => null,
            string unparsed when IsValidValue(unparsed) => null,
            string unparsed => COVE.LITERAL_INVALID(context, unparsed, this.TypeName),
            _ => COVE.INCORRECT_LITERAL_VALUE_TYPE(context, JsonValue, this.TypeName)
        };


    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => FhirUri.IsValidValue(value);

    /// <summary>
    /// Constructs a Canonical to represent a FHIR core type given by name.
    /// </summary>
    /// <remarks>If the typename is an absolute url, this function assumes the
    /// typename is already fully qualified and will return a canonical with that value.
    /// </remarks>
    public static Canonical ForCoreType(string typename)
    {
        var typeNameUri = new Canonical(typename);
        return typeNameUri.IsAbsolute ? typeNameUri : ResourceIdentity.Core(typename).OriginalString;
    }

    /// <summary>
    /// The version string of the canonical (if present).
    /// </summary>
    public string? Version => Value is null ? null : splitCanonical(Value).version;

    /// <summary>
    /// Optional anchor at the end of the canonical, without the '#' prefix.
    /// </summary>
    public string? Fragment => Value is null ? null : splitCanonical(Value).fragment;

    /// <summary>
    /// The uri part of the canonical, which is the canonical without the version indication.
    /// </summary>
    public string? Uri => Value is null ? null : splitCanonical(Value).url;

    /// <summary>
    /// Converts the canonical to a <see cref="System.Uri" />.
    /// </summary>
    /// <returns></returns>
    public Uri ToUri() => new(
        Value ?? throw new InvalidOperationException("Cannot turn a canonical without a value into a Uri"),
        UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Whether the canonical is a relative or an absolute uri.
    /// </summary>
    public bool IsAbsolute => Uri is not null && new Uri(this.Uri, UriKind.RelativeOrAbsolute).IsAbsoluteUri;
    /// <summary>
    /// Whether the canonical has a version part.
    /// </summary>
    public bool HasVersion => Version is not null;

    /// <summary>
    /// Whether the canonical end with an anchor.
    /// </summary>
    public bool HasAnchor => Fragment is not null;


	/// <summary>
    /// Determines if a resource version matches a query version according to FHIR canonical matching rules.
    /// Supports both exact matching and partial version matching (e.g., "1.5" matches "1.5.0").
    /// </summary>
    /// <param name="resourceVersion">The version of the resource being checked.</param>
    /// <param name="queryVersion">The version specified in the canonical URL query.</param>
    /// <returns>True if the resource version matches the query version according to FHIR canonical matching rules.</returns>
    public static bool MatchesVersion(string? resourceVersion, string queryVersion)
    {
        // If either version is null or empty, treat as no version specified
        if (string.IsNullOrEmpty(resourceVersion) || string.IsNullOrEmpty(queryVersion))
            return string.IsNullOrEmpty(resourceVersion) && string.IsNullOrEmpty(queryVersion);

        // First try exact match for backwards compatibility and performance
        if (resourceVersion == queryVersion)
            return true;

        // Implement partial version matching according to FHIR canonical matching rules
        // The query version should be a prefix of the resource version when split by dots
        var resourceParts = resourceVersion!.Split('.');
        var queryParts = queryVersion.Split('.');

        // Query version cannot have more parts than resource version for partial matching
        if (queryParts.Length > resourceParts.Length)
            return false;

        // Check if all query version parts match the corresponding resource version parts
        for (int i = 0; i < queryParts.Length; i++)
        {
            if (resourceParts[i] != queryParts[i])
                return false;
        }

        return true;
    }


    private static (string? url, string? version, string? fragment) splitCanonical(string canonical)
    {
        var (rest, a) = splitOff(canonical, '#');
        var (u, v) = splitOff(rest, '|');

        return (u == String.Empty ? null : u, v, a);

        static (string, string?) splitOff(string url, char separator)
        {
            if (url.EndsWith(separator.ToString())) url = url[..^1];
            var position = url.LastIndexOf(separator);

            return position == -1
                ? (url, null)
                : (url[..position], url[(position + 1)..]);
        }
    }

    /// <summary>
    /// Converts this canonical to a <see cref="P.String" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value of this canonical is null,
    /// which is not valid for System strings.</exception>
    public P.String ToSystemString() => (P.String?)TryConvertToSystemTypeInternal() ??
                                        throw new InvalidOperationException("Value is null.");

    protected internal override P.Any? TryConvertToSystemTypeInternal() =>
        Value is not null ? new P.String(Value) : null;
}