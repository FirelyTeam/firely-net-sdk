/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using System.Text.RegularExpressions;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Checks if codes are valid language types (urn:ietf:bcp:47)
/// </summary>
public class LanguageTerminologyService()
    : CustomValueSetTerminologyService("language", LANGUAGE_SYSTEM, [LANGUAGE_VALUESET])
{
    private const string LANGUAGE_SYSTEM = "urn:ietf:bcp:47";
    public const string LANGUAGE_VALUESET = "http://hl7.org/fhir/ValueSet/all-languages";

    /// <summary>
    /// Validates if the code is a valid language code.
    /// </summary>
    /// <remarks>
    /// Matches either two lowercase letters OR 2 lowercase letters followed by a dash and two uppercase letters.
    /// </remarks>
    protected override bool ValidateCodeType(string code)
    {
        var regex = new Regex("^[a-z]{2}(-[A-Z]{2})?$");
        return regex.IsMatch(code);
    }
}

