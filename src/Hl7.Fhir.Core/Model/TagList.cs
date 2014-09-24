/*
  IList<Tag>
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

using Hl7.Fhir.Introspection;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Model
{
    [FhirType(IsResource=true)]
    public class TagList
    {
        public TagList()
        {
            Category = new List<Tag>();
        }

        public TagList(IEnumerable<Tag> tags)
        {
            this.Category = new List<Tag>(tags);
        }

        [FhirElement("category")]
        public List<Tag> Category { get; set; }
    }


    [FhirType]
    public class Tag
    {
       
        public static readonly Uri FHIRTAGSCHEME_GENERAL = new Uri(XmlNs.FHIRTAG, UriKind.Absolute);
        public static readonly Uri FHIRTAGSCHEME_PROFILE = new Uri(XmlNs.TAG_PROFILE, UriKind.Absolute);
        public static readonly Uri FHIRTAGSCHEME_SECURITY = new Uri(XmlNs.TAG_SECURITY, UriKind.Absolute);

        [Required]
        public string Term { get; private set; }
        [Required]
        public Uri Scheme { get; private set; }
        public string Label { get; private set; }


        public Tag()
        {
            // FhirParsers needs this constructor to be public
        }


        public Tag(string term, Uri scheme, string label=null)
        {
            if (term == null) throw new ArgumentNullException("term");
            if (scheme == null) throw new ArgumentNullException("scheme");

            this.Term = term;
            this.Scheme = scheme;
            this.Label = label;
        }

        public Tag(string term, string scheme, string label = null)
            : this(term, scheme==null ? null : new Uri(scheme,UriKind.RelativeOrAbsolute), label)
        {
        }

     
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var t = (Tag)obj;
            return String.Equals(this.Term, t.Term) && Uri.Equals(this.Scheme, t.Scheme);
        }

        public override int GetHashCode()
        {
            int hash = 0;

            if (Term != null) hash ^=  Term.GetHashCode();
            if (Scheme != null) hash ^= Scheme.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(this.Term);

            if(this.Scheme != null)
                result.AppendFormat("@{0}", this.Scheme);

            if(this.Label != null)
                result.AppendFormat(" ({0})", this.Label);

            return result.ToString();
        }

    }
}

