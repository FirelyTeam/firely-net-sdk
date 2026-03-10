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

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace Hl7.Fhir.Model
{
    public abstract partial class Base : IDeepCopyable, IDeepComparable,
        IAnnotated, IAnnotatable, IValidatableObject, INotifyPropertyChanged, IReadOnlyDictionary<string, object>
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => Enumerable.Empty<ValidationResult>();

        #region << Annotations >>
        [NonSerialized]
        private AnnotationList _annotations = null;

        private AnnotationList annotations => LazyInitializer.EnsureInitialized(ref _annotations, () => new());

        public IEnumerable<object> Annotations(Type type) => annotations.OfType(type);

        public void AddAnnotation(object annotation) => annotations.AddAnnotation(annotation);

        public void RemoveAnnotations(Type type) => annotations.RemoveAnnotations(type);
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion

        public IReadOnlyDictionary<string, object> AsReadOnlyDictionary() => this;

        #region IReadOnlyDictionary
        IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => GetElementPairs().Select(kvp => kvp.Key);

        IEnumerable<object> IReadOnlyDictionary<string, object>.Values => GetElementPairs().Select(kvp => kvp.Value);

        int IReadOnlyCollection<KeyValuePair<string, object>>.Count => GetElementPairs().Count();

        object IReadOnlyDictionary<string, object>.this[string key] => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();

        bool IReadOnlyDictionary<string, object>.ContainsKey(string key) => TryGetValue(key, out _);

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => GetElementPairs().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetElementPairs().GetEnumerator();

        bool IReadOnlyDictionary<string, object>.TryGetValue(string key, out object value) => TryGetValue(key, out value);
        #endregion
    }
}