using System.Linq;
using System.Collections.Generic;
using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel.Adapters;

namespace Hl7.Fhir.ElementModel
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

        public static bool InPipeline(this ISourceNavigator navigator, Type componentType) =>
                    navigator is IAnnotated ia ? ia.Annotation(componentType) != null : false;

        public static bool InPipeline<T>(this ISourceNavigator navigator) =>
                    navigator.InPipeline(navigator.GetType());

        public static ElementDefinitionSummary GetElementDefinitionSummary(this ISourceNavigator navigator) =>
                navigator is IAnnotated ia ? ia.GetElementDefinitionSummary() : null;

        public static List<ExceptionNotification> VisitAndCatch(this ISourceNavigator nav)
        {
            var errors = new List<ExceptionNotification>();

            using (nav.Catch((o, arg) => errors.Add(arg)))
            {
                nav.Visit(n => { var dummy = n.Text; });
            }

            return errors;
        }

        public static string GetResourceType(this ISourceNavigator navigator) => 
            navigator is IAnnotated ia ? ia.GetResourceType() : null;

        public static IElementNavigator ToElementNavigator(this ISourceNavigator sourceNav, IStructureDefinitionSummaryProvider provider, string type = null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            return new TypedNavigator(sourceNav, type, provider);
        }

        [Obsolete("WARNING! For internal API use only. Turning an untyped SourceNavigator into a typed ElementNavigator without providing" +
            "type information (see other overload) will cause side-effects with components in the API that are not prepared to deal with" +
            "missing type information. Please don't use this overload unless you know what you are doing.")]
        public static IElementNavigator ToElementNavigator(this ISourceNavigator sourceNav, string type = null) =>
            new SourceNavToElementNavAdapter(sourceNav);
    }
}