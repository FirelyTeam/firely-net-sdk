﻿/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class ElementNodeExtensions
    {
        public static IEnumerable<ITypedElement> Children(this IEnumerable<ITypedElement> nodes, string? name = null) =>
            nodes.SelectMany(n => n.Children(name));

        public static IEnumerable<ITypedElement> Descendants(this ITypedElement element)
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

        public static IEnumerable<ITypedElement> Descendants(this IEnumerable<ITypedElement> elements) =>
            elements.SelectMany(e => e.Descendants());


        public static IEnumerable<ITypedElement> DescendantsAndSelf(this ITypedElement element) =>
            (new[] { element }).Concat(element.Descendants());

        public static IEnumerable<ITypedElement> DescendantsAndSelf(this IEnumerable<ITypedElement> elements) =>
            elements.SelectMany(e => e.DescendantsAndSelf());

        public static void Visit(this ITypedElement root, Action<int, ITypedElement> visitor) => root.visit(visitor, 0);

        private static void visit(this ITypedElement root, Action<int, ITypedElement> visitor, int depth = 0)
        {
            visitor(depth, root);

            foreach (var child in root.Children())
            {
                visit(child, visitor, depth + 1);
            }
        }

        public static IDisposable Catch(this ITypedElement source, ExceptionNotificationHandler handler) =>
            source is IExceptionSource s ? s.Catch(handler) : throw new NotImplementedException("Element does not implement IExceptionSource.");

        public static void VisitAll(this ITypedElement nav) => nav.Visit((_, n) => { var dummy = n.Value; });

        public static List<ExceptionNotification> VisitAndCatch(this ITypedElement node)
        {
            var errors = new List<ExceptionNotification>();

            using (node.Catch((o, arg) => errors.Add(arg)))
            {
                node.VisitAll();
            }

            return errors;
        }



        public static IEnumerable<object> Annotations(this ITypedElement nav, Type type) =>
        nav is IAnnotated ann ? ann.Annotations(type) : Enumerable.Empty<object>();
        public static T? Annotation<T>(this ITypedElement nav) =>
            nav is IAnnotated ann ? ann.Annotation<T>() : default;

        public static ISourceNode ToSourceNode(this ITypedElement node) => new TypedElementToSourceNodeAdapter(node);

        public static IReadOnlyCollection<IElementDefinitionSummary> ChildDefinitions(this ITypedElement me,
            IStructureDefinitionSummaryProvider provider)
        {
            if (me.Definition != null)
            {
                // If this is a backbone element, the child type is the nested complex type
                if (me.Definition.Type[0] is IStructureDefinitionSummary be)
                    return be.GetElements();
                else
                {
                    if (me.InstanceType != null)
                    {
                        var si = provider.Provide(me.InstanceType);
                        if (si != null) return si.GetElements();
                    }
                }

            }

            // Note: fall-through in all failure cases - return empty collection
            return new List<IElementDefinitionSummary>();
        }

        public static ScopedNode ToScopedNode(this ITypedElement node) =>
            node as ScopedNode ?? new ScopedNode(node);

        /// <summary>
        /// Convert a <see cref="ITypedElement"/> to a <see cref="IScopedNode"/>.
        /// </summary>
        /// <param name="node">An <see cref="ITypedElement"/></param>
        /// <returns></returns>
        internal static IScopedNode AsScopedNode(this ITypedElement node) => ToScopedNode(node);
    }
}

#nullable restore