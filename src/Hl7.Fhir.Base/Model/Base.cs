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
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.Model;

public abstract partial class Base : IAnnotatable, INotifyPropertyChanged
{
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public virtual string TypeName => GetType().Name;
    
    private Dictionary<string, object>? _overflow = null;

    /// <summary>
    /// Whether the object has any overflow elements.
    /// </summary>
    public bool HasOverflow => _overflow?.Count > 0;

    /// <summary>
    /// A dictionary containing all elements that are not explicitly defined in the class.
    /// </summary>
    protected Dictionary<string, object> Overflow =>
        LazyInitializer.EnsureInitialized(ref _overflow, () => new Dictionary<string, object>())!;

    #region << Annotations >>

    [NonSerialized] private AnnotationList? _annotations = null;

    private AnnotationList annotations => LazyInitializer.EnsureInitialized(ref _annotations, () => [])!;

    public IEnumerable<object> Annotations(Type type)
    {
        if (type == typeof(IFhirValueProvider))
            return [this];
        
        if (annotations.OfType(type).ToList() is { Count: > 0 } annotation)
            return annotation;

        if (annotations.TryGetAnnotation(out TypedElementAnnotatedProvider? original))
            return original.OriginalElement.Annotations(type);

        return [];
    }

    public void AddAnnotation(object annotation) => annotations.AddAnnotation(annotation);

    public void RemoveAnnotations(Type type) => annotations.RemoveAnnotations(type);

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string property) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    #endregion

    /// <summary>
    /// Sets the value of an element in the POCO, or, if the element is not defined, in the overflow dictionary.
    /// </summary>
    /// <param name="key">The name of the element.</param>
    /// <param name="value">Either a <see cref="Base"/> or an <see cref="IReadOnlyList{T}"/> of <see cref="Base"/>.</param>
    /// <returns>The currect object, so the calls can be chained fluently.</returns>
    /// <exception cref="InvalidCastException">Thrown if the value is not a <c>Base</c> or <c>IReadOnlyList&lt;Base&gt;</c>.</exception>
    /// <remarks>If the value is set to <c>null</c>, the property is set to null, or, if not defined, the
    /// element is removed from the overflow dictionary. If the key refers to an existing property, the value
    /// must be compatible with the type of the property in the POCO, otherwise an <see cref="InvalidCastException"/> is thrown.</remarks>
    public virtual Base SetValue(string key, object? value)
    {
        if (value is null && HasOverflow)
            Overflow.Remove(key);
        else
        {
            if (value is not Base && value is not IReadOnlyList<Base>)
                throw new InvalidCastException($"Value for key '{key}' must be of type Base or a list of type Base.");
            Overflow[key] = value;
        }

        return this;
    }

    /// <summary>
    /// /// Gets the value of an element in the POCO, or, if the element is not defined, in the overflow dictionary.
    /// </summary>
    /// <param name="key">The name of the element.</param>
    /// <returns>A <see cref="Base"/> or an <see cref="IReadOnlyList{T}"/> of <see cref="Base"/>.</returns>
    /// <exception cref="KeyNotFoundException">If the element is not set, or is an empty list.</exception>
    public object this[string key]
    {
        get => this.TryGetValue(key, out var value)
            ? value
            : throw new KeyNotFoundException($"Element '{key}' is not a known FHIR element or has no value.");
        set => SetValue(key, value);
    }

    /// <summary>
    /// Gets the value of an element in the POCO, or, if the element is not defined, in the overflow dictionary.
    /// </summary>
    /// <param name="key">The name of the element.</param>
    /// <param name="value">Will be a <see cref="Base"/> or an <see cref="IReadOnlyList{T}"/> of <see cref="Base"/>.</param>
    /// <returns><c>true</c> if the given value was set in the POCO or present in the overflow dictionary, <c>false</c> otherwise.
    /// For lists, this means they should not be empty.</returns>
    public virtual bool TryGetValue(string key, [NotNullWhen(true)] out object? value)
    {
        if (HasOverflow) return Overflow.TryGetValue(key, out value);

        value = null;
        return false;
    }

    /// <summary>
    /// Enumerates all non-empty elements in the POCO and the overflow dictionary.
    /// </summary>
    /// <returns>A <see cref="KeyValuePair{TKey,TValue}" /> containing the key and the value, which is
    /// either a <see cref="Base"/> or an <see cref="IReadOnlyList{T}"/> of <see cref="Base"/>.</returns>
    public virtual IEnumerable<KeyValuePair<string, object>> EnumerateElements() => _overflow ?? [];

    /// <summary>
    /// Compare the children of this Base object with the children of another Base object using the specified comparer.
    /// </summary>
    /// <remarks>The <paramref name="comparer"/> must implement both <c>IEqualityComparer&lt;Base&gt;</c> and
    /// <c>IEqualityComparer&lt;IEnumerable&lt;Base&gt;&gt;</c>.</remarks>
    public virtual bool CompareChildren(Base other, IEqualityComparer<Base> comparer) => true;

    /// <summary>
    /// Validate invariants that hold across properties, or cannot be expressed by attributes on properties.
    /// </summary>
    protected internal virtual IReadOnlyCollection<CodedValidationException> ValidateInvariants(PocoValidationContext validationContext)
    {
       if(!this.EnumerateElements().Any())
           return [CodedValidationException.ELEMENT_CANNOT_BE_EMPTY(validationContext)];
       
       return [];
    }
}