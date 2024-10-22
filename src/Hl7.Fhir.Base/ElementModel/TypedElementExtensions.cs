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
using System;
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
        [TemporarilyChanged]
        public static ITypedElement ToTypedElement(this Base @base, ModelInspector modelInspector, string? rootName = null)
            => new PocoElementNode(modelInspector, @base, rootName: rootName);

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