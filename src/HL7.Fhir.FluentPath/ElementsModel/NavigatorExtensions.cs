using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.FluentPath
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

        public static IEnumerable<object> Values<T>(this T navigator) where T : INavigator<T>, IValueProvider
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav.Value;
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<object> Values<T>(this T navigator, Predicate<T> predicate) where T : INavigator<T>, IValueProvider
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    if (predicate(nav)) yield return nav.Value;
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<object> Values<T>(this T navigator, string name) where T : INavigator<T>, INameProvider, IValueProvider
        {
            return navigator.Values(n => n.Name == name);
        }

        public static IEnumerable<object> ChildrenValues<T>(this IEnumerable<T> navigators, string name) where T: INavigator<T>, INameProvider, IValueProvider
        {
            return navigators.SelectMany(n => n.Values(name));
        }

        public static IEnumerable<string> GetChildNames<T>(this T navigator)where T : INavigator<T>, INameProvider
        {
            return navigator.Children().Select(c => c.Name).Distinct();
        }

        public static IEnumerable<T> GetChildrenByName<T>(this T navigator, string name)where T : INavigator<T>, INameProvider
        {
            return navigator.Children().Where(c => c.Name == name);
        }

        public static IEnumerable<T> GetChildrenByName<T>(this IEnumerable<T> navigators, string name) where T : INavigator<T>, INameProvider
        {
            return navigators.SelectMany(n => n.Children().Where(c => c.Name == name));
        }


        public static IEnumerable<T> Descendants<T>(this T element) where T: INavigator<T>
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