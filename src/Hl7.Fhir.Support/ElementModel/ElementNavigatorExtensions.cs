using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.ElementModel
{

    public static class ElementNavigatorExtensions
    {
        public static IEnumerable<IElementNavigator> Children(this IElementNavigator navigator, string name = null)
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild(name))
            {
                do
                {
                    yield return nav.Clone();
                }
                while (nav.MoveToNext(name));
            }
        }


        public static IEnumerable<IElementNavigator> Children(this IEnumerable<IElementNavigator> navigators, string name = null)
        {
            return navigators.SelectMany(n => n.Children(name));

            // [20170524 EK] This is unneccessary, since this is exactly what SelectMany() does, so this just results
            // in duplication of code inside the foreach() here.

            // use a standard enumerator approach
            // this will then only grab 1 value if
            // that is all the caller requires.
            //foreach (var navigator in navigators)
            //{
            //    var nav = navigator.Clone();
            //    if (nav.MoveToFirstChild())
            //    {
            //        do
            //        {
            //            yield return nav.Clone();
            //        }
            //        while (nav.MoveToNext());
            //    }
            //}
        }

        public static bool HasChildren(this IEnumerable<IElementNavigator> navigators, string name = null)
        {
            return navigators.Children(name).Any();

            // [20170524 EK] This is unneccessary, since this is exactly what Any() does, so this just results
            // in duplication of code inside the foreach() here.

            // if any of the navigators have children
            // its true! (no need to expand the children)
            //foreach (var nav in navigators)
            //{
            //    if (nav.HasChildren())
            //        return true;
            //}
            //return false;
        }

        public static bool HasChildren(this IElementNavigator navigator, string name = null)
        {
            return navigator.Children(name).Any();
        }

        public static IEnumerable<IElementNavigator> Descendants(this IElementNavigator navigator)
        {
            foreach (var child in navigator.Children())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

        public static IEnumerable<IElementNavigator> DescendantsAndSelf(this IElementNavigator navigator)
        {
            return (new[] { navigator }).Concat(navigator.Descendants());
        }

        public static IEnumerable<IElementNavigator> DescendantsAndSelf(this IEnumerable<IElementNavigator> navigators)
        {
            return navigators.SelectMany(n => n.DescendantsAndSelf());
        }

        public static void Visit(this IElementNavigator navigator, Action<IElementNavigator> visitor)
        {
            visitor(navigator);
            foreach (var child in navigator.Children())
            {
                Visit(child, visitor);
            }
        }

        private static void visit(this IElementNavigator navigator, Action<int, IElementNavigator> visitor, int depth = 0)
        {
            visitor(depth, navigator);
            foreach (var child in navigator.Children())
            {
                visit(child, visitor, depth + 1);
            }
        }

        public static void Visit(this IElementNavigator navigator, Action<int, IElementNavigator> visitor) => navigator.visit(visitor, 0);
    }
}