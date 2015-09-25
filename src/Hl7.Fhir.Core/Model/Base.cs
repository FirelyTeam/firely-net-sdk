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
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    [InvokeIValidatableObject]
    [System.Runtime.Serialization.DataContract]
    public abstract class Base : Hl7.Fhir.Validation.IValidatableObject, IDeepCopyable, IDeepComparable
    {
        public abstract bool IsExactly(IDeepComparable other);
        public abstract bool Matches(IDeepComparable pattern);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>Does a deep-copy of all elements, except UserData</remarks>
        /// <returns></returns>
        public virtual IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Base;

            if (dest != null)
            {
                if (UserData != null) dest.UserData = new Dictionary<string,object>(UserData);
                if (FhirComments != null) dest.FhirComments = new List<string>(FhirComments);
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        public abstract IDeepCopyable DeepCopy();

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }

        private Dictionary<string, object> _userData = new Dictionary<string, object>();

        [NotMapped]
        public Dictionary<string, object> UserData
        {
            get { return _userData; }
            private set { _userData = value; }
        }


        public const string COMMENT_USERDATA_KEY = "$FHIR_COMMENTS";


        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        [FhirElement("fhir_comments", InSummary = false, Order = 5)]
        [Cardinality(Min = 0, Max = -1)]
        public List<Hl7.Fhir.Model.FhirString> FhirCommentsElement
        {
            get { if (_FhirCommentsElement == null) _FhirCommentsElement = new List<Hl7.Fhir.Model.FhirString>(); return _FhirCommentsElement; }
            set { _FhirCommentsElement = value; OnPropertyChanged("FhirCommentsElement"); }
        }

        private List<Hl7.Fhir.Model.FhirString> _FhirCommentsElement;

        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> FhirComments
        {
            get { return FhirCommentsElement != null ? FhirCommentsElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    FhirCommentsElement = null;
                else
                    FhirCommentsElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("FhirComments");
            }
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(property));
        }


        public abstract string TypeName { get; }
    }
}


