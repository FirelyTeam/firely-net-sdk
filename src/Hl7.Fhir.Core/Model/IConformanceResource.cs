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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model
{
    public interface IConformanceResource
    {       
        string Url { get; set; }
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }        
        string Name { get; set; }
        FhirString NameElement { get; set; }
        PublicationStatus? Status { get; set; }
        string Publisher { get; set; }
        FhirString PublisherElement { get; set; }        
        List<ContactDetail> Contact { get; set; }
        Markdown Description { get; set; }
        //FhirString DescriptionElement { get; set; }
        List<UsageContext> UseContext { get; set; }
        Markdown Purpose { get; set; }       
        Code<Hl7.Fhir.Model.PublicationStatus> StatusElement { get; set; }
        bool? Experimental { get; set; }
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        string Date { get; set; }
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }              
    }

    public interface IVersionableConformanceResource : IConformanceResource
    {
        string Version { get; set; }

        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
    }

    public partial class StructureDefinition : IVersionableConformanceResource
    {

    }
    
    public partial class ValueSet : IVersionableConformanceResource
    {

    }

    public partial class SearchParameter :IVersionableConformanceResource
    {

    }

    public partial class OperationDefinition :IVersionableConformanceResource
    {

    }

    public partial class CapabilityStatement : IVersionableConformanceResource
    {

    }

    public partial class MessageDefinition : IVersionableConformanceResource
    {

    }

    public partial class ImplementationGuide : IVersionableConformanceResource
    {
        //I think ImplementationGuide should have a purpose element.
        [NotMapped]
        public Markdown Purpose
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }
    }

    public partial class CompartmentDefinition : IConformanceResource
    {

    }
    public partial class StructureMap : IVersionableConformanceResource
    {

    }
    public partial class GraphDefinition : IVersionableConformanceResource
    {

    }

    public partial class CodeSystem : IVersionableConformanceResource
    {

    }

    public partial class ConceptMap : IVersionableConformanceResource
    {

    }
    public partial class TestScript : IVersionableConformanceResource
    {

    }


    public partial class ExpansionProfile : IVersionableConformanceResource
    {
        public Markdown Purpose
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }
    }

    public partial class DataElement : IConformanceResource
    {
        // I think DataElement should have Description too
        [NotMapped]
        [Obsolete("This property is internal only, and doesn't actually exist in the FHIR object model")]
        public Markdown Description
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        [NotMapped]
        public Markdown Purpose
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }
    }


    public partial class NamingSystem : IConformanceResource
    {
        // I think NamingSystem should have Experimental too
        [NotMapped]
        public Markdown Purpose
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        [NotMapped]
        public bool? Experimental
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        [NotMapped]
        public FhirBoolean ExperimentalElement
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Will return the (first) preferred UniqueId, or the first UniqueId if there is no preferred UniqueId
        /// </summary>
        [NotMapped]
        public string Url
        {
            get
            {
                var preferred = UniqueId.FirstOrDefault(id => id.Preferred == true)?.Value;
                return preferred ?? UniqueId.FirstOrDefault()?.Value;
            }
            set { throw new NotImplementedException(); }
        }

        [NotMapped]
        public FhirUri UrlElement
        {
            get
            {
                if (Url != null)
                    return new FhirUri(Url);
                else
                    return null;
            }
            set { throw new NotImplementedException(); }
        }
    }
}
