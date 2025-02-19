/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class FhirJsonBuilderExtensions
{
    internal static void writeTo(this JObject root, JsonWriter destination)
    {
        root.WriteTo(destination);
        destination.Flush();
    }

    internal static async Task writeToAsync(this JObject root, JsonWriter destination)
    {
        await root.WriteToAsync(destination).ConfigureAwait(false);
        await destination.FlushAsync().ConfigureAwait(false);
    }
}