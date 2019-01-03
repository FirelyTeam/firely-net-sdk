/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionSchemaWalker
    {
        public readonly IResourceResolver Resolver;

        public readonly ElementDefinitionNavigator Current;

        public StructureDefinitionSchemaWalker(StructureDefinition sd, IResourceResolver resolver) 
            : this(ElementDefinitionNavigator.ForSnapshot(sd), resolver)
        {
            // nothing more
        }

        public StructureDefinitionSchemaWalker(ElementDefinitionNavigator element, IResourceResolver resolver)
        {
            Current = element.ShallowCopy();
            Resolver = resolver;

            // Make sure there is always a current item
            if (Current.AtRoot) Current.MoveToFirstChild();
        }

        public StructureDefinitionSchemaWalker(StructureDefinitionSchemaWalker other)
        {
            Current = other.Current.ShallowCopy();
            Resolver = other.Resolver;
        }

        public StructureDefinitionSchemaWalker FromCanonical(string canonical)
        {
            try
            {
                var sd = Resolver.FindStructureDefinition(canonical, requireSnapshot: true);
                return new StructureDefinitionSchemaWalker(ElementDefinitionNavigator.ForSnapshot(sd), Resolver);
            }
            catch (Exception e)
            {
                throw new StructureDefinitionSchemaWalkerException($"Cannot create a walker for StructureDefinition with canonical '{canonical}' at '{Current.UrlAndPath()}'", e);
            }
        }


        /// <summary>
        /// Returns a new walker that represents the definition for the given child.
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when there is no childName given.</exception>
        public StructureDefinitionSchemaWalker Child(string childName)
        {
            if (childName == null) throw Error.ArgumentNull(nameof(childName));

            var canonicals = Current.Current.Type.Select(t => t.Canonical()).Distinct().ToArray();
            if (canonicals.Length > 1)
                throw new StructureDefinitionSchemaWalkerException($"Cannot determine which child to select, since there are multiple paths leading from here ('{Current.UrlAndPath()}'), use 'ofType()' to disambiguate");

            var expanded = Expand();
            // Take First(), since all canonicals are the same anyway.
            var definitions = childDefinitions(expanded.First(), childName).Take(2).ToList();

            if (definitions.Count == 1)
                return new StructureDefinitionSchemaWalker(definitions.Single(), Resolver);
            else if (definitions.Count > 1)
                throw new InvalidOperationException($"Internal error: childDefinitions() produced more than one child with name '{childName} at '{Current.UrlAndPath()}' ");
            else
                throw new StructureDefinitionSchemaWalkerException($"Cannot walk into unknown child '{childName}' at '{Current.UrlAndPath()}'.");
        }

        private static IEnumerable<ElementDefinitionNavigator> childDefinitions(StructureDefinitionSchemaWalker walker, string childName = null)
        {
            var nav = walker.Current.ShallowCopy();
            var lastName = "";

            if (!nav.MoveToFirstChild()) yield break;

            do
            {
                if (nav.PathName == lastName) continue;    // ignore slices
                if (nav.Current.IsPrimitiveValueConstraint()) continue;      // ignore value attribute
                lastName = nav.PathName;

                if (childName != null && nav.Current.MatchesName(childName)) yield return nav.ShallowCopy();
            }
            while (nav.MoveToNext());
        }


        /// <summary>
        /// Returns a set of walkers containing the children of the current node
        /// </summary>
        /// <returns></returns>
        /// <remarks>There are three cases:
        /// 1. If the walker contains an ElementDefinition with children, it returns itself. 
        /// 2. If the ElementDefinition has a NameReference, it returns the node referred to by the namereference.
        /// 3. If not 1 or 2, it returns a set of walkers representing the types the ElementDefinition refers to.</remarks>
        public IEnumerable<StructureDefinitionSchemaWalker> Expand()
        {
            if (Current.HasChildren)
                return new[] { this };
            else if (Current.Current.NameReference != null)
            {
                var name = Current.Current.NameReference;
                var reference = Current.ShallowCopy();

                if (!reference.JumpToNameReference(name))
                    new StructureDefinitionSchemaWalkerException($"Found a namereference '{name}' that cannot be resolved at '{Current.UrlAndPath()}'.");
                return new[] { new StructureDefinitionSchemaWalker(reference, Resolver) };
            }
            else if(Current.Current.Type.Count >= 1)
            {
                return Current.Current.Type
                    .Select(t => t.Canonical())
                    .Select(c => FromCanonical(c));
            }

            throw new StructureDefinitionSchemaWalkerException($"Invalid StructureDefinition: element misses either a type reference or nameReference at '{Current.UrlAndPath()}'");
        }

        /// <summary>
        /// In an ElementDefinition that possibly is a choice, returns a set of walkers representing the
        /// constraints for only the <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>This is basically an Expand(), filtering by the given type. Note that this function might return
        /// multiple walkers if there are multiple different constraints for the same type (typerefs with the same
        /// type code, but multiple different profiles.</remarks>
        public IEnumerable<StructureDefinitionSchemaWalker> OfType(string type)
        {
            var expanded = Expand();

            return expanded.Where(w => typeCanonical(w.Current)?.GetLiteral() == type);

            // Determining the canonical for the type is a bit tricky, but
            // basically there are only three possibilities after expansion of the
            // types into their definitions:
            // 1) The root node of an SD for a type or constraint on the type => derive the base type
            // 2) At a non-root BackboneElement or Element => use the TypeRef.Type (After the expand, this can be only 1)
            FHIRDefinedType? typeCanonical(ElementDefinitionNavigator nav) =>
               nav.Current.IsRootElement() ? 
                    nav.StructureDefinition.BaseType()
                    : nav.Current.Type.FirstOrDefault()?.Code;
        }

        /// <summary>
        /// Walks across a reference, if the current node is a reference.
        /// </summary>
        /// <returns></returns>
        /// <remarks>May return multiple walkers, one for each of the references encountered.</remarks>
        /// <exception cref="StructureDefinitionSchemaWalkerException">Thrown when the ElementDefinition's type is not a Reference.</exception>
        public IEnumerable<StructureDefinitionSchemaWalker> Resolve()
        {
            if (!Current.Current.Type.Any(t => t.IsReference()))
                throw new StructureDefinitionSchemaWalkerException("resolve() should only be called on elements of type Reference at '{Current.UrlAndPath()}'.");

            return Current.Current.Type
                    .Where(t => t.IsReference())
                    .Select(t => t.TargetCanonical())
                    .Select(c => FromCanonical(c));
        }

        public StructureDefinitionSchemaWalker Extension(string url) => FromCanonical(url);
    };


    public static class StructureDefinitionSchemaWalkerEnumerables
    {
        public static IEnumerable<StructureDefinitionSchemaWalker> Child(this IEnumerable<StructureDefinitionSchemaWalker> me, string childName) =>
            me.Select(w => w.Child(childName));

        public static IEnumerable<StructureDefinitionSchemaWalker> Expand(this IEnumerable<StructureDefinitionSchemaWalker> me) =>
            me.SelectMany(w => w.Expand());

        public static IEnumerable<StructureDefinitionSchemaWalker> OfType(this IEnumerable<StructureDefinitionSchemaWalker> me, string canonical) =>
            me.SelectMany(w => w.OfType(canonical));

        public static IEnumerable<StructureDefinitionSchemaWalker> Resolve(this IEnumerable<StructureDefinitionSchemaWalker> me) =>
            me.SelectMany(w => w.Resolve());
        public static IEnumerable<StructureDefinitionSchemaWalker> Extension(this IEnumerable<StructureDefinitionSchemaWalker> me, string canonical) =>
            me.Select(w => w.Extension(canonical));
    }
}
