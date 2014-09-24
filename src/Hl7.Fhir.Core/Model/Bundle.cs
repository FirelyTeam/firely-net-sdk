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
    public class Bundle : Hl7.Fhir.Validation.IValidatableObject
    {
        [Required(AllowEmptyStrings=false)]
        public string Title { get; set; }

        [Required]
        public DateTimeOffset? LastUpdated { get; set; }

        [Required(AllowEmptyStrings=false)]
        public Uri Id { get; set; }
        public UriLinkList Links { get; set; }
        public IList<Tag> Tags { get; set; }

        public string AuthorName { get; set; }
        public string AuthorUri { get; set; }

        public int? TotalResults { get; set; }

        public IList<BundleEntry> Entries { get; set; }

        public Bundle()
        {
            Entries = new List<BundleEntry>();
            Links = new UriLinkList();
            Tags = new List<Tag>();
        }

        public Bundle(Uri id, string title, DateTimeOffset lastUpdated) : this()
        {
            Id = id;
            Title = title;
            LastUpdated = lastUpdated;
        }

        public Bundle(string title, DateTimeOffset lastUpdated)
            : this()
        {
            Id = new Uri("urn:uuid:" + Guid.NewGuid());
            Title = title;
            LastUpdated = lastUpdated;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            //if (String.IsNullOrWhiteSpace(Title))
            //    result.Add(new ValidationResult("Feed must contain a title", FhirValidator.SingleMemberName("Title"));

            //if (!UriHasValue(Id))
            //    result.Add(new ValidationResult("Feed must have an id"));
            //else
            //    if (!Id.IsAbsoluteUri)
            //        result.Add(new ValidationResult("Feed id must be an absolute URI"));

            if (Id != null && !Id.IsAbsoluteUri)
                result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Feed id must be an absolute URI"));

            //if (LastUpdated == null)
            //    result.Add(new ValidationResult("Feed must have a updated date"));

            if (Links.SearchLink != null)
                result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Links with rel='search' can only be used on feed entries"));

            bool feedHasAuthor = !String.IsNullOrEmpty(this.AuthorName);

            if (Entries != null && validationContext.ValidateRecursively())
            {
                foreach (var entry in Entries.Where(e => e != null))
                {
                    if (!feedHasAuthor && entry is ResourceEntry && String.IsNullOrEmpty(((ResourceEntry)entry).AuthorName))
                        result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Bundle's author and Entry author cannot both be empty"));

                    DotNetAttributeValidation.TryValidate(entry, result, validationContext.ValidateRecursively());
                }
            }

            return result;
        }

        internal static bool UriHasValue(Uri u)
        {
            return u != null && !String.IsNullOrEmpty(u.ToString());
        }

    }  
}
