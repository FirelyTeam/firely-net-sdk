#nullable enable

/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest
{
    internal record EntryRequest(HTTPVerb? Method, string Url, InteractionType Type)
    {
        public string? ContentType { get; set; }
        public byte[]? RequestBodyContent { get; set; }
        public EntryRequestHeaders Headers { get; set; } = new();

        public string? FhirVersion { get; set; }

        public bool IsPostOrPut => Method is HTTPVerb.POST or HTTPVerb.PUT;        
    }

    internal class EntryRequestHeaders
    {
        public string? IfMatch { get; set; }
        public string? IfNoneMatch { get; set; }
        public string? IfNoneExist { get; set; }
        public DateTimeOffset? IfModifiedSince { get; set; }
        public string? Accept { get; set; }
    }

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

#nullable restore