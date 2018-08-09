using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class ElementNodeExtensions
    {
        public static IEnumerable<IElementNode> Children(this IEnumerable<IElementNode> nodes, string name = null) => 
            nodes.SelectMany(n => n.Children(name));

        public static IEnumerable<IElementNode> Descendants(this IElementNode element)
        {
            foreach (var child in element.Children())
            {
                yield return child;

                foreach (var grandchild in child.Descendants())
                {
                    yield return grandchild;
                }
            }
        }

        public static IEnumerable<IElementNode> Descendants(this IEnumerable<IElementNode> elements) => 
            elements.SelectMany(e => e.Descendants());


        public static IEnumerable<IElementNode> DescendantsAndSelf(this IElementNode element) => 
            (new[] { element }).Concat(element.Descendants());

        public static IEnumerable<IElementNode> DescendantsAndSelf(this IEnumerable<IElementNode> elements) => 
            elements.SelectMany(e => e.DescendantsAndSelf());


        //public static IEnumerable<IElementNode> Ancestors(this IElementNode node)
        //{
        //    var scan = node.Parent;

        //    while(scan != null)
        //    {
        //        yield return scan;
        //        scan = scan.Parent;
        //    }
        //}

        //public static IEnumerable<IElementNode> Ancestors(this IEnumerable<IElementNode> nodes)
        //{
        //    // all the parents of the given lists of nodes. Does not include the nodes, except if one is a parent of another.
        //    return nodes.SelectMany(e => e.Ancestors()).Distinct();
        //}

        [Obsolete("This method can be prohibitively expensive, and should not be used anymore.")]
        public static bool HasChildren(this IElementNode node) => node.Children().Any();

        [Obsolete("This method can be prohibitively expensive, and should not be used anymore.")]
        public static bool HasChildren(this IEnumerable<IElementNode> nodes) => nodes.Children().Any();


        private static void visit(this IElementNode node, Action<int, IElementNode> visitor, int depth = 0)
        {
            visitor(depth, node);

            foreach (var child in node.Children())
            {
                visit(child, visitor, depth + 1);
            }
        }

        public static void Visit(this IElementNode node, Action<int, IElementNode> visitor) => node.visit(visitor, 0);

        public static bool InPipeline(this IElementNode node, Type componentType) =>
            node is IAnnotated ia ? ia.Annotation(componentType) != null : false;
        public static bool InPipeline<T>(this IElementNode node) =>
            node.InPipeline(node.GetType());

        public static IDisposable Catch(this IElementNode source, ExceptionNotificationHandler handler) =>
            source is IExceptionSource s ? s.Catch(handler) : throw new NotImplementedException("source does not implement IExceptionSource");


        public static List<ExceptionNotification> VisitAndCatch(this IElementNode node)
        {
            var errors = new List<ExceptionNotification>();

            using (node.Catch((o, arg) => errors.Add(arg)))
            {
                node.Visit((d,n) => { var dummy = n.Value; });
            }

            return errors;
        }

        public static IEnumerable<object> Annotations(this IElementNode nav, Type type) =>
        nav is IAnnotated ann ? ann.Annotations(type) : Enumerable.Empty<object>();
        public static T Annotation<T>(this IElementNode nav) where T : class =>
            nav is IAnnotated ann ? ann.Annotation<T>() : null;

        [Obsolete("IElementNavigator should be replaced by the IElementNode interface, which is returned by the parsers")]
        public static IElementNavigator ToElementNavigator(this IElementNode node) => new ElementNodeToElementNavAdapter(node);

        public static ISourceNode ToSourceNode(this IElementNode node) => new ElementNodeToSourceNodeAdapter(node);

        public static ElementDefinitionSummary GetElementDefinitionSummary(this IElementNode node) =>
            node is IAnnotated ia ? ia.GetElementDefinitionSummary() : null;

        public static string GetResourceType(this IElementNode navigator) =>
                navigator is IAnnotated ia ? ia.GetResourceType() : null;

    }
}
