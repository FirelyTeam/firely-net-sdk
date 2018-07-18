using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.ElementModel
{

    public static class ElementNavigatorExtensions
    {
        public static IEnumerable<T> Children<T>(this T navigator, string name = null) where T:INavigator
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild(name))
            {
                do
                {
                    yield return (T)nav.Clone();
                }
                while (nav.MoveToNext(name));
            }
        }


        public static IEnumerable<T> Children<T>(this IEnumerable<T> navigators, string name = null) where T:INavigator
        {
            return navigators.SelectMany(n => n.Children(name)).Cast<T>();
        }

        public static bool HasChildren(this IEnumerable<INavigator> navigators, string name = null)
        {
            return navigators.Children(name).Any();
        }

        public static bool HasChildren(this INavigator navigator, string name = null)
        {
            return navigator.Children(name).Any();
        }

        public static IEnumerable<T> Descendants<T>(this T navigator) where T:INavigator
        {
            foreach (var child in navigator.Children().Cast<T>())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

        public static IEnumerable<T> DescendantsAndSelf<T>(this T navigator) where T:INavigator
        {
            return (new[] { (T)navigator }).Concat(navigator.Descendants().Cast<T>());
        }

        public static IEnumerable<T> DescendantsAndSelf<T>(this IEnumerable<T> navigators) where T:INavigator
        {
            return navigators.SelectMany(n => n.DescendantsAndSelf().Cast<T>());
        }

        public static void Visit<T>(this T navigator, Action<T> visitor) where T:INavigator
        {
            visitor(navigator);
            foreach (var child in navigator.Children())
            {
                Visit(child, visitor);
            }
        }
    }
}