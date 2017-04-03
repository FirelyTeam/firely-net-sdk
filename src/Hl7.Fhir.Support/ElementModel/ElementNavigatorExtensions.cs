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
            return navigators.SelectMany(n => n.Children());
        }

        public static IEnumerable<IElementNavigator> Children(this IElementNavigator navigator, string name)
        {
            return navigator.Children().Where(c => c.Name == name);
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
            return navigators.Children().Any();
        }

        public static IEnumerable<IElementNavigator> Descendants(this IElementNavigator navigator)
        {
            //TODO: Don't think this is performant with these nested yields
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