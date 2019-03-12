/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath
{
    internal static class IValueProviderListExtensions
    {
        public static IEnumerable<ITypedElement> JustElements(this IEnumerable<ITypedElement> focus)
        {
            // todo: this is a tautology now --mh
            return focus.OfType<ITypedElement>();
        }

        public static IEnumerable<ITypedElement> Children(this IEnumerable<ITypedElement> focus)
        {
            // todo: this is now a tautology --mh
            // return focus.JustElements().SelectMany(node => node.Children());
            return focus.SelectMany(node => node.Children());
        }

        public static IEnumerable<ITypedElement> Descendants(this IEnumerable<ITypedElement> focus)
        {
            return focus.JustElements().SelectMany(node => node.Descendants());
        }
    }
}


