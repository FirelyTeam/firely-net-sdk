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



using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    // Resource is not a subclass of Composite, since it
    // cannot be used in places where you can use composites.
    [InvokeIValidatableObject]
    public abstract partial class Resource : IExtendable, Hl7.Fhir.Validation.IValidatableObject, 
                IDeepCopyable, IDeepComparable
    {
        public abstract IDeepCopyable DeepCopy();

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO: Contained resources share the same internal id resolution space as the parent
            // resource -> verify id uniqueness
            var result = new List<ValidationResult>();

            // Validate specific invariants for contained items. The content of the contained
            // items is validated by the "normal" validation triggered by the FhirElement attribute
            if (Contained != null)
            {
                foreach (var contained in Contained)
                {
                    if (contained.Contained != null && contained.Contained.Any())
                        result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Contained resources cannot contain nested contained resources"));

                    if (contained.Text != null)
                        result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Contained resources should not contain narrative"));
                }
            }

            return result;
        }


        /// <summary>
        /// Finds a Resource amongst this Resource's contained resources
        /// </summary>
        /// <param name="containedReference">A ResourceReference containing an anchored resource id.</param>
        /// <returns>The found resource, or null if no matching contained resource was found. Will throw an exception if there's more than
        /// one matching contained resource</returns>
        public Resource FindContainedResource(ResourceReference containedReference)
        {
            return FindContainedResource(containedReference.Reference);
        }

        /// <summary>
        /// Finds a Resource amongst this Resource's contained resources
        /// </summary>
        /// <param name="containedReference">A Uri containing an anchored resource id.</param>
        /// <returns>The found resource, or null if no matching contained resource was found. Will throw an exception if there's more than
        /// one matching contained resource</returns>
        public Resource FindContainedResource(Uri containedReference)
        {
            return FindContainedResource(containedReference.ToString());
        }

        /// <summary>
        /// Finds a Resource amongst this Resource's contained resources
        /// </summary>
        /// <param name="containedReference">A string containing an anchored resource id.</param>
        /// <returns>The found resource, or null if no matching contained resource was found. Will throw an exception if there's more than
        /// one matching contained resource</returns>
        public Resource FindContainedResource(string containedReference)
        {
            if(containedReference == null) throw new ArgumentNullException("containedReference");
            if(!containedReference.StartsWith("#")) throw new ArgumentException("Reference is not a local anchored reference", "containedReference");
            
            var rref = containedReference.Substring(1);

            if(Contained == null) return null;

            return Contained.SingleOrDefault(r => r.Id != null && r.Id == rref);
        }
    }
}


