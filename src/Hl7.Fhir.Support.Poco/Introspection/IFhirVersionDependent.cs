/*
  Copyright (c) 2011-2013, HL7, Inc.
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

using Hl7.Fhir.Specification;
using System;

namespace Hl7.Fhir.Introspection
{
    public interface IFhirVersionDependent
    {
        /// <summary>
        /// First version of FHIR for which this attribute applies, as a major FHIR release number
        /// </summary>
        FhirRelease Since { get; }
    }

    public static class FhirVersionDependentExtensions
    {
        [Obsolete("Use Attribute.AppliesToRelease() instead.")]
        public static bool AppliesToVersion(this IFhirVersionDependent me, FhirRelease fhirVersion)
            => me.Since <= fhirVersion;

        /// <summary>
        /// Determines whether the given attribute applies to a given FHIR release.
        /// </summary>
        /// <remarks>An attribute is applicable to a given <see cref="FhirRelease"/> if
        /// the attribute has a <see cref="IFhirVersionDependent.Since"/> value that
        /// equivalent to or older than <paramref name="release"/> or has no <c>Since</c>
        /// value at all.</remarks>
        public static bool AppliesToRelease(this Attribute me, FhirRelease release) =>
             me is not IFhirVersionDependent vd || vd.Since <= release;


    }
}