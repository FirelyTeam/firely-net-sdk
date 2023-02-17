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

using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={DebuggerDisplay}}")]
    public abstract partial class DomainResource : IModifierExtendable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Rest.ResourceIdentity DebuggerDisplay => this.ResourceIdentity();

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>(base.Validate(validationContext));

            if (this.Contained != null)
            {
                if (!Contained.OfType<DomainResource>().All(cr => cr.Contained == null || !cr.Contained.Any()))
                    result.Add(COVE.CONTAINED_RESOURCES_CANNOT_BE_NESTED.AsResult(validationContext));
            }

            return result;
        }

        /// <summary>
        /// Finds the contained resource defined by the <paramref name="reference"/>. A reference to a contained resource starts with the
        /// character #.
        /// </summary>
        /// <param name="reference">the reference to contained resource</param>
        /// <returns>The resource referenced by <paramref name="reference"/>, null otherwise.</returns>
        public Resource FindContainedResource(ResourceReference reference)
        {
            if (reference == null) throw Error.ArgumentNull(nameof(reference));

            if (!reference.IsContainedReference) return null;

            if (reference.Reference == "#") return this;

            // search the contained resource by removing '#' 
            return Contained.FirstOrDefault(c => c.Id == reference.Reference.Remove(0, 1));
        }
        /// <summary>
        /// Finds the contained resource defined by the <paramref name="reference"/>. A reference to a contained resource starts with the
        /// character #.
        /// </summary>
        /// <param name="reference">the reference to contained resource</param>
        /// <returns>The resource referenced by <paramref name="reference"/>, null otherwise.</returns>

        public Resource FindContainedResource(string reference)
        {
            if (reference == null) throw Error.ArgumentNullOrEmpty(nameof(reference));

            return FindContainedResource(new ResourceReference(reference));
        }
    }
}
