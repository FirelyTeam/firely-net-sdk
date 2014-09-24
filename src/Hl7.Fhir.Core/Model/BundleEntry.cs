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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using System.IO;

using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Model
{
    [InvokeIValidatableObject]
    public abstract class BundleEntry : Hl7.Fhir.Validation.IValidatableObject
    {
        public BundleEntry()
        {
            Links = new UriLinkList();
            Tags = new List<Tag>();
        }

        [Required]
        public Uri Id { get; set; }
        public UriLinkList Links { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public Uri SelfLink
        {
            get { return Links.SelfLink; }
            set { Links.SelfLink = value; }
        }

        /// <summary>
        /// Read-only property getting a summary from a Resource or a descriptive text in other cases.
        /// </summary>
        public abstract string Summary { get; }


        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if (Id != null && !Id.IsAbsoluteUri)
                result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry id must be an absolute URI"));

            if (Bundle.UriHasValue(SelfLink) && !SelfLink.IsAbsoluteUri)
                result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry selflink must be an absolute URI"));

            if (Links.FirstLink != null || Links.LastLink != null || Links.PreviousLink != null || Links.NextLink != null)
                result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Paging links can only be used on feeds, not entries"));

            if (Tags != null && validationContext.ValidateRecursively())
                DotNetAttributeValidation.TryValidate(Tags,result,true);

            return result;
        }
    }
}
