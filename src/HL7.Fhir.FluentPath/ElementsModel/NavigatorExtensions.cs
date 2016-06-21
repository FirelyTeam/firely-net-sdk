using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.FluentPath
{
    public static class NavigatorExtensions
    {
        public static IEnumerable<T> EnumerateChildren<T>(this T navigator) where T : INavigator<T>
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav;
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<string> GetChildNames<T>(this T navigator)where T : INavigator<T>, INameProvider
        {
            return navigator.EnumerateChildren().Select(c => c.Name).Distinct();
        }

        public static IEnumerable<T> GetChildrenByName<T>(this T navigator, string name)where T : INavigator<T>, INameProvider
        {
            return navigator.EnumerateChildren().Where(c => c.Name == name);
        }

        public static IEnumerable<T> Descendants<T>(this T element) where T: INavigator<T>
        {
            //TODO: Don't think this is performant with these nested yields
            foreach (var child in element.EnumerateChildren())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

    }
}