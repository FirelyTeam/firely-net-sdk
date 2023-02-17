/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Rest;
using Hl7.Fhir.Support.Poco;
using System;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A set of functions on <see cref="ScopedNode"/> (and <see cref="ITypedElement"/>) that
    /// help resolving references from one resource to another. This includes both resolving
    /// the reference within the resource (in en Bundle or contained resource) or reaching out
    /// to an external resolver.
    /// </summary>
    public static class ScopedNodeExtensions
    {
        /// <summary>
        /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
        /// </summary>
        /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
        public static ResourceIdentity MakeAbsolute(this ScopedNode node, ResourceIdentity identity)
        {
            if (identity.IsRelativeRestUrl)
            {
                // Relocate the relative url on the base given in the fullUrl of the entry (if applicable)
                var fullUrl = node.FullUrl();

                if (fullUrl != null)
                {
                    var parentIdentity = new ResourceIdentity(fullUrl);

                    if (parentIdentity.IsAbsoluteRestUrl)
                        identity = identity.WithBase(parentIdentity.BaseUri);
                    else if (parentIdentity.IsUrn)
                        identity = new ResourceIdentity($"{parentIdentity}/{identity.Id}");
                }

                // Return the identity - will remain relative if we did not find a fullUrl              
            }

            return identity;
        }

        /// <inheritdoc cref="MakeAbsolute(ScopedNode, ResourceIdentity)"/>
        public static string MakeAbsolute(this ScopedNode node, string reference) =>
             node.MakeAbsolute(new ResourceIdentity(reference)).ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="reference"></param>
        /// <param name="externalResolver"></param>
        /// <returns></returns>
        public static T? Resolve<T>(this T element, string reference, Func<string, T?>? externalResolver = null) where T : class, ITypedElement
        {
            // Then, resolve the url within the instance data first - this is only
            // possible if we have a ScopedNode at hand
            if (element is ScopedNode scopedNode)
            {
                // a special case for a reference to the container (in this parent) resource.
                // It should make sure that we're only resolving to the first parent that actually contains resources
                // of which the current element is a child.
                if (reference == "#")
                {
                    return (T?)(object?)locateContainer(scopedNode);
                }
                else
                {
                    var identity = scopedNode.MakeAbsolute(new ResourceIdentity(reference));
                    var result = locateLocalResource(identity);
                    if (result != null) return (T)(object)result;
                }
            }

            // Nothing found internally, now try the external resolver
            return externalResolver != null ? externalResolver(reference) : null;

            ScopedNode? locateContainer(ScopedNode containee)
            {
                var scan = containee;
                while (scan is not null)
                {
                    if (scan.ParentResource is ScopedNode parent)
                    {
                        if (parent.ContainedResources().Any(cr => cr.Location == scan.Location)) return parent;
                    }

                    scan = scan.ParentResource;
                }

                return null;
            }

            ScopedNode? locateLocalResource(ResourceIdentity identity)
            {
                var url = identity.ToString();

                foreach (var parent in scopedNode.ParentResources())
                {
                    if (parent.InstanceType == FhirTypeConstants.BUNDLE)
                    {
                        var result = parent.BundledResources().FirstOrDefault(br => br.FullUrl == url)?.Resource;
                        if (result != null) return result;
                    }
                    else
                    {
                        if (parent.Id() == url) return parent;
                        var result = parent.ContainedResources().FirstOrDefault(cr => cr.Id() == url);
                        if (result != null) return result;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Where this element is a Reference datatype, get the reference from it and resolve it.
        /// </summary>
        public static T? Resolve<T>(this T element, Func<string, T?>? externalResolver = null) where T : class, ITypedElement
        {
            if (element is null) return default;

            // First, get the url to fetch from the focus
            string? url = element switch
            {
                { Value: string s } => s,
                { InstanceType: FhirTypeConstants.REFERENCE } => element.ParseResourceReference()?.Reference,
                _ => null
            };

            return url is not null ? Resolve(element, url, externalResolver) : default;
        }
    }
}