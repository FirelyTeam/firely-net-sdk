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
using System.Threading;

namespace Hl7.Fhir.Utility;

/// <summary>
/// This class implements the interfaces <see cref="IAnnotatable"/> and <see cref="IAnnotated"/>. It can be used by the classes that also implements these
/// interfaces to have a common implementation.
/// This list is thread safe
/// </summary>
public class AnnotationList : IAnnotatable, IEnumerable<object>
{
    // Null until the first annotation is written. Reads never create the dictionary, so an
    // object that is only inspected for annotations does not pay for an empty ConcurrentDictionary.
    private ConcurrentDictionary<Type, List<object>> _annotations;

    // Returns the dictionary, creating (and safely publishing) it when it does not exist yet. For writes only.
    // Right-sized: an element rarely carries more than a handful of annotation types, so avoid the default capacity (31 buckets).
    private ConcurrentDictionary<Type, List<object>> getOrCreate() =>
        LazyInitializer.EnsureInitialized(ref _annotations, () => new ConcurrentDictionary<Type, List<object>>(concurrencyLevel: 1, capacity: 3));

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
    /// Add an annotation to the list, combining with existing ones.
    /// </summary>
    public void AddAnnotation(object annotation)
    {
        getOrCreate().AddOrUpdate(
            annotation.GetType(),
            [annotation],
            (_, existingList) => [..existingList, annotation]);
    }

    /// <summary>
    /// Removes the annotation of the given type. If it does not exist, nothing happens.
    /// </summary>
    public void RemoveAnnotations(Type type) => _annotations?.TryRemove(type, out _);

    /// <inheritdoc/>
    public void SetAnnotation(object annotation) => getOrCreate().AddOrUpdate(annotation.GetType(), [annotation], (_, _) => [annotation]);

    /// <summary>
    /// Returns all annotations of type <paramref name="type"/>
    /// </summary>
    public IEnumerable<object> Annotations(Type type) =>
        _annotations is { } dict && dict.TryGetValue(type, out var values) ? values : Array.Empty<object>();

    /// <summary>
    /// Returns all annotations of type <paramref name="type"/>
    /// </summary>
    public IEnumerable<object> OfType(Type type) => Annotations(type);

    /// <summary>
    /// Gets a value that indicates whether there is an annotation present
    /// </summary>
    public bool IsEmpty => _annotations is not { } dict || dict.IsEmpty;

    /// <summary>
    /// Adds all the annotations from the <paramref name="source"/> to here. It will remove all existing annotations
    /// </summary>
    public void AddRange(AnnotationList source)
    {
        // The lists inside the dictionary are never mutated (writes replace them), so a shallow copy is independent.
        ConcurrentDictionary<Type, List<object>> copy = null;
        if (source._annotations is { IsEmpty: false } sourceDict)
        {
            copy = new ConcurrentDictionary<Type, List<object>>(1, sourceDict.Count);
            foreach (var (type, values) in sourceDict)
                copy[type] = values;
        }

        Interlocked.Exchange(ref _annotations, copy);
    }

    IEnumerator<object> IEnumerable<object>.GetEnumerator() =>
        _annotations is { } dict ? dict.Values.SelectMany(v => v).GetEnumerator() : Enumerable.Empty<object>().GetEnumerator();

    public IEnumerator GetEnumerator() => ((IEnumerable<object>)this).GetEnumerator();
}