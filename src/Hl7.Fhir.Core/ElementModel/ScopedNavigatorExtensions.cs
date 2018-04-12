/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;


namespace Hl7.Fhir.ElementModel
{
    public static class ScopedNavigatorExtensions
    {
        public static string MakeAbsolute(this ScopedNavigator nav, string reference) =>
                     nav.MakeAbsolute(new ResourceIdentity(reference)).ToString();

        /// <summary>
        /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
        /// </summary>
        /// <param name="nav"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
        public static ResourceIdentity MakeAbsolute(this ScopedNavigator nav, ResourceIdentity identity)
        {

            if (identity.IsRelativeRestUrl)
            {
                // Relocate the relative url on the base given in the fullUrl of the entry (if applicable)
                var fullUrl = nav.FullUrl();

                if (fullUrl != null)
                {
                    var parentIdentity = new ResourceIdentity(fullUrl);

                    if (parentIdentity.IsAbsoluteRestUrl)
                        identity = identity.WithBase(parentIdentity.BaseUri);
                    else if(parentIdentity.IsUrn)
                        identity = new ResourceIdentity($"{parentIdentity}/{identity.Id}");
                }

                // Return the identity - will remain relative if we did not find a fullUrl              
            }

            return identity;
        }

        public static T Resolve<T>(this T nav, string reference, Func<string, T> externalResolver = null) where T:class,IElementNavigator
        {
            // Then, resolve the url within the instance data first - this is only
            // possibly if we have a ScopedNavigator at hand
            var scopedNav = nav as ScopedNavigator;
            if (scopedNav != null)
            {
                var identity = scopedNav.MakeAbsolute(new ResourceIdentity(reference));

                if (identity.IsLocal || identity.IsAbsoluteRestUrl || identity.IsUrn)
                {
                    var result = locateResource(identity);
                    if (result != null) return (T)(object)result;
                }
            }

            // Nothing found internally, now try the external resolver
            if (externalResolver != null)
                return externalResolver(reference);
            else
                return null;

            ScopedNavigator locateResource(ResourceIdentity identity)
            {
                var url = identity.ToString();

                foreach (var parent in scopedNav.Parents())
                {
                    if (parent.AtBundle)
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
        /// Where this item is a reference, resolve it to an actual resource, and return that
        /// </summary>
        /// <param name="nav"></param>
        /// <param name="externalResolver"></param>
        /// <returns></returns>
        public static T Resolve<T>(this T nav, Func<string, T> externalResolver=null) where T:class,IElementNavigator
        {
            // First, get the url to fetch from the focus
            string url = null;

            if (nav.Type == FHIRDefinedType.String.GetLiteral() && nav.Value is string s)
                url = s;
            else if (nav.Type == FHIRDefinedType.Reference.GetLiteral())
                url = nav.ParseResourceReference()?.Reference;

            if (url == null) return default(T);   // nothing found to resolve

            return Resolve(nav, url, externalResolver);
        }


    }
}
