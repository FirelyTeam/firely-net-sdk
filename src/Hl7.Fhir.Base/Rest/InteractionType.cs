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
        Read,
        VRead, 
        Update, 
        ConditionalUpdate,
        Patch,
        ConditionalPatch,
        Delete,
        DeleteHistory,
        DeleteHistoryVersion,
        History,
        Create,
        ConditionalCreate,
        Search,
        ConditionalDeleteSingle,
        ConditionalDeleteMultiple,
        Capabilities,
        Transaction,
        Unspecified,
        Operation
    }
}
