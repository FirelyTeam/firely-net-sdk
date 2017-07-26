/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
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

        public static ScopedNavigator Resolve(this ScopedNavigator nav, string reference)
        {
            var identity = nav.MakeAbsolute(new ResourceIdentity(reference));

            if (identity.IsLocal || identity.IsAbsoluteRestUrl || identity.IsUrn)
                return locateResource();
            else
                return null;

            ScopedNavigator locateResource()
            {
                var url = identity.ToString();

                foreach (var parent in nav.Parents())
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


    }
}
