using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A ratio of two Quantity values - a numerator and a denominator
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Ratio")]
    [DataContract]
    public partial class Ratio : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Ratio"; } }
    
        
        /// <summary>
        /// Numerator value
        /// </summary>
        [FhirElement("numerator", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Numerator
        {
            get { return _Numerator; }
            set { _Numerator = value; OnPropertyChanged("Numerator"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Numerator;
        
        /// <summary>
        /// Denominator value
        /// </summary>
        [FhirElement("denominator", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Denominator
        {
            get { return _Denominator; }
            set { _Denominator = value; OnPropertyChanged("Denominator"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Denominator;
    
    
        public static ElementDefinitionConstraint[] Ratio_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "rat-1",
                severity: ConstraintSeverity.Warning,
                expression: "numerator.empty() xor denominator",
                human: "Numerator and denominator SHALL both be present, or both are absent. If both are absent, there SHALL be some extension present",
                xpath: "(count(f:numerator) = count(f:denominator)) and ((count(f:numerator) > 0) or (count(f:extension) > 0))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4,Hl7.Fhir.Model.Version.STU3},
                key: "rat-1",
                severity: ConstraintSeverity.Warning,
                expression: "(numerator.empty() xor denominator.exists()) and (numerator.exists() or extension.exists())",
                human: "Numerator and denominator SHALL both be present, or both are absent. If both are absent, there SHALL be some extension present",
                xpath: "(count(f:numerator) = count(f:denominator)) and ((count(f:numerator) > 0) or (count(f:extension) > 0))"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Ratio;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Numerator != null) dest.Numerator = (Hl7.Fhir.Model.Quantity)Numerator.DeepCopy();
                if(Denominator != null) dest.Denominator = (Hl7.Fhir.Model.Quantity)Denominator.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Ratio());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Ratio;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Numerator, otherT.Numerator)) return false;
            if( !DeepComparable.Matches(Denominator, otherT.Denominator)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Ratio;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Numerator, otherT.Numerator)) return false;
            if( !DeepComparable.IsExactly(Denominator, otherT.Denominator)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Ratio");
            base.Serialize(sink);
            sink.Element("numerator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Numerator?.Serialize(sink);
            sink.Element("denominator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Denominator?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "numerator":
                    Numerator = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "denominator":
                    Denominator = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "numerator":
                    Numerator = source.Populate(Numerator);
                    return true;
                case "denominator":
                    Denominator = source.Populate(Denominator);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Numerator != null) yield return Numerator;
                if (Denominator != null) yield return Denominator;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Numerator != null) yield return new ElementValue("numerator", Numerator);
                if (Denominator != null) yield return new ElementValue("denominator", Denominator);
            }
        }
    
    }

}
