/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

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
        public IResourceResolver? Resolver => _resolver as IResourceResolver;
        public IAsyncResourceResolver AsyncResolver => _resolver.AsAsync();

#pragma warning disable CS0618 // Type or member is obsolete
        private readonly ISyncOrAsyncResourceResolver _resolver;
#pragma warning restore CS0618 // Type or member is obsolete

        public readonly ElementDefinitionNavigator Current;

        /// <summary>
        /// When walked into a Reference, the targetprofiles are copied to here
        /// </summary>
        private readonly string[]? _targetProfile;

#pragma warning disable CS0618 // Type or member is obsolete
        public StructureDefinitionWalker(StructureDefinition sd, ISyncOrAsyncResourceResolver resolver)
#pragma warning restore CS0618 // Type or member is obsolete
            : this(ElementDefinitionNavigator.ForSnapshot(sd), resolver)
        {
            // nothing more
        }

#pragma warning disable CS0618 // Type or member is obsolete
        public StructureDefinitionWalker(ElementDefinitionNavigator element, ISyncOrAsyncResourceResolver resolver)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            Current = element.ShallowCopy();
            _resolver = resolver;

            // Make sure there is always a current item
            if (Current.AtRoot) Current.MoveToFirstChild();
        }

#pragma warning disable CS0618 // Type or member is obsolete
        public StructureDefinitionWalker(ElementDefinitionNavigator element, IEnumerable<string> targetProfiles, ISyncOrAsyncResourceResolver resolver) :
