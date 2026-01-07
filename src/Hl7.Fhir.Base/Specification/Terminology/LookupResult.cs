/* 
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the CodeSystem/$lookup operation.
/// </summary>
public class LookupResult : Parameters
{
    public const string NAME_ATTRIBUTE = "name";
    public const string VERSION_ATTRIBUTE = "version";
    public const string DISPLAY_ATTRIBUTE = "display";
    public const string DESIGNATION_ATTRIBUTE = "designation";
    public const string PROPERTY_ATTRIBUTE = "property";

    public LookupResult()
    {
        // Nothing
    }

    public LookupResult(Parameters parameters) : base(parameters.Parameter)
    {
        // Nothing
    }

    /// <summary>
    /// A display name for the code system.
    /// </summary>
    public FhirString? Name => this.GetSingleValue<FhirString>(NAME_ATTRIBUTE);

    /// <summary>
    /// The version of the code system.
    /// </summary>
    public FhirString? Version => this.GetSingleValue<FhirString>(VERSION_ATTRIBUTE);

    /// <summary>
    /// The display value for the code.
    /// </summary>
    public FhirString? Display => this.GetSingleValue<FhirString>(DISPLAY_ATTRIBUTE);

    /// <summary>
    /// Additional designations for the concept.
    /// </summary>
    public IEnumerable<ParameterComponent> Designation
    {
        get => this.Get(DESIGNATION_ATTRIBUTE);
    }

    /// <summary>
    /// Properties of the concept.
    /// </summary>
    public IEnumerable<ParameterComponent> Property
    {
        get => this.Get(PROPERTY_ATTRIBUTE);
    }
}

