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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A description of a query with a set of parameters
    /// </summary>
    [FhirType("Query", IsResource=true)]
    [DataContract]
    public partial class Query : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The outcome of processing a query request
        /// </summary>
        [FhirEnumeration("QueryOutcome")]
        public enum QueryOutcome
        {
            /// <summary>
            /// The query was processed successfully.
            /// </summary>
            [EnumLiteral("ok")]
            Ok,
            /// <summary>
            /// The query was processed successfully, but some additional limitations were added.
            /// </summary>
            [EnumLiteral("limited")]
            Limited,
            /// <summary>
            /// The server refused to process the query.
            /// </summary>
            [EnumLiteral("refused")]
            Refused,
            /// <summary>
            /// The server tried to process the query, but some error occurred.
            /// </summary>
            [EnumLiteral("error")]
            Error,
        }
        
        [FhirType("QueryResponseComponent")]
        [DataContract]
        public partial class QueryResponseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Links response to source query
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _IdentifierElement;
            
            /// <summary>
            /// Links response to source query
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// ok | limited | refused | error
            /// </summary>
            [FhirElement("outcome", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Query.QueryOutcome> OutcomeElement
            {
                get { return _OutcomeElement; }
                set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
            }
            private Code<Hl7.Fhir.Model.Query.QueryOutcome> _OutcomeElement;
            
            /// <summary>
            /// ok | limited | refused | error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                    OnPropertyChanged("Outcome");
                }
            }
            
            /// <summary>
            /// Total number of matching records
            /// </summary>
            [FhirElement("total", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer TotalElement
            {
                get { return _TotalElement; }
                set { _TotalElement = value; OnPropertyChanged("TotalElement"); }
            }
            private Hl7.Fhir.Model.Integer _TotalElement;
            
            /// <summary>
            /// Total number of matching records
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                    OnPropertyChanged("Total");
                }
            }
            
            /// <summary>
            /// Parameters server used
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Parameter
            {
                get { return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            private List<Hl7.Fhir.Model.Extension> _Parameter;
            
            /// <summary>
            /// To get first page (if paged)
            /// </summary>
            [FhirElement("first", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> First
            {
                get { return _First; }
                set { _First = value; OnPropertyChanged("First"); }
            }
            private List<Hl7.Fhir.Model.Extension> _First;
            
            /// <summary>
            /// To get previous page (if paged)
            /// </summary>
            [FhirElement("previous", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Previous
            {
                get { return _Previous; }
                set { _Previous = value; OnPropertyChanged("Previous"); }
            }
            private List<Hl7.Fhir.Model.Extension> _Previous;
            
            /// <summary>
            /// To get next page (if paged)
            /// </summary>
            [FhirElement("next", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Next
            {
                get { return _Next; }
                set { _Next = value; OnPropertyChanged("Next"); }
            }
            private List<Hl7.Fhir.Model.Extension> _Next;
            
            /// <summary>
            /// To get last page (if paged)
            /// </summary>
            [FhirElement("last", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Extension> Last
            {
                get { return _Last; }
                set { _Last = value; OnPropertyChanged("Last"); }
            }
            private List<Hl7.Fhir.Model.Extension> _Last;
            
            /// <summary>
            /// Resources that are the results of the search
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=120)]
            [References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Reference;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QueryResponseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirUri)IdentifierElement.DeepCopy();
                    if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.Query.QueryOutcome>)OutcomeElement.DeepCopy();
                    if(TotalElement != null) dest.TotalElement = (Hl7.Fhir.Model.Integer)TotalElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.Extension>(Parameter.DeepCopy());
                    if(First != null) dest.First = new List<Hl7.Fhir.Model.Extension>(First.DeepCopy());
                    if(Previous != null) dest.Previous = new List<Hl7.Fhir.Model.Extension>(Previous.DeepCopy());
                    if(Next != null) dest.Next = new List<Hl7.Fhir.Model.Extension>(Next.DeepCopy());
                    if(Last != null) dest.Last = new List<Hl7.Fhir.Model.Extension>(Last.DeepCopy());
                    if(Reference != null) dest.Reference = new List<Hl7.Fhir.Model.ResourceReference>(Reference.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new QueryResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QueryResponseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.Matches(TotalElement, otherT.TotalElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.Matches(First, otherT.First)) return false;
                if( !DeepComparable.Matches(Previous, otherT.Previous)) return false;
                if( !DeepComparable.Matches(Next, otherT.Next)) return false;
                if( !DeepComparable.Matches(Last, otherT.Last)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QueryResponseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.IsExactly(TotalElement, otherT.TotalElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.IsExactly(First, otherT.First)) return false;
                if( !DeepComparable.IsExactly(Previous, otherT.Previous)) return false;
                if( !DeepComparable.IsExactly(Next, otherT.Next)) return false;
                if( !DeepComparable.IsExactly(Last, otherT.Last)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Links query and its response(s)
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri IdentifierElement
        {
            get { return _IdentifierElement; }
            set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
        }
        private Hl7.Fhir.Model.FhirUri _IdentifierElement;
        
        /// <summary>
        /// Links query and its response(s)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Identifier");
            }
        }
        
        /// <summary>
        /// Set of query parameters with values
        /// </summary>
        [FhirElement("parameter", Order=80)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> Parameter
        {
            get { return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        private List<Hl7.Fhir.Model.Extension> _Parameter;
        
        /// <summary>
        /// If this is a response to a query
        /// </summary>
        [FhirElement("response", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Query.QueryResponseComponent Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        private Hl7.Fhir.Model.Query.QueryResponseComponent _Response;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Query;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirUri)IdentifierElement.DeepCopy();
                if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.Extension>(Parameter.DeepCopy());
                if(Response != null) dest.Response = (Hl7.Fhir.Model.Query.QueryResponseComponent)Response.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Query());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Query;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Query;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            
            return true;
        }
        
    }
    
}
