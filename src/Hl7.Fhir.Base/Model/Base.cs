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

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Hl7.Fhir.Model;

public abstract partial class Base : IDeepCopyable, IDeepComparable,
    IAnnotated, IAnnotatable, IValidatableObject, INotifyPropertyChanged
{
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public virtual string TypeName => GetType().Name;

    private Dictionary<string, object>? _overflow = null;

    /// <summary>
    /// A dictionary containing all elements that are not explicitly defined in the class.
    /// </summary>
    protected Dictionary<string, object> Overflow =>
        LazyInitializer.EnsureInitialized(ref _overflow, () => new Dictionary<string, object>())!;

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => [];

    #region << Annotations >>

    [NonSerialized] private AnnotationList? _annotations = null;

    private AnnotationList annotations => LazyInitializer.EnsureInitialized(ref _annotations, () => [])!;

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ITypedElement) || type == typeof(IShortPathGenerator) || type == typeof(IScopedNode))
                    return [this];
            else if (type == typeof(IFhirValueProvider))
                return [this];
            else if (type == typeof(IResourceTypeSupplier))
                return [this];
            else
                return annotations.OfType(type);
        }

    public void AddAnnotation(object annotation) => annotations.AddAnnotation(annotation);

    public void RemoveAnnotations(Type type) => annotations.RemoveAnnotations(type);

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string property) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    #endregion

    internal protected virtual Base SetValue(string key, object? value)
    {
        if (value is null)
            Overflow.Remove(key);
        else
        {
            if (value is not Base && value is not IReadOnlyList<Base>)
                throw new InvalidCastException($"Value for key '{key}' must be of type Base or a list of type Base.");
            Overflow[key] = value;
        }

        return this;
    }

    internal object this[string key]
    {
        get => this.TryGetValue(key, out var value)
            ? value
            : throw new KeyNotFoundException($"Element '{key}' is not a known FHIR element or has no value.");
        set => SetValue(key, value);
    }

    internal protected virtual bool TryGetValue(string key, [NotNullWhen(true)] out object? value) =>
        Overflow.TryGetValue(key, out value);

    internal protected virtual IEnumerable<KeyValuePair<string, object>> GetElementPairs() => Overflow;
}