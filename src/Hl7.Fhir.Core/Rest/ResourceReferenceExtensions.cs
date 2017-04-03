/*
  Copyright (c) 2011-2012, HL7, Inc
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

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class ResourceReferenceExtensions
    {
        /// <summary>
        /// When a ResourceReference is relative, use the parent resource's fullUrl (e.g. from a Bundle's entry)
        /// to make it absolute.
        /// </summary>
        /// <param name="reference">The ResourceReference to get the (possibily relative) url from</param>
        /// <param name="parentResourceUri">Absolute uri representing the location of the resource this reference is in.</param>
        /// <remarks>Implements (part of the logic) as described in bundle.html#6.7.4.1</remarks>
        /// <returns></returns>
        public static Uri GetAbsoluteUriForReference(this ResourceReference reference, Uri parentResourceUri)
        {
            if (parentResourceUri == null) throw Error.ArgumentNull(nameof(parentResourceUri));
            if (reference == null) throw Error.ArgumentNull(nameof(reference));
            if (reference.Reference == null) return null;

            // Don't need to do anything when Uri is absolute
            var referenceUri = new Uri(reference.Reference, UriKind.RelativeOrAbsolute);
            if (referenceUri.IsAbsoluteUri) return referenceUri;

            if (!ResourceIdentity.IsRestResourceIdentity(parentResourceUri)) throw Error.Argument(nameof(parentResourceUri), "Must be an absolute FHIR REST identity when reference is relative");
            var parent = new ResourceIdentity(parentResourceUri);
            return HttpUtil.MakeAbsoluteToBase(referenceUri, parent.BaseUri);
        }

        /// <summary>
        /// When a ResourceReference is relative, use the parent resource's fullUrl (e.g. from a Bundle's entry)
        /// to make it absolute.
        /// </summary>
        /// <param name="reference">The ResourceReference to get the (possibily relative) url from</param>
        /// <param name="parentResourceUri">Absolute uri representing the location of the resource this reference is in.</param>
        /// <remarks>Implements (part of the logic) as described in bundle.html#6.7.4.1</remarks>
        /// <returns></returns>
        public static Uri GetAbsoluteUriForReference(this ResourceReference reference, string parentResourceUri)
        {
            return reference.GetAbsoluteUriForReference(new Uri(parentResourceUri, UriKind.RelativeOrAbsolute));
        }
    }
}
