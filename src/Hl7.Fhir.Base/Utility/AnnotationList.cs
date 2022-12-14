/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// This class implements the interfaces IAnnotatable and IAnnotated. It can be used by the classes that also implements these
    /// interfaces to have a common implementation. 
    /// This list is thread safe
    /// </summary>
    public class AnnotationList : IAnnotatable, IAnnotated
    {
        private Lazy<ConcurrentDictionary<Type, List<object>>> _annotations = new Lazy<ConcurrentDictionary<Type, List<object>>>(() => new ConcurrentDictionary<Type, List<object>>());
        private ConcurrentDictionary<Type, List<object>> annotations { get { return _annotations.Value; } }

        public void AddAnnotation(object annotation)
        {
            annotations.AddOrUpdate(
                annotation.GetType(),
                new List<object>() { annotation },
                (t, existingList) => new List<object>(existingList) { annotation });
        }

        public void RemoveAnnotations(Type type) => annotations.TryRemove(type, out _);

        public IEnumerable<object> Annotations(Type type)
        {
            if (annotations.TryGetValue(type, out var values))
                return values;
            return Enumerable.Empty<object>();
        }

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
        /// <param name="source"></param>
        public void AddRange(AnnotationList source)
        {
            _annotations = new Lazy<ConcurrentDictionary<Type, List<object>>>(() => new ConcurrentDictionary<Type, List<object>>(source.annotations));
        }
    }
}
