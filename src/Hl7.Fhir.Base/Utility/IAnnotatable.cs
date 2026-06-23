/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
#nullable enable

using System;

namespace Hl7.Fhir.Utility;

public interface IAnnotatable : IAnnotated
{
    void AddAnnotation(object annotation);

    void RemoveAnnotations(Type type);

    /// <summary>
    /// Replaces any existing annotation of the same type with <paramref name="annotation"/>,
    /// ensuring exactly one annotation of that type is present.
    /// </summary>
    void SetAnnotation(object annotation)
    {
        lock (this)
        {
            RemoveAnnotations(annotation.GetType());
            AddAnnotation(annotation);
        }
    }
}

public static class AnnotatableExtensions
{
    public static void RemoveAnnotations<T>(this IAnnotatable annotatable)
    {
        annotatable.RemoveAnnotations(typeof(T));
    }

    public static void SetAnnotation<A>(this IAnnotatable annotatable, A annotation)
    {
        if (annotation != null)
            annotatable.SetAnnotation(annotation);
        else
        {
            lock (annotatable)
                annotatable.RemoveAnnotations<A>();
        }
    }
}