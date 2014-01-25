using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Name of a human - parts and usage
    /// </summary>
    [FhirType("HumanName")]
    [DataContract]
    public partial class HumanName : Hl7.Fhir.Model.Element
    {
        /// <summary>
        /// The use of a human name
        /// </summary>
        [FhirEnumeration("NameUse")]
        public enum NameUse
        {
            [EnumLiteral("usual")]
            Usual, // Known as/conventional/the one you normally use.
            [EnumLiteral("official")]
            Official, // The formal name as registered in an official (government) registry, but which name might not be commonly used. May be called "legal name".
            [EnumLiteral("temp")]
            Temp, // A temporary name. Name.period can provide more detailed information. This may also be used for temporary names assigned at birth or in emergency situations.
            [EnumLiteral("nickname")]
            Nickname, // A name that is used to address the person in an informal manner, but is not part of their formal or usual name.
            [EnumLiteral("anonymous")]
            Anonymous, // Anonymous assigned name, alias, or pseudonym (used to protect a person's identity for privacy reasons).
            [EnumLiteral("old")]
            Old, // This name is no longer in use (or was never correct, but retained for records).
            [EnumLiteral("maiden")]
            Maiden, // A name used prior to marriage. Marriage naming customs vary greatly around the world. This name use is for use by applications that collect and store "maiden" names. Though the concept of maiden name is often gender specific, the use of this term is not gender specific. The use of this term does not imply any particular history for a person's name, nor should the maiden name be determined algorithmically.
        }
        
        /// <summary>
        /// usual | official | temp | nickname | anonymous | old | maiden
        /// </summary>
        [FhirElement("use", Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.HumanName.NameUse> UseElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.HumanName.NameUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if(value == null)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.HumanName.NameUse>(value);
            }
        }
        
        /// <summary>
        /// Text representation of the full name
        /// </summary>
        [FhirElement("text", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Text
        {
            get { return TextElement != null ? TextElement.Value : null; }
            set
            {
                if(value == null)
                  TextElement = null; 
                else
                  TextElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Family name (often called 'Surname')
        /// </summary>
        [FhirElement("family", Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> FamilyElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Family
        {
            get { return FamilyElement != null ? FamilyElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  FamilyElement = null; 
                else
                  FamilyElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
            }
        }
        
        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        [FhirElement("given", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> GivenElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Given
        {
            get { return GivenElement != null ? GivenElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  GivenElement = null; 
                else
                  GivenElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
            }
        }
        
        /// <summary>
        /// Parts that come before the name
        /// </summary>
        [FhirElement("prefix", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> PrefixElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Prefix
        {
            get { return PrefixElement != null ? PrefixElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  PrefixElement = null; 
                else
                  PrefixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
            }
        }
        
        /// <summary>
        /// Parts that come after the name
        /// </summary>
        [FhirElement("suffix", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> SuffixElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Suffix
        {
            get { return SuffixElement != null ? SuffixElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  SuffixElement = null; 
                else
                  SuffixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
            }
        }
        
        /// <summary>
        /// Time period when name was/is in use
        /// </summary>
        [FhirElement("period", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
    }
    
}
