/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Rest
{
    public class EntryResponse
    {
        public string Status { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string ContentType { get; set; }
        public string Etag { get; set; }
        public byte[] Body { get; set; }
        public string Location { get; set; }
        public Uri ResponseUri { get; set; }

        public EntryResponse()
        {
            Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }


    public class TypedEntryResponse : EntryResponse
    {
        public ITypedElement TypedElement { get; set; }
    }

}
