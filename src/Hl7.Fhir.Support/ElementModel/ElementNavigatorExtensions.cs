using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.ElementModel
{

    public static class ElementNavigatorExtensions
    {
        public static IEnumerable<IElementNavigator> Children(this IElementNavigator navigator)
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

        public static IEnumerable<IElementNavigator> Children(this IEnumerable<IElementNavigator> navigators)
        {
            // use a standard enumerator approach
            // this will then only grab 1 value if
            // that is all the caller requires.
            foreach (var navigator in navigators)
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
        }

        public static IEnumerable<IElementNavigator> Children(this IElementNavigator navigator, string name)
        {
            // use a standard enumerator approach
            // this will then only grab 1 value if
            // that is all the caller requires.
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild(name))
            {
                do
                {
                    // Some safety checking that the 
                    // nameFilter parameter has been accurately
                    // applied to the navigator
                    if (nav?.Name == name)
                        yield return nav.Clone();
                    else
                        throw new InvalidOperationException("Found an unexpected item in the navigator");
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<IElementNavigator> Children(this IEnumerable<IElementNavigator> navigators, string name)
        {
            return navigators.SelectMany(n => n.Children(name));
        }

        public static bool HasChildren(this IElementNavigator navigator)
        {
            var nav = navigator.Clone();
            return nav.MoveToFirstChild();
        }

        public static bool HasChildren(this IEnumerable<IElementNavigator> navigators)
        {
            // if any of the navigators have children
            // its true! (no need to expand the children)
            foreach (var nav in navigators)
            {
                if (nav.HasChildren())
                    return true;
            }
            return false;
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