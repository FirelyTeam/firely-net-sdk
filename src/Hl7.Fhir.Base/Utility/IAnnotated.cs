/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    public interface IAnnotated
    {
        IEnumerable<object> Annotations(Type type);
    }

    public static class AnnotatedExtensions
    {
        public static object Annotation(this IAnnotated annotated, Type type) => annotated.Annotations(type)?.FirstOrDefault();

        public static bool TryGetAnnotation(this IAnnotated annotated, Type type, out object annotation)
        {
            annotation = annotated.Annotations(type)?.FirstOrDefault();
            return annotation != null;
        }

        public static A Annotation<A>(this IAnnotated annotated) => (A)annotated.Annotation(typeof(A));

        public static bool TryGetAnnotation<A>(this IAnnotated annotated, out A annotation) where A:class
        {
            annotation = annotated.Annotations<A>()?.FirstOrDefault();
            return annotation != null;
        }

        public static IEnumerable<A> Annotations<A>(this IAnnotated annotated) => annotated.Annotations(typeof(A))?.Cast<A>() ?? Enumerable.Empty<A>();

        public static bool HasAnnotation(this IAnnotated annotated, Type type) => annotated.Annotations(type)?.Any() == true;

        public static bool HasAnnotation<A>(this IAnnotated annotated) => annotated.HasAnnotation(typeof(A));
    }
}
