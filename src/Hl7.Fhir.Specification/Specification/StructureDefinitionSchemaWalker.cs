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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionSchemaWalker
    {
        public readonly IResourceResolver Resolver;
        public Exception StuckReason { get; private set; }

        public readonly ElementDefinitionNavigator Current;

        /// <summary>
        /// If any of the walking operations (Child, Resolve, etc) failed, this is set to true.
        /// </summary>
        /// <remarks>When this is true, <c>Current</c> will be the last position the walker got stuck. Also, 
        /// <c>StuckReason</c> will be set to an Exception containing details of why the walker got stuck.</remarks>
        public bool WasStuck => StuckReason != null;

        public StructureDefinitionSchemaWalker(ElementDefinitionNavigator element, IResourceResolver resolver)
        {
            Current = element.ShallowCopy();
            Resolver = resolver;
            StuckReason = null;

            // Make sure there is always a current item
            if (Current.AtRoot) Current.MoveToFirstChild();
        }

        public StructureDefinitionSchemaWalker(StructureDefinitionSchemaWalker other)
        {
            Current = other.Current.ShallowCopy();
            Resolver = other.Resolver;
            StuckReason = other.StuckReason;
        }

        public StructureDefinitionSchemaWalker FromCanonical(string canonical)
        {
            if (WasStuck) return this;

            try
            {
                var sd = Resolver.FindStructureDefinition(canonical, requireSnapshot: true);
                return new StructureDefinitionSchemaWalker(ElementDefinitionNavigator.ForSnapshot(sd), Resolver);
            }
            catch (Exception e)
            {
                return Stuck(new StructureDefinitionSchemaWalkerException($"Cannot retrieve StructureDefinition with canonical '{canonical}' at '{Current.UrlAndPath()}'", e));
            }
        }

        public StructureDefinitionSchemaWalker Stuck(Exception reason) =>
            WasStuck ? (this) : new StructureDefinitionSchemaWalker(this) { StuckReason = reason };


        public IEnumerable<StructureDefinitionSchemaWalker> Child(string childName)
        {
            if (WasStuck) return single(this);

            var scan = Current.ShallowCopy();

            if (scan.MoveToFirstChild())
            {
                var results = new List<StructureDefinitionSchemaWalker>();
                do
                {
                    // Return the first matching child, for slices this would just be the original,
                    // unsliced element.
                    if (scan.Current.MatchesName(childName))
                        return single(new StructureDefinitionSchemaWalker(scan, Resolver));
                }
                while (scan.MoveToNext());

                return single(Stuck(
                        new StructureDefinitionSchemaWalkerException($"Cannot walk into unknown child '{childName}' at '{Current.UrlAndPath()}'.")));
            }
            else if (scan.Current.NameReference != null)
            {
                var name = scan.Current.NameReference;
                var reference = scan.ShallowCopy();

                if (!reference.JumpToNameReference(name))
                    single(Stuck(
                        new StructureDefinitionSchemaWalkerException($"Found a namereference '{name}' on that cannot be resolved at '{Current.UrlAndPath()}'.")));

                return single(new StructureDefinitionSchemaWalker(reference,Resolver));
            }
            else
            {
                return scan.Current.Type
                    .Select(t => t.Canonical())
                    .Select(c => FromCanonical(c))
                    .SelectMany(result => result.Child(childName));

                //StructureDefinitionWalker moveToChild((ElementDefinitionNavigator nav, Exception err) result) =>
                //    result.nav != null ? MoveToChild(result.nav, childName) : (new[] { result });

                // Stuck(new StructureDefinitionSchemaWalkerException($"There are multiple patchs leading from here ('{Current.Path}'), use 'ofType()' to disambiguate"));
            }
        }

        private IEnumerable<StructureDefinitionSchemaWalker> single(StructureDefinitionSchemaWalker w) => new[] { w };

        private IEnumerable<StructureDefinitionSchemaWalker> nothing() => Enumerable.Empty<StructureDefinitionSchemaWalker>();

        public IEnumerable<StructureDefinitionSchemaWalker> OfType(string canonical)
        {
            if (WasStuck) return single(this);

            var count = Current.Current.Type.Count;

            if (count == 1)
                return Current.Current.Type.Single().Canonical() == canonical ? single(this) : nothing();
            else if (count == 0)
                return Enumerable.Empty<StructureDefinitionSchemaWalker>();
            else
                return Current.Current.Type
                    .Select(t => t.Canonical())
                    .Select(c => FromCanonical(c))
                    .SelectMany(w => w.OfType(canonical));
        }

        public IEnumerable<StructureDefinitionSchemaWalker> Resolve()
        {
            if (WasStuck) return single(this);

            if (!Current.Current.Type.Any(t => t.IsReference()))
                return single(Stuck(new StructureDefinitionSchemaWalkerException("resolve() should only be called on elements of type Reference at '{Current.UrlAndPath()}'.")));

            return Current.Current.Type
                    .Where(t => t.IsReference())
                    .Select(t => t.TargetCanonical())
                    .Select(c => FromCanonical(c));
        }

        public IEnumerable<StructureDefinitionSchemaWalker> Extension(string url) =>
            single(FromCanonical(url));

        public IEnumerable<StructureDefinitionSchemaWalker> Slice(string name) =>
            Current.Current.SliceName() == name ? single(this) : nothing();
    };


    public static class StructureDefinitionSchemaWalkerEnumerables
    {
        public static IEnumerable<StructureDefinitionSchemaWalker> Child(this IEnumerable<StructureDefinitionSchemaWalker> me, string childName) =>
            me.SelectMany(w => w.Child(childName));

        public static IEnumerable<StructureDefinitionSchemaWalker> WithCanonical(this IEnumerable<StructureDefinitionSchemaWalker> me, string canonical) =>
            me.SelectMany(w => w.OfType(canonical));

        public static IEnumerable<StructureDefinitionSchemaWalker> Resolve(this IEnumerable<StructureDefinitionSchemaWalker> me) =>
            me.SelectMany(w => w.Resolve());
        public static IEnumerable<StructureDefinitionSchemaWalker> Extension(this IEnumerable<StructureDefinitionSchemaWalker> me, string canonical) =>
            me.SelectMany(w => w.Extension(canonical));

        public static IEnumerable<StructureDefinitionSchemaWalker> Slice(this IEnumerable<StructureDefinitionSchemaWalker> me, string name) =>
            me.SelectMany(w => w.Slice(name));
    }
}
