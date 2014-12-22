using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011+, HL7, Inc.
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
// Generated on Mon, Dec 22, 2014 15:52+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Resource data element
    /// </summary>
    [FhirType("DataElement", IsResource=true)]
    [DataContract]
    public partial class DataElement : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DataElement; } }
        [NotMapped]
        public override string TypeName { get { return "DataElement"; } }
        
        /// <summary>
        /// Indicates the degree of precision of the data element definition
        /// </summary>
        [FhirEnumeration("DataElementGranularity")]
        public enum DataElementGranularity
        {
            /// <summary>
            /// The data element is sufficiently well-constrained that multiple pieces of data captured according to the constraints of the data element will be comparable (though in some cases, a degree of automated conversion/normalization may be required).
            /// </summary>
            [EnumLiteral("comparable")]
            Comparable,
            /// <summary>
            /// The data element is fully specified down to a single value set, single unit of measure, single data type, etc.  Multiple pieces of data associated with this data element are fully compareable.
            /// </summary>
            [EnumLiteral("fully specified")]
            FullySpecified,
            /// <summary>
            /// The data element allows multiple units of measure having equivalent meaning.  E.g. "cc" (cubic centimeter) and "mL".
            /// </summary>
            [EnumLiteral("equivalent")]
            Equivalent,
            /// <summary>
            /// The data element allows multiple units of measure that are convertable between each other (e.g. Inches and centimeters) and/or allows data to be captured in multiple value sets for which a known mapping exists allowing conversion of meaning.
            /// </summary>
            [EnumLiteral("convertable")]
            Convertable,
            /// <summary>
            /// A convertable data element where unit conversions are different only by a power of 10.  E.g. g, mg, kg.
            /// </summary>
            [EnumLiteral("scaleable")]
            Scaleable,
            /// <summary>
            /// The data element is unconstrained in units, choice of data types and/or choice of vocabulary such that automated comparison of data captured using the data element is not possible.
            /// </summary>
            [EnumLiteral("flexible")]
            Flexible,
        }
        
        /// <summary>
        /// The lifecycle status of a Resource data element
        /// </summary>
        [FhirEnumeration("ResourceDataElementStatus")]
        public enum ResourceDataElementStatus
        {
            /// <summary>
            /// This data element is still under development.
            /// </summary>
            [EnumLiteral("draft")]
            Draft,
            /// <summary>
            /// This data element is ready for normal use.
            /// </summary>
            [EnumLiteral("active")]
            Active,
            /// <summary>
            /// This data element has been deprecated, withdrawn or superseded and should no longer be used.
            /// </summary>
            [EnumLiteral("retired")]
            Retired,
        }
        
        [FhirType("DataElementMappingComponent")]
        [DataContract]
        public partial class DataElementMappingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DataElementMappingComponent"; } }
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            [FhirElement("uri", InSummary=true, Order=20)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if(value == null)
                      UriElement = null; 
                    else
                      UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// True if mapping defines element
            /// </summary>
            [FhirElement("definitional", InSummary=true, Order=30)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean DefinitionalElement
            {
                get { return _DefinitionalElement; }
                set { _DefinitionalElement = value; OnPropertyChanged("DefinitionalElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _DefinitionalElement;
            
            /// <summary>
            /// True if mapping defines element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Definitional
            {
                get { return DefinitionalElement != null ? DefinitionalElement.Value : null; }
                set
                {
                    if(value == null)
                      DefinitionalElement = null; 
                    else
                      DefinitionalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Definitional");
                }
            }
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc
            /// </summary>
            [FhirElement("comments", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement
            {
                get { return _CommentsElement; }
                set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _CommentsElement;
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comments
            {
                get { return CommentsElement != null ? CommentsElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentsElement = null; 
                    else
                      CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comments");
                }
            }
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            [FhirElement("map", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MapElement
            {
                get { return _MapElement; }
                set { _MapElement = value; OnPropertyChanged("MapElement"); }
            }
            private Hl7.Fhir.Model.FhirString _MapElement;
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Map
            {
                get { return MapElement != null ? MapElement.Value : null; }
                set
                {
                    if(value == null)
                      MapElement = null; 
                    else
                      MapElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Map");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DataElementMappingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(DefinitionalElement != null) dest.DefinitionalElement = (Hl7.Fhir.Model.FhirBoolean)DefinitionalElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                    if(MapElement != null) dest.MapElement = (Hl7.Fhir.Model.FhirString)MapElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DataElementMappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DataElementMappingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(DefinitionalElement, otherT.DefinitionalElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
                if( !DeepComparable.Matches(MapElement, otherT.MapElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DataElementMappingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionalElement, otherT.DefinitionalElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
                if( !DeepComparable.IsExactly(MapElement, otherT.MapElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DataElementBindingComponent")]
        [DataContract]
        public partial class DataElementBindingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DataElementBindingComponent"; } }
            
            /// <summary>
            /// Can additional codes be used?
            /// </summary>
            [FhirElement("isExtensible", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsExtensibleElement
            {
                get { return _IsExtensibleElement; }
                set { _IsExtensibleElement = value; OnPropertyChanged("IsExtensibleElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _IsExtensibleElement;
            
            /// <summary>
            /// Can additional codes be used?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsExtensible
            {
                get { return IsExtensibleElement != null ? IsExtensibleElement.Value : null; }
                set
                {
                    if(value == null)
                      IsExtensibleElement = null; 
                    else
                      IsExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsExtensible");
                }
            }
            
            /// <summary>
            /// required | preferred | example
            /// </summary>
            [FhirElement("conformance", InSummary=true, Order=30)]
            [DataMember]
            public Hl7.Fhir.Model.Code ConformanceElement
            {
                get { return _ConformanceElement; }
                set { _ConformanceElement = value; OnPropertyChanged("ConformanceElement"); }
            }
            private Hl7.Fhir.Model.Code _ConformanceElement;
            
            /// <summary>
            /// required | preferred | example
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Conformance
            {
                get { return ConformanceElement != null ? ConformanceElement.Value : null; }
                set
                {
                    if(value == null)
                      ConformanceElement = null; 
                    else
                      ConformanceElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Conformance");
                }
            }
            
            /// <summary>
            /// Human explanation of the value set
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Human explanation of the value set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Source of value set
            /// </summary>
            [FhirElement("valueSet", InSummary=true, Order=50)]
            [References("ValueSet")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ValueSet
            {
                get { return _ValueSet; }
                set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
            }
            private Hl7.Fhir.Model.ResourceReference _ValueSet;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DataElementBindingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IsExtensibleElement != null) dest.IsExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)IsExtensibleElement.DeepCopy();
                    if(ConformanceElement != null) dest.ConformanceElement = (Hl7.Fhir.Model.Code)ConformanceElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ValueSet != null) dest.ValueSet = (Hl7.Fhir.Model.ResourceReference)ValueSet.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DataElementBindingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DataElementBindingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
                if( !DeepComparable.Matches(ConformanceElement, otherT.ConformanceElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DataElementBindingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
                if( !DeepComparable.IsExactly(ConformanceElement, otherT.ConformanceElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this data element
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Logical id for this version of the data element
        /// </summary>
        [FhirElement("version", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical id for this version of the data element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DataElement.ResourceDataElementStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.DataElement.ResourceDataElementStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DataElement.ResourceDataElementStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.DataElement.ResourceDataElementStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Date for this version of the data element
        /// </summary>
        [FhirElement("date", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for this version of the data element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Descriptive label for this element definition
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Descriptive label for this element definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("category", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// comparable | fully specified | equivalent | convertable | scaleable | flexible
        /// </summary>
        [FhirElement("granularity", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DataElement.DataElementGranularity> GranularityElement
        {
            get { return _GranularityElement; }
            set { _GranularityElement = value; OnPropertyChanged("GranularityElement"); }
        }
        private Code<Hl7.Fhir.Model.DataElement.DataElementGranularity> _GranularityElement;
        
        /// <summary>
        /// comparable | fully specified | equivalent | convertable | scaleable | flexible
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DataElement.DataElementGranularity? Granularity
        {
            get { return GranularityElement != null ? GranularityElement.Value : null; }
            set
            {
                if(value == null)
                  GranularityElement = null; 
                else
                  GranularityElement = new Code<Hl7.Fhir.Model.DataElement.DataElementGranularity>(value);
                OnPropertyChanged("Granularity");
            }
        }
        
        /// <summary>
        /// Identifying concept
        /// </summary>
        [FhirElement("code", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// Prompt for element phrased as question
        /// </summary>
        [FhirElement("question", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString QuestionElement
        {
            get { return _QuestionElement; }
            set { _QuestionElement = value; OnPropertyChanged("QuestionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _QuestionElement;
        
        /// <summary>
        /// Prompt for element phrased as question
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Question
        {
            get { return QuestionElement != null ? QuestionElement.Value : null; }
            set
            {
                if(value == null)
                  QuestionElement = null; 
                else
                  QuestionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Question");
            }
        }
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        [FhirElement("label", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LabelElement
        {
            get { return _LabelElement; }
            set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
        }
        private Hl7.Fhir.Model.FhirString _LabelElement;
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Label
        {
            get { return LabelElement != null ? LabelElement.Value : null; }
            set
            {
                if(value == null)
                  LabelElement = null; 
                else
                  LabelElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Label");
            }
        }
        
        /// <summary>
        /// Definition/description as narrative text
        /// </summary>
        [FhirElement("definition", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DefinitionElement
        {
            get { return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DefinitionElement;
        
        /// <summary>
        /// Definition/description as narrative text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Value : null; }
            set
            {
                if(value == null)
                  DefinitionElement = null; 
                else
                  DefinitionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        [FhirElement("comments", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentsElement
        {
            get { return _CommentsElement; }
            set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
        }
        private Hl7.Fhir.Model.FhirString _CommentsElement;
        
        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comments
        {
            get { return CommentsElement != null ? CommentsElement.Value : null; }
            set
            {
                if(value == null)
                  CommentsElement = null; 
                else
                  CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comments");
            }
        }
        
        /// <summary>
        /// Why is this needed?
        /// </summary>
        [FhirElement("requirements", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Why is this needed?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if(value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Other names
        /// </summary>
        [FhirElement("synonym", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> SynonymElement
        {
            get { if(_SynonymElement==null) _SynonymElement = new List<Hl7.Fhir.Model.FhirString>(); return _SynonymElement; }
            set { _SynonymElement = value; OnPropertyChanged("SynonymElement"); }
        }
        private List<Hl7.Fhir.Model.FhirString> _SynonymElement;
        
        /// <summary>
        /// Other names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Synonym
        {
            get { return SynonymElement != null ? SynonymElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  SynonymElement = null; 
                else
                  SynonymElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Synonym");
            }
        }
        
        /// <summary>
        /// Name of Data type
        /// </summary>
        [FhirElement("type", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// Name of Data type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Example value: [as defined for type]
        /// </summary>
        [FhirElement("example", Order=220, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
        [DataMember]
        public Hl7.Fhir.Model.Element Example
        {
            get { return _Example; }
            set { _Example = value; OnPropertyChanged("Example"); }
        }
        private Hl7.Fhir.Model.Element _Example;
        
        /// <summary>
        /// Length for strings
        /// </summary>
        [FhirElement("maxLength", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Integer MaxLengthElement
        {
            get { return _MaxLengthElement; }
            set { _MaxLengthElement = value; OnPropertyChanged("MaxLengthElement"); }
        }
        private Hl7.Fhir.Model.Integer _MaxLengthElement;
        
        /// <summary>
        /// Length for strings
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? MaxLength
        {
            get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
            set
            {
                if(value == null)
                  MaxLengthElement = null; 
                else
                  MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("MaxLength");
            }
        }
        
        /// <summary>
        /// Units to use for measured value
        /// </summary>
        [FhirElement("units", Order=240, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Units
        {
            get { return _Units; }
            set { _Units = value; OnPropertyChanged("Units"); }
        }
        private Hl7.Fhir.Model.Element _Units;
        
        /// <summary>
        /// ValueSet details if this is coded
        /// </summary>
        [FhirElement("binding", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.DataElement.DataElementBindingComponent Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        private Hl7.Fhir.Model.DataElement.DataElementBindingComponent _Binding;
        
        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        [FhirElement("mapping", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DataElement.DataElementMappingComponent> Mapping
        {
            get { if(_Mapping==null) _Mapping = new List<Hl7.Fhir.Model.DataElement.DataElementMappingComponent>(); return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        private List<Hl7.Fhir.Model.DataElement.DataElementMappingComponent> _Mapping;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DataElement;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DataElement.ResourceDataElementStatus>)StatusElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(GranularityElement != null) dest.GranularityElement = (Code<Hl7.Fhir.Model.DataElement.DataElementGranularity>)GranularityElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(QuestionElement != null) dest.QuestionElement = (Hl7.Fhir.Model.FhirString)QuestionElement.DeepCopy();
                if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
                if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(SynonymElement != null) dest.SynonymElement = new List<Hl7.Fhir.Model.FhirString>(SynonymElement.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                if(Units != null) dest.Units = (Hl7.Fhir.Model.Element)Units.DeepCopy();
                if(Binding != null) dest.Binding = (Hl7.Fhir.Model.DataElement.DataElementBindingComponent)Binding.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.DataElement.DataElementMappingComponent>(Mapping.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DataElement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DataElement;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(GranularityElement, otherT.GranularityElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(QuestionElement, otherT.QuestionElement)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(SynonymElement, otherT.SynonymElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Example, otherT.Example)) return false;
            if( !DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.Matches(Units, otherT.Units)) return false;
            if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DataElement;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(GranularityElement, otherT.GranularityElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(QuestionElement, otherT.QuestionElement)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(SynonymElement, otherT.SynonymElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
            if( !DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.IsExactly(Units, otherT.Units)) return false;
            if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            
            return true;
        }
        
    }
    
}
