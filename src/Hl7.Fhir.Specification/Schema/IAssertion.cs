/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Newtonsoft.Json.Linq;

internal interface IAssertion : IJsonSerializable
{
}

internal interface IJsonSerializable
{
    JToken ToJson();
}
