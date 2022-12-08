/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
 
*/

using System;

#nullable enable

namespace Hl7.Fhir.Model
{
    public partial class Canonical
    {
        /// <summary>
        /// Constructs a Canonical based on a given <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri"></param>
        public Canonical(Uri uri) : this(uri?.OriginalString)
        {
            // nothing
        }

        /// <summary>
        /// Converts a string to a canonical.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Canonical(string value) => new(value);

        /// <summary>
        /// Converts a canonical to a string.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string?(Canonical value) => value?.Value;

        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        public static bool IsValidValue(string value) => FhirUri.IsValidValue(value);

        public static readonly Uri FHIR_CORE_PROFILE_BASE_URI = new(@"http://hl7.org/fhir/StructureDefinition/");
        public static Canonical CanonicalUriForFhirCoreType(string typename) => new(FHIR_CORE_PROFILE_BASE_URI + typename);
    }
}

#nullable restore