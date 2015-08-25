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
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay("\\{\"{TypeName,nq}/{Id,nq}\" Identity={ResourceIdentity()}}")]
    [InvokeIValidatableObject]
    public abstract partial class Resource 
    {
        /// <summary>
        /// This is the base URL of the FHIR server that this resource is hosted on
        /// </summary>
        [NotMapped]
        public Uri ResourceBase
        {
            get {
                object data;
                var result = UserData.TryGetValue("@@@RESOURCEBASE@@@", out data);
                if (result)
                    return data as Uri;
                else
                    return null;
            }

            set
            {
                UserData["@@@RESOURCEBASE@@@"] = value;
            }
        }        


        /// <summary>
        /// Returns the entire URI of the location that this resource was retrieved from
        /// </summary>
        /// <remarks>
        /// It is not stored, but reconstructed from the components of the resource
        /// </remarks>
        /// <returns></returns>
        public ResourceIdentity ResourceIdentity(string baseUrl = null)
        {
            if (Id == null) return null;

            var result =  Hl7.Fhir.Rest.ResourceIdentity.Build(TypeName, Id, VersionId);

            if (!string.IsNullOrEmpty(baseUrl))
                return result.WithBase(baseUrl);

            if (ResourceBase != null)
                return result.WithBase(ResourceBase);
            else
                return result;
        }

        public ResourceIdentity ResourceIdentity(Uri baseUrl)
        {
            return ResourceIdentity(baseUrl.OriginalString);
        }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            // The ID field does not need to be an abolute URI,
            // this should be the ResourceIdentity.
            // if (Id != null && !new Uri(Id,UriKind.RelativeOrAbsolute).IsAbsoluteUri)
            //    result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry id must be an absolute URI"));

            if(Meta != null)
            {
                // if (!String.IsNullOrEmpty(this.Meta.VersionId) && !new Uri(Id,UriKind.RelativeOrAbsolute).IsAbsoluteUri)
                //     result.Add(DotNetAttributeValidation.BuildResult(validationContext, "Entry selflink must be an absolute URI"));

                if (Meta.Tag != null && validationContext.ValidateRecursively())
                    DotNetAttributeValidation.TryValidate(Meta.Tag,result,true);
            }

            return result;
        }

        [NotMapped]
        public string VersionId
        {
            get 
            {
                if (HasVersionId)
                    return Meta.VersionId;
                else 
                    return null;
            }
            set
            {
                if (Meta == null) Meta = new Meta();
                Meta.VersionId = value;
            }
        }

        [NotMapped]
        public bool HasVersionId
        {
            get { return Meta != null && Meta.VersionId != null; }
        }
    }
}


