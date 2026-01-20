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
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Typed result utility class for the CodeSystem/$lookup operation.
/// </summary>
public class LookupResult : Parameters
{
    public const string NAME_ATTRIBUTE = "name";
    public const string VERSION_ATTRIBUTE = "version";
    public const string DISPLAY_ATTRIBUTE = "display";
    public const string DEFINITION_ATTRIBUTE = "definition";
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
    /// A statement of the meaning of concept from the code system
    /// </summary>
    public FhirString? Definition => this.GetSingleValue<FhirString>(DEFINITION_ATTRIBUTE);
    
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

    public static LookupResult ForResult(string name, string display, string? version = null, string? definition = null, IEnumerable<ParameterComponent>? properties = null)
    {
        var result = new LookupResult();

        result.Add(NAME_ATTRIBUTE, new FhirString(name));
        
        if(version is not null)
            result.Add(VERSION_ATTRIBUTE, new FhirString(version));
        
        result.Add(DISPLAY_ATTRIBUTE, new FhirString(display));
        
        if(definition is not null)
            result.Add(DEFINITION_ATTRIBUTE, new FhirString(definition));
        
        if(properties?.ToList() is { Count: >0 } props)
            result.SetMultipleValues(PROPERTY_ATTRIBUTE, props);
        
        return result;
    }
}

