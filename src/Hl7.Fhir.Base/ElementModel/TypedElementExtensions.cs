/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using EM=Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementExtensions
    {
        /// <summary>
        /// Converts a Poco to an ITypedElement.
        /// </summary>
        /// <param name="base">The Poco that should be converted to an <see cref="ITypedElement"/>.</param>
        /// <param name="modelInspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static ITypedElement ToTypedElementLegacy(this Base @base, ModelInspector modelInspector, string? rootName = null)
            => new PocoElementNode(modelInspector, @base, rootName: rootName);
        

        /// <summary>
        /// Creates an adapter which implements ITypedElement on top of a POCO instance, with explicit version-specific metadata.
        /// </summary>
        /// <param name="base">The POCO instance</param>
        /// <param name="inspector">The ModelInspector instance supplying version-specific metadata for the instance</param>
        /// <param name="rootName">The name you wish to have at the root of the tree. This will determine e.g. the root element name for serialization.
        /// If none is given, the type of the underlying poco will be used.</param>
        /// <remarks>The implementation of this method has changed. If you notice regressions, please let the SDK team know.
        /// In the meantime, you can restore the old behaviour with a call to <see cref="ToTypedElementLegacy"/></remarks>
#if NETSTANDARD2_1
        [Obsolete("The implementation of this method has changed to use our new model stack. If you want to try the new behaviour, "+
                  "either ignore this warning or call ToPocoNode(). For reverting to the old behaviour, call .ToTypedElementLegacy()")]
#else
        [Experimental("SDK0001")]
#endif
        public static ITypedElement ToTypedElement(this Base @base, ModelInspector inspector, string? rootName = null) =>
            @base.ToPocoNode(inspector, rootName);

        /// <summary>
        /// Converts a Poco to a new PocoElementNode.
        /// </summary>
        /// <param name="base">The Poco that should be converted to an <see cref="ITypedElement"/>.</param>
        /// <param name="inspector">An optional <see cref="ModelInspector"/> that should be used to access metadata about the resource.</param>
        /// <param name="rootName">An optional nome for the node at the root of the tree.</param>
        public static PocoNode ToPocoNode(this Base @base, ModelInspector? inspector = null, string? rootName = null)
        {
            var result = PocoNodeOrList.Root(@base, rootName);
            if(inspector is not null)
                ((IAnnotatable)result).AddAnnotation(inspector);

            return result;
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
}
#nullable restore