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
using Hl7.Fhir.Introspection;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
#if NET45
    [Serializable]
#endif
    [InvokeIValidatableObject]
    [System.Runtime.Serialization.DataContract]
    public abstract class Base : Hl7.Fhir.Validation.IValidatableObject, IDeepCopyable, IDeepComparable, IAnnotated, IAnnotatable
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
            if (other is Base dest)
            {
                // if (UserData != null) dest.UserData = new Dictionary<string, object>(UserData);
                if (_annotations.IsValueCreated)
                {
                    dest.annotations.Clear();
                    dest.annotations.AddRange(annotations);
                }

#pragma warning disable 618,620            
                if (UserData != null) dest.UserData = new Dictionary<string, object>(UserData);
#pragma warning restore 618

               // if (FhirComments != null) dest.FhirComments = new List<string>(FhirComments);
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

        #region << Annotations and UserData >>
        private Dictionary<string, object> _userData = new Dictionary<string, object>();

        [NotMapped]
        [Obsolete("Use the typed interface provided by IAnnotatable instead")]
        public Dictionary<string, object> UserData
        {
            get { return _userData; }
            private set { _userData = value; }
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public IEnumerable<object> Annotations(Type type)
        {
            return annotations.OfType(type);
        }

        public void AddAnnotation(object annotation)
        {
            annotations.Add(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            annotations.RemoveOfType(type);
        }

        #endregion

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(property));
        }


        public abstract string TypeName { get; }

        /// <summary>
        /// Enumerate all child nodes.
        /// Return a sequence of child elements, components and/or properties.
        /// Child nodes are returned in the order defined by the FHIR specification.
        /// First returns child nodes inherited from any base class(es), recursively.
        /// Finally returns child nodes defined by the current class.
        /// </summary>
        [NotMapped]
        public virtual IEnumerable<Base> Children { get { return Enumerable.Empty<Base>(); } }

        /// <summary>
        /// Enumerate all child nodes.
        /// Return a sequence of child elements, components and/or properties.
        /// Child nodes are returned as tuples with the name and the node itself, in the order defined 
        /// by the FHIR specification.
        /// First returns child nodes inherited from any base class(es), recursively.
        /// Finally returns child nodes defined by the current class.
        /// </summary>
        [NotMapped]
        internal virtual IEnumerable<ElementValue> NamedChildren => Enumerable.Empty<ElementValue>();
    }
}
