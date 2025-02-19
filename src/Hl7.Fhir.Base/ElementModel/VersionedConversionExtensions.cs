/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using EM=Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

public static partial class TypedElementExtensions
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

        public static void VisitAll(this ITypedElement nav) => nav.Visit((_, n) =>
        {
            var dummyValue = n.Value;
            var dummyDefinition = n.Definition;
        });

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
    
    /// <summary>
    /// Determines whether the specified ITypedElement is equal to the current ITypedElement. You can discard the order of the elements
    /// by setting the <paramref name="ignoreOrder"/> to <c>true</c>.
    /// </summary>
    /// <param name="left">The current <see cref="ITypedElement"/> to use in the equation.</param>
    /// <param name="right">The <see cref="ITypedElement"/> to compare with the current ITyoedElement.</param>
    /// <param name="ignoreOrder">When <c>true</c> the order of the children is discarded. When <c>false</c> the order of children is part
    /// of the equation.</param>
    /// <returns><c>true</c> when the ITypedElements are equal, <c>false</c> otherwise.</returns>
#pragma warning disable CS0618 // Type or member is obsolete
    public static bool IsExactlyEqualTo(this ITypedElement? left, ITypedElement? right, bool ignoreOrder = false)
#pragma warning restore CS0618 // Type or member is obsolete
    {
        if (left == null && right == null) return true;
        if (left == null || right == null) return false;

        if (!ValueEquality(left.Value, right.Value)) return false;

        // Compare the children.
        var childrenL = left.Children();
        var childrenR = right.Children();

        if (childrenL.Count() != childrenR.Count())
            return false;

        if (ignoreOrder)
        {
            childrenL = childrenL.OrderBy(x => x.Name).ToList();
            childrenR = childrenR.OrderBy(x => x.Name).ToList();
        }

        return childrenL.Zip(childrenR,
            (childL, childR) => childL.Name == childR.Name && childL.IsExactlyEqualTo(childR, ignoreOrder)).All(t => t);
    }

    /// <summary>
    /// Determines whether the generic values <paramref name="val1"/> and <paramref name="val2"/> are equal.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="val1"></param>
    /// <param name="val2"></param>
    /// <returns></returns>
    public static bool ValueEquality<T1, T2>(T1? val1, T2? val2)
    {
        // Compare the value
        if (val1 is null && val2 is null) return true;
        if (val1 is null || val2 is null) return false;

        try
        {
            if (EM.Any.TryConvert(val1, out var lAny) && EM.Any.TryConvert(val2, out var rAny))
            {
                return lAny is EM.ICqlEquatable cqle && cqle.IsEqualTo(rAny!) == true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether a <see cref="ITypedElement"/> matches a certain pattern.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pattern"></param>
    /// <returns><c>true</c> when <paramref name="value"/> matches the <paramref name="pattern"/>, <c>false</c> otherwise.</returns>
#pragma warning disable CS0618 // Type or member is obsolete
    public static bool Matches(this ITypedElement value, ITypedElement pattern)
#pragma warning restore CS0618 // Type or member is obsolete
    {
        if (value == null && pattern == null) return true;
        if (value == null || pattern == null) return false;

        if (!ValueEquality(value.Value, pattern.Value)) return false;

        // Compare the children.
        var valueChildren = value.Children();
        var patternChildren = pattern.Children();

        return patternChildren.All(patternChild => valueChildren.Any(valueChild =>
            patternChild.Name == valueChild.Name && valueChild.Matches(patternChild)));

    }
}
#nullable restore