/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Utility;

/// <summary>
/// This class implements the interfaces <see cref="IAnnotatable"/> and <see cref="IAnnotated"/>. It can be used by the classes that also implements these
/// interfaces to have a common implementation.
/// This list is thread safe
/// </summary>
public class AnnotationList : IAnnotatable, IEnumerable<object>
{
    private Lazy<ConcurrentDictionary<Type, List<object>>> _annotations = new(() => new ConcurrentDictionary<Type, List<object>>());
    private ConcurrentDictionary<Type, List<object>> annotations { get { return _annotations.Value; } }

    /// <summary>
    /// Create a list containing the given annotations.
    /// </summary>
    public static AnnotationList Create(ReadOnlySpan<object> annotations)
    {
        var list = new AnnotationList();
        foreach (var annotation in annotations)
        {
            list.AddAnnotation(annotation);
        }
        return list;
    }

    /// <summary>
    /// Add an annotation to the list, possibly replacing an existing one.
    /// </summary>
    public void AddAnnotation(object annotation)
    {
        annotations.AddOrUpdate(
            annotation.GetType(),
            [annotation],
            (_, existingList) => [..existingList, annotation]);
    }

    /// <summary>
    /// Removes the annotation of the given type. If it does not exist, nothing happens.
    /// </summary>
    public void RemoveAnnotations(Type type) => annotations.TryRemove(type, out _);

    /// <summary>
    /// Returns all annotations of type <paramref name="type"/>
    /// </summary>
    public IEnumerable<object> Annotations(Type type) =>
        annotations.TryGetValue(type, out var values) ? values : [];

    /// <summary>
    /// Returns all annotations of type <paramref name="type"/>
    /// </summary>
    public IEnumerable<object> OfType(Type type) => Annotations(type);

    /// <summary>
    /// Gets a value that indicates whether there is an annotation present
    /// </summary>
    public bool IsEmpty => annotations.IsEmpty;

    /// <summary>
    /// Adds all the annotations from the <paramref name="source"/> to here. It will remove all existing annotations
    /// </summary>
    public void AddRange(AnnotationList source)
    {
        _annotations = new Lazy<ConcurrentDictionary<Type, List<object>>>(() => new ConcurrentDictionary<Type, List<object>>(source.annotations));
    }

    IEnumerator<object> IEnumerable<object>.GetEnumerator() => annotations.Values.SelectMany(v => v).GetEnumerator();

    public IEnumerator GetEnumerator() => ((IEnumerable<object>)this).GetEnumerator();
}