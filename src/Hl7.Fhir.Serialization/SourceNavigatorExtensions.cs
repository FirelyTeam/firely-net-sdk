using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{

    public static class SourceNavigatorExtensions
    {
        public static IEnumerable<ISourceNavigator> Children(this ISourceNavigator navigator, string name = null)
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


        public static IEnumerable<ISourceNavigator> Children(this IEnumerable<ISourceNavigator> navigators, string name = null) =>
            navigators.SelectMany(n => n.Children(name));


        //Since moving to the first child can be very expensive on stacked navigators, we should not offer this
        //functionality, unless by adding it to ISourceNavigator
        //public static bool HasChildren(this IEnumerable<ISourceNavigator> navigators, string name = null) => navigators.Children(name).Any();
        //public static bool HasChildren(this ISourceNavigator navigator, string name = null) => navigator.Children(name).Any();

        public static IEnumerable<ISourceNavigator> Descendants(this ISourceNavigator navigator)
        {
            foreach (var child in navigator.Children())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

        public static IEnumerable<ISourceNavigator> DescendantsAndSelf(this ISourceNavigator navigator)
        {
            return (new[] { navigator }).Concat(navigator.Descendants());
        }

        public static IEnumerable<ISourceNavigator> DescendantsAndSelf(this IEnumerable<ISourceNavigator> navigators)
        {
            return navigators.SelectMany(n => n.DescendantsAndSelf());
        }

        public static void Visit(this ISourceNavigator navigator, Action<ISourceNavigator> visitor)
        {
            visitor(navigator);
            foreach (var child in navigator.Children())
            {
                Visit(child, visitor);
            }
        }

        private static void visit(this ISourceNavigator navigator, Action<int, ISourceNavigator> visitor, int depth = 0)
        {
            visitor(depth, navigator);
            foreach (var child in navigator.Children())
            {
                visit(child, visitor, depth + 1);
            }
        }

        public static void Visit(this ISourceNavigator navigator, Action<int, ISourceNavigator> visitor) => navigator.visit(visitor, 0);

        public static IDisposable Catch(this ISourceNavigator source, ExceptionNotificationHandler handler) =>
            source is IExceptionSource s ? s.Catch(handler) : throw new NotImplementedException("source does not implement IExceptionSource");

        public static IEnumerable<object> Annotations(this ISourceNavigator nav, Type type) =>
            nav is IAnnotated ann ? ann.Annotations(type) : Enumerable.Empty<object>();

        internal static IElementNavigator AsElementNavigator(this ISourceNavigator sourceNav, string type=null, ISerializationInfoProvider provider=null)
        {
            IElementNavigator typedNav;

            if (provider != null)
            {
                typedNav = type == null ? new TypedNavigator(sourceNav, provider) :
                                new TypedNavigator(sourceNav, type, provider);
            }
            else
                typedNav = new TypedShimNavigator(sourceNav);

            // This is normally true
            if (sourceNav is IExceptionSource esrc && typedNav is IExceptionSink esnk && typedNav is IExceptionSource tsrc)
            {
                // Insert the newly created typed navigator in the IExceptionSource/Sink chain
                tsrc.Sink = esrc.Sink;
                esrc.Sink = esnk;
            }

            return typedNav;
        }
    }
}