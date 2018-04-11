/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
        private class Cache
        {
            public string Id;
            public IEnumerable<ScopedNavigator> ContainedResources;
            public IEnumerable<BundledResource> BundledResources;
            public string FullUrl;
        }
        private Cache _cache = new Cache();

        private IElementNavigator _wrapped = null;

        public ScopedNavigator(IElementNavigator wrapped)
        {
            _wrapped = wrapped.Clone();
            if (this.AtResource) ResourceContext = wrapped.Clone();
        }

        public ScopedNavigator Parent { get; private set; } = null;

        public string Name => _wrapped.Name;

        public string Type => _wrapped.Type;

        public object Value => _wrapped.Value;

        public string Location => _wrapped.Location;

        public bool MoveToNext(string nameFilter = null)
        {
            _cache = new Cache();
            return _wrapped.MoveToNext(nameFilter);
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            ScopedNavigator me = null;

            if (this.AtResource)
            {
                me = (ScopedNavigator)this.Clone();

                // Is the current position not a contained resource?
                if (Parent?.ContainedResources().FirstOrDefault() == null)
                {
                    ResourceContext = _wrapped.Clone();
                }
            }

            if (!_wrapped.MoveToFirstChild(nameFilter)) return false;

            // If the current position is a resource, we'll be the new _parentScope
            if (me != null)
                Parent = me;

            _cache = new Cache();

            return true;
        }

        public bool AtResource => Type != null ? Char.IsUpper(Type[0]) && ModelInfo.IsKnownResource(Type) : false;
        public bool AtBundle => Type != null ? Type == "Bundle" : false;

        public IElementNavigator ResourceContext { get; private set; } = null;

        public IEnumerable<ScopedNavigator> Parents()
        {
            var scan = this.Parent;

            while (scan != null)
            {
                yield return scan;

                scan = scan.Parent;
            }
        }

        public string Id()
        {
            if (_cache.Id == null)
            {
                _cache.Id = AtResource ? "#" + _wrapped.Children("id").FirstOrDefault()?.Value as string : null;
            }

            return _cache.Id;
        }

        public IEnumerable<ScopedNavigator> ContainedResources()
        {
            if (_cache.ContainedResources == null)
            {
                if (AtResource)
                    _cache.ContainedResources = this.Children("contained").Cast<ScopedNavigator>();
                else
                    _cache.ContainedResources = Enumerable.Empty<ScopedNavigator>();
            }
            return _cache.ContainedResources;
        }

        public class BundledResource
        {
            public string FullUrl;
            public ScopedNavigator Resource;
        }

        public IEnumerable<BundledResource> BundledResources()
        {
            if (_cache.BundledResources == null)
            {
                if (AtBundle)
                    _cache.BundledResources = from e in this.Children("entry")
                                              let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                                              let resource = e.Children("resource").FirstOrDefault() as ScopedNavigator
                                              select new BundledResource { FullUrl = fullUrl, Resource = resource };
                else
                    _cache.BundledResources = Enumerable.Empty<BundledResource>();
            }

            return _cache.BundledResources;
        }

        public string FullUrl()
        {
            if (_cache.FullUrl == null)
            {
                foreach (var parent in Parents())
                {
                    if (parent.AtBundle)
                    {
                        var fullUrl = parent.BundledResources()
                            .SingleOrDefault(be => this.Location.StartsWith(be.Resource.Location))
                            ?.FullUrl;
                        if (fullUrl != null) _cache.FullUrl = fullUrl;
                    }
                }
            }

            return _cache.FullUrl;
        }

        public IElementNavigator Clone()
        {
            return new ScopedNavigator(_wrapped)
            {
                Parent = this.Parent,
                ResourceContext = this.ResourceContext
            };
        }
    }
}
