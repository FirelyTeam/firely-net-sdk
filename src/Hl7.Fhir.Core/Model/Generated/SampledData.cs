using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A series of measurements taken by a device
    /// </summary>
    [FhirType("SampledData")]
    [DataContract]
    public partial class SampledData : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "SampledData"; } }
        
        /// <summary>
        /// Zero value and units
        /// </summary>
        [FhirElement("origin", InSummary=true, Order=30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity Origin
        {
            get { return _Origin; }
            set { _Origin = value; OnPropertyChanged("Origin"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _Origin;
        
        /// <summary>
        /// Number of milliseconds between samples
        /// </summary>
        [FhirElement("period", InSummary=true, Order=40)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal PeriodElement
        {
            get { return _PeriodElement; }
            set { _PeriodElement = value; OnPropertyChanged("PeriodElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _PeriodElement;
        
        /// <summary>
        /// Number of milliseconds between samples
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? Period
        {
            get { return PeriodElement != null ? PeriodElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  PeriodElement = null; 
                else
                  PeriodElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Period");
            }
        }
        
        /// <summary>
        /// Multiply data by this before adding to origin
        /// </summary>
        [FhirElement("factor", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal FactorElement
        {
            get { return _FactorElement; }
            set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _FactorElement;
        
        /// <summary>
        /// Multiply data by this before adding to origin
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? Factor
        {
            get { return FactorElement != null ? FactorElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  FactorElement = null; 
                else
                  FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Factor");
            }
        }
        
        /// <summary>
        /// Lower limit of detection
        /// </summary>
        [FhirElement("lowerLimit", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal LowerLimitElement
        {
            get { return _LowerLimitElement; }
            set { _LowerLimitElement = value; OnPropertyChanged("LowerLimitElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _LowerLimitElement;
        
        /// <summary>
        /// Lower limit of detection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? LowerLimit
        {
            get { return LowerLimitElement != null ? LowerLimitElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  LowerLimitElement = null; 
                else
                  LowerLimitElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("LowerLimit");
            }
        }
        
        /// <summary>
        /// Upper limit of detection
        /// </summary>
        [FhirElement("upperLimit", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal UpperLimitElement
        {
            get { return _UpperLimitElement; }
            set { _UpperLimitElement = value; OnPropertyChanged("UpperLimitElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _UpperLimitElement;
        
        /// <summary>
        /// Upper limit of detection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? UpperLimit
        {
            get { return UpperLimitElement != null ? UpperLimitElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  UpperLimitElement = null; 
                else
                  UpperLimitElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("UpperLimit");
            }
        }
        
        /// <summary>
        /// Number of sample points at each time point
        /// </summary>
        [FhirElement("dimensions", InSummary=true, Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt DimensionsElement
        {
            get { return _DimensionsElement; }
            set { _DimensionsElement = value; OnPropertyChanged("DimensionsElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _DimensionsElement;
        
        /// <summary>
        /// Number of sample points at each time point
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Dimensions
        {
            get { return DimensionsElement != null ? DimensionsElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  DimensionsElement = null; 
                else
                  DimensionsElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Dimensions");
            }
        }
        
        /// <summary>
        /// Decimal values with spaces, or "E" | "U" | "L"
        /// </summary>
        [FhirElement("data", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DataElement
        {
            get { return _DataElement; }
            set { _DataElement = value; OnPropertyChanged("DataElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DataElement;
        
        /// <summary>
        /// Decimal values with spaces, or "E" | "U" | "L"
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Data
        {
            get { return DataElement != null ? DataElement.Value : null; }
            set
            {
                if (value == null)
                  DataElement = null; 
                else
                  DataElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Data");
            }
        }
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SampledData;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Origin != null) dest.Origin = (Hl7.Fhir.Model.SimpleQuantity)Origin.DeepCopy();
                if(PeriodElement != null) dest.PeriodElement = (Hl7.Fhir.Model.FhirDecimal)PeriodElement.DeepCopy();
                if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                if(LowerLimitElement != null) dest.LowerLimitElement = (Hl7.Fhir.Model.FhirDecimal)LowerLimitElement.DeepCopy();
                if(UpperLimitElement != null) dest.UpperLimitElement = (Hl7.Fhir.Model.FhirDecimal)UpperLimitElement.DeepCopy();
                if(DimensionsElement != null) dest.DimensionsElement = (Hl7.Fhir.Model.PositiveInt)DimensionsElement.DeepCopy();
                if(DataElement != null) dest.DataElement = (Hl7.Fhir.Model.FhirString)DataElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SampledData());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SampledData;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Origin, otherT.Origin)) return false;
            if( !DeepComparable.Matches(PeriodElement, otherT.PeriodElement)) return false;
            if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
            if( !DeepComparable.Matches(LowerLimitElement, otherT.LowerLimitElement)) return false;
            if( !DeepComparable.Matches(UpperLimitElement, otherT.UpperLimitElement)) return false;
            if( !DeepComparable.Matches(DimensionsElement, otherT.DimensionsElement)) return false;
            if( !DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SampledData;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
            if( !DeepComparable.IsExactly(PeriodElement, otherT.PeriodElement)) return false;
            if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
            if( !DeepComparable.IsExactly(LowerLimitElement, otherT.LowerLimitElement)) return false;
            if( !DeepComparable.IsExactly(UpperLimitElement, otherT.UpperLimitElement)) return false;
            if( !DeepComparable.IsExactly(DimensionsElement, otherT.DimensionsElement)) return false;
            if( !DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Origin != null) yield return Origin;
                if (PeriodElement != null) yield return PeriodElement;
                if (FactorElement != null) yield return FactorElement;
                if (LowerLimitElement != null) yield return LowerLimitElement;
                if (UpperLimitElement != null) yield return UpperLimitElement;
                if (DimensionsElement != null) yield return DimensionsElement;
                if (DataElement != null) yield return DataElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (Origin != null) yield return new ElementValue("origin", Origin);
                if (PeriodElement != null) yield return new ElementValue("period", PeriodElement);
                if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                if (LowerLimitElement != null) yield return new ElementValue("lowerLimit", LowerLimitElement);
                if (UpperLimitElement != null) yield return new ElementValue("upperLimit", UpperLimitElement);
                if (DimensionsElement != null) yield return new ElementValue("dimensions", DimensionsElement);
                if (DataElement != null) yield return new ElementValue("data", DataElement);
 
            } 
        } 
    
    
    }
    
}
