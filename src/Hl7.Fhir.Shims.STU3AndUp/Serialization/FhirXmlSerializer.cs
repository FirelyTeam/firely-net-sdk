/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

public class FhirXmlSerializer() : BaseFhirXmlSerializer(ModelInfo.ModelInspector)
{
    public static readonly FhirXmlSerializer Default = new();
}