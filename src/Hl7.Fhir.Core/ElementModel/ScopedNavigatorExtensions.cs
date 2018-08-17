/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Rest;
using System;

namespace Hl7.Fhir.ElementModel
{
    public static class ScopedNavigatorExtensions
    {
        [Obsolete("Use Resolve<T>(this T nav, string reference, Func<string, T> externalResolver = null) where T : class, ITypedElement instead ")]
        public static T Resolve<T>(this T nav, string reference, Func<string, T> externalResolver = null) where T : class, IElementNavigator
        {
            var node = nav.Annotation<ScopedNode>() ?? (ITypedElement)nav.Annotation<TypedElement>();

            Func<string, ITypedElement> convertedExternalResolver = null;
            if (externalResolver != null)
                convertedExternalResolver  = (s) => externalResolver(s).Annotation<ScopedNode>() ?? nav.ToTypedElement();
            return (T)node.Resolve(reference, convertedExternalResolver)?.ToElementNavigator();
        }

        /// <summary>
        /// Where this item is a reference, resolve it to an actual resource, and return that
        /// </summary>
        /// <param name="nav"></param>
        /// <param name="externalResolver"></param>
        /// <returns></returns>
        [Obsolete("Use Resolve<T>(this T element, Func<string, T> externalResolver = null) where T : class, ITypedElement instead ")]
        public static T Resolve<T>(this T nav, Func<string, T> externalResolver = null) where T : class, IElementNavigator
        {
            var node = nav.Annotation<ScopedNode>() ?? (ITypedElement)nav.ToTypedElement();
            Func<string, ITypedElement> convertedExternalResolver = null;
            if (externalResolver != null)
                convertedExternalResolver = (s) => externalResolver(s).Annotation<ScopedNode>() ?? nav.ToTypedElement();
            return (T)node.Resolve(convertedExternalResolver)?.ToElementNavigator();
        }
    }
}
