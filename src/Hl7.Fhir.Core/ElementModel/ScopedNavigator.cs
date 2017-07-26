/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class ScopedNavigator : IElementNavigator
    {
        private IElementNavigator _wrapped = null;

        public ScopedNavigator(IElementNavigator wrapped)
        {
            _wrapped = wrapped.Clone();
        }

        public ScopedNavigator Parent { get; private set; } = null;

        public string Name => _wrapped.Name;

        public string Type => _wrapped.Type;

        public object Value => _wrapped.Value;

        public string Location => _wrapped.Location;

        public bool MoveToNext(string nameFilter = null) => _wrapped.MoveToNext(nameFilter);

        public bool MoveToFirstChild(string nameFilter = null)
        {
            ScopedNavigator me = null;

            if (this.AtResource)
                me = (ScopedNavigator)this.Clone();

            if (!_wrapped.MoveToFirstChild(nameFilter)) return false;

            // If the current position is a resource, we'll be the new _parentScope
            if (me != null)
                Parent = me;

            return true;
        }

        public bool AtResource => Type != null ? Char.IsUpper(Type[0]) && ModelInfo.IsKnownResource(Type) : false;
        public bool AtBundle => Type != null ? Type == "Bundle" : false;

        public IEnumerable<ScopedNavigator> Parents()
        {
            var scan = this.Parent;

            while (scan != null)
            {
                yield return scan;

                scan = scan.Parent;
            }
        }

        //TODO: This could be cached
        public string Id() => AtResource ? "#" + _wrapped.Children("id").FirstOrDefault()?.Value as string : null;

        public IEnumerable<ScopedNavigator> ContainedResources()
        {
            //TODO: This could be cached
            if (AtResource)
                return this.Children("contained").Cast<ScopedNavigator>();
            else
                return Enumerable.Empty<ScopedNavigator>();
        }

        public class BundledResource
        {
            public string FullUrl;
            public ScopedNavigator Resource;
        }

        public IEnumerable<BundledResource> BundledResources()
        {
            //TODO: This could be cached
            if (AtBundle)
                return from e in this.Children("entry")
                       let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                       let resource = e.Children("resource").FirstOrDefault() as ScopedNavigator
                       select new BundledResource { FullUrl = fullUrl, Resource = resource };
            else
                return Enumerable.Empty<BundledResource>();
        }

        public string FullUrl()
        {
            foreach (var parent in Parents())
            {
                if(parent.AtBundle)
                {
                    var fullUrl = parent.BundledResources()
                        .SingleOrDefault(be => this.Location.StartsWith(be.Resource.Location))
                        ?.FullUrl;
                    if (fullUrl != null) return fullUrl;
                }
            }

            return null;
        }

        public IElementNavigator Clone()
        {
            return new ScopedNavigator(_wrapped)
            {
                Parent = this.Parent
            };
        }        
    }
}
