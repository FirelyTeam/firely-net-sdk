/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Terminology
{
    public enum ContextDirection
    {
        [EnumLiteral("incoming")]
        Incoming,
        [EnumLiteral("outgoing")]
        Outgoing,
    }
}
