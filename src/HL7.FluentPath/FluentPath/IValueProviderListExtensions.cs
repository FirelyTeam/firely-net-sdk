/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.ElementModel;
using Hl7.FluentPath.Functions;

namespace Hl7.FluentPath
{
    internal static class IValueProviderListExtensions
    {
        public static IEnumerable<IElementNavigator> JustElements(this IEnumerable<IValueProvider> focus)
        {
            return focus.OfType<IElementNavigator>();
        }


        public static IEnumerable<IValueProvider> Children(this IEnumerable<IValueProvider> focus)
        {
            return focus.JustElements().SelectMany(node => node.Children());
        }

        public static IEnumerable<IValueProvider> Descendants(this IEnumerable<IValueProvider> focus)
        {
            return focus.JustElements().SelectMany(node => node.Descendants());
        }
 
    }
}


