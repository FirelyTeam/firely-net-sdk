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
    /// A description of a query with a set of parameters
    /// </summary>
    [FhirType("Query", IsResource=true)]
    [DataContract]
    public partial class Query : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The outcome of processing a query request
        /// </summary>
        [FhirEnumeration("QueryOutcome")]
        public enum QueryOutcome
        {
            [EnumLiteral("ok")]
            Ok, // The query was processed successfully.
            [EnumLiteral("limited")]
            Limited, // The query was processed successfully, but some additional limitations were added.
            [EnumLiteral("refused")]
            Refused, // The server refused to process the query.
            [EnumLiteral("error")]
            Error, // The server tried to process the query, but some error occurred.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("QueryResponseComponent")]
        [DataContract]
        public partial class QueryResponseComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Links response to source query
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri IdentifierElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// ok | limited | refused | error
            /// </summary>
            [FhirElement("outcome", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Query.QueryOutcome> OutcomeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Query.QueryOutcome? Outcome
            {
                get { return OutcomeElement != null ? OutcomeElement.Value : null; }
                set
                {
                    if(value == null)
                      OutcomeElement = null; 
                    else
                      OutcomeElement = new Code<Hl7.Fhir.Model.Query.QueryOutcome>(value);
                }
            }
            
            /// <summary>
            /// Total number of matching records
            /// </summary>
            [FhirElement("total", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer TotalElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Total
            {
                get { return TotalElement != null ? TotalElement.Value : null; }
                set
                {
                    if(value == null)
                      TotalElement = null; 
                    else
                      TotalElement = new Hl7.Fhir.Model.Integer(value);
                }
            }
            
            /// <summary>
            /// Parameters server used
            /// </summary>
            [FhirElement("parameter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Parameter { get; set; }
            
            /// <summary>
            /// To get first page (if paged)
            /// </summary>
            [FhirElement("first", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> First { get; set; }
            
            /// <summary>
            /// To get previous page (if paged)
            /// </summary>
            [FhirElement("previous", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Previous { get; set; }
            
            /// <summary>
            /// To get next page (if paged)
            /// </summary>
            [FhirElement("next", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Next { get; set; }
            
            /// <summary>
            /// To get last page (if paged)
            /// </summary>
            [FhirElement("last", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Last { get; set; }
            
            /// <summary>
            /// Resources that are the results of the search
            /// </summary>
            [FhirElement("reference", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Reference { get; set; }
            
        }
        
        
        /// <summary>
        /// Links query and its response(s)
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri IdentifierElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.FhirUri(value);
            }
        }
        
        /// <summary>
        /// Set of query parameters with values
        /// </summary>
        [FhirElement("parameter", Order=80)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> Parameter { get; set; }
        
        /// <summary>
        /// If this is a response to a query
        /// </summary>
        [FhirElement("response", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Query.QueryResponseComponent Response { get; set; }
        
    }
    
}
