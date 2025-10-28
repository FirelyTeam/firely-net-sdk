/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

internal static class JsonDocumentExtensions
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