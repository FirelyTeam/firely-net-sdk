/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// A structure that can represent a "normal" class mapping, but also a dynamic class mapping. If it is a dynamic class mapping,
/// we also store the dynamic type name as an override to just "DynamicType".
/// </summary>
internal record ClassMappingDynamic(ClassMapping Original, string? DynamicName)
{
    /// <summary>
    /// Create a new instance of the class represented by this mapping, setting the dynamic type name if applicable.
    /// </summary>
    public Base CreateInstance()
    {
        var result = (Base)Original.Factory();
        if(result is IDynamicType dt) dt.DynamicTypeName = DynamicName;

        return result;
    }
}