#pragma warning restore CS0618 // Type or member is obsolete
            this(element, resolver)
        {
            _targetProfile = targetProfiles?.ToArray();
        }

        public StructureDefinitionWalker(StructureDefinitionWalker other)
        {
            Current = other.Current.ShallowCopy();
            _resolver = other._resolver;
        }

        public StructureDefinitionWalker FromCanonical(string canonical, IEnumerable<string>? targetProfiles = null) =>
            TaskHelper.Await(() => FromCanonicalAsync(canonical, targetProfiles));

        public async T.Task<StructureDefinitionWalker> FromCanonicalAsync(string canonical, IEnumerable<string>? targetProfiles = null)
        {
            var sd = await AsyncResolver.FindStructureDefinitionAsync(canonical).ConfigureAwait(false);
            if (sd == null)
                throw new StructureDefinitionWalkerException($"Cannot walk into unknown StructureDefinition with canonical '{canonical}' at '{Current.CanonicalPath()}'");

            return (targetProfiles is not null)
                ? new StructureDefinitionWalker(ElementDefinitionNavigator.ForSnapshot(sd), targetProfiles, _resolver)
                : new StructureDefinitionWalker(ElementDefinitionNavigator.ForSnapshot(sd), _resolver);
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

            var definitions = childDefinitions(childName).ToList();

            if (definitions.Count == 0)
                throw new StructureDefinitionWalkerException($"Cannot walk into unknown child '{childName}' at '{Current.CanonicalPath()}'.");
            else if (definitions.Count == 1) // Single element, no slice
                return new StructureDefinitionWalker(definitions.Single(), _resolver);
            else if (definitions.Count == 2) // element with an entry + single slice
                return new StructureDefinitionWalker(definitions[1], _resolver);
            else
                throw new StructureDefinitionWalkerException($"Child with name '{childName}' is sliced to more than one choice and cannot be used as a discriminator at '{Current.CanonicalPath()}' ");
        }

        private IEnumerable<ElementDefinitionNavigator> childDefinitions(string? childName = null)
        {
            var canonicals = Current.Current.Type.Select(t => t.GetTypeProfile()).Distinct().ToArray();
            if (canonicals.Length > 1)
                throw new StructureDefinitionWalkerException($"Cannot determine which child to select, since there are multiple paths leading from here ('{Current.CanonicalPath()}'), use 'ofType()' to disambiguate");

            // Take First(), since we have determined above that there's just one distinct result to expect.
            // (this will be the case when Type=R
            var expanded = Expand().Single();
            var nav = expanded.Current.ShallowCopy();

            if (!nav.MoveToFirstChild()) yield break;

            do
            {
                if (nav.Current.IsPrimitiveValueConstraint()) continue;      // ignore value attribute
                if (childName != null && nav.Current.MatchesName(childName)) yield return nav.ShallowCopy();
            }
            while (nav.MoveToNext());
        }


        /// <summary>
        /// Returns a set of walkers containing the children of the current node.
        /// </summary>
        /// <returns></returns>
        /// <remarks>There are three cases:
        /// 1. If the walker contains an ElementDefinition with children, it returns itself. 
        /// 2. If the ElementDefinition has a NameReference, it returns the node referred to by the namereference.
        /// 3. If not 1 or 2, it returns a set of walkers representing the type(s) the ElementDefinition refers to.
        /// 
        /// This order ensures that local ("inline") constraints for the children in the snapshot take
        /// precedence over following the type.profile link.
        /// </remarks>
        public IEnumerable<StructureDefinitionWalker> Expand()
        {
            if (Current.HasChildren)
                return new[] { this };
            else if (Current.Current.ContentReference != null)
            {
                var name = Current.Current.ContentReference;
                var reference = Current.ShallowCopy();

                if (!reference.JumpToNameReference(name))
                    throw new StructureDefinitionWalkerException($"Found a namereference '{name}' that cannot be resolved at '{Current.CanonicalPath()}'.");
                return new[] { new StructureDefinitionWalker(reference, _resolver) };
            }
            else if (Current.Current.Type.Count >= 1)
            {
                return Current.Current.Type
                    .GroupBy(t => t.GetTypeProfile(), t => t.TargetProfile)
                    .Select(group => FromCanonical(group.Key, group.SelectMany(g => g))); // no use returning multiple "reference" profiles when they only differ in targetReference
            }

            throw new StructureDefinitionWalkerException("Invalid StructureDefinition: element misses either a type reference or " +
                $"a value in ElementDefinition.contentReference at '{Current.CanonicalPath()}'");
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
            string? typeCanonical(ElementDefinitionNavigator nav) =>
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
            if (_targetProfile is not null)
            {
                return _targetProfile.Select(p => FromCanonical(p));
            }

            if (!Current.Current.Type.Any(t => t.IsReference()))
                throw new StructureDefinitionWalkerException($"resolve() should only be called on elements of type Reference at '{Current.CanonicalPath()}'.");

            return Current.Current.Type
                    .Where(t => t.IsReference() && t.TargetProfile.Any())
                    .SelectMany(t => t.TargetProfile)
                    .Select(c => FromCanonical(c));
        }

        /// <summary>
        /// Finds the constraints for a specific extension within the (sliced) extension collection
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <remarks>Note that this is yet another place where we need to deal with slices along the discriminator
        /// path. In this case we assume (but is this always true????) that the extension element is sliced so it
        /// can either point to a defined extension or contains inline child constraints on (probably the value element of)
        /// the referenced extension.
        /// </remarks>
        /*
           The elementdefs would look something like this:
         
            <element>
               <path value="Observation.identifier.extension"/>
               <slicing>
                <discriminator>
                    <type value="value"/>
                    <path value="url"/>
                </discriminator>
                <rules value="open"/>
               </slicing>
            </element>
            <element>
               <path value="Observation.identifier.extension"/>
               <sliceName value="myExtension"/>
               <type>
                  <code value="Extension"/>
                  <profile value="http://example.org/fhir/StructureDefinition/MyExtension"/>
               </type>
            </element>
            <element>
               <path value="Observation.identifier.extension.valueString"/>
               <fixedString value="hi!"/>
            </element> 
       */

        public StructureDefinitionWalker Extension(string url)
        {
            // find the extension children of the current node
            var extensionChildren = childDefinitions("extension").ToList();
            var selection = extensionChildren.Where(c => isExtensionFor(c, url)).ToList();

            return selection switch
            {
                // No children in the current profile -> we can still continue in the definition
                // of the extension itself.
                { Count: 0 } when isAbsoluteUri(url) => FromCanonical(url),

                // No matching child extensions at all -> invalid discriminator.
                { Count: 0 } => throw new StructureDefinitionWalkerException($"extension('{url}') found no extension slices constraining the same extension, with is not allowed for the discriminator at '{Current.CanonicalPath()}'."),

                { Count: 1 } => new StructureDefinitionWalker(selection.Single(), _resolver),

                // Too many matching child extensions, no discriminating power -> illegal
                _ => throw new StructureDefinitionWalkerException($"extension('{url}') found multiple extension slices constraining the same extension, with is not allowed for the discriminator at '{Current.CanonicalPath()}'.")
            };


            static bool isExtensionFor(ElementDefinitionNavigator nav, string u)
            {
                //  nav.Current.Type.Any(tr => tr.Code == FHIRAllTypes.Extension.GetLiteral() && tr.Profile == u);
                var childNav = nav.ShallowCopy();
                if (!childNav.MoveToChild("url")) return false;
                return childNav.Current.Fixed is FhirUri fu && fu.Value == u;
            }

            static bool isAbsoluteUri(string uri) =>
                 new Uri(uri, UriKind.RelativeOrAbsolute).IsAbsoluteUri;
        }
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

#nullable restore