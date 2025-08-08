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
using System.Collections;

namespace Hl7.Fhir.Serialization;


/// <summary>
/// A structure that represent a property mapping + the classmapping of this property in the encountered instance.
/// </summary>
internal record PropertyValueMapping(PropertyMapping PropertyMapping, ClassMapping ValueMapping)
{
    public Base CreateInstance() => ValueMapping.CreateInstance();

    public IList CreateList() =>
        PropertyMapping.Inspector.FindClassMapping(PropertyMapping.ImplementingType)!.CreateList();
}