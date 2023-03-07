/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Rest
{
    //Needs to be in sync with Bundle.HTTPVerbs
    public enum InteractionType
    {
        Search,
        Unspecified,
        Read,
        VRead,
        Update,
        Delete,
        Create,
        Capabilities,
        History,
        Operation,
        Transaction,
        Patch
    }
}
