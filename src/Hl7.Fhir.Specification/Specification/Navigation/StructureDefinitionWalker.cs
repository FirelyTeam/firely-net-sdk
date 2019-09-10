/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    /// <summary>
    /// This class implements basic functions to walk deeper into a StructureDefinition, 
    /// using a child name, disambiguating on type, navigating across references or directly
    /// to expected extensions. This functionality mirrors the potential navigation concepts
    /// on a discriminator, as documented in http://hl7.org/fhir/profiling.html#discriminator.
    /// </summary>
    public class StructureDefinitionWalker
    {
        public readonly IResourceResolver Resolver;

        public readonly ElementDefinitionNavigator Current;

        public StructureDefinitionWalker(StructureDefinition sd, IResourceResolver resolver)
            : this(ElementDefinitionNavigator.ForSnapshot(sd), resolver)
        {
            // nothing more
        }

        public StructureDefinitionWalker(ElementDefinitionNavigator element, IResourceResolver resolver)
        {
            Current = element.ShallowCopy();
            Resolver = resolver;

            // Make sure there is always a current item
            if (Current.AtRoot) Current.MoveToFirstChild();
        }

        public StructureDefinitionWalker(StructureDefinitionWalker other)
        {
            Current = other.Current.ShallowCopy();
            Resolver = other.Resolver;
        }

        public StructureDefinitionWalker FromCanonical(string canonical)
        {
            try
            {
                var sd = Resolver.FindStructureDefinition(canonical, requireSnapshot: true);
                return new StructureDefinitionWalker(ElementDefinitionNavigator.ForSnapshot(sd), Resolver);
            }
            catch (Exception e)
            {
                throw new StructureDefinitionWalkerException($"Cannot create a walker for StructureDefinition with canonical '{canonical}' at '{Current.CanonicalPath()}'", e);
            }
        }


        /// <summary>
        /// Returns a new walker that represents the definition for the given child.
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when there is no childName given.</exception>
        public StructureDefinitionWalker Child(string childName)
        {
            if (childName == null) throw Error.ArgumentNull(nameof(childName));

            var canonicals = Current.Current.Type.Select(t => t.GetTypeProfile()).Distinct().ToArray();
            if (canonicals.Length > 1)
                throw new StructureDefinitionWalkerException($"Cannot determine which child to select, since there are multiple paths leading from here ('{Current.CanonicalPath()}'), use 'ofType()' to disambiguate");

            var expanded = Expand();
            // Take First(), since all canonicals are the same anyway.
            var definitions = childDefinitions(expanded.First(), childName).ToList();

            if(definitions.Count == 0)
                throw new StructureDefinitionWalkerException($"Cannot walk into unknown child '{childName}' at '{Current.CanonicalPath()}'.");
            else if (definitions.Count == 1) // Single element, no slice
                return new StructureDefinitionWalker(definitions.Single(), Resolver);
            else if (definitions.Count == 2) // element with an entry + single slice
                return new StructureDefinitionWalker(definitions[1], Resolver);
            else
                throw new StructureDefinitionWalkerException($"Child with name '{childName}' is sliced to more than one choice and cannot be used as a discriminator at '{Current.CanonicalPath()}' ");
        }

        private static IEnumerable<ElementDefinitionNavigator> childDefinitions(StructureDefinitionWalker walker, string childName = null)
        {
            var nav = walker.Current.ShallowCopy();

            if (!nav.MoveToFirstChild()) yield break;

            do
            {
                if (nav.Current.IsPrimitiveValueConstraint()) continue;      // ignore value attribute
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
        public IEnumerable<StructureDefinitionWalker> Expand()
        {
            if (Current.HasChildren)
                return new[] { this };
            else if (Current.Current.ContentReference != null)
            {
                var name = Current.Current.ContentReference;
                var reference = Current.ShallowCopy();

                if (!reference.JumpToNameReference(name))
                    new StructureDefinitionWalkerException($"Found a namereference '{name}' that cannot be resolved at '{Current.CanonicalPath()}'.");
                return new[] { new StructureDefinitionWalker(reference, Resolver) };
            }
            else if (Current.Current.Type.Count >= 1)
            {
                return Current.Current.Type
                    .Select(t => t.GetTypeProfile())
                    .Select(c => FromCanonical(c));
            }

            throw new StructureDefinitionWalkerException($"Invalid StructureDefinition: element misses either a type reference or nameReference at '{Current.CanonicalPath()}'");
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
        public IEnumerable<StructureDefinitionWalker> OfType(string type)
        {
            var expanded = Expand();

            return expanded.Where(w => typeCanonical(w.Current) == type);

            // Determining the canonical for the type is a bit tricky, but
            // basically there are only two possibilities after expansion of the
            // types into their definitions:
            // 1) The root node of an SD for a type or constraint on the type => derive the base type
            // 2) At a non-root BackboneElement or Element => use the TypeRef.Type (After the expand, this can be only 1)
            string typeCanonical(ElementDefinitionNavigator nav) =>
               nav.Current.IsRootElement() ?
                    nav.StructureDefinition.Type
                    : nav.Current.Type.FirstOrDefault()?.Code;
        }

        /// <summary>
        /// Walks across a reference, if the current node is a reference.
        /// </summary>
        /// <returns></returns>
        /// <remarks>May return multiple walkers, one for each of the references encountered.</remarks>
        /// <exception cref="StructureDefinitionWalkerException">Thrown when the ElementDefinition's type is not a Reference.</exception>
        public IEnumerable<StructureDefinitionWalker> Resolve()
        {
            if (!Current.Current.Type.Any(t => t.IsReference()))
                throw new StructureDefinitionWalkerException("resolve() should only be called on elements of type Reference at '{Current.UrlAndPath()}'.");

            return Current.Current.Type
                    .Where(t => t.IsReference() && t.TargetProfile != null)
                    .Select(t => t.TargetProfile)
                    .Select(c => FromCanonical(c));
        }

        public StructureDefinitionWalker Extension(string url) => FromCanonical(url);
    };


    public static class StructureDefinitionSchemaWalkerEnumerables
    {
        public static IEnumerable<StructureDefinitionWalker> Child(this IEnumerable<StructureDefinitionWalker> me, string childName) =>
            me.Select(w => w.Child(childName));

        public static IEnumerable<StructureDefinitionWalker> Expand(this IEnumerable<StructureDefinitionWalker> me) =>
            me.SelectMany(w => w.Expand());

        public static IEnumerable<StructureDefinitionWalker> OfType(this IEnumerable<StructureDefinitionWalker> me, string canonical) =>
            me.SelectMany(w => w.OfType(canonical));

        public static IEnumerable<StructureDefinitionWalker> Resolve(this IEnumerable<StructureDefinitionWalker> me) =>
            me.SelectMany(w => w.Resolve());
        public static IEnumerable<StructureDefinitionWalker> Extension(this IEnumerable<StructureDefinitionWalker> me, string canonical) =>
            me.Select(w => w.Extension(canonical));
    }
}
