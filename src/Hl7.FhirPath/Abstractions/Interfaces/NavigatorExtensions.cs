/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;

namespace Hl7.ElementModel
{
    public static class NavigatorExtensions
    {
        public static IEnumerable<T> Children<T>(this T navigator) where T : INavigator<T>
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav.Clone();
                }
                while (nav.MoveToNext());
            }
        }

        public static bool HasChildren<T>(this T navigator) where T : INavigator<T>
        {
            var nav = navigator.Clone();

            return nav.MoveToFirstChild();
        }

        public static IEnumerable<T> Descendants<T>(this T element) where T : INavigator<T>
        {
            //TODO: Don't think this is performant with these nested yields
            foreach (var child in element.Children())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

    }
